# mvc-project

University MVC **ASP .NET Core 3.1** Course Project

## About

This is an ecommerce web application, for selling beverages. There are the following features implemented:
- User Registration/Authentication/Authorization
  - Registration and Log in pages with both client and server side data validation.
  - Admin User
     -  _The `Authorize` attribute is used, with `Roles` set to `Administrator`_
     - **no hardcoded credentials**, only the user's `AspNetUserRoles` should be set to `Administrator` inside the Database.
* Every session has it's own shopping cart, with shopping cart management system(_`Add item`, `Remove item`, `Proceed to Checkout`_). Users must be authenticated with the App, to be able to place an order (_also the `Cookie Policy` has to be accepted_ )
* A `Checkout` form providing the details to whom the goods will be shipped to.
* Order status tracking functionality for each client
* Easy-to-navigate, category-based(_`Alcoholic` and `Non-Alcoholic` drinks filter_) products list, with indicators if the product is in stock.
* Products base full text search engine
* Dynamically generated pages for each product, with comments section for clients
* Comments Moderation/Menagement system
* Users Management system
- **Admin Panel Features:**
  - Manage the products list (`Drinks table`) from the App with options for `Create`, `Delete`, `Edit`, `Details View`. The produce range can be changed on the fly through the user friendly interface.
  - Provides an overview of all orders(`Orders table`), with option for more details (`Details`), including the data the user has specified in the `Checkout View`(_Shipping and Contact information_), and the requested beverages (_displaying them, taking advantage of the following `many-to-many` DB relation:_ `Orders <-> OrderDetails <-> Drinks`). Therefore, the products can be handed over to the delivery team ASAP.  
The _new, unprocessed_ orders are at the top of the list and color highlighted ( ![#1589F0](https://via.placeholder.com/15/1589F0/000000?text=+) ), so you can determine with a glance of an eye, do you have any  work to do.
Once you pack the order, you can simply click on `Mark as Processed` under `Details`, and the status will be changed immediately. You can _undo_ this, by clicking on the newly appeared `Mark as Unprocessed` button, under the same section. There is a `Delete` functionality, too.
  - Comments moderation system: `Approve`/`Dissaprove` Comments, only `Approved` comments are shown to the end user, every new comment is `dissaproved` by default, and awaits review. `List`,`Edit`,`Delete` Functionality for each comment.
`Interactive Mode` - when a product page is viewed by and Administrator, the changes can be done from the page it self, quick and easy.
  - Users And Roles Management System: `Delete`/`Edit`/`Ban`/`Reset` User
  - `Create`/`Edit`/`Delete` Roles and `promote`/`demote` User accounts from each role

## Deployment

There are several migrations I have made while developing the project. To make  it run, make sure you have MS SQL Server instance running on your machine. Make sure that `appsettings.json` has the right SQL server parameters specified. To run the migration you can either use powershell(make sure you are in the root folder of the cloned Git repo):
```powershell
dotnet ef database update
```
**Or inside Visual Studio Package Manager Console:**
```
Update-Database
```

There is `DbInitializer.cs` inside `/Data/Models/` to populate the database with products. It is set to run automatically if there are no products in the database. You can turn it off. To do so, in `Startup.cs`,at the end of the file, comment the following line which calls the `Seed` method:
```c#
DbInitializer.Seed(serviceProvider);
```
If the above method is never called tough, the `Drinks` and `Categories` tables will be empty.
#### Environment Variables
Inside `Startup.cs` is a check for the `ASPNETCORE_ENVIRONMENT` variable. The default value is `Development`.
If it is set to `Production`, the
`UseDeveloperExceptionPage` and `UseStatusCodePages` middlewares are **disabled** and the exception handling is done by the `UseExceptionHandler` middleware (custom  action - `/Home/Error`). There is also another middleware which is configured specifically to handle only the **404** errors - `UseStatusCodePagesWithReExecute`, with custom action (`/Home/HttpError`).


## Screenshots Slideshow
[![bloggif_5f88bcb466163.gif](https://s8.gifyu.com/images/bloggif_5f88bcb466163.gif)](https://gifyu.com/image/8NXx)


## Database
![DB-tables](https://i.imgur.com/zNCv50W.png)

## License
[MIT](https://github.com/p4nd4ta/mvc-project/blob/main/LICENSE)
