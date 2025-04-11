To Add Migrations and Update Datbase Use This Command

dotnet ef migrations add InitialCreate --project SportHub.Data --startup-project SportHub.Api

dotnet ef database update --project SportHub.Data --startup-project SportHub.Api
