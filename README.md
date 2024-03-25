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

### Como rodar em outro computador?

- Primeiro clone o projeto `https://github.com/Samfaria2002/ToDoListKeevo.git`;
- Abra o projeto em uma ide de sua preferência e inicie um terminal;
- Entre no diretório ToDoListKeevo_api e dê o seguinte comando `dotnet restore`;
  - Isso irá atualizar todos os pacotes do NuGet em sua máquina.
- Dê o comando `docker-compose up`;
  - Ele irá fazer o download da imagem do Postgres e criar o container responsável por armazenar o banco de dados.
- Volte um diretório e entre na pasta ToDoListKeevo-ng e dê o seguinte comando `npm install`;
  - Isso irá baixar todos os pacotes e dependências necessários para rodar o app Angular.

### Configuração do Docker e PostgreSQL

Nessa parte, será necessário fazer uma nova configuração. Ao rodar o comando `docker-compose up`, o Docker irá baixar a imagem do Postgres e criar um container onde será hospedado o banco de dados. Para definir corretamente o banco, abra o arquivo `ToDoListKeevo_api/docker-compose.yaml` e preencha os campos de acordo com a configuração do seu Docker Desktop: 
- `hostname`;
- `POSTGRES_USER`;
- `POSTGRES_PASSWORD`.

Entre no diretório `ToDoListKeevo_api/appsettings.json` e `appsettings.Development.json`. Em ambos os arquivos JSON, você encontrará uma chave nomeada de `PostgreConnection`, altere os campos em ambos JSON de acordo com a configuração feita no `docker-compose.yaml`.
Para realizar a conexão do banco de dados PostgreSQL com o container do Docker, recomendo a utilização de alguma interface para administração de banco de dados. Caso sua ide seja o Visual Studio Code, recomendo a extensão Database Client `https://database-client.com/#/home`.
Agora, é necessário ligar o container para podermos dar prosseguimento a configuração do banco. Abra o terminal e insira o comando `docker container start postgres-container`.

Para configurar, estabeleça uma nova conexão passando nos campos os mesmos parâmetros que definimos no `docker-compose.yaml` e na chave `PostgreConnection`, adicione também a porta que o container foi hospedada. Para este projeto, hospeedei o container nas portas 5433:5432, caso tenha problemas com a configuração de portas, vá até o `docker-compose.yaml` e altere o campo `ports` para a porta que você deseja ou alguma outra disponível, lembre-se de apagar o container e criar um novo através do `docker-compose up`. Caso a interface de conexão do banco mostre vários tipos de bancos de dados, selecione o PostgreSQL. 
Seguindo, volte para o terninal e entre no diretório `ToDoListKeevo_api`, em seguida insira o comando `dotnet ef database update`. Esse comando irá recriar o modelo do banco de dados e atualizar-lo na nova conexão.

### Inicialização do programa

- Abra dois terminais e digite o comando `dotnet watch run` (ou apenas `dotnet run`) e o comando `ng serve` no outro terminal.

Caso queira acessar a documentação das rotas HTTP via Swagger, acesse `http://localhost:{porta_da_api}/swagger/index.html`.

Será necessário trocar a porta da Api nos arquivos do Angular, pois variam de serviço. Ao rodar o script dotnet pelo `dotnet watch run`, observe o terminal 
pois ele retornará mensagem indicando uma porta disponível que foi configurada dinâmicamente e hopspedará a api dotnet. Vá para o diretório `ToDoListKeevo-ng/src/environment/environment.prod.ts` e troque, na url da api, a porta pela a que foi gerada dinâmicamente, retornada no terminal: `mainUrlAPI: 'http://localhost:{porta_da_api}/api/tarefa'`.

Pronto, agora ambos os sistemas backend e frontend devem estar devidamente configurados e prontos para serem usados.


# Estrutura do projeto

