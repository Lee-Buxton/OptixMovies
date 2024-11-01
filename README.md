# OptixMovies

OptixMovies is a demo RESTful API project that allows users to access a database of movies. 

## Task Items

This project was requested to have the following features: 

- Required
  - As a user I want to search movies by title/name”
  - As a user I want to be able to limit the number of results per search
  - As a user I want to be able to ‘page’ through the list of movies
  - As a user I want to filter movies by genre
- Optional
  - As a user I want to filter movies by actors
  - As a user I want to be able to sort movies by title/name or release date

The project can do all except "As a user I want to filter movies by actors" as the dataset provided doesn't include the actor information.
If I wanted to achieve that I would have needed to setup a connection to TMDB or IMDB to get the actor information direct from their API.

## How to run
Run the following command to start the application. Alternatively open the application in Visual Stuido and press F5. 

`dotnet run --project .\src\OptixMovies.API\`

### Production

If you wanted to use in production, the manual method would be as follow. This will only work if you setup the project to use an Azure Cosmos instance and not the Cosmos emulator.

`docker build -f .\src\OptixMovies.API\Dockerfile -t optixmovies.api:latest .`

`docker run -p 60123:8080 optixmovies.api:latest`

If you run this locally you will be able to access it on http://localhost:60123/swagger.

## Endpoints

The following endpoints exist within the project, you can also access these via the Swagger interface.

/movies   
- GET - Used to get a list of all movies. Supports querying.

### Query String Parameters

#### Title - String
You can search the title of any movie. Supports partial matching.

example   
`title=man` - Returns movies like Batman, spiderman, superman, etc


#### Genres - Array\<string\>
You can search on movie genres. This supports multiple values comma seperated. 

Examples   
`genres=action` - Single Genre  
`genres=action&genres=fantasy` - Multiple Genres. 


#### OrderByReleaseDate - bool
Order any results by the release date. By default this is in Ascending order. 

Examples   
`orderbyReleaseDate=true` - Order By Release Date Ascending  
`orderbyReleaseDate=true&orderByDescending=true` - Order By Release Date Descending


#### OrderByTitle - bool
Order any results by the title. By default this is in Ascending order. 

Examples   
`orderByTitlee=true` - Order By Release Date Ascending  
`orderByTitle=true&orderByDescending=true` - Order By Release Date Descending


#### OrderByDirectionDescending - bool
Changed the order by direction from ascending to descending. Example can be seen in the OrderByTitle and OrderByReleaseDate parameters mentioned above.


#### PageSize - int
Defines the page size of the results. Default value is 20

Example   
`pageSize=50` - Sets the page size to 50. 


#### PageNumber - int
Defines the page of results to load. Default value is 0

Example   
`pageNumber=5` - Sets the page number to 5. 


Details can be found in the swagger documentation, located at /swagger/index.html on the API project.


### Order By
The order by is fairly straight forward and only supports two values:
- ASC (Ascending)
- DESC (Descending)

The format is as follows.
`<Field>(space)<ASC or DESC>`

example
`ReleaseDate DESC`
