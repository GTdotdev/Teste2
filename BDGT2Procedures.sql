-- Pressione F5 --
USE BDGT2
GO

--- ==================== PROCEDURES ==================== ---

-- Procedure para fazer o cadastro de produtos
CREATE PROCEDURE sp_Cadastrar_Produto
@ProdDesc nvarchar(200),
@ProdMarca nvarchar(200),
@ProdPV decimal(19,2)
AS
insert into tblProduto
(Produto_Desc, Produto_Marca, Produto_Preco)
values
(@ProdDesc, @ProdMarca, @ProdPV)
GO

-- Procedure para editar produtos
CREATE PROCEDURE sp_Editar_Produto
@ProdCod int, 
@ProdDesc nvarchar(200),
@ProdMarca nvarchar(200),
@ProdPV decimal(19,2)
AS
update tblProduto
set
Produto_Desc = @ProdDesc,
Produto_Marca = @ProdMarca,
Produto_Preco = @ProdPV
where
Produto_Cod = @ProdCod
GO

-- Procedure para fazer o cadastro de vendedores
CREATE PROCEDURE sp_Cadastrar_Vendedor
@VendNome nvarchar(200)
AS
insert into tblVendedor
(Vendedor_Nome)
values
(@VendNome)
GO

-- Procedure para editar vendedores
CREATE PROCEDURE sp_Editar_Vendedor
@VendCod int,
@VendNome nvarchar(200)
AS
update tblVendedor
set
Vendedor_Nome = @VendNome
where
Vendedor_Cod = @VendCod
GO

-- Procedure para fazer o cadastro de clientes
CREATE PROCEDURE sp_Cadastrar_Cliente
@CliNome nvarchar(200),
@CliIdade int,
@CliCel nvarchar(11),
@CliEnd nvarchar(200)
AS
insert into tblCliente
(Cliente_Nome, Cliente_Idade, Cliente_Cel, Cliente_End)
values
(@CliNome, @CliIdade, @CliCel, @CliEnd)
GO

-- Procedure para editar clientes
CREATE PROCEDURE sp_Editar_Cliente
@CliCod int,
@CliNome nvarchar(200),
@CliIdade int,
@CliCel nvarchar(11),
@CliEnd nvarchar(200)
AS
update tblCliente
set
Cliente_Nome = @CliNome,
Cliente_Idade = @CliIdade,
Cliente_Cel = @CliCel,
Cliente_End = @CliEnd
where
Cliente_Cod = @CliCod
GO

-- Procedure para fazer o cadastro de pedido
CREATE PROCEDURE sp_Cadastrar_Pedido
@CliNome nvarchar(200),
@VendNome nvarchar(200)
AS
insert into tblPedido
(Pedido_Cli, Pedido_Vend)
values
(@CliNome, @VendNome)
GO

-- Procedure para editar pedido
CREATE PROCEDURE sp_Editar_Pedido
@PedCod int,
@CliNome nvarchar(200),
@VendNome nvarchar(200)
AS
update tblPedido
set
Pedido_Cli = @CliNome,
Pedido_Vend = @VendNome
where
Pedido_Cod = @PedCod
GO

-- Procedure para incluir itens no pedido
CREATE PROCEDURE sp_Incluir_Item
@PedCod int,
@ProdCod int
AS
insert into tblPitens
(Pedido_Cod, Produto_Cod)
values
(@PedCod, @ProdCod)
GO
-- Procedure para editar item do pedido
CREATE PROCEDURE sp_Editar_Item
@AddOrdem int,
@ProdCod int
AS
update tblPitens
set
Produto_Cod = @ProdCod
where
AddOrdem = @AddOrdem
GO

-- Procedure para fazer a soma dos itens do pedido
CREATE PROCEDURE sp_Soma_Pedido
@PedCod int
AS
select tblPitens.Pedido_Cod AS [Codigo Pedido],
	   SUM(tblProduto.Produto_Preco) AS [Soma]
	   from tblPitens
	   inner join tblProduto on tblPitens.Produto_Cod = tblProduto.Produto_Cod
	   where tblPitens.Pedido_Cod = @PedCod
	   group by tblPitens.Pedido_Cod
GO

