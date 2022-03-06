IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'info_cadastro')
BEGIN
  CREATE DATABASE info_cadastro;
END;
GO