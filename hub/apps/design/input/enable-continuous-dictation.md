---
description: Learn how to capture and recognize long-form, continuous dictation speech input.
title: Enable continuous dictation
ms.assetid: 383B3E23-1678-4FBB-B36E-6DE2DA9CA9DC
label: Continuous dictation
template: detail.hbs
keywords: speech, voice, speech recognition, natural language, dictation, input, user interaction
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Continuous dictation

Learn how to capture and recognize long-form, continuous dictation speech input.

> **Important APIs**: [**SpeechContinuousRecognitionSession**](/uwp/api/Windows.Media.SpeechRecognition.SpeechContinuousRecognitionSession), [**ContinuousRecognitionSession**](/uwp/api/windows.media.speechrecognition.speechrecognizer.continuousrecognitionsession)

In [Speech recognition](speech-recognition.md), you learned how to capture and recognize relatively short speech input using the [**RecognizeAsync**](/uwp/api/windows.media.speechrecognition.speechrecognizer.recognizeasync) or [**RecognizeWithUIAsync**](/uwp/api/windows.media.speechrecognition.speechrecognizer.recognizewithuiasync) methods of a [**SpeechRecognizer**](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognizer) object, for example, when composing a short message service (SMS) message or when asking a question.

For longer, continuous speech recognition sessions, such as dictation or email, use the [**ContinuousRecognitionSession**](/uwp/api/windows.media.speechrecognition.speechrecognizer.continuousrecognitionsession) property of a [**SpeechRecognizer**](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognizer) to obtain a [**SpeechContinuousRecognitionSession**](/uwp/api/Windows.Media.SpeechRecognition.SpeechContinuousRecognitionSession) object.

> [!NOTE]
> Dictation language support depends on the [device](../devices/index.md) where your app is running. For PCs and laptops, only en-US is recognized, while Xbox and phones can recognize all languages supported by speech recognition. For more info, see [Specify the speech recognizer language](specify-the-speech-recognizer-language.md).

## Set up

Your app needs a few objects to manage a continuous dictation session:

- An instance of a [**SpeechRecognizer**](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognizer) object.
- A reference to a UI dispatcher to update the UI during dictation.
- A way to track the accumulated words spoken by the user.

Here, we declare a [**SpeechRecognizer**](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognizer) instance as a private field of the code-behind class. Your app needs to store a reference elsewhere if you want continuous dictation to persist beyond a single Extensible Application Markup Language (XAML) page.

```CSharp
private SpeechRecognizer speechRecognizer;
```

During dictation, the recognizer raises events from a background thread. Because a background thread cannot directly update the UI in XAML, your app must use a dispatcher to update the UI in response to recognition events.

Here, we declare a private field that will be initialized later with the UI dispatcher.

```CSharp
// Speech events may originate from a thread other than the UI thread.
// Keep track of the UI thread dispatcher so that we can update the
// UI in a thread-safe manner.
private CoreDispatcher dispatcher;
```

To track what the user is saying, you need to handle recognition events raised by the speech recognizer. These events provide the recognition results for chunks of user utterances.

Here, we use a [**StringBuilder**](/dotnet/api/system.text.stringbuilder) object to hold all the recognition results obtained during the session. New results are appended to the **StringBuilder** as they are processed.

```CSharp
private StringBuilder dictatedTextBuilder;
```

## Initialization

During the initialization of continuous speech recognition, you must:

- Fetch the dispatcher for the UI thread if you update the UI of your app in the continuous recognition event handlers.
- Initialize the speech recognizer.
- Compile the built-in dictation grammar.
    **Note**   Speech recognition requires at least one constraint to define a recognizable vocabulary. If no constraint is specified, a predefined dictation grammar is used. See [Speech recognition](speech-recognition.md).
- Set up the event listeners for recognition events.

In this example, we initialize speech recognition in the [**OnNavigatedTo**](/uwp/api/windows.ui.xaml.controls.page.onnavigatedto) page event.

1. Because events raised by the speech recognizer occur on a background thread, create a reference to the dispatcher for updates to the UI thread. [**OnNavigatedTo**](/uwp/api/windows.ui.xaml.controls.page.onnavigatedto) is always invoked on the UI thread.
```csharp
this.dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;
```

2.  We then initialize the [**SpeechRecognizer**](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognizer) instance.
```csharp
this.speechRecognizer = new SpeechRecognizer();
```

3.  We then add and compile the grammar that defines all of the words and phrases that can be recognized by the [**SpeechRecognizer**](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognizer).

    If you don't specify a grammar explicitly, a predefined dictation grammar is used by default. Typically, the default grammar is best for general dictation.

    Here, we call [**CompileConstraintsAsync**](/uwp/api/windows.media.speechrecognition.speechrecognizer.compileconstraintsasync) immediately without adding a grammar.

    
```csharp
SpeechRecognitionCompilationResult result =
      await speechRecognizer.CompileConstraintsAsync();
```

## Handle recognition events

You can capture a single, brief utterance or phrase by calling [**RecognizeAsync**](/uwp/api/windows.media.speechrecognition.speechrecognizer.recognizeasync) or [**RecognizeWithUIAsync**](/uwp/api/windows.media.speechrecognition.speechrecognizer.recognizewithuiasync). 

However, to capture a longer, continuous recognition session, we specify event listeners to run in the background as the user speaks and define handlers to build the dictation string.

We then use the [**ContinuousRecognitionSession**](/uwp/api/windows.media.speechrecognition.speechrecognizer.continuousrecognitionsession) property of our recognizer to obtain a [**SpeechContinuousRecognitionSession**](/uwp/api/Windows.Media.SpeechRecognition.SpeechContinuousRecognitionSession) object that provides methods and events for managing a continuous recognition session.

Two events in particular are critical:

- [**ResultGenerated**](/uwp/api/windows.media.speechrecognition.speechcontinuousrecognitionsession.resultgenerated), which occurs when the recognizer has generated some results.
- [**Completed**](/uwp/api/windows.media.speechrecognition.speechcontinuousrecognitionsession.completed), which occurs when the continuous recognition session has ended.

The [**ResultGenerated**](/uwp/api/windows.media.speechrecognition.speechcontinuousrecognitionsession.resultgenerated) event is raised as the user speaks. The recognizer continuously listens to the user and periodically raises an event that passes a chunk of speech input. You must examine the speech input, using the [**Result**](/uwp/api/windows.media.speechrecognition.speechcontinuousrecognitionresultgeneratedeventargs.result) property of the event argument, and take appropriate action in the event handler, such as appending the text to a StringBuilder object.

As an instance of [**SpeechRecognitionResult**](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognitionResult), the [**Result**](/uwp/api/windows.media.speechrecognition.speechcontinuousrecognitionresultgeneratedeventargs.result) property is useful for determining whether you want to accept the speech input. A [**SpeechRecognitionResult**](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognitionResult) provides two properties for this:

- [**Status**](/uwp/api/windows.media.speechrecognition.speechrecognitionresult.status) indicates whether the recognition was successful. Recognition can fail for a variety of reasons.
- [**Confidence**](/uwp/api/windows.media.speechrecognition.speechrecognitionresult.confidence) indicates the relative confidence that the recognizer understood the correct words.

Here are the basic steps for supporting continuous recognition:  

1. Here, we register the handler for the [**ResultGenerated**](/uwp/api/windows.media.speechrecognition.speechcontinuousrecognitionsession.resultgenerated) continuous recognition event in the [**OnNavigatedTo**](/uwp/api/windows.ui.xaml.controls.page.onnavigatedto) page event.
```csharp
speechRecognizer.ContinuousRecognitionSession.ResultGenerated +=
        ContinuousRecognitionSession_ResultGenerated;
```

2.  We then check the [**Confidence**](/uwp/api/windows.media.speechrecognition.speechrecognitionresult.confidence) property. If the value of Confidence is [**Medium**](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognitionConfidence) or better, we append the text to the StringBuilder. We also update the UI as we collect input.

    **Note**  the [**ResultGenerated**](/uwp/api/windows.media.speechrecognition.speechcontinuousrecognitionsession.resultgenerated) event is raised on a background thread that cannot update the UI directly. If a handler needs to update the UI (as the \[Speech and TTS sample\] does), you must dispatch the updates to the UI thread through the [**RunAsync**](/uwp/api/windows.ui.core.coredispatcher.runasync) method of the dispatcher.
