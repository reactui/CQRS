Invelop.CQRS.UI  (the Angular app)
Invelop.CQRS.WebApi (the WebApi C# app)

Enter the Invelop.CQRS.WebApi  folder and type from command prompt "dotnet run", this will start the Kestrel web-server on http://localhost:5000
Enter the Invelop.CQRS.UI folder and type "npm install" to install the needed modules, then "npm start". This will start the angular application on http://localhost:4200/

Open a browser and  type http://localhost:4200/, if everything is correct you should be able to a grid with data

Implementation Details:

Since CQRS is essential. I went on to implement the solution this way, by separating Commands (Create, Update, Delete) from Query (Read). This way it is easier to use different Models (e.g. you may need a more complex model for Read, e.g. to fetch related tables, than Delete). This way it is also easier to hook complex code, e.g. queues for Commands and Cache for Queries. Also easier to Unit Test.  In .NET core this goes well with the Mediator pattern, which I have used in addition to my CQRS approach (using the Nuget MediatR package). MediatR nicely abstracts the plumbing needed to organize the CQRS chain through events and provides a pipeline, where we can plug in the FluentValidator logic as well. In the project file structure, you will find the commands and queries in the Feature/ContactFeatures folder, and the FluentValidator pipeline hooked in the PipelineBehaviour and Validator folders.

image.png

Since EntityFramework is essential as well, I chose to demonstrate it too. The simplest way to do it is using SQLite, since it is file only database provider, still relational and supported by EntityFramework. This way, there is no extra need for you to setup a database, it will self-create on startup.

There is also a Swagger API view available when you run directly the .NET app in Visual Studio - via this URL https://localhost:44311/swagger/index.html

Rich Domain Models (as opposed to anemic models) is having actual code and logic in the models as well (only code that pertains to the respective model), however in my case using the MediatR package, Commands encapsulate the different models for different commands and I think this is close enough to what you need, other approaches are also possible of course, this is just one way to approach the problem.

On the client-side (Angular), I've chosen to use the PrimeNG components, namely their Table component, which provides all the means for CRUD operations. For e2e Cypress testing, I've set up a test to click on the first row of the grid and verify that the popup edit window is displayed. This can easily be enhanced to a whole suite of tests covering the UI. To start Cypress go to the UI folder and type  npx cypress run

