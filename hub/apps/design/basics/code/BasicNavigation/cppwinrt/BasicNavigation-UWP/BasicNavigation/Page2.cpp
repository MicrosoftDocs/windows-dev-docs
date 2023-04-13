#include "pch.h"
#include "Page2.h"
#if __has_include("Page2.g.cpp")
#include "Page2.g.cpp"
#endif

using namespace winrt;
using namespace Windows::UI::Xaml;
using namespace winrt::Windows::UI::Xaml::Media::Animation;

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

	//void Page2::ClickHandler(IInspectable const&, RoutedEventArgs const&)
	//{
	//    Button().Content(box_value(L"Clicked"));
	//}
}

void winrt::BasicNavigation::implementation::Page2::HyperlinkButton_Click(winrt::Windows::Foundation::IInspectable const& sender, winrt::Windows::UI::Xaml::RoutedEventArgs const& e)
{
	// Frame().Navigate(winrt::xaml_typename<BasicNavigation::MainPage>());

	// Create the slide transition and set the transition effect to FromLeft.
	SlideNavigationTransitionInfo slideEffect = SlideNavigationTransitionInfo();
	slideEffect.Effect(SlideNavigationTransitionEffect(SlideNavigationTransitionEffect::FromLeft));
	Frame().Navigate(winrt::xaml_typename<BasicNavigation::MainPage>(),
		nullptr,
		slideEffect);
}
