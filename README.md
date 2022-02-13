# PunkAPI Test
 Test API project for <a href="https://punkapi.com/">PunkAPI</a>

 <p>The purpose of this project is to allow users to post reviews for the beers they have tried that are presently listed on PunkAPI, or to find reviews for beers they're considering.</p>

# For API users
<h3>Searching Reviews</h3>
<p>The API accepts one parameter in the query string of the request. The purpose of this parameter is to denote the name (or partial name) of the beer to search for. The public PunkAPI is used to retrieve all beers matching the search parameter.</p>

/getratings/ThisBeer

<p>Sample output can be seen below:</p>
[
	{
		"id":24,
		"name":"The End Of History",
		"description":"The End of History: The name derives from the famous work of philosopher Francis Fukuyama, this is to beer what democracy is to history. Complexity defined. Floral, grapefruit, caramel and cloves are intensified by boozy heat.",
		"userRatings":[
			{
				"id":24,
				"username":"test@test.com",
				"rating":4,
				"comments":"This one is quality!"
			}
		]
	}
]

<h3>Posting Reviews</h3>
<p>Users can post a rating to the database using a HTTP POST with a beer id URL parameter and JSON request body which includes the following properties: username, rating, comments.</p>

Sample:</br>
username: 'test@test.com', rating: 4, comments: 'This one is quality!'

<p>The username must be a valid email address and the rating must be an integer between 0 and 5.</p>

# For Developers
<h3>Deployment</h3>
<p>After pulling from this repo, the project can be run locally through the IDE using IISExpress. For unit tests, Google Chrome and/or Firefox are recommended</p>

<h3>Data Store</h3>
<p>Data is stored in a JSON database file. Any ratings posted by users will be stored here. By default, the file is created in the following location:</p>
%APPDATA%\Roaming\PunkAPIProject\database.json</br>

<p>Here is a sample of a stored rating in the file:</p>
[
	{
		"id": 1,
		"username": "test@test.com",
		"rating": 4,
		"comments": "This one is quality!"
	}
]</br>

<p>The passed username must pass a RegEx expression to conform to typical email address standards, and rating values must be integers between 0 and 5.</p>

<p>The RatingDB class can be modified or replaced to support other technologies.</p>

<h3>Unit Tests</h3>

<p>Unit tests can be found in unitTests.html. A sequence of valid and invalid tests can be run here to test the behavior of the API.</p>

http://localhost:64937/UnitTests/unitTests.html



  

