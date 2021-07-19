# ToDoList
## web api:
Run the webapi.sln in Visual Studio.

Please ensure port 44302 is free in your machine, or you can reconfigure the port to run the web api in different port number in the solution's properties.

You could connect the database connection string in web.config, for the setting "GCUK_ToDoItemDBEntities".

Run the solution. Try to access the part /alexapi/todo to ensure it returns the data from the database.

### API DOCS:
Please visit Swagger UI in https://localhost:44302/swagger/ui/index#/ToDo

Please put this value https://localhost:44302/swagger/docs/v1 into the textbox.

## Running Vue (after ensuring webapi is running):
Please execute the index.html in frontVue directory

