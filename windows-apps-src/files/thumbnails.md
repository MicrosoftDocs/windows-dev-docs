---
title: Guidelines for thumbnail images in UWP apps
description: Learn how to use thumbnail images to help users preview files as they browse in a Universal Windows Platform (UWP) app.
label: Thumbnail images
template: detail.hbs
ms.date: 12/19/2018
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Thumbnail images

These guidelines describe how to use thumbnail images to help users preview files as they browse in your UWP app. 

**Important APIs**

-   [**ThumbnailMode**](/uwp/api/windows.storage.fileproperties.thumbnailmode)

## Should my app include thumbnails?

If your app allows users to browse files, you can display thumbnail images to help users quickly preview those files. 

Use thumbnails when: 
- Displaying previews for many items in a gallery collection (like files and folders). For example, a photo gallery should use thumbnails to give users a small view of each picture as they browse through their photo files.

    ![video gallery](images/thumbnail-gallery.png)

- Displaying a preview for an individual item in a list (like a specific file). For example, the user may want to see more information about a file, including a larger thumbnail for a better preview, before deciding whether to open the file. 

    ![video preview](images/thumbnail-preview.png)

## Dos and don'ts
- Specify the [thumbnail mode](/uwp/api/windows.storage.fileproperties.thumbnailmode) (PicturesView, VideosView, DocumentsView, MusicView, ListView, or SingleItem) when you retrieve thumbnails. This ensures that thumbnail images are optimized to display the type of files users want to see. 
    - Use the SingleItem mode to retrieve a thumbnail for a single item, regardless of file type. The other thumbnail modes are meant to display previews of multiple files. 

- Display generic placeholder images in place of thumbnails while thumbnails load. Using placeholders helps your app seem more responsive because users can interact with previews before the thumbnail load. 

    Placeholder images should be:
    * Specific to the kind of item that it stands in for. For example, folders, pictures, and videos should all have their own specialized placeholders. 
    * The same size and aspect ratio as the thumbnail image it stands in for. 
    * Displayed until the thumbnail image is loaded. 

- Use placeholder images with text labels to represent folders and file groups to differentiate from individual files.

- If you can't retrieve a thumbnail, display a placeholder image. 

- Display additional file information when providing previews for document and music files. Users can then identify key information about a file that might not be readily available from a thumbnail image alone. For example, for a music file, you might display the name of the artist along with the thumbnail of the album art. 

- Don't display additional file info for picture and video files. In most cases, a thumbnail image is sufficient for users browsing through pictures and videos. 

## Additional usage guidelines
Recommended [thumbnail modes](/uwp/api/windows.storage.fileproperties.thumbnailmode) and their features:

<table>
<tr>
<th> Display previews for</th>
<th> Thumbnail modes </th>
<th> Features of the retrieved thumbnail images </th>
</tr>
<tr>
<td> Pictures<br /> Videos </td>
<td> PicturesView <br />VideosView </td>
<td> <b>Size</b>: Medium, preferably at least 190 (if the image size is 190x130) <br />
<b>Aspect ratio</b>: Uniform, wide aspect ratio of about .7 (190x130 if the size is 190) <br />
Cropped for previews. <br /> 
Good for aligning images in a grid because of uniform aspect ratio.  </td>
</tr>
<tr>
<td> Documents<br />Music </td>
<td> DocumentsView <br />MusicView <br /> ListView</td>
<td> <b>Size</b>: Small, preferably at least 40 x 40 pixels <br />
<b>Aspect ratio</b>:  Uniform, square aspect ratio  <br />
Good for previewing album art because of the square aspect ratio. <br /> 
Documents look the same as they look in a file picker window (it uses the same icons). </td>
</tr>
</tr>
<tr>
<td> Any single item (regardless of file type) </td>
<td> SingleItem </td>
<td> <b>Size</b>: Small, preferably at least 40 x 40 pixels <br />
<b>Aspect ratio</b>:  Uniform, square aspect ratio  <br />
Good for previewing album art because of the square aspect ratio. <br /> 
Documents look the same as they look in a file picker window (it uses the same icons). </td>
</tr>
</table>

