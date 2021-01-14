using SamplesApp.UITests.TestFramework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Uno.UITest.Helpers;
using Uno.UITest.Helpers.Queries;
using Uno.UITests.Helpers;

namespace SamplesApp.UITests.Windows_UI_Xaml_Controls.DatePickerTests
{
	[TestFixture]
	public partial class DatePickerTests_Tests : SampleControlUITestBase
	{
		[Test]
		[AutoRetry]
		[ActivePlatforms(Platform.iOS, Platform.Android)]
		public void DatePickerFlyout_Native_HasDataContextTest()
		{
			Run("UITests.Shared.Windows_UI_Xaml_Controls.DatePicker.DatePicker_SampleContent", skipInitialScreenshot: true);


			var theDatePicker = _app.Marked("theDatePicker");
			_app.WaitForElement(theDatePicker);
			theDatePicker.SetDependencyPropertyValue("UseNativeStyle", "True");

			var datePickerFlyout = _app.CreateQuery(q => q.WithClass("Windows_UI_Xaml_Controls_DatePickerSelector"));

			Console.WriteLine($"1: {theDatePicker.GetDependencyPropertyValue<string>("DataContext")}");

			_app.WaitForDependencyPropertyValue(theDatePicker, "DataContext", "UITests.Shared.Windows_UI_Xaml_Controls.DatePicker.Models.DatePickerViewModel");

			// Open flyout
			theDatePicker.Tap();

			_app.WaitForElement(datePickerFlyout);

			_app.WaitForDependencyPropertyValue(datePickerFlyout, "DataContext", "UITests.Shared.Windows_UI_Xaml_Controls.DatePicker.Models.DatePickerViewModel");

			_app.TapCoordinates(20, 20);
		}

		[Test]
		[AutoRetry]
		public void DatePickerFlyout_HasContentTest()
		{
			Run("UITests.Shared.Windows_UI_Xaml_Controls.DatePicker.DatePicker_SampleContent", skipInitialScreenshot: true);

			var theDatePicker = _app.Marked("theDatePicker");
			_app.WaitForElement(theDatePicker);

			var datePickerFlyout = _app.CreateQuery(q => q.WithClass("Windows_UI_Xaml_Controls_DatePickerFlyoutPresenter"));

			_app.WaitForNoElement(datePickerFlyout);

			// Open flyout
			theDatePicker.Tap();

			_app.WaitForElement(datePickerFlyout);

			//_app.TapCoordinates(10, 10);
			_app.Marked("DismissButton").Tap();

			_app.WaitForNoElement(datePickerFlyout);
		}

		[Test]
		[AutoRetry]
		[ActivePlatforms(Platform.iOS, Platform.Android)]
		public void DatePickerFlyout_Native_HasContentTest()
		{
			Run("UITests.Shared.Windows_UI_Xaml_Controls.DatePicker.DatePicker_SampleContent", skipInitialScreenshot: true);

			var theDatePicker = _app.Marked("theDatePicker");
			_app.WaitForElement(theDatePicker);
			theDatePicker.SetDependencyPropertyValue("UseNativeStyle", "True");

			var datePickerFlyout = _app.CreateQuery(q => q.WithClass("Windows_UI_Xaml_Controls_DatePickerFlyoutPresenter"));

			theDatePicker.GetDependencyPropertyValue("Date");
			theDatePicker.GetDependencyPropertyValue("Date");

			using var screenshotNotOpened = TakeScreenshot("NotOpenedPicker", ignoreInSnapshotCompare: true);

			// Open flyout
			theDatePicker.Tap();

			if (Helpers.Platform == Platform.Android)
			{
				// On Android, the presenter is not part of the visual tree
				theDatePicker.GetDependencyPropertyValue("Date");
				theDatePicker.GetDependencyPropertyValue("Date");
			}
			else
			{

				_app.WaitForElement(datePickerFlyout);

				_app.WaitForDependencyPropertyValue(datePickerFlyout, "Content",
					"Windows.UI.Xaml.Controls.DatePickerSelector");
			}

			using var screenshotOpened = TakeScreenshot("OpenedPicker");

			ImageAssert.AreNotEqual(screenshotNotOpened, screenshotOpened);

			_app.Marked("btn").Tap();

			using var screenshotClosed = TakeScreenshot("ClosedPicker", ignoreInSnapshotCompare: true);

			ImageAssert.AreEqual(screenshotNotOpened, screenshotClosed);
		}