- `./ToDoListKeevo_api/Models/Tarefa.cs`: Essa é minha classe de abstração, responsável por representar as tarefas que serão gerenciadas pelo sistema de lista de tarefas. Os atributos foram definidos como id, nome, status ("EmAndamento", "Pendente" ou "Concluida"), tipo ("Estudo", "Trabalho", "Lazer", "Compras" ou "Pessoal"), prioridade ("Alta, "Media" ou "Baixa") e prazo. Caso uma tarefa seja definida para um dia anterior ao dia atual, ela automaticamente receberá o status de "Concluida";
- `./ToDoListKeevo_api`: É o diretório que armazena todo o script da API .Net;
- `./ToDoListKeevo_api/Dto`: É uma das melhorias que resolvi implementar, responsável pelo desacoplamento e encapsulamento dos dados;
- `./ToDoListKeevo_api/Data`: Aqui temos a implementação do padrão de projeto Repository Pattern;
- `./ToDoListKeevo_api/Helpers`: Esta pasta é responsável principalmente pela paginação da API durante as fases de teste;
- `./ToDoListKeevo_api/docker-compose.yaml`: Este arquivo define um serviço Docker que executa um contêiner PostgreSQL com configurações específicas de usuário, senha e mapeamento de porta;
- `./ToDoListKeevo_api/Startup.cs`: O Startup.cs é uma parte crucial dessa aplicação, pois é onde a aplicação é inicializada e configurada. Ele define como os diferentes componentes da aplicação se comunicam e interagem entre si.
- `./ToDoListKeevo_api/Program.cs`: A classe Program.cs é o o ponto de entrada responsável pela inicialização e configuração do host da aplicação.

- `./ToDoListKeevo-ng`: É o diretório que armazena todo o script do client Angular;
- `./ToDoListKeevo-ng/src`: É um dos principais diretorios do Angular, onde contém os códigos-fonte da aplicação;
- `./ToDoListKeevo-ng/src/app/components`: Este diretório é usado para armazenar os componentes da sua aplicação;
- `./ToDoListKeevo-ng/src/app/services`: Aqui, é definido os serviços que fornecem funcionalidades compartilhadas em toda a aplicação;
- `./ToDoListKeevo-ng/src/app/models`: Este diretório contém os modelos ou interfaces que definem a estrutura dos dados usados na aplicação;
- `./ToDoListKeevo-ng/src/app/app.module.ts`: Esse é um componente crucial. Ele define o módulo raiz da aplicação no Angular e é responsável por importar e configurar todos os outros módulos, componentes, serviços e recursos necessários para o funcionamento da aplicação;
- `./ToDoListKeevo-ng/src/main.ts`: Este é o arquivo principal onde o Angular é inicializado. Ele importa o módulo raiz (AppModule) e inicializa a aplicação usando a função.

- Padrão MVC: O padrão MVC foi empregue no desenvolvimento de todo o programa. Responsável por separar a aplicação em três componentes: o Model (modelo) para os dados, a View (visualização) para a interface do usuário e o Controller (controlador) para gerenciar as interações entre o modelo e a visualização.


# Melhorias implementadas

- DTO (Data Transfer Object):  É um padrão de design usado para transferir dados entre subsistemas de um aplicativo. As vantagens dele incluem a redução do tráfego de rede, encapsulamento e desacoplamento dos dados e flexibilidade de desenvolvimetno;
- Repository Pattern: É um padrão de arquitetura de software. A interface IRepository permite uma flexibilidade de implementação de novos métodos, sem alterar o resto do código. Já a classe Repository encapsula a lógica dos dados. Ela facilida a manutenção e escalonamento do código, porque a lógica dos dados está centrada em um local apenas;
- SwaggerUI: É uma ferramenta que permite visualizar e interagir com APIs Restful, fornecendo toda uma interface gráfica e interativa; facilita a visualização da documentação e possibilita o envio de solicitações HTTP;
- Docker-Compose: O Docker Compose permite definir toda a configuração em um único arquivo YAML, incluindo versões de imagens, variáveis de ambiente, volumes, redes, etc. Isso simplifica bastante a configuração e a portabilidade em diferentes computadores.
