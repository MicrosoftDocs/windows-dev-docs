// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

#include "pch.h"
#include "MainPage.xaml.h"
#if __has_include("MainPage.g.cpp")
#include "MainPage.g.cpp"
#endif

using namespace winrt;
using namespace Microsoft::UI::Xaml;
using namespace winrt::Microsoft::UI::Xaml::Media::Animation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace winrt::BasicNavigation::implementation
{
    MainPage::MainPage()
    {
        InitializeComponent();
    }

    int32_t MainPage::MyProperty()
    {
        throw hresult_not_implemented();
    }

    void MainPage::MyProperty(int32_t /* value */)
    {
        throw hresult_not_implemented();
    }

    //void MainPage::myButton_Click(IInspectable const&, RoutedEventArgs const&)
    //{
    //    myButton().Content(box_value(L"Clicked"));
    //}
}


void winrt::BasicNavigation::implementation::MainPage::HyperlinkButton_Click(winrt::Windows::Foundation::IInspectable const& sender, winrt::Microsoft::UI::Xaml::RoutedEventArgs const& e)
{
    //Frame().Navigate(winrt::xaml_typename<BasicNavigation::Page2>());
    
    //Frame().Navigate(winrt::xaml_typename<BasicNavigation::Page2>(), winrt::box_value(name().Text()));

    // Create the slide transition and set the transition effect to FromRight.
    SlideNavigationTransitionInfo slideEffect = SlideNavigationTransitionInfo();
    slideEffect.Effect(SlideNavigationTransitionEffect(SlideNavigationTransitionEffect::FromRight));
    Frame().Navigate(xaml_typename<BasicNavigation::Page2>(),
        winrt::box_value(name().Text()),
        slideEffect);
}
