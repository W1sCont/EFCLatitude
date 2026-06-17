CREATE DATABASE DapperDB;
GO
USE DapperDB;
GO

-- 1. Таблиця країн
CREATE TABLE Countries (
    CountryID INT IDENTITY(1,1) PRIMARY KEY,
    CountryName NVARCHAR(100) NOT NULL UNIQUE
);

-- 2. Таблиця міст (пов'язана з країнами)
CREATE TABLE Cities (
    CityID INT IDENTITY(1,1) PRIMARY KEY,
    CityName NVARCHAR(100) NOT NULL,
    CountryID INT NOT NULL,
    -- ПОПРАВКА: при видаленні країни, видаляються її міста
    CONSTRAINT FK_Cities_Countries FOREIGN KEY (CountryID) REFERENCES Countries(CountryID) ON DELETE CASCADE
);

-- 3. Таблиця покупців
CREATE TABLE Customers (
    CustomerID INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(150) NOT NULL,
    BirthDate DATE NOT NULL,
    Gender CHAR(1) CHECK (Gender IN ('M', 'F', 'O')), 
    Email NVARCHAR(100) NOT NULL UNIQUE,
    CityID INT NOT NULL,
    -- ПОПРАВКА: при видаленні міста, видаляються покупці з цього міста
    CONSTRAINT FK_Customers_Cities FOREIGN KEY (CityID) REFERENCES Cities(CityID) ON DELETE CASCADE
);

-- 4. Таблиця категорій (розділів) товарів
CREATE TABLE Categories (
    CategoryID INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName NVARCHAR(100) NOT NULL UNIQUE
);

-- 5. Проміжна таблиця для зв'язку Покупців та Категорій (Багато-до-багатьох)
CREATE TABLE CustomerCategories (
    CustomerID INT NOT NULL,
    CategoryID INT NOT NULL,
    PRIMARY KEY (CustomerID, CategoryID),
    CONSTRAINT FK_CustCat_Customers FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID) ON DELETE CASCADE,
    CONSTRAINT FK_CustCat_Categories FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID) ON DELETE CASCADE
);

-- 6. Таблиця товарів
CREATE TABLE Goods (
    GoodID INT IDENTITY(1,1) PRIMARY KEY,
    GoodName NVARCHAR(150) NOT NULL,
    CategoryID INT NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    -- ПОПРАВКА: при видаленні категорії, видаляються товари в ній
    CONSTRAINT FK_Goods_Categories FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID) ON DELETE CASCADE
);

-- 7. Таблиця акцій (прив'язана до товару, країни та має терміни)
CREATE TABLE Promotions (
    PromotionID INT IDENTITY(1,1) PRIMARY KEY,
    GoodID INT NOT NULL,
    CountryID INT NOT NULL,
    DiscountPercent INT CHECK (DiscountPercent BETWEEN 1 AND 100),
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    -- ПОПРАВКА: при видаленні товару або країни, акція видаляється автоматично
    CONSTRAINT FK_Promotions_Goods FOREIGN KEY (GoodID) REFERENCES Goods(GoodID) ON DELETE CASCADE,
    CONSTRAINT FK_Promotions_Countries FOREIGN KEY (CountryID) REFERENCES Countries(CountryID) ON DELETE CASCADE,
    CONSTRAINT CHK_PromoDates CHECK (EndDate >= StartDate) 
);
GO

---------------------------------------------------------------------
-- ЗАПОВНЕННЯ ДАНИМИ
---------------------------------------------------------------------

-- Заповнення країн
INSERT INTO Countries (CountryName) VALUES 
(N'Україна'), (N'Польща'), (N'Німеччина'), (N'США'), (N'Чехія');

-- Заповнення міст
INSERT INTO Cities (CityName, CountryID) VALUES 
(N'Київ', 1), (N'Варшава', 2), (N'Берлін', 3), (N'Нью-Йорк', 4), (N'Прага', 5);

-- Заповнення покупців
INSERT INTO Customers (FullName, BirthDate, Gender, Email, CityID) VALUES 
(N'Іванов Іван Іванович', '1990-05-15', 'M', 'ivanov@email.com', 1),  
(N'Anna Kowalska', '1995-09-22', 'F', 'anna.k@email.com', 2),          
(N'Hans Müller', '1988-12-01', 'M', 'hans.m@email.com', 3),            
(N'John Doe', '2000-01-10', 'M', 'johndoe@email.com', 4),              
(N'Katerina Dvořáková', '1993-07-04', 'F', 'katerina@email.com', 5);   

-- Заповнення категорій (розділів)
INSERT INTO Categories (CategoryName) VALUES 
(N'Мобільні телефони'), (N'Ноутбуки'), (N'Кухонна техніка'), (N'Аудіосистеми'), (N'Смарт-годинники');

-- Заповнення інтересів покупців
INSERT INTO CustomerCategories (CustomerID, CategoryID) VALUES 
(1, 1), (1, 2), 
(2, 3),         
(3, 2), (3, 4), 
(4, 1), (4, 5), 
(5, 3), (5, 5); 

-- Заповнення товарів
INSERT INTO Goods (GoodName, CategoryID, Price) VALUES 
(N'iPhone 15 Pro', 1, 1200.00),
(N'MacBook Air M3', 2, 1400.00),
(N'Мультиварка Philips', 3, 150.00),
(N'Навушники Sony WH-1000XM5', 4, 350.00),
(N'Apple Watch Ultra 2', 5, 800.00);

-- Заповнення акційних пропозицій
INSERT INTO Promotions (GoodID, CountryID, DiscountPercent, StartDate, EndDate) VALUES 
(1, 1, 10, '2026-06-01', '2026-06-30'), 
(2, 2, 15, '2026-06-15', '2026-07-15'), 
(3, 3, 20, '2026-07-01', '2026-07-10'), 
(4, 4, 5,  '2026-05-01', '2026-08-01'), 
(5, 5, 25, '2026-06-10', '2026-06-20'); 
GO