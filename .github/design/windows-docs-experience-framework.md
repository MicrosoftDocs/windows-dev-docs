---
title: Windows documentation experience framework
description: Define a WinUI-first documentation framework with evidence-based journeys, reusable visual patterns, and editorial quality gates.
author: GrantMeStrength
ms.author: jken
ms.topic: article
ms.date: 10/02/2026
---

# Windows documentation experience framework

## Purpose and status

This is an internal design framework for the Windows app documentation experience. It defines how to select, organize, write, and visually present content. The draft implementation now applies the reviewed Essentials experience to canonical pages and real ToCs; it is not a new publishing schema or a requirement to rewrite every article.

The intended promise is: **build a new app with WinUI 3, or bring an existing app to WinUI 3.** Readers should reach a useful result without first reconstructing the Windows platform taxonomy.

The working navigation direction is **Getting started, Design, Add Windows features, Modernize an existing app, Package and deploy, Distribute through the Store, Tools, Code samples**. Final implementation requires review. This document preserves the Windows App SDK collection, gives tools a dedicated ProductSubHub and ToC, keeps code samples directly accessible, and treats modernization as a distinct entry journey.

Research date: September 7, 2026. The pre-change content baseline is commit `fbd4d0833`; some source links below now resolve to revised pages in this draft. The comparison examines representative official entry pages, quickstarts, and migration guidance, not entire competing documentation sites. Observations about structure are evidence; predictions about reader confusion are hypotheses to test, not measured abandonment or conversion rates.

## 1. What needs to change, and what should stay

The problem is not that the documentation lacks useful material. The main homepage already links directly to a short WinUI quickstart. The Notes tutorial has substantive instruction and sample links. The AI-assisted tutorial teaches framework constraints and observable acceptance criteria. The Gallery makes controls tangible.

The problem is that those assets do not consistently form one understandable experience.

| Observed condition | Evidence in this repository | Consequence to investigate | Design response |
|---|---|---|---|
| Background precedes the main action. | [The first ToC](../../hub/apps/toc.yml) expands Core concepts, including SDK release history, before Start here. | New readers may interpret background as prerequisite work. | Put the first-app path first; keep the SDK collection intact in a later platform group. |
| Several pages offer similar beginnings. | The [homepage](../../hub/apps/index.yml), former platform introduction, [path chooser](../../hub/apps/get-started/index.yml), and [WinUI getting-started overview](../../hub/apps/get-started/winui-get-started-overview.md) offered overlapping orientation. | Readers may compare introductions instead of starting. | Retire the overlapping introduction and converge on one canonical first-run route. |
| Setup descriptions disagree. | [Quickstart](../../hub/apps/get-started/start-here.md), homepage, and WinUI getting-started overview use different workload or template descriptions. | Readers and agents may assemble an incompatible setup. | Maintain one setup contract; distinguish supported platform versions from the tutorial's selected toolchain. |
| A tooling promise conflicts with another article. | The quickstart advertises visual designer support; [runtime design tools](../../hub/apps/develop/ui/xaml-runtime-design-tools.md) states that the WinUI 3 XAML Designer is unavailable. | Readers may mistake a product limitation for a broken installation. | Name limitations where they affect a decision; do not imply a runtime tool is an equivalent design-time replacement. |
| The first-run handoff changes scale abruptly. | The quickstart ends at a running window; [Notes project setup](../../hub/apps/tutorials/winui-notes/project.md) introduces project anatomy, a custom title bar, and navigation infrastructure. | Readers may lose momentum before implementing behavior. | Continue the same project with one small UI change before a larger application tutorial. |
| Tools and samples have competing entry points. | The first ToC, [Develop ToC](../../hub/apps/develop/toc.yml), and DocsRoot's `content-nav/MSDocsHeader-WinDevCenter.yml` distribute tools, samples, and agent setup across sections. | The route depends on where the reader enters. | Give Tools a canonical landing page and ToC, keep code samples directly accessible, and retain useful contextual links elsewhere. |
| Machine-oriented content has a separate version snapshot. | [Root llms.txt](../../llms.txt) describes Visual Studio 2022 setup and its own current-version list, while the quickstart describes Visual Studio 2026 and a .NET 10 CLI baseline. | An agent may follow a different contract from a human. | Align machine entry points with canonical setup and release sources instead of maintaining independent version claims. |

Do not conclude that cards are inherently better than prose or every overview is redundant. Equally, an existing article does not earn a place in the new experience merely because it exists. Preserve authoritative knowledge and meaningful destinations, not the current page count.

