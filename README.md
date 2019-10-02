# Web App Ticket System

Stack using
- C#
- DotNet Core 2.1
- React
- SQL Server

## Steps to build:
- load project file in Visual Studio 2019
- NuGet
- npm intall
- setup SQL Server Express:
    - setup db
    - setup login using User Id=userdb;Password=sqlSQL!123456
```
create database webapp_db;
use webapp_db;
create table UserData(Id int NOT NULL PRIMARY KEY, Name varchar(255), Password varchar(512), Token varchar(MAX), GroupRole int);
```

## Assumptions about requirements:

- Needs role management
- User database, chose SQL, could have used an in-memory middleware
- ticket class, field for ownership
- create, update, view, delete routes
- security didn't seem to be an issue at this point, but some scaffolding for future security is present

## Overview:

The initial setup leveraged Visual Studio's built-in templates. React bootstrap was a part of the template.

React uses a navigation bar to allow switching between the different views. React router allows navigating to the different client abilities: Login, logout, View tickets, create tickets, edit tickets. Deleting tickets did not need a view, maybe version 2 could offer a modal confirmation of deletion.

The idea is to keep the model data separate from the view. Role and security remain on the controller end. Data sent by the routes is configured by the user's role. User's roles are stored in a cookie authentication middleware. Routes such as create and delete check permissions at the controller level, before the database is engaged. Standard status codes are used for client response checking. Version 2 could offer client level feedback for different respones.

Axios library was used to handle ajax calls and responses.

The UserGroup class was created to create roles using a permission enumumerator. Using a bit flag structure, this is flexible for future role assignments. Version 2 could offer administrator role for assigning permissions.

The user database was faked, because I ran into problems seeding it and ran out of time. It would be used to store user names, and their roles. Roles can be independent of the user names. Version 2 would need to store users and passwords. Passwords would need to be hashed and stored properly.

SQL access uses Linq commands.

### Current Problems:
- login name could be stored at client level (to stop the lag between displaying login fields or not)
- could use database seeding for faster setup
- security issues
