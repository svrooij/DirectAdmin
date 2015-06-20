# DirectAdmin
Control your directadmin users from your C# application

## Getting started

```C#
using DirectAdmin.Client;
// If you will be creating new users
using DirectAdmin.Client.RequestOptions;

...

using(var client = new DirectAdminClient(
    new DirectAdminClientOptions{
      ServerName = "directadmin.domain.com",
      Username = "yourUsername",
      Password = "yourPassword", // Could also be an API key, see below
      Port = 2222, // This is the default port number
      UseHttps = false // Set to true if your directadmin installation is protected with an SSL certificate/
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

## Issues or feature requests?
If you found an issue with the software, please visit the [github repo](https://github.com/svrooij/DirectAdmin) and create an issue.
