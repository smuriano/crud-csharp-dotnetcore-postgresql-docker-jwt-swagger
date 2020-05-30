# API's de Cadastro de Usuário com C# .Net Code, JWT, Swagger, PostgreSQL e Docker

Exemplo de API's de cadastro de usuário com C# .Net Core, PostgreSQL, autenticação por JWT e Swagger para documenta-las, também com controle de autenticação para testa-las.

Sendo executados em container Docker através do Docker-Compose.

## Database com PostgreSQL
* Utiliza a imagem Docker oficial do PostgreSQL para publicação do banco de dados
* É necessário criar uma pasta local `/PostgreSQL/db` para armazenar os arquivos físicos do banco de dados

## Backend com C# .Net Core
* Utiliza o pacote `Swashbuckle.AspNetCore@5.4.1` para documentação das API via Swagger com possibilidade para testa-las
* Utiliza o pacote `Microsoft.EntityFrameworkCore.Tools@3.1.4` e `Npgsql.EntityFrameworkCore.PostgreSQL@3.1.4` para conexão com o banco de dados PostgreSQL através do Entity Framework Core
* Utiliza o pacote `Microsoft.AspNetCore.Authentication@2.2.0` e `Microsoft.AspNetCore.Authentication.JwtBearer@3.1.4` para autenticação e autorização via JWT (Json Web Token)
* Utiliza o pacote `BCrypt.Net-Core@1.6.0` para criptografia da senha do usuário
* Utiliza o pacote `FluentValidation@8.6.2` para validação das regras de negócios das entidades do domínio
* Utiliza a imagem Docker oficial do .Net Core (alpine) para publicação das API's
    
## Executar a aplicação
* É necessario a instalação do [Docker](https://www.docker.com/products/docker-desktop).

### Docker-Compose
1. Executar `docker-compose up --force-recreate` através do terminar ou Docker Quickstart Terminal (para Windows 10 Home).
2. Navegar para `http://localhost:5000` ou `http://<container ip>:5000`
3. Após acessar o endereço, a aplicação já irá abrir na página de documentação da API's