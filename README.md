 [![NuGet](https://img.shields.io/nuget/dt/LiteLib%20.svg)](https://www.nuget.org/packages/LiteLib/)
 [![Analytics](https://ga-beacon.appspot.com/UA-8535255-2/unosquare/litelib/)](https://github.com/igrigorik/ga-beacon)
 [![Build Status](https://travis-ci.org/unosquare/litelib.svg?branch=master)](https://travis-ci.org/unosquare/litelib)
 [![Build status](https://ci.appveyor.com/api/projects/status/iwk94ol34b7y5411?svg=true)](https://ci.appveyor.com/project/geoperez/litelib)
 [![Coverage Status](https://coveralls.io/repos/github/unosquare/litelib/badge.svg?branch=master)](https://coveralls.io/github/unosquare/litelib?branch=master)

<img src="https://raw.githubusercontent.com/unosquare/litelib/master/litelib-logo.png" alt="LiteLib Logo" />

# LiteLib

_A cool little wrapper for [SQLite](https://www.sqlite.org/) based on [Dapper](https://github.com/StackExchange/Dapper) from Unosquare Labs -- It's also free and MIT-licensed_

:star: *Please star this project if you find it useful!* 

LiteLib is a library that helps you get the job done quickly and easily by defining POCO classes and turns those classes into SQLite-mapped tables. You define a database context and LiteLib will automatically create the necessary SQLite tables for your context. You will then be able to easily query, insert, update or delete records from the database tables via the database context you just defined. LiteLib is not intended to be a replacement for Entity Framework, but rather a lighter alternative that saves you the work of creating tables, managing connection instances, logging SQL commands, and still allows you to use Dapper-style querying. So, if you like Entity Framework, but you prefer the speed of Dapper, and you are using SQLite for your project, then we hope you'll love LiteLib!

Stuff that LiteLib *does very well*:
- Creates your database files and tables that map to classes and properties of your objects.
- Automatically gives you access to predefined `SELECT`, `UPDATE`, `INSERT` and `DELETE` commands for each of your classes.
- Automatically provides you with a simple and extensible data access layer.
- Automatically manages connection instances.
- Provides you with a log of SQL commands executed against your database file.

Stuff that LiteLib *does not* cover:
- Migrations of any kind. You'll have to drop and recreate the DB file if your schema changes or migrate it manually.
- Navigation properties or relationships. You'll have to implement and ensure consistency of data relations manually -- which BTW, it's not hard at all and lets you write faster, lighter code.
- Automatic transactions or changesets. You'll have to `BeginTransaction` and `Commit` manually. The Data context class you define simply exposes the underlying Dapper connection.

## Installation [![NuGet version](https://badge.fury.io/nu/litelib.svg)](https://badge.fury.io/nu/litelib)

You can install LiteLib via NuGet Package Manager as follows:

<pre>
PM> Install-Package LiteLib
</pre>

LiteLib doesn't contains any SQLite interop or library, so you need to add it to your project. You can choose to a general bundle or custom bundle.

<pre>
PM> Install-Package SQLitePCLRaw.bundle_green
</pre>

If you are targeting only Linux environments (only .NET Core), you can use the sqlite3 bundle.

<pre>
PM> Install-Package SQLitePCLRaw.bundle_e_sqlite3
</pre>

**Mono Users** - If you are using Mono please target to NET452.

## Usage

We designed LiteLib to be extremely easy to use. There are 3 steps you need to follow.

1. Create your model classes. All model classes must extend from `LiteModel`. There are a few class and property attributes that LiteLib understands. See the examples below.
2. Create your context class. It must extend `LiteDbContext`, and it must expose your `LiteDbSet` classes
3. Use your context class. An example provided in the following section.

## Example

Create your model class. Use the `Table` attribute to specify the name of the table you want to map your model to. Also, note we inherit from `LiteModel`. If you wish to create a unique index on a column, use the `LiteUnique` attribute on a property. If you wish to index a column, then simply use the `LiteIndex` attribute. Please note properties with complex datatypes will not be mapped to the database.

```cs
namespace Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using Unosquare.Labs.LiteLib;

    [Table("ClientAccounts")]
    public class ClientAccount : LiteModel
    {
        [LiteUnique]
        public string Username { get; set; }

        public string Password { get; set; }

        [LiteIndex]
        public bool IsUsernameIP { get; set; }

        [LiteIndex]
        public long RelayServerId { get; set; }

        public DateTime DateCreatedUtc { get; set; }
        public DateTime LastAccessDateUtc { get; set; }
        public DateTime? LockedOutDateUtc { get; set; }
        public int FailedLoginAttempts { get; set; }
    }
}
```

Next, create your database context class. Extend `LiteDbContext` and expose any number of tables via properties of the generic type `LiteDbSet<>`. A context should always be disposable so the recommendation is to query your database inside a `using` block of statements.

```cs
namespace Models
{
    using Labs.LiteLib;

    public class AppDbContext : LiteDbContext
    {
        public AppDbContext()
            : base("mydbfile.db")
        {
            // map this context to the database file mydbfile.db
        }
        
        public virtual LiteDbSet<ClientAccount> ClientAccounts { get; set; }
    }
}
```

Finally, use your database context class. For example, to query your database by username asynchronously you can just do the following:

```cs
using (var db = new AppDbContext())
{
  var accounts = await db.ClientAccounts.SelectAsync(
      $"{nameof(ClientAccount.Username)} = @{nameof(ClientAccount.Username)}", 
      new { Username = "someuser" });
}
```

At this point, it should be easy for you to see that you can easily extend your data access logic via extension methods or by extending the `LiteDbSet<>` class and exposing it as a property in your database context class.

That's it! Happy coding!
