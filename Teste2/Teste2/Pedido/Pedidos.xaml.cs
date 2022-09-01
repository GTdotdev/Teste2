using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Teste2
{
    /// <summary>
    /// Interaction logic for Pedidos.xaml
    /// </summary>
    public partial class Pedidos : Window
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader? dr;
        public Pedidos()
        {
            InitializeComponent();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["Teste2.Properties.Settings.ConnectionString"].ConnectionString.ToString();
        }

        // Cadastra o pedido e valida se existem o cliente e vendedor especificados
        private void btnCadastrar_Click(object sender, RoutedEventArgs e)
        {
            if (txtVendedor.Text.Length == 0 || txtCliente.Text.Length == 0)
            {
                MessageBox.Show("Preencha todos os campos");
                return;
            }
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }

            con.Open();
            com.Connection = con;

            com.CommandText = "select COUNT(*) from tblCliente where Cliente_Nome = '" + txtCliente.Text + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                int nomeExiste = dr.GetInt32(0);
                if (nomeExiste == 0)
                {
                    MessageBox.Show("Não existe um cliente com esse nome");
                    return;

                }
            }
            dr.Close();

            com.CommandText = "select COUNT(*) from tblVendedor where Vendedor_Nome = '" + txtVendedor.Text + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                int nomeExiste = dr.GetInt32(0);
                if (nomeExiste == 0)
                {
                    MessageBox.Show("Não existe um vendedor com esse nome");
                    return;

                }
            }
            dr.Close();

            string cliNome = txtCliente.Text;
            string vendNome = txtVendedor.Text;

            com.CommandText = "EXEC sp_Cadastrar_Pedido @CliNome = '" + cliNome + "'," +
                                                       " @VendNome = '" + vendNome + "'";
            dr = com.ExecuteReader();
            MessageBox.Show("Pedido cadastrado com sucesso!", "Cadastro", MessageBoxButton.OK, MessageBoxImage.Information);
            dr.Close();

            com.CommandText = "select MAX(Pedido_Cod) from tblPedido";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                txtNumPedido.Text = Convert.ToString(dr.GetInt32(0));
            }
            dr.Close();
            con.Close();
        }
        // Exclui o pedido e os itens relacionados a ele
        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            if (txtNumPedido.Text.Length == 0)
            {
                MessageBox.Show("Selecione um Pedido para excluir.");
                return;
            }

            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }

            con.Open();
            com.Connection = con;

            com.CommandText = "delete from tblPitens " +
                              "where Pedido_Cod = '" + txtNumPedido.Text + "'";
            dr = com.ExecuteReader();
            dr.Close();

            com.CommandText = "delete from tblPedido " +
                              "where Pedido_Cod = '" + txtNumPedido.Text + "'";
            dr = com.ExecuteReader();

            MessageBox.Show("Pedido excluído com sucesso!", "Exclusão", MessageBoxButton.OK, MessageBoxImage.Information);
            dr.Close();
            con.Close();

            txtNumPedido.Clear();
            txtCliente.Clear();
            txtVendedor.Clear();
            txtProduto.Clear();
            txtDescProduto.Clear();
            txtPVenda.Clear();
        }
        // Edita o pedido e valida se existem o cliente e vendedor especificados

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (txtNumPedido.Text.Length == 0)
            {
                MessageBox.Show("Selecione um Pedido para editar.");
                return;
            }
            if (txtCliente.Text.Length == 0 || txtVendedor.Text.Length == 0)
            {
                MessageBox.Show("Preencha todos os campos");
                return;
            }

            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }

            con.Open();
            com.Connection = con;

            com.CommandText = "select COUNT(*) from tblCliente where Cliente_Nome = '" + txtCliente.Text + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                int nomeExiste = dr.GetInt32(0);
                if (nomeExiste == 0)
                {
                    MessageBox.Show("Não existe um cliente com esse nome");
                    return;

                }
            }
            dr.Close();

            com.CommandText = "select COUNT(*) from tblVendedor where Vendedor_Nome = '" + txtVendedor.Text + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                int nomeExiste = dr.GetInt32(0);
                if (nomeExiste == 0)
                {
                    MessageBox.Show("Não existe um vendedor com esse nome");
                    return;

                }
            }
            dr.Close();

            string pedCod = txtNumPedido.Text;
            string cliNome = txtCliente.Text;
            string vendNome = txtVendedor.Text;

            com.CommandText = "EXEC sp_Editar_Pedido @PedCod = '" + pedCod + "'," +
                                                    " @CliNome = '" + cliNome + "', " +
                                                    "@VendNome = '" + vendNome + "'";
            dr = com.ExecuteReader();
            MessageBox.Show("Pedido editado com sucesso!", "Edição", MessageBoxButton.OK, MessageBoxImage.Information);
            con.Close();
            dr.Close();
        }

        // Abre a janela de Consulta de Pedidos
        private void btnConsultar_Click(object sender, RoutedEventArgs e)
        {
            txtVendedor.Focus();
            Pedido.ConsultaPedido consultaPedido = new Pedido.ConsultaPedido();
            consultaPedido.ShowDialog();
        }

        // Atualiza os campos de acordo com o pedido selecionado na consulta

        private void txtNumPedido_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            if (txtNumPedido.Text.Length == 0)
            {
                txtProduto.IsEnabled = false;
                lblSoma.Content = "";
            }

            con.Open();
            com.Connection = con;

            com.CommandText = "Select Pedido_Cli," +
                             " Pedido_Vend" +
                             " from tblPedido" +
                             " where Pedido_Cod = '" + txtNumPedido.Text + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                string cliNome = dr.GetString(0);
                string vendNome = dr.GetString(1);
               

                txtCliente.Text = cliNome;
                txtVendedor.Text = vendNome;
                txtProduto.IsEnabled = true;
            }
            dr.Close();

            com.CommandText = "EXEC sp_Soma_Pedido @PedCod = '" + txtNumPedido.Text + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                string soma = Convert.ToString(dr.GetDecimal(1));
                lblSoma.Content = $"Valor Total: \n {soma}";
            }
            dr.Close();

            com.CommandText = "select * from vw_gridpedido " +
                              "where [Codigo Pedido] = '" + txtNumPedido.Text + "'";
            DataTable dt = new DataTable("tblPitens");
            SqlDataAdapter dataAdp = new SqlDataAdapter(com);
            dataAdp.Fill(dt);
            GridPedido.ItemsSource = dt.DefaultView;
            dataAdp.Update(dt);
            dr.Close();
            
            con.Close();
        }

        // Inclui o produto selecionado no pedido, faz a validação se o mesmo existe e atualiza o grid
        private void btnIncluir_Click(object sender, RoutedEventArgs e)
        {
            if (txtProduto.Text.Length == 0 || txtDescProduto.Text.Length == 0 || txtPVenda.Text.Length == 0)
            {
                MessageBox.Show("Preencha todos os campos");
                return;
            }
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }

            con.Open();
            com.Connection = con;

            com.CommandText = "select COUNT(*) from tblProduto where Produto_Cod = '" + txtProduto.Text + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                int nomeExiste = dr.GetInt32(0);
                if (nomeExiste == 0)
                {
                    MessageBox.Show("Não existe um produto com esse código");
                    return;

                }
            }
            dr.Close();

            string pedCod = txtNumPedido.Text;
            string prodCod = txtProduto.Text;

            com.CommandText = "EXEC sp_Incluir_Item @PedCod = '" + pedCod + "'," +
                                                  " @ProdCod = '" + prodCod + "'";
            dr = com.ExecuteReader();
            dr.Close();

            com.CommandText = "select * from vw_gridpedido " +
                              "where [Codigo Pedido] = '" + txtNumPedido.Text + "'";
            DataTable dt = new DataTable("tblPitens");
            SqlDataAdapter dataAdp = new SqlDataAdapter(com);
            dataAdp.Fill(dt);
            GridPedido.ItemsSource = dt.DefaultView;
            dataAdp.Update(dt);
            dr.Close();

            com.CommandText = "EXEC sp_Soma_Pedido @PedCod = '" + txtNumPedido.Text + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                string soma = Convert.ToString(dr.GetDecimal(1));
                lblSoma.Content = $"Valor Total: \n {soma}";
            }
            dr.Close();
            con.Close();
        }

        // Preenche os campos descrição e preço de venda de acordo com o produto selecionado

        private void txtProduto_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }

            con.Open();
            com.Connection = con;

            com.CommandText = "Select Produto_Desc," +
                             " Produto_Preco" +
                             " from tblProduto" +
                             " where Produto_Cod = '" + txtProduto.Text + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                string prodDesc = dr.GetString(0);
                string prodPvenda = Convert.ToString(dr.GetDecimal(1));


                txtDescProduto.Text = prodDesc;
                txtPVenda.Text = prodPvenda;
            }
            else
            {
                txtDescProduto.Clear();
                txtPVenda.Clear();
            }
            dr.Close();
            con.Close();
        }

        // Limpa os campos
        private void btnLimpar_Click(object sender, RoutedEventArgs e)
        {
            txtNumPedido.Clear();
            txtCliente.Clear();
            txtVendedor.Clear();
            txtProduto.Clear();
            txtDescProduto.Clear();
            txtPVenda.Clear();
        }

        // Abre a janela de consulta vendedores ao pressionar Enter ou F2

        private void txtVendedor_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.F2)
            {
                Vendedor.ConsultaVendedor consultaVendedor = new Vendedor.ConsultaVendedor();
                consultaVendedor.ShowDialog();
            }
        }

        // Abre a janela de consulta clientes ao pressionar Enter ou F2

        private void txtCliente_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.F2)
            {
                Cliente.ConsultaCliente consultaCliente = new Cliente.ConsultaCliente();
                consultaCliente.ShowDialog();
            }
        }

        // Abre a janela de consulta produtos ao pressionar Enter ou F2 e previne que sejam inseridos espaços no campo

        private void txtProduto_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space) e.Handled = true;

            if (e.Key == Key.Enter || e.Key == Key.F2)
            {
                Produto.ConsultaProduto consultaProduto = new Produto.ConsultaProduto();
                consultaProduto.ShowDialog();
            }
        }

        // Lista de caractéres permitidos

        private static readonly Regex _regex = new Regex(@"^[0-9]\d*?$");

        // Verifica se o caractére inserido está na lista
        private static bool VerificaTexto(string textoInserido)
        {
            return _regex.IsMatch(textoInserido);
        }

        // Método para permitir o texto a ser inserido se a verificação for verdadeira, se não remove

        private bool PermiteTexto(TextBox? tb, string textoInserido)
        {
            bool permitido = true;
            if (tb != null)
            {
                string texto = tb.Text;
                if (!string.IsNullOrEmpty(tb.SelectedText))
                    texto = texto.Remove(tb.CaretIndex, tb.SelectedText.Length);
                permitido = VerificaTexto(texto.Insert(tb.CaretIndex, textoInserido));
            }
            return permitido;
        }

        // Passa o texto pelo método PermiteTexto
        private void txtInt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !PermiteTexto(sender as TextBox, e.Text);
        }

        // Previne que seja colado texto inválido no campo
        private void ColarTexto(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string texto = (string)e.DataObject.GetData(typeof(string));
                if (!PermiteTexto(sender as TextBox, texto))
                    e.CancelCommand();
            }
            else
                e.CancelCommand();
        }

        // Foca no campo vendedor ao abrir a janela pedidos

        private void PedWindow_Loaded(object sender, RoutedEventArgs e)
        {
            txtVendedor.Focus();
        }

        // Construtor do item para armazenar o campo addordem

        public class Item
        {
            public string? addOrdem { get; set; }
        }

        // Métodos para excluir e editar produtos do pedido no grid atráves da tecla Delete e Enter
        private void GridPedido_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (MessageBox.Show("Deseja excluir esse item?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.No)
                {
                    return;
                }
                
                foreach (DataRowView row in GridPedido.SelectedItems)
                {
                    Item produto = new Item();
                    produto.addOrdem = row.Row.ItemArray[1].ToString();

                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        con.Close();
                    }

                    con.Open();
                    com.Connection = con;

                    com.CommandText = "delete from tblPitens " +
                                      "where AddOrdem = '" + produto.addOrdem + "'";
                    dr = com.ExecuteReader();
                    dr.Close();                    
                }

                com.CommandText = "select * from vw_gridpedido " +
                                      "where [Codigo Pedido] = '" + txtNumPedido.Text + "'";
                DataTable dt = new DataTable("tblPitens");
                SqlDataAdapter dataAdp = new SqlDataAdapter(com);
                dataAdp.Fill(dt);
                GridPedido.ItemsSource = dt.DefaultView;
                dataAdp.Update(dt);

                com.CommandText = "EXEC sp_Soma_Pedido @PedCod = '" + txtNumPedido.Text + "'";
                dr = com.ExecuteReader();
                if (dr.Read())
                {
                    string soma = Convert.ToString(dr.GetDecimal(1));
                    lblSoma.Content = $"Valor Total: \n {soma}";
                }
                dr.Close();

                con.Close();

            }
            if (e.Key == Key.Enter)
            {
                if (txtProduto.Text.Length == 0)
                {
                    MessageBox.Show("Digite um código de produto para editar");
                    e.Handled = true;
                    return;
                }

                if (MessageBox.Show("Deseja editar esse item?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.No)
                {
                    return;
                }
                con.Open();
                com.Connection = con;

                com.CommandText = "select COUNT(*) from tblProduto where Produto_Cod = '" + txtProduto.Text + "'";
                dr = com.ExecuteReader();
                if (dr.Read())
                {
                    int nomeExiste = dr.GetInt32(0);
                    if (nomeExiste == 0)
                    {
                        MessageBox.Show("Não existe um produto com esse código");
                        return;

                    }
                }
                dr.Close();

                string prodCod = txtProduto.Text;

                foreach (DataRowView row in GridPedido.SelectedItems)
                {
                    Item produto = new Item();
                    produto.addOrdem = row.Row.ItemArray[1].ToString();

                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        con.Close();
                    }

                    con.Open();
                    com.Connection = con;

                    com.CommandText = "EXEC sp_Editar_Item @AddOrdem = '" + produto.addOrdem + "'," +
                                                  " @ProdCod = '" + prodCod + "'";
                    dr = com.ExecuteReader();
                    dr.Close();
                }

                com.CommandText = "select * from vw_gridpedido " +
                                      "where [Codigo Pedido] = '" + txtNumPedido.Text + "'";
                DataTable dt = new DataTable("tblPitens");
                SqlDataAdapter dataAdp = new SqlDataAdapter(com);
                dataAdp.Fill(dt);
                GridPedido.ItemsSource = dt.DefaultView;
                dataAdp.Update(dt);

                com.CommandText = "EXEC sp_Soma_Pedido @PedCod = '" + txtNumPedido.Text + "'";
                dr = com.ExecuteReader();
                if (dr.Read())
                {
                    string soma = Convert.ToString(dr.GetDecimal(1));
                    lblSoma.Content = $"Valor Total: \n {soma}";
                }
                dr.Close();

                con.Close();
            }
        }

        // Preenche o cabeçalho do grid assim que a janela pedido for aberta

        private void GridPedido_Loaded(object sender, RoutedEventArgs e)
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            com.Connection = con;

            com.CommandText = "select * from vw_gridpedido " +
                      "where [Codigo Pedido] = '" + txtNumPedido.Text + "'";
            DataTable dt = new DataTable("tblPitens");
            SqlDataAdapter dataAdp = new SqlDataAdapter(com);
            dataAdp.Fill(dt);
            GridPedido.ItemsSource = dt.DefaultView;
            dataAdp.Update(dt);

            con.Close();
        }
    }
}