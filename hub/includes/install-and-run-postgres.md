---
ms.topic: include
ms.date: 10/04/2019
---
To install PostgreSQL:

1. Open your WSL terminal (ie. Ubuntu 18.04).
2. Update your Ubuntu packages: `sudo apt update`
3. Once the packages have updated, install PostgreSQL (and the -contrib package which has some helpful utilities) with: `sudo apt install postgresql postgresql-contrib`
4. Confirm installation and get the version number: `psql --version`

There are 3 commands you need to know once PostgreSQL is installed:

1. `sudo service postgresql status` for checking the status of your database.
2. `sudo service postgresql start`  to start running your database.
3. `sudo service postgresql stop` to stop running your database.

### PostgreSQL User Setup

The default admin user, `postgres`, needs a password assigned in order to connect to a database. To set a password:

1. Enter the command: `sudo passwd postgres`
2. You will get a prompt to enter your new password.
3. Close and reopen your terminal.

### Run PostgreSQL with psql shell

[psql](https://www.postgresql.org/docs/10/app-psql.html) is a terminal-based front-end to PostgreSQL. It enables you to type in queries interactively, issue them to PostgreSQL, and see the query results. Alternatively, input can be from a file. In addition, it provides a number of meta-commands and various shell-like features to facilitate writing scripts and automating a wide variety of tasks.

To start the psql shell:

1. Start your postgres service: `sudo service postgresql start`
2. Connect to the postgres service and open the psql shell: `sudo -u postgres psql`

Once you have successfully entered the psql shell, you will see your command line change to look like this: `postgres=#`

> [!NOTE]
> Alternatively, you can open the psql shell by switching to the postgres user with: `su - postgres` and then entering the command: `psql`.

To exit postgres=# enter: `\q` or use the shortcut key: Ctrl+D

To see what user accounts have been created on your PostgreSQL installation, use from your WSL terminal: `psql -c "\du"` ...or just `\du` if you have the psql shell open. This command will display columns: Account User Name, List of Roles Attributes, and Member of role group(s). To exit back to the command line, enter: `q`.
