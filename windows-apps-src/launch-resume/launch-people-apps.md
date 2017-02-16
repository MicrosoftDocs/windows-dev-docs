---
author: TylerMSFT
title: Launch the People app
description: This topic describes the ms-people URI scheme. Your app can use this URI scheme to launch the People app for specific actions.
ms.assetid: 1E604599-26EF-421C-932F-E9935CDB248E
ms.author: twhitney
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# Launch the People app


\[ Updated for UWP apps on Windows 10. For Windows 8.x articles, see the [archive](http://go.microsoft.com/fwlink/p/?linkid=619132) \]


This topic describes the **ms-people:** URI scheme. Your app can use this URI scheme to launch the People app for specific actions.

## ms-people: URI scheme reference


<table>
<colgroup>
<col width="50%" />
<col width="50%" />
</colgroup>
<thead>
<tr class="header">
<th align="left">Results</th>
<th align="left">URI scheme</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td align="left">Allows other apps to launch the People app Main page.</td>
<td align="left">ms-people:</td>
</tr>
<tr class="even">
<td align="left">Allows other apps to launch the People app Settings page.</td>
<td align="left">ms-people:settings</td>
</tr>
<tr class="odd">
<td align="left">Allows other apps to provide a search string that will launch the People app with the result page of the search.
<div class="alert">
**Note**  
<p>The parameters are case sensitive.</p>
<p>If you do not enter the syntax correctly, or are missing the search string value, the default behavior is to return a full list of contacts without any filtering.</p>
</div>
<div>
 
</div></td>
<td align="left">ms-people:search?SearchString=&lt;contactsearchinfo&gt;</td>
</tr>
<tr class="even">
<td align="left">Launches to an existing contact card, if the contact is found. Or, launches to a temporary contact card, if no contact is found. If no input parameter is supplied, we will launch the People App with a contact list.
<div class="alert">
**Note**  
<p>The parameters are case sensitive.</p>
<p>The order of the parameters doesn’t matter.</p>
<p>If there is more than one match, we will return the first match of the contact.</p>
</div>
<div>
 
</div></td>
<td align="left">ms-people:viewcontact:?ContactId=&lt;contactid&gt;&amp;AggregatedId=&lt;aggid&gt;&amp;PhoneNumber= &lt;phonenum&gt;&amp;Email=&lt;email&gt;&amp;ContactName=&lt;name&gt;&amp;Contact=&lt;contactobj&gt;</td>
</tr>
<tr class="odd">
<td align="left">Launches to a Save-contact page within the People app to save the given contact with the supplied phone number or email address.
<div class="alert">
**Note**  
<p>The parameters are case sensitive.</p>
<p>The order of the parameters doesn’t matter.</p>
</div>
<div>
 
</div></td>
<td align="left">ms-people:savetocontact?PhoneNumber= &lt;phonenum&gt;&amp;Email=&lt;email&gt;&amp;ContactName=&lt;name&gt;</td>
</tr>
</tbody>
</table>

 

## ms-people:search: parameter reference


<table>
<colgroup>
<col width="33%" />
<col width="33%" />
<col width="33%" />
</colgroup>
<thead>
<tr class="header">
<th align="left">Parameter</th>
<th align="left">Description</th>
<th align="left">Example</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td align="left">**SearchString**</td>
<td align="left"><p>Optional.</p>
<p>The search string for the contact search information.</p>
<p>The phone number or the contact name.</p></td>
<td align="left"><p>ms-people:search?SearchString=Smith</p></td>
</tr>
</tbody>
</table>

 

## ms-people:viewcontact: parameter reference


<table>
<colgroup>
<col width="33%" />
<col width="33%" />
<col width="33%" />
</colgroup>
<thead>
<tr class="header">
<th align="left">Parameter</th>
<th align="left">Description</th>
<th align="left">Example</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td align="left">**ContactId**</td>
<td align="left"><p>Optional.</p>
<p>Contact Id of the contact.</p></td>
<td align="left"><p>ms-people:viewcontact?ContactId={ContactId}</p></td>
</tr>
<tr class="even">
<td align="left">**PhoneNumber**</td>
<td align="left"><p>Optional.</p>
<p>Phone number of the contact.</p></td>
<td align="left"><p>ms-people:viewcontact?PhoneNumber=%2014257069326</p></td>
</tr>
<tr class="odd">
<td align="left">**Email**</td>
<td align="left"><p>Optional.</p>
<p>Email of the contact.</p></td>
<td align="left"><p>ms-people:viewcontact?Email=johnsmith@contsco.com</p></td>
</tr>
<tr class="even">
<td align="left">**ContactName**</td>
<td align="left"><p>Optional.</p>
<p>Name of the contact.</p></td>
<td align="left"><p>ms-people:viewcontact?ContactName=John%20%Smith</p></td>
</tr>
<tr class="odd">
<td align="left">**Contact**</td>
<td align="left"><p>Optional.</p>
<p>Contact object.</p></td>
<td align="left"><p>ms-people:viewcontact?Contact={Serialized Contact}</p></td>
</tr>
</tbody>
</table>

 

## ms-people:savetocontact: parameter reference


<table>
<colgroup>
<col width="33%" />
<col width="33%" />
<col width="33%" />
</colgroup>
<thead>
<tr class="header">
<th align="left">Parameter</th>
<th align="left">Description</th>
<th align="left">Example</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td align="left">**PhoneNumber**</td>
<td align="left"><p>Optional.</p>
<p>Phone number of the contact.</p></td>
<td align="left"><p>ms-people:savetocontact?PhoneNumber=%2014257069326</p></td>
</tr>
<tr class="even">
<td align="left">**Email**</td>
<td align="left"><p>Optional.</p>
<p>Email of the contact.</p></td>
<td align="left"><p>ms-people:savetocontact?Email=johnsmith@contsco.com</p></td>
</tr>
<tr class="odd">
<td align="left">**ContactName**</td>
<td align="left"><p>Optional.</p>
<p>Name of the contact.</p></td>
<td align="left"><p>ms-people:savetocontact?Email=johnsmith@contsco.com&amp;ContactName= John%20%Smith</p></td>
</tr>
</tbody>
</table>

 

 

 
