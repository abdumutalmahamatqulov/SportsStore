CREATE TABLE Categories (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    Name varchar(255),
    Description varchar(255),
	CreateDate datetime,
);

GO

CREATE TABLE Products (
    Id UNIQUEIDENTIFIER NOT NULL,
    Name varchar(255),
    Description varchar(255),
    Price decimal,
	Discount int,
	CreateDate datetime,
	CategoryId UNIQUEIDENTIFIER,
	PRIMARY KEY (Id),
	FOREIGN KEY (CategoryId) REFERENCES Categories(Id)
);

GO

CREATE TABLE Orders (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    OrderStatus varchar(255),
	CustomerName varchar(255),
	CreateDate datetime,
);

GO

CREATE TABLE OrderDetails (
    Id UNIQUEIDENTIFIER NOT NULL,
	OrderId UNIQUEIDENTIFIER,
	ProductId UNIQUEIDENTIFIER,
	Count int,
	CreateDate datetime,
	PRIMARY KEY (Id),
	FOREIGN KEY (ProductId) REFERENCES Products(Id),
	FOREIGN KEY (OrderId) REFERENCES Orders(id)
);





