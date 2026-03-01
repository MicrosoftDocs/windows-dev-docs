---
title: Understanding Arm64EC ABI and assembly code
description: An in-depth look at Arm64EC ABI, register mapping, call checkers, stack checkers, variadic calling, entry thunks, exit thunks, and adjuster thunks, fast-forward sequences, authoring Arm64EC in Assembly, and dynamically generating (JIT Compiling) Arm64EC code. 
ms.date: 11/06/2025
ms.topic: concept-article
ms.service: windows
ms.subservice: arm
---

# Understanding Arm64EC ABI and assembly code

Arm64EC ("Emulation Compatible") is a new application binary interface (ABI) for building apps for Windows 11 on Arm. For an overview of Arm64EC and how to start building Win32 apps as Arm64EC, see [Using Arm64EC to build apps for Windows 11 on Arm devices](./arm64ec.md).

This article provides a detailed view of the Arm64EC ABI with enough information for an application developer to write and debug code compiled for Arm64EC, including low-level/assembler debugging and writing assembly code targeting the Arm64EC ABI.

## Design of Arm64EC

Arm64EC delivers native-level functionality and performance, while providing transparent and direct interoperability with x64 code running under emulation.

Arm64EC is mostly additive to the Classic Arm64 ABI. The Classic ABI changed very little, but the Arm64EC ABI added portions to enable x64 interoperability.

In this document, the original, standard Arm64 ABI is referred to as "Classic ABI". This term avoids the ambiguity inherent to overloaded terms like "Native". Arm64EC is every bit as native as the original ABI.

## Arm64EC vs. Arm64 Classic ABI

The following list points out where Arm64EC diverges from Arm64 Classic ABI.