```csharp
private async void ContinuousRecognitionSession_ResultGenerated(
      SpeechContinuousRecognitionSession sender,
      SpeechContinuousRecognitionResultGeneratedEventArgs args)
      {

        if (args.Result.Confidence == SpeechRecognitionConfidence.Medium ||
          args.Result.Confidence == SpeechRecognitionConfidence.High)
          {
            dictatedTextBuilder.Append(args.Result.Text + " ");

            await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
              dictationTextBox.Text = dictatedTextBuilder.ToString();
              btnClearText.IsEnabled = true;
            });
          }
        else
        {
          await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
              dictationTextBox.Text = dictatedTextBuilder.ToString();
            });
        }
      }
```

3.  We then handle the [**Completed**](/uwp/api/windows.media.speechrecognition.speechcontinuousrecognitionsession.completed) event, which indicates the end of continuous dictation.

    The session ends when you call the [**StopAsync**](/uwp/api/windows.media.speechrecognition.speechcontinuousrecognitionsession.stopasync) or [**CancelAsync**](/uwp/api/windows.media.speechrecognition.speechcontinuousrecognitionsession.cancelasync) methods (described the next section). The session can also end when an error occurs, or when the user has stopped speaking. Check the [**Status**](/uwp/api/windows.media.speechrecognition.speechrecognitionresult.status) property of the event argument to determine why the session ended ([**SpeechRecognitionResultStatus**](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognitionResultStatus)).

    Here, we register the handler for the [**Completed**](/uwp/api/windows.media.speechrecognition.speechcontinuousrecognitionsession.completed) continuous recognition event in the [**OnNavigatedTo**](/uwp/api/windows.ui.xaml.controls.page.onnavigatedto) page event.
```csharp
speechRecognizer.ContinuousRecognitionSession.Completed +=
      ContinuousRecognitionSession_Completed;
```

4.  The event handler checks the Status property to determine whether the recognition was successful. It also handles the case where the user has stopped speaking. Often, a [**TimeoutExceeded**](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognitionResultStatus) is considered successful recognition as it means the user has finished speaking. You should handle this case in your code for a good experience.

    **Note**  the [**ResultGenerated**](/uwp/api/windows.media.speechrecognition.speechcontinuousrecognitionsession.resultgenerated) event is raised on a background thread that cannot update the UI directly. If a handler needs to update the UI (as the \[Speech and TTS sample\] does), you must dispatch the updates to the UI thread through the [**RunAsync**](/uwp/api/windows.ui.core.coredispatcher.runasync) method of the dispatcher.
```csharp
private async void ContinuousRecognitionSession_Completed(
      SpeechContinuousRecognitionSession sender,
      SpeechContinuousRecognitionCompletedEventArgs args)
      {
        if (args.Status != SpeechRecognitionResultStatus.Success)
        {
          if (args.Status == SpeechRecognitionResultStatus.TimeoutExceeded)
          {
            await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
              rootPage.NotifyUser(
                "Automatic Time Out of Dictation",
                NotifyType.StatusMessage);

              DictationButtonText.Text = " Continuous Recognition";
              dictationTextBox.Text = dictatedTextBuilder.ToString();
            });
          }
          else
          {
            await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
              rootPage.NotifyUser(
                "Continuous Recognition Completed: " + args.Status.ToString(),
                NotifyType.StatusMessage);

              DictationButtonText.Text = " Continuous Recognition";
            });
          }
        }
      }
```

## Provide ongoing recognition feedback


When people converse, they often rely on context to fully understand what is being said. Similarly, the speech recognizer often needs context to provide high-confidence recognition results. For example, by themselves, the words "weight" and "wait" are indistinguishable until more context can be gleaned from surrounding words. Until the recognizer has some confidence that a word, or words, have been recognized correctly, it will not raise the [**ResultGenerated**](/uwp/api/windows.media.speechrecognition.speechcontinuousrecognitionsession.resultgenerated) event.

This can result in a less than ideal experience for the user as they continue speaking and no results are provided until the recognizer has high enough confidence to raise the [**ResultGenerated**](/uwp/api/windows.media.speechrecognition.speechcontinuousrecognitionsession.resultgenerated) event.

