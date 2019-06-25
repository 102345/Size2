# Size - Simulação de Conta Digital
Projeto exemplo em ASP.NET Core MVC com DDD, AutoMapper , ORM Entity Framework. e com consumo de métodos de serviços em WEB API MVC Core Rest
com autentiacação via JWT

# Para Testar funcionamento do projeto , siga os seguintes passos:

1. Instalar o SDGB Ms SQL Server Express 2012.
2. Rodar os scripts que se encontram na pasta \DataBase na seguinte ordem:
a - Create_Database.sql
b - Create_Tables.sql
c - PopularBase.sql

3. Abrir o arquivo appsettings.json do projeto Size.ContaDigital.Presentation e localizar a chave API_Access e mudar o parametro 
UrlBase para a url onde roda a Web APi.
  
4. Abrir o arquivo appsettings.json do projeto Size.ContaDigital.Api e localizar a chave ConnectionStrings  e mudar os parametros BaseIdentity
e ServiceConnection para o endereço local do SGDB Sql Server.

5. Rodar primeiro o projeto Size.ContaDigital.api

6. Em seguida rodar o projeto Size.ContaDigital.Presentation

7. Para testar entre com usuario : TESTE e senha : TESTE.
8. Para outra conta de usuário entre com usuário : TESTE2  e senha TESTE2.

OBS: Para rodar os projetos desta solução, usar o Visual Studio 2017 ou versão superior.
