# Keevo - Desafio Full Stack Developer

Olá! Me chamo Samuel Faria Garcia e este é o repositório contendo meu programa, de acordo com o desafio proposto para a última estapa do processo seletivo de estágio da Keevo https://github.com/keevosoftwares/desafio-fullstack .

# Entendendo decisões arquiteturais e a estrutura do projeto

## Requisitos para rodar o projeto

### Setup de ambiente:
- [Node LTS v20.11.1](https://nodejs.org/en/download)
  - Ou usando [`nvm`](https://github.com/nvm-sh/nvm)
- [Angular CLI v17.3.0](https://angular.io/guide/setup-local)
- [.Net v8.0.203](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0)
- [PostgreSQL 16.2](https://www.postgresql.org/)

### Como rodar em outra máquina?

- Primeiro clone o projeto `https://github.com/Samfaria2002/ToDoListKeevo.git`
- Abra o projeto em uma ide de preferência e inicie um terminal
- Entre no diretório ToDoListKeevo_api e dê o seguinte comando `dotnet restore`
  - Isso irá atualizar todos os pacotes do NuGet em sua máquina
- Volte um diretório e entre na pasta ToDoListKeevo-ng e dê o seguinte comando `npm install`
  - Isso irá baixar todos os pacotes e dependências necessários para rodar o app Angular

### Configuração do PostgreSQL

Nessa parte, será necessário fazer uma última configuração. Durante esse tempo de desenvolvimento, tive meu primeiro contato com Postgre, então para entregar um projeto satisfatório, realizei configurações simples.
Infelizmente não consegui usar o Docker pois estava muito problemático de configurar na minha máquina e sempre estava dando erro, então optei por usar o Postgre localmente pelo software do pgAdmin4.

- Entre no diretório 
