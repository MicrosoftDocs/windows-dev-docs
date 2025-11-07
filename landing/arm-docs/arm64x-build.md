---
title: Build Arm64X Files
description: Build Arm64X files for situations where one file is loaded into both x64/Arm64EC and Arm64 processes.
ms.date: 11/06/2025
ms.topic: how-to
ms.service: windows
ms.subservice: arm
---

# Build Arm64X binaries

You can build Arm64X binaries, also known as [Arm64X PE files](./arm64x-pe.md), to support loading a single binary into both x64/Arm64EC and Arm64 processes.  

## Build an Arm64X binary from a Visual Studio project

To enable building Arm64X binaries, the property pages of Arm64EC configuration has a new "Build Project as ARM64X" property, known as `BuildAsX` in the project file.

![Property page for an Arm64EC configuration showing the Build Project as ARM64X option](./images/arm64x-build.png)

When you build a project, Visual Studio normally compiles for Arm64EC and then links the outputs into an Arm64EC binary. When you set `BuildAsX` to `true`, Visual Studio compiles for both Arm64EC **and** Arm64. The Arm64EC link step links both outputs into a single Arm64X binary. The output directory for this Arm64X binary is the output directory set under the [Arm64EC configuration](./arm64ec-build.md).

For `BuildAsX` to work correctly, you must have an existing Arm64 configuration, in addition to the Arm64EC configuration. The Arm64 and Arm64EC configurations must use the same C runtime and C++ standard library (for example, both set to [/MT](/cpp/c-runtime-library/crt-library-features)). To avoid build inefficiencies, such as building full Arm64 projects rather than just compilation, set `BuildAsX` to true for all direct and indirect references of the project.

The build system assumes that the Arm64 and Arm64EC configurations have the same name. If the Arm64 and Arm64EC configurations have different names (such as `Debug|ARM64` and `MyDebug|ARM64EC`), you can manually edit the [vcxproj](/cpp/build/reference/vcxproj-file-structure) or `Directory.Build.props` file to add an `ARM64ConfigurationNameForX` property to the Arm64EC configuration that provides the name of the Arm64 configuration.

If you want the Arm64X binary to combine two separate projects, one as Arm64 and one as Arm64EC, you can manually edit the vxcproj of the Arm64EC project to add an `ARM64ProjectForX` property and specify the path to the Arm64 project. The two projects must be in the same solution.

## Building an Arm64X DLL with CMake

