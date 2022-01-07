CREATE TABLE Brands (
    Id INT NOT NULL IDENTITY,
    Name VARCHAR(MAX),
    PRIMARY KEY (Id)
);

CREATE TABLE Phones (
    Id INT NOT NULL IDENTITY,
    Type VARCHAR(MAX),
    Description VARCHAR(MAX),
    Price NUMERIC,
    Stock INT,
    BrandId INT, 
    CONSTRAINT FK_phones_brands FOREIGN KEY (BrandId) REFERENCES brands(Id)
);