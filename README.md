# adamhilton.net - an ASP.NET Core 1.0 website

### Hello
Currently, [adamhilton.net](http://adamhilton.net) is just a 1 page site that I used to test out running an ASP.NET Core 1.0 app targeting the Core CLR on a linux server.
My goal is to create a blogging engine targeting the Core CLR that anyone can use. I'm licensing this project under the [MIT license](LICENSE), 
so feel free to use my code however you want! My only request is that you don't 100% steal my website design (you can do way better anyways).

### Run this yourself!
1. Fork, clone, restore dependencies, and build. 

2. Install [PostgreSql](http://www.postgresql.org/download/) and add user with permisions to create databases

3.  Add an appsettings.json file to the root of both AHNet.Web and AHNet.Domain.

###### AHNet.Web/appsettings.json:
``` json
{
  "Logging": {
    "IncludeScopes": false,
    "LogLevel": {
      "Default": "Verbose",
      "System": "Information",
      "Microsoft": "Information"
    }
  }
}
```

###### AHNet.Domain/appsettings.json:
``` json
{
  "Data": {
    "DefaultConnection": {
      "ConnectionString": "User ID={userid};Password={password};Host=localhost;Port=5432;Database={database};Pooling=true;"
    }
  }
}
```



