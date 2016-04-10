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

3. Add connection string of the postgres database as an environment variable called AHNet:Data:DefaultConnection:ConnectionString on your system

4. Finally, run 'dnx ef database update' inside the Camp.Domain root





