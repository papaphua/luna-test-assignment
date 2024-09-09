# TaskManger

## How to launch

1. Create .env file in TaskManager.App with following content:
```dotenv
JWT_ISSUER=YOUR_ISSUER
JWT_AUDIENCE=YOUR_AUDIENCE
JWT_KEY=YOUR_KEY
```
2. Within Task.Persistence using terminal create ef migration and update db:
```shell
dotnet ef migrations add MIGRATION_NAME --startup-project "../TaskManger.App"
dotnet ef database update --startup-project "../TaskManger.App"
```
3. Start app and proceed to swagger page.

## APIs

1. POST "api/users/register" - create new users
2. POST "api/users/login" - authorize users, receives jwt token
3. POST "api/tasks" - create new task, requires auth
4. GET "api/tasks" - receives paginated list of tasks, require auth
5. GET "api/tasks/{id}" - receives singe task by its id, requires auth
6. PUT "api/tasks/{id}" - updates task by its id, requires auth
7. DELETE "api/tasks/{id}" - deletes task by its id, requires auth

**To authorize you need to copy jwt key received after login, click authorize button at the top, and insert key into field. Afterward each request will contain it in header.** 

## Architecture and design

**Structure:**
1. App - entry point
2. Domain - entities and some base functionality
3. Application - business logic
4. Persistence - db access
5. Presentation - api controllers

**Generic repository pattern**
1. Shared base features among all entities.
2. Entity specific methods.

**Unit of work**
1. Saves changes
2. Starts transactions

**Result pattern**
1. For each entity there is Error class which contains its possible errors.
2. Allows to return result of operation and error message via api.


