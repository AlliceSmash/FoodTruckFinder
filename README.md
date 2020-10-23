## Summary
This is a console application that utilizes Socrata API to display current food trucks that are open in SF area at the time of running the Console App. It includes 3 projects: Common which defines interfaces and data models, FoodTruckDataService which implements IDataService defined in Common, and FoodTruckFinder which is the start up project. To run the app, if it has been built, go to *buildDir/bin/Debug/netcoreapp3.1/*, with any shell, type `FoodTruckFinder.exe`, it will start displaying list of food truck(s) that are open currently.

## Future work
To extend the console app to a web application, we could reuse the `Common` and `FoodTruckDataService` projects, and create a new Web Application project. For the API call, first we need to get an AppToken and update the API call with proper token. 

Currently the dataset hosted at Socrata is fairly small, therefore we are not worried about query/http request to be a bottleneck. If in the future, there are millions of food trucks registered, we could consider a dedicated data service to pull data from Socrata constantly, then our web application will just query this dedicated service. 

Secondly, even with current data size, to decrease number of requests to the socrata API, we could use some sort of cache mechanism, it could be in memory cache or in memory database. 

The app can be hosted in the Cloud with load balancer in front of several hosting servers so that each server can share the traffic load. Third, we need to add proper logging and performance metric. Elastic search logger can be used and easily integrated with .net core. With the data collected via Elastic Search, we can use Grafana or Kibana Dashboard to analyze traffic pattern and our application usage as well. NewRelic plugins will be useful to monitor traffic load and proper apdex configurations can alert us if servers are under pressure so that we can promptly scale up.

In addition, we should implement proper error handling. Current console app did not implement any error handling or retry when http request is timed out. We should implement retry policy with a combination of fixed retry times and increased wait time. For other error situations, we should have a friendly UI display and log properly.
