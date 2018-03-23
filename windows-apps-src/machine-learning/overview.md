---
author: serenaz
title: Windows ML overview
description: Learn about Windows Machine Learning and how to develop with Windows ML.
ms.author: sezhen
ms.date: 03/07/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, windows machine learning
ms.localizationpriority: medium
---

# Windows ML overview

## What is machine learning?
Machine learning (ML) allows computers to use existing data to predict expected outcomes and behaviors. By processing previously collected data, ML algorithms build models that can predict the correct output when presented with a new input. For example, a model can be trained to evaluate email messages (input) as spam or not spam (output).

The model-building phase is called "training." Once trained with existing data, the model can perform predictions with new, previously unseen data, which is called "inferencing," "evaluation," or "scoring." For a list of key ML terms and concepts, see [What is Machine Learning?](https://docs.microsoft.com/azure/machine-learning/studio/what-is-machine-learning#key-machine-learning-terms-and-concepts).
	 
Trained ML models often produce better results than programs written to follow a strict set of instructions, especially for complex tasks with many possible combinations of inputs and outputs. For example, recommendation algorithms provide personalized recommendations for millions of users on e-commerce and media streaming sites, which would be nearly impossible without ML. Computer vision is another field that leverages ML, allowing computers to classify and identify images after training on previously labelled images.

The possibilities and applications of ML are endless; for more information about research and solutions, visit [Artifical Intelligence at Microsoft](https://www.microsoft.com/ai) and [Microsoft Machine Learning Technologies](https://docs.microsoft.com/en-us/azure/machine-learning/#More-Microsoft-Machine-Learning-Technologies). If you'd like to build Machine Learning and AI models, you can also check out [Azure Machine Learning Services](https://docs.microsoft.com/en-us/azure/machine-learning/preview/overview-what-is-azure-ml).

## What is Windows ML?
Windows ML is a platform for local evaluation of trained machine learning models on Windows 10 devices, allowing developers to use pre-trained models within their applications. 

Here are some highlights of Windows ML:

### Hardware acceleration
On DirectX12 capable devices, Windows ML accelerates the evaluation of Deep Learning models using the GPU. CPU optimizations additionally enable high-performance evaluation of both classical ML and Deep Learning algorithms.

### Local evaluation
Windows ML evaluates on local hardware, removing concerns of connectivity, bandwith, and data privacy. Local evaluation also enables low latency and high performance for quick evaluation results.

### Image processing
For computer vision scenarios, Windows ML simplifies and optimizes the use of image, video, and camera data by handling frame pre-processing and providing camera pipeline setup for model input.

## How to develop with Windows ML

> [!VIDEO https://www.youtube.com/embed/8MCDSlm326U]

### System requirements
To build applications that use Windows ML, you'll need the [Windows SDK - Build 17110+](https://www.microsoft.com/en-us/software-download/windowsinsiderpreviewSDK).

### ONNX models
To use Windows ML, you'll need a pre-trained machine learning model in the [Open Neural Network Exchange (ONNX)](https://onnx.ai) format. Windows ML supports the v1.0 release of the ONNX format, which allows developers to use models produced by different training frameworks. 

To learn how to train an ONNX model with Visual Studio Tools for AI, see [Train a model](train-ai-model.md). 

For a list of publicly available ONNX models, see [ONNX Models](https://github.com/onnx/models) on GitHub. 

### Convert existing models to ONNX
ONNX models are already natively supported in many training frameworks, and there are converter tools for many frameworks and libraries. To learn how to export from frameworks such as Caffe 2, PyTorch, CNTK, Chainer, and more, see [ONNX tutorials](https://github.com/onnx/tutorials) on GitHub.

If you already have a pre-trained machine learning model from another framework, then you can use [WinMLTools](https://aka.ms/winmltools) to convert it to the ONNX format accepted by Windows ML. 

WinMLTools supports conversion from these formats:
- Core ML
- Scikit-Learn
- XGBoost
- LibSVM

To learn how to install and use WinMLTools, please see [Convert a model](conversion-samples.md). 

### ONNX operators
Windows ML supports 100+ ONNX operators on the CPU and accelerates computation on DirectX12 compatible GPUs. For a full list of operator signatures, see the ONNX operators schemas documentation for the [ai.onnx](https://github.com/onnx/onnx/blob/rel-1.0/docs/Operators.md) (default) and [ai.onnx.ml](https://github.com/onnx/onnx/blob/rel-1.0/docs/Operators-ml.md) namespaces.

Windows ML supports all of the operators defined in the ONNX v1.0 documentation with the following differences:
- Operators marked "experimental" supported by Windows ML:
	- Affine
	- Crop
	- FC
	- Identity
	- ImageScaler
	- MeanVarianceNormalization
	- ParametricSoftplus
	- ScaledTanh
	- ThresholdedRelu
	- Upsample
- MatMul - greater than 2D matrix multiplication is not currently supported, supported on CPU only
- Cast - supported on CPU only
- The following operators are not supported at this time:
	- RandomUniform
	- RandomUniformLike
	- RandomNormal
	- RandomNormalLike

### Automatic interface code generation

With an ONNX model file, Windows ML's code generator can generate wrapper classes that call the [Windows ML API](/uwp/api/windows.ai.machinelearning.preview) for you, providing an interface to interact with the model in your app. The generated classes represent the model, inputs, and outputs, allowing you to easily load, bind, and evaluate the model in your project. 

The code generator currently supports both C# and C++/CX. For UWP developers, Windows ML's automatic code generator is natively integrated with [Visual Studio (version 15.7 - Preview 1)](https://www.visualstudio.com/vs/preview/). (Note that you'll need to check off the optional Windows 10 Insider Preview SDK, Build 17110, inside the Visual Studio Installer.) Simply add your ONNX file as an exiting item to your project, and VS will generate Windows ML wrapper classes in a new interface file.

You can also use the command line tool `mlgen.exe`, which comes with the Windows SDK, to generate Windows ML wrapper classes. The tool is located in `(SDK_root)\bin\<version>\x64` or `(SDK_root)\bin\<version>\x86`, where SDK_root is the SDK installation directory. To run the tool, use the command below.

```
mlgen -i INPUT-FILE -l LANGUAGE -n NAMESPACE [-o OUTPUT-FILE]
```
Input parameters definition:
- `INPUT-FILE`: the ONNX model file
- `LANGUAGE`: CPPCX or CS
- `NAMESPACE`: the namespace of the generated code
- `OUTPUT-FILE`: file path where the generated code will be written to. If OUTPUT-FILE is not specified, the generated code is written to the standard output

To learn how to use the generated code in your app, see [Integrate a model into your app with Windows ML](integrate-model.md).

## Next steps
Try creating your first Windows ML app with a step-by-step tutorial in [Get Started](get-started.md).
