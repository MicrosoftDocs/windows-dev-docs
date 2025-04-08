---
title: Making games accessible
description: Learn how to make games accessible. Use the inclusive game design principle to achieve game accessibility.
ms.assetid: f5ba1e60-0d7c-11e6-91ec-0002a5d5c51b
ms.date: 11/09/2017
ms.topic: article
keywords: windows 10, uwp, accessibility, games
ms.localizationpriority: medium
---

#  Making games accessible

Accessibility can empower every person and every organization on the planet to achieve more, and this applies to making games more accessible too. This article is written for game developers, game designers, and producers. It provides an overview of game accessibility guidelines derived from various organizations (listed in the reference section below), and introduces the inclusive game design principle for creating more accessible games.

## Gaming for Everyone

At Microsoft, we believe that gaming should be fun for everyone. We "felt compelled to do more to make gaming an inclusive environment that embraced everyone. We fundamentally believe that what we build for our fans and the way we show up – inside and outside the walls of Microsoft – is a reflection of who we are. We designed the program to reflect the core values we have as an organization and believe that the program could result in positive change – not only in our workplace but in the products we build for the gamers we serve." ([Blog post](https://blogs.microsoft.com/blog/2016/06/13/gaming-for-everyone/) from Phil Spencer)

We want to create a fun, diverse, and inclusive environment where everyone can play in. "To truly have a lasting impact requires a culture shift, one that won’t happen overnight. However, our team is committed to get better each day, to teach one another to pause in our decision making process and think about the amazing diversity of needs, abilities and interests amongst gamers around the world." ([Blog post](https://blogs.microsoft.com/blog/2016/06/13/gaming-for-everyone/) from Phil Spencer)

We hope you'll join us on this journey to make [Gaming for Everyone](https://news.microsoft.com/gamingforeveryone/) happen. 

##  Why make games accessible?

### Increased gamer base

At its most basic level, the business justification for accessibility is straightforward:

Number of users who can play your game x Awesomeness of game = Game sales

If you made an amazing game that is so complicated or convoluted that only a handful of people can play it, you limit your sales. Similarly, if you made a game that is unplayable by those with physical, sensory, or cognitive impairments, you are missing out on potential sales. Considering that, for example, [19% of people in the United States have some form of disability](https://www.census.gov/newsroom/releases/archives/miscellaneous/cb12-134.html), [estimated 14% of adults in the US have difficulty reading](https://nces.ed.gov/naal/estimates/overview.aspx), and [estimated 10% of males have some form of color vision deficiency](https://www.aao.org/eye-health/diseases/color-blindness-risk), this can potentially have a large impact on your title’s revenue. 

For more business justifications, see [Making Video Games Accessible](/windows/desktop/DxTechArts/accessibility-best-practices).

### Better games

Creating a more accessible game can create a better game in the end. 

An example is subtitles in games. In the past, games rarely supported subtitles or closed captioning for game dialogues. Today, it’s expected that games include subtitles and closed captioning. This change was not driven by gamers with disabilities. Instead, it was driven by localization, but became popular with a wide range of gamers who simply preferred to play with subtitles because it made the gaming experience better. Gamers turn subtitles and closed captioning on when they are playing with too much background noise, are having difficulty hearing voices with various sound effects or ambient sounds playing at the same time, or when they simply need to keep the volume low to avoid disturbing others. Subtitles and closed captions not only helped gamers to have a better gaming experience, but it also allows people with hearing disabilities to game as well.

Controller remapping is another feature that is slowly becoming a standard for the game industry for similar reasons. It is most commonly offered as a benefit for all players. Some gamers enjoy customizing their gaming experiences and some simply prefer something different from what the designers had in mind. What most people don’t realize is that the ability to remap buttons on an input device is actually also an accessibility feature that was intended to make a game playable for people with various types of motor disabilities, who are physically unable to or find it difficult to operate certain areas of the controller.

Ultimately, the thought process used to make your game more accessible will often result in a better game because you have designed a more user-friendly, customizable experience for your players to enjoy.

### Social space and quality of life

Video games are one of the highest grossing forms of entertainment and gaming can provide hours of joy. For some, gaming is not only a form of entertainment but it is an escape from a hospital bed, chronic pain, or debilitating social anxiety. Gamers are transported into a world where they become the main characters in the video game. Through gaming, they can create and participate in a social space for themselves that provides distraction from the day-to-day struggles brought on by their disabilities, and that provides an opportunity to communicate with people they might otherwise be unable to interact with. 

Gaming is also a culture. Being able to take part in the same thing that all of your friends are talking about is something that can be hugely valuable to someone’s quality of life.

##  Is the game you are making today accessible?

If you are thinking about making your game accessible for the first time, here are some questions to ask yourself:

* Can you complete the game using a single hand? 
* Can an average person be able to pick the game up and play?
* Can you effectively play the game on a small monitor or TV sitting at a distance?
* Do you support more than one type of input device that can be used to play through the entire game?
* Can you play the game with sound muted?
* Can you play the game with your monitor set to black and white?
* When you load your last saved game after a month, can you easily figure out where you are in the game and know what you need to do in order to progress?

If your answers are mostly no, or you do not know the answers, it is time to step up and put accessibility into your game.

## Defining disability

Disability is defined as "a mismatch between the needs of the individual and the service, product or environment offered." ([Inclusive video](https://www.microsoft.com/design/inclusive/), Microsoft.com.) This means that anyone can experience a disability, and that it can be a short-term or situational condition. Envision what challenges gamers with these conditions might have when playing your game, and think about how your game can be better designed for them. Here are some disabilities to consider:

### Vision

*	Medical, long-term conditions like glaucoma, cataracts, color blindness, near-sightedness, and diabetic retinopathy
*	Short-term, situational conditions like a small monitor or screen size, a low resolution screen, or screen glare due to bright light sources like the sun on a monitor or mobile screen
        
### Hearing

* Medical, long-term conditions like complete deafness or partial hearing loss due to diseases or genetics
* Short-term, situational conditions like excessive background noise, low quality audio quality, or restricted volume to avoid disturbing others
        
### Motor

* Medical, long-term conditions like Parkinson’s disease, amyotrophic lateral sclerosis (ALS), arthritis, and muscular dystrophy
* Short-term, situational conditions like an injured hand, holding a beverage, or carrying a child in one arm
  
### Cognitive

* Medical, long-term conditions like dyslexia, epilepsy, attention deficit hyperactivity disorder (ADHD), dementia, and amnesia
* Short-term, situational conditions like alcohol consumption, lack of sleep, or temporary distractions like siren from an emergency vehicle driving by the house

### Speech

* Medical, long-term conditions like vocal cord damage, dysarthria, and apraxia
* Short-term, situational conditions like dental work, or eating and drinking


## How to make games more accessible?

### Design shift: Inclusive game design approach

Inclusive design focuses on creating products and services more accessible to a broader spectrum of consumers, including people with disabilities.

To be successful, today’s game designers need to think beyond creating games that they enjoy. Game designers need to be aware of how their design decisions impact the overall accessibility of the game; the playability of the game for their entire target audience, including those with disabilities.

As such, traditional game design paradigms must shift to embrace the inclusive game design concept. Inclusive game design means going beyond the basic game design of creating fun for the target audience, to creating additional or modified personas to include a wider spectrum of players. You need to be acutely aware about designing barriers in your game and make sure that they do not add unnecessary barriers that take the fun away from the intended experience.

By identifying gaps, you can optimise, iterate on the original design concept and make it better, allowing more people to experience your vision. When you take the time to be more inclusive in your game design process, your final game becomes more accessible. No game can ever work for everyone, the definition of game requires that there is some degree of challenge, but through considering accessibility you can ensure that no one is unnecessarily excluded.

### Empower gamers: Give gamers options

Nearly every accessibility solution comes down to one of two principles. The first is giving your gamers the options to customize their gaming experience. If you already have a huge fan base, you may have a significant portion of your audience who do not want the experience to change in any way. That’s okay. Give your gamers the ability to turn these features on and off, and make features configurable individually. You need to allow people to experience the game in the way that best suits their own needs and preferences.

### Reinforce: Communicate information in more than one way

The second principle is where the concept of universal design comes in, a single approach that not only brings in more players but also improves the experience for all. For example an image as well as text, a symbol as well as colour. A map that is based on a range of different coloured markers is not only impossible for color blind gamers to use, it is also frustrating for everyone else who must remember what everything equates to. Adding symbols makes it a better experience for everyone.

### Innovate: Be creative

There are many creative ways to improve the accessibility of your game. Put on your creative hat and learn from other accessible games out there. If you already have an existing game, learn to identify current game features that could be improved while keeping the core game mechanics and experience as designed. As mentioned above, accessibility in games is all about providing gamers with options to customize their gaming experience. It could be through reinforcement or communicating information in more than one way. 

Considering accessibility allows you to approach design from a new angle and possibly ideas you would have not thought of otherwise. This approach to design resulted not only in interesting concepts but have created products that have wide spread adoption or mass market commercial success. Examples include predictive text, voice recognition, curb cuts, the loudspeaker, the typewriter, and Optical Character Recognition (OCR). Ideas for these products came from those who started thinking about solutions for accessibility.

### Adopt: Quality means accessible features

Accessibility is a measure of quality. It has to be a feature requirement and not a good-to-have work item. For example, "Adapt minimap for colourblindness" is not considered a low priority work item that you get to if you have extra time. If this work item is not done, it simply means that entire minimap feature is incomplete and cannot be shipped.

### Evangelize: Make accessibility a priority in your game studio

Game development is always running on a tight timeline, so prioritizing accessibility will help make it an easier process. One way is to design from the start with accessibility in mind. The earlier you consider accessibility, the easier and cheaper it becomes. 

Share your knowledge about accessibility with your team, share the business justifications, and dispel the common misconceptions – that it doesn’t benefit many people, it dilutes your mechanic, and it's difficult and expensive to implement.

### Review: Constantly evaluate your game

During development, you can introduce a review process to make sure that at every step of the way you are thinking about accessibility. Make a checklist like the one below to help your team constantly evaluate whether what you are creating is accessible or not.

| Checklist                                         | Accessibility features                                                                                                         |
|---------------------------------------------------|-------------------------------------------------------------------------------------------------------------------------------|
| In-game cinematics                                | Has subtitles and captions, photosensitivity tested                                                                           |
| Overall artwork (2D and 3D graphics)              | Color blind friendly colors and options, not dependent entirely on color for identification but use shapes and patterns as well|
| Start screen, settings menu and other menus       | Ability to read options aloud, ability to remember settings, alternate command control input method, adjustable UI font size  |
| Gameplay                                          | Wide adjustable difficulty levels, subtitles and captions, good visual and audio feedback for gamer                           |
| HUD display                                       | Adjustable screen position, adjustable font size, color blind friendly option                                                  |        
| Control input                                     | Mappable controls to input device, custom controller support, simplified input for game allowed                               |        

### Playtest and iterate: Get gamers' feedback

When organizing playtesting sessions, invite play testers with disabilities that your game is designed for and get them to play your game. Remember to include accessibility questions in the Beta test questionaires. Local disability groups are a great source of participants. Observe how they play and get feedback from them. Figure out what changes need to be made to make the game better.

Use social media and your game's forum to listen for input about which accessibility features matter most and how they should be implemented. 

### Shout it out: Let the world know your game is accessible

Consumers will want to know if your game can be played by gamers with disabilities. State the game’s accessibility clearly on the game website, press releases, and packaging to ensure that consumers know what to expect when they buy your game. Remember to make your website and all sales channels to the game accessible as well. Most importantly, reach out to the accessibility gaming community and tell them about your game.

## Game accessibility features

This section outlines some features that can make your game more accessible. These features are derived from guidelines taken from the [Game accessibility guidelines](http://gameaccessibilityguidelines.com/) website. That resource represent the findings of a collaborative group of studios, specialists, and academics.

### Color blind friendly graphics and user interface

The retina of the eye has two types of light-sensitive cells: the cones for seeing where there is light, and the rods for seeing in low light conditions. 

There are three types of cones (red, green, and blue) to enable us to view colors correctly. Color blindness occurs when one or more of these three types light cones is not functioning as expected. The degree of color blindness can range from almost normal color perception with reduced sensitivity towards red, green, or blue light, to a complete inability to perceive any color at all. 

Since it’s less common to have reduced sensitivity to blue light, when designing for the color blind, the selection of colors are geared towards people who are red or green color blind:
 
  + Use color combinations that can be differentiated by people with red/green color blindness:
  
    * Colors that appear similar: All shades of red and green including brown and orange
    * Colors that stand out: Blue and yellow
    
  + Do not rely solely on color to communicate or distinguish game objects. Use shapes and patterns as well.
  + If you have to rely on colors alone, combine presets with a free selection of colors, so that it can be fully customizable by the players who need them and not creating extra work for players who do not need them.
  + Use a color blind simulator to test your designs so that you can view your designs through color blind eyes. This can help you avoid common contrast issues. [Color Oracle](https://www.colororacle.org) is a free color blind simulator that can simulate the three most common types of color vision deficiency – deuteranopia, protanopia, and tritanopia.
  
### Closed captioning and subtitles

When designing the closed captions and subtitles for your game, the objective is provide readable captions as an option so that your game can also be enjoyed without audio. It should be possible to have game components like game dialogues, game audio, and sound effects displayed as text on screen.

Here are some basic guidelines to consider when designing closed captions and subtitles:

*	Select simple readable font.
*	Select sufficiently large font size, or consider having adjustable font size option for more flexibility. (Ideal font size depends on screen size, viewing distance from screen, and so on.)
*	Create high contrast between background and font color. Use strong outline and shadows for the text. Use a dark background overlay for the captions and remember to provide options for it to be turned on or off. (For more information, see [Information on contrast ratio](/windows/apps/design/accessibility/accessible-text-requirements).)
* Display short sentences on screen, maximum 38 characters per line and maximum 2-3 lines at any one time. (Remember not to give the game away by displaying the text before event occurs.)
*	Differentiate what is making the sound or who is talking. (Example: "Daniel: Hi!")
*	Provide the option to turn closed captions and subtitles on and off. (Additional feature: Ability to select how much sound information is displayed based on importance.)

### Game chat transcription

If your title allows gamers to communicate using voice and send text messages to one another, Text-to-Speech and Speech-to-Text functionalities should be available as an option.

People who do not have microphones attached to their gaming device can still have a voice conversation with someone who is speaking. They are able to type text into the chat window and have those messages converted into voice. It also allows someone who can't hear very well to read the transcribed text messages from the person they're having a voice chat with.

For developers in the ID@Xbox and managed partners program, Text-to-Speech and Speech-to-Text features are available as part of the [Game Chat 2 accessibility features](/gaming/gdk/_content/gc/accessibility/accessibility-overview) in the Xbox Live service. For more information, see [Game Chat 2 Overview](/gaming/gdk/_content/gc/chat/overviews/game-chat2/game-chat-2-overview).

### Sound feedback

Sound provides feedback to the player, in addition to visual feedback. Good game audio design can improve accessibility for players with visual impairment. Here are some guidelines to consider:

*	Use 3D audio cues to provide additional spatial information.
* Separate music, speech and sound effects volume controls.
*	Design speech that provides meaningful information for gamers. (Example: "Enemies are approaching" vs. "Enemies are entering from the back door.")
*	Ensure speech is spoken at a reasonable rate, and provide rate control for better accessibility.

### Fully mappable controls

There are companies and organizations, such as [SpecialEffect](https://www.specialeffect.org.uk), that design custom game controllers that can be used with various gaming systems such as Windows and Xbox devices. This customization allows people with different forms of disabilities to play games that they might not be able to play otherwise. For more information on people who are now able to play games independently because of customized controllers, see [Whom we've helped](https://www.specialeffect.org.uk/what-we-do/who-weve-helped) on the SpecialEffect website.

As a game developer, you can make your game more accessible by allowing fully mappable controls so that gamers have the option to plug in their custom controllers and remap the keys according to their needs.

Having fully mappable controls also benefits people who use standard controllers. Your gamers can design a layout that suits their unique individual needs.

Both standard Xbox One and Xbox Elite controllers offer customization of the controllers for precision gaming. To fully utliize their remapping capabilities, __it is recommended that developers include remapping directly in the game__. For more information, see [Xbox One](https://support.xbox.com/xbox-one/accessories/customize-standard-controller-with-accessories-app) and [Xbox Elite](https://support.xbox.com/xbox-one/accessories/use-accessories-app-configure-elite-controller).

### Wider selection of difficulty levels

Video games provide entertainment. The challenge for game developers is to tune the difficulty level such that the gamer experiences the right amount challenge. Firstly, not all gamers have the same skill level and capability, so designing a wider selection of difficulty options increases the chance of providing gamers with the right amount of challenge. At the same time, this wider selection also makes your video game more accessible because it could potentially allow more people with disabilities to play your game. Remember, gamers want to overcome challenges in a game and be rewarded for it. They do not want a game that they cannot win.

Tweaking the difficulty level of your game is a delicate process. If it is too easy, gamers might get bored. If it is too difficult, gamers may give up and not play any further from that point on. The balancing process is both art and science. There are many ways to make a game level that has the right amount of challenge. Some games offer simplified inputs, like a single button press game option for their game, a rewind and replay option to make gameplay more forgiving, or less and weaker enemies to make it easier to proceed forward after several tries.

### Photosensitivity epilepsy testing

Photosensitive epilepsy (PSE) is a condition where seizures are triggered by visual stimuli, including exposures to flashing lights or certain moving visual forms and patterns. This occurs in about three percent of people and is more common in children and adolescents. In terms of numbers, we are looking at approximately [1 in 4000 people aged 5-24](https://www.epilepsy.com/learn/professionals/about-epilepsy-seizures/reflex-seizures-and-related-epileptic-syndromes-0).

There are many factors that can cause a photosensitive reaction when playing video games, including the duration of gameplay, the frequency of the flash, the intensity of the light, the contrast of the background and the light, the distance between the screen and the gamer, and the wavelength of the light.

Many people discover that they have epilepsy through having a seizure. Gamers can and do have their first seizures through videogames, and this can result in physical injury. As a developer, here are some tips for designing a game to reduce the risk of seizures caused by photosensitive epilepsy.

Avoid the following:
* Having flashing lights with a frequency of 5 to 30 flashes per second (Hertz) because flashing lights in that range are most likely to trigger seizures.
* Any sequence of flashing images that lasts for more than 5 seconds
* More than three flashes in a single second, covering 25%+ of the screen
* Moving repeated patterns or uniform text, covering 25%+ of the screen
* Static repeated patterns or uniform text, covering 40%+ of the screen
* An instantaneous high change in brightness/contrast (including fast cuts), or to/from the colour red
* More than five evenly spaced high contrast repeated stripes – rows or columns such as grids and checkerboards, that may be composed of smaller regular elements such as polkadots
* More than five lines of text formatted as capital letters only, with not much spacing between letters, and line spacing the same height as the lines themselves, effectively turning it into high contrast evenly alternating rows

Use an automated system to check gameplay for stimuli that could trigger photosensitive epilepsy. (Example: [The Harding Test](https://www.hardingtest.com/index.php?page=test) and [Harding Flash and Pattern Analyzer (FPA) G2](https://www.hardingfpa.com/harding-fpa-for-games/) developed by Cambridge Research System Ltd and Professor Graham Harding.) 

Include **Flashing On/Off** as a setting option and set **Flashing** as **Off** by default. In doing so, you protect players who do not yet know they are susceptible to seizures.

Design for breaks between game levels, encouraging players to take a break from playing non-stop.

## Other accessibility resources

Here are some external sites that provide additional information about game accessibility.

### Game accessibility guidelines
* [Game accessibility guidelines](http://gameaccessibilityguidelines.com/) (used as a reference in this topic)
* [AbleGamers Foundation guidelines](https://accessible.games/accessible-player-experiences/) (used as a reference in this topic)
* [Design Universally Accessible (UA) games](https://www.ics.forth.gr/hci/ua-games/index_main.php?l=e&c=555)

### Custom input controllers
* [SpecialEffect](https://www.specialeffect.org.uk)
* [War fighter engaged](https://www.warfighterengaged.org/)

### Other references used
* [Color Blind Awareness, a Community Interest Company](https://www.colourblindawareness.org/colour-blindness/types-of-colour-blindness/)
* [How to do subtitles well&mdash;a blog article on Gamasutra by Ian Hamilton](https://www.gamasutra.com/blogs/IanHamilton/20150715/248571/How_to_do_subtitles_well__basics_and_good_practices.php)
* [Innovation for All Programme](https://doga.no/en/tools/inclusive-design/cases/nordan-windows/)
* [Epilepsy foundation](https://www.epilepsy.com/)

### Related links
* [Inclusive Design](https://www.microsoft.com/design/inclusive/)
* [Accessibility in Windows 11 and Windows 10](/windows/apps/develop/accessibility)
* [Developing accessible UWP apps](/windows/apps/design/accessibility/accessibility)
* [Engineering Software For Accessibility eBook](https://www.microsoft.com/download/details.aspx?id=19262)
