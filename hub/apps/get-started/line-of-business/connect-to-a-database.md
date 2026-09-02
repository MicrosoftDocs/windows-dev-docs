---
title: Connect a WinUI app to a database
description: Connect a WinUI 3 app to local or enterprise data using Entity Framework Core, asynchronous operations, and secure service boundaries.
ms.topic: how-to
ms.date: 09/02/2026
author: GrantMeStrength
ms.author: jken
---

# Connect a WinUI app to a database

A line-of-business app can store data on the device, call an enterprise service, or combine both approaches for offline use. Keep database code outside the UI layer so that you can test it, handle failures consistently, and change providers without rewriting the view.

:::image type="content" source="images/03-database-access.png" alt-text="A WinUI 3 task tracker displaying records loaded from a local SQLite database.":::

## Choose an architecture

| Scenario | Starting point |
|---|---|
| Local settings, records, or cache | SQLite, optionally through EF Core |
| Enterprise data shared by multiple users | An authenticated HTTPS API that owns database access |
| Direct access in a controlled environment | A supported database client or EF Core provider, with authentication and authorization reviewed for that environment |
| Read-only service data | `HttpClient` and JSON deserialization, with a local cache if offline access is required |

Don't embed a shared database password or privileged connection string in a desktop app. A user can inspect files and application memory on a device they control. A service layer protects database topology and provides a central authorization, validation, auditing, and versioning boundary.

## Use EF Core

EF Core maps .NET objects to a relational database. Its provider model supports SQL Server, SQLite, PostgreSQL, MySQL, MariaDB, Oracle, and other databases. Most data-access code can remain provider-independent, but migrations, SQL features, and type behavior can differ by provider.

Add the provider that your app needs. For SQLite:

```console
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
```

Keep package versions on a supported servicing release and review transitive dependency advisories as part of normal dependency maintenance.

## Define the model and context

The following example context stores its database beneath the current user's local application-data folder, which works for packaged and unpackaged desktop apps:

```csharp
public sealed class TaskDbContext : DbContext
{
    public DbSet<TaskItem> Tasks => Set<TaskItem>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        string folder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "DatabaseAccess");
        Directory.CreateDirectory(folder);

        string databasePath = Path.Combine(folder, "tasks.db");
        options.UseSqlite($"Data Source={databasePath}");
    }
}
```

Don't write a database into the installed application directory. Choose a per-user or shared data location that matches your deployment and access requirements.

## Create and evolve the database

`Database.EnsureCreatedAsync()` is convenient for a prototype or a database that can be recreated. It bypasses migrations and isn't suitable for evolving a production schema in place.

Use [EF Core migrations](/ef/core/managing-schemas/migrations/) when an app must preserve existing data across schema versions. Test upgrades from every supported deployed version and define a recovery strategy for migration failures.

## Keep the UI responsive

Use asynchronous database and network APIs where the provider implements them. Don't block the UI thread with `.Result`, `.Wait()`, or synchronous network calls.

Asynchronous APIs don't automatically mean that every provider performs all work on a background thread. Isolate data access in a service, measure realistic queries, and move demonstrably blocking provider work off the UI thread when necessary. Update an `ObservableCollection<T>` on the UI thread; use `DispatcherQueue` only when control returns on another thread.

Create a short-lived `DbContext` for each unit of work unless your architecture deliberately manages a longer lifetime. `DbContext` isn't thread-safe.

```csharp
public async Task<List<TaskItem>> GetAllAsync()
{
    await using var db = new TaskDbContext();
    return await db.Tasks
        .AsNoTracking()
        .OrderBy(task => task.DueDate)
        .ToListAsync();
}
```

Catch exceptions at a boundary that can add useful context or present an actionable message. Don't silently convert a failed load or save into an empty or successful result.

## Plan offline synchronization

A local SQLite database can cache remote data, but synchronization is an application protocol, not an EF Core feature. Define:

- Stable record identifiers and a server version or change token.
- Which operations can be queued offline.
- How conflicts are detected and resolved.
- How authorization changes affect cached data.
- Encryption, retention, and sign-out behavior for sensitive local records.

Test interrupted synchronization, duplicate delivery, clock differences, and a user losing access while offline.

## Protect credentials and tokens

Prefer user identity, managed authentication, and short-lived tokens over stored passwords. Don't put secrets in source code or checked-in configuration. If the app must retain a credential or token, use an appropriate Windows credential-protection API and follow the identity provider's token-cache guidance.

See [Credential locker](../../develop/security/credential-locker.md) for the Windows Credential Locker API. Verify API behavior and packaging requirements for your deployment model rather than assuming credentials roam between devices.

## Related content

- [EF Core documentation](/ef/core/)
- [HTTP client](../../develop/networking/httpclient.md)
- [Data binding and MVVM](../../develop/data-binding/data-binding-and-mvvm.md)
- [Credential locker](../../develop/security/credential-locker.md)
