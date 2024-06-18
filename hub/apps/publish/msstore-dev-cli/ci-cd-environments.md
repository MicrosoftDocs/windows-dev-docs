---
description: How to setup the Microsoft Store Developer CLI (preview) in CI/CD environments.
title: Microsoft Store Developer CLI (preview) CI/CD Setup
ms.date: 12/01/2022
ms.topic: article
ms.localizationpriority: medium
---

# CI/CD Environments

The Microsoft Store Developer CLI (preview) supports running in CI/CD environments. This means that you can use the Microsoft Store Developer CLI (preview) in your CI/CD pipelines to, for example, automatically publish your applications to the Microsoft Store.

The firststep to achieve this it to install the Microsoft Store Developer CLI (preview) on your CI/CD environment. You can find instructions on how to do this [here](install.md).

After installing the Microsoft Store Developer CLI (preview), you have to configure your environment to be able to run commands. You can do this by running the `msstore reconfigure` command with the specific parameters that identify your partner center account (_TenantId_, _SellerId_, _ClientId_). You also need to provide either a _ClientSecret_ or a _Certificate_.

It is very important to hide these credentials, as they will be visible in the logs of your CI/CD pipeline. You can do this by using **secrets**. Each CI/CD pipeline system have different names for these secrets. For example, Azure DevOps call them [_Secret Variables_](/azure/devops/pipelines/process/set-secret-variables), but GitHub Action calls them [Encrypted Secrets](https://docs.github.com/actions/security-guides/encrypted-secrets). Create one **secret** for each of the parameters (_TenantId_, _SellerId_, _ClientId_, and _ClientSecret_ or a _Certificate_), and then use the `reconfigure` command to setup your environment.

For example:

## Azure DevOps

```yaml
- task: UseMSStoreCLI@0
  displayName: Setup Microsoft Store Developer CLI
- script: msstore reconfigure --tenantId $(PARTNER_CENTER_TENANT_ID) --sellerId $(PARTNER_CENTER_SELLER_ID) --clientId $(PARTNER_CENTER_CLIENT_ID) --clientSecret $(PARTNER_CENTER_CLIENT_SECRET)
  displayName: Configure Microsoft Store Developer CLI
```

## GitHub Actions

```yaml
- name: Setup Microsoft Store Developer CLI
  uses: microsoft/setup-msstore-cli@v1
- name: Configure Microsoft Store Developer CLI
  run: msstore reconfigure --tenantId ${{ secrets.PARTNER_CENTER_TENANT_ID }} --sellerId ${{ secrets.PARTNER_CENTER_SELLER_ID }} --clientId ${{ secrets.PARTNER_CENTER_CLIENT_ID }} --clientSecret ${{ secrets.PARTNER_CENTER_CLIENT_SECRET }}
```

Once this command is executed, the Microsoft Store Developer CLI (preview) will be configured to use the credentials provided. You can now use the Microsoft Store Developer CLI (preview) in your CI/CD pipeline.