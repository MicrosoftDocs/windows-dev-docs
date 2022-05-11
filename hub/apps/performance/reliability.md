---
title: Reliability improvements and Windows Application Performance
description: This guide will demonstrate ways to improve the reliability of your Windows app(s).
ms.author: mattwoj
author: mattwojo
ms.reviewer: rajroy
ms.topic: conceptual
ms.date: 05/10/2022
#Customer intent: As a Windows application developer, I want to know how to improve the responsive of my app.
---

# Improve reliability and minimize crashes

**WORK IN PROGRESS** > NEEDS FURTHER ATTENTION (Not Yet Ready for Final Review)

This guide will demonstrate ways to improve the reliability of your Windows app(s), including:

- Identify and minimize crashes
- Measure crash frequency and time to failure
- Review guidelines for building, testing, and deploying reliable apps and services

Primary question we want this doc to answer:

- What are the top 2-3 things a developer could do to improve the reliability of their Windows desktop app (client app), with consideration to fundamentals and minimizing crashes?

Other open questions:

- How to ensure an application returns to a fully functioning state after a failure occurs?

- Are there particular aspects of the Azure [Reliability design principles](/azure/architecture/framework/resiliency/principles) that would apply to Windows desktop app development and make sense to highlight here?

- Are there aspects of the [Windows Desktop Application Program](/windows/win32/appxpkg/windows-desktop-application-program) that would make sense to highlight here? (Maybe the [Health Report](/windows/win32/appxpkg/windows-desktop-application-program#health-report)?)

## Gathering reliability metrics

You can get detailed telemetry data and analytics reports that let you see how your Windows desktop applications are performing through the [Windows Desktop Application Program](/windows/win32/appxpkg/windows-desktop-application-program).

The [Health report](/windows/win32/appxpkg/windows-desktop-application-program#health-report) provides data specifically on crashes and unresponsive events related to your Windows application.

## What makes an application reliable?

**To-do** - This is taken from the Azure [Design for reliability](/azure/architecture/framework/resiliency/design-checklist) doc and I'm not sure that these definitions and principles all apply as well for Windows desktop apps and developers...?

Reliable applications:

- maintain a pre-defined percentage of uptime (availability);

- maintain a balance between high resiliency, low latency, and cost (high availability);

- maintain the ability to recover from failures (resiliency).


A reliable workload is both resilient and available.

## Maintain availability

The first step is to identify the workload stack that needs to be deployed by defining its usage pattern such as the level of availability during day and time of year.

As many applications do not need 100% high availability, this can help to optimize costs during non-critical periods.

Once the specific workload is identified as critical, it is important to identify the underlying single point of failure component.

This recommended approach will enable you to decide on a design approach to mitigate the identified failure(s).

**To-do:** Does this sort of recommendation apply to Windows desktop apps?
Another example, the Azure guidance says that a key Reliability principle is to design your app to meet business requirements and explains that "reliability is subjective" and focuses on cost implications and trade-offs between greater reliability/availability and costs. Is this an appropriate focus for Windows desktop devs?

## Maintain balance

TBD

## Maintain resiliency

TBD

. 


Reliability documentation - Microsoft Azure Well-Architected Framework | Microsoft Docs

Reliability ensures your application can meet the commitments you make to your customers.

Architecting resiliency into your application framework ensures your workloads are available and can recover from failures at any scale.

•	Overview of the reliability pillar - Microsoft Azure Well-Architected Framework | Microsoft Docs
•	Reliability Best Practices - .NET Framework | Microsoft Docs.

Testing for reliability - Microsoft Azure Well-Architected Framework | Microsoft Docs discusses some of the best practices for coding, and code reviews and testing.
 