### Scope decisions before visual redesign

The September 7 editorial direction is to drop **Coming to Windows development** from the WinUI experience, not rename it or create a replacement orientation article. It mixes workstation setup, framework selection, migration, and in-place modernization. Existing developer-environment and framework hubs already serve those audiences.

Being useful somewhere in Windows documentation is not sufficient justification for belonging in WinUI Essentials. Review recent additions against the same standard as older content; a recent date or attractive layout is not evidence of a necessary reader task.

Use four dispositions before rewriting:

| Disposition | Meaning |
|---|---|
| Keep | The content has a distinct WinUI task or necessary explanatory role; retain it after technical review. |
| Consolidate | Preserve useful information in its canonical page instead of retaining competing introductions or answers. |
| Route elsewhere | The task belongs to another documentation hub; link to its existing guidance rather than copying it here. |
| Retire | The article adds no distinct value in this experience. Do not rewrite it merely to save the page. |

The initial Essentials inventory is:

| Content | Disposition | Rationale and treatment |
|---|---|---|
| [Coming to Windows development](../../hub/apps/whats-new/coming-to-windows.md) | **Retire from this experience; confirmed direction** | Leave it out of the PoC. Workstation migration, WSL, and broad framework selection belong in their existing hubs. WinUI app migration has its own destination. No replacement article is needed. |
| [Windows developers and their tools](../../hub/apps/whats-new/personae.md) | **Retirement candidate** | Named developer biographies, broad technology menus, and generic tips do not establish a concrete WinUI task. Do not turn them into another set of WinUI personas merely to retain the format. |
| Former platform introduction, [development-path chooser](../../hub/apps/get-started/index.yml), and [WinUI getting-started overview](../../hub/apps/get-started/winui-get-started-overview.md) | **Consolidate** | Use the WinUI landing page for orientation and the tabbed quickstart for setup and first success. Retire the platform introduction through an approved redirect. Keep the workflow overview as a short pointer pending its separate HTTP redirect review. |
| [Windows developer FAQ](../../hub/apps/get-started/windows-developer-faq.md) | **Keep only focused questions; consolidate the rest** | Questions about the SDKs, tooling, and limitations can help. Generic advocacy such as why client apps matter in digital transformation does not help someone build a WinUI app. Link answers to authoritative owners rather than duplicate full guides. |
| [Windows developer glossary](../../hub/apps/get-started/windows-developer-glossary.md) | **Keep and scope** | WinUI terminology and app-model distinctions support understanding. Review unrelated product definitions and duplicate explanations; it need not become an encyclopedia of the wider Microsoft ecosystem. |
| [Windows app development best practices](../../hub/apps/get-started/best-practices.md) | **Refocus or consolidate** | Retain actionable app-quality guidance with clear criteria. Move detailed design and implementation instructions to their canonical sections and remove generic assertions that lack actionable consequences. |
| Windows App SDK and Windows SDK collections | **Keep together** | These have a distinct reference and platform-understanding role. Do not distribute their pages across menus merely to fill cards. |
| Notes and AI-assisted tutorials | **Keep distinct learning roles** | A deterministic code tutorial and an assistant-directed workflow can both be useful. Resolve project/setup drift and make their entry conditions clear; coordinate with existing tutorial drafts. |
| Line-of-business recipes | **Keep as task guidance, not required onboarding** | Forms, data display, and persistence are concrete tasks. Offer them after first-run success or through Features rather than presenting the whole collection as prerequisites. |

Except for the confirmed Coming to Windows direction, these are review recommendations, not approved production deletions. This inventory evaluates content purpose; it does not certify every technical claim in a retained article.

Exclude retired candidates from new PoC routes before investing in their artwork or prose. Production retirement is a separate change: inventory inbound links, identify the correct destination where one exists, and obtain the required ToC and redirect review. Do not delete a published source in isolation or redirect unrelated intents indiscriminately to the WinUI quickstart.

## 2. What other documentation experiences teach us

### OpenAI: reach one result, then add capabilities

