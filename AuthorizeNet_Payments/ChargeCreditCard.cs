using System;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers.Bases;

namespace AuthorizeNet_Payments
{
    public static class ChargeCreditCard
    {
        public static void Run(string ApiLoginID, string ApiTransactionKey, CreditCardInfo cardInfo, Environment env)
        {
            Console.WriteLine("Charge Credit Card Sample");

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = env == Environment.SANDBOX ? AuthorizeNet.Environment.SANDBOX : AuthorizeNet.Environment.PRODUCTION;

            // define the merchant information (authentication / transaction id)
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey,
            };

            var creditCard = new creditCardType
            {
                cardNumber = cardInfo.CardNumber,
                expirationDate = cardInfo.ExpirationDateToString
            };

            //standard api call to retrieve response
            var paymentType = new paymentType { Item = creditCard };

            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),   // charge the card
                amount = 133.45m,
                payment = paymentType
            };

            var request = new createTransactionRequest { transactionRequest = transactionRequest };

            // instantiate the contoller that will call the service
            var controller = new createTransactionController(request);
            controller.Execute();

            // get the response from the service (errors contained if any)
            var response = controller.GetApiResponse();

            if (response.messages.resultCode == messageTypeEnum.Ok)
            {
                if (response.transactionResponse != null)
                {
                    Console.WriteLine("Success, Auth Code : " + response.transactionResponse.authCode);
                }
            }
            else
            {
                Console.WriteLine("Error: " + response.messages.message[0].code + "  " + response.messages.message[0].text);
                if (response.transactionResponse != null)
                {
                    Console.WriteLine("Transaction Error : " + response.transactionResponse.errors[0].errorCode + " " + response.transactionResponse.errors[0].errorText);
                }
            }

        }
    }
}
