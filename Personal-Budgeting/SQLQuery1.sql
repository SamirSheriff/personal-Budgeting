IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'PersonalBudget')
BEGIN
    CREATE DATABASE PersonalBudget;
END
GO

USE PersonalBudget;
GO

CREATE TABLE [User](
	ID INT IDENTITY(1,1) PRIMARY KEY,
	Username VARCHAR(50) UNIQUE,
	[Password] VARCHAR(50),
	Email VARCHAR(50)
)

CREATE TABLE Income(
	incomeID INT IDENTITY(1,1) PRIMARY KEY,
	userID INT REFERENCES [User](ID),
	Amount FLOAT,
	recieveDate DATE,
	isNonRecurring BIT
)

CREATE TABLE Expense(
	expenseID INT IDENTITY(1,1) PRIMARY KEY,
	userID INT REFERENCES [User](ID),
	Category VARCHAR(50),
	Amount FLOAT,
	isRecurring BIT
)

CREATE TABLE Budget(
	budgetID INT IDENTITY(1,1) PRIMARY KEY,
	userID INT REFERENCES [User](ID),
	Limit FLOAT,
	amountSpent FLOAT,
	Category VARCHAR(50) UNIQUE
)
