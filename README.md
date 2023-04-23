# OPTO - Test .NET Client

This project features a sample .NET Client, using Windows Forms, to test basic CRUD functionality of Elasticsearch, by using a .NET Elasticsearch client named NEST.
Additionally, some additional contents were also added to the "Guidelines_and_notes" folder:

## .DLLs to be used in the Unity project
After analysis and testing of the correct .DLL files to be used in the Unity project, the stable versions and their dependencies have been placed here to allow easier transition to the Unity + Elasticsearch integration further on.

## Example inserted Data
This folder features two listings of Index data in the Exercises and Sessions index, after a bulk insertion of data from an Excel file to Elasticsearch, using its API.
(Please note that only a few of the hundreds of example documents are being displayed).

## Information about the Indexes
You can find information about the Exercises and Sessions indexes that were generated directly by mapping the Classes defined in this project (NOTE: subject to changes)

## Postman collection
This folder contains a Postman collection of test calls to the REST API exposed by the Elasticsearch server, namely the same CRUD operations using plain JSON, and some more.

## Virtual Machine files
### 1) VM Configuration files
The Elasticsearch server installation was done on a Ubuntu Virtual Machine. Its configuration files were also added here, to display the parameters being used on the server (NOTE: subject to changes). 

### 2) VM study notes
Additionally, some notes were written during the initial setup and approach of discovering Elasticsearch and Kibana, and they can also be found here. It features a mix of logs, good practices, specific notes deemed as important, and study notes that helped understand how these complex systems work.

Please note that any of the files here, or even the test client itself, are subject to reanalysis, to allow accomodation of changes in requirements/best practices over time. Thank you.
