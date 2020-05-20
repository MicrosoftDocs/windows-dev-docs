---
Description: Learn about evolving inclusive design with Windows apps for Windows 10.  Design and build inclusive software with accessibility in mind.
ms.assetid: A6393A57-53F2-4F06-89AF-0D806FD76DB0
title: Designing inclusive software in Windows 10
label: Designing inclusive software
template: detail.hbs
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Designing inclusive software for Windows 10  

Learn about evolving inclusive design with Windows apps for Windows 10.  Design and build inclusive software with accessibility in mind.

At Microsoft, we’re evolving our design principles and practices. These inform how our experiences look, feel, function, and behave. We’re elevating our perspective.

This new design philosophy is called inclusive design. The idea is to design software with everyone in mind from the very beginning. This is in contrast to viewing accessibility as a technology you bolt on at the end of the development process in order to satisfy some small group of users.

“We define disability as a mismatch between the needs of the individual and the service, product or environment offered. Anyone can experience a disability. It is a common human trait to be excluded.”  \- from the [Inclusive](https://www.microsoft.com/design/inclusive/) video  

Inclusive design creates better products for everyone. It’s about considering the full range of human diversity. Consider the curb cutouts that you now find on most street corner sidewalks. They were clearly intended to be used by people in wheelchairs. But now nearly everyone uses them, including people with baby strollers, bicyclists, skateboarders. Even pedestrians will often use curb cutouts because they are there and provide a better experience. The television remote control could be considered an assistive technology (AT) for someone with physical limitations. And yet, today it is nearly impossible to buy a television without one. Before children learn to tie their shoes, they can wear slip-on or easy fastening shoes. Shoes that are easy to put on and take off are often preferred in cultures where shoes are removed before entering a home. They are also better for people with dexterity issues such as arthritis or even a temporarily broken wrist.

## Inclusive design principles  
The following 4 principles are guiding Microsoft’s shift to inclusive design:

**Think universal**: We focus on what unifies people — human motivations, relationships, and abilities. This drives us to consider the broader social impact of our work. The result is an experience that has a diversity of ways for all people to participate.

**Make it personal**: Next, we challenge ourselves to create emotional connections. Human-to-human interactions can inspire better human-to-technology interaction. A person’s unique circumstances can improve a design for everyone. The result is an experience that feels like it was created for one person.

**Keep it simple**: We start with simplicity as the ultimate unifier. When we reduce clutter people know what to do next. They’re inspired to move forward into spaces that are clean, light, and open. The result is an experience that’s honest and timeless.

**Create delight**: Delightful experiences evoke wonder and discovery. Sometimes it’s magical. Sometimes it’s a detail that’s just right. We design these moments to feel like a welcomed change in tempo. The result is an experience that has momentum and flow.

## Inclusive design users  
There are essentially two types of users of assistive technology (AT):

1. Those who need it, because of disabilities or impairments, age-related conditions, or temporary conditions (such as limited mobility from a broken limb)  
2. Those who use it out of preference, for a more comfortable or convenient computing experience

The majority of computer users (54 per-cent) are aware of some form of assistive technology, and 44 percent of computer users use some form of it, but many of them are not using AT that would benefit them (Forrester 2004).  

A 2003-2004 study commissioned by Microsoft and conducted by Forrester Research found that over half &mdash; 57 percent &mdash; of computer users in the United States between the ages of 18 and 64 could benefit from assistive technology. Most of these users did not identify themselves as having a disability or being impaired but expressed certain task-related difficulties or impairments when using a computer. Forrester (2003) also found the following number of users with these specific difficulties: One in four experiences a visual difficulty. One in four experiences pain in the wrists or hands. One in five experiences hearing difficulty.  

Besides permanent disabilities, the severity and types of difficulties an individual experiences can vary throughout their life. There is no such thing as a normal human. Our capabilities are always changing. Margaret Meade said, “We are all unique. Being all unique makes us all the same.”  

Microsoft is dedicated to conducting computer science and software engineering research with goals to enhance the computing experience and invent novel computing technologies. See [Current Microsoft Research and Development Projects](https://www.microsoft.com/accessibility/) aimed at making the computer more accessible, and easier to see, hear, and interact with.  

## Practical design steps  
If you're all in, then this section is for you. It describes the practical design steps to consider when implementing inclusive design for your app.  

### Describe the target audience  
Define the potential users of your app. Think through all of their different abilities and characteristics. For example, age, gender, language, deaf or hard of hearing users, visual impairments, cognitive abilities, learning style, mobility restrictions, and so on. Is your design meeting their individual needs?  

### Talk to actual humans with specific needs  
Meet with potential users who have diverse characteristics. Make sure you are considering all of their needs when designing your app. For example, Microsoft discovered that deaf users were turning off toast notifications on their Xbox consoles. When we asked actual deaf users about this, we learned that toast notifications were obscuring a section of closed captioning. The fix was to display the toast slight higher on the screen. This was a simple solution that was not necessarily obvious from the telemetry data that initially revealed the behavior.  

### Choose a development framework wisely  
In the design stage, the development framework you will use (i.e. UWP, Win32, web) is critical to the development of your product. If you have the luxury of choosing your framework, think about how much effort it will take to create your controls within the framework. What are the default or built-in accessibility properties that come with it? Which controls will you need to customize? When choosing your framework, you are essentially choosing how much of the accessibility controls you will get “for free” (that is, how much of the controls are already built-in) and how much will require additional development costs because of control customizations.   

Use standard Windows controls whenever possible. These controls are already enabled with the technology necessary to interface with assistive technologies.

### Design a logical hierarchy for your controls  
Once you have your framework, design a logical hierarchy to map out your controls. The logical hierarchy of your app includes the layout and tab order of controls. When assistive technology (AT) programs, such as screen readers, read your UI, visual presentation is not sufficient; you must provide a programmatic alternative that makes sense structurally to your users. A logical hierarchy can help you do that. It is a way of studying the layout of your UI and structuring each element so that users can understand it. A logical hierarchy is mainly used:  

1.	To provide programs context for the logical (reading) order of the elements in the UI  
2.	To identify clear boundaries between custom controls and standard controls in the UI  
3.	To determine how pieces of the UI interact together  

A logical hierarchy is a great way to address any potential usability issues. If you cannot structure the UI in a relatively simple manner, you may have problems with usability. A logical representation of a simple dialog box should not result in pages of diagrams. For logical hierarchies that become too deep or too wide, you may need to redesign your UI. For more information, download the [Engineering Software for Accessibility](https://www.microsoft.com/download/details.aspx?id=19262) eBook.  

### Design appropriate visual UI settings  
When designing the visual UI, ensure that your product has a high contrast setting, uses the default system fonts and smoothing options, correctly scales to the dots per inch (dpi) screen settings, has default text with at least a 5:1 contrast ratio with the background, and has color combinations that will be easy for users with color deficiencies to differentiate.  

#### High contrast setting  
One of the built-in accessibility features in Windows is High Contrast mode, which heightens the color contrast of text and images. For some people, increasing the contrast in colors reduces eyestrain and makes it easier to read. When you verify your UI in high contrast mode, you want to check that controls, such as links, have been coded consistently and with system colors (not with hard-coded colors) to ensure that they will be able to see all the controls on the screen that a user not using high contrast would see.  

#### System font settings  
To ensure readability and minimize any unexpected distortions to the text, make sure that your product always adheres to the default system fonts and uses the anti-aliasing and smoothing options. If your product uses custom fonts, users may face significant readability issues and distractions when they customize the presentation of their UI (through the use of a screen reader or by using different font styles to view your UI, for instance).  

#### High DPI resolutions  
For users with vision impairments, having a scalable UI is important. User interfaces that do not scale correctly in high dots-per-inch (DPI) resolutions may cause important components to overlap or hide other components and can become inaccessible.  

#### Color contrast ratio  
The updated Section 508 of the Americans with Disability Act (ADA), as well as other legislations, requires that the default color contrasts between text and its background must be 5:1. For large texts (18-point font sizes, or 14 points and bolded) the required default contrast is 3:1.  

#### Color combinations  
About 7 percent of males (and less than 1 percent of females) have some form of color deficiency. Users with colorblindness have problems distinguishing between certain colors, so it is important that color alone is never used to convey status or meaning in an application. As for decorative images (such as icons or backgrounds), color combinations should be chosen in a manner that maximizes the perception of the image by colorblind users. If you design using these color recommendations from the beginning, your app will already be taking significant steps toward being inclusive.  

## Summary &mdash; seven steps for inclusive design  
In summary, follow these seven steps to ensure your software is inclusive.  
1.	Decide if inclusive design is an important aspect to your software. If it is, learn and appreciate how it enables real users to live, work, and play, to help guide your design.  
2.	As you design solutions for your requirements, use controls provided by your framework (standard controls) as much as possible, and avoid any unnecessary effort and costs of custom controls.  
3.	Design a logical hierarchy for your product, noting where the standard controls, any custom controls, and keyboard focus are in the UI.  
4.	Design useful system settings (such as keyboard navigation, high contrast, and high dpi) into your product.  
5.	Implement your design, using the [Microsoft accessibility developer hub](https://developer.microsoft.com/windows/accessible-apps) and your framework’s accessibility specification as a reference point.  
6.	Test your product with users who have special needs to ensure they will be able to take advantage of the inclusive design techniques implemented in it.  
7.	Deliver your finished product and document your implementation for those who may work on the project after you.  

## Related topics  
* [Inclusive design](https://www.microsoft.com/design/inclusive/)
* [Engineering Software for Accessibility](https://www.microsoft.com/download/details.aspx?id=19262)
* [Microsoft accessibility developer hub](https://developer.microsoft.com/windows/accessible-apps)
* [Developing inclusive Windows apps](developing-inclusive-windows-apps.md) 
* [Accessibility](accessibility.md)
