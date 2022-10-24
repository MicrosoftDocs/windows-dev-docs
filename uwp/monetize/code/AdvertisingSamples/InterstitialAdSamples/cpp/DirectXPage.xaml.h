﻿//
// DirectXPage.xaml.h
// Declaration of the DirectXPage class.
//

#pragma once

#include "DirectXPage.g.h"

#include "Common\DeviceResources.h"
#include "InterstitialAdSamplesCppMain.h"

namespace InterstitialAdSamplesCpp
{
    /// <summary>
    /// A page that hosts a DirectX SwapChainPanel.
    /// </summary>
    public ref class DirectXPage sealed
    {
    public:
        DirectXPage();
        virtual ~DirectXPage();

        void SaveInternalState(Windows::Foundation::Collections::IPropertySet^ state);
        void LoadInternalState(Windows::Foundation::Collections::IPropertySet^ state);

    private:
        // XAML low-level rendering event handler.
        void OnRendering(Platform::Object^ sender, Platform::Object^ args);

        // Window event handlers.
        void OnVisibilityChanged(Windows::UI::Core::CoreWindow^ sender, Windows::UI::Core::VisibilityChangedEventArgs^ args);

        // DisplayInformation event handlers.
        void OnDpiChanged(Windows::Graphics::Display::DisplayInformation^ sender, Platform::Object^ args);
        void OnOrientationChanged(Windows::Graphics::Display::DisplayInformation^ sender, Platform::Object^ args);
        void OnDisplayContentsInvalidated(Windows::Graphics::Display::DisplayInformation^ sender, Platform::Object^ args);

        // Other event handlers.
        void AppBarButton_Click(Platform::Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ e);
        void OnCompositionScaleChanged(Windows::UI::Xaml::Controls::SwapChainPanel^ sender, Object^ args);
        void OnSwapChainPanelSizeChanged(Platform::Object^ sender, Windows::UI::Xaml::SizeChangedEventArgs^ e);

        // Track our independent input on a background worker thread.
        Windows::Foundation::IAsyncAction^ m_inputLoopWorker;
        Windows::UI::Core::CoreIndependentInputSource^ m_coreInput;

        // Independent input handling functions.
        void OnPointerPressed(Platform::Object^ sender, Windows::UI::Core::PointerEventArgs^ e);
        void OnPointerMoved(Platform::Object^ sender, Windows::UI::Core::PointerEventArgs^ e);
        void OnPointerReleased(Platform::Object^ sender, Windows::UI::Core::PointerEventArgs^ e);

        // Resources used to render the DirectX content in the XAML page background.
        std::shared_ptr<DX::DeviceResources> m_deviceResources;
        std::unique_ptr<InterstitialAdSamplesCppMain> m_main; 
        bool m_windowVisible;

        void RequestAdButton_Click(Platform::Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ e);
        void ShowAdButton_Click(Platform::Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ e);

        // <Snippet1>
        Microsoft::Advertising::WinRT::UI::InterstitialAd^ m_interstitialAd;
        void OnAdReady(Object^ sender, Object^ args);
        void OnAdCompleted(Object^ sender, Object^ args);
        void OnAdCancelled(Object^ sender, Object^ args);
        void OnAdError(Object^ sender, Microsoft::Advertising::WinRT::UI::AdErrorEventArgs^ args);
        // </Snippet1>

        // <Snippet2>
        Platform::String^ myAppId = L"d25517cb-12d4-4699-8bdc-52040c712cab";
        Platform::String^ myAdUnitId = L"test";
        // </Snippet2>
    };
}
