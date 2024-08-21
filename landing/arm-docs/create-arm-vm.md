---
title: Quickstart - Create a Windows on Arm VM in the Azure portal
description: In this quickstart, you learn how to use the Azure portal to create a Windows on Arm virtual machine
ms.date: 12/20/2023
ms.topic: article
ms.service: windows
ms.subservice: arm
ms.reviewer: jcoliz
---

# Quickstart: Create a Windows on Arm virtual machine in the Azure portal (Preview)

You can create and deploy Windows 11 Arm64 VMs with with Ampere Altra Arm–based processors on Azure.
While there are many ways to create an Azure virtual machine, the easiest way to get started is using
the Azure portal. This method provides a browser-based user interface to create VMs and their associated resources.

This quickstart shows you how to use the Azure portal to deploy a virtual machine (VM) in Azure that runs Windows 11 Professional on Arm-based processors. To see your VM in action, you then connect to the VM using a Remote Desktop client.

If you don't have an Azure subscription, create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F) before you begin.

## Sign in to Azure

Sign in to the [Azure portal](https://portal.azure.com).

## Create virtual machine

1. Enter *virtual machines* in the search.
1. Under **Services**, select **Virtual machines**.
1. In the **Virtual machines** page, select **Create** and then **Azure virtual machine**. The **Create a virtual machine** page opens.
1. Under **Instance details**, enter a name for the **Virtual machine name** (for example *myVM*), **No infrastructure redundancy required** for **Availability options**, and **Standard** for **Security type** as shown here:

    :::image type="content" source="images/create-arm-vm/instance-details-crop.png" alt-text="Screenshot of the Instance details section where you provide a name for the virtual machine and select its region, image and size." lightbox="images/create-arm-vm/instance-details.png":::

1. Under **Image**, select **See all images**. The **Select an image** page opens.

    :::image type="content" source="images/create-arm-vm/instance-details-image-crop.png" alt-text="Screenshot of the Image Details size section where you select a machine image." lightbox="images/create-arm-vm/instance-details-image.png":::

1. Enter *Windows* in the **Search the Marketplace** box. Next, in the **Image type** drop-down, select **Arm64**. This will filter the images to only display results for available Windows on Arm64 VMs.

    :::image type="content" source="images/create-arm-vm/select-an-image-search.png" alt-text="Screenshot of searching for a VM image" lightbox="images/create-arm-vm/select-an-image-search.png":::

1. In the Windows 11 result card, select **Select** to display available images. Choose your preferred image from among the list.

    :::image type="content" source="images/create-arm-vm/select-an-image-select-crop.png" alt-text="Screenshot of selecting a particular VM image" lightbox="images/create-arm-vm/select-an-image-select.png":::

1. Under **Size**, select **See all sizes**. The **Select a VM size** page opens.

    :::image type="content" source="images/create-arm-vm/instance-details-size-crop.png" alt-text="Screenshot of the Image Details size section where you select a machine size." lightbox="images/create-arm-vm/instance-details-size.png":::

1. Expand one of the **VM Size** headers, then choose your desired VM size from the available options. We recommend **D2ps_v5** to get started. Press the **Select** button to return to the **Create a virtual machine** page.

    :::image type="content" source="images/create-arm-vm/select-a-vm-size.png" alt-text="Screenshot of the section where you select a VM size" lightbox="images/create-arm-vm/select-a-vm-size.png":::

1. Under **Administrator account**,  provide a username, such as *azureuser* and a password. The password must be at least 12 characters long and meet the [defined complexity requirements](/azure/virtual-machines/windows/faq#what-are-the-password-requirements-when-creating-a-vm-).

    :::image type="content" source="images/create-arm-vm/administrator-account.png" alt-text="Screenshot of the Administrator account section where you provide the administrator username and password" lightbox="images/create-arm-vm/administrator-account.png" :::

1. Under **Inbound port rules**, choose **Allow selected ports** and then select **RDP (3389)** from the drop-down.

    :::image type="content" source="images/create-arm-vm/inbound-port-rules.png" alt-text="Screenshot of the inbound port rules section where you select what ports inbound connections are allowed on" lightbox="images/create-arm-vm/inbound-port-rules.png":::

1. Review [Subscription Licenses that qualify for Multitenant Hosting Rights](/azure/virtual-machines/windows/windows-desktop-multitenant-hosting-deployment#subscription-licenses-that-qualify-for-multitenant-hosting-rights) to ensure you have the necessary Windows license. This is required in order to use Windows 11 images in Azure for any production workload. Click the checkbox under **Licensing** after confirming this.

1. Leave the remaining defaults and then select the **Review + create** button at the bottom of the page.

    :::image type="content" source="images/create-arm-vm/review-create.png" alt-text="Screenshot showing the Review + create button at the bottom of the page." lightbox="images/create-arm-vm/review-create.png":::

1. After validation runs, select the **Create** button at the bottom of the page.

    :::image type="content" source="images/create-arm-vm/validation.png" alt-text="Screenshot showing that validation has passed. Select the Create button to create the VM." lightbox="images/create-arm-vm/validation.png":::

1. After deployment is complete, select **Go to resource**.

     :::image type="content" source="images/create-arm-vm/next-steps.png" alt-text="Screenshot showing the next step of going to the resource." lightbox="images/create-arm-vm/next-steps.png":::

## Connect to virtual machine

Create a remote desktop connection to the virtual machine. These directions tell you how to connect to your VM from a Windows computer. On a Mac, you need an RDP client such as this [Remote Desktop Client](https://apps.apple.com/app/microsoft-remote-desktop/id1295203466?mt=12) from the Mac App Store.

1. On the overview page for your virtual machine, select **Connect** > **Connect**.

    :::image type="content" source="images/create-arm-vm/portal-quick-start-9.png" alt-text="Screenshot of the virtual machine overview page showing the location of the connect button." lightbox="images/create-arm-vm/portal-quick-start-9.png":::

1. In the **Native RDP** section, click **Download RDP file**.

    :::image type="content" source="images/create-arm-vm/remote-desktop.png" alt-text="Screenshot showing the remote desktop settings and the Download RDP file button." lightbox="images/create-arm-vm/remote-desktop.png":::

1. Open the downloaded RDP file and click **Connect** when prompted.

1. In the **Windows Security** window, select **More choices** and then **Use a different account**. Type the username as **localhost**\\*username*, enter the password you created for the virtual machine, and then click **OK**.

1. You may receive a certificate warning during the sign-in process. Click **Yes** or **Continue** to create the connection.

## Clean up resources

### Delete resources

When no longer needed, you can delete the resource group, virtual machine, and all related resources.

1. On the Overview page for the VM, select the **Resource group** link.
1. At the top of the page for the resource group, select **Delete resource group**.
1. A page will open warning you that you are about to delete resources. Type the name of the resource group and select **Delete** to finish deleting the resources and the resource group.

### Auto-shutdown

If the VM is still needed, Azure provides an Auto-shutdown feature for virtual machines to help manage costs and ensure you are not billed for unused resources.

1. On the **Operations** section for the VM, select the **Auto-shutdown** option.
1. A page will open where you can configure the auto-shutdown time. Select the **On** option to enable and then set a time that works for you.
1. Once you have set the time, select **Save**  at the top to enable your Auto-shutdown configuration.

> [!NOTE]
> Remember to configure the time zone correctly to match your requirements, as (UTC) Coordinated Universal Time is the default setting in the Time zone dropdown.

For more information see [Auto-shutdown](/azure/virtual-machines/auto-shutdown-vm).

## Next steps

In this quickstart, you deployed a simple virtual machine, opened a network port for remote desktop traffic, and connected using a Remote Desktop, To learn more about Azure Windows virtual machines, continue on to the detailed tutorials.

> [!div class="nextstepaction"]
> [Azure Windows virtual machine tutorials](/azure/virtual-machines/windows/tutorial-manage-vm)

The App Assure Arm Advisory Service is available to help if you get stuck. This service is in addition to our existing promise: your apps will run on Windows on Arm, and if you encounter any issues, Microsoft will help you remediate them.

> [!div class="nextstepaction"]
> [Arm Advisory Service](https://blogs.windows.com/windowsdeveloper/2023/10/16/windows-launching-arm-advisory-service-for-developers/)

## Additional resources

- [Announcing Azure Virtual Machines with Ampere Altra Arm–based processors](https://azure.microsoft.com/blog/azure-virtual-machines-with-ampere-altra-arm-based-processors-generally-available/)
- [Dpsv5 and Dpdsv5 virtual machine series](/azure/virtual-machines/dpsv5-dpdsv5-series) documentation
- [Microsoft Windows 11 Preview arm64](https://azuremarketplace.microsoft.com/marketplace/apps/microsoftwindowsdesktop.windows11preview-arm64) image details on Azure Marketplace
- [Windows 11 release information](/windows/release-health/windows11-release-information)
- [Windows Commercial Licensing overview](/windows/whats-new/windows-licensing)
