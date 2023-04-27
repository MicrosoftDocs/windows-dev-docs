## Rename Notification
This repo has recently been renamed from "windows-uwp" to "windows-dev-docs" to better represent the content source files contained.

If you have a copy of the repo on your local machine, you will need to update the associated URL following these steps:

1. `cd` to your local Git directory for the repo and find the remote name with the command: `git remote -v`

You will receive a response like the following example:
`origin  https://github.com/MicrosoftDocs/windows-uwp.git (fetch)`
`origin  https://github.com/MicrosoftDocs/windows-uwp.git (push)`

2. Set the new URL using the command:
`git remote set-url origin https://github.com/MicrosoftDocs/windows-dev-docs.git`

*Alternatively, you can just delete your local copy of the repo and reclone it if you don't have any active work on the repo that you are concerned with losing.

Following the rename of this repo, the content team will also be working on renaming and restructuring the source file directories within the repo to align more closely with the structure of our live documentation. We are aiming to have this work complete by September 16, 2022.

## Legal Notices
Microsoft and any contributors grant you a license to the Microsoft documentation and other content
in this repository under the [Creative Commons Attribution 4.0 International Public License](https://creativecommons.org/licenses/by/4.0/legalcode),
see the [LICENSE](LICENSE) file, and grant you a license to any code in the repository under the [MIT License](https://opensource.org/licenses/MIT). See the
[LICENSE-CODE](LICENSE-CODE) file.

Microsoft, Windows, Microsoft Azure, and/or other Microsoft products and services referenced in the documentation
may be either trademarks or registered trademarks of Microsoft in the United States and/or other countries.
The licenses for this project do not grant you rights to use any Microsoft names, logos, or trademarks.
Microsoft's general trademark guidelines can be found at https://go.microsoft.com/fwlink/?LinkID=254653.

Privacy information can be found at https://privacy.microsoft.com/en-us/

Microsoft and any contributors reserve all others rights, whether under their respective copyrights, patents,
or trademarks, whether by implication, estoppel, or otherwise.
