-- Pressione F5 --
USE master
GO
--Criar Banco de dados
CREATE DATABASE BDGT2
GO
--Selecionar Banco de Dados
USE BDGT2

--- ==================== TABELAS ==================== ---

--Criar Tabela Usuario que será usada para salvar dados de login
CREATE TABLE tblUsuario(
Usuario_Cod int NOT NULL PRIMARY KEY IDENTITY,
Usuario_Nome nvarchar(200) NOT NULL,
Usuario_Senha nvarchar(200) NOT NULL
)

--Criar Tabela Vendedor
CREATE TABLE tblVendedor(
Vendedor_Cod int NOT NULL PRIMARY KEY IDENTITY,
Vendedor_Nome nvarchar(200) NOT NULL UNIQUE
)

--Criar Tabela Clientes
CREATE TABLE tblCliente(
Cliente_Cod int NOT NULL PRIMARY KEY IDENTITY,
Cliente_Nome nvarchar(200) NOT NULL UNIQUE,
Cliente_Idade int NOT NULL,
Cliente_Cel nvarchar(11) NOT NULL,
Cliente_End nvarchar(200) NOT NULL
)

--Criar Tabela de Produtos, com referência a tabela fornecedor
CREATE TABLE tblProduto(
Produto_Cod int NOT NULL PRIMARY KEY IDENTITY,
Produto_Desc nvarchar(200) NOT NULL UNIQUE,
Produto_Marca nvarchar(200) NOT NULL,
Produto_Preco decimal(19,2) NOT NULL
)

--Criar Tabela Pedido
CREATE TABLE tblPedido(
Pedido_Cod int NOT NULL PRIMARY KEY IDENTITY,
Pedido_Cli nvarchar(200) NOT NULL,
Pedido_Vend nvarchar(200) NOT NULL
)

--Criar Tabela Itens do Pedido
CREATE TABLE tblPitens(
AddOrdem int NOT NULL IDENTITY,
Pedido_Cod int NOT NULL,
Produto_Cod int NOT NULL,
CONSTRAINT FK_Pedido_Cod FOREIGN KEY ([Pedido_Cod]) REFERENCES [tblPedido] (Pedido_Cod),
CONSTRAINT FK_Produto_Cod FOREIGN KEY ([Produto_Cod]) REFERENCES [tblProduto] (Produto_Cod)
)

--Inserir Dados de login padrão na tabela
Insert into tblUsuario 
(Usuario_Nome, Usuario_Senha)
values
('ADMIN', 'ADMIN')