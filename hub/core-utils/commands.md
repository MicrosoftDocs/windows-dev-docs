---
title: Coreutils for Windows commands
description: Reference list of the UNIX core utility commands available in Coreutils for Windows, with links to upstream documentation.
ms.topic: reference
ms.date: 05/14/2026
---

# Commands

Coreutils for Windows ships the following commands. Each command supports the standard `--help` flag for full syntax and options. Descriptions and links go to the upstream documentation:

- Most utilities come from [uutils/coreutils](https://uutils.github.io/coreutils/docs/).
- `find` and `xargs` come from [uutils/findutils](https://github.com/uutils/findutils).
- `grep` comes from the [uutils/grep](https://github.com/uutils/grep) submodule.

For information on commands that collide with shell built-ins, see [Shell conflicts](https://github.com/microsoft/coreutils#shell-conflicts).

| Command | Description |
|---------|-------------|
| [`arch`](https://uutils.github.io/coreutils/docs/utils/arch.html) | Display machine architecture. |
| [`b2sum`](https://uutils.github.io/coreutils/docs/utils/b2sum.html) | Print or check BLAKE2b checksums. |
| [`base32`](https://uutils.github.io/coreutils/docs/utils/base32.html) | Encode or decode data using base32 and print to standard output. |
| [`base64`](https://uutils.github.io/coreutils/docs/utils/base64.html) | Encode or decode data using base64 and print to standard output. |
| [`basename`](https://uutils.github.io/coreutils/docs/utils/basename.html) | Print NAME with any leading directory components removed. |
| [`basenc`](https://uutils.github.io/coreutils/docs/utils/basenc.html) | Encode or decode data using one of several base encodings. |
| [`cat`](https://uutils.github.io/coreutils/docs/utils/cat.html) | Concatenate FILE(s), or standard input, to standard output. |
| [`cksum`](https://uutils.github.io/coreutils/docs/utils/cksum.html) | Print CRC and size for each file. |
| [`comm`](https://uutils.github.io/coreutils/docs/utils/comm.html) | Compare two sorted files line by line. |
| [`cp`](https://uutils.github.io/coreutils/docs/utils/cp.html) | Copy SOURCE to DEST, or multiple SOURCE(s) to DIRECTORY. |
| [`csplit`](https://uutils.github.io/coreutils/docs/utils/csplit.html) | Split a file into sections determined by context lines. |
| [`cut`](https://uutils.github.io/coreutils/docs/utils/cut.html) | Print specified byte or field columns from each line of input. |
| [`date`](https://uutils.github.io/coreutils/docs/utils/date.html) | Print or set the system date and time. |
| [`df`](https://uutils.github.io/coreutils/docs/utils/df.html) | Show information about the file system on which each FILE resides. |
| [`dirname`](https://uutils.github.io/coreutils/docs/utils/dirname.html) | Strip the last component from a file name. |
| [`du`](https://uutils.github.io/coreutils/docs/utils/du.html) | Estimate file space usage. |
| [`echo`](https://uutils.github.io/coreutils/docs/utils/echo.html) | Display a line of text. |
| [`env`](https://uutils.github.io/coreutils/docs/utils/env.html) | Set each NAME to VALUE in the environment and run COMMAND. |
| [`expr`](https://uutils.github.io/coreutils/docs/utils/expr.html) | Print the value of EXPRESSION to standard output. |
| [`factor`](https://uutils.github.io/coreutils/docs/utils/factor.html) | Print the prime factors of the given NUMBER(s). |
| [`false`](https://uutils.github.io/coreutils/docs/utils/false.html) | Make a command always exit with 1. |
| [`find`](https://github.com/uutils/findutils) | Search for files in a directory hierarchy. |
| [`fmt`](https://uutils.github.io/coreutils/docs/utils/fmt.html) | Reformat paragraphs from input (or standard input) to stdout. |
| [`fold`](https://uutils.github.io/coreutils/docs/utils/fold.html) | Wrap each input line to fit a specified width. |
| `grep` | Print lines that match patterns. |
| [`head`](https://uutils.github.io/coreutils/docs/utils/head.html) | Print the first 10 lines of each FILE to standard output. |
| [`hostname`](https://uutils.github.io/coreutils/docs/utils/hostname.html) | Display or set the system's host name. |
| [`join`](https://uutils.github.io/coreutils/docs/utils/join.html) | Join two files on a common field. |
| [`la`](https://uutils.github.io/coreutils/docs/utils/ls.html) | List directory contents, including hidden entries (alias for `ls -A`). |
| [`link`](https://uutils.github.io/coreutils/docs/utils/link.html) | Call the `link` function to create a link named FILE2 to an existing FILE1. |
| [`ln`](https://uutils.github.io/coreutils/docs/utils/ln.html) | Make links between files. |
| [`ls`](https://uutils.github.io/coreutils/docs/utils/ls.html) | List directory contents. |
| [`md5sum`](https://uutils.github.io/coreutils/docs/utils/md5sum.html) | Print or check MD5 checksums. |
| [`mkdir`](https://uutils.github.io/coreutils/docs/utils/mkdir.html) | Create the given DIRECTORY(ies) if they do not exist. |
| [`mktemp`](https://uutils.github.io/coreutils/docs/utils/mktemp.html) | Create a temporary file or directory. |
| [`mv`](https://uutils.github.io/coreutils/docs/utils/mv.html) | Move SOURCE to DEST, or multiple SOURCE(s) to DIRECTORY. |
| [`nl`](https://uutils.github.io/coreutils/docs/utils/nl.html) | Number lines of files. |
| [`nproc`](https://uutils.github.io/coreutils/docs/utils/nproc.html) | Print the number of cores available to the current process. |
| [`numfmt`](https://uutils.github.io/coreutils/docs/utils/numfmt.html) | Convert numbers from or to human-readable strings. |
| [`od`](https://uutils.github.io/coreutils/docs/utils/od.html) | Dump files in octal and other formats. |
| [`pathchk`](https://uutils.github.io/coreutils/docs/utils/pathchk.html) | Check whether file names are valid or portable. |
| [`pr`](https://uutils.github.io/coreutils/docs/utils/pr.html) | Paginate or columnate FILE(s) for printing. |
| [`printenv`](https://uutils.github.io/coreutils/docs/utils/printenv.html) | Display the values of the specified environment VARIABLE(s). |
| [`printf`](https://uutils.github.io/coreutils/docs/utils/printf.html) | Print output based on a format string and arguments. |
| [`ptx`](https://uutils.github.io/coreutils/docs/utils/ptx.html) | Produce a permuted index of file contents. |
| [`pwd`](https://uutils.github.io/coreutils/docs/utils/pwd.html) | Display the full filename of the current working directory. |
| [`readlink`](https://uutils.github.io/coreutils/docs/utils/readlink.html) | Print the value of a symbolic link or canonical file name. |
| [`realpath`](https://uutils.github.io/coreutils/docs/utils/realpath.html) | Print the resolved absolute path. |
| [`rm`](https://uutils.github.io/coreutils/docs/utils/rm.html) | Remove (unlink) the FILE(s). |
| [`rmdir`](https://uutils.github.io/coreutils/docs/utils/rmdir.html) | Remove the DIRECTORY(ies), if they are empty. |
| [`seq`](https://uutils.github.io/coreutils/docs/utils/seq.html) | Display numbers from FIRST to LAST, in steps of INCREMENT. |
| [`sha1sum`](https://uutils.github.io/coreutils/docs/utils/sha1sum.html) | Print or check SHA1 checksums. |
| [`sha224sum`](https://uutils.github.io/coreutils/docs/utils/sha224sum.html) | Print or check SHA224 checksums. |
| [`sha256sum`](https://uutils.github.io/coreutils/docs/utils/sha256sum.html) | Print or check SHA256 checksums. |
| [`sha384sum`](https://uutils.github.io/coreutils/docs/utils/sha384sum.html) | Print or check SHA384 checksums. |
| [`sha512sum`](https://uutils.github.io/coreutils/docs/utils/sha512sum.html) | Print or check SHA512 checksums. |
| [`shuf`](https://uutils.github.io/coreutils/docs/utils/shuf.html) | Shuffle the input by outputting a random permutation of input lines. |
| [`sleep`](https://uutils.github.io/coreutils/docs/utils/sleep.html) | Pause for NUMBER seconds. |
| [`sort`](https://uutils.github.io/coreutils/docs/utils/sort.html) | Display sorted concatenation of all FILE(s). |
| [`split`](https://uutils.github.io/coreutils/docs/utils/split.html) | Create output files containing consecutive or interleaved sections of input. |
| [`stat`](https://uutils.github.io/coreutils/docs/utils/stat.html) | Display file or file-system status. |
| [`sum`](https://uutils.github.io/coreutils/docs/utils/sum.html) | Checksum and count the blocks in a file. |
| [`tac`](https://uutils.github.io/coreutils/docs/utils/tac.html) | Write each file to standard output, last line first. |
| [`tail`](https://uutils.github.io/coreutils/docs/utils/tail.html) | Print the last 10 lines of each FILE to standard output. |
| [`tee`](https://uutils.github.io/coreutils/docs/utils/tee.html) | Copy standard input to each FILE, and also to standard output. |
| [`test`](https://uutils.github.io/coreutils/docs/utils/test.html) | Check file types and compare values. |
| [`touch`](https://uutils.github.io/coreutils/docs/utils/touch.html) | Update the access and modification times of each FILE. |
| [`tr`](https://uutils.github.io/coreutils/docs/utils/tr.html) | Translate or delete characters. |
| [`true`](https://uutils.github.io/coreutils/docs/utils/true.html) | Make a command always exit with 0. |
| [`truncate`](https://uutils.github.io/coreutils/docs/utils/truncate.html) | Shrink or extend the size of each file to a specified size. |
| [`tsort`](https://uutils.github.io/coreutils/docs/utils/tsort.html) | Topological sort of the strings in FILE. |
| [`unexpand`](https://uutils.github.io/coreutils/docs/utils/unexpand.html) | Convert blanks in each FILE to tabs. |
| [`uniq`](https://uutils.github.io/coreutils/docs/utils/uniq.html) | Report or omit repeated lines. |
| [`unlink`](https://uutils.github.io/coreutils/docs/utils/unlink.html) | Unlink the file at FILE. |
| [`uptime`](https://uutils.github.io/coreutils/docs/utils/uptime.html) | Display the current time, system uptime, user count, and load averages. |
| [`wc`](https://uutils.github.io/coreutils/docs/utils/wc.html) | Print newline, word, and byte counts for each FILE. |
| [`xargs`](https://github.com/uutils/findutils) | Build and execute command lines from standard input. |
| [`yes`](https://uutils.github.io/coreutils/docs/utils/yes.html) | Repeatedly display a line with STRING (or `y`). |

## Related content

- [Coreutils for Windows overview](overview.md)
- [Shell conflicts](https://github.com/microsoft/coreutils#shell-conflicts)