- [Register mapping and blocked registers](#register-mapping-and-blocked-registers)
- [Call checkers](#call-checkers)
- [Stack checkers](#stack-checkers)
- [Variadic calling convention](#variadic-calling-convention)

These differences are small changes when seen in perspective of how much the entire ABI defines.

### Register mapping and blocked registers

To enable type-level interoperability with x64 code, Arm64EC code compiles with the same preprocessor architecture definitions as x64 code.

In other words, `_M_AMD64` and `_AMD64_` are defined. One of the types affected by this rule is the `CONTEXT` structure. The `CONTEXT` structure defines the state of the CPU at a given point. It is used for things like `Exception Handling` and `GetThreadContext` APIs. Existing x64 code expects the CPU context to be represented as an x64 `CONTEXT` structure or, in other words, the `CONTEXT` structure as it is defined during x64 compilation.

You must use this structure to represent the CPU context while executing x64 code and Arm64EC code. Existing code doesn't understand a novel concept, such as the CPU register set changing from function to function. If you use the x64 `CONTEXT` structure to represent Arm64 execution states, you effectively map Arm64 registers into x64 registers.

This mapping also means you can't use any Arm64 registers that don't fit into the x64 `CONTEXT`. Their values can be lost anytime an operation uses `CONTEXT` (and some operations can be asynchronous and unexpected, such as the Garbage Collection operation of a Managed Language Runtime, or an APC).

The Windows headers in the SDK represent the mapping rules between Arm64EC and x64 registers with the `ARM64EC_NT_CONTEXT` structure. This structure is essentially a union of the `CONTEXT` structure, exactly as it's defined for x64, but with an extra Arm64 register overlay.

For example, `RCX` maps to `X0`, `RDX` to `X1`, `RSP` to `SP`, `RIP` to `PC`, and so on. The registers `x13`, `x14`, `x23`, `x24`, `x28`, and `v16` through `v31` have no representation and, thus, can't be used in Arm64EC.

This register usage restriction is the first difference between the Arm64 Classic and EC ABIs.

### Call checkers

Call checkers have been a part of Windows ever since [Control Flow Guard (CFG)](/windows/win32/secbp/control-flow-guard) was introduced in Windows 8.1. Call checkers are address sanitizers for function pointers (before these things were called address sanitizers). Every time you compile code with the option `/guard:cf`, the compiler generates an extra call to the checker function just before every indirect call or jump. Windows provides the checker function itself. For CFG, it performs a validity check against the known-to-be-good call targets. Binaries compiled with `/guard:cf` also include this information.

This example shows a call checker use in Classic Arm64:

```
mov     x15, <target>
adrp    x16, __guard_check_icall_fptr
ldr     x16, [x16, __guard_check_icall_fptr]
blr     x16                                     ; check target function
blr     x15                                     ; call function
```

In the CFG case, the call checker simply returns if the target is valid, or fast-fails the process if it isn't. Call checkers have custom calling conventions. They take the function pointer in a register not used by the normal calling convention and preserve all normal calling-convention registers. This way, they don't introduce register spillage around them.

Call checkers are optional on all other Windows ABIs, but mandatory on Arm64EC. On Arm64EC, call checkers accumulate the task of verifying the architecture of the function being called. They verify whether the call is another EC ("Emulation Compatible") function or an x64 function that must be executed under emulation. In many cases, this can only be verified at runtime.

Arm64EC call checkers build on top of the existing Arm64 checkers, but they have a slightly different custom calling convention. They take an extra parameter and they may modify the register containing the target address. For example, if the target is x64 code, control must be transferred to the emulation scaffolding logic first.

In Arm64EC, the same call checker use would become:

```
mov     x11, <target>
adrp    x9, __os_arm64x_check_icall_cfg
ldr     x9, [x9, __os_arm64x_check_icall_cfg] 
adrp    x10, <name of the exit thunk>
add     x10, x10, <name of the exit thunk>
blr     x9                                      ; check target function
blr     x11                                     ; call function
```

Slight differences from Classic Arm64 include:

- Symbol name for the call checker is different.
- The target address is supplied in `x11` instead of `x15`.
- The target address (`x11`) is `[in, out]` instead of `[in]`.
- There is an extra parameter, provided through `x10`, called an "Exit Thunk".

An [Exit Thunk](#exit-thunks) is a funclet which transforms function parameters from Arm64EC calling convention to x64 calling convention.

The Arm64EC call checker is located through a different symbol than is used for the other ABIs in Windows. On the Classic Arm64 ABI, the symbol for the call checker is `__guard_check_icall_fptr`. This symbol will be present in Arm64EC, but it is there for x64 statically linked code to use, not Arm64EC code itself. Arm64EC code will use either `__os_arm64x_check_icall` or `__os_arm64x_check_icall_cfg`.

On Arm64EC, call checkers are not optional. However, CFG is still optional, as is the case for other ABIs. CFG may be disabled at compile-time, or there may be a legitimate reason to not perform a CFG check even when CFG is enabled (e.g. the function pointer never resides in RW memory). For an indirect call with CFG check, the `__os_arm64x_check_icall_cfg` checker should be used. If CFG is disabled or unnecessary, `__os_arm64x_check_icall` should be used instead.

Below is a summary table of the call checker usage on Classic Arm64, x64 and Arm64EC noting the fact that an Arm64EC binary can have two options depending on the architecture of the code.

|Binary  |Code   |Unprotected indirect call |CFG protected indirect call|
|--------------|---------|---------------|------------------------------------------------------------|
|x64           |x64      |no call checker| `__guard_check_icall_fptr` or `__guard_dispatch_icall_fptr`|
|Arm64 Classic |Arm64    |no call checker| `__guard_check_icall_fptr`|
|Arm64EC       |x64      |no call checker| `__guard_check_icall_fptr` or `__guard_dispatch_icall_fptr`|
| |Arm64EC |`__os_arm64x_check_icall`| `__os_arm64x_check_icall_cfg`|

Independently of the ABI, having CFG enabled code (code with reference to the CFG call-checkers), does not imply CFG protection at runtime. CFG protected binaries can run down-level, on systems not supporting CFG: the call-checker is initialized with a no-op helper at compile time. A process may also have CFG disabled by configuration. When CFG is disabled (or OS support is not present) on previous ABIs the OS will simply not update the call-checker when the binary is loaded. On Arm64EC, if CFG protection is disabled, the OS will set `__os_arm64x_check_icall_cfg` the same as `__os_arm64x_check_icall`, which will still provide the needed target architecture check in all cases, but not CFG protection.

As with CFG in Classic Arm64, the call to the target function (`x11`) must immediately follow the call to the Call Checker. The address of the Call Checker must be placed in a volatile register and neither it, nor the address of the target function, should ever be copied to another register or spilled to memory.

### Stack Checkers

`__chkstk` is used automatically by the compiler every time a function allocates an area on the stack larger than a page. To avoid skipping over the stack guard page protecting the end of the stack, `__chkstk` is called to make sure all pages in the allocated area are probed.

`__chkstk` is usually called from the prolog of the function. For that reason, and for optimal code generation, it uses a custom calling convention.

This implies that x64 code and Arm64EC code need their own, distinct, `__chkstk` functions, as Entry and Exit thunks assume standard calling conventions.

x64 and Arm64EC share the same symbol namespace so there can't be two functions named `__chkstk`. To accommodate compatibility with pre-existing x64 code, `__chkstk` name will be associated with the x64 stack checker. Arm64EC code will use `__chkstk_arm64ec` instead.

The custom calling convention for `__chkstk_arm64ec` is the same as for Classic Arm64 `__chkstk`: `x15` provides the size of the allocation in bytes, divided by 16. All non-volatile registers, as well as all volatile registers involved in the standard calling convention are preserved.

Everything said above about `__chkstk` applies equally to `__security_check_cookie` and its Arm64EC counterpart: `__security_check_cookie_arm64ec`.

### Variadic calling convention

Arm64EC follows the Classic Arm64 ABI calling convention, except for variadic functions (also known as varargs or functions with the ellipsis (. . .) parameter keyword).

For the variadic specific case, Arm64EC follows a calling convention very similar to x64 variadic, with only a few differences. The following list shows the major rules for Arm64EC variadic:

- Only the first four registers are used for parameter passing: `x0`, `x1`, `x2`, `x3`. The remaining parameters spill onto the stack. This rule follows the x64 variadic calling convention exactly and differs from Arm64 Classic, where registers `x0` through `x7` are used.
- Floating point and SIMD parameters passed by register use a general-purpose register, not a SIMD one. This rule is similar to Arm64 Classic and differs from x64, where FP/SIMD parameters are passed in both a general-purpose and SIMD register. For example, for a function `f1(int, …)` called as `f1(int, double)`, on x64, the second parameter is assigned to both `RDX` and `XMM1`. On Arm64EC, the second parameter is assigned to just `x1`.
- When passing structures by value through a register, x64 size rules apply: Structures with sizes exactly 1, 2, 4, and 8 bytes load directly into the general-purpose register. Structures with other sizes spill onto the stack, and a pointer to the spilled location is assigned to the register. This rule essentially demotes by-value into by-reference at the low level. On the Classic Arm64 ABI, structures of any size up to 16 bytes are assigned directly to general-purpose registers.
- The `x4` register loads a pointer to the first parameter passed via stack (the fifth parameter). This rule doesn't include structures spilled due to the size restrictions outlined earlier.
- The `x5` register loads the size, in bytes, of all parameters passed by stack (size of all parameters, starting with the fifth). This rule doesn't include structures passed by value spilled due to the size restrictions outlined earlier.

In the following example, `pt_nova_function` takes parameters in a non-variadic form, so it follows the Classic Arm64 calling convention. It then calls `pt_va_function` with the exact same parameters but in a variadic call instead.

```cpp
struct three_char {
    char a;
    char b;
    char c;
};

void
pt_va_function (
    double f,
    ...
);

void
pt_nova_function (
    double f,
    struct three_char tc,
    __int64 ull1,
    __int64 ull2,
    __int64 ull3
)
{
    pt_va_function(f, tc, ull1, ull2, ull3);
}
```

`pt_nova_function` takes five parameters, which it assigns following the Classic Arm64 calling convention rules:

- 'f ' is a double. It assigns to `d0`.
- 'tc' is a struct with a size of 3 bytes. It assigns to `x0`.
- `ull1` is an 8-byte integer. It assigns to `x1`.
- `ull2` is an 8-byte integer. It assigns to `x2`.
- `ull3` is an 8-byte integer. It assigns to `x3`.

`pt_va_function` is a variadic function, so it follows the Arm64EC variadic rules outlined earlier:

- 'f ' is a double. It assigns to `x0`.
- 'tc' is a struct with a size of 3 bytes. It spills onto the stack and its location loads into `x1`.
- `ull1` is an 8-byte integer. It assigns to `x2`.
- `ull2` is an 8-byte integer. It assigns to `x3`.
- `ull3` is an 8-byte integer. It assigns directly to the stack.
- `x4` loads the location of `ull3` in the stack.
- `x5` loads the size of `ull3`.

The following example shows possible compilation output for `pt_nova_function`, which illustrates the parameter assignment differences outlined earlier.

```
stp         fp,lr,[sp,#-0x30]!
mov         fp,sp
sub         sp,sp,#0x10

str         x3,[sp]          ; Spill 5th parameter
mov         x3,x2            ; 4th parameter to x3 (from x2)
mov         x2,x1            ; 3rd parameter to x2 (from x1)
str         w0,[sp,#0x20]    ; Spill 2nd parameter
add         x1,sp,#0x20      ; Address of 2nd parameter to x1
fmov        x0,d0            ; 1st parameter to x0 (from d0)
mov         x4,sp            ; Address of the 1st in-stack parameter to x4
mov         x5,#8            ; Size of the in-stack parameter area

bl          pt_va_function

add         sp,sp,#0x10
ldp         fp,lr,[sp],#0x30
ret
```

## ABI additions

To achieve transparent interoperability with x64 code, make many additions to the classic Arm64 ABI. These additions handle the calling conventions differences between Arm64EC and x64.

The following list includes these additions:

- [Entry and Exit Thunks](#entry-and-exit-thunks)
- [Exit Thunks](#exit-thunks)
- [Entry Thunks](#entry-thunks)
- [Adjustor Thunks](#adjustor-thunks)
- [Fast-Forward Sequences](#fast-forward-sequences)

## Entry and exit thunks

Entry and exit thunks translate the Arm64EC calling convention (mostly the same as classic Arm64) into the x64 calling convention, and vice versa.

A common misconception is that you can convert calling conventions by following a single rule applied to all function signatures. The reality is that calling conventions have parameter assignment rules. These rules depend on the parameter type and are different from ABI to ABI. A consequence is that translation between ABIs is specific to each function signature, varying with the type of each parameter.

Consider the following function:

```cpp
int fJ(int a, int b, int c, int d);
```

Parameter assignment occurs as follows:

- Arm64: a -> x0, b -> x1, c -> x2, d -> x3
- x64: a -> RCX, b -> RDX, c -> R8, d -> r9
- Arm64 -> x64 translation: x0 -> RCX, x1 -> RDX, x2 -> R8, x3 -> R9

Now consider a different function:

```cpp
int fK(int a, double b, int c, double d);
```

Parameter assignment occurs as follows:

- Arm64: a -> x0, b -> d0, c -> x1, d -> d1
- x64: a -> RCX, b -> XMM1, c -> R8, d -> XMM3
- Arm64 -> x64 translation: x0 -> RCX, d0 -> XMM1, x1 -> R8, d1 -> XMM3

These examples demonstrate that parameter assignment and translation vary by type, but also depend on the types of the preceding parameters in the list. This detail is illustrated by the third parameter. In both functions, the parameter's type is `int`, but the resulting translation is different.

Entry and exit thunks exist for this reason and are specifically tailored for each individual function signature.

Both types of thunks are functions. The emulator automatically invokes entry thunks when x64 functions call into Arm64EC functions (execution *Enters* Arm64EC). Call checkers automatically invoke exit thunks when Arm64EC functions call into x64 functions (execution *Exits* Arm64EC).

When compiling Arm64EC code, the compiler generates an entry thunk for each Arm64EC function, matching its signature. The compiler also generates an exit thunk for every function an Arm64EC function calls.

Consider the following example:

```cpp
struct SC {
    char a;
    char b;
    char c;
};

int fB(int a, double b, int i1, int i2, int i3);

int fC(int a, struct SC c, int i1, int i2, int i3);

int fA(int a, double b, struct SC c, int i1, int i2, int i3) {
    return fB(a, b, i1, i2, i3) + fC(a, c, i1, i2, i3);
}
```

When compiling the preceding code targeting Arm64EC, the compiler generates:

- Code for `fA`.
- Entry thunk for `fA`
- Exit thunk for `fB`
- Exit thunk for `fC`

The compiler generates the `fA` entry thunk in case `fA` is called from x64 code. The compiler generates exit thunks for `fB` and `fC` in case `fB` and `fC` are x64 code.

The compiler might generate the same exit thunk multiple times because it generates them at the call site rather than the function itself. This duplication might result in a considerable amount of redundant thunks. To avoid this duplication, the compiler applies trivial optimization rules to make sure only the required thunks make it into the final binary.

For example, in a binary where Arm64EC function `A` calls Arm64EC function `B`, `B` isn't exported and its address is never known outside of `A`. It's safe to eliminate the exit thunk from `A` to `B`, along with the entry thunk for `B`. It's also safe to alias together all exit and entry thunks that contain the same code, even if they were generated for distinct functions.

#### Exit thunks

Using the example functions `fA`, `fB`, and `fC` in the preceding section, the compiler generates both `fB` and `fC` exit thunks as follows:

Exit thunk to `int fB(int a, double b, int i1, int i2, int i3);`

```
$iexit_thunk$cdecl$i8$i8di8i8i8:
    stp         fp,lr,[sp,#-0x10]!
    mov         fp,sp
    sub         sp,sp,#0x30
    adrp        x8,__os_arm64x_dispatch_call_no_redirect
    ldr         xip0,[x8]
    str         x3,[sp,#0x20]  ; Spill 5th param (i3) into the stack
    fmov        d1,d0          ; Move 2nd param (b) from d0 to XMM1 (x1)
    mov         x3,x2          ; Move 4th param (i2) from x2 to R9 (x3)
    mov         x2,x1          ; Move 3rd param (i1) from x1 to R8 (x2)
    blr         xip0           ; Call the emulator
    mov         x0,x8          ; Move return from RAX (x8) to x0
    add         sp,sp,#0x30
    ldp         fp,lr,[sp],#0x10
    ret
```

Exit thunk to `int fC(int a, struct SC c, int i1, int i2, int i3);`

```
$iexit_thunk$cdecl$i8$i8m3i8i8i8:
    stp         fp,lr,[sp,#-0x20]!
    mov         fp,sp
    sub         sp,sp,#0x30
    adrp        x8,__os_arm64x_dispatch_call_no_redirect
    ldr         xip0,[x8]
    str         w1,[sp,#0x40]       ; Spill 2nd param (c) onto the stack
    add         x1,sp,#0x40         ; Make RDX (x1) point to the spilled 2nd param
    str         x4,[sp,#0x20]       ; Spill 5th param (i3) into the stack
    blr         xip0                ; Call the emulator
    mov         x0,x8               ; Move return from RAX (x8) to x0
    add         sp,sp,#0x30
    ldp         fp,lr,[sp],#0x20
    ret
```

In the `fB` case, the presence of a `double` parameter causes the remaining GP register assignment to reshuffle, a result of Arm64 and x64's different assignment rules. You can also see that x64 only assigns four parameters to registers, so the fifth parameter must be spilled onto the stack.

In the `fC` case, the second parameter is a structure of 3-byte length. Arm64 allows any size structure to be assigned directly to a register. x64 only allows sizes 1, 2, 4, and 8. This Exit Thunk must transfer this `struct` from the register onto the stack and assign a pointer to the register instead. This approach still consumes one register (to carry the pointer), so it doesn't change assignments for the remaining registers: no register reshuffling happens for the third and fourth parameters. Just as for the `fB` case, the fifth parameter must be spilled onto the stack.

Additional considerations for Exit Thunks:

- The compiler names them not by the function name they translate from and to, but rather by the signature they address. This naming convention makes it easier to find redundancies.
- The call checker sets the register `x9` to carry the address of the target (x64) function. The Exit Thunk calls the emulator, passing `x9` without any changes.

After rearranging the parameters, the Exit Thunk calls into the emulator through `__os_arm64x_dispatch_call_no_redirect`.

At this point, it's worth reviewing the function of the call checker and its custom ABI. Here's what an indirect call to `fB` looks like:

```
mov     x11, <target>
adrp    x9, __os_arm64x_check_icall_cfg
ldr     x9, [x9, __os_arm64x_check_icall_cfg] 
adrp    x10, $iexit_thunk$cdecl$i8$i8di8i8i8    ; fB function's exit thunk
add     x10, x10, $iexit_thunk$cdecl$i8$i8di8i8i8
blr     x9                                      ; check target function
blr     x11                                     ; call function
```

When calling the call checker:

- `x11` supplies the address of the target function to call (`fB` in this case). At this point, the call checker might not know if the target function is Arm64EC or x64.
- `x10` supplies an Exit Thunk matching the signature of the function being called (`fB` in this case).

The data the call checker returns depends on whether the target function is Arm64EC or x64.

If the target is Arm64EC:

- `x11` returns the address of the Arm64EC code to call. This value might be the same as the one provided in.

If the target is x64 code:

- `x11` returns the address of the Exit Thunk. This address is copied from the input provided in `x10`.
- `x10` returns the address of the Exit Thunk, undisturbed from the input.
- `x9` returns the target x64 function. This value might be the same as the one provided in via `x11`.

Call checkers always leave calling convention parameter registers undisturbed. The calling code should follow the call to the call checker immediately with `blr x11` (or `br x11` in case of a tail-call). Call checkers always preserve these registers above and beyond standard non-volatile registers: `x0`-`x8`, `x15`(`chkstk`), and `q0`-`q7`.

#### Entry Thunks

Entry Thunks take care of the transformations required from the x64 to the Arm64 calling conventions. This transformation is essentially the reverse of Exit Thunks but involves a few more aspects to consider.

Consider the previous example of compiling `fA`. An Entry Thunk is generated so that x64 code can call `fA`.

Entry Thunk for `int fA(int a, double b, struct SC c, int i1, int i2, int i3)`

```
$ientry_thunk$cdecl$i8$i8dm3i8i8i8:
    stp         q6,q7,[sp,#-0xA0]!  ; Spill full non-volatile XMM registers
    stp         q8,q9,[sp,#0x20]
    stp         q10,q11,[sp,#0x40]
    stp         q12,q13,[sp,#0x60]
    stp         q14,q15,[sp,#0x80]
    stp         fp,lr,[sp,#-0x10]!
    mov         fp,sp
    ldrh        w1,[x2]             ; Load 3rd param (c) bits [15..0] directly into x1
    ldrb        w8,[x2,#2]          ; Load 3rd param (c) bits [16..23] into temp w8
    bfi         w1,w8,#0x10,#8      ; Merge 3rd param (c) bits [16..23] into x1
    mov         x2,x3               ; Move the 4th param (i1) from R9 (x3) to x2
    fmov        d0,d1               ; Move the 2nd param (b) from XMM1 (d1) to d0
    ldp         x3,x4,[x4,#0x20]    ; Load the 5th (i2) and 6th (i3) params
                                    ; from the stack into x3 and x4 (using x4)
    blr         x9                  ; Call the function (fA)
    mov         x8,x0               ; Move the return from x0 to x8 (RAX)
    ldp         fp,lr,[sp],#0x10
    ldp         q14,q15,[sp,#0x80]  ; Restore full non-volatile XMM registers
    ldp         q12,q13,[sp,#0x60]
    ldp         q10,q11,[sp,#0x40]
    ldp         q8,q9,[sp,#0x20]
    ldp         q6,q7,[sp],#0xA0
    adrp        xip0,__os_arm64x_dispatch_ret
    ldr         xip0,[xip0,__os_arm64x_dispatch_ret]
    br          xip0
```

The emulator provides the address of the target function in `x9`.

Before calling the Entry Thunk, the x64 emulator pops the return address from the stack into the `LR` register. `LR` is then expected to point at x64 code when control transfers to the Entry Thunk.

The emulator might also perform another adjustment to the stack, depending on the following: Both Arm64 and x64 ABIs define a stack alignment requirement where the stack must be aligned to 16 bytes at the point a function is called. When running Arm64 code, hardware enforces this rule, but there's no hardware enforcement for x64. While running x64 code, erroneously calling functions with an unaligned stack might go unnoticed indefinitely, until some 16-byte alignment instruction is used (some SSE instructions do) or Arm64EC code is called.

To address this potential compatibility problem, before calling the Entry Thunk, the emulator always aligns down the Stack Pointer to 16 bytes and stores its original value in the `x4` register. This way, Entry Thunks always start executing with an aligned stack but can still correctly reference the parameters passed on the stack, via `x4`.

When it comes to non-volatile SIMD registers, there's a significant difference between the Arm64 and x64 calling conventions. On Arm64, the low 8 bytes (64 bits) of the register are considered non-volatile. In other words, only the `Dn` part of the `Qn` registers is non-volatile. On x64, the entire 16 bytes of the `XMMn` register is considered non-volatile. Furthermore, on x64, `XMM6` and `XMM7` are non-volatile registers whereas D6 and D7 (the corresponding Arm64 registers) are volatile.

To address these SIMD register manipulation asymmetries, Entry Thunks must explicitly save all SIMD registers that are considered non-volatile in x64. This saving is only needed on Entry Thunks (not Exit Thunks) because x64 is stricter than Arm64. In other words, register saving and preservation rules in x64 exceed the Arm64 requirements in all cases.

To address the correct recovery of these register values when unwinding the stack (for example, setjmp + longjmp, or throw + catch), a new unwind opcode was introduced: `save_any_reg (0xE7)`. This new 3-byte unwind opcode enables saving any General Purpose or SIMD register (including the ones considered volatile) and including full-sized `Qn` registers. This new opcode is used for the `Qn` register spills and fill operations. `save_any_reg` is compatible with `save_next_pair (0xE6)`.

For reference, the following unwind information belongs to the Entry Thunk presented earlier:

```
   Prolog unwind:
      06: E76689.. +0004 stp   q6,q7,[sp,#-0xA0]! ; Actual=stp   q6,q7,[sp,#-0xA0]!
      05: E6...... +0008 stp   q8,q9,[sp,#0x20]   ; Actual=stp   q8,q9,[sp,#0x20]
      04: E6...... +000C stp   q10,q11,[sp,#0x40] ; Actual=stp   q10,q11,[sp,#0x40]
      03: E6...... +0010 stp   q12,q13,[sp,#0x60] ; Actual=stp   q12,q13,[sp,#0x60]
      02: E6...... +0014 stp   q14,q15,[sp,#0x80] ; Actual=stp   q14,q15,[sp,#0x80]
      01: 81...... +0018 stp   fp,lr,[sp,#-0x10]! ; Actual=stp   fp,lr,[sp,#-0x10]!
      00: E1...... +001C mov   fp,sp              ; Actual=mov   fp,sp
                   +0020 (end sequence)
   Epilog #1 unwind:
      0B: 81...... +0044 ldp   fp,lr,[sp],#0x10   ; Actual=ldp   fp,lr,[sp],#0x10
      0C: E74E88.. +0048 ldp   q14,q15,[sp,#0x80] ; Actual=ldp   q14,q15,[sp,#0x80]
      0F: E74C86.. +004C ldp   q12,q13,[sp,#0x60] ; Actual=ldp   q12,q13,[sp,#0x60]
      12: E74A84.. +0050 ldp   q10,q11,[sp,#0x40] ; Actual=ldp   q10,q11,[sp,#0x40]
      15: E74882.. +0054 ldp   q8,q9,[sp,#0x20]   ; Actual=ldp   q8,q9,[sp,#0x20]
      18: E76689.. +0058 ldp   q6,q7,[sp],#0xA0   ; Actual=ldp   q6,q7,[sp],#0xA0
      1C: E3...... +0060 nop                      ; Actual=90000030
      1D: E3...... +0064 nop                      ; Actual=ldr   xip0,[xip0,#8]
      1E: E4...... +0068 end                      ; Actual=br    xip0
                   +0070 (end sequence)
```

After the Arm64EC function returns, the `__os_arm64x_dispatch_ret` routine re-enters the emulator, back to x64 code (pointed to by `LR`).

Arm64EC functions reserve the four bytes before the first instruction in the function for storing information to be used at runtime. In these four bytes, the relative address of Entry Thunk for the function can be found. When performing a call from an x64 function to an Arm64EC function, the emulator reads the four bytes before the start of the function, masks out the lower two bits, and adds that amount to the address of the function. This process produces the address of the Entry Thunk to call.

### Adjustor Thunks

Adjustor Thunks are signature-less functions that transfer control to (tail-call) another function. Before transferring control, they transform one of the parameters. The type of the parameters being transformed is known, but all the remaining parameters can be anything and can be in any number. Adjustor Thunks don't touch any register that potentially holds a parameter, and they don't touch the stack. This characteristic makes Adjustor Thunks signature-less functions.

The compiler can automatically generate Adjustor Thunks. This generation is common, for example, with C++ multiple inheritance, where any virtual method can delegate to the parent class without modification, except for an adjustment to the `this` pointer. 

The following example shows a real-world scenario:

```
[thunk]:CObjectContext::Release`adjustor{8}':
    sub         x0,x0,#8
    b           CObjectContext::Release
```

The thunk subtracts 8 bytes to the `this` pointer and forwards the call to the parent class.

In summary, Arm64EC functions callable from x64 functions must have an associated Entry Thunk. The Entry Thunk is signature specific. Arm64 signature-less functions, such as Adjustor Thunks need a different mechanism which can handle signature-less functions.

The Entry Thunk of an Adjustor Thunk uses the `__os_arm64x_x64_jump` helper to defer the execution of the real Entry Thunk work (adjust the parameters from one convention to the other) to the next call. It is at this time that the signature becomes apparent. This includes the option of not doing calling convention adjustments at all, if the target of the Adjustor Thunk turns out to be an x64 function. Remember that by the time an Entry Thunk starts running, the parameters are in their x64 form.

In the example above, consider how the code looks in Arm64EC.

**Adjustor Thunk in Arm64EC**

```
[thunk]:CObjectContext::Release`adjustor{8}':
    sub         x0,x0,#8
    adrp        x9,CObjectContext::Release
    add         x11,x9,CObjectContext::Release
    stp         fp,lr,[sp,#-0x10]!
    mov         fp,sp
    adrp        xip0, __os_arm64x_check_icall
    ldr         xip0,[xip0, __os_arm64x_check_icall]
    blr         xip0
    ldp         fp,lr,[sp],#0x10
    br          x11
```

**Adjustor Thunk's Entry Trunk**

```
[thunk]:CObjectContext::Release$entry_thunk`adjustor{8}':
    sub         x0,x0,#8
    adrp        x9,CObjectContext::Release
    add         x9,x9,CObjectContext::Release
    adrp        xip0,__os_arm64x_x64_jump
    ldr         xip0,[xip0,__os_arm64x_x64_jump]
    br          xip0
```

### Fast-forward sequences

Some applications make run-time modifications to functions residing in binaries that they don't own but depend on – commonly operating system binaries – for the purpose of detouring execution when the function is called. This process is also known as hooking.

At a high level, the hooking process is simple. In detail, however, hooking is architecture specific and quite complex given the potential variations the hooking logic must address.

In general terms, the process involves the following steps:

- Determine the address of the function to hook.
- Replace the first instruction of the function with a jump to the hook routine.
- When the hook is done, come back to the original logic, which includes running the displaced original instruction.

The variations arise from things like:

- The size of the first instruction: It's a good idea to replace it with a JMP that's the same size or smaller, to avoid replacing the top of the function while other thread might be running it in flight.
- The type of the first instruction: If the first instruction has some PC relative nature to it, relocating it might require changing things like the displacement fields. Since they might overflow when an instruction is moved to a distant place, this change might require providing equivalent logic with different instructions altogether.

Due to all of this complexity, robust and generic hooking logic is rare to find. Frequently, the logic present in applications can only cope with a limited set of cases that the application expects to encounter in the specific APIs it is interested in. It's not hard to imagine how much of an application compatibility problem this is. Even a simple change in the code or compiler optimizations might render applications unusable if the code no longer looks exactly as expected.

What would happen to these applications if they encountered Arm64 code when setting up a hook? They would most certainly fail.

Fast-forward sequence (FFS) functions address this compatibility requirement in Arm64EC.

FFS are very small x64 functions that contain no real logic and tail-call to the real Arm64EC function. They're optional but enabled by default for all DLL exports and for any function decorated with `__declspec(hybrid_patchable)`.

For these cases, when code obtains a pointer to a given function, either by `GetProcAddress` in the export case, or by `&function` in the `__declspec(hybrid_patchable)` case, the resulting address contains x64 code. That x64 code passes for a legitimate x64 function, satisfying most of the hooking logic currently available.

Consider the following example (error handling omitted for brevity):

```cpp
auto module_handle = 
    GetModuleHandleW(L"api-ms-win-core-processthreads-l1-1-7.dll");

auto pgma = 
    (decltype(&GetMachineTypeAttributes))
        GetProcAddress(module_handle, "GetMachineTypeAttributes");

hr = (*pgma)(IMAGE_FILE_MACHINE_Arm64, &MachineAttributes);
```

The function pointer value in the `pgma` variable contains the address of `GetMachineTypeAttributes`'s FFS.

This example shows a Fast-Forward Sequence:

```
kernelbase!EXP+#GetMachineTypeAttributes:
00000001`800034e0 488bc4          mov     rax,rsp
00000001`800034e3 48895820        mov     qword ptr [rax+20h],rbx
00000001`800034e7 55              push    rbp
00000001`800034e8 5d              pop     rbp
00000001`800034e9 e922032400      jmp     00000001`80243810
```

The FFS x64 function has a canonical prolog and epilog, ending with a tail-call (jump) to the real `GetMachineTypeAttributes` function in Arm64EC code:

```
kernelbase!GetMachineTypeAttributes:
00000001`80243810 d503237f pacibsp
00000001`80243814 a9bc7bfd stp         fp,lr,[sp,#-0x40]!
00000001`80243818 a90153f3 stp         x19,x20,[sp,#0x10]
00000001`8024381c a9025bf5 stp         x21,x22,[sp,#0x20]
00000001`80243820 f9001bf9 str         x25,[sp,#0x30]
00000001`80243824 910003fd mov         fp,sp
00000001`80243828 97fbe65e bl          kernelbase!#__security_push_cookie
00000001`8024382c d10083ff sub         sp,sp,#0x20
                           [...]
```

It would be quite inefficient if it was required to run five emulated x64 instructions between two Arm64EC functions. FFS functions are special. FFS functions don't really run if they remain unaltered. The call-checker helper efficiently checks if the FFS hasn't been changed. If that's the case, the call transfers directly to the real destination. If the FFS is changed in any possible way, then it no longer is an FFS. Execution transfers to the altered FFS and runs whichever code might be there, emulating the detour and any hooking logic.

When the hook transfers execution back to the end of the FFS, it eventually reaches the tail-call to the Arm64EC code, which then executes after the hook, just as the application expects.

## Authoring Arm64EC in assembly

Windows SDK headers and the C compiler simplify the job of authoring Arm64EC assembly. For example, you can use the C compiler to generate entry and exit thunks for functions that aren't compiled from C code.

Consider the example of an equivalent to the following function `fD` that you must author in assembly (ASM). Both Arm64EC and x64 code can call this function, and the `pfE` function pointer can point to either Arm64EC or x64 code.

```cpp
typedef int (PF_E)(int, double);

extern PF_E * pfE;

int fD(int i, double d) {
    return (*pfE)(i, d);
}
```

Writing `fD` in ASM might look like the following code:

```
#include "ksarm64.h"

        IMPORT  __os_arm64x_check_icall_cfg
        IMPORT |$iexit_thunk$cdecl$i8$i8d|
        IMPORT pfE

        NESTED_ENTRY_COMDAT A64NAME(fD)
        PROLOG_SAVE_REG_PAIR fp, lr, #-16!

        adrp    x11, pfE                                  ; Get the global function
        ldr     x11, [x11, pfE]                           ; pointer pfE

        adrp    x9, __os_arm64x_check_icall_cfg           ; Get the EC call checker
        ldr     x9, [x9, __os_arm64x_check_icall_cfg]     ; with CFG
        adrp    x10, |$iexit_thunk$cdecl$i8$i8d|          ; Get the Exit Thunk for
        add     x10, x10, |$iexit_thunk$cdecl$i8$i8d|     ; int f(int, double);
        blr     x9                                        ; Invoke the call checker

        blr     x11                                       ; Invoke the function

        EPILOG_RESTORE_REG_PAIR fp, lr, #16!
        EPILOG_RETURN

        NESTED_END

        end
```

In the preceding example:

- Arm64EC uses the same procedure declaration and prolog/epilog macros as Arm64.
- Wrap function names with the `A64NAME` macro. When you compile C or C++ code as Arm64EC, the compiler marks the `OBJ` as `ARM64EC` containing Arm64EC code. This marking doesn't happen with `ARMASM`. When you compile ASM code, you can inform the linker that the produced code is Arm64EC by prefixing the function name with `#`. The `A64NAME` macro performs this operation when `_ARM64EC_` is defined and leaves the name unchanged when `_ARM64EC_` isn't defined. This approach makes it possible to share source code between Arm64 and Arm64EC.
- You must first run the `pfE` function pointer through the EC call checker, together with the appropriate exit thunk, in case the target function is x64.

## Generating entry and exit thunks

The next step is to generate the entry thunk for `fD` and the exit thunk for `pfE`. The C compiler can perform this task with minimal effort by using the `_Arm64XGenerateThunk` compiler keyword.

```cpp
void _Arm64XGenerateThunk(int);

int fD2(int i, double d) {
    UNREFERENCED_PARAMETER(i);
    UNREFERENCED_PARAMETER(d);
    _Arm64XGenerateThunk(2);
    return 0;
}

int fE(int i, double d) {
    UNREFERENCED_PARAMETER(i);
    UNREFERENCED_PARAMETER(d);
    _Arm64XGenerateThunk(1);
    return 0;
}
```

The `_Arm64XGenerateThunk` keyword tells the C compiler to use the function signature, ignore the body, and generate either an exit thunk (when the parameter is 1) or an entry thunk (when the parameter is 2).

Place thunk generation in its own C file. Being in isolated files makes it simpler to confirm the symbol names by dumping the corresponding `OBJ` symbols or even disassembly.

## Custom entry thunks

The SDK includes macros that help you create custom, hand-coded entry thunks. You can use these macros when you create custom adjustor thunks.

Most adjustor thunks are generated by the C++ compiler, but you can also generate them manually. You might manually generate an adjustor thunk when a generic callback transfers control to the real callback, and one of the parameters identifies the real callback.

The following example shows an adjustor thunk in Arm64 Classic code:

```
    NESTED_ENTRY MyAdjustorThunk
    PROLOG_SAVE_REG_PAIR    fp, lr, #-16!
    ldr     x15, [x0, 0x18]
    adrp    x16, __guard_check_icall_fptr
    ldr     x16, [x16, __guard_check_icall_fptr]
    blr     xip0
    EPILOG_RESTORE_REG_PAIR fp, lr, #16
    EPILOG_END              br  x15
    NESTED_END
```

In this example, the first parameter provides a reference to a structure. The code retrieves the target function address from an element of this structure. Because the structure is writable, Control Flow Guard (CFG) must validate the target address.

The following example shows how to port the equivalent adjustor thunk to Arm64EC:

```
    NESTED_ENTRY_COMDAT A64NAME(MyAdjustorThunk)
    PROLOG_SAVE_REG_PAIR    fp, lr, #-16!
    ldr     x11, [x0, 0x18]
    adrp    xip0, __os_arm64x_check_icall_cfg
    ldr     xip0, [xip0, __os_arm64x_check_icall_cfg]
    blr     xip0
    EPILOG_RESTORE_REG_PAIR fp, lr, #16
    EPILOG_END              br  x11
    NESTED_END
```

The preceding code doesn't supply an exit thunk (in register x10). This approach isn't possible because the code can execute for many different signatures. This code takes advantage of the caller setting x10 to the exit thunk. The caller makes the call targeting an explicit signature.

The preceding code needs an entry thunk to address the case when the caller is x64 code. The following example shows how to author the corresponding entry thunk by using the macro for custom entry thunks:

```
    ARM64EC_CUSTOM_ENTRY_THUNK A64NAME(MyAdjustorThunk)
    ldr     x9, [x0, 0x18]
    adrp    xip0, __os_arm64x_x64_jump
    ldr     xip0, [xip0, __os_arm64x_x64_jump]
    br      xip0
    LEAF_END
```

Unlike other functions, this entry thunk doesn't eventually transfer control to the associated function (the adjustor thunk). In this case, the entry thunk embeds the functionality itself (performing the parameter adjustment) and transfers control directly to the end target through the `__os_arm64x_x64_jump` helper.

## Dynamically generating (JIT compiling) Arm64EC code

In Arm64EC processes, two types of executable memory exist: Arm64EC code and x64 code.

The operating system extracts this information from the loaded binaries. x64 binaries are all x64, and Arm64EC binaries contain a range table for Arm64EC versus x64 code pages.

What about dynamically generated code? Just-in-time (JIT) compilers generate code at runtime that isn't backed by any binary file.

Usually, this process involves the following steps:

- Allocating writable memory (`VirtualAlloc`).
- Producing the code in the allocated memory.
- Re-protecting the memory from read-write to read-execute (`VirtualProtect`).
- Adding unwind function entries for all non-trivial (non-leaf) generated functions (`RtlAddFunctionTable` or `RtlAddGrowableFunctionTable`).

For trivial compatibility reasons, if an application performs these steps in an Arm64EC process, the operating system considers the code as x64 code. This behavior happens for any process that uses the unmodified x64 Java Runtime, .NET runtime, JavaScript engine, and so on.

To generate Arm64EC dynamic code, follow the same process with two differences:

- When allocating the memory, use the newer `VirtualAlloc2` (instead of `VirtualAlloc` or `VirtualAllocEx`) and provide the `MEM_EXTENDED_PARAMETER_EC_CODE` attribute.
- When adding function entries:
  - They must be in Arm64 format. When compiling Arm64EC code, the `RUNTIME_FUNCTION` type matches the x64 format. For Arm64 format when compiling Arm64EC, use the `ARM64_RUNTIME_FUNCTION` type instead.
  - Don't use the older `RtlAddFunctionTable` API. Always use the newer `RtlAddGrowableFunctionTable` API.

The following example shows memory allocation:

```cpp
    MEM_EXTENDED_PARAMETER Parameter = { 0 };
    Parameter.Type = MemExtendedParameterAttributeFlags;
    Parameter.ULong64 = MEM_EXTENDED_PARAMETER_EC_CODE;

    HANDLE process = GetCurrentProcess();
    ULONG allocationType = MEM_RESERVE;
    DWORD protection = PAGE_EXECUTE_READ | PAGE_TARGETS_INVALID;

    address = VirtualAlloc2 (
        process,
        NULL,
        numBytesToAllocate,
        allocationType,
        protection,
        &Parameter,
        1);
```

And the following example shows how to add one unwind function entry:

```cpp
ARM64_RUNTIME_FUNCTION FunctionTable[1];

FunctionTable[0].BeginAddress = 0;
FunctionTable[0].Flags = PdataPackedUnwindFunction;
FunctionTable[0].FunctionLength = nSize / 4;
FunctionTable[0].RegF = 0;                   // no D regs saved
FunctionTable[0].RegI = 0;                   // no X regs saved beyond fp,lr
FunctionTable[0].H = 0;                      // no home for x0-x7
FunctionTable[0].CR = PdataCrChained;        // stp fp,lr,[sp,#-0x10]!
                                             // mov fp,sp
FunctionTable[0].FrameSize = 1;              // 16 / 16 = 1

this->DynamicTable = NULL;
Result == RtlAddGrowableFunctionTable(
    &this->DynamicTable,
    reinterpret_cast<PRUNTIME_FUNCTION>(FunctionTable),
    1,
    1,
    reinterpret_cast<ULONG_PTR>(pBegin),
    reinterpret_cast<ULONG_PTR>(reinterpret_cast<PBYTE>(pBegin) + nSize)
);
```
