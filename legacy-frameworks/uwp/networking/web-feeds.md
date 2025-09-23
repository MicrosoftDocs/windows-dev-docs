---
description: Retrieve or create the most current and popular Web content using syndicated feeds generated according to the RSS and Atom standards using features in the Windows.Web.Syndication namespace.
title: RSS/Atom feeds
ms.assetid: B196E19B-4610-4EFA-8FDF-AF9B10D78843
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# RSS/Atom feeds


**Important APIs**

-   [**Windows.Data.Xml.Dom**](/uwp/api/Windows.Data.Xml.Dom)
-   [**Windows.Web.AtomPub**](/uwp/api/Windows.Web.AtomPub)
-   [**Windows.Web.Syndication**](/uwp/api/Windows.Web.Syndication)

Retrieve or create the most current and popular Web content using syndicated feeds generated according to the RSS and Atom standards using features in the [**Windows.Web.Syndication**](/uwp/api/Windows.Web.Syndication) namespace.

## What is a feed?

A web feed is a document that contains any number of individual entries made up of text, links, and images. Updates made to a feed are in the form of new entries used to promote the latest content across the Web. Content consumers can use a feed reader app to aggregate and monitor feeds from any number of individual content authors, gaining access to the latest content quickly and conveniently.

## Which feed format standards are supported?

The Universal Windows Platform (UWP) supports feed retrieval for RSS format standards from 0.91 to RSS 2.0, and Atom standards from 0.3 to 1.0. Classes in the [**Windows.Web.Syndication**](/uwp/api/Windows.Web.Syndication) namespace can define feeds and feed items capable of representing both RSS and Atom elements.

Additionally, Atom 1.0 and RSS 2.0 formats both allow their feed documents to contain elements or attributes not defined in the official specifications. Over time, these custom elements and attributes have become a way to define domain-specific information consumed by other web service data formats like GData and OData. To support this added feature, the [**SyndicationNode**](/uwp/api/Windows.Web.Syndication.SyndicationNode) class represents generic XML elements. Using **SyndicationNode** with classes in the [**Windows.Data.Xml.Dom**](/uwp/api/Windows.Data.Xml.Dom) namespace, allows apps to access attributes, extensions, and any content that they may contain.

Note that, for publication of syndicated content, the UWP implementation of the Atom Publication Protocol ([**Windows.Web.AtomPub**](/uwp/api/Windows.Web.AtomPub)) only supports feed content operations according to the Atom and Atom Publication standards.

## Using syndicated content with network isolation

The network isolation feature in the UWP enables a developer to control and limit network access by a UWP app. Not all apps may require access to the network. However for those apps that do, UWP provides different levels of access to the network that can be enabled by selecting appropriate capabilities.

Network isolation allows a developer to define for each app the scope of required network access. An app without the appropriate scope defined is prevented from accessing the specified type of network, and specific type of network request (outbound client-initiated requests or both inbound unsolicited requests and outbound client-initiated requests). The ability to set and enforce network isolation ensures that if an app does get compromised, it can only access networks where the app has explicitly been granted access. This significantly reduces the scope of the impact on other applications and on Windows.

Network isolation affects any class elements in the [**Windows.Web.Syndication**](/uwp/api/Windows.Web.Syndication) and [**Windows.Web.AtomPub**](/uwp/api/Windows.Web.AtomPub) namespaces that try to access the network. Windows actively enforces network isolation. A call to a class element in the **Windows.Web.Syndication** or **Windows.Web.AtomPub** namespace that results in network access may fail because of network isolation if the appropriate network capability has not been enabled.

The network capabilities for an app are configured in the app manifest when the app is built. Network capabilities are usually added using Microsoft Visual Studio 2015 when developing the app. Network capabilities may also be set manually in the app manifest file using a text editor.

For more detailed information on network isolation and networking capabilities, see the "Capabilities" section in the [Networking basics](networking-basics.md) topic.

## How to access a web feed

This section shows how to retrieve and display a web feed using classes in the [**Windows.Web.Syndication**](/uwp/api/Windows.Web.Syndication) namespace in your UWP app written in C# or JavaScript.

**Prerequisites**

To ensure your UWP app is network ready, you must set any network capabilities that are needed in the project **Package.appxmanifest** file. If your app needs to connect as a client to remote services on the Internet, then the **internetClient** capability is needed. For more information, see the "Capabilities" section in the [Networking basics](networking-basics.md) topic.

