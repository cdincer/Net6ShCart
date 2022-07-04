## Net6ShCart
Practicing couple of things and seeing how NET 6.0 coming along. Everything you see below has NET 6.0 associated with them.
Used Rule Engine for checking if a Item can be added to shopping cart, Repository pattern for connecting to DB to keep things simple.
Technologies used on this project are:
```
* Sqlite 6.0.6
* EF Core 6.0.6
* XUnit 2.4.1
* XUnit DI 6.2.17
* EF InMemory 6.0.6

```

### Features:
```
1-Start to finish done in Visual Stuido Code. No Sln,no problem.
2-Single .csproj, no seperation for tests.
3-Simple extensions are used: .NET Core Test Explorer, C# Extensions, Docker. 
4-Two seperate DB Connections. One for testing only,one for development. For this project both of them are in-memory.
5-Testing is done via DI. Everything is written to in-memory DB. Everything is seeded for these tests.
6-For accessibility everything is seeded through JSON files.
```

### Usage:
```
For local things:
Listening on :http://localhost:7294
Swagger is on:http://localhost:7294/swagger/index.html
For docker things:
While in Visual Studio Code, make sure you have the Docker extension and right click on "docker-compose.yml" file and pick "Compose Up"
Afterwards same as above head to for
Listening on:  http://localhost:7294
Swagger is on: http://localhost:7294/swagger/index.html
```
