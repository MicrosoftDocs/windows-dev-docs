---
title: Floating-point rules
description: Direct3D supports several floating-point representations. All floating-point computations operate under a defined subset of the IEEE 754 32-bit single precision floating-point rules.
ms.assetid: 3B0C95E2-1025-4F70-BF14-EBFF2BB53AFF
keywords:
- Floating-point rules
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# <span id="direct3dconcepts.floating-point_rules"></span>Floating-point rules


Direct3D supports several floating-point representations. All floating-point computations operate under a defined subset of the IEEE 754 32-bit single precision floating-point rules.

## <span id="alpha_32_bit"></span><span id="ALPHA_32_BIT"></span>32-bit floating-point rules


There are two sets of rules: those that conform to IEEE-754, and those that deviate from the standard.

### <span id="alpha_754_Rules"></span><span id="alpha_754_rules"></span><span id="ALPHA_754_RULES"></span>Honored IEEE-754 rules

Some of these rules are a single option where IEEE-754 offers choices.

-   Divide by 0 produces +/- INF, except 0/0 which results in NaN.
-   log of (+/-) 0 produces -INF.
     

    log of a negative value (other than -0) produces NaN.
-   Reciprocal square root (rsq) or square root (sqrt) of a negative number produces NaN.
     

    The exception is -0; sqrt(-0) produces -0, and rsq(-0) produces -INF.
-   INF - INF = NaN
-   (+/-)INF / (+/-)INF = NaN
-   (+/-)INF \* 0 = NaN
-   NaN (any OP) any-value = NaN
-   The comparisons EQ, GT, GE, LT, and LE, when either or both operands is NaN returns **FALSE**.
-   Comparisons ignore the sign of 0 (so +0 equals -0).
-   The comparison NE, when either or both operands is NaN returns **TRUE**.
-   Comparisons of any non-NaN value against +/- INF return the correct result.

### <span id="alpha_754_Deviations"></span><span id="alpha_754_deviations"></span><span id="ALPHA_754_DEVIATIONS"></span>Deviations or additional requirements from IEEE-754 rules

-   IEEE-754 requires floating-point operations to produce a result that is the nearest representable value to an infinitely-precise result, known as round-to-nearest-even.

    Direct3D 11 and up define the same requirement as IEEE-754: 32-bit floating-point operations produce a result that is within 0.5 unit-last-place (ULP) of the infinitely-precise result. This means that, for example, hardware is allowed to truncate results to 32-bit rather than perform round-to-nearest-even, as that would result in error of at most 0.5 ULP. This rule applies only to addition, subtraction, and multiplication.

    Earlier versions of Direct3D define a looser requirement than IEEE-754: 32-bit floating-point operations produce a result that is within one unit-last-place (1 ULP) of the infinitely-precise result. This means that, for example, hardware is allowed to truncate results to 32-bit rather than perform round-to-nearest-even, as that would result in error of at most one ULP.

