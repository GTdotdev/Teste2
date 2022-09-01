# Teste2
Software referente ao projeto Teste Prático da Séculos Sistemas

NOTA: Este é um software não executável, necessita de uma IDE para executá-lo.

São necessários para executar o software:
Visual Studio 2022
.NET 6.0
SQL Server 2014 ou mais recente com Management Studio para executar as criações

O software consiste de:

Tela de Login
Tela Principal do Sistema
Tela Clientes -- Tela Consulta Clientes
Tela Produtos -- Tela Consulta Produtos
Tela Vendedores -- Tela Consulta Vendedores
Tela Pedidos -- Tela Consulta Pedidos
3 Querys SQL para criação do banco de dados (BDGT2)

Instruções básicas:

Rodar F5 nas Querys SQL que acompanham o software para criar as tabelas, views e procedures.
Devem ser executadas na ordem: Tabelas -> Views -> Procedures.

Modificar o ConnectionString no arquivo Settings na pasta Properties para o nome de sua máquina e instância SQL,
Basta alterar o campo Data Source="" para o nome da máquina de execução e após a barra inversa o nome da instância SQL.

Credenciais padrões de login: 'ADMIN/ADMIN'.

Os menus são acessados por meio de clique.

Alguns campos de texto possuem restrição de caráctere pois só aceitam determinado tipo de dado.
Ex: Somente números e sem espaços, números com apenas duas casas decimais separadas por '.'.

A tela de consulta das janelas do sistema pode ser acessada por botão ou por meio das teclas F2 e Enter.

No menu de consulta é necessário selecionar a linha desejada e pressionar a tecla Enter para enviar a seleção.

Na tela de pedidos: 
É necessário primeiro cadastrar o pedido para depois incluir produtos.
É possível consultar cliente, vendedor e produto através das teclas Enter ou F2.
Os produtos no grid podem ser editados ou excluídos por meio da tecla enter e delete (Legendas presentes na tela).

Os códigos são automáticamente inseridos e incrementados por 1 a cada adição.
