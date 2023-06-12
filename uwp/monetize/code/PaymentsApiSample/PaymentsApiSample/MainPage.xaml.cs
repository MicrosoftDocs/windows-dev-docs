using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// <SnippetUsing>
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Windows.ApplicationModel.Payments;
using Windows.UI.Popups;
// </SnippetUsing>

namespace PaymentsApiSample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            // <SnippetEnumerate>
            // Enumerate all the payment methods out store accepts.
            string data =
                "{" +
                "\"merchantId\":\"merchant-id-from-seller-portal\"," +
                "\"supportedNetworks\":[\"visa\",\"mastercard\"]," +
                "\"supportedTypes\":[\"credit\"]" +
                "}";

            var acceptedPaymentMethods = new[]
            {
                new PaymentMethodData(new[] { "https://pay.microsoft.com/microsoftpay" }, data)
            };

            // Determine which of our accepted payment methods are supported by the customer's payment applications.
            var paymentMediator = new PaymentMediator();
            var supportedPaymentMethods = await paymentMediator.GetSupportedMethodIdsAsync();
            // </SnippetEnumerate>

            // <SnippetDisplayItems>
            // Describe the individual items in this purchase.
            var displayItemsInCart = new List<PaymentItem>
            {
                new PaymentItem("Contoso shoes", new PaymentCurrencyAmount("88.00", "USD")),
                new PaymentItem("Contoso shoelaces", new PaymentCurrencyAmount("0.56", "USD")),
            };

            // Calculate the total value of display items.
            var totalValue = displayItemsInCart.Sum(item => Convert.ToDecimal(item.Amount.Value));
            // </SnippetDisplayItems>

            // <SnippetTaxes>
            // Add items for any/all applicable taxes.
            decimal salesTax = 0.095241M * totalValue;
            var displayItemsTaxes = new[]
            {
                new PaymentItem("Taxes",
                new PaymentCurrencyAmount(salesTax.ToString(CultureInfo.InvariantCulture), "USD"))
            };

            // Describe the complete list of display items, and total them up.
            var displayItems = displayItemsInCart.Concat(displayItemsTaxes).ToList();
            totalValue = displayItems.Sum(item => Convert.ToDecimal(item.Amount.Value));
            var totalItem = new PaymentItem("Total",
                new PaymentCurrencyAmount(totalValue.ToString(CultureInfo.InvariantCulture), "USD"));
            // </SnippetTaxes>

            // <SnippetDiscountRate>
            // Create an item to apply a 5% discount if the customer pays with a Contoso credit card.
            var contosoDiscountValue = Convert.ToDecimal(totalItem.Amount.Value) * -0.05M;
            var displayItemsForContosoCard = new[]
            {
                new PaymentItem("Contoso Card Discount (5%)", 
                new PaymentCurrencyAmount($"{contosoDiscountValue}", "USD"))
            };

            displayItems.Concat(displayItemsForContosoCard);

            // Re-calculate the total value with discount
            totalValue = displayItemsInCart.Sum(item => Convert.ToDecimal(item.Amount.Value));
            var totalItemForContosoCard = new PaymentItem("Total", 
                new PaymentCurrencyAmount(totalValue.ToString(CultureInfo.InvariantCulture), "USD"));
            // </SnippetDiscountRate>

            // <SnippetAggregate>
            // Aggregate all of the prepared payment details.
            var details = new PaymentDetails()
            {
                DisplayItems = displayItems,
                Total = totalItem,
                ShippingOptions = new List<PaymentShippingOption>
                {
                    new PaymentShippingOption("Standard (3-5 business days)", new PaymentCurrencyAmount("0.00", "USD")),
                    new PaymentShippingOption( "Express (2-3 buiness days)", new PaymentCurrencyAmount("4.99", "USD")),
                    new PaymentShippingOption("Overnight (1 business day)", new PaymentCurrencyAmount("11.99", "USD")),
                },
                Modifiers = new[]
                {
                    new PaymentDetailsModifier(new[] { "contoso/credit" }, 
                    totalItemForContosoCard, displayItemsForContosoCard)
                },
            };
            // </SnippetAggregate>

            // <SnippetPaymentOptions>
            // Describe options for any additional information needed to process the transaction.
            var options = new PaymentOptions()
            {
                RequestPayerName = PaymentOptionPresence.None,
                RequestPayerEmail = PaymentOptionPresence.None,
                RequestPayerPhoneNumber = PaymentOptionPresence.None,
                RequestShipping = true,
                ShippingType = PaymentShippingType.Shipping
            };
            // </SnippetPaymentOptions>

            // <SnippetSubmit>
            // Create a structure describing our merchant application.
            var merchantInfo = new PaymentMerchantInfo(new Uri("https://store.contoso.com"));

            // Create a new payment request and associated internal state describing this proposed transaction.
            var request = new PaymentRequest(details, acceptedPaymentMethods, merchantInfo, options);

            // Submit the payment request for mediation and (possibly) user review and wait for the user to approve
            // or reject the request.
            var submissionResult = await paymentMediator.SubmitPaymentRequestAsync(request, (s, ev) =>
            {
                ev.Acknowledge(new PaymentRequestChangedResult(true));
            });

            if (submissionResult.Status != PaymentRequestStatus.Succeeded)
            {
                await new MessageDialog($"Payment request rejected by customer {submissionResult.Status}.").ShowAsync();
                return;
            }
            // </SnippetSubmit>

            // <SnippetComplete>
            await submissionResult.Response.CompleteAsync(PaymentRequestCompletionStatus.Succeeded);
            // </SnippetComplete>
        }
    }
}
