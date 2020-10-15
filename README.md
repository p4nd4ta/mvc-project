# mvc-project

University MVC **ASP .NET Core 3.1** Course Project

## About

This is an ecommerce web application, for selling drinks. There are the following features implemented:
* User Registration/Authorization
* Admin User (**no hardcoded credentials**, only the user's `AspNetUserRoles` should be set to `Administrator`)
* Every session has it's own shopping cart, with shopping cart management system, users must be logged in to order.
* A `checkout form` providing the details to whom the goods will be shipped to.
* Easy-to-navigate, category-based products list, with indicators if the product is in stock.
* Admin panel with interface to the products database with options for `Create`, `Delete`, `Edit`, `Details View`. The produce range can be changed on the fly through the user friendly interface.
* Admin panel with interface to the database for orders, which provides detailed view(`Details`), so orders can be handed over to the delivery team ASAP. When the order is complete, there is a `Delete` functionality.


## Deployment

There are several migrations I have made while developing the project. To make  it run, make sure you have MS SQL Server instance running on your machine. Make sure that `appsettings.json` has the right SQL server parameters specified. To run the migration you can either use powershell:
```powershell
dotnet ef database update
```
**Or inside Visual Studio Package Manager Console:**
```
Update-Database
```

There is `DbInitializer.cs` inside `/Data/Models/` to populate the database with products. It is set to run automatically if there are no products in the database. You may want to turn it off. To do so, in `Startup.cs`,at the end of the file, comment the following line which calls the `Seed` method:
```c#
DbInitializer.Seed(serviceProvider);
```

## Screenshots Slideshow
[![bloggif_5f88bcb466163.gif](https://s8.gifyu.com/images/bloggif_5f88bcb466163.gif)](https://gifyu.com/image/8NXx)


## Database
![DB-tables](https://i.ibb.co/pK4m6v4/DB-tables.png)

## License
[MIT](https://github.com/p4nd4ta/mvc-project/blob/main/LICENSE)