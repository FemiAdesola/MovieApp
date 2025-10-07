# MovieApp Project

![TypeScript](https://img.shields.io/badge/TypeScript-v.4-green)
![React](https://img.shields.io/badge/React-v.18-blue)
![.NET Core](https://img.shields.io/badge/.NET%20Core-v.7-purple)
![EF Core](https://img.shields.io/badge/EF%20Core-v.7-cyan)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-v.14-drakblue)


## Table of content

- [Introduction](#introduction)
- [Technologies](#technologies)
- [Installation](#installation)
- [Getting started](#getting-started)


## Introduction
This is a project built with ASP .NET core for the backend and React Typescript for the Frontend.
This project aims to understand the connection between the backend, database structure, and the frontend.
In addition, the backend was modeled first by designing the model from top to bottom and vice versa with ERD (Entity Relation Diagram), to get the relation needed, such as one-one, one-many, and many-many.

## Technologies
- Backend
    + PostgreSQL
    + ASP .NET Core, 
    + Entity Framework Core

- Frontend
    + RectJS
    + React bootsrap (for design and styling)
    + TypeScript
    + React-leaflet
    + React-router-dom
    + Fontawesome.
    + React-bootstrap-typeahead
    + Sweetalert2

## Installation

- Steps to perform the installation for the `Backend`
    + Register the database server with PostgreSQL
    + Check your local machine for .NET Core compatibility from microsoft webiste
    + Create an `appsettings.json` file in to main root like [example.json file](/backend/API/example.json)
    + Perform these following commands
        1. dotnet restore
        2. dotnet build
        3. dotnet run
    + For database migration
        1. dotnet ef migrations  add [added new name here]
        2. dotnet ef database update
- Steps to perform the installation for the `Frontend`
    + Install all the dependencies
        1. Write `npm install` on your terminal 
    + Runs the app in the development mode.
        1.  Write `npm start` on your terminal 


## Getting started

- Users have to generate a token and insert it before they could be able to get total access to all the functionality.


### Frontend page

![Frontend](/img/FrontPage.png)


### Admin Filter Page

![Admin Filter Page](/img/AdminFilterPage.png)


### user Filter Page

![User Filter Page](/img/userFilterPage.png)


### Movie page

![Move Page](/img/MoviePage.png)


### Single movie page with rating by users

![Rating Page](/img/Rating.png)
