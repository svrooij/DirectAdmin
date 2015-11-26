# DirectAdmin
Control your DirectAdmin users from your C# application

[![Build status](https://ci.appveyor.com/api/projects/status/u6gtqri2fie11768?svg=true)](https://ci.appveyor.com/project/svrooij/directadmin)

## Getting started

Install DirectAdmin.Client with NuGet. PackageManager `PM> Install-Package DirectAdmin.Client`

```C#
using DirectAdmin.Client;
// If you will be creating new users, you should also add DirectAdmin.Client.RequestOptions
using DirectAdmin.Client.RequestOptions;

...

using(var client = new DirectAdminClient(
    new DirectAdminClientOptions{
      ServerName = "directadmin.domain.com",
      Username = "yourUsername",
      Password = "yourPassword", // Could also be an API key, see below
      Port = 2222, // This is the default port number
      UseHttps = false // Set to true if your directadmin installation is protected with an SSL certificate
    }
  )){
    // List all users
    var users = await client.ListUsers();

    // Set a password for a user
    await client.ResetPasswordForUser("userName","newPassword");

    // Create a new user
    await client.CreateUser(new CreateUserOptions{
        DefaultIpAddress = "198.51.100.24",
        Domain = "stephan.com",
        Email = "github@svrooij.nl",
        NotifyUser = true, // Should the user get an email containing the hosting information?
        PackageName = "NameOfAHostingPackage",
        Password = "theNewPasswordForTheUser",
        Username = "Stephan"
    });
  }
```

## Use an API key for **improved security**!
Directadmin has an option to create a (restricted) [API key](http://help.directadmin.com/item.php?id=523). This can be used instead of the ``Password`` in the ``DirectAdminClientOptions``.
To create an API key go to ``/CMD_LOGIN_KEYS`` in your DirectAdmin installation. Or Select ``User Level`` and click ``Login Keys``

## Issues or feature requests?
If you found an issue with the software, please visit the [github repo](https://github.com/svrooij/DirectAdmin) and create an issue.