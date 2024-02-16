Submit an HTTPS-enabled download URL (direct link) to the product’s installer binaries. Products submitted in this manner are subject to the following requirements:

- The installer binary may only be an .msi or .exe.

- The binary and all of its Portable Executable (PE) files must be digitally signed with a code signing certificate that chains up to a certificate issued by  a Certificate Authority (CA) that is part of the  [Microsoft Trusted Root Program](/security/trusted-root/participants-list).

- You must submit a versioned download URL in Partner Center. The binary associated with that URL must not change after submission.

- Whenever you have an updated binary to distribute, you must provide an updated versioned download URL in Partner Center associated with the updated binary. You are responsible for maintaining and updating the download URL.

- Initiating the install must not display an installation user interface (i.e., silent install is required), however a User Account Control (UAC) dialog is allowed.

- The installer is a standalone installer and is not a downloader stub/web installer that downloads bits when run.
