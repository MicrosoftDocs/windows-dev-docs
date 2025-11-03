---
title: Microsoft.CommandPalette.Extensions.Toolkit Namespace
description: The Command Palette provides a full extension model, allowing developers to create their own experiences for the palette. Find info about how to use the Microsoft.CommandPalette.Extensions.Toolkit namespace to author an extension.
ms.date: 2/27/2025
ms.topic: concept-article
no-loc: [PowerToys, Windows, Insider]
# Customer intent: As a Windows developer, I want to learn how to use the Command Palette SDK to create an extension.
---

# Microsoft.CommandPalette.Extensions.Toolkit Namespace

Contains helper classes that make creating extensions easier.

## Interfaces

| Interface | Description |
| :--- | :--- |
| [ISettingsForm](isettingsform.md) | Defines the interface for a settings form. |

## Classes

| Class | Description |
| :--- | :--- |
| [AnonymousCommand](anonymouscommand.md) | AnonymousCommand is a command that can be invoked without a specific target. It is typically used for commands that do not require any parameters or context. |
| [BaseObservable](baseobservable.md) | Base class for observable objects. |
| [ChoiceSetSetting](choicesetsetting.md) | Represents a setting that allows the user to choose from a set of predefined options. |
| [ClipboardHelper](clipboardhelper.md) | Helper class for clipboard operations. |
| [ColorHelpers](colorhelpers.md) | Helper class for color operations. |
| [Command](command.md) | Base class for commands. |
| [CommandContextItem](commandcontextitem.md) | Represents an item in the command's context menu. |
| [CommandItem](commanditem.md) | Represents an item in the command palette. |
| [CommandProvider](commandprovider.md) | Base class for command providers. |
| [CommandResult](commandresult.md) | Represents the result of a command invocation. |
| [ConfirmationArgs](confirmationargs.md) | Represents the arguments for a confirmation dialog. |
| [ContentPage](contentpage.md) | Represents a page that contains content. |
| [CopyTextCommand](copytextcommand.md) | Represents a command that copies text to the clipboard. |
| [Details](details.md) | Represents a details view in the command palette. |
| [DetailsCommand](detailscommand.md) | Represents a command that displays details in the command palette. |
| [DetailsElement](detailselement.md) | Represents an element in the details view. |
| [DetailsLink](detailslink.md) | Represents a link in the details view. |
| [DetailsSeparator](detailsseparator.md) | Represents a separator in the details view. |
| [DetailsTags](detailstags.md) | Represents tags in the details view. |
| [DynamicListPage](dynamiclistpage.md) | Represents a dynamic list page in the command palette. |
| [ExtensionHost](extensionhost.md) | Represents the host for an extension. |
| [FallbackCommandItem](fallbackcommanditem.md) | Represents a fallback command item in the command palette. |
| [Filter](filter.md) | Represents a filter for command items. |
| [FormContent](formcontent.md) | Represents a form in the command palette. |
| [GoToPageArgs](gotopageargs.md) | Represents the arguments for navigating to a page. |
| [IconData](icondata.md) | Represents icon data for command items. |
| [IconHelpers](iconhelpers.md) | Helper class for icon operations. |
| [IconInfo](iconinfo.md) | Represents information about an icon. |
| [InvokableCommand](invokablecommand.md) | Represents a command that can be invoked. |
| [ItemsChangedEventArgs](itemschangedeventargs.md) | Represents the event arguments for items changed events. |
| [JsonSettingsManager](jsonsettingsmanager.md) | Manages settings stored in JSON format. |
| [KeyChordHelpers](keychordhelpers.md) | Helper class for key chord operations. |
| [ListHelpers](listhelpers.md) | Helper class for list operations. |
| [ListItem](listitem.md) | Represents an item in a list. |
| [ListPage](listpage.md) | Represents a list page in the command palette. |
| [LogMessage](logmessage.md) | Represents a log message. |
| [MarkdownContent](markdowncontent.md) | Represents markdown content in the command palette. |
| [MatchOption](matchoption.md) | Represents options for matching command items. |
| [MatchResult](matchresult.md) | Represents the result of a match operation. |
| [NoOpCommand](noopcommand.md) | Represents a command that does nothing. It is typically used as a placeholder. |
| [OpenUrlCommand](openurlcommand.md) | Represents a command that opens a URL in the default web browser. |
| [Page](page.md) | Base class for pages in the command palette. |
| [ProgressState](progressstate.md) | Represents the state of a progress operation. |
| [PropChangedEventArgs](propchangedeventargs.md) | Represents the event arguments for property changed events. |
| [Setting](setting.md) | Base class for settings. |
| [Settings](settings.md) | Represents the settings for an extension. |
| [SettingsForm](settingsform.md) | Represents a form for displaying settings. |
| [ShellHelpers](shellhelpers.md) | Helper class for shell operations. |
| [StatusMessage](statusmessage.md) | Represents a status message in the command palette. |
| [StringMatcher](stringmatcher.md) | Helper class for string matching operations. |
| [Tag](tag.md) | Represents a tag in the command palette. |
| [TextSetting](textsetting.md) | Represents a setting that allows the user to enter text. |
| [ThumbnailHelper](thumbnailhelper.md) | Helper class for thumbnail operations. |
| [ToastArgs](toastargs.md) | Represents the arguments for a toast notification. |
| [ToastStatusMessage](toaststatusmessage.md) | Represents a toast status message in the command palette. |
| [ToggleSetting](togglesetting.md) | Represents a setting that allows the user to toggle a value on or off. |
| [TreeContent](treecontent.md) | Represents tree content in the command palette. |
| [Utilities](utilities.md) | Utility class for common operations. |

## Enums

| Enum | Description |
| :--- | :--- |
| [SearchPrecisionScore](searchprecisionscore.md) | The precision score of a search result. |