-   There is no support for floating-point exceptions, status bits or traps.
-   Denorms are flushed to sign-preserved zero on input and output of any floating-point mathematical operation. Exceptions are made for any I/O or data movement operation that doesn't manipulate the data.
-   States that contain floating-point values, such as Viewport MinDepth/MaxDepth or BorderColor values, may be provided as denorm values and may or may not be flushed before the hardware uses them.
-   Min or max operations flush denorms for comparison, but the result may or may not be denorm flushed.
-   NaN input to an operation always produces NaN on output. But the exact bit pattern of the NaN is not required to stay the same (unless the operation is a raw move instruction - which doesn't alter data.)
-   Min or max operations for which only one operand is NaN return the other operand as the result (contrary to comparison rules we looked at earlier). This is an IEEE 754R rule.

    The IEEE-754R specification for floating point min and max operations states that if one of the inputs to min or max is a quiet QNaN value, the result of the operation is the other parameter. For example:

    ```ManagedCPlusPlus
    min(x,QNaN) == min(QNaN,x) == x (same for max)
    ```

    A revision of the IEEE-754R specification adopted a different behavior for min and max when one input is a "signaling" SNaN value versus a QNaN value:

    ```ManagedCPlusPlus
    min(x,SNaN) == min(SNaN,x) == QNaN (same for max)
     
    ```

    Generally, Direct3D follows the standards for arithmetic: IEEE-754 and IEEE-754R. But in this case, we have a deviation.

    The arithmetic rules in Direct3D 10 and later don't make any distinctions between quiet and signaling NaN values (QNaN versus SNaN). All NaN values are handled the same way. In the case of min and max, the Direct3D behavior for any NaN value is like how QNaN is handled in the IEEE-754R definition. (For completeness - if both inputs are NaN, any NaN value is returned.)

-   Another IEEE 754R rule is that min(-0,+0) == min(+0,-0) == -0, and max(-0,+0) == max(+0,-0) == +0, which honors the sign, in contrast to the comparison rules for signed zero (as we saw earlier). Direct3D recommends the IEEE 754R behavior here, but doesn't enforce it; it is permissible for the result of comparing zeros to be dependent on the order of parameters, using a comparison that ignores the signs.
-   x\*1.0f always results in x (except denorm flushed).
-   x/1.0f always results in x (except denorm flushed).
-   x +/- 0.0f always results in x (except denorm flushed). But -0 + 0 = +0.
-   Fused operations (such as mad, dp3) produce results that are no less accurate than the worst possible serial ordering of evaluation of the unfused expansion of the operation. The definition of the worst possible ordering, for the purpose of tolerance, is not a fixed definition for a given fused operation; it depends on the particular values of the inputs. The individual steps in the unfused expansion are each allowed 1 ULP tolerance (or for any instructions Direct3D calls out with a more lax tolerance than 1 ULP, the more lax tolerance is allowed).
-   Fused operations adhere to the same NaN rules as non-fused operations.
-   sqrt and rcp have 1 ULP tolerance. The shader reciprocal and reciprocal square-root instructions, [**rcp**](/previous-versions/windows/desktop/legacy/hh447205(v=vs.85)) and [**rsq**](/windows/desktop/direct3dhlsl/rsq--sm4---asm-), have their own separate relaxed precision requirement.
-   Multiply and divide each operate at the 32-bit floating-point precision level (accuracy to 0.5 ULP for multiply, 1.0 ULP for reciprocal). If x/y is implemented directly, results must be of greater or equal accuracy than a two-step method.

## <span id="double_prec_64_bit"></span><span id="DOUBLE_PREC_64_BIT"></span>64-bit (double precision) floating point rules


Hardware and display drivers optionally support double-precision floating-point. To indicate support, when you call [**ID3D11Device::CheckFeatureSupport**](/windows/desktop/api/d3d11/nf-d3d11-id3d11device-checkfeaturesupport) with [**D3D11\_FEATURE\_DOUBLES**](/windows/desktop/api/d3d11/ne-d3d11-d3d11_feature), the driver sets **DoublePrecisionFloatShaderOps** of [**D3D11\_FEATURE\_DATA\_DOUBLES**](/windows/desktop/api/d3d11/ns-d3d11-d3d11_feature_data_doubles) to TRUE. The driver and hardware must then support all double-precision floating-point instructions.

Double-precision instructions follow IEEE 754R behavior requirements.

Support for generation of denormalized values is required for double-precision data (no flush-to-zero behavior). Likewise, instructions don't read denormalized data as a signed zero, they honor the denorm value.

## <span id="alpha_16_bit"></span><span id="ALPHA_16_BIT"></span>16-bit floating-point rules


Direct3D also supports 16-bit representations of floating-point numbers.

Format:

-   1 sign bit (s)in the MSB bit position
-   5 bits of biased exponent (e)
-   10 bits of fraction (f), with an additional hidden bit

A float16 value (v) follows these rules:

-   if e == 31 and f != 0, then v is NaN regardless of s
-   if e == 31 and f == 0, then v = (-1)s\*infinity (signed infinity)
-   if e is between 0 and 31, then v = (-1)s\*2(e-15)\*(1.f)
-   if e == 0 and f != 0, then v = (-1)s\*2(e-14)\*(0.f) (denormalized numbers)
-   if e == 0 and f == 0, then v = (-1)s\*0 (signed zero)

32-bit floating-point rules also hold for 16-bit floating-point numbers, adjusted for the bit layout described earlier. Exceptions to this include:

-   Precision: Unfused operations on 16-bit floating-point numbers produce a result that is the nearest representable value to an infinitely-precise result (round to nearest even, per IEEE-754, applied to 16-bit values). 32-bit floating-point rules adhere to 1 ULP tolerance, 16-bit floating-point rules adhere to 0.5 ULP for unfused operations, and 0.6 ULP for fused operations.
-   16-bit floating-point numbers preserve denorms.

## <span id="alpha_11_bit"></span><span id="ALPHA_11_BIT"></span>11-bit and 10-bit floating-point rules


Direct3D also supports 11-bit and 10-bit floating-point formats.

Format:

-   No sign bit
-   5 bits of biased exponent (e)
-   6 bits of fraction (f) for an 11-bit format, 5 bits of fraction (f) for a 10-bit format, with an additional hidden bit in either case.

A float11/float10 value (v) follows the following rules:

-   if e == 31 and f != 0, then v is NaN
-   if e == 31 and f == 0, then v = +infinity
-   if e is between 0 and 31, then v = 2(e-15)\*(1.f)
-   if e == 0 and f != 0, then v = \*2(e-14)\*(0.f) (denormalized numbers)
-   if e == 0 and f == 0, then v = 0 (zero)

32-bit floating-point rules also hold for 11-bit and 10-bit floating-point numbers, adjusted for the bit layout described earlier. Exceptions include:

-   Precision: 32-bit floating-point rules adhere to 0.5 ULP.
-   10/11-bit floating-point numbers preserve denorms.
-   Any operation that would result in a number less than zero is clamped to zero.

## <span id="related-topics"></span>Related topics


[Appendices](appendix.md)

[Resources](/windows/desktop/direct3d11/overviews-direct3d-11-resources)

[Textures](/windows/desktop/direct3d11/overviews-direct3d-11-resources-textures)

 

 