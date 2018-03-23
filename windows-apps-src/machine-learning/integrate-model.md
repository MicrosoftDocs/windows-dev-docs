---
author: serenaz
title: Integrate a model into your app with Windows ML 
description: Integrate a model into your app by following the load, bind, and evaluate pattern.
ms.author: sezhen
ms.date: 03/22/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, winml, Windows machine learning
ms.localizationpriority: medium
---

# Integrate a model into your app with Windows ML

Windows ML's [automatic code generation](overview.md#automatic-interface-code-generation) creates wrapper classes that call the [Windows ML APIs](/uwp/api/windows.ai.machinelearning.preview) for you, providing an interface to interact with your model. Using the wrapper classes, you'll follow the load, bind, and evaluate pattern to integrate your ML model into your app. 

![load, bind, evaluate](images/load-bind-evaluate.png)

In this article, we'll use the MNIST model from [Get Started](get-started.md) to demonstrate how to load, bind, and evaluate a model in your app.

## Add the model
First, you'll need to add your ONNX model to your Visual Studio project's Assets. If you're building a UWP app with [Visual Studio (version 15.7 - Preview 1)](https://www.visualstudio.com/vs/preview/), then Visual Studio will automatically generate the wrapper classes in a new file. For other workflows, you'll need to use the [mlgen](overview.md#automatic-interface-code-generation) tool to generate wrapper classes.

Below are the Windows ML generated wrapper classes for the MNIST model. We'll use the remainder of this article to explain how to use these classes in your app.
```csharp
public sealed class MNISTModelInput
{
    public VideoFrame Input3 { get; set; }
}

public sealed class MNISTModelOutput
{
    public IList<float> Plus214_Output_0 { get; set; }
    public MNISTModelOutput()
    {
        this.Plus214_Output_0 = new List<float>();
    }
}

public sealed class MNISTModel
{
    private LearningModelPreview learningModel;
    public static async Task<MNISTModel> CreateMNISTModel(StorageFile file)
    {
        LearningModelPreview learningModel = await LearningModelPreview.LoadModelFromStorageFileAsync(file);
        MNISTModel model = new MNISTModel();
        model.learningModel = learningModel;
        return model;
    }
    public async Task<MNISTModelOutput> EvaluateAsync(MNISTModelInput input) {
        MNISTModelOutput output = new MNISTModelOutput();
        LearningModelBindingPreview binding = new LearningModelBindingPreview(learningModel);
        binding.Bind("Input3", input.Input3);
        binding.Bind("Plus214_Output_0", output.Plus214_Output_0);
        LearningModelEvaluationResultPreview evalResult = await learningModel.EvaluateAsync(binding, string.Empty);
        return output;
    }
}
```

## Load
Once you have the generated wrapper classes, you'll load the model in your app. 

The MNISTModel class represents the MNIST model, and to load the model, we call the CreateMNISTModel method, passing in the ONNX file as the parameter.

```csharp
// Load the model
StorageFile modelFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri($"ms-appx:///Assets/MNIST.onnx"));
MNISTModel model = MISTModel.CreateMNISTModel(modelFile);
```

## Bind
The generated code also includes Input and Output wrapper classes. The Input class represents the model's expected inputs, and the Output class represents the model's expected outputs.

To initialize the model's input object, call the Input class constructor, passing in your application data, and make sure that your input data matches the input type that your model expects. The MNISTModelInput class expects a VideoFrame, so we use a helper method to get a VideoFrame for the input.

```csharp
//Bind the input data to the model
MNISTModelInputs ModelInput = new MNISTModelInputs();
ModelInput.Input3 = await helper.GetHandWrittenImage(inkGrid);

```

Output objects are initialized to *Null* values and will be updated with the model's results after the next step, Evaluate. 

**Note**: Currently, the Windows ML generated code does not initialize members of Dictionary types. To bind Dictionary types, you'll need to:
- Get the number X of expected values in the Dictionary.
- Create a loop that creates X unique keys in the output dictionary and sets values to arbitrary floating values.

For example, if you have a Dictionary of type Dictionary<string, float> containing N unique keys:

```csharp
// Initialize Dictionary<string, float> with N unique keys
for (int i = 0; i < N; i++)
{
    this.MyDictionary.Add(i.ToString(), float.NaN);
}
```

## Evaluate
Once your inputs are initialized, call the model's EvaluateAsync method to evaluate your model on the input data. EvaluateAsync binds your inputs and outputs to the model object and evaluates the model on the inputs.

```csharp
// Evaluate the model
MNISTModelOuput ModelOutput = model.EvaluateAsync(ModelInput);
```

After evaluation, your output contains the model's results, which you now can view and analyze. Since the MNIST model outputs a list of probabilities, we iterate through the list to find and display the digit with the highest probability.

```csharp
 //Iterate through output to determine highest probability digit
float maxProb = 0;
int maxIndex = 0;
for (int i = 0; i < 10; i++)
{
    if (ModelOutput.Plus214_Output_0[i] > maxProb)
    {
        maxIndex = i;
        maxProb = ModelOutput.Plus214_Output_0[i];
    }
}
numberLabel.Text = maxIndex.ToString();
```

## Using the Windows ML APIs directly
Although Windows ML's automatic code generator provides a convenient interface to interact with your model, you don't have to use the wrapper classes. Instead, you can call the Windows ML APIs directly in your app.
If you choose to do so, you'll follow the same pattern: load your model, bind your inputs and outputs, and evaluate.
 
For more information on using the APIs, see the [Windows ML API reference](/uwp/api/windows.ai.machinelearning.preview).
