# Windows Developer Docs — Agent Instructions

This file is read automatically by GitHub Copilot CLI, VS Code Copilot, and the GitHub Copilot cloud agent. It provides context for AI agents working in the `windows-dev-docs-pr` repository.

## What this repo is

This is the **private preview** source for Windows developer documentation published at [learn.microsoft.com/windows/apps/](https://learn.microsoft.com/windows/apps/) and related paths. Changes here go through review before publishing.

> [!IMPORTANT]
> **API reference content is NOT in this repo.** API reference for WinRT, Win32, and Windows App SDK lives in separate Azure DevOps repositories. If a user asks you to edit API reference pages (namespaces, classes, methods, properties, parameters, or types), stop and direct them to John Kennedy at jken@microsoft.com.

## Directory structure

| Directory | Content |
|-----------|---------|
| `hub/` | **Primary content** — Windows App SDK, WinUI 3, tools, design, get-started, tutorials |
| `uwp/` | Legacy UWP content — migration to `hub/` is in progress; do not edit |
| `terminal/` | Windows Terminal documentation |
| `WSL/` | Windows Subsystem for Linux documentation |
| `landing/` | Landing pages (index.yml files) |

Most new and updated conceptual content goes in `hub/`.

## Requests that start from a learn.microsoft.com URL

Most change requests give you the URL of a published page, not a file path. To find the source file:

1. If you can fetch the page, read its `original_content_git_url` meta tag. It contains the source path.
2. Otherwise, map the URL path to the repo:
   - `/windows/apps/...` → `hub/apps/...`
   - `/windows/terminal/...` → `terminal/...`
   - `/windows/wsl/...` → `WSL/...`
   - `/windows/uwp/...` → `uwp/...` (do not edit; see "When to stop instead of editing")
   - A URL that ends in `/` maps to `index.md` or `index.yml` in that folder.
3. Confirm the match: the file's `title` metadata must match the published page title.
4. If you can't find a confident match, stop and report what you searched. Never edit a different file instead.

## Scope rules for change requests

- Change only what the request describes. Don't rewrite, reorganize, or restyle other content.
- If you notice other problems, list them under **Other issues noticed** in the PR description. Don't fix them in the same PR.
- On existing files, keep `author` and `ms.author` as they are. Update `ms.date` only.
- Include the requester's source of truth (spec, header file, PR, release notes, or "PM confirmation") in the PR description.

## When to stop instead of editing

Don't make the change. Instead, explain why in a draft PR or an issue comment when:

- The source file is in `uwp/`
- The change requires edits to `toc.yml`, `docfx.json`, or `.openpublishing.redirection.json`
- The request is about API reference content (signatures, parameters, types, or API reference pages)
- The requested change contradicts other content in this repo
- You can't identify the source file with confidence

## Check for existing content before adding a topic

Before you create a conceptual article or add substantial standalone guidance, search the generated content catalog:

```console
python tools/content_index/content_index.py search "describe the proposed topic"
```

Review both current and legacy matches. Prefer, in this order:

1. Update an existing canonical article if it serves the same audience and scenario.
2. Add the missing information to an existing article and link to it from related pages.
3. Create a new article only when the existing coverage has a materially different audience, product, version, programming language, or reader goal.

For a new topic, also search the available catalogs from `MicrosoftDocs/win32-pr`, `MicrosoftDocs/windows-driver-docs-pr`, and `MicrosoftDocs/windows-ai-docs-pr`. Pass each downloaded or locally checked-out catalog with an additional `--index` argument as described in `tools/content_index/README.md`.

When you create a topic, include the catalog search terms and the reason existing pages do not meet the reader need in the pull request description. Do not copy an explanation or procedure into multiple articles when a link to canonical coverage is sufficient.

New topics need a table of contents entry, but you must not edit `toc.yml`. In the PR description, propose the TOC entry: the `toc.yml` file, the parent node, and the entry name and `href`.

After you change conceptual Markdown, regenerate the catalog and commit the result:

```console
python tools/content_index/content_index.py build
```

If Python isn't available in your environment, don't edit the index manually. Instead, note in the PR description that the index needs regenerating. The **Conceptual content index** check uploads a corrected file that can be committed (see the next section).

### Repair a stale content index in an existing PR

If the **Conceptual content index** check fails because `.github/content-index.jsonl` is stale, update the existing PR branch. The JSONL file is generated, but committing the generator's output is required and permitted. Do not edit the JSONL manually.

From the repository root, run:

```console
python tools/content_index/content_index.py build
python tools/content_index/content_index.py check
python -m unittest tools.content_index.test_content_index
```

Commit the regenerated `.github/content-index.jsonl` file to the PR branch. Remove `tools/content_index/__pycache__/` if Python creates it, and don't stage unrelated working-tree changes.

When the check finds a stale index, the workflow uploads the corrected file as the `generated-content-index` artifact on the failed workflow run. You can download that artifact and commit its `content-index.jsonl` file as `.github/content-index.jsonl`, or use the local commands above. The workflow doesn't push the correction automatically because commits made with its GitHub token wouldn't trigger the other required PR checks.

Use `python tools/content_index/content_index.py audit` to produce likely duplicate pairs for human review. Similarity is not proof of duplication; account for audience, product, language, version, and lifecycle differences before consolidating content.

## Review and triage skills

- When asked to clean up PR discussions or reconcile SME feedback, use `.github/skills/pr-review-cleanup/SKILL.md`. A review-only request does not authorize discussion changes.
- When asked for morning issue and PR triage, use `.github/skills/windows-docs-daily-triage/SKILL.md`. Read its run profile and action limits first; installing the skill does not schedule or authorize a run.

## Branch naming

Always create a new branch before making changes. Never commit directly to `main`.

Pattern: `{your-alias}-main/{brief-description}`

Examples:

- `jken-main/fix-winui3-quickstart`
- `pmsmith-main/update-drag-drop-sample`
- `aliasname-main/add-notificationmanager-overview`

Exception: branches created by the GitHub Copilot cloud agent can use its default naming (for example, `copilot/...`).

## Pull request requirements

- **Target branch:** `main`
- **PR title:** brief, imperative ("Fix broken link in quickstart", "Add drag-and-drop overview")
- **PR description:** include:
  - the affected `learn.microsoft.com` URL(s)
  - a one-line summary of what changed and why
  - the source of truth for the change
  - **Other issues noticed**, if any
- **Review:** John Kennedy is added as reviewer automatically through CODEOWNERS. Don't attempt to send email.
- **Requester:** the person who asked for the change confirms technical accuracy on the PR before it's reviewed.
- **Urgent changes:** the requester emails jken@microsoft.com.

## Required YAML front-matter

Every `.md` file must have this metadata block at the top:

```yaml
---
title: Title here (–65 characters)
description: One-sentence summary (115–145 characters, no quotes)
author: GrantMeStrength
ms.author: jken
ms.topic: overview
ms.date: MM/DD/YYYY
---
```

Common `ms.topic` values: `overview`, `how-to`, `tutorial`, `article`, `reference`

- **New files:** use the block above.
- **Existing files:** update `ms.date` to today's date. Keep all other existing values, including `author` and `ms.author`, unless the request is specifically to change them.

## Writing style rules

- **Active voice, present tense, second person** — "you call the method", not "the method is called"
- **Neutral and instructional tone** — no marketing language ("powerful", "seamless", "easy")
- **Short paragraphs** — 3–4 sentences maximum
- **Procedural steps** — 7 or fewer; consolidate trivial actions into one step
- **Code blocks** — always include a language tag (`csharp`, `cpp`, `powershell`, `console`)
- **Exact product names** — `Windows App SDK`, `Win32 API`, `WinUI 3`, `Windows Runtime`, `IntelliSense`
- **Do not abbreviate** namespaces in prose — write `Microsoft.UI.Xaml`, not `MUX`
- **Images** — always include descriptive `alt` text; never generate AI images

## What you can change

- ✅ Conceptual `.md` files in `hub/`, `terminal/`, `WSL/`
- ✅ Updating outdated version numbers, API names in conceptual content, or step-by-step instructions
- ✅ Fixing broken links (use relative paths within the repo when linking to other articles)
- ✅ Adding code examples (C# or C++, with language tags and explanatory comments)
- ✅ Fixing typos and grammar
- ✅ Updating YAML metadata (`ms.date`, `description`, `title`)

## What you must NOT change

- ❌ Files in `uwp/` — active migration in progress; check with John Kennedy first
- ❌ `.openpublishing.redirection.json` — redirects require special review
- ❌ `docfx.json` — repo configuration; do not touch
- ❌ `toc.yml` files — table of contents changes require John Kennedy's approval; propose them in the PR description instead
- ❌ Files that look like API reference stubs (YAML-only, or tiny files with just metadata and parameter tables) — these belong in the DevOps API reference repos, not here

## Publish timing

- Merges to `main` publish **automatically after PR has been merged**
- Preview your changes at `review.learn.microsoft.com` before the PR is merged
- For urgent changes, email jken@microsoft.com

## Pre-written prompts

Use these prompts with GitHub Copilot on github.com, GitHub Copilot CLI, or VS Code Copilot. Fill in the bracketed placeholders.

### Request a doc fix (recommended for PMs and feature owners)

```
Update a published Windows developer docs topic. Follow the AGENTS.md instructions in this repo.

Topic URL: [learn.microsoft.com URL]

What's wrong:
[Describe the problem.]

What it should say:
[The correct information. Exact wording is optional.]

Source of truth:
[Spec, header file, PR, release notes, or "PM confirmation".]

Change only what's described above. Open a pull request targeting main, and list any other issues you notice in the PR description instead of fixing them.
```

### Fix outdated content

```
The page at [URL] has outdated information. Specifically: [describe what is wrong and what the correct information is].
Source of truth: [spec link, header file path, release notes, or "PM confirmation"]

Find the markdown source file in this repo, make only the necessary changes, create a branch named [alias]-main/fix-[description], commit the changes with a clear message, and open a pull request targeting main. Include the affected URL and the source of truth in the PR description.
```

### Add missing information

```
The page at [URL] is missing information about [topic].

The new section should cover: [describe the content]
Source / authoritative reference: [spec link, header file path, or description]

Find the markdown source file, add a new H2 or H3 section in the appropriate place, create a branch named [alias]-main/add-[description], commit, and open a PR targeting main.
```

### Fix a broken link

```
There is a broken link on the page at [URL]. The broken link text is "[link text]" and it should point to [correct destination or describe what it should link to].

Find the markdown file, fix the link, create a branch named [alias]-main/fix-broken-link, commit, and open a PR.
```

### Create a new overview topic

```
Create a new conceptual overview topic for [feature or concept name].

Audience: Windows developers (intermediate level)
Key points to cover: [list the main things the page should explain]
Related pages to link to: [URLs of related articles]
File location: hub/apps/[appropriate-subdirectory]/[filename].md

First, search the content catalog as described in AGENTS.md and confirm no existing article covers this. Generate the markdown file with proper YAML front-matter (title 10–65 chars, description 115–145 chars, author: GrantMeStrength, ms.author: jken, ms.topic: overview, ms.date: today's date). Use active voice, present tense, second person. No marketing language. Where a code example helps the reader, include C# or C++ with a language tag and explanatory comments. Don't edit toc.yml; propose the TOC entry in the PR description. Create a branch named [alias]-main/add-[description], commit, and open a PR targeting main.
```

## Contacts

| Area | Contact |
|------|---------|
| Windows developer docs, including API reference questions (Win32, WinRT, Windows App SDK) | John Kennedy — jken@microsoft.com |
| Drivers / Server docs | Robin Harwood |

Do not attempt to edit API reference files in this repo.
