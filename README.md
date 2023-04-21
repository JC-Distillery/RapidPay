# RapidPay
Note: 
  1. This project use InMemoryDB.
  2. You can modify the time value in charge to generate a new feed in the class: \RapidPay\BussinesLogic\FeedValue.cs
    a. By default the time are 59 minutes.
  3. all endPoints use a ComomonResponse class in order to notify if something went wrong or was success. Below  yuo can see an CommonReponse sample
  
      {
        "response": true or false,
        "httpStatusCode": 200 for success cases & 400 when somethin fail,
        "message": "" , give us some information about what was wrong.
      }

Endpoint definition.

I. RegisterCard endponit: receive 2 paramaters
  CardNumber: this field just allosw 15 digits. 
  creditAmount: is used for specify the max amount granted by the bank.
     
     Json sample for register one card.
      {
        "cardNumber": "012345678912345",
        "creditAmount": 85400
      }
      
II. Payment endponit: receive 3 parameters
  cardNumber: in this field we must insert the 15 digist for any register card.
  paymentAmout: Amout in order to register one Income or Expense.
  type: is used for identify what kinf of operation we want perfom, "I" for incom o "E" for expense.
  
      Json sample for register an expense.
      {
        "cardNumber": "012345678912345",
        "paymentAmout": 500,
        "type": "E"
      }
      
      Json sample for register an income.
      {
        "cardNumber": "012345678912345",
        "paymentAmout": 1500,
        "type": "I"
      }

III. Balance endponit: receice a header parameter, this is must be the cardNumber from where we wan get the balance. Like you can see in below sample contains the information about: CardNumber, CreditAmount, Balance and all movements(income or expenses) asscitaed with the card.

Json sample for get balance.
{
  "response": {
    "cardId": "e42def81-ee7d-45c0-84a2-a2f8942a43f4",
    "cardNumber": "012345678912345",
    "creditAmount": 85400,
    "balance": 83180,
    "balanceDetails": [
      {
        "amount": 1500,
        "feed": 0.11,
        "amountWithFeed": 1665,
        "currentBalance": 83180,
        "dateTimeOperation": "2023-04-21T16:34:00.2719719-06:00",
        "type": "E"
      },
      {
        "amount": 500,
        "feed": 0.11,
        "amountWithFeed": 555,
        "currentBalance": 84845,
        "dateTimeOperation": "2023-04-21T16:33:54.8834032-06:00",
        "type": "E"
      },
      {
        "amount": 0,
        "feed": 0,
        "amountWithFeed": 0,
        "currentBalance": 85400,
        "dateTimeOperation": "2023-04-21T16:33:23.7341293-06:00",
        "type": "I"
      }
    ]
  },
  "httpStatusCode": 200,
  "message": ""
}
