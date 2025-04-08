---
description: How to run the Microsoft Store Developer CLI (preview) flights create command.
title: Microsoft Store Developer CLI (preview) - Flights - Create Command
ms.date: 07/02/2024
ms.topic: article
ms.localizationpriority: medium
---

# Flights - Create Command

Creates a flight for the specified Application and flight.

## Usage

```console
msstore flights create <productId> <friendlyName> --group-ids <group-ids>
```

## Arguments

| Argument    | Description |
|-------------|-------------|
| `productId` | The product ID. |
| `friendlyName` | The friendly name of the flight. |

## Options

| Option | Description |
|--------|-------------|
| -g, --group-ids | The group IDs to associate with the flight. |
  -r, --rank-higher-than | The flight ID to rank higher than. |

## Help

```console
msstore flights create --help
```