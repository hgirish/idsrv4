Identity Server 4

http://docs.identityserver.io/en/release/

Thanks to Getting Started tutorial by Scott Brady
https://www.scottbrady91.com/Identity-Server/Getting-Started-with-IdentityServer-4

OpenId Discovery Document http://localhost:7581/.well-known/openid-configuration


Download Quickstart UI
iex ((New-Object System.Net.WebClient).DownloadString('https://raw.githubusercontent.com/IdentityServer/IdentityServer4.Quickstart.UI/release/get.ps1'))

## Running EF migrations
dotnet ef migrations add InitialIdentityServerMigration -c PersistedGrantDbContext
dotnet ef migrations add InitialIdentityServerMigration -c ConfigurationDbContext

## Update database
dotnet ef database update -c PersistedGrantDbContext
dotnet ef database update -c ConfigurationDbContext