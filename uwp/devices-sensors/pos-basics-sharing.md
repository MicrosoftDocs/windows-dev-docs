---
title: PointOfService device sharing 
description: Learn how to share network or Bluetooth connected peripherals with other computers in an environment where multiple PCs rely on shared peripherals.
ms.date: 05/04/2023
ms.topic: article

ms.localizationpriority: medium
---

# PointOfService device sharing

This topic shows how to share network or Bluetooth connected peripherals with other computers in an environment where multiple PCs rely on shared peripherals rather than dedicated peripherals attached to each computer.

**Important APIs**

- [BarcodeScanner.Dispose](/uwp/api/windows.devices.pointofservice.barcodescanner.dispose)
- [CashDrawer.Dispose](/uwp/api/windows.devices.pointofservice.cashdrawer.dispose)
- [LineDisplay.Dispose](/uwp/api/windows.devices.pointofservice.linedisplay.dispose)
- [MagneticStripeReader.Dispose](/uwp/api/windows.devices.pointofservice.magneticstripereader.dispose)  
- [PosPrinter.Dispose](/uwp/api/windows.devices.pointofservice.posprinter.dispose)


## Device sharing

Network and Bluetooth connected PointOfService peripherals are typically used in an environment where multiple client devices are sharing the same peripherals throughout the day.  In a busy retail or food services environment any delay in the ability for a client device to attach to a peripheral has an impact on the efficiency in which an associate can close a transaction with the customer and move on to the next. In a quick service restaurant scenario where a receipt printer is used as a kitchen printer to transfer the details of a customer's order to the kitchen for preparation there will be multiple client devices taking orders from customers.  Once the order is complete each client device should be able to claim the shared printer and immediately print the order for the kitchen.

In these environments, it is important for the application to fully **dispose** the device object so that another can claim the same device.

Disposing of a PosPrinter at the end of a ‘using’ block

```Csharp 
using Windows.Devices.PointOfService;
using(PosPrinter printer = await PosPrinter.FromIdAsync("Device ID"))
{
    if (printer != null)
    {
        // Exercise the printer.
    }

    // When leaving this scope, printer.Dispose() is automatically invoked, 
    // releasing the session we have with the printer.
}
```

Disposing of a PosPrinter by calling Dispose() explicitly

```csharp
using Windows.Devices.PointOfService;

PosPrinter printer = await PosPrinter.FromIdAsync("Device ID");
if (printer != null)
{
    // Exercise the printer, then dispose of the printer explicitly.
    printer.Dispose();
}
```

[!INCLUDE [feedback](./includes/pos-feedback.md)]
