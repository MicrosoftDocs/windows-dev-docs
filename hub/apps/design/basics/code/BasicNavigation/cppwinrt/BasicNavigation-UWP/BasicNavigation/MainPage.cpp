#include "pch.h"
#include "MainPage.h"
#include "MainPage.g.cpp"

using namespace winrt;
using namespace Windows::UI::Xaml;
using namespace winrt::Windows::UI::Xaml::Media::Animation;

namespace winrt::BasicNavigation::implementation
{
    int32_t MainPage::MyProperty()
    {
        throw hresult_not_implemented();
    }

    void MainPage::MyProperty(int32_t /* value */)
    {
        throw hresult_not_implemented();
    }

    //void MainPage::ClickHandler(IInspectable const&, RoutedEventArgs const&)
    //{
    //    myButton().Content(box_value(L"Clicked"));
    //}


}


void winrt::BasicNavigation::implementation::MainPage::HyperlinkButton_Click(winrt::Windows::Foundation::IInspectable const& sender, winrt::Windows::UI::Xaml::RoutedEventArgs const& e)
{
    // Frame().Navigate(winrt::xaml_typename<BasicNavigation::Page2>());

    // Frame().Navigate(winrt::xaml_typename<BasicNavigation::Page2>(), winrt::box_value(name().Text()));
    
    // Create the slide transition and set the transition effect to FromRight.
    SlideNavigationTransitionInfo slideEffect = SlideNavigationTransitionInfo();
    slideEffect.Effect(SlideNavigationTransitionEffect(SlideNavigationTransitionEffect::FromRight));
    Frame().Navigate(winrt::xaml_typename<BasicNavigation::Page2>(),
        		     winrt::box_value(name().Text()),
                     slideEffect);
}
