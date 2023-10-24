# FraudDetectionCsharp - Detecção de Fraudes Bancárias 🚀 🔄 🌐

Codificação em Csharp para aplicação de Detecção de Fraudes Bancárias FraudDetectionCsharp é uma aplicação desenvolvida para detectar Fraudes Bancárias de forma robusta e prevenir fraudes bancárias em tempo real, garantindo a segurança das transações e proporcionando confiança aos usuários e prevenir fraudes bancárias em tempo real. Utilizando técnicas avançadas e regras específicas, a aplicação analisa transações e contas para identificar atividades suspeitas. Implementado com Banco de dados MS SQL e usado do Entity Framework para subir as Migrations e uso de Apache Kafka para mensageria e integrações assíncronas em fila usando o Swagger para realizar consultas nos Endpoints e Docker para subir containers para testar aplicação gravando Tópicos Personalizados por ID para gerenicar os Tópicos com Kafka Drops.


## Funcionalidades

1. **Criação de Contas**: Permite a criação de novas contas bancárias, garantindo a validação e verificação de possíveis fraudes no momento da criação.

2. **Análise de Transações**: Analisa cada transação realizada, verificando padrões suspeitos, como transações de alto valor, transações frequentes em horários incomuns e transações em múltiplos países em curtos períodos de tempo.

3. **Listagem de Contas**: Oferece uma visão geral de todas as contas registradas no sistema.

4. **Detalhes da Conta**: Permite a visualização detalhada de uma conta específica, incluindo todas as suas transações associadas.

5. **Exclusão de Contas**: Permite a exclusão de contas, garantindo que todas as transações associadas sejam tratadas adequadamente.

6 **Detecção em Tempo Real**: Descrição sobre como a detecção em tempo real é feita.

7 **Integração com Kafka**: Os tópicos são enviados e processados usando Kafka.

8 **Notificações**: Os usuários são notificados em caso de atividades suspeitas.

## Tecnologias Utilizadas

- **.NET Core 6.0**: Framework utilizado para desenvolver a aplicação.
- **Entity Framework**: ORM utilizado para interação com o banco de dados.
- **SQL Server**: Banco de dados relacional.
- **Kafka**: Utilizado para mensageria e integrações assíncronas.


### Instalação e Configuração

1. Clone o repositório:

```
git clone https://github.com/seu-usuario/FraudDetectionCsharp.git
```

2. Navegue até o diretório do projeto:

```
cd FraudDetectionCsharp
```


3. Restaure os pacotes:

```
dotnet restore
```

4. Crie o Banco de dados no Microsoft SQL
```
SeuBancoDB
```

5. Suba as Migrations isso no diretório principal da aplicação:

```
dotnet ef migrations add InitialCreate
```

```
dotnet ef database update
```

6. Suba o container do Apache Kafka e a interface do Kafka Drops do docker-compose.yml

```
docker-compose up -d
```

7. Rode o comando para rodar aplicação:

```
dotnet run
```

### Descrição do Código `AccountController`

Este código é uma implementação de um controller em ASP.NET Core que gerencia contas bancárias e detecção de fraudes. 
Vamos decompor o código e discutir os conceitos, padrões e princípios aplicados:

## Padrões de Arquitetura

### **MVC (Model-View-Controller)**
Este código segue o padrão MVC, que é comum em aplicações ASP.NET Core. O `AccountController` atua como o "Controller" no MVC. 
Ele gerencia a lógica de negócios, interage com serviços e responde às ações do usuário/cliente.

## Orientação a Objetos

### **Injeção de Dependência**
A injeção de dependência é um padrão de projeto que implementa o princípio da inversão de controle. No construtor do `AccountController`, 
vemos que `AccountService` e `FraudDetectionRules` são injetados. Isso torna o código mais modular, testável e flexível.

### **Encapsulamento**
Os campos `_accountService` e `_fraudDetectionRules` são privados, garantindo que o estado interno do `AccountController` seja protegido 
de acessos externos. Os métodos públicos expõem apenas as funcionalidades necessárias.

## SOLID

### **Princípio da Responsabilidade Única (SRP)**
O `AccountController` tem uma única responsabilidade: gerenciar requisições relacionadas a contas. Qualquer lógica adicional, como detecção 
de fraudes, é delegada a serviços especializados, como `FraudDetectionRules`.

### **Princípio da Substituição de Liskov (LSP)**
Embora não possamos ver a implementação completa dos serviços, a maneira como eles são utilizados sugere que se qualquer subclasse de 
`AccountService` ou `FraudDetectionRules` for utilizada, o comportamento do `AccountController` não deve ser afetado adversamente.

### **Princípio da Inversão de Dependência (DIP)**
O `AccountController` depende de abstrações (interfaces ou classes base) e não de implementações concretas. Isso é evidente pela injeção 
de `AccountService` e `FraudDetectionRules` no construtor.

## Adicional

### **Atributos de Rota e Ação**
Os atributos `[ApiController]`, `[Route]`, `[HttpPost]`, `[HttpGet]`, e `[HttpDelete]` são usados para definir rotas e métodos HTTP 
para os endpoints da API.

