# .NET 10.0 Upgrade Plan

## Execution Steps

Execute steps below sequentially one by one in the order they are listed.

1. Validate that a .NET 10.0 SDK required for this upgrade is installed on the machine and if not, help to get it installed.
2. Ensure that the SDK version specified in global.json files is compatible with the .NET 10.0 upgrade.
3. Upgrade Defra.PTS.Pet.Domain\Defra.PTS.Pet.Domain.csproj
4. Upgrade src\Defra.PTS.Pet.Repositories\Defra.PTS.Pet.Repositories.csproj
5. Upgrade src\Defra.PTS.Pet.ApiServices\Defra.PTS.Pet.ApiServices.csproj
6. Upgrade src\Defra.PTS.Pet.Functions\Defra.PTS.Pet.Functions.csproj
7. Upgrade test\Defra.PTS.Pet.Functions.Tests\Defra.PTS.Pet.Functions.Tests.csproj
8. Run unit tests to validate upgrade in the projects listed below:
   - test\Defra.PTS.Pet.Functions.Tests\Defra.PTS.Pet.Functions.Tests.csproj

## Settings

This section contains settings and data used by execution steps.

### Excluded projects

No projects are excluded from the upgrade.

### Aggregate NuGet packages modifications across all projects

NuGet packages used across all selected projects or their dependencies that need version update in projects that reference them.

| Package Name                                     | Current Version | New Version | Description                                                              |
|:-------------------------------------------------|:---------------:|:-----------:|:-------------------------------------------------------------------------|
| FluentValidation.AspNetCore                      | 11.3.0          | 11.3.1      | Deprecated, update to latest version                                     |
| Microsoft.AspNetCore.Http.Abstractions           | 2.2.0           | 2.3.9       | Deprecated, update to latest version                                     |
| Microsoft.AspNetCore.Mvc                         | 2.2.0           | 2.3.9       | Deprecated, update to latest version                                     |
| Microsoft.Azure.WebJobs.Extensions.OpenApi       | 1.0.0           |             | Remove - functionality included with framework reference                 |
| Microsoft.Azure.WebJobs.Extensions.Sql           | 3.0.534         |             | Remove - functionality included with framework reference                 |
| Microsoft.EntityFrameworkCore                    | 8.0.10          | 10.0.6      | Recommended upgrade for .NET 10.0                                        |
| Microsoft.EntityFrameworkCore.SqlServer          | 8.0.10          | 10.0.6      | Recommended upgrade for .NET 10.0                                        |
| Microsoft.EntityFrameworkCore.Tools              | 8.0.10          | 10.0.6      | Recommended upgrade for .NET 10.0                                        |
| Microsoft.NET.Sdk.Functions                      | 4.5.0           |             | Replace with Microsoft.Azure.Functions.Worker packages                   |
| Microsoft.Azure.Functions.Worker                 |                 | 2.51.0      | Replacement for Microsoft.NET.Sdk.Functions                              |
| Microsoft.Azure.Functions.Worker.Extensions.Http |                 | 3.3.0       | Replacement for Microsoft.NET.Sdk.Functions                              |
| Microsoft.Azure.Functions.Worker.Sdk             |                 | 2.0.7       | Replacement for Microsoft.NET.Sdk.Functions                              |

### Project upgrade details

#### Defra.PTS.Pet.Domain modifications

Project properties changes:
  - Target framework should be changed from `net8.0` to `net10.0`

NuGet packages changes:
  - FluentValidation.AspNetCore should be updated from `11.3.0` to `11.3.1` (*deprecated*)
  - Microsoft.AspNetCore.Mvc should be updated from `2.2.0` to `2.3.9` (*deprecated*)

#### Defra.PTS.Pet.Repositories modifications

Project properties changes:
  - Target framework should be changed from `net8.0` to `net10.0`

NuGet packages changes:
  - Microsoft.EntityFrameworkCore should be updated from `8.0.10` to `10.0.6` (*recommended for .NET 10.0*)
  - Microsoft.EntityFrameworkCore.SqlServer should be updated from `8.0.10` to `10.0.6` (*recommended for .NET 10.0*)
  - Microsoft.EntityFrameworkCore.Tools should be updated from `8.0.10` to `10.0.6` (*recommended for .NET 10.0*)

#### Defra.PTS.Pet.ApiServices modifications

Project properties changes:
  - Target framework should be changed from `net8.0` to `net10.0`

#### Defra.PTS.Pet.Functions modifications

Project properties changes:
  - Target framework should be changed from `net8.0` to `net10.0`

NuGet packages changes:
  - Microsoft.NET.Sdk.Functions should be removed (replaced by Worker packages)
  - Microsoft.Azure.Functions.Worker should be added with version `2.51.0`
  - Microsoft.Azure.Functions.Worker.Extensions.Http should be added with version `3.3.0`
  - Microsoft.Azure.Functions.Worker.Sdk should be added with version `2.0.7`
  - Microsoft.Azure.WebJobs.Extensions.OpenApi should be removed (*functionality included with framework reference*)
  - Microsoft.Azure.WebJobs.Extensions.Sql should be removed (*functionality included with framework reference*)

#### Defra.PTS.Pet.Functions.Tests modifications

Project properties changes:
  - Target framework should be changed from `net8.0` to `net10.0`

NuGet packages changes:
  - Microsoft.AspNetCore.Http.Abstractions should be updated from `2.2.0` to `2.3.9` (*deprecated*)