		[Test]
		[AutoRetry]
		public void DatePicker_Flyout()
		{
			Run("UITests.Shared.Windows_UI_Xaml_Controls.DatePicker.DatePickerFlyout_Automated", skipInitialScreenshot: true);

			//DatePicker is broken: https://github.com/unoplatform/uno/issues/188
			//Using a Button with DatePickerFlyout to simulate a DatePicker
			var button = _app.Marked("TestDatePickerFlyoutButton");

			_app.WaitForElement(button);

			button.FastTap();

			TakeScreenshot("DatePicker - Flyout", ignoreInSnapshotCompare: AppInitializer.GetLocalPlatform() == Platform.Android /*Status bar appears with clock*/);

			_app.TapCoordinates(20, 20);
		}

		[Test]
		[AutoRetry]
		public void DatePicker_Header()
		{
			Run("UITests.Windows_UI_Xaml_Controls.DatePicker.DatePicker_Header", skipInitialScreenshot: true);

			var headerContentTextBlock = _app.Marked("DatePickerHeaderContent");
			_app.WaitForElement(headerContentTextBlock);

			Assert.AreEqual("This is a DatePicker Header", headerContentTextBlock.GetDependencyPropertyValue("Text").ToString());
		}

		[Test]
		[AutoRetry]
		[ActivePlatforms(Platform.iOS, Platform.Android)]
		public void DatePickerFlyout_Native_MinYearProperlySets()
		{
			Run("UITests.Shared.Windows_UI_Xaml_Controls.DatePicker.DatePicker_SampleContent", skipInitialScreenshot: true);

			var theDatePicker = _app.Marked("theDatePicker");
			_app.WaitForElement(theDatePicker);
			theDatePicker.SetDependencyPropertyValue("UseNativeStyle", "True");

			var datePickerFlyout = _app.CreateQuery(q => q.WithClass("Windows_UI_Xaml_Controls_DatePickerSelector"));

			_app.WaitFor(
				() => theDatePicker.GetDependencyPropertyValue<string>("MinYear") != null,
				timeoutMessage: "Unable to get property MinYear on theDataPicker");
			var minYear = theDatePicker.GetDependencyPropertyValue<string>("MinYear");

			// Open flyout
			theDatePicker.FastTap();

			_app.WaitFor(
				() => datePickerFlyout.GetDependencyPropertyValue<string>("MinYear") == minYear,
				timeoutMessage: "Property MinYear on the flyout is not set properly");

			_app.TapCoordinates(20, 20);
		}

		[Test]
		[AutoRetry]
		[ActivePlatforms(Platform.iOS, Platform.Android)]
		public void DatePickerFlyout_Native_MaxYearProperlySets()
		{
			Run("UITests.Shared.Windows_UI_Xaml_Controls.DatePicker.DatePicker_SampleContent", skipInitialScreenshot: true);

			var theDatePicker = _app.Marked("theDatePicker");
			_app.WaitForElement(theDatePicker);
			theDatePicker.SetDependencyPropertyValue("UseNativeStyle", "True");

			var datePickerFlyout = _app.CreateQuery(q => q.WithClass("Windows_UI_Xaml_Controls_DatePickerSelector"));

			_app.WaitFor(
				() => theDatePicker.GetDependencyPropertyValue<string>("MaxYear") != null,
				timeoutMessage: "Unable to get property MaxYear on theDataPicker");
			var maxYear = theDatePicker.GetDependencyPropertyValue<string>("MaxYear");

			// Open flyout
			theDatePicker.FastTap();

			//Assert
			_app.WaitFor(
				() => datePickerFlyout.GetDependencyPropertyValue<string>("MaxYear") == maxYear,
				timeoutMessage: "Property MaxYear on the flyout is not set properly");

			_app.TapCoordinates(20, 20);
		}

