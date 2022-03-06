IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'InfoCadastrais')
BEGIN
  CREATE DATABASE InfoCadastrais;
END;
GO

USE InfoCadastrais;
GO

IF OBJECT_ID('Clientes', 'U') IS NULL
BEGIN
  CREATE TABLE dbo.Clientes
  (
    Id   INT PRIMARY KEY IDENTITY(1, 1)
  , Nome VARCHAR(100)
  , Endereco VARCHAR(100)
  );
END;