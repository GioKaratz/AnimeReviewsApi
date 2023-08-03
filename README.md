# AnimeReviewsApi
This is a RESTful API for managing anime reviews. It provides endpoints to access and manipulate anime reviews, reviewers, categories and authors

**API Endpoints**  
Anime Endpoints  
GET /api/anime: Retrieve a list of all anime.  
GET /api/anime/{id}: Retrieve details of a specific anime by its ID.  
POST /api/anime: Create a new anime entry.  
PUT /api/anime/{id}: Update an existing anime entry by its ID.  
DELETE /api/anime/{id}: Delete an anime entry by its ID.  

Review Endpoints  
GET /api/reviews: Retrieve a list of all reviews.  
GET /api/reviews/{id}: Retrieve details of a specific review by its ID.  
POST /api/reviews: Create a new review entry.  
PUT /api/reviews/{id}: Update an existing review entry by its ID.  
DELETE /api/reviews/{id}: Delete a review entry by its ID.  

Reviewer Endpoints  
GET /api/reviewers: Retrieve a list of all reviewers.  
GET /api/reviewers/{id}: Retrieve details of a specific reviewer by their ID.  
POST /api/reviewers: Create a new reviewer entry.  
PUT /api/reviewers/{id}: Update an existing reviewer entry by their ID.  
DELETE /api/reviewers/{id}: Delete a reviewer entry by their ID.  

Category Endpoints  
GET /api/categories: Retrieve a list of all categories.  
GET /api/categories/{id}: Retrieve details of a specific category by its ID.  
POST /api/categories: Create a new category entry.  
PUT /api/categories/{id}: Update an existing category entry by its ID.  
DELETE /api/categories/{id}: Delete a category entry by its ID.  

Author Endpoints  
GET /api/authors: Retrieve a list of all authors.  
GET /api/authors/{id}: Retrieve details of a specific author by their ID.  
POST /api/authors: Create a new author entry.  
PUT /api/authors/{id}: Update an existing author entry by their ID.  
DELETE /api/authors/{id}: Delete an author entry by their ID.  

**Technologies Used**  
This API was built using the following technologies and frameworks:

.NET 7  
Microsoft Asp.Net Core  
Entity Framework Core  
AutoMapper  
Swagger/OpenAPI  
Microsoft SQL Server  

**CORS Configuration**  
The API is configured to allow cross-origin requests from any domain (AllowAnyOrigin). This is suitable for development but should be modified for production deployments to restrict allowed origins.

**How to use**  
Clone this repository to your local machine  
Set up the database connection:  
  Edit the **appsettings.json** file located in the AnimeReviewsApi project to specify your SQL Server connection string  
To create and apply the initial database schema, run the following command in the AnimeReviewsData project:  
 ```bash
 dotnet Update - Database
```
