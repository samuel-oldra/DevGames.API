# Projeto de API usando C# e .NET 6

## DevGames - Jornada .NET Direto ao Ponto

Foi desenvolvida uma API REST completa de gerenciamento de boards, posts e comentários de uma plataforma como o Reddit.

## Tecnologias e práticas utilizadas
- ASP.NET Core com .NET 6
- Entity Framework Core
- SQL Server / In-Memory database
- Swagger
- AutoMapper
- Injeção de Dependência
- Programação Orientada a Objetos
- Padrão Repository
- Logs com Serilog
- Publicação

## Funcionalidades
- Cadastro, Listagem, Detalhes, Atualização de Board
- Cadastro, Listagem e Detalhes de um Post
- Cadastro de Comentários

###

![alt text](https://raw.githubusercontent.com/samuel-oldra/Projeto-WebApi-DotNet-6/main/README_IMGS/swagger_ui.png)

## Tool Entity Framework Core (migrations)
```
dotnet tool install --global dotnet-ef
```

## Migrations
```
dotnet ef migrations add InitialMigration -o Persistence/Migrations
dotnet ef database update
```
