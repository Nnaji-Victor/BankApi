# BankApi
A restful service that supports a wide range of banking applications.

# Application
BankApi implements some element of CRUD such as GET and POST so far. A user is able to login, view account details, withdraw, transfer and deposit money.
An Admin is able to login and create customers and accounts.

# ✨ How To Use
   **login [HttpPost]**
   
	   https://api/login

	   {
	   	   "username":"VIC0005",
		   "password":"vik"
	   }

   **Get Account Balance [HttpGet]**
   
   N.b: A user must sign in first to access their balance

	   https://api/customer/balance

   **Registering user[HttpPost]**
      
	   https://api/admin/register

	   {
		"LastName":"Marcus",
		"FirstName":"Rashford",
		"City":"Manchester",
		"Phone":"2345678752",
		"CustomerId":"Vic0006",
		"AccounType":"savings",
		"Description":"footballer account",
		"Balance":"20000",
		"Password":"marcus",
		"AccountId":"6060005",
		"Date":"1/2/2019"
		}


   **Withdraw [HttpPost]**
   
	   https://api/customer/withdraw

	    {
		"UserId":"VIC0001",
		"Date":"24/9/2019",
		"WithdrawAmount":2000.00
        }


   **Deposit [HttpPost]**
   
	   https://api/customer/deposit

	    {
		"UserId":"VIC0001",
		"Date":"24/9/2019",
		"DepositAmount":2000.00
        }