IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'info_cadastrais')
BEGIN
  CREATE DATABASE info_cadastrais;
END;
GO

USE info_cadastrais;
GO

IF OBJECT_ID('clientes', 'U') IS NULL
BEGIN
  CREATE TABLE dbo.clientes
  (
    Id   INT PRIMARY KEY IDENTITY(1, 1)
  , Nome VARCHAR(100)
  , Endereco VARCHAR(100)
  );
END;