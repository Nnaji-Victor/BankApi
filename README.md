# BankApi
A restful service that supports a wide range of banking applications.

# Application
BankApi implements some element of CRUD such as GET and POST so far. A user is able to login, view account details, withdraw, transfer and deposit money.
An Admin is able to login and create customers and accounts.

# ✨ How To Use
   **login [HttpPost]**
   
	   https://api/login

   **Get Account Balance [HttpGet]**
   
   N.b: A user must sign in first to access their balance

	   https://api/customer/balance

