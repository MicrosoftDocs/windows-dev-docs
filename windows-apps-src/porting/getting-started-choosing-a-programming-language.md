---
title: Choosing a programming language
ms.assetid: 6CA46432-BF03-4B20-9187-565B3503B497
description: Learn what programming languages you can choose from when you develop Universal Windows Platform (UWP) apps.
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Getting started: Choosing a programming language

## Choosing a programming language

Before we go any further, you should know about the programming languages that you can choose from when you develop Universal Windows Platform (UWP) apps. Although the walkthroughs in this article use C#, you can develop UWP apps using one or more programming languages (see [Languages, tools and frameworks](/previous-versions/windows/apps/dn465799(v=win.10))).

You can develop using C++, C#, Microsoft Visual Basic, and JavaScript. JavaScript uses HTML5 markup for UI layout, and the other languages use a markup language called *Extensible Application Markup Language (XAML)* to describe their UI.

Although we're focusing on C# in this article, the other languages offer unique benefits, which you may want to explore. For example, if your app's performance is a primary concern, especially for intensive graphics, then C++ might be the right choice. The Microsoft .NET version of Visual Basic is great for Visual Basic app developers. JavaScript with HTML5 is great for those coming from a web development background. For more info, see one of the following:

-   [Create a "Hello, World!" app using C# or Visual Basic](../get-started/create-a-hello-world-app-xaml-universal.md)
-   [Create a "Hello, World!" app using C++/WinRT](../get-started/create-a-basic-windows-10-app-in-cppwinrt.md)
-   [Create a "Hello, World!" app using C++/CX](../get-started/create-a-basic-windows-10-app-in-cpp.md)
-   [Create a "Hello, World!" app using JavaScript](../get-started/create-a-hello-world-app-js-uwp.md)

**Note**  For apps that use 3D graphics, the OpenGL and OpenGL ES standards are not natively available for UWP apps. If you would rather not rewrite your OpenGL ES code into Microsoft DirectX, you may be interested to know about **Angle**. Angle is an on-going project designed to convert OpenGL to DirectX by translating OpenGL API calls into DirectX API calls. To learn more, see the following:
-   [Angle](https://bugs.chromium.org/p/angleproject/)
-   [Create your first UWP app using DirectX](/previous-versions/windows/apps/br229580(v=win.10))
-   [UWP app samples that use DirectX](/samples/browse/?expanded=windows&products=windows-uwp&terms=directx)
-   [Where is the DirectX SDK?](/windows/desktop/directx-sdk--august-2009-)

## Giving C# a go

As an iOS developer, you're accustomed to Objective-C and Swift. The closest Microsoft programming language to both is C#. For most developers and most apps, we think C# is the easiest and fastest language to learn and use, so this article's info and walkthroughs focus on that language. To learn more about C#, see the following:

-   [Create your first UWP app using C# or Visual Basic](../get-started/create-a-hello-world-app-xaml-universal.md)
-   [UWP app samples that use C#](/samples/browse/?expanded=windows&languages=csharp&products=windows-uwp)
-   [Visual C#](/dotnet/csharp/)

Following is a class written in Objective-C and C#. The Objective-C version is shown first, followed by the C# version.

```obj-c
// Objective-C header: SampleClass.h.

#import <Foundation/Foundation.h>

@interface SampleClass : NSObject {
    BOOL localVariable;
}

@property (nonatomic) BOOL localVariable;

-(int) addThis: (int) firstNumber andThis: (int) secondNumber;

@end
```

```obj-c
// Objective-C implementation.

#import "SampleClass.h"

@implementation SampleClass

@synthesize localVariable = _localVariable;

- (id)init {
    self = [super init];
    if (self) {
        localVariable = true;
    }
    return self;
}

-(int) addThis: (int) firstNumber andThis: (int) secondNumber {
    return firstNumber + secondNumber;
}

@end
```

```obj-c
// Objective-C usage.

SampleClass *mySampleClass = [[SampleClass alloc] init];
mySampleClass.localVariable = false;
int result = [mySampleClass addThis:1 andThis:2];
```

Now, for the C# version. You'll see that like Swift, the header and the implementation are not in separate files.

```csharp
// C# header and implementation.

using System;

namespace MyApp  // Defines this code' s scope.
{
    class SampleClass
    {
        private bool localVariable;

        public SampleClass() // Constructor.
        {
            localVariable = true;
        }

        public bool myLocalVariable // Property.
        {
            get
            {
                return localVariable;
            }
            set
            {
                localVariable = value; 
            }
        }

        public int AddTwoNumbers(int numberOne, int numberTwo)
        {
            return numberOne + numberTwo;
        }        
    }
}
```

```csharp
// C# usage.

SampleClass mySampleClass = new SampleClass();
mySampleClass.myLocalVariable = false;
int result = mySampleClass.AddTwoNumbers(1, 2);
```

C# is an easy language to pick up, and comes with the many support classes and frameworks that make up .NET. In no time, you'll be happily writing your code without a square bracket in sight!

## Next step

[Getting started: Getting around in Visual Studio](getting-started-getting-around-in-visual-studio.md)