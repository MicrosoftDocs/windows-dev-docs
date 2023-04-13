// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

#include "pch.h"
#include "Page2.xaml.h"
#if __has_include("Page2.g.cpp")
#include "Page2.g.cpp"
#endif

using namespace winrt;
using namespace Microsoft::UI::Xaml;
using namespace winrt::Microsoft::UI::Xaml::Media::Animation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace winrt::BasicNavigation::implementation
{
    Page2::Page2()
    {
        InitializeComponent();
    }

    int32_t Page2::MyProperty()
    {
        throw hresult_not_implemented();
    }

    void Page2::MyProperty(int32_t /* value */)
    {
        throw hresult_not_implemented();
    }

    //void Page2::myButton_Click(IInspectable const&, RoutedEventArgs const&)
    //{
    //    myButton().Content(box_value(L"Clicked"));
    //}
}


void winrt::BasicNavigation::implementation::Page2::HyperlinkButton_Click(winrt::Windows::Foundation::IInspectable const& sender, winrt::Microsoft::UI::Xaml::RoutedEventArgs const& e)
{
    //Frame().Navigate(winrt::xaml_typename<BasicNavigation::MainPage>());

    // Create the slide transition and set the transition effect to FromLeft.
    SlideNavigationTransitionInfo slideEffect = SlideNavigationTransitionInfo();
    slideEffect.Effect(SlideNavigationTransitionEffect(SlideNavigationTransitionEffect::FromLeft));
    Frame().Navigate(winrt::xaml_typename<BasicNavigation::MainPage>(),
        nullptr,
        slideEffect);
}
