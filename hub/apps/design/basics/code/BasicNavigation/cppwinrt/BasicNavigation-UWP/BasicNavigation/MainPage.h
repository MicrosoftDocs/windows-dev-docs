#pragma once

#include "MainPage.g.h"
#include "Page2.h"
//using namespace winrt::Windows::UI::Xaml::Media::Animation;

namespace winrt::BasicNavigation::implementation
{
	struct MainPage : MainPageT<MainPage>
	{
		MainPage()
		{
			// Xaml objects should not call InitializeComponent during construction.
			// See https://github.com/microsoft/cppwinrt/tree/master/nuget#initializecomponent
		}

		int32_t MyProperty();
		void MyProperty(int32_t value);

		//void ClickHandler(Windows::Foundation::IInspectable const& sender, Windows::UI::Xaml::RoutedEventArgs const& args);

		void HyperlinkButton_Click(winrt::Windows::Foundation::IInspectable const& sender, winrt::Windows::UI::Xaml::RoutedEventArgs const& e);
	};
}

namespace winrt::BasicNavigation::factory_implementation
{
	struct MainPage : MainPageT<MainPage, implementation::MainPage>
	{
	};
}
