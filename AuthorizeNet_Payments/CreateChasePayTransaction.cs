﻿using System;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers.Bases;

namespace AuthorizeNet_Payments
{
    public static class CreateChasePayTransaction
    {
        public static Tuple<ANetApiResponse, createTransactionController> Run(string ApiLoginID, string ApiTransactionKey, CreditCardInfo cardInfo, decimal amount, Environment env)
        {
            Console.WriteLine("Create a ChasePay Transaction Sample");

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
                //cardNumber = "4111111111111111",
                cardNumber = cardInfo.CardNumber,
                //expirationDate = "0725",
                expirationDate = cardInfo.ExpirationDateToString,
                //cardCode = "999",
                cardCode = cardInfo.CardCode,
                // Set the token specific info
                isPaymentToken = true,
                // Set this to the value of the cryptogram received from the token provide
                cryptogram = "EjRWeJASNFZ4kBI0VniQEjRWeJA=",
                //tokenRequestorName = "CHASE_PAY",
                //tokenRequestorId = "12345678901",
                //tokenRequestorEci = "07"
            };

            //standard api call to retrieve response
            var paymentType = new paymentType { Item = creditCard };

            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),
                amount = amount,
                payment = paymentType
            };

            var request = new createTransactionRequest { transactionRequest = transactionRequest };

            // instantiate the controller that will call the service
            var controller = new createTransactionController(request);
            controller.Execute();

            // get the response from the service (errors contained if any)
            var response = controller.GetApiResponse();

            // validate response
            if (response != null)
            {
                if (response.messages.resultCode == messageTypeEnum.Ok)
                {
                    if (response.transactionResponse.messages != null)
                    {
                        Console.WriteLine("Successfully created transaction with Transaction ID: " + response.transactionResponse.transId);
                        Console.WriteLine("Response Code: " + response.transactionResponse.responseCode);
                        Console.WriteLine("Message Code: " + response.transactionResponse.messages[0].code);
                        Console.WriteLine("Description: " + response.transactionResponse.messages[0].description);
                        Console.WriteLine("Hash Code: " + response.transactionResponse.transHash);
                        Console.WriteLine("Success, Auth Code : " + response.transactionResponse.authCode);
                    }
                    else
                    {
                        Console.WriteLine("Failed Transaction.");
                        if (response.transactionResponse.errors != null)
                        {
                            Console.WriteLine("Error Code: " + response.transactionResponse.errors[0].errorCode);
                            Console.WriteLine("Error message: " + response.transactionResponse.errors[0].errorText);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Failed Transaction.");
                    if (response.transactionResponse != null && response.transactionResponse.errors != null)
                    {
                        Console.WriteLine("Error Code: " + response.transactionResponse.errors[0].errorCode);
                        Console.WriteLine("Error message: " + response.transactionResponse.errors[0].errorText);
                    }
                    else
                    {
                        Console.WriteLine("Error Code: " + response.messages.message[0].code);
                        Console.WriteLine("Error message: " + response.messages.message[0].text);
                    }
                }
            }
            else
            {
                // Display the error code and message when response is null
                ANetApiResponse errorResponse = controller.GetErrorResponse();
                Console.WriteLine("Failed to get response");
                if (!string.IsNullOrEmpty(errorResponse.messages.message.ToString()))
                {
                    Console.WriteLine("Error Code: " + errorResponse.messages.message[0].code);
                    Console.WriteLine("Error message: " + errorResponse.messages.message[0].text);
                }
            }

            return new Tuple<ANetApiResponse, createTransactionController>(response, controller);
        }
    }
}
