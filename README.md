# Keevo - Desafio Full Stack Developer

Olá! Me chamo Samuel Faria Garcia e este é o repositório contendo meu programa, de acordo com o desafio proposto para a última estapa do processo seletivo de estágio da Keevo https://github.com/keevosoftwares/desafio-fullstack .
Nunca tinha mexido antes com .NET nem com Postgre, então foi um grande desafio aprender e desenvolver um programa usando essas tecnologias
em 13 dias. Gostei muito mesmo de todo o processo de estudo e desenvolvimento; expandi ainda mais meus conhecimentos acerca da minha área de atuação, que é o desenvolvimento full-stack e pude aprimorar ainda mais meus conhecimentos na a área.

# Configuração do projeto

## Requisitos para rodar o projeto

### Setup de ambiente:
- [Node LTS v20.11.1](https://nodejs.org/en/download)
  - Ou usando [`nvm`](https://github.com/nvm-sh/nvm)
- [Angular CLI v17.3.0](https://angular.io/guide/setup-local)
- [.Net v8.0.203](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0)
- [PostgreSQL 16.2](https://www.postgresql.org/)

### Como rodar em outra máquina?

- Primeiro clone o projeto `https://github.com/Samfaria2002/ToDoListKeevo.git`;
- Abra o projeto em uma ide de preferência e inicie um terminal;
- Entre no diretório ToDoListKeevo_api e dê o seguinte comando `dotnet restore`;
  - Isso irá atualizar todos os pacotes do NuGet em sua máquina.
- Volte um diretório e entre na pasta ToDoListKeevo-ng e dê o seguinte comando `npm install`;
  - Isso irá baixar todos os pacotes e dependências necessários para rodar o app Angular.

### Configuração do PostgreSQL

Nessa parte, será necessário fazer uma última configuração. Durante esse tempo de desenvolvimento, tive meu primeiro contato com Postgre, então para entregar um projeto satisfatório, realizei configurações simples.
Infelizmente não consegui usar o Docker pois estava muito problemático de configurar na minha máquina e sempre estava dando erro, então optei por usar o Postgre localmente pelo software do pgAdmin4.

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
