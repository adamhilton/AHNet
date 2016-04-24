# adamhilton.net - an ASP.NET Core 1.0 website

[![Build status](https://img.shields.io/appveyor/ci/felsig/ahnet/master.svg?style=flat-square)](https://ci.appveyor.com/project/Felsig/ahnet/branch/master)
[![License](https://img.shields.io/badge/license-MIT-blue.svg?style=flat-square)](LICENSE)


### Hello
Currently, [adamhilton.net](http://adamhilton.net) is just a 1 page site that I used to test out running an ASP.NET Core 1.0 app targeting the Core CLR on a linux server.
My goal is to create a blogging engine targeting the Core CLR that anyone can use. I'm licensing this project under the [MIT license](LICENSE), 
so feel free to use my code however you want! My only request is that you don't 100% steal my website design (you can do way better anyways).

### Run this yourself!
1. Fork, clone, restore dependencies, and build. 

2. Install [PostgreSql](http://www.postgresql.org/download/) and add user with permisions to create databases

3. Add connection string of the postgres database as an environment variable called AHNetConnectionString on your system

4. Run 'dnx ef database update' inside the Camp.Web root

5. Before running the project, it's a good idea to add two environment variables, AHNetSeedUserName and AHNetSeedPassword. 
   This will be used to create a seed user if no user has been created yet. 

### Import information for unit test project!
The unit test project is using a version of [Moq](https://github.com/moq/moq4) that is only available via MyGet. 
It's a version that Microsoft has ported over for their own personal use, which is compatible with ASP.NET Core.

Use this command to add the nuget source before restoring the test project's dependencies:
``` Shell
nuget sources add -Name aspnet-contrib -Source https://www.myget.org/F/aspnet-contrib/api/v3/index.json
```




