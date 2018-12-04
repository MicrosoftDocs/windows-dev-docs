---
Description: When a resource is requested, there may be several candidates that match the current resource context to some degree. The Resource Management System will analyze all of the candidates and determine the best candidate to return. This topic describes that process in detail and gives examples.
title: How the Resource Management System matches and chooses resources
template: detail.hbs
ms.date: 10/23/2017
ms.topic: article
keywords: windows 10, uwp, resource, image, asset, MRT, qualifier
ms.localizationpriority: medium
---
# How the Resource Management System matches and chooses resources
When a resource is requested, there may be several candidates that match the current resource context to some degree. The Resource Management System will analyze all of the candidates and determine the best candidate to return. This is done by taking all qualifiers into consideration to rank all of the candidates.

In this ranking process, the different qualifiers are given different priorities: language has the greatest impact on the overall ranking, followed by contrast, then scale, and so on. For each qualifier, candidate qualifiers are compared with the context qualifier value to determine a quality of match. How the comparison is done depends upon the qualifier.

For specific details on how language tag matching is done, see [How the Resource Management System matches language tags](how-rms-matches-lang-tags.md).

For some qualifiers, such as scale and contrast, there is always some minimal degree of match. For example, a candidate qualified for "scale-100" matches a context of "scale-400" to some small degree, albeit not as well as a candidate qualified for "scale-200" or (for a perfect match) "scale-400".

For other qualifiers, however, such as language or home region, it is possible to have a non-match comparison (as well as degrees of matching). For example, a candidate qualified for language as "en-US" is a partial match for a context of "en-GB", but a candidate qualified as "fr" is not a match at all. Similarly, a candidate qualified for home region as "155" (Western Europe) matches a context for a user with a home region setting of "FR" somewhat well, but a candidate qualified as "US" does not match at all.

When a candidate is evaluated, if there is a non-match comparison for any qualifier, then that candidate will get an overall non-match ranking and will not be selected. In this way, the higher-priority qualifiers can have the greatest weight in selecting the best match, but even a low-priority qualifier can eliminate a candidate due to a non-match.

A candidate is neutral in relation to a qualifier if it is not marked for that qualifier at all. For any qualifier, a neutral candidate is always a match for the context qualifier value, but only with a lower quality of match than any candidate that was marked for that qualifier and has some degree of match (exact or partial). For example, if we have candidates qualified for "en-US", "en", "fr", and also a language-neutral candidate, then for a context with a language qualifier value of "en-GB", the candidates will be ranked in the following order: "en", "en-US", neutral, and "fr". In this case, "fr" does not match at all, while the other candidates match to some degree.

The overall ranking process begins by evaluating candidates in relation to the highest-priority qualifier, which is language. Non-matches are eliminated. The remaining candidates are ranked in relation to their quality of match for language. If there are any ties, then the next-highest-priority qualifier, contrast, is considered, using the quality of match for contrast to differentiate among tied candidates. After contrast, the scale qualifier is used to differentiate remaining ties, and so on through as many qualifiers as are needed to arrive at a well-ordered ranking.

If all candidates are removed from consideration due to qualifiers that don't match the context, the resource loader goes through a second pass looking for a default candidate to display. Default candidates are determined during creation of the PRI file and are required to ensure there is always some candidate to select for any runtime context. If a candidate has any qualifiers that don't match and aren't a default, that resource candidate is thrown permanently out of consideration.

For all the resource candidates still in consideration, the resource loader looks at the highest-priority context qualifier value and chooses the one that has the best match or best default score. Any actual match is considered better than a default score.

If there is a tie, the next-highest priority context qualifier value is inspected and the process continues, until a best match is found.

## Example of choosing a resource candidate
Consider these files.

```console
en/images/logo.scale-400.jpg
en/images/logo.scale-200.jpg
en/images/logo.scale-100.jpg  
fr/images/logo.scale-100.jpg
fr/images/contrast-high/logo.scale-400.jpg
fr/images/contrast-high/logo.scale-100.jpg
de/images/logo.jpg
```

And suppose that these are the settings in the current context.

```console
Application language: en-US; fr-FR;
Scale: 400
Contrast: Standard
```

The Resource Management System eliminates three of the files, because high contrast and the German language do not match the context defined by the settings. That leaves these candidates.

```console
en/images/logo.scale-400.jpg
en/images/logo.scale-200.jpg
en/images/logo.scale-100.jpg  
fr/images/logo.scale-100.jpg
```

For those remaining candidates, the Resource Management System uses the highest-priority context qualifier, which is language. The English resources are a closer match than the French ones because English is listed before French in the settings.

```console
en/images/logo.scale-400.jpg
en/images/logo.scale-200.jpg
en/images/logo.scale-100.jpg  
```

Next, the Resource Management System uses the next-highest priority context qualifier, scale. So this is the resource returned.

```console
en/images/logo.scale-400.jpg
```

You can use the advanced [**NamedResource.ResolveAll**](/uwp/api/windows.applicationmodel.resources.core.namedresource.resolveall?branch=live) method to retrieve all of the candidates in the order that they match the context settings. For the example we just walked through, **ResolveAll** returns candidates in this order.

```console
en/images/logo.scale-400.jpg
en/images/logo.scale-200.jpg
en/images/logo.scale-100.jpg  
fr/images/logo.scale-100.jpg
```

## Example of producing a fallback choice
Consider these files.

```console
en/images/logo.scale-400.jpg
en/images/logo.scale-200.jpg
en/images/logo.scale-100.jpg  
fr/images/contrast-standard/logo.scale-400.jpg
fr/images/contrast-standard/logo.scale-100.jpg
de/images/contrast-standard/logo.jpg
```

And suppose that these are the settings in the current context.

```console
User language: de-DE;
Scale: 400
Contrast: High
```

All the files are eliminated because they do not match the context. So we enter a default pass, where the default (see [Compile resources manually with MakePri.exe](compile-resources-manually-with-makepri.md)) during creation of the PRI file was this.

```console
Language: fr-FR;
Scale: 400
Contrast: Standard
```

This leaves all the resources that match either the current user or the default.

```console
fr/images/contrast-standard/logo.scale-400.jpg
fr/images/contrast-standard/logo.scale-100.jpg
de/images/contrast-standard/logo.jpg
```

The Resource Management System uses the highest-priority context qualifier, language, to return the named resource with the highest score.

```console
de/images/contrast-standard/logo.jpg
```

## Important APIs
* [NamedResource.ResolveAll](/uwp/api/windows.applicationmodel.resources.core.namedresource.resolveall?branch=live)

## Related topics
* [Compile resources manually with MakePri.exe](compile-resources-manually-with-makepri.md)