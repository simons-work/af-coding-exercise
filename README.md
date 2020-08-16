# af-coding-exercise

## Installation notes ##

If you used git clone to download the source code, then it definitely builds ok  in Visual Studio 2019 Community Edition so hopefully will be ok in other version

To create the local database where it saves the new customer details, double click on the top level folder file: CreateLocalDatabase.bat

It will tell you the name of the database and where it plans to install it ... hopefully you have a local instance (localdb)\\MSSQLLocalDB

## Usage notes ##

When you Debug the solution, it should open a Swagger UI page where you can exercise the Web API endpoint

Can also use POSTMAN if you prefer:

```
POST https://localhost:44349/api/customers
{
    "firstName": "Simon", 
    "lastName": "Evans",
    "policyNumber": "AB-223454",
    "dateOfBirth": "1972-08-16",
    "email": "simon.evans@test.com"
}
```
