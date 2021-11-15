# WeatherService

The weather service that will retrieve daily information for number of cities in the world. 

## Getting Started

- Take the clone of the project in your local machine
- Open the solution in Visual Studio preferably Visual Studio 2020
- Build the solution and it should restore the nuget packages and succeed.

### Prerequisites

- Visual Studio 2020
- .Net 6.0

## Assumptions
- The list of cities will be in the form of csv file.
- The format of the csv file is as below:
   - CityId,CityName
   - 2643741,City of London
   - 5128581,New York
   - 1275339,Mumbai
   - 6539761,Rome
- Sample csv file will be found in the <b>SampleCsv</b> folder
- All the weather data is retrieved for the city. 
- The result will also be a csv file.


## Details
- HTTP Method: GET
- Host Name: api.openweathermap.org
- Service context: data/2.5/weather
- More information regarding the API can be found here: https://openweathermap.org/current

## How to run
- Postman can be used for executing and seeing the response
- Select the Get and paste url 'http://localhost:xxxxx/api/v1/getweatherforcities'.  
- Select the key value as file with type as file and select the csv file in the value.
- Hit send to get the response

## Authors
   Brijesh Kothari

