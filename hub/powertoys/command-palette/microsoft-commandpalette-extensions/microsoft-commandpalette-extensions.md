---
title: Microsoft.CommandPalette.Extensions Namespace
description: The Command Palette provides a full extension model, allowing developers to create their own experiences for the palette.
ms.date: 2/6/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# Microsoft.CommandPalette.Extensions Namespace

Contains the interfaces to create extensions for the Command Palette.

These are the raw WinRT interfaces that Command Palette uses to communicate with your extension. These can be implemented however you'd like, in any language that supports implementing WinRT interfaces. For simplicity, there's a reference C# implementation of these interfaces in the [`Microsoft.CommandPalette.Extensions.Toolkit`](../microsoft-commandpalette-extensions-toolkit/microsoft-commandpalette-extensions-toolkit.md) namespace.

## Structs

| Struct | Description |
| :--- | :--- |
| [Color](color.md) | Represents a color value. |
| [KeyChord](keychord.md) | Represents a key chord, which is a combination of keys that can be pressed together. |
| [OptionalColor](optionalcolor.md) | Represents a color that can be either specified or not specified. |

## Interfaces

| Interface | Description |
| :--- | :--- |
| [ICommand](icommand.md) | Action a user can take within the Command Palette. |
| [ICommandContextItem](icommandcontextitem.md) | Represents a context menu item for a command. |
| [ICommandItem](icommanditem.md) | Represents an item that can be used in a command. |
| [ICommandProvider](icommandprovider.md) | Represents a provider that can be used to create commands. |
| [ICommandResult](icommandresult.md) | Represents the result of a command. |
| [ICommandResultArgs](icommandresultargs.md) | Represents the arguments for a command result. |
| [ICommandSettings](icommandsettings.md) | Represents the settings for a command. |
| [IConfirmationArgs](iconfirmationargs.md) | Represents the arguments for a confirmation dialog. |
| [IContent](icontent.md) | Represents the content of a command. |
| [IContentPage](icontentpage.md) | Represents a page that can be used in a command. |
| [IContextItem](icontextitem.md) | Represents a context menu item. |
| [IDetails](idetails.md) | Represents the details of a command. |
| [IDetailsCommand](idetailscommand.md) | Represents a command that contains details. |
| [IDetailsData](idetailsdata.md) | Represents the data that can be used in the details. |
| [IDetailsElement](idetailselement.md) | Represents an element that can be used in the details. |
| [IDetailsLink](idetailslink.md) | Represents a link that can be used in the details. |
| [IDetailsSeparator](idetailsseparator.md) | Represents a separator that can be used in the details. |
| [IDetailsTags](idetailstags.md) | Represents the tags that can be used in the details. |
| [IDynamicListPage](idynamiclistpage.md) | Represents a dynamic list page that can be used in a command. |
| [IExtension](iextension.md) | Represents an extension that can be used in the Command Palette. |
| [IExtensionHost](iextensionhost.md) | Represents the host for an extension. |
| [IFallbackCommandItem](ifallbackcommanditem.md) | Represents a fallback command item that can be used in the Command Palette. |
| [IFallbackHandler](ifallbackhandler.md) | Represents a handler that can be used for fallback commands. |
| [IFilter](ifilter.md) | Represents a filter that can be used in the Command Palette. |
| [IFilters](ifilters.md) | Represents a collection of filters that can be used in the Command Palette. |
| [IFilterItem](ifilteritem.md) | Represents an item that can be used in a filter. |
| [IForm](iform.md) | Represents a form that can be used in the Command Palette. |
| [IFormContent](iformcontent.md) | Represents the content of a form. |
| [IFormPage](iformpage.md) | Represents a page that can be used in a form. |
| [IGoToPageArgs](igotopageargs.md) | Represents the arguments for navigating to a page. |
| [IGridProperties](igridproperties.md) | Represents the properties of a grid. |
| [IIconData](iicondata.md) | Represents the data for an icon. |
| [IIconInfo](iiconinfo.md) | Represents the information for an icon. |
| [IInvokableCommand](iinvokablecommand.md) | Represents a command that can be invoked. |
| [IItemsChangedEventArgs](iitemschangedeventargs.md) | Represents the arguments for an items changed event. |
| [IListItem](ilistitem.md) | Represents an item that can be used in a list. |
| [IListPage](ilistpage.md) | Represents a page that can be used in a list. |
| [ILogMessage](ilogmessage.md) | Represents a log message. |
| [IMarkdownContent](imarkdowncontent.md) | Represents the content of a markdown page. |
| [IMarkdownPage](imarkdownpage.md) | Represents a page that can be displayed as markdown. |
| [INotifyItemsChanged](inotifyitemschanged.md) | Represents an interface for notifying when items have changed. |
| [INotifyPropChanged](inotifypropchanged.md) | Represents an interface for notifying when a property has changed. |
| [IPage](ipage.md) | Represents a page that can be used in the Command Palette. |
| [IProgressState](iprogressstate.md) | Represents the state of a progress indicator. |
| [IPropChangedEventArgs](ipropchangedeventargs.md) | Represents the arguments for a property changed event. |
| [ISeparatorContextItem](iseparatorcontextitem.md) | Represents a separator as a context menu item. |
| [ISeparatorFilterItem](iseparatorfilteritem.md) | Represents a separator as a filter item. |
| [IStatusMessage](istatusmessage.md) | Represents a status message. |
| [ITag](itag.md) | Represents a tag that can be used in the Command Palette. |
| [IToastArgs](itoastargs.md) | Represents the arguments for a toast notification. |
| [ITreeContent](itreecontent.md) | Represents the content of a tree. |

## Enums

| Enum | Description |
| :--- | :--- |
| [CommandResultKind](commandresultkind.md) | Specifies what kind of command it is. |
| [MessageState](messagestate.md) | Specifies the state of a message. |
| [NavigationMode](navigationmode.md) | Specifies which navigation direction to take. |
| [ProviderType](providertype.md) | Specifies the type of provider. Currently **Command** is the only type. |
