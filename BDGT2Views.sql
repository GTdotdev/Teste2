-- Pressione F5 --
USE BDGT2
GO

--- ==================== VIEWS ==================== ---

--Cria a view para popular grid de itens do pedido
CREATE VIEW vw_gridpedido
AS
select tblPitens.Pedido_Cod AS [Codigo Pedido],
	   tblPitens.AddOrdem AS [Seq Interna],
	   tblPitens.Produto_Cod AS [Codigo Produto],
	   tblProduto.Produto_Desc AS [Descrição],
	   tblProduto.Produto_Marca AS [Marca],
	   tblProduto.Produto_Preco AS [Preço]
	   from tblPitens
	   inner join tblProduto on tblPitens.Produto_Cod = tblProduto.Produto_Cod
GO