---
author: serenaz
title: Windows Machine Learning overview
description: Learn about Windows Machine Learning (WinML) and how to develop with WinML.
ms.author: sezhen
ms.date: 03/07/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, windows machine learning, winml
ms.localizationpriority: medium
---

# Windows Machine Learning overview

## What is machine learning?
Machine learning (ML) allows computers to use existing data to predict expected outcomes and behaviors. By processing previously collected data, ML algorithms build models that can predict the correct output when presented with a new input. For example, a model can be trained to evaluate email messages (input) as spam or not spam (output).

The model-building phase is called "training." Once trained with existing data, the model can perform predictions with new, previously unseen data, which is called "inferencing," "evaluation," or "scoring." For a list of key ML terms and concepts, see [What is Machine Learning?](https://docs.microsoft.com/azure/machine-learning/studio/what-is-machine-learning#key-machine-learning-terms-and-concepts).
	 
Trained ML models often produce better results than programs written to follow a strict set of instructions, especially for complex tasks with many possible combinations of inputs and outputs. For example, recommendation algorithms provide personalized recommendations for millions of users on e-commerce and media streaming sites, which would be nearly impossible without ML. Computer vision is another field that leverages ML, allowing computers to classify and identify images after training on previously labelled images.

The possibilities and applications of ML are endless; for more information about research and solutions, visit [Artifical Intelligence at Microsoft](https://www.microsoft.com/ai) and [Microsoft Machine Learning Technologies](https://docs.microsoft.com/en-us/azure/machine-learning/#More-Microsoft-Machine-Learning-Technologies). If you'd like to build Machine Learning and AI models, you can also check out [Azure Machine Learning Services](https://docs.microsoft.com/en-us/azure/machine-learning/preview/overview-what-is-azure-ml).

## What is WinML?
WinML is a platform for local evaluation of trained machine learning models on Windows 10 devices, allowing developers to use pre-trained models within their applications. 

Here are some highlights of WinML:

### Hardware acceleration
On DirectX12 capable devices, WinML accelerates the evaluation of Deep Learning models using the GPU. CPU optimizations additionally enable high-performance evaluation of both classical ML and Deep Learning algorithms.

### Local evaluation
WinML evaluates on local hardware, removing concerns of connectivity, bandwith, and data privacy. Local evaluation also enables low latency and high performance for quick evaluation results.

### Image processing
For computer vision scenarios, WinML simplifies and optimizes the use of image, video, and camera data by:
- Handling image pre-processing for format and dimension conversions 
- Managing whether the CPU or GPU processes the image, based on hardware capabilities of the host
- Completing media pipeline setup for instantaneous input from device camera data

## How to develop with WinML

### System requirements
To build applications that use WinML, you'll need the [Windows SDK - Build 17110](https://www.microsoft.com/software-download/windowsinsiderpreviewSDK).

### ONNX models
To use WinML, you'll need a pre-trained machine learning (ML) model in the [Open Neural Network Exchange (ONNX)](https://onnx.ai) format. WinML supports the v1.0 release of the ONNX format, which allows developers to use models produced by different training frameworks. There are converter tools for many frameworks and libraries, and ONNX models are already natively supported in many training frameworks. For a list of publicly available ONNX models, see [ONNX Models](https://github.com/onnx/models) on GitHub. 

You can also train your own ONNX models to use with WinML. To learn how to train a model with Visual Studio Tools for AI, see [Train a model](train-ai-model.md). 

### Convert existing models to ONNX
If you already have a pre-trained machine learning (ML) model from another framework, then you can use WinMLTools to convert it to the ONNX format accepted by WinML. 

WinMLTools supports conversion from these formats:
- CoreML
- Scikit-Learn
- Keras
- XGBoost
- LibSVM

To learn how to install and use WinMLTools, please see [conversion samples](conversion-samples.md). 

### ONNX operators
WinML supports 100+ ONNX operators on the CPU and accelerates computation on DirectX12 comptaible GPUs. For a full list of operator signatures, see the ONNX operators schemas documentation for the [ai.onnx](https://github.com/onnx/onnx/blob/rel-1.0/docs/Operators.md) (default) and [ai.onnx.ml](https://github.com/onnx/onnx/blob/rel-1.0/docs/Operators-ml.md) namespaces.

WinML supports all of the operators defined in the ONNX v1.0 documentation with the following differences:
- Operators marked "experimental" supported by WinML:
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

With an ONNX model file, WinML's code generator can generate wrapper classes that call the [WinML API](/uwp/api/windows.ai.machinelearning.preview) for you, providing an interface to interact with the model in your app. The generated classes represent the model, inputs, and outputs, allowing you to easily load, bind, and evaluate the model.

For UWP developers, WinML's automatic code generator is natively integrated with [Visual Studio (version 15.7 - Preview 1)](https://www.visualstudio.com/vs/preview/). Simply add your ONNX file as an existing item to your project, and Visual Studio will automatically add wrapper classes for your model as a new file in your project. The code generation is currently supported in both C# and C++/CX.  

For workloads other than UWP, you can install the [Visual Studio Tools for AI](https://www.visualstudio.com/downloads/ai-tools-vs/) extension for a similar experience. 

Finally, the code generator is also available as a command line tool `mlgen.exe` as part of the Windows SDK. The tool is located in `(SDK_root)\bin\<version>\x64` or `(SDK_root)\bin\<version>\x86`, where SDK_root is the SDK installation directory. To run the tool, use the command below.

```
mlgen -i INPUT-FILE -l LANGUAGE -n NAMESPACE [-o OUTPUT-FILE]
```
Input parameters definition:
- `INPUT-FILE`: the ONNX model file
- `LANGUAGE`: CPPCX or CS
- `NAMESPACE`: the namespace of the generated code
- `OUTPUT-FILE`: file path where the generated code will be written to. If OUTPUT-FILE is not specified, the generated code is written to the standard output

## Next steps
Try creating your first WinML app with a step-by-step tutorial in [Get Started](get-started.md).