# PetFam

Pet Project at Kirill Sachkov course

## Additional Info

### EFCore commands

#### Add migration

```
dotnet ef migrations add Initial -s .\PetFam.Api -p .\PetFam.Infrastructure
dotnet ef migrations add Initial -s .\PetFam.Api -p .\PetFam.Infrastructure --context WriteDbContext --output-dir Migrations/WriteDb
dotnet ef migrations add Initial -s .\PetFam.Api -p .\PetFam.Infrastructure --context ReadDbContext --output-dir Migrations/ReadDb
```

#### Apply migration

```
dotnet ef database update -s .\PetFam.Api -p .\PetFam.Infrastructure
dotnet ef database update -s .\PetFam.Api -p .\PetFam.Infrastructure --context WriteDbContext
dotnet ef database update -s .\PetFam.Api -p .\PetFam.Infrastructure --context ReadDbContext
```

#### Undo migrations

```
dotnet ef database update 0 -s .\PetFam.Api -p .\PetFam.Infrastructure
```

#### Remove migration

```
dotnet ef migrations remove -s .\PetFam.Api -p .\PetFam.Infrastructure
```
