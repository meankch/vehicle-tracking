# Vehicle Tracking System
Track vehicles position using GPS navigation. A device emboarded in a vehicle, will communicate with the APIs to register the vehicle and update its position.

## Technical Information
This project is implemented with `.NET 5.0`. The security applied to the APIs is `JWT token` authentication. The database used in this project is SQLite.

## Swagger API Documentation
To view Swagger API documentation of this project, you need to have the app runs on your local workspace first, then enter url `http://localhost:5000/swagger/index.html` to your internet browser, there you will see Swagger documentation of this project.

## APIs
| Usage  | API |
| ------------- | ------------- |
| Swagger API documentation  | localhost:5000/swagger/index.html |
| Register vehicle  | POST /api/vehicles  |
| Update vehicle position  | PUT /api/vehicles/positions  |
| Get current vehicle position  | GET /api/vehicles/positions/current  |
| Get vehicle position withing time range  | GET /api/vehicles/positions?userId=<userId>?deviceId=<deviceId>?startDate=<startDate>?endDate=<endDate>  |
| Login  | POST /api/login  |

## Running project in Local Workspace
To run this project in your local workspace, please follow the steps descriped below.

### Pre-requisites
1) Install [.NET 5.0 SDK](https://dotnet.microsoft.com/download/dotnet/5.0)
2) Install [Visual Studio 2019](https://visualstudio.microsoft.com/downloads/?utm_medium=microsoft&utm_source=docs.microsoft.com&utm_campaign=inline+link&utm_content=download+vs2019)
3) Install [DBeaver](https://dbeaver.io/download/) for database monitoring

### Running the project
1) Open your terminal and enter `dotnet build`
2) Then run command `dotnet run` to start the app on your local workspace. The app will be running at `localhost:5000`
3) Open DBeaver and connect to the DB of the app by adding a new Database connection
4) Then, choose SQLite as a database type and click next
5) On JDBC Connection Setting screen, at 'Path', browse for the file 'app.db' of the project and enter it to 'Path' field.
6) Click on 'Test connection' and if it turns out good, you can click on 'Finish' to add it to DBeaver