# Dwitter
A twitter clone created for learning angular and .net core development.

An SPA application created with .NET Core 3.1 as the backend and Angular 8 as the front end. Includes the following functionalities:
* User registration and login using JWT authentication.
* Post messages which can be viewed by all users.
* Comment on posts.
* Like posts.

# Project Structure
The frontend code can be found inside the ClientApp directory. All major functionalities have been grouped together for reader's convenience.
* Auth - Contains all authentication related functionality
* Home - Entry point for the posts and comments
* Shared - Contains few files that belong to the application as a whole
* Other directories are self explanatory

The backend code is contained in all the other directories apart from ClientApp. Contains 3 Controllers for Users, Posts and Comments API's. The applicatoin is using SQLiteDb for now but replacing it with any other db is as easy as adding the connection string to appsettings.json and Adding the db context to ConfigureServies in Startup file.

# Project highlights
* JWT authenticatoin using interceptors on front end.
* Generalised error handling using interceptors
* Generalised service for handling APIs

# Work in progress
* Front end functionalities for editing, deleting posts and comments (Currently only POST methods are implemented).
* The Alert component that should show all unexpected errors that can occur in the app. Might get replaced with Toast messages soon.
* UI layout.

# Steps to run
Make sure you have .NET core SDK installed. Then just 'cd' into the project and run 'dotnet run'. In case of any issue on front end packages, try doing an npm install (Need node and npm) seperately in the ClientsApp folder and then proceed.
