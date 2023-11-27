---
title: Device Portal remote input API reference
description: Learn how to remotely send controller, keyboard, and mouse input on an Xbox.
ms.localizationpriority: medium
ms.topic: article
ms.date: 02/08/2017
---
# Remote Input API reference
You can send controller, keyboard, and mouse input in real time remotely via this API.

> [!NOTE]
> Keyboard KeyCodes do not work in game titles, we recommend using the Keyboard ScanCodes option instead.

**Request**

| Method | Request URI |
|--------|-------------|
| Websocket | /ext/remoteinput |

**URI parameters**

- None

**Request headers**

- None

**Request**

The websocket a series of byte array messages. For each message the format is as follows.

The first byte indicates the input type. The following input types are supported:

| Input Type | Byte Value |
|------------|------------|
| Keyboard KeyCodes | 0x01 |
| Keyboard ScanCodes | 0x02 |
| Mouse | 0x03 |
| Clear All | 0x04 |

For KeyboardKeyCodes and KeyboardScanCodes, the second byte is the value of the keycode or scancode and the third byte is 0x01 for a down press and 0x00 for a release.

For a Mouse message, the next value is a UINT16 in network order (2 bytes) indicating the type of mouse event:

| Action Type | UINT16 Value |
|-------------|--------------|
| Move | 0x0001 |
| LeftDown | 0x0002 |
| LeftUp | 0x0004 |
| RightDown | 0x0008 |
| RightUp | 0x0010 |
| MiddleDown | 0x0020 |
| MiddleUp | 0x0040 |
| X1Down | 0x0080 |
| X1Up | 0x0100 |
| X2Down | 0x0200 |
| X2Up | 0x0400 |
| VerticalWheelMoved | 0x0800 |
| HorizontalWheelMoved | 0x1000 |

This byte is followed by two UINT32 values in network order, and an optional third UINT32 for wheel actions. The first two values are the X and Y coordinate and the third is the wheel delta. The X and Y coordinates are expected to be a value between 0 and 65535 indicating the relative position of the mouse in the horizontal and vertical planes.

ClearAll indicates any currently held down keys should be released. No other bytes are expected.

For sending Gamepad input, the keycode values which represent Gamepad button presses can be used with KeyboardKeyCodes. Those values are as follows:

| Gamepad Type | Byte Value |
|--------------|------------|
| VK_GAMEPAD_A                       |  0xC3 |
| VK_GAMEPAD_B                       |  0xC4 |
| VK_GAMEPAD_X                       |  0xC5 |
| VK_GAMEPAD_Y                       |  0xC6 |
| VK_GAMEPAD_RIGHT_SHOULDER          |  0xC7 |
| VK_GAMEPAD_LEFT_SHOULDER           |  0xC8 |
| VK_GAMEPAD_LEFT_TRIGGER            |  0xC9 |
| VK_GAMEPAD_RIGHT_TRIGGER           |  0xCA |
| VK_GAMEPAD_DPAD_UP                 |  0xCB |
| VK_GAMEPAD_DPAD_DOWN               |  0xCC |
| VK_GAMEPAD_DPAD_LEFT               |  0xCD |
| VK_GAMEPAD_DPAD_RIGHT              |  0xCE |
| VK_GAMEPAD_MENU                    |  0xCF |
| VK_GAMEPAD_VIEW                    |  0xD0 |
| VK_GAMEPAD_LEFT_THUMBSTICK_BUTTON  |  0xD1 |
| VK_GAMEPAD_RIGHT_THUMBSTICK_BUTTON |  0xD2 |
| VK_GAMEPAD_LEFT_THUMBSTICK_UP      |  0xD3 |
| VK_GAMEPAD_LEFT_THUMBSTICK_DOWN    |  0xD4 |
| VK_GAMEPAD_LEFT_THUMBSTICK_RIGHT   |  0xD5 |
| VK_GAMEPAD_LEFT_THUMBSTICK_LEFT    |  0xD6 |
| VK_GAMEPAD_RIGHT_THUMBSTICK_UP     |  0xD7 |
| VK_GAMEPAD_RIGHT_THUMBSTICK_DOWN   |  0xD8 |
| VK_GAMEPAD_RIGHT_THUMBSTICK_RIGHT  |  0xD9 |
| VK_GAMEPAD_RIGHT_THUMBSTICK_LEFT   |  0xDA |

**Response**   

- None

**Status code**

This API has the following expected status codes.

HTTP status code | Description |
|----------------|-------------|
| 200 | Request was successful |
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Xbox
