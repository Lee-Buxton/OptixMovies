# OptixMovies

OptixMovies is a demo RESTful API project that allows users to access and edit a database of movies. 

## Data

The data directory contains the following two files: 

Full Movies DB - Around 10k Movies
data/MoviesDB/mymoviedb.csv

Slim Movies DB - Around 500 Movies
data/MoviesDB/mymoviedb-slim.csv

Due to the full DB taking 7-8 minutes to import, a smaller set of sample data is also provided. 

## Endpoints

The following endpoints exist within the project, you can also access these via the Swagger interface.

/v1/movies/
- GET - Used to get a list of all movies. Supports querying.
- PUT - Used to create a new movie.

/v1/movies/{id}
- GET - Used to retrieve a single movie. 
- POST - Used to update an existing movie.

/v1/movies/import
- PUT - Used to import the DB.

/v1/movies/genres
- GET - Used to get a list of Movie Genres. 

More details can be found in the swagger documentation, located at /swagger/index.html on the API project (https://localhost:60514/swagger/index.html).

## Query Syntax

Querying is fairly straight forward, here are an example: 

/v1/movies?filter="genre eq 'action',releaseDate ge 2024-06-01"&orderBy="releaseDate DESC"&top=10&skip=0

### Filter
You can have multiple filters, though they are AND'd together. Each filter is seperated by a comma. Each filter uses the following format. 

`<Field>(space)<Operator>(space)<Value>`

For the value, if it's a string then it should be surrounded by single quotes. If it's a number those quotes are not needed.

#### Available Fields

- releaseDate
- genre
- title
- originalLanguage
- rating.averageScore
- rating.voteCount

#### Operators

- eq (Equals)
- lt (Less Than)
- gt (Greater Than)
- ge (Greater Than or Equal to)
- le (Less Than or Equal to)
- ne (Not Equal)

#### Supported Characters for Value

- A-Z (A to Z, both upper and lower case.)
- 0-9 (Zero to Nine)
- \+ (Plus)
- \- (Hyphen)
- . (Full Stop)
- () (Left and Right parenthesis)
- | (Pipe)
- ' ' (Space)
- ' (Apostrophe)

## Projects

The project consists of the following two parts: 

API Project - OptixMovies.API
This represents the presentation layer in clean architecture. I have kept the core application logic within the module project. Only the API endpoints and the logic need to serve those are included in this project.

Movie Module - OptixMovies.Modules.Movies
This represents a couple layers of the clean architecture. Due to simplicity I have merged the core/domain, application, infrastructure and presistence within a single project. Though it would be easy to split out the code into seperate class libraries.
The Module name, comes from the overall architecture being modular monolith, though it would be fairly easy to change it to a microservice.

If I was to implement clean architecture more explicitly the project could look like the following.

Presentation
 - OptixMovies.API

Application
 - OptixMovies.Modules.Movies

Infrastructure / Persistence
 - OptixMovies.Infra.Azure
   I would group all azure items into one library, though you could seperate them further based on individual product.

Domain / Core
 - OptixMovies.Domain
   The name can easily be changed, I'm partial to using common or core over domain. 