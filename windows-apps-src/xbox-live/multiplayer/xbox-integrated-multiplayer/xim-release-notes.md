---
title: Xbox Integrated Multiplayer Release Notes
author: KevinAsgari
description: Contains release notes about Xbox Integrated Multiplayer.
ms.assetid: 38df7a49-71b9-4d86-9c49-683ffa7308d6
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, xbox integrated multiplayer
ms.localizationpriority: low
---
# Xbox Integrated Multiplayer Release Notes

Updated December 14, 2016

The following methods are not available in this release of Xbox Integrated Multiplayer (XIM):

-	`xim_authority::set_authority_reconciled_data()`
-	`xim_authority::set_authority_reconciliation_data()`
-	`xim_authority::send_data_to_players()`
-	`xim_authority::network_path_information()`
-	`xim_player::xim_local::send_data_to_authority()`

The following state changes are not provided in this release of XIM:

-	`xim_state_change_type::player_to_authority_data_received`
-	`xim_state_change_type::authority_to_player_data_received`
-	`xim_state_change_type::authority_reconciling`
-	`xim_state_change_type::authority_reconciled_local`
-	`xim_state_change_type::authority_reconciled_remote`
-	`xim_state_change_type::send_queue_alert_triggered`