Here are examples showing how retrieved thumbnail images differ depending on file type and thumbnail mode:
<div class="mx-responsive-img">
<table>
<tr>
<th>Item type</th>
<th>When retrieved using: <ul><li>PicturesView <li>VideosView</ul></th>
<th>When retrieved using: <ul><li>DocumentsView <li>MusicView <li>ListView</ul></th>
<th>When retrieved using: <ul><li>SingleItem</ul></th>
<tr>
<tr>
<td>Picture</td>
<td>The thumbnail image uses the original aspect ratio of the file. <br />
<img src="images/thumbnail-pic-picvidmode.png" alt="Picture thumbnail in picture or video mode"/></td>
<td>The thumbnail is cropped to a square aspect ratio. <br />
<img src="images/thumbnail-pic-doclistmusic-modes.png" alt="Picture thumbnail in documents, music, or list modes"/></td>
<td>The thumbnail image uses the original aspect ratio of the file.<br />
<img src="images/thumbnail-pic-single-mode.png" alt="Picture thumbnail in single mode"/> </td>
</tr>
<tr>
<td>Video</td>
<td>The thumbnail has an icon that differentiates it from pictures. <br />
<img src="images/thumbnail-vid-picvid-modes.png" alt="Video thumbnail in picture or video mode"/></td>
<td>The thumbnail is cropped to a square aspect ratio. <br />
<img src="images/thumbnail-vid-doclistmusic-modes.png" alt="Video thumbnail in documents, music, or list mode"/> </td>
<td>The thumbnail image uses the original aspect ratio of the file. <br />
<img src="images/thumbnail-vid-single-mode.png" alt="Video thumbnail in single mode"/></td>
</tr>
<tr>
<td>Music</td>
<td>The thumbnail is an icon on a background of appropriate size. The background color is determined by the app's tile background color. <br />
<img src="images/thumbnail-music-picvid-modes.png" alt="Music thumbnail in picture or video mode"/></td>
<td>If the file has album art, then the thumbnail is the album art.  <br />
<img src="images/thumbnail-music-doclistmusic-modes.png" alt="Music thumbnail in documents, music, or list mode"/> <br />
Otherwise, the thumbnail is an icon on a background of appropriate size.</td>
<td>If the file has album art, then the thumbnail is the album art with the original aspect ratio of the file.  <br />
<img src="images/thumbnail-music-single-mode.png" alt="Music thumbnail in single mode"/> <br />
Otherwise, the thumbnail is an icon. </td>
</tr>
<tr>
<td>Document</td>
<td>The thumbnail is an icon on a background of appropriate size. The background color is determined by the app's tile background color. <br />
<img src="images/thumbnail-docs-picvid-modes.png" alt="Document thumbnail in picture or video mode"/></td>
<td>The thumbnail is an icon on a background of appropriate size. The background color is determined by the app's tile background color. <br />
<img src="images/thumbnail-doc-doclistmusic-modes.png" alt="Document thumbnail in documents, music, or list mode"/></td>
<td>The document thumbnail, if one exists. <br />
<img src="images/thumbnail-doc1-single-mode.png" alt="Document thumbnail in single mode"/><br />
Otherwise, the thumbnail is an icon. <br />
<img src="images/thumbnail-doc2-single-mode.png" alt="Document thumbnail icon in single mode"/></td>
</tr>
<tr>
<td>Folder</td>
<td>If there is a picture file in the folder, then the picture thumbnail is used.  <br />
<img src="images/thumbnail-dir-picvid-modes.png" alt="Folder thumbnail in picture or video mode"/> <br />
Otherwise, no thumbnail is retrieved.</td>
<td>No thumbnail image is retrieved.</td>
<td>The thumbnail is the folder icon.<br />
<img src="images/thumbnail-dir-single-mode.png" alt="Folder icon thumbnail in single mode"/></td>
</tr>
<tr>
<td>File group</td>
<td>If there is a picture file in the folder, then the picture thumbnail is used.<br />
<img src="images/thumbnail-grp-picvid-modes.png" alt="File group thumbnail in picture or video mode"/> <br /> Otherwise, no thumbnail is retrieved. </td>
<td>If there is a file that has album art among the files in the group, the thumbnail is the album art. <br />
<img src="images/thumbnail-grp-doclistmusic-modes.png" alt="File group thumbnail in documents, music or list mode"/> <br />Otherwise, no thumbnail is retrieved. </td>
<td>If there is a file that has album art among the files in the group, the thumbnail is the album art and uses the original aspect ratio of the file. <br />
<img src="images/thumbnail-grp1-single-mode.png" alt="File group thumbnail in picture or video mode"/> <br />Otherwise, the thumbnail is an icon that represents a group of files. <br />
<img src="images/thumbnail-grp2-single-mode.png" alt="File group icon in single mode"/> 
</td>
</tr>
</table>
</div>

## Related topics
- [ThumbnailMode enum](/uwp/api/windows.storage.fileproperties.thumbnailmode)
- [StorageItemThumbnail class](/uwp/api/Windows.Storage.FileProperties.StorageItemThumbnail)
- [StorageFile class](/uwp/api/windows.storage.storagefile)
- [File and folder thumbnail sample (GitHub)](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/FileThumbnails)
- [List and grid view](../design/controls-and-patterns/lists.md)