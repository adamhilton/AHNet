# adamhilton.net - an ASP.NET Core website

[![Build status](https://img.shields.io/appveyor/ci/felsig/ahnet/master.svg?style=flat-square)](https://ci.appveyor.com/project/Felsig/ahnet/branch/master)
[![License](https://img.shields.io/badge/license-MIT-blue.svg?style=flat-square)](LICENSE)


## quick start

1. navigate to src/AHNet.Web
2. run 'dotnet user-secrets set AHNET_ADMINUSER {admin-user-name} && dotnet user-secrets set ANET_ADMINPASS {admin-user-password}'
3. run 'dotnet run --Environment=Development' in AHNet.Web project
4. site should be running on localhost:5000


## supported databases

- in memory 
- postgreSql


## supported environment variables

**AHNET_ADMINUSER** (Required) - used to set initial admin username

**AHNET_ADMINPASS** (Required) - used to set initial admin password

**AHNET_DBTYPE** (Required) - used to select which database to use

**AHNET_DBHOST** (Required if database is  not in memory) - used as part of db connection string if a non in-memory database is chosen

**AHNET_DBNAME** (Required if database is  not in memory) - used as part of db connection string if a non in-memory database is chosen

**AHNET_DBOWNER** (Required if database is  not in memory) - used as part of db connection string if a non in-memory database is chosen

**AHNET_DBPASSWORD** (Required if database is  not in memory) - used as part of db connection string if a non in-memory database is chosen

**AHNET_DBPORT** (Required if database is  not in memory) - used as part of db connection string if a non in-memory database is chosen

**AHNET_DBPOOLING** (Required if database is  not in memory) - used as part of db connection string if a non in-memory database is chosen

## license

[MIT License](LICENSE)
 
