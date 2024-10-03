# PetFam

Pet Project at Kirill Sachkov course

## Additional Info

### EFCore commands

#### Add migration

```
dotnet ef migrations add Initial -s .\PetFam.Api -p .\PetFam.Infrastructure
```

#### Apply migration

```
dotnet ef database update -s .\PetFam.Api -p .\PetFam.Infrastructure
```

#### Undo migration

```
dotnet ef database update 0 -s .\PetFam.Api -p .\PetFam.Infrastructure
```

#### Remove migration

```
dotnet ef migrations remove -s .\PetFam.Api -p .\PetFam.Infrastructure
```
