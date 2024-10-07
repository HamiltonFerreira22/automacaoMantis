# Mantis Bug Tracker Test Automation

Este projeto foi desenvolvido como parte de um teste técnico para avaliar habilidades em automação de testes. O objetivo é automatizar cenários de teste que cubram funcionalidades essenciais do Mantis Bug Tracker (MantisBT), uma ferramenta de gestão de bugs para projetos de desenvolvimento de software.

## Índice

- [Descrição do Projeto](#descrição-do-projeto)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Configuração do Ambiente](#configuração-do-ambiente)
- [Como Executar os Testes](#como-executar-os-testes)
- [Cenários de Teste Automatizados](#cenários-de-teste-automatizados)
- [Boas Práticas Aplicadas](#boas-práticas-aplicadas)

## Descrição do Projeto

O projeto consiste em automatizar casos de testes para o sistema Mantis, disponível em: [http://mantis-prova.base2.com.br](http://mantis-prova.base2.com.br). Os casos de teste foram escolhidos considerando as funcionalidades mais relevantes para o contexto do software e para demonstrar boas práticas de automação.

Este projeto visa:

- Demonstrar conhecimento técnico em testes automatizados utilizando C# e Selenium WebDriver.
- Aplicar boas práticas de organização arquitetural, reuso e parametrização.
- Utilizar corretamente os recursos disponíveis do Selenium WebDriver.

## Tecnologias Utilizadas

- **C# (.NET Core 6.0)**: Linguagem de programação utilizada para escrever os testes.
- **NUnit**: Framework de testes unitários para .NET.
- **Selenium WebDriver**: Ferramenta para automação de navegadores web.
- **Selenium WebDriver ChromeDriver**: Driver para automação do navegador Google Chrome.
- **Selenium WebDriver GeckoDriver**: Driver para automação do navegador Mozilla Firefox.
- **DotNetSeleniumExtras.WaitHelpers**: Biblioteca auxiliar para condições de espera explícita no Selenium.
- **Microsoft.Extensions.Configuration**: Biblioteca para gerenciamento de configurações em aplicações .NET.

## Estrutura do Projeto

```bash
MantisTestAutomation/
├── MantisTestAutomation.Tests/
│   ├── Tests/
│   │   ├── IssueReportingTests.cs
│   │   ├── LoginTests.cs
│   │   ├── NavigationTests.cs
│   │   ├── NegativeTests.cs
│   │   └── TestBase.cs
│   ├── Utils/
│   │   └── ConfigReader.cs
│   ├── PageObjects/
│   │   └── LoginPage.cs
│   ├── MantisTestAutomation.Tests.csproj
└── README.md
```
- Tests/: Contém as classes de testes automatizados.
- Utils/: Contém classes utilitárias, como o leitor de configurações.
- PageObjects/: Implementa o padrão Page Object Model para as páginas da aplicação.
- MantisTestAutomation.Tests.csproj: Arquivo de configuração do projeto de testes.
- README.md: Este arquivo, com informações sobre o projeto.

## Configuração do Ambiente

### Pré-requisitos

- .NET Core SDK 6.0 ou superior: Para compilar e executar os testes.
- Navegador Web: Google Chrome, Mozilla Firefox ou Microsoft Edge.

### Drivers Correspondentes:

- **ChromeDriver**: Para testes no Chrome.
- **GeckoDriver**: Para testes no Firefox.
- **EdgeDriver**: Para testes no Edge.

### Instalação

Clone o repositório:

```bash
git clone https://github.com/seu-usuario/MantisTestAutomation.git
```

Navegue até o diretório do projeto:

```bash
cd MantisTestAutomation/MantisTestAutomation.Tests
```

Restaure os pacotes NuGet:

```bash
dotnet restore
```

Atualize o arquivo de configuração:
Edite o arquivo `ConfigReader.cs` na pasta `Utils/` e atualize as configurações conforme o seu ambiente:

```csharp
public static class ConfigReader
{
    public static string BaseUrl = "http://mantis-prova.base2.com.br";
    public static string Username = "seu-usuario";
    public static string Password = "sua-senha";
    public static string Browser = "Chrome"; // Ou "Firefox", "Edge"
}
```
**Importante**: Substitua "seu-usuario" e "sua-senha" pelas credenciais fornecidas para o teste.

Certifique-se de que os drivers dos navegadores estão instalados e configurados:  
Para o **ChromeDriver**, instale o pacote via NuGet:

```bash
dotnet add package Selenium.WebDriver.ChromeDriver
```

Para o **GeckoDriver** (Firefox), instale o pacote via NuGet:

```bash
dotnet add package Selenium.WebDriver.GeckoDriver
```
Para o **EdgeDriver**, você pode precisar instalar manualmente ou via NuGet.

## Como Executar os Testes

### Compilar o projeto:

```bash
dotnet build
```
### Executar todos os testes:

```bash
dotnet test
```

### Executar um teste específico:

```bash
dotnet test --filter "FullyQualifiedName~NomeDoTeste"
```
Substitua `NomeDoTeste` pelo nome completo do teste que deseja executar.

## Cenários de Teste Automatizados

Os seguintes cenários foram automatizados para cobrir funcionalidades essenciais do MantisBT:

### LoginTests.cs

- **SuccessfulLoginTest**: Verifica se um usuário pode fazer login com credenciais válidas.
- **UnsuccessfulLoginTest**: Verifica se uma mensagem de erro é exibida ao tentar fazer login com credenciais inválidas.
- **LogoutTest**: Verifica se um usuário pode fazer logout corretamente.

### NavigationTests.cs

- **SidebarLinksNavigationTest**: Verifica se os links na barra lateral estão funcionando corretamente.
- **AccessMyAccountTest**: Verifica se é possível acessar a página "Conta Pessoal".

### IssueReportingTests.cs

- **ReportIssueTest**: Verifica se um usuário pode relatar um problema com sucesso.
- **ReportIssueLimitTest**: Verifica se o sistema impede a criação de problemas quando o limite de atividades é atingido.
- **VerifyCreatedIssuesTest**: Verifica se os problemas criados são exibidos na lista de problemas.

### NegativeTests.cs

- **InvalidLoginTest**: Tenta fazer login com credenciais inválidas e verifica se a mensagem de erro é exibida.

## Boas Práticas Aplicadas

- **Organização Arquitetural**: O projeto utiliza o padrão Page Object Model (POM) para separar a lógica de interação com a interface do usuário dos testes.
- **Reuso e Parametrização**: Métodos e componentes são reutilizados sempre que possível, com parâmetros para aumentar a flexibilidade.
- **Espera Explícita**: Uso de `WebDriverWait` e condições de espera para sincronizar as interações com a aplicação.
- **Relatórios e Logs**: Os testes incluem captura de screenshots e logs para facilitar a análise de falhas.
- **Configuração Centralizada**: Uso de uma classe `ConfigReader` para gerenciar as configurações, facilitando alterações e manutenção.
- **Uso Correto do Selenium WebDriver**: Segue as melhores práticas de uso do Selenium, incluindo limpeza adequada dos recursos.

### Informações de Contato:

- **Email**: hfpj18@gmail.com
- **LinkedIn**: [Hamilton Ferreira](https://www.linkedin.com/in/hamilton-ferreira/)