### **Autorização**
Os comentários `//[Authorize]` sugerem que a autenticação e autorização podem ser aplicadas a esses endpoints. Quando descomentados, 
eles garantirão que apenas usuários autenticados e autorizados possam acessar os métodos correspondentes.

### **Manipulação de Erros**
O código verifica explicitamente erros e condições não ideais (como contas nulas ou contas fraudulentas) e retorna respostas HTTP apropriadas.

Em resumo, este código é um exemplo bem estruturado de um controller ASP.NET Core que segue boas práticas de design, padrões de arquitetura 
e princípios SOLID.

### Faça uma Requisição Post em Account no Swagger usando o Json abaixo:

```
/api/account
```

```
{
  "accountNumber": "123181981",
  "accountHolderName": "Emerson Amorim",
  "createdDate": "2023-10-01T12:00:00Z",
  "lastModifiedDate": "2023-10-21T12:00:00Z",
  "accountType": 1,
  "balance": 5000.00,
  "isFrozen": false,
  "isBlocked": false,
  "transactions": [
    {
      "userId": 1,
      "amount": 200.00,
      "date": "2023-10-20T12:00:00Z",
      "type": 1,
      "description": "Payment for groceries",
      "isForeignTransaction": false,
      "country": "BRL",
      "isApproved": true,
      "status": 1
    },
    {
      "userId": 1,
      "amount": 50.00,
      "date": "2023-10-21T12:00:00Z",
      "type": 2,
      "description": "Online shopping",
      "isForeignTransaction": false,
      "country": "BRL",
      "isApproved": true,
      "status": 1
    }
  ],
  "userId": 1
}
```

### Faça uma Requisição Post em Payment no Swagger usando o Json abaixo:

```
/api/payment
```

```
{
  "TransactionId": 1,
  "amount": 518.00,
  "paymentDate": "2023-10-22T22:15:21.904Z",
  "paymentMethod": "Credit Card",
  "status": "Processed",
  "currency": "BRL",
  "paymentConfirmationNumber": "CONF123456"
}
```

### Faça uma Requisição Post em Transaction no Swagger usando o Json abaixo:

```
/api/transaction
```

```
{
  "userId": 456,
  "amount": 181.75,
  "date": "2023-10-23T09:58:58.283Z",
  "type": 2,
  "description": "Purchase at Amazon",
  "isForeignTransaction": true,
  "country": "BRL",
  "isApproved": true,
  "status": 1
}
```


### Descrição do Código `PaymentFraudDetectionRules`

Este código é uma implementação das regras de detecção de fraudes específicas para pagamentos. Ele é responsável por avaliar se um pagamento 
é potencialmente fraudulento com base em certos critérios predefinidos. Vamos decompor o código e discutir os conceitos, padrões e princípios 
aplicados:

## Padrões de Arquitetura

### **Domain Services**
A classe `PaymentFraudDetectionRules` atua como um serviço de domínio que encapsula a lógica de negócios específica para avaliação 
de pagamentos fraudulentos. Em arquiteturas Domain-Driven Design (DDD), isso é comum para lógicas que não se encaixam diretamente 
em uma entidade ou valor-objeto.

## Orientação a Objetos

### **Encapsulamento**
Os métodos `ValidatePayment` e `IsLargePayment` são privados, garantindo que a lógica interna de verificação seja oculta e protegida 
de acessos externos. Apenas o método público `IsFraudulentPaymentAsync` expõe a funcionalidade necessária para avaliar um pagamento.

### **Métodos**
O método `IsFraudulentPaymentAsync` combina várias verificações menores para determinar se um pagamento é fraudulento. O método 
`ValidatePayment` assegura que o pagamento não é nulo antes de qualquer processamento, enquanto `IsLargePayment` verifica se o pagamento 
excede um limite pré-definido.

## SOLID

### **Princípio da Responsabilidade Única (SRP)**
A classe `PaymentFraudDetectionRules` tem uma única responsabilidade: avaliar se um pagamento é fraudulento. Ele não se preocupa 
com armazenamento, recuperação ou qualquer outra lógica de negócios não relacionada.

### **Princípio Aberto/Fechado (OCP)**
O código foi projetado de uma forma que novas regras ou verificações podem ser adicionadas no futuro sem modificar a estrutura existente. 
Por exemplo, novos métodos privados podem ser adicionados e invocados dentro de `IsFraudulentPaymentAsync`.

### **Princípio da Segregação da Interface (ISP)**
Embora não possamos ver todas as interfaces e classes relacionadas, o design deste serviço sugere que ele implementa apenas 
as funcionalidades necessárias para detecção de fraudes em pagamentos, sem incluir funcionalidades não essenciais.


Em resumo, `PaymentFraudDetectionRules` é uma implementação bem projetada de regras de detecção de fraudes para pagamentos. 
Ele segue boas práticas de design, padrões de arquitetura e princípios SOLID, garantindo um código modular, extensível e manutenível.


### Descrição do Código `AccountService.cs`

