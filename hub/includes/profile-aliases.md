---
ms.topic: include
ms.date: 10/04/2019
---
Typing out `sudo service mongodb start` or `sudo service postgres start` and `sudo -u postgrest psql` can get tedious.  However, you could consider setting up aliases in your `.profile` file on WSL to make these commands quicker to use and easier to remember. 

To set up your own custom alias, or shortcut, for executing these commands:

1. Open your WSL terminal and enter `cd ~` to be sure you're in the root directory.
2. Open the `.profile` file, which controls the settings for your terminal, with the terminal text editor, Nano: `sudo nano .profile`
3. At the bottom of the file (don't change the `# set PATH` settings), add the following:

    ```bash
    # My Aliases
    alias start-pg='sudo service postgresql start'
    alias run-pg='sudo -u postgres psql'
    ```

This will allow you to enter `start-pg` to start running the postgresql service and `run-pg` to open the psql shell. You can change `start-pg` and `run-pg` to whatever names you want, just be careful not to overwrite a command that postgres already uses!

4. Once you've added your new aliases, exit the Nano text editor using **Ctrl+X** -- select `Y` (Yes) when prompted to save and Enter (leaving the file name as `.profile`).
5. Close and re-open your WSL terminal, then try your new alias commands.
