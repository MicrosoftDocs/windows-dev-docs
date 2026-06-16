# Windows Developer Docs — Agent Instructions

This file is read automatically by GitHub Copilot CLI, VS Code Copilot, and the GitHub Copilot coding agent. It provides context for AI agents working in the `windows-dev-docs-pr` repository.

## What this repo is

This is the **private preview** source for Windows developer documentation published at [learn.microsoft.com/windows/apps/](https://learn.microsoft.com/windows/apps/) and related paths. Changes here go through review before publishing.

> [!IMPORTANT]
> **API reference content is NOT in this repo.** API reference for WinRT, Win32, and Windows App SDK lives in separate Azure DevOps repositories. If a user asks you to edit API reference pages (namespaces, classes, methods, properties), stop and direct them to the docs team at jkendirs@microsoft.com.

## Directory structure

| Directory | Content |
|-----------|---------|
| `hub/` | **Primary content** — WinAppSDK, WinUI 3, tools, design, get-started, tutorials |
| `uwp/` | Legacy UWP content — migration to `hub/` is in progress; do not edit without checking |
| `terminal/` | Windows Terminal documentation |
| `WSL/` | Windows Subsystem for Linux documentation |
| `landing/` | Landing pages (index.yml files) |

Most new and updated conceptual content goes in `hub/`.

## Branch naming

Always create a new branch before making changes. Never commit directly to `main`.

Pattern: `{your-alias}-main/{brief-description}`

Examples:
- `jken-main/fix-winui3-quickstart`
- `pmsmith-main/update-drag-drop-sample`
- `aliasname-main/add-notificationmanager-overview`

## Pull request requirements

- **Target branch:** `main`
- **PR title:** brief, imperative ("Fix broken link in quickstart", "Add drag-and-drop overview")
- **PR description:** include the `learn.microsoft.com` URL(s) affected, and a one-line summary of what changed and why
- **After opening PR:** email jkendirs@microsoft.com so the docs team can review

## Required YAML front-matter

Every `.md` file must have this metadata block at the top:

```yaml
---
title: Title here (10–65 characters)
description: One-sentence summary (115–145 characters, no quotes)
author: GrantMeStrength
ms.author: jken
ms.topic: overview
ms.date: MM/DD/YYYY
---
```

Common `ms.topic` values: `overview`, `how-to`, `tutorial`, `article`, `reference`

When updating an existing file, always update `ms.date` to today's date.

## Writing style rules

- **Active voice, present tense, second person** — "you call the method", not "the method is called"
- **Neutral and instructional tone** — no marketing language ("powerful", "seamless", "easy")
- **Short paragraphs** — 3–4 sentences maximum
- **Procedural steps** — 7 or fewer; consolidate trivial actions into one step
- **Code blocks** — always include a language tag (` ```csharp `, ` ```cpp `, ` ```powershell `, ` ```console `)
- **Exact product names** — `Windows App SDK`, `Win32 API`, `WinUI 3`, `Windows Runtime`, `IntelliSense`
- **Do not abbreviate** namespaces in prose — write `Microsoft.UI.Xaml`, not `MUX`
- **Images** — always include descriptive `alt` text; never generate AI images

## What you can change

- ✅ Conceptual `.md` files in `hub/`, `terminal/`, `WSL/`
- ✅ Updating outdated version numbers, API names, or step-by-step instructions
- ✅ Fixing broken links (use relative paths within the repo when linking to other articles)
- ✅ Adding code examples (C# or C++, with language tags and explanatory comments)
- ✅ Fixing typos and grammar
- ✅ Updating YAML metadata (`ms.date`, `description`, `title`)

## What you must NOT change

- ❌ Files in `uwp/` — active migration in progress; check with the docs team first
- ❌ `.openpublishing.redirection.json` — redirects require special review
- ❌ `docfx.json` — repo configuration; do not touch
- ❌ `toc.yml` files — table of contents changes require docs team approval
- ❌ Files that look like API reference stubs (YAML-only, or tiny files with just metadata and parameter tables) — these belong in the DevOps API-ref repos, not here

## Publish timing

- Merges to `main` publish **nightly at approximately 10 PM Pacific time**
- Preview your changes at `review.learn.microsoft.com` before the PR is merged
- For urgent changes, email jkendirs@microsoft.com

## Pre-written prompts

Use these prompts with GitHub Copilot CLI or VS Code Copilot to perform common tasks. Fill in the bracketed placeholders.

### Fix outdated content

```
The page at [URL] has outdated information. Specifically: [describe what is wrong and what the correct information is].

Find the markdown source file in this repo, make only the necessary changes, create a branch named [alias]-main/fix-[description], commit the changes with a clear message, and open a pull request targeting main. Include the affected URL in the PR description.
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

Generate the markdown file with proper YAML front-matter (title 10–65 chars, description 115–145 chars, author: GrantMeStrength, ms.author: jken, ms.topic: overview, ms.date: today's date). Use active voice, present tense, second person. No marketing language. Include at least one C# or C++ code example with a language tag and explanatory comments. Create a branch named [alias]-main/add-[description], commit, and open a PR targeting main.
```

## Contacts

| Area | Contact |
|------|---------|
| Windows Developer docs | John Kennedy — jkendirs@microsoft.com |
| Drivers / Server docs | Robin Harwood |

For questions about API reference repos (Win32, WinRT, Windows App SDK), contact jkendirs@microsoft.com — do not attempt to edit those files here.
