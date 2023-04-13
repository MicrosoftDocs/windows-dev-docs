#pragma once

#include "Page2.g.h"
#include "MainPage.h"
//using namespace winrt::Windows::UI::Xaml::Media::Animation;

namespace winrt::BasicNavigation::implementation
{
	struct Page2 : Page2T<Page2>
	{
		Page2();

		int32_t MyProperty();
		void MyProperty(int32_t value);

		//void ClickHandler(Windows::Foundation::IInspectable const& sender, Windows::UI::Xaml::RoutedEventArgs const& args);

		void HyperlinkButton_Click(winrt::Windows::Foundation::IInspectable const& sender, winrt::Windows::UI::Xaml::RoutedEventArgs const& e);

		void Page2::OnNavigatedTo(Windows::UI::Xaml::Navigation::NavigationEventArgs const& e)
		{
			auto propertyValue{ e.Parameter().as<Windows::Foundation::IPropertyValue>() };
			if (propertyValue.Type() == Windows::Foundation::PropertyType::String)
			{
				auto name{ winrt::unbox_value<winrt::hstring>(e.Parameter()) };
				if (!name.empty())
				{
					greeting().Text(L"Hello, " + name);
					__super::OnNavigatedTo(e);
					return;
				}
			}
			greeting().Text(L"Hello!");
			__super::OnNavigatedTo(e);
		}

	};
}

namespace winrt::BasicNavigation::factory_implementation
{
	struct Page2 : Page2T<Page2, implementation::Page2>
	{
	};
}