		[Test]
		[AutoRetry]
		public void DatePickerFlyout_Unloaded()
		{
			Run("UITests.Shared.Windows_UI_Xaml_Controls.DatePicker.DatePickerFlyout_Unloaded", skipInitialScreenshot: true);

			var TestDatePickerFlyoutButton = _app.Marked("TestDatePickerFlyoutButton");
			var datePickerFlyout = _app.CreateQuery(q => q.WithClass("DatePickerFlyoutPresenter"));

			_app.WaitForElement(TestDatePickerFlyoutButton);

			TestDatePickerFlyoutButton.FastTap();

			_app.WaitForElement(datePickerFlyout);

			// Load another sample to dismiss the popup
			Run("UITests.Shared.Windows_UI_Xaml_Controls.DatePicker.DatePicker_SampleContent", waitForSampleControl: false);

			_app.WaitForNoElement(datePickerFlyout);
		}

		[Test]
		[AutoRetry]
		[ActivePlatforms(Platform.iOS, Platform.Android)]
		public void DatePickerFlyout_Native_Unloaded()
		{
			Run("UITests.Shared.Windows_UI_Xaml_Controls.DatePicker.DatePickerFlyout_Unloaded", skipInitialScreenshot: true);

			var TestDatePickerFlyoutButton = _app.Marked("TestNativeDatePickerFlyoutButton");
			var datePickerFlyout = _app.CreateQuery(q => q.WithClass("Windows_UI_Xaml_Controls_DatePickerSelector"));

			_app.WaitForElement(TestDatePickerFlyoutButton);

			TestDatePickerFlyoutButton.FastTap();

			_app.WaitForElement(datePickerFlyout);

			// Load another sample to dismiss the popup
			Run("UITests.Shared.Windows_UI_Xaml_Controls.DatePicker.DatePicker_SampleContent", waitForSampleControl: false);

			_app.WaitForNoElement(datePickerFlyout);
		}

		[Test]
		[AutoRetry]
		[ActivePlatforms(Platform.iOS)] // iOS Specific selection
		public void DatePickerFlyout_Date_Binding()
		{
			Run("UITests.Shared.Windows_UI_Xaml_Controls.DatePicker.DatePickerFlyout_Date_Binding", skipInitialScreenshot: true);

			var TestDatePickerFlyoutButton = _app.Marked("TestDatePickerFlyoutButton");
			var datePickerFlyout = _app.CreateQuery(q => q.WithClass("Windows_UI_Xaml_Controls_DatePickerSelector"));

			_app.WaitForElement(TestDatePickerFlyoutButton);
			TestDatePickerFlyoutButton.FastTap();

			_app.WaitForElement(datePickerFlyout);

			_app.Query(x => x.Class("UIPickerView").Invoke("selectRow", "2020", "inComponent", 2, "animated", true));
			_app.Tap(x => x.Class("UIPickerView").Descendant().Marked("2020"));

			// Dismiss the flyout
			_app.Tap(x => x.Marked("AcceptButton"));

			var theDatePicker = _app.Marked("Result");
			_app.WaitForText(theDatePicker, "5/4/2020 +00:00");

			// Load another sample to dismiss the popup
			Run("UITests.Shared.Windows_UI_Xaml_Controls.DatePicker.DatePicker_SampleContent", waitForSampleControl: false);

			_app.WaitForNoElement(datePickerFlyout);
		}
	}
}
