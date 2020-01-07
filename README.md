# AdmCondAPI
API do sistema de administração condominial

O seu propósito é fornecer funcionalidades a serem utilizadas pela interface do sistema, AdmCond, bem como, permitir a integração com outros sistemas. Esta API foi desenvolvida em ASP.Net Core 3.1 Web API e utiliza um banco de dados SQL Server. Os scripts para a criação do banco encontram-se no diretório SqlServerDBScript. A versão utilizada para a criação do mesmo foi Microsoft SQL Server 2017. O comunicado criado pela aplicação pode ser visualizado no Visualizador de Eventos do Windows (Event Viewer), na pasta Logs do Windows/Aplicativos. Na lista de eventos exibida, procure os que tem a fonte (source) de nome .Net Runtime.

Para executar a API, siga os passos abaixo:
1) Baixe o projeto para o seu computador
2) Abra o diretório do projeto, procure pelo diretório SqlServerDBScript e abra-o.
3) Abra o arquivo de scripts de criação do banco de dados, AlexandreMMunizAdmCond_DB.sql, no Microsoft SQL Server Management Studio.
4) No início do arquivo, há o comando CREATE DATABASE e nele se encontra a configuração do diretório de armazenamento dos 
bancos de dados do SQL Server. Altere esta configuração apropriadamente de acordo com a instalação do seu SQL Server.
5) Execute o script do arquivo. No final, o banco de dados deverá estar completamente criado e pronto para uso.
6) Abra o projeto da API no Microsoft Visual Studio 2019.
7) Abra o arquivo appsettings.Development.json e mude os parâmetros da string de conexão com o banco de dados para ficar 
de acordo com o seu servidor de banco de dados.
8) No Visual Studio, na lista de seleção Debug/Release, verifique se a opção Debug está selecionada. Caso contrário, selecione-a.
9) Execute o aplicativo, sem utilizar debug, utilizando Start Without Debugging, atalho Ctrl+F5.
10) Pronto! A página do Swagger, de documentação, se abrirá e a API estará pronta para uso.

Arquitetura

A API REST acessa um banco de dados Microsoft SQL Server, para gravar e obter informações, utilizando o Entity Framework Core 3.1.
Apenas o formato de objetos JSON é aceito nas operações. Estas últimas, representam a camada de negócios do sistema. 
A camada de dados é representada pelo contexto do EF Core. Todas as operações possuem tipos próprios específicos (classes) 
de entrada e saída de dados. O propósito disso é ocultar, o modelo de dados do banco, de quem consome a API, como também, 
permitir que somente as informações necessárias trafeguem pela rede. Quem consome o serviço tem o benefício de lidar 
com um modelo de dados específico, mais fácil de trabalhar.
