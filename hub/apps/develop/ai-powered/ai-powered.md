---
title: AI-powered features
description: This article lists the Windows features that are powered using AI and links to the how-to articles that show how to use them.
ms.topic: article
ms.date: 07/05/2026
ms.author: jken
author: GrantMeStrength
---


# AI-powered features

This section provides guidance for using the Windows App SDK to create apps that leverage AI-powered features in Windows.

## AI-powered Windows features

| Topic | Description |
|---------------------------|-----------------------------|
| [Click to Do](/windows/apps/develop/windows-integration/click-to-do) | Learn how to use the Click to Do feature to take action based on content seen in apps. |
| [Copilot hardware key](/windows/apps/develop/windows-integration/microsoft-copilot-key-provider) | Learn how to use the Copilot hardware key feature to quickly access Copilot. |
| [Recall](/windows/apps/develop/windows-integration/recall/index) | Learn how to use the Recall feature to help find content. |
| [Studio Effects](/windows/apps/develop/windows-integration/studio-effects) | Learn how to use Studio Effects to enhance video calls with AI-powered background effects. |

## Other AI features

| Topic | Description |
|---------------------------|-----------------------------|
| [App Content Search](/windows/ai/apis/app-content-search) | Learn how to use App Content Search to enable AI-powered search capabilities. |
| [Phi Silica](/windows/ai/apis/phi-silica) | Learn how to use Phi Silica to enhance app performance with AI acceleration. |
| [Imaging SDK](/windows/ai/apis/imaging) | Learn how to use the Imaging SDK to add AI-powered image processing capabilities to your apps. |
| [Image generation API](/windows/ai/apis/image-generation) | Learn how to use the Image Generation API to create images from text prompts. |
| [Text Recognition API](/windows/ai/apis/text-recognition) | Learn how to use the Text Recognition API to extract text from images. |
| [Video super resolution](/windows/ai/apis/video-super-resolution) | Learn how to use AI to enhance video playback quality. |

## On-device model deployment

For running ML models directly on a user's device (without cloud calls), Windows supports several approaches depending on your scenario:

| Approach | Best for | Key link |
|----------|----------|----------|
| **ONNX Runtime** | General-purpose inference (vision, NLP, classification) with hardware acceleration via DirectML | [ONNX Runtime](https://onnxruntime.ai/) |
| **ONNX Runtime GenAI** | Running generative AI models (SLMs like Phi) locally with optimized text generation | [ONNX Runtime GenAI](https://github.com/microsoft/onnxruntime-genai) |
| **Windows ML** | WinRT-based inference using the built-in Windows ML runtime (DirectML backend) | [Windows ML](/windows/ai/new-windows-ml/overview) |
| **DirectML** | Low-level GPU-accelerated ML operators for custom inference engines | [DirectML](/windows/ai/directml/dml) |

> [!NOTE]
> ONNX Runtime with the DirectML execution provider is the recommended approach for most desktop apps. It supports the widest range of models from the [ONNX Model Zoo](https://github.com/onnx/models) and [Hugging Face](https://huggingface.co/models?library=onnx), and runs on any DirectX 12 GPU. For generative AI scenarios (chatbots, text completion, summarization), use ONNX Runtime GenAI, which adds optimized decoding loops on top of ONNX Runtime.

## Related topics

* [AI-assisted Windows development](../ai-assisted/index.md)
* [Choose your Windows AI solution](/windows/ai/windows-ai-comparison)
* [Windows AI](/windows/ai)
* [Microsoft Foundry on Windows](/windows/ai/overview)
* [Windows ML](/windows/ai/new-windows-ml/overview)
* [ONNX Runtime](https://onnxruntime.ai/)
* [Windows Machine Learning samples](https://github.com/microsoft/Windows-Machine-Learning)
* [MCP on Windows](/windows/ai/mcp/overview)
* [Responsible AI](/windows/ai/rai)