To build your CMake project binaries as Arm64X, use any version of [CMake](https://cmake.org/documentation/) that supports building as Arm64EC. First, build the project targeting Arm64 to generate the Arm64 linker inputs. Then, build the project again targeting Arm64EC, combining the Arm64 and Arm64EC inputs to form Arm64X binaries. The following steps show how to use [CMakePresets.json](/cpp/build/cmake-presets-vs).

1. Ensure you have separate configuration presets targeting Arm64 and Arm64EC. For example:

	```JSON
	{
	  "version": 3,
	  "configurePresets": [
	    {
	      "name": "windows-base",
	      "hidden": true,
	      "binaryDir": "${sourceDir}/out/build/${presetName}",
	      "installDir": "${sourceDir}/out/install/${presetName}",
	      "cacheVariables": {
	        "CMAKE_C_COMPILER": "cl.exe",
	        "CMAKE_CXX_COMPILER": "cl.exe"
	      },
		  "generator": "Visual Studio 17 2022",
	    },
	    {
	      "name": "arm64-debug",
	      "displayName": "arm64 Debug",
	      "inherits": "windows-base",
	      "hidden": true,
		  "architecture": {
			 "value": "arm64",
			 "strategy": "set"
		  },
	      "cacheVariables": {
	        "CMAKE_BUILD_TYPE": "Debug"
	      }
	    },
	    {
	      "name": "arm64ec-debug",
	      "displayName": "arm64ec Debug",
	      "inherits": "windows-base",
	      "hidden": true,
	      "architecture": {
	        "value": "arm64ec",
	        "strategy": "set"
	      },
	      "cacheVariables": {
	        "CMAKE_BUILD_TYPE": "Debug"
	      }
	    }
	  ]
	}
	```

1. Add two new configurations that inherit from the Arm64 and Arm64EC presets you created in the previous step. Set `BUILD_AS_ARM64X` to `ARM64EC` in the config that inherits from Arm64EC and `BUILD_AS_ARM64X` to `ARM64` in the other. These variables signify that the builds from these two presets are part of Arm64X.

	 ```JSON
	    {
	      "name": "arm64-debug-x",
	      "displayName": "arm64 Debug (arm64x)",
	      "inherits": "arm64-debug",
	      "cacheVariables": {
	        "BUILD_AS_ARM64X": "ARM64"
	      }
            },
	    {
	      "name": "arm64ec-debug-x",
	      "displayName": "arm64ec Debug (arm64x)",
	      "inherits": "arm64ec-debug",
	      "cacheVariables": {
	        "BUILD_AS_ARM64X": "ARM64EC"
	      }
            }
	```

1. Add a new .cmake file to your CMake project called `arm64x.cmake`. Copy the following snippet into the new .cmake file.

	```cmake
	# directory where the link.rsp file generated during arm64 build will be stored
	set(arm64ReproDir "${CMAKE_CURRENT_SOURCE_DIR}/repros")
	
	# This function reads in the content of the rsp file outputted from arm64 build for a target. Then passes the arm64 libs, objs and def file to the linker using /machine:arm64x to combine them with the arm64ec counterparts and create an arm64x binary.
	
	function(set_arm64_dependencies n)
		set(REPRO_FILE "${arm64ReproDir}/${n}.rsp")
		file(STRINGS "${REPRO_FILE}" ARM64_OBJS REGEX obj\"$)
		file(STRINGS "${REPRO_FILE}" ARM64_DEF REGEX def\"$)
		file(STRINGS "${REPRO_FILE}" ARM64_LIBS REGEX lib\"$)
		string(REPLACE "\"" ";" ARM64_OBJS "${ARM64_OBJS}")
		string(REPLACE "\"" ";" ARM64_LIBS "${ARM64_LIBS}")
		string(REPLACE "\"" ";" ARM64_DEF "${ARM64_DEF}")
		string(REPLACE "/def:" "/defArm64Native:" ARM64_DEF "${ARM64_DEF}")
	
		target_sources(${n} PRIVATE ${ARM64_OBJS})
		target_link_options(${n} PRIVATE /machine:arm64x "${ARM64_DEF}" "${ARM64_LIBS}")
	endfunction()
	
	# During the arm64 build, create link.rsp files that containes the absolute path to the inputs passed to the linker (objs, def files, libs).
	
	if("${BUILD_AS_ARM64X}" STREQUAL "ARM64")
		add_custom_target(mkdirs ALL COMMAND cmd /c (if not exist \"${arm64ReproDir}/\" mkdir \"${arm64ReproDir}\" ))
		foreach (n ${ARM64X_TARGETS})
			add_dependencies(${n} mkdirs)
			# tell the linker to produce this special rsp file that has absolute paths to its inputs
			target_link_options(${n} PRIVATE "/LINKREPROFULLPATHRSP:${arm64ReproDir}/${n}.rsp")
		endforeach()
	
	# During the ARM64EC build, modify the link step appropriately to produce an arm64x binary
	elseif("${BUILD_AS_ARM64X}" STREQUAL "ARM64EC")
		foreach (n ${ARM64X_TARGETS})
			set_arm64_dependencies(${n})
		endforeach()
	endif()
	```

[/LINKREPROFULLPATHRSP](/cpp/build/reference/link-repro-full-path-rsp) is only supported if you build by using the MSVC linker from Visual Studio 17.11 or later.

If you need to use an older linker, copy the following snippet instead. This route uses an older flag [/LINK_REPRO](/cpp/build/reference/linkrepro). Using the /LINK_REPRO route results in a slower overall build time due to the copying of files and has known issues when using Ninja generator.

```cmake
# directory where the link_repro directories for each arm64x target will be created during arm64 build.
set(arm64ReproDir "${CMAKE_CURRENT_SOURCE_DIR}/repros")

# This function globs the linker input files that was copied into a repro_directory for each target during arm64 build. Then it passes the arm64 libs, objs and def file to the linker using /machine:arm64x to combine them with the arm64ec counterparts and create an arm64x binary.

function(set_arm64_dependencies n)
	set(ARM64_LIBS)
	set(ARM64_OBJS)
	set(ARM64_DEF)
	set(REPRO_PATH "${arm64ReproDir}/${n}")
	if(NOT EXISTS "${REPRO_PATH}")
		set(REPRO_PATH "${arm64ReproDir}/${n}_temp")
	endif()
	file(GLOB ARM64_OBJS "${REPRO_PATH}/*.obj")
	file(GLOB ARM64_DEF "${REPRO_PATH}/*.def")
	file(GLOB ARM64_LIBS "${REPRO_PATH}/*.LIB")

	if(NOT "${ARM64_DEF}" STREQUAL "")
		set(ARM64_DEF "/defArm64Native:${ARM64_DEF}")
	endif()
	target_sources(${n} PRIVATE ${ARM64_OBJS})
	target_link_options(${n} PRIVATE /machine:arm64x "${ARM64_DEF}" "${ARM64_LIBS}")
endfunction()

# During the arm64 build, pass the /link_repro flag to linker so it knows to copy into a directory, all the file inputs needed by the linker for arm64 build (objs, def files, libs).
# extra logic added to deal with rebuilds and avoiding overwriting directories.
if("${BUILD_AS_ARM64X}" STREQUAL "ARM64")
	foreach (n ${ARM64X_TARGETS})
		add_custom_target(mkdirs_${n} ALL COMMAND cmd /c (if exist \"${arm64ReproDir}/${n}_temp/\" rmdir /s /q \"${arm64ReproDir}/${n}_temp\") && mkdir \"${arm64ReproDir}/${n}_temp\" )
		add_dependencies(${n} mkdirs_${n})
		target_link_options(${n} PRIVATE "/LINKREPRO:${arm64ReproDir}/${n}_temp")
		add_custom_target(${n}_checkRepro ALL COMMAND cmd /c if exist \"${n}_temp/*.obj\" if exist \"${n}\" rmdir /s /q \"${n}\" 2>nul && if not exist \"${n}\" ren \"${n}_temp\" \"${n}\" WORKING_DIRECTORY ${arm64ReproDir})
		add_dependencies(${n}_checkRepro ${n})
	endforeach()

# During the ARM64EC build, modify the link step appropriately to produce an arm64x binary
elseif("${BUILD_AS_ARM64X}" STREQUAL "ARM64EC")
	foreach (n ${ARM64X_TARGETS})
		set_arm64_dependencies(${n})
	endforeach()
endif()
```

1. At the bottom of the top level `CMakeLists.txt` file in your project, add the following snippet. Be sure to substitute the contents of the angle brackets with actual values. This step consumes the `arm64x.cmake` file you just created.

	```cmake
	if(DEFINED BUILD_AS_ARM64X)
		set(ARM64X_TARGETS <Targets you want to Build as ARM64X>)
		include("<directory location of the arm64x.cmake file>/arm64x.cmake")
	endif()
	```

1. Build your CMake project by using the Arm64X enabled Arm64 preset (arm64-debug-x).

1. Build your CMake project by using the Arm64X enabled Arm64EC preset (arm64ec-debug-x). The final DLLs in the output directory for this build are Arm64X binaries.

## Building an Arm64X pure forwarder DLL

An **Arm64X pure forwarder DLL** is a small Arm64X DLL that forwards APIs to separate DLLs depending on their type:

- Arm64 APIs are forwarded to an Arm64 DLL.

- x64 APIs are forwarded to an x64 or Arm64EC DLL.

An Arm64X pure forwarder enables the advantages of using an Arm64X binary even if there are challenges with building a merged Arm64X binary containing all of the Arm64EC and Arm64 code. For more information, see [Arm64X PE files](./arm64x-pe.md).

You can build an Arm64X pure forwarder from the Arm64 developer command prompt by following the steps below. The resulting Arm64X pure forwarder routes x64 calls to `foo_x64.DLL` and Arm64 calls to `foo_arm64.DLL`.

1. Create empty `OBJ` files that the linker uses to create the pure forwarder. These files are empty because the pure forwarder contains no code. To create these files, create an empty file. In the following example, the file is named **empty.cpp**. Use `cl` to create empty `OBJ` files, with one for Arm64 (`empty_arm64.obj`) and one for Arm64EC (`empty_x64.obj`):

    ```batch
    cl /c /Foempty_arm64.obj empty.cpp
    cl /c /arm64EC /Foempty_x64.obj empty.cpp
    ```
    
   > If you see the error message "cl : Command line warning D9002 : ignoring unknown option '-arm64EC'", you're using the wrong compiler. To fix this issue, switch to [the Arm64 Developer Command Prompt](https://devblogs.microsoft.com/cppblog/arm64ec-support-in-visual-studio/#developer-command-prompt).

1. Create `DEF` files for both x64 and Arm64. These files list all of the API exports of the DLL and point the loader to the name of the DLL that can fulfill those API calls.

    `foo_x64.def`:

    ```cpp
    EXPORTS
        MyAPI1  =  foo_x64.MyAPI1
        MyAPI2  =  foo_x64.MyAPI2
    ```

    `foo_arm64.def`:

    ```cpp
    EXPORTS
        MyAPI1  =  foo_arm64.MyAPI1
        MyAPI2  =  foo_arm64.MyAPI2
    ```

1. Use `link` to create `LIB` import files for both x64 and Arm64:

    ```batch
    link /lib /machine:x64 /def:foo_x64.def /out:foo_x64.lib
    link /lib /machine:arm64 /def:foo_arm64.def /out:foo_arm64.lib
    ```

1. Link the empty `OBJ` and import `LIB` files by using the flag `/MACHINE:ARM64X` to produce the Arm6X pure forwarder DLL:

    ```cpp
    link /dll /noentry /machine:arm64x /defArm64Native:foo_arm64.def /def:foo_x64.def empty_arm64.obj empty_x64.obj /out:foo.dll foo_arm64.lib foo_x64.lib
    ```

The resulting `foo.dll` can be loaded into either an Arm64 or an x64/Arm64EC process. When an Arm64 process loads `foo.dll`, the operating system immediately loads `foo_arm64.dll` in its place and any API calls are handled by `foo_arm64.dll`.