O código da classe `AccountService`, que é responsável por gerenciar as operações relacionadas às contas. Ela é parte 
essencial do domínio da aplicação `FraudDetectionCsharp`. A seguir, uma análise detalhada do código com foco em padrões de arquitetura, 
orientação a objetos e princípios SOLID.

## Padrões de Arquitetura

### **Camada de Serviço (Service Layer)**
A classe `AccountService` representa uma camada de serviço no padrão de arquitetura em camadas. Esta camada é responsável por conter 
a lógica de negócios, coordenando as interações entre a camada de apresentação e a camada de acesso a dados.

## Orientação a Objetos

### **Encapsulamento**
A classe possui campos privados (`_accountRepository` e `_producer`) e métodos privados (`PublishAccountToKafka`) que garantem 
que os detalhes internos da classe sejam ocultados e protegidos.

### **Polimorfismo**
A classe utiliza a interface `IAccountRepository` para se comunicar com o repositório, permitindo flexibilidade na implementação 
do repositório sem alterar o serviço.

## SOLID

### **Princípio da Responsabilidade Única (SRP)**
A classe `AccountService` tem uma única responsabilidade: gerenciar operações relacionadas às contas. Ela lida com a criação, listagem 
e exclusão de contas, bem como a publicação de informações de contas no Kafka.

### **Princípio Aberto/Fechado (OCP)**
A classe está aberta para extensões e fechada para modificações. Por exemplo, novas funcionalidades ou integrações podem ser adicionadas 
sem necessidade de modificar a classe existente.

### **Princípio da Inversão de Dependência (DIP)**
A classe depende de abstrações (`IAccountRepository`) e não de implementações concretas. Isso promove um desacoplamento e facilita 
testes unitários e manutenção.

### Análise Final

`AccountService` é uma implementação que gerencia operações relacionadas a contas em um sistema de detecção de fraudes. Ela segue boas 
práticas e princípios de design de software, garantindo que o código seja modular, extensível e fácil de manter. A inclusão de mensagens 
Kafka também destaca uma arquitetura orientada a eventos, permitindo uma comunicação assíncrona e desacoplada entre diferentes partes 
do sistema.

Essa parte do código é um bom exemplo de como aplicar padrões de arquitetura, orientação a objetos e princípios SOLID em um contexto real 
de desenvolvimento.


### Descrição do Código `TransactionRepository.cs`

O código apresentado refere-se à classe `TransactionRepository`, que é responsável por gerenciar as operações de banco de dados relacionadas 
às transações. Esta classe é uma peça central na infraestrutura da aplicação `FraudDetectionCsharp`. Vamos abordar detalhadamente o código 
com foco em padrões de arquitetura, orientação a objetos e princípios SOLID.

## Padrões de Arquitetura

### **Repositório (Repository Pattern)**
A classe `TransactionRepository` implementa o padrão de repositório, que isola a lógica que recupera os dados e mapeia-os para a entidade 
do domínio. Isso permite que a camada de domínio seja agnóstica em relação às técnicas de acesso a dados.

## Orientação a Objetos

### **Encapsulamento**
A classe utiliza um campo privado (`_context`) para armazenar o contexto do banco de dados, garantindo que os detalhes internos da classe 
sejam ocultados e protegidos.

### **Polimorfismo**
A classe implementa a interface `ITransactionRepository`, permitindo que diferentes implementações possam ser usadas e testadas.

## SOLID

### **Princípio da Responsabilidade Única (SRP)**
A classe `TransactionRepository` tem uma única responsabilidade: gerenciar as operações do banco de dados relacionadas às transações. 
Isso garante que cada classe tenha apenas um motivo para mudar.

### **Princípio Aberto/Fechado (OCP)**
A classe está aberta para extensões (por exemplo, novos métodos podem ser adicionados) e fechada para modificações. Qualquer nova 
funcionalidade ou alteração nos métodos de acesso a dados pode ser feita sem alterar a classe existente.

### **Princípio da Inversão de Dependência (DIP)**
A classe `TransactionRepository` implementa a interface `ITransactionRepository`, o que significa que ela depende de abstrações e não 
de implementações concretas. Isso facilita a injeção de dependências e a realização de testes unitários.

### **Princípio da Substituição de Liskov (LSP)**
A classe, ao implementar a interface `ITransactionRepository`, garante que qualquer instância da classe `TransactionRepository` possa ser 
substituída por qualquer objeto que implemente a mesma interface.


### Análise Final

O `TransactionRepository` serve como uma ponte entre a lógica de negócios e o banco de dados, garantindo que as operações de banco de dados 
sejam realizadas de maneira eficaz e eficiente. Além disso, ao seguir os princípios SOLID e padrões de arquitetura, o código é modular, 
extensível e fácil de manter.


```
+---------------------+
|    E = mc^2          |
|                      |
|  E - Energia         |
|  m - Massa           |
|  c - Velocidade da   |
|      luz (3x10^8     |
|      m/s)            |
+---------------------+

```



## Autor:
Emerson Amorim
