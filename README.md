# Keevo - Desafio Full Stack Developer

Olá! Me chamo Samuel Faria Garcia e este é o repositório contendo meu programa, de acordo com o desafio proposto para a última estapa do processo seletivo de estágio da Keevo https://github.com/keevosoftwares/desafio-fullstack .
Não possuia vasta experiência com estas tecnologias, então foi um super desafio aprendê-las e desenvolver um programa em 13 dias. Adorei todo o processo de estudo e desenvolvimento; expandi meus conhecimentos acerca da minha área de atuação, que é o desenvolvimento full-stack. Pude aprimorar ainda mais meus conhecimentos na área.

# Configuração

## Requisitos para rodar o projeto

### Setup de ambiente:
- [Node LTS v20.11.1](https://nodejs.org/en/download)
  - Ou usando [`nvm`](https://github.com/nvm-sh/nvm)
- [Angular CLI v17.3.0](https://angular.io/guide/setup-local)
- [.Net v8.0.203](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0)
  - [EntityFramework Core v8.0.3 (abaixo mostro como baixar)](https://learn.microsoft.com/pt-br/ef/core/)
- [PostgreSQL 16.2](https://www.postgresql.org/)
- [DockerDesktop](https://www.docker.com/products/docker-desktop/)
  - [Docker-Compose](https://github.com/docker/compose)

### Como rodar em outra máquina?

- Primeiro clone o projeto `https://github.com/Samfaria2002/ToDoListKeevo.git`;
- Abra o projeto em uma ide de preferência e inicie um terminal;
- Entre no diretório ToDoListKeevo_api e dê o seguinte comando `dotnet restore`;
  - Isso irá atualizar todos os pacotes do NuGet em sua máquina.
- Dê o comando `docker-compose up`;
  - Ele irá fazer o download da imagem do Postgres e criar o container responsável por armazenar o banco de dados.
- Volte um diretório e entre na pasta ToDoListKeevo-ng e dê o seguinte comando `npm install`;
  - Isso irá baixar todos os pacotes e dependências necessários para rodar o app Angular.

### Configuração do Docker e PostgreSQL

Nessa parte, será necessário fazer uma nova configuração. Ao rodar o comando docker-compose up, o Docker irá baixar a imagem do Postgres e criar um container onde será hospedado o banco de dados. Para definir corretamente o banco, abra o arquivo `ToDoListKeevo_api/docker-compose.yaml` e preencha os campos `hostname`, `POSTGRES_USER` e `POSTGRES_PASSWORD`;
- Crie uma nova instância de server ou utilize algum já existente;
  - Caso deseja criar outro server, se atente apenas para o host, port, username e password definidos na criação do server e nome da database.
- Entre no diretório ToDoListKeevo_api > appsettings.json e appsettings.Development.json;
- Em ambos os arquivos JSON, você encontrará uma chave nomeada `PostgreConnection`, altere os campos em ambos JSON:
  - Host, port, database, username e password;
  - Todas essas configurações variam de máquina e de acordo com sua configuração do Postgre.
- Abra o terminal e entre no diretório ToDoListKeevo_api;
- Apague a pasta Migrations e insira os seguintes comandos no terminal respectivamente: `dotnet ef migrations add init` e `dotnet ef database update`.

### Inicialização do programa

Como dito antes, infelizmente tive problemas ao configurar o docker e docker-compose, então tive que executar os programas via terminal.

- Abra dois terminais e digite o comando `dotnet watch run` (ou apenas `dotnet run`) e o comando `ng serve` no outro terminal.

Caso queira acessar a documentação das rotas HTTP via Swagger, acesse `http://localhost:{porta}/swagger/index.html`
Talvez seja necessário a troca das portas, pois variam de serviço. Caso seja o cenário, ao rodar a api pelo Dotnet run, observe o terminal 
pois ele irá avisar sobre uma porta disponível, pois eu a configurei dinâmicamente, ou seja, o script irá procurar por uma porta disponível 
e hopspedará a api .net. Guarde a porta da api pois ela deve ser adicionada ao arquivo de configuração do Angular.

Vá para o diretório `ToDoListKeevo-ng/src/environment/environment.prod.ts` e troque, na url da api, a porta para a que foi gerada dinâmicamente: `mainUrlAPI: 'http://localhost:{porta}/api/tarefa'`.

Pronto, agora ambos os sistemas backend e frontend devem estar devidamente configurados e prontos para serem usados.


# Estrutura do projeto

- `./ToDoListKeevo_api`: É o diretório que armazena todo o script da API .Net
- `./ToDoListKeevo_api/Dto`: É uma das melhorias que resolvi implementar, responsável pelo desacoplamento e encapsulamento dos dados
- `./ToDoListKeevo_api/Data`: Aqui temos a implementação do padrão de projeto Repository Pattern
- `./ToDoListKeevo_api/Helpers`: Esta pasta é responsável principalmente pela paginação da API durante as fases de teste

- `./ToDoListKeevo-ng`: É o diretório que armazena todo o script do client Angular
- `./ToDoListKeevo-ng/src`: É um dos principais diretorios do Angular, onde contém os códigos-fonte da aplicação
- `./ToDoListKeevo-ng/src/app/components`: Este diretório é usado para armazenar os componentes da sua aplicação
- `./ToDoListKeevo-ng/src/app/services`: Aqui, é definido os serviços que fornecem funcionalidades compartilhadas em toda a aplicação
- `./ToDoListKeevo-ng/src/app/models`: Este diretório contém os modelos ou interfaces que definem a estrutura dos dados usados na aplicação
- `./ToDoListKeevo-ng/src/app/app.module.ts`: Esse é um componente crucial. Ele define o módulo raiz da aplicação no Angular e é responsável por importar e configurar todos os outros módulos, componentes, serviços e recursos necessários para o funcionamento da aplicação
- `./ToDoListKeevo-ng/src/main.ts`: Este é o arquivo principal onde o Angular é inicializado. Ele importa o módulo raiz (AppModule) e inicializa a aplicação usando a função

- Padrão MVC: O padrão MVC foi empregue no desenvolvimento da Api.


# Melhorias implementadas

- DTO (Data Transfer Object):  É um padrão de design usado para transferir dados entre subsistemas de um aplicativo. As vantagens dele incluem a redução do tráfego de rede, encapsulamento e desacoplamento dos dados e flexibilidade de desenvolvimetno.
- Repository Pattern: É um padrão de arquitetura de software. A interface IRepository permite uma flexibilidade de implementação de novos métodos, sem alterar o resto do código. Já a classe Repository encapsula a lógica dos dados. Ela facilida a manutenção e escalonamento do código, porque a lógica dos dados está centrada em um local apenas.
- SwaggerUI: É uma ferramenta que permite visualizar e interagir com APIs Restful, fornecendo toda uma interface gráfica e interativa; facilita a visualização da documentação e possibilita o envio de solicitações HTTP
