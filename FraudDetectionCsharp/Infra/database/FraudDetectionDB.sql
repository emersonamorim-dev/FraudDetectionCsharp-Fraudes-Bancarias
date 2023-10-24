-- Up Migration

-- Criando a tabela Accounts
CREATE TABLE Accounts (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    AccountNumber NVARCHAR(20) NOT NULL,
    AccountHolderName NVARCHAR(MAX) NOT NULL,
    CreatedDate DATETIME2 NOT NULL,
    LastModifiedDate DATETIME2,
    AccountType INT NOT NULL,
    Balance DECIMAL(18,2) NOT NULL,
    IsFrozen BIT NOT NULL,
    IsBlocked BIT NOT NULL,
    UserId INT NOT NULL
);

-- Criando a tabela FraudReport
CREATE TABLE FraudReport (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    AccountId INT NOT NULL,
    Description NVARCHAR(MAX) NOT NULL,
    ReportedDate DATETIME2 NOT NULL,
    Status INT NOT NULL
);

-- Criando a tabela Frauds
CREATE TABLE Frauds (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    AccountId INT NOT NULL,
    Description NVARCHAR(1000) NOT NULL,
    Type INT NOT NULL,
    Status INT NOT NULL,
    DetectedDate DATETIME2 NOT NULL,
    ResolvedDate DATETIME2,
    FraudAmount DECIMAL(18,2),
    DetectedBy NVARCHAR(500) NOT NULL,
    Notes NVARCHAR(MAX) NOT NULL
);

-- Criando a tabela Payment
CREATE TABLE Payment (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    TransactionId INT NOT NULL,
    Amount DECIMAL(18,2) NOT NULL,
    PaymentDate DATETIME2 NOT NULL,
    PaymentMethod NVARCHAR(MAX) NOT NULL,
    Status NVARCHAR(MAX) NOT NULL,
    Currency NVARCHAR(MAX) NOT NULL,
    PaymentConfirmationNumber NVARCHAR(MAX) NOT NULL
);

-- Criando a tabela Transactions
CREATE TABLE Transactions (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    Amount DECIMAL(18,2) NOT NULL,
    Date DATETIME2 NOT NULL,
    Type INT NOT NULL,
    Description NVARCHAR(MAX) NOT NULL,
    IsForeignTransaction BIT NOT NULL,
    Country NVARCHAR(MAX) NOT NULL,
    IsApproved BIT NOT NULL,
    Status INT NOT NULL,
    AccountId INT FOREIGN KEY REFERENCES Accounts(Id)
);

-- Down Migration (Reverter as mudanças)

-- Deletando tabelas
DROP TABLE FraudReport;
DROP TABLE Frauds;
DROP TABLE Payment;
DROP TABLE Transactions;
DROP TABLE Accounts;
