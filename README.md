# Manga Register

## Descrição do Projeto

O **Manga Register** é uma aplicação web simples de cadastro e login de usuários. O objetivo do projeto é fornecer uma interface amigável para os usuários se registrarem, efetuarem login, alterarem suas senhas e verem uma mensagem de boas-vindas ao entrarem no sistema.

## Funcionalidades

- **Tela de Cadastro:** Permite que novos usuários se cadastrem no sistema.
- **Tela de Login:** Permite que usuários existentes façam login.
- **Tela de Esqueci a Senha:** Permite que os usuários alterem suas senhas caso as esqueçam.
- **Tela de Boas-Vindas:** Mostra uma mensagem de boas-vindas ao usuário logado.
- **Validações Simples:** Impede o cadastro de e-mails duplicados e garante que todas as informações necessárias sejam fornecidas.

## Requisitos Técnicos

- **Visual Studio 2015**
- **ASP.NET Web Application utilizando .NET Framework 4.5.2**
- **Visual Basic para a codificação**
- **SQL Server 2019 para o armazenamento de dados**

## Configuração do Projeto

### 1. Configuração do Banco de Dados

Crie um banco de dados no SQL Server 2019 e configure a string de conexão no arquivo `web.config`:

```xml
<connectionStrings>
    <add name="DefaultConnection" connectionString="Data Source=.;Initial Catalog=UserDatabase;Integrated Security=True" providerName="System.Data.SqlClient" />
</connectionStrings>