Handle the [**HypothesisGenerated**](/uwp/api/windows.media.speechrecognition.speechrecognizer.hypothesisgenerated) event to improve this apparent lack of responsiveness. This event is raised whenever the recognizer generates a new set of potential matches for the word being processed. The event argument provides a [**Hypothesis**](/uwp/api/windows.media.speechrecognition.speechrecognitionhypothesisgeneratedeventargs.hypothesis) property that contains the current matches. Show these to the user as they continue speaking and reassure them that processing is still active. Once confidence is high and a recognition result has been determined, replace the interim **Hypothesis** results with the final [**Result**](/uwp/api/windows.media.speechrecognition.speechcontinuousrecognitionresultgeneratedeventargs.result) provided in the [**ResultGenerated**](/uwp/api/windows.media.speechrecognition.speechcontinuousrecognitionsession.resultgenerated) event.

Here, we append the hypothetical text and an ellipsis ("…") to the current value of the output [**TextBox**](/uwp/api/Windows.UI.Xaml.Controls.TextBox). The contents of the text box are updated as new hypotheses are generated and until the final results are obtained from the [**ResultGenerated**](/uwp/api/windows.media.speechrecognition.speechcontinuousrecognitionsession.resultgenerated) event.

```CSharp
private async void SpeechRecognizer_HypothesisGenerated(
  SpeechRecognizer sender,
  SpeechRecognitionHypothesisGeneratedEventArgs args)
  {

    string hypothesis = args.Hypothesis.Text;
    string textboxContent = dictatedTextBuilder.ToString() + " " + hypothesis + " ...";

    await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
    {
      dictationTextBox.Text = textboxContent;
      btnClearText.IsEnabled = true;
    });
  }
```

## Start and stop recognition


Before starting a recognition session, check the value of the speech recognizer [**State**](/uwp/api/windows.media.speechrecognition.speechrecognizer.state) property. The speech recognizer must be in an [**Idle**](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognizerState) state.

After checking the state of the speech recognizer, we start the session by calling the [**StartAsync**](/uwp/api/windows.media.speechrecognition.speechcontinuousrecognitionsession.startasync) method of the speech recognizer's [**ContinuousRecognitionSession**](/uwp/api/windows.media.speechrecognition.speechrecognizer.continuousrecognitionsession) property.

```CSharp
if (speechRecognizer.State == SpeechRecognizerState.Idle)
{
  await speechRecognizer.ContinuousRecognitionSession.StartAsync();
}
```

Recognition can be stopped in two ways:

-   [**StopAsync**](/uwp/api/windows.media.speechrecognition.speechcontinuousrecognitionsession.stopasync) lets any pending recognition events complete ([**ResultGenerated**](/uwp/api/windows.media.speechrecognition.speechcontinuousrecognitionsession.resultgenerated) continues to be raised until all pending recognition operations are complete).
-   [**CancelAsync**](/uwp/api/windows.media.speechrecognition.speechcontinuousrecognitionsession.cancelasync) terminates the recognition session immediately and discards any pending results.

After checking the state of the speech recognizer, we stop the session by calling the [**CancelAsync**](/uwp/api/windows.media.speechrecognition.speechcontinuousrecognitionsession.cancelasync) method of the speech recognizer's [**ContinuousRecognitionSession**](/uwp/api/windows.media.speechrecognition.speechrecognizer.continuousrecognitionsession) property.

```CSharp
if (speechRecognizer.State != SpeechRecognizerState.Idle)
{
  await speechRecognizer.ContinuousRecognitionSession.CancelAsync();
}
```

> [!NOTE]
> A [**ResultGenerated**](/uwp/api/windows.media.speechrecognition.speechcontinuousrecognitionsession.resultgenerated) event can occur after a call to [**CancelAsync**](/uwp/api/windows.media.speechrecognition.speechcontinuousrecognitionsession.cancelasync).  
> Because of multithreading, a [**ResultGenerated**](/uwp/api/windows.media.speechrecognition.speechcontinuousrecognitionsession.resultgenerated) event might still remain on the stack when [**CancelAsync**](/uwp/api/windows.media.speechrecognition.speechcontinuousrecognitionsession.cancelasync) is called. If so, the **ResultGenerated** event still fires.  
> If you set any private fields when canceling the recognition session, always confirm their values in the [**ResultGenerated**](/uwp/api/windows.media.speechrecognition.speechcontinuousrecognitionsession.resultgenerated) handler. For example, don't assume a field is initialized in your handler if you set them to null when you cancel the session.

 

## Related articles


* [Speech interactions](speech-interactions.md)

**Samples**
* [Speech recognition and speech synthesis sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/SpeechRecognitionAndSynthesis)
 

 