The [Agents SDK entry page](https://developers.openai.com/api/docs/guides/agents) leads with running a first agent, then routes readers to additional capabilities. Its task table explains not only where to go, but why that destination is relevant. The [quickstart](https://developers.openai.com/api/docs/guides/agents/quickstart) starts with a small integration and adds capabilities around the same basic example.

**Apply here:** make a running WinUI app the first milestone, then extend that app. Describe follow-up links by the reader's task rather than by department or SDK component. A capability catalog is useful after the first success and as a direct entry for experienced developers.

**Do not copy blindly:** a desktop app requires a host environment, native build tooling, UI inspection, and deployment context. We cannot promise API-quickstart setup costs. The Agents SDK page is a narrower surface than the entire Windows documentation site; compare it to the WinUI entry experience, not the Windows umbrella.

OpenAI also advertises [machine-readable documentation indexes](https://developers.openai.com/llms.txt). This is not evidence that Windows lacks one: this repository already has `llms.txt`. The lesson is discoverability and agreement with current instructions, not adding another index for its own sake.

### Anthropic: distinguish starting, building, and looking up

The [Claude developer homepage](https://platform.claude.com/docs/en/home) separates immediate actions, developer-surface choices, and a lifecycle journey. Its published content includes dedicated journey tabs and steps. The Messages route separates getting started, building, evaluating and shipping, and operating.

The [quickstart](https://platform.claude.com/docs/en/get-started) provides language-specific steps, filenames, commands, and expected output. It recommends a next topic before offering broader exploration links.

**Apply here:** make page purpose visible. An entry page routes; a quickstart gets something working; a feature guide solves a task; reference supports lookup. Use a small, consistent set of visual patterns to express these roles.

**Do not copy blindly:** tabs and card components are implementation details, not the reason the route works. Do not reproduce a component-heavy interface without clear destinations. The inspected content does not establish persistent progress or cross-page preference behavior.

### Apple: connect code, visible UI, design, and learning progression

Apple's [SwiftUI getting-started pathway](https://developer.apple.com/swiftui/get-started/) progresses through framework introduction, views, design, navigation, and data. It connects tutorials with design guidance and deeper explanations rather than treating design as an unrelated destination.

**Apply here:** show what a reader is building and connect each new concept to a visible change. Introduce Windows design and accessibility as part of implementing a feature, while retaining dedicated reference destinations. Use genuine app screenshots or focused diagrams to explain results.

**Do not copy blindly:** the pathway includes substantial explanatory prose and video-led learning. That is useful for guided study, but it should not precede our shortest first-run route. WinUI tooling must be described on its own merits; SwiftUI previews do not establish equivalent WinUI designer functionality.

Apple's [SwiftUI overview](https://developer.apple.com/swiftui/), [AppKit integration](https://developer.apple.com/documentation/swiftui/appkit-integration), and [lifecycle migration guide](https://developer.apple.com/documentation/swiftui/migrating-to-the-swiftui-life-cycle) also make room for existing code. A preferred destination framework need not imply an all-at-once rewrite.

### Android: separate first UI, foundations, and adoption

Android's [Compose documentation entry](https://developer.android.com/develop/ui/compose/documentation) distinguishes its overview, tutorial, focused quick guides, and foundational topics. Its foundations describe concepts such as state and lifecycle; they are not presented as a language-neutral catalog of every Android UI option.

The [migration strategy](https://developer.android.com/develop/ui/compose/migrate/strategy) has a clear destination: Compose. It recommends new Compose screens, reusable components, and incremental replacement of existing screens.

**Apply here:** make WinUI 3 the default, retain a coherent platform explanation, and provide a separate route for existing-app developers. A feature recipe should answer a task such as displaying data, not restart the full platform introduction.

**Do not copy blindly:** migration feasibility depends on actual WinUI interoperability and feature coverage. Do not promise Android's adoption mechanics or hide missing equivalents. The existence of a tutorial or a migration guide does not establish that either must occupy a top-level menu item.

### Synthesis

| Pattern worth adopting | Change it motivates | Pattern to avoid |
|---|---|---|
| A clearly preferred first outcome | One canonical WinUI first-run route | A framework-selection survey before every new app |
| A sequence around the same artifact | Run, change, and extend one project | Restarting with a new template at each transition |
| Intent-based destinations | Titles and descriptions that distinguish tasks | Generic Overview, Explore, and Learn more labels everywhere |
| Code linked to visible results | Screenshots, expected behavior, and recovery steps | Decorative artwork substituted for proof |
| Optional depth and direct lookup | A coherent Windows App SDK collection plus contextual links | Splitting SDK material merely to balance menus |
| Explicit migration toward a destination | A WinUI migration entry for existing applications | Treating all modernization as UI migration |

The competitors provide useful patterns, not a scorecard proving that one site is universally better. The target is lower uncertainty and more successful tasks, not visual resemblance.

## 3. Organize by reader intent without fragmenting ownership

The proposed L0 responsibilities are:

| Section | Owns | Does not own |
|---|---|---|
| Getting started | First-app path, guided beginner learning, platform orientation | The complete tools catalog |
| Design | UX principles, visual foundations, interaction and accessibility guidance | Every API implementation detail |
| Add Windows features | Implementing UI, data, Windows integration, and app-quality behavior | Generic setup or an alternative resource directory |
| Modernize an existing app | Assessment, framework mappings, migration steps, and incremental adoption | Guidance limited to full UI migration to WinUI 3 |
| Package and deploy | Packaging, identity, runtime delivery, signing, distribution mechanics | Store listing and account management |
| Distribute through the Store | Store submission, listings, certification, commerce, and management | Every possible distribution channel |
| Tools | Developer environments, command-line workflows, coding-assistant setup, testing, and automation | A second authoritative copy of SDK releases or setup requirements |
| Code samples | Focused examples, WinUI Gallery, and links to maintained sample repositories | A duplicate tools catalog or tutorial hierarchy |

Within the first section's ToC, use **Build with WinUI** as a non-clickable group and put **Get started** first within it, before the first-app quickstart and tutorials. Keep **Understand the platform** as the next top-level group, with Windows App SDK overview, channels, support, release notes, and downloads grouped within it. Tools and code-samples shortcuts do not move ownership of the SDK collection.

Migration remains a proposed L0 because existing-app developers have a distinct job, not because Azure has the same label. Link to it from Get started without duplicating its full sidebar. Keep framework-specific modernization in its respective documentation and retain a single authoritative home for shared SDK guidance.

Treat the three navigation surfaces differently:

| Surface | Responsibility |
|---|---|
| L0 menu | Stable destinations for major reader intents |
| Section landing page | Curated starting points and important tasks |
| Section ToC | The complete local collection and its hierarchy |

A journey crosses those surfaces without copying their content. Documentation can have multiple entry links while retaining one canonical owner. Moving navigation does not require renaming source folders or published URLs.

## 4. Start every page with a brief, not generated prose

Before choosing a layout, write a short authoring brief. This is an editorial worksheet, not metadata that Learn automatically understands.

| Brief field | Required answer |
|---|---|
| Reader and starting state | Who arrives, what they already have, and what they do not need to know |
| Primary job | The task or decision this page exists to support |
| Outcome | What the reader can do or understand afterward |
| Scope | Framework, language, setup assumptions, and relevant limitations |
| Evidence | Authoritative sources, working sample or example, and ownership of volatile claims |
| Main action and continuation | What the reader does now and the most useful next step |
| Exclusions | What this page deliberately leaves elsewhere |
| Visual purpose | What each proposed asset or component helps the reader recognize, decide, or do |

If two pages have essentially the same brief, consolidate or differentiate their jobs before polishing them. If a page has no meaningful action or explanatory purpose, a card grid will not fix it.

For a journey, add prerequisites, milestones, completion evidence, and recovery links. Preserve the selected project assumptions across steps. A later page that deliberately changes them must explain why.

## 5. Use page contracts, not one universal template

Consistency means readers recognize how a page works. It does not mean every page has three scenarios, three cards, or a "What you will learn" section.

| Page type | Required content | Optional content | Avoid |
|---|---|---|---|
| L0 landing | Scope, a clear primary route, distinct destinations, access to deeper content | A scenario collection, relevant visual, direct reference links | Reproducing the complete ToC or a long product pitch |
| Quickstart | Starting state, exact setup, small sequence, expected result, recovery, continuation | A concise outcomes checklist, language or tool variants | Full architecture education or publishing as part of the first-run requirement |
| Guided tutorial | Final artifact, learning outcomes, prerequisites, incremental milestones, runnable source | Optional concept links, an assistant-assisted mode | Changing sample projects without explanation |
| Task guide | When to use it, context, focused implementation, result and limitations | Alternatives when the choice matters | Repeating general onboarding |
| Platform or SDK overview | A clear mental model, relationships, boundaries, authoritative collection links | A diagram, comparison, or small explanatory example | An unmaintained copy of current-version claims |
| Migration guide | Source and destination, assessment, mappings, gaps, reuse, migration sequence | A supported staged-adoption route, case study | Describing an unsupported conversion as automatic |
| Resource directory | What each resource does, who it serves, relevant prerequisites, destination | Filters or categories when the collection warrants them | Calling every resource essential or requiring every tool |

Use "What you will learn" for actual instruction. On a routing page, explain the destinations instead. On a task page, state the result directly. A reference directory does not need a learning promise.

As a starting heuristic, try one primary action and two to four genuinely distinct choices on a landing page. This is a design hypothesis, not a quota. Use fewer when fewer exist; create a separate catalog when breadth demands it. Do not invent a third scenario or an extra resource to satisfy visual symmetry or a template minimum.

## 6. Build a small visual vocabulary

### Primary action

Use an explicit destination such as "Create your first WinUI app." Distinguish it from supporting actions through the available layout. A primary action should move the reader toward the page's outcome, not another introduction with the same promise.

### Scenario card

Use a title, a short differentiating description, and a clear destination. An icon is optional. Prefer cards when readers are choosing between meaningful tasks, not when they are following mandatory consecutive steps.

Example content for an implementation directory:

| Title | Description |
|---|---|
| Display a collection | Choose a WinUI list control and bind it to your app's data. |
| Open a file | Follow the picker guidance that applies to your app and Windows App SDK version. |
| Show a confirmation | Use a dialog to confirm an action and handle the user's choice. |

These are examples of editorial structure, not promises that every destination is ready for publication.

### Horizontal resource card

Use this pattern for compact, scannable collections such as tools and samples. The intended composition is:

```text
[icon]  WinUI Gallery                         [destination indicator]
        Browse controls and inspect their example code.
```

The icon sits beside the text instead of consuming a large banner area. The title identifies the resource; the description explains why to open it. Keep a stable icon size and text alignment across the collection. On narrow screens, reflow rather than truncate essential text.

A navigation card is a link, not a button that performs an action. If an entire card is clickable, use one accessible link target and do not nest other links inside it. If the card contains several destinations, use distinct text links instead. Do not rely on an arrow, icon, color, or hover state as the only indication of a destination.

The exact horizontal rendering is a proof-of-concept question. Existing Markdown/HTML patterns and new Learn templates are candidates; neither a YAML field nor a local approximation proves the intended published layout.

### Journey milestone

Show an action, its result, and the next step. A sequence can be useful without persistent progress tracking.

```text
Make your first UI change
Starting point: Your WinUI app already launches.
Outcome: Clicking a button changes the text in the window.

[Instructions and a focused example]
[Expected result and troubleshooting]

Continue: Display a collection
Optional: Understand XAML and code-behind
```

Do not mark a milestone complete merely because a reader opened the page. Do not imply that a numbered card list detects success or remembers progress.

### Assets that explain

Use real UI screenshots to establish an expected result, diagrams to clarify relationships, and designed icons to help scan categories. Do not place a generic platform illustration before every article or repeat the same banner where it adds no information.

Give the design team a brief containing the asset's purpose, placement, rendered size, light/dark requirements, accessibility requirements, source ownership, and update trigger. Prefer reusable assets whose meaning survives a product release. Do not generate AI artwork or place essential instructions solely inside an image.

## 7. Keep content independent from the rendering choice

This repository already uses image-and-column layouts, linked images, and HTML buttons, including the [WinUI overview](../../hub/apps/winui/winui3/index.md) and [shared overview content](../../hub/includes/apps-essentials-overview.md). Those are existing patterns to evaluate, not a reason to prohibit useful visual composition.

The [Markdown reference](https://learn.microsoft.com/contribute/content/markdown-reference) supports next-step actions and other extensions, but cautions against custom column layouts and unrestricted HTML. Working examples, schema acceptance, accessibility, and publishing behavior are different questions. Do not infer that arbitrary CSS or JavaScript is available from the presence of an HTML button.

| Rendering route | Use it when | Constraint |
|---|---|---|
| Markdown and existing HTML/column patterns | A guide needs narrative, code, results, and modest visual navigation | Preserve semantics and reading order; inspect the actual Learn result |
| Hub or Landing YAML | A standard curated index satisfies the page brief | The renderer controls appearance and supported fields |
| ProductHub or ProductSubHub YAML | The new structured cards and workflow groups fit the intended page | These templates are already being explored in draft PRs; do not treat them as arbitrary layout systems |
| A new platform component | A required behavior or presentation cannot be expressed by available patterns | Scope and review it with the platform team rather than inventing undocumented authoring syntax |

Existing experiments are the starting point: [PR #7250](https://github.com/MicrosoftDocs/windows-dev-docs-pr/pull/7250) includes template studies and a numbered WinUI journey; [PR #7263](https://github.com/MicrosoftDocs/windows-dev-docs-pr/pull/7263) explores compact PowerToys cards. Reuse the findings instead of restarting template discovery.

The published [ProductHub](https://learn.microsoft.com/static/ui/latest/schemas/ProductHub.schema.json) and [ProductSubHub](https://learn.microsoft.com/static/ui/latest/schemas/ProductSubHub.schema.json) schemas define the available fields. They do not establish pixel-level control. The [JourneyLearnSection](https://learn.microsoft.com/static/ui/latest/schemas/partials/JourneyLearnSection.partial.schema.json) is training-oriented, not a general journey through arbitrary articles.

Do not choose the new template because it is new, or reject an existing Markdown pattern because it contains HTML. Choose the least complex approach that fulfills the page brief and the published experience requirements.

## 8. Prevent generic, repetitive, ungrounded content

In this framework, "AI slop" means content that looks complete but fails to help: interchangeable prose, repeated introductions, unsupported claims, artificial symmetry, or decoration without purpose. Human-written content can fail the same way. AI assistance is not the failure criterion.

Apply these seven rules:

1. **Require a specific job.** If the introduction could describe another developer platform after replacing the product name, rewrite it around the actual task.
1. **Make every section earn its place.** Keep it only if it helps a reader decide, act, understand, recover, or find an authoritative resource.
1. **Ground claims before polishing copy.** Check API names, templates, versions, commands, and limitations against current sources. AI-generated confidence is not evidence.
1. **Remove competing starts and unnecessary choices.** Do not ask readers to reselect their framework, toolchain, or sample at each transition.
1. **Use honest outcome language.** Distinguish a submitted app from a published app and a successful build from a working interaction. Do not invent completion times or describe unsupported behavior as easy.
1. **Avoid forced decoration and repetition.** Do not add filler cards, universal checklists, large illustrations, or generic closing sections to make pages match.
1. **Protect maintenance.** Give volatile facts one authoritative owner, identify who reviews the page, and update machine-facing summaries alongside human guidance.

Examples of a stronger editorial direction:

| Weak copy or structure | Better direction |
|---|---|
| Unlock powerful Windows experiences | Create a WinUI app and run it on your Windows device |
| Explore our comprehensive resources | Find the WinUI Gallery, project samples, and development tools |
| Three benefits, three scenarios, and three next steps on every page | Include only the decisions and outcomes this page actually supports |
| Visual designer support | Name the available editing and runtime inspection tools, and state the design-time limitation |
| Get started again after completing the quickstart | Continue the existing project with one useful change |

An assistant can draft from the page brief, compare sources, and identify repetition. It must not fill missing evidence with plausible versions, APIs, tools, or sample behavior. A human owner remains responsible for the technical and editorial decision.

## 9. Give agents the same reliable route

A human and a coding agent need different reading affordances but the same technical contract.

For agent-facing guidance, surface the supported host environment, framework, language, project configuration, exact commands, expected artifacts, and completion evidence. Link to canonical setup and release information. Preserve meaningful headings and text alongside visuals so the route remains understandable without seeing the layout.

Do not maintain an independent "latest versions" table in an agent index unless it has an explicit update mechanism and owner. The existing `llms.txt` discrepancy is a concrete maintenance problem to address later, not permission to add another competing index.

Keep two intentions separate: **build with an AI assistant** belongs in onboarding and resource setup; **add AI features to an app** belongs in feature guidance. An assistant-assisted variant should not silently change the tutorial's framework or deployment assumptions.

## 10. Build from the brief upward

1. **Inventory and classify the current route.** Assign keep, consolidate, route elsewhere, or retire before selecting reusable material. Identify entry links, competing pages, constraints, and the canonical owner of each destination.
1. **Write the page and journey briefs.** Agree on reader state, outcome, exclusions, and completion evidence before drafting headings.
1. **Draft the minimal content.** Select the actual destinations and write their titles and descriptions without layout filler.
1. **Choose visual patterns.** Add only the cards, horizontal rows, screenshots, or diagrams that make that content easier to use.
1. **Implement an isolated specimen.** Choose Markdown or a supported template based on the brief, keeping production navigation unchanged.
1. **Review and refine with tasks.** Use actual reader and agent tasks, inspect the published preview, and change the content or pattern where it fails.

Keep design, technical, and navigation ownership explicit. Changing a menu label, replacing a landing page, and correcting a framework example are related but separate review decisions. Do not move content to another repository until its receiving owner and link/redirect plan are agreed.

## 11. Review gates and evidence of improvement

These are review criteria, not a new automated test tool or a numeric quality score.

| Gate | Evidence needed before publishing |
|---|---|
| Intent | A reader can identify who the page serves and what to do next without reading every section |
| Technical truth | Setup, examples, framework boundaries, and limitations have authoritative sources and applicable execution evidence |
| Continuity | Each journey step starts from the state produced by the previous one, or explicitly explains the change |
| Distinct choices | Card titles and descriptions differentiate real destinations; no filler or duplicate primary routes |
| Visual purpose | Every asset or component improves recognition, explanation, selection, or expected-result comparison |
| Accessibility | Logical reading order, keyboard access, visible focus, appropriate image alternatives, and usable narrow/zoomed layouts |
| Maintainability | Canonical ownership, valid destinations, and a defined response to SDK, toolchain, or sample changes |
| Agent parity | Text extraction and agent instructions retain the same scope, defaults, and completion expectations |

Incorrect instructions, misleading limitations, broken primary destinations, or inaccessible navigation block publication. A visual preference alone does not justify adding duplicate content or an unsupported layout mechanism.

Use task-based comparisons against the current experience. For a small pilot, ask representative new developers, existing-app developers, and a coding agent to complete relevant tasks. Record where they go, what they misunderstand, whether they recover, and whether they reach the result.

| Pilot task | What to observe |
|---|---|
| Find the WinUI first-run route | Wrong turns, repeated introductions, and whether the required setup is clear |
| Change behavior in the running app | Whether the same project continues and the expected interaction works |
| Find a control example or development tool | Whether Code samples and Tools are discoverable without navigating through unrelated features |
| Find an SDK release or support detail | Whether the Windows App SDK collection remains coherent |
| Assess a WPF-to-WinUI migration | Whether the destination and known gaps are clear without pushing unrelated modernization |
| Give an agent the first-app task | Whether it selects the same framework and setup contract as the human route |

Do not present page views, time-on-page, or clicks alone as proof of successful onboarding. Separate environment installation time from wayfinding time. Report small-pilot findings as observations, not statistically established population effects.

## 12. Review the integrated draft

The initial experiment used isolated pages and an in-page navigation proposal. At the user's request, the draft now presents the experience in its intended locations with the actual sidebar. The isolated pages, preview banners, mock navigation, and preview-only indexing metadata have been removed.

| Canonical page | Purpose |
|---|---|
| [Getting started with WinUI](../../hub/apps/get-started/index.yml) | The canonical WinUI entry page: three starting choices, five blue-icon learning and sample paths, and four links that continue the app journey through design, development, deployment, and publishing. |
| [Design Windows apps](../../hub/apps/design/index.yml) | The canonical design entry page: three starting choices, six design workflow paths, and four links that continue the app journey through onboarding, development, deployment, and publishing. |
| [Package and deploy Windows apps](../../hub/apps/package-and-deploy/index.yml) | The canonical packaging and deployment entry page: separate choices for packaging mode, distribution path, and Windows App SDK deployment, followed by packaging, signing, distribution, and runtime-delivery workflows. |
| [Build your first WinUI app](../../hub/apps/get-started/start-here.md) | One quickstart with winapp CLI and Visual Studio tabs, followed by a small XAML change in the same project. |
| [About WinUI](../../hub/apps/winui/winui3/index.md) | The existing UI-framework overview, separate from setup instructions. |
| [FAQ](../../hub/apps/get-started/windows-developer-faq.md) | Existing Windows app development questions and answers. |
| [Terminology](../../hub/apps/get-started/windows-developer-glossary.md) | Definitions used in the first-app and platform guidance. |
| [Tools for Windows app development](../../hub/apps/tools/index.yml) | A ProductSubHub for Visual Studio, Visual Studio Code, Windows App Development CLI, WinUI Agent, testing, and automation. |
| [Code samples](../../hub/apps/dev-tools/samples.md) | Focused examples, WinUI Gallery, and links to maintained Windows app sample repositories. |

The [Essentials ToC](../../hub/apps/toc.yml) starts with a non-clickable **Build with WinUI** group. Its first link opens the focused WinUI landing page, followed by the combined quickstart and standalone Tutorials ToC. **Understand the platform** is a separate non-clickable grouping containing About WinUI and the intact SDK collections, followed by Windows versions and compatibility. Help and guidance stays separate. Tools opens its own [ToC](../../hub/apps/tools/toc.yml), and Code samples remains a direct destination.

The standalone first-change article is removed; the quickstart includes the small edit after its stage-specific workflow tabs. On the WinUI landing page, readers can create their first app, understand WinUI, or choose a modernization path. The skill section mixes concept paths for XAML, controls and layouts, and data binding and MVVM with separate calls to action for the Notes and AI-assisted Task Tally tutorials. WinUI Gallery and Windows App SDK Samples appear in a separate final section. No new tutorial is introduced.

The Develop ToC focuses on Windows capabilities instead of duplicating the tools tree. Tutorials and tools use standalone ToCs, while Code samples remains directly accessible. Coming to Windows and the persona catalog are removed from the Essentials sidebar; their existing article URLs remain available pending a separate retirement decision.

The existing DocsRoot header is not edited in this repository. The coordinated DocsRoot change should point the onboarding entry directly to `get-started/index.yml`, which now serves WinUI onboarding. Approved redirects preserve requests to the retired `/windows/apps/introduction` and `/windows/apps/desktop/` URLs. Renaming the global L0 items and adding global Tools and Code samples entries remain part of the separate DocsRoot change.

The WinUI, Design, Develop, API reference, Package and deploy, and Tools landing pages use the ProductSubHub schema without custom stylesheets or scripts. Their single-TOC configurations preserve the standard left ToC and group starting points, task-focused workflows, and onward navigation into structured sections.

Each ProductSubHub presents focused starting points, workflow paths, and destinations that continue the Windows app journey. The schema supports custom icons but not thumbnail images. This change does not revert the quickstart, sidebar organization, or icon exporter.

The former platform and Desktop landing pages are not retained as competing overviews. Links that relied on their SDK, design, and deployment sections now point directly to the canonical landing pages and content owners.

The ProductSubHub cards use blue Fluent System Icons. Navigation and factual copy are preserved.

The reusable [icon exporter](../../tools/export-fluent-icons.ps1) produces SVG paths and transparent PNGs from pinned, MIT-licensed sources; [usage and attribution](../../tools/fluent-icons/usage.md) accompany it. This is not Segoe Fluent font extraction: explicit approval for publishing a converted font-glyph asset set was not established, so the implementation uses the separately licensed Fluent System Icons instead.

The quickstart is explicitly C#-specific and uses `winapp new` and `winapp run` as the CLI path. It states that the .NET SDK is a prerequisite and that the CLI is in public preview. It does not promise a five-minute clean-machine installation. Moving into Notes explicitly starts a separate project.

Inspection of the current CLI blank template showed that `MainWindow.xaml` hosts a frame and title bar while app content belongs in `MainPage.xaml`. The shared quickstart edit adds a `TextBlock` to the existing layout panel and preserves the window shell and named controls. This avoids replacing framework-generated elements that the code-behind still references.

### Workflow overview retirement

`get-started/winui-get-started-overview.md` no longer contains its own framework introduction, workflow comparison, prerequisites, or AI onboarding. It is a short pointer to the combined quickstart, keeping the existing published URL usable.

The intended HTTP redirect is `/windows/apps/get-started/winui-get-started-overview` to `/windows/apps/get-started/start-here`. Applying that redirect requires the publishing team's special review of `.openpublishing.redirection.json`; this pass does not modify that file. Remove the pointer page when the reviewed redirect is applied, not before.

### Draft PR previews

Draft status does not itself create a preview; the Learn publishing integration must process the changed published content. There is direct precedent in [the build-service comment on PR #7250](https://github.com/MicrosoftDocs/windows-dev-docs-pr/pull/7250#issuecomment-5412144342), which supplies `review.learn.microsoft.com` links with the PR preview branch.

Use the canonical-page review links emitted by the build service for the current commit. Do not use the removed test-page URLs or treat GitHub Markdown rendering as Learn rendering. Review the actual sidebar, page transitions, cards, images, focus, reading order, narrow widths, zoom, and theme behavior in Learn. Preview access may require sign-in.

This framework lives under `.github/design`, outside the configured publishing roots. It is a repository review document and will not itself become a Learn preview page. Keep the PR in draft for docs-team content, navigation, and design review before merge.

## Decision

Build a documentation experience around explicit reader outcomes, then express it through a small set of visual patterns. Keep the WinUI path clear, the Windows App SDK collection coherent, and every claim grounded. Use comparison sites to identify useful behavior, not to prescribe a universal card count or copy a visual style.
