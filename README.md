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
## Development notes ##

This section is to explain some of the decisions made.

I named the endpoint Customers as that is what it is being added to. Even though the requirements say it is for Registering a customer, I think REST guidelines say don't use the describing verbs, try to stick to the HTTP verbs. So in effect Registering is really 'Creating' a customer and Create's should be done via HTTP POST verb. Some people call the method name which handles the POST verb 'Post' but I called it 'Create'.

### Layers ###

For a Web service this small I would normally keep everything except the unit tests in single Web API project but have sub folders for Models, Data (layer), Controllers, Validators to show logical separation. However I've put these into different assemblies to show how a bigger application might be structured. Excluding testing, I've gone with a compromise of just 3 layers although lots of projects i've worked on have 7 or more layers. Here they are in terms of how they provide separation of concerns:

#### 1. Web.Api #### 
Just the REST API layer with knowledge of how to receive the input and perform model binding and return the output including correct HTTP status codes.  

#### 2. Web.Api.Core #### 
More of pure C# assembly with no references to HTTP layer above and no knowledge of persistance layer. This layer includes the DTO objects in folder Models, validation rules in folder Validators and Business logic in folder Services. Probably a bit over the top but choose to allow service to receive a DTO rather than the Entity used by persistance layer as you don't normally expose the data entities to the UI or Web Service layer directly... normally a 'ViewModel' or 'DTO' is used for that purpose. In the Service layer, the Adapt extension method from Mapster library is used to convert DTO to Entity.

#### 3. Web.Api.Infrastructure ####
This is anything which is not Web.Api and not 'Core' logic e.g. things which would access the database, network, file system. For these requirements though, there is just a single collection of concerns in subfolder 'Data'. This contains the data entities for things like Customer, and a repository for Customer. Migrations folder is creating by the Entity Framework tooling

#### 4. Web.Api.Core.Tests ####
As the name implies, this is just the unit tests of classes from Web.Api.Core. Only had time to implement validation tests 

### Implementation of the validation requirements ###

You can use the built in model data annotations such as Required, Length to implement some of the validation requirements, but I went with the FluentValidation library as they have helpers to make Unit testing easier. When I came to add Swagger later on, the only downside is that using plain data annotations would have improved the swagger documentation so would indicate which fields were required automatically. I guess you could go with both approaches but then it is not very DRY.

I have tested that if the model submitted does not pass the validation rules, then you get a HTTP 400 error with the JSON array of model errors. Some people go with HTTP 422 I think as the model may be 'correct' in terms of properties submitted but just invalid in terms of validation.

Likewise when the model is correct and the customer can be created, I just went with HTTP 200, but again some people uses HTTP 201 Created. 

### Areas for improvement ###

I treated the Age check in the most simplistic manner by just subtracting 18 years from current date and doing date comparison but ran out of time to think about about people born on Feb 29th in Leap years so didn't write any unit tests for that. As the age check is delegated to a function in the validator class, I did create an overload with intention of allowing a set of unit tests to pass in different 'today dates' but ran out of time to write any such tests.

I treated the Email check with a regular expression in the end. The FluentValidation library does offer a EmailAddress built in function but it only seems to enforce "a@b" type addresses. The requirements might also have been wrong, in that they said at least 4 alphanumerics followed by @, followed by least 2 alphanumerics but email addresses should be able to contain more than alphanumerics before the @ symbol e.g. dash, hyphen, period, etc so in the end i went with regular expression which allows any character except whitespace before an @ symbol following by at least two non whitespace characters. I didn't allow for leading or trailing whitespace in the emails too which probably needs to be improved.

### Non requirements (but maybe needed?) ###
The requirements did not ask for this but you said in the interview, try to read in between the lines, so I added some business logic in the CustomerService class to perform a check to see if the customer  has already registered with the same email before and if so it does not create a new customer. This is detected in the CustomerController and returned as HTTP 400 Bad Request like any other validation problem.