**Retrieving syndicated content from a web feed**

Now we will review some code that demonstrates how to retrieve a feed, and then display each individual item that the feed contains. Before we can configure and send the request, we'll define a few variables we'll be using during the operation, and initialize an instance of [**SyndicationClient**](/uwp/api/Windows.Web.Syndication.SyndicationClient), which defines the methods and properties we'll use to retrieve and display the feed.

The [**Uri**](/uwp/api/windows.foundation.uri.-ctor#Windows_Foundation_Uri__ctor_System_String_) constructor throws an exception if the *uriString* passed to the constructor is not a valid URI. So we validate the *uriString* using a try/catch block.

> [!div class="tabbedCodeSnippets"]
```csharp
Windows.Web.Syndication.SyndicationClient client = new Windows.Web.Syndication.SyndicationClient();
Windows.Web.Syndication.SyndicationFeed feed;
// The URI is validated by catching exceptions thrown by the Uri constructor.
Uri uri = null;
// Use your own uriString for the feed you are connecting to.
string uriString = "";
try
{
    uri = new Uri(uriString);
}
catch (Exception ex)
{
    // Handle the invalid URI here.
}
```
```javascript
var currentFeed = null;
var currentItemIndex = 0;
var client = new Windows.Web.Syndication.SyndicationClient();
// The URI is validated by catching exceptions thrown by the Uri constructor.
var uri = null;
try {
    uri = new Windows.Foundation.Uri(uriString);
} catch (error) {
    WinJS.log && WinJS.log("Error: Invalid URI");
    return;
}
```

Next we configure the request by setting any Server credentials (the [**ServerCredential**](/uwp/api/windows.web.syndication.syndicationclient.servercredential) property), proxy credentials (the [**ProxyCredential**](/uwp/api/windows.web.syndication.syndicationclient.proxycredential) property), and HTTP headers (the [**SetRequestHeader**](/uwp/api/windows.web.syndication.syndicationclient.setrequestheader) method) needed. With the basic request parameters configured, a valid [**Uri**](/uwp/api/windows.foundation.uri) object, created using a feed URI string provided by the app. The **Uri** object is then passed to the [**RetrieveFeedAsync**](/uwp/api/windows.web.syndication.syndicationclient.retrievefeedasync) function to request the feed.

Assuming the desired feed content was returned, the example code iterates through each feed item, calling **displayCurrentItem** (which we define next), to display items and their contents as a list through the UI.

You must write code to handle exceptions when you call most asynchronous network methods. Your exception handler can retrieve more detailed information on the cause of the exception to better understand the failure and make appropriate decisions.

The [**RetrieveFeedAsync**](/uwp/api/windows.web.syndication.syndicationclient.retrievefeedasync) method throws an exception if a connection could not be established with the HTTP server or the [**Uri**](/uwp/api/windows.foundation.uri) object does not point to a valid AtomPub or RSS feed. The JavaScript sample code uses an **onError** function to catch any exceptions and print out more detailed information on the exception if an error occurs.

> [!div class="tabbedCodeSnippets"]
```csharp
try
{
    // Although most HTTP servers do not require User-Agent header, 
    // others will reject the request or return a different response if this header is missing.
    // Use the setRequestHeader() method to add custom headers.
    client.SetRequestHeader("User-Agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");
    feed = await client.RetrieveFeedAsync(uri);
    // Retrieve the title of the feed and store it in a string.
    string title = feed.Title.Text;
    // Iterate through each feed item.
    foreach (Windows.Web.Syndication.SyndicationItem item in feed.Items)
    {
        displayCurrentItem(item);
    }
}
catch (Exception ex)
{
    // Handle the exception here.
}
```
```javascript
function onError(err) {
    WinJS.log && WinJS.log(err, "sample", "error");
    // Match error number with an ErrorStatus value.
    // Use Windows.Web.WebErrorStatus.getStatus() to retrieve HTTP error status codes.
    var errorStatus = Windows.Web.Syndication.SyndicationError.getStatus(err.number);
    if (errorStatus === Windows.Web.Syndication.SyndicationErrorStatus.invalidXml) {
        displayLog("An invalid XML exception was thrown. Please make sure to use a URI that points to a RSS or Atom feed.");
    }
}
// Retrieve and display feed at given feed address.
function retreiveFeed(uri) {
    // Although most HTTP servers do not require User-Agent header, 
    // others will reject the request or return a different response if this header is missing.
    // Use the setRequestHeader() method to add custom headers.
    client.setRequestHeader("User-Agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");
    client.retrieveFeedAsync(uri).done(function (feed) {
        currentFeed = feed;
        WinJS.log && WinJS.log("Feed download complete.", "sample", "status");
        var title = "(no title)";
        if (currentFeed.title) {
            title = currentFeed.title.text;
        }
        document.getElementById("CurrentFeedTitle").innerText = title;
        currentItemIndex = 0;
        if (currentFeed.items.size > 0) {
            displayCurrentItem();
        }
        // List the items.
        displayLog("Items: " + currentFeed.items.size);
     }, onError);
}
```

In the previous step, [**RetrieveFeedAsync**](/uwp/api/windows.web.syndication.syndicationclient.retrievefeedasync) returned the requested feed content and the example code got to work iterating through available feed items. Each of these items is represented using a [**SyndicationItem**](/uwp/api/Windows.Web.Syndication.SyndicationItem) object that contains all of the item properties and content afforded by the relevant syndication standard (RSS or Atom). In the following example we observe the **displayCurrentItem** function working through each item and displaying its content through various named UI elements.

> [!div class="tabbedCodeSnippets"]
```csharp
private void displayCurrentItem(Windows.Web.Syndication.SyndicationItem item)
{
    string itemTitle = item.Title == null ? "No title" : item.Title.Text;
    string itemLink = item.Links == null ? "No link" : item.Links.FirstOrDefault().ToString();
    string itemContent = item.Content == null ? "No content" : item.Content.Text;
    //displayCurrentItem is continued below.
```
```javascript
function displayCurrentItem() {
    var item = currentFeed.items[currentItemIndex];
    // Display item number.
    document.getElementById("Index").innerText = (currentItemIndex + 1) + " of " + currentFeed.items.size;
    // Display title.
    var title = "(no title)";
    if (item.title) {
        title = item.title.text;
    }
    document.getElementById("ItemTitle").innerText = title;
    // Display the main link.
    var link = "";
    if (item.links.size > 0) {
        link = item.links[0].uri.absoluteUri;
    }
    var link = document.getElementById("Link");
    link.innerText = link;
    link.href = link;
    // Display the body as HTML.
    var content = "(no content)";
    if (item.content) {
        content = item.content.text;
    }
    else if (item.summary) {
        content = item.summary.text;
    }
    document.getElementById("WebView").innerHTML = window.toStaticHTML(content);
                //displayCurrentItem is continued below.
```

As suggested earlier, the type of content represented by a [**SyndicationItem**](/uwp/api/Windows.Web.Syndication.SyndicationItem) object will differ depending on the feed standard (RSS or Atom) employed to publish the feed. For example, an Atom feed is capable of providing a list of [**Contributors**](/uwp/api/windows.web.syndication.syndicationitem.contributors), but an RSS feed is not. However, extension elements included in a feed item that are not supported by either standard (for example, Dublin Core extension elements) can be accessed using the [**SyndicationItem.ElementExtensions**](/uwp/api/windows.web.syndication.syndicationitem.elementextensions) property and then displayed as demonstrated in the following example code.

> [!div class="tabbedCodeSnippets"]
```csharp
    //displayCurrentItem continued.
    string extensions = "";
    foreach (Windows.Web.Syndication.SyndicationNode node in item.ElementExtensions)
    {
        string nodeName = node.NodeName;
        string nodeNamespace = node.NodeNamespace;
        string nodeValue = node.NodeValue;
        extensions += nodeName + "\n" + nodeNamespace + "\n" + nodeValue + "\n";
    }
    this.listView.Items.Add(itemTitle + "\n" + itemLink + "\n" + itemContent + "\n" + extensions);
}
```
```javascript
    // displayCurrentItem function continued.
    var bindableNodes = [];
    for (var i = 0; i < item.elementExtensions.size; i++) {
        var bindableNode = {
            nodeName: item.elementExtensions[i].nodeName,
             nodeNamespace: item.elementExtensions[i].nodeNamespace,
             nodeValue: item.elementExtensions[i].nodeValue,
        };
        bindableNodes.push(bindableNode);
    }
    var dataList = new WinJS.Binding.List(bindableNodes);
    var listView = document.getElementById("extensionsListView").winControl;
    WinJS.UI.setOptions(listView, {
        itemDataSource: dataList.dataSource
    });
}
```