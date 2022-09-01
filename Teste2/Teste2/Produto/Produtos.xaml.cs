using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Teste2
{
    /// <summary>
    /// Interaction logic for Produtos.xaml
    /// </summary>
    public partial class Produtos : Window
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader? dr;
        public Produtos()
        {
            InitializeComponent();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["Teste2.Properties.Settings.ConnectionString"].ConnectionString.ToString();
        }


        // Cadastra o produto no banco de dados através da execução de uma procedure com parâmetros;
        // Faz também uma verificação de campos vazios ou repetidos no banco de dados;
        private void btnCadastrar_Click(object sender, RoutedEventArgs e)
        {
            if (txtDescricao.Text.Length == 0 || txtMarca.Text.Length == 0 || txtPVenda.Text.Length == 0)
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

            com.CommandText = "select COUNT(*) from tblProduto where Produto_Desc = '" + txtDescricao.Text + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                int nomeExiste = dr.GetInt32(0);
                if (nomeExiste > 0)
                {
                    MessageBox.Show("Já existe um produto com essa descrição");
                    return;

                }
            }
            dr.Close();

            string prodDesc = txtDescricao.Text;
            string prodMarca = txtMarca.Text;
            string prodPV = txtPVenda.Text;


            com.CommandText = "EXEC sp_Cadastrar_Produto @ProdDesc = '" + prodDesc + "'," +
                                                       " @ProdMarca = '" + prodMarca + "'," +
                                                       " @ProdPV = '" + prodPV + "'";
            
            dr = com.ExecuteReader();
            MessageBox.Show("Produto cadastrado com sucesso!", "Cadastro", MessageBoxButton.OK, MessageBoxImage.Information);
            dr.Close();

            com.CommandText = "select MAX(Produto_Cod) from tblProduto";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                txtCodigo.Text = Convert.ToString(dr.GetInt32(0));
            }
            dr.Close();
            con.Close();
        }

        // Exclui o produto de acordo com o código selecionado
        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            if (txtCodigo.Text.Length == 0)
            {
                MessageBox.Show("Selecione um Produto para excluir.");
                return;
            }

            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }

            con.Open();
            com.Connection = con;

            com.CommandText = "delete from tblProduto " +
                              "where Produto_Cod = '" + txtCodigo.Text + "'";
            dr = com.ExecuteReader();
            MessageBox.Show("Produto excluído com sucesso!", "Exclusão", MessageBoxButton.OK, MessageBoxImage.Information);
            con.Close();

            txtCodigo.Clear();
            txtDescricao.Clear();
            txtMarca.Clear();
            txtPVenda.Clear();
        }

        // Edita o produto de acordo com o código selecionado e os textos nos campos
        // através de uma procedure com parâmetros

        // Também verifica se a descrição já existe para não permitir duplicatas
        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (txtCodigo.Text.Length == 0)
            {
                MessageBox.Show("Selecione um Produto para editar.");
                return;
            }
            if (txtDescricao.Text.Length == 0 || txtMarca.Text.Length == 0 || txtPVenda.Text.Length == 0)
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

            com.CommandText = "select COUNT(*) from tblProduto" +
                             " where Produto_Desc = '" + txtDescricao.Text + "'" +
                             " and not Produto_Cod = '" + txtCodigo.Text + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                int nomeExiste = dr.GetInt32(0);
                if (nomeExiste > 0)
                {
                    MessageBox.Show("Já existe um produto com essa descrição");
                    return;

                }
            }
            dr.Close();

            string prodCod = txtCodigo.Text;
            string prodDesc = txtDescricao.Text;
            string prodMarca = txtMarca.Text;
            string prodPV = txtPVenda.Text;

            com.CommandText = "EXEC sp_Editar_Produto @ProdCod = '" + prodCod + "'," +
                                                    " @ProdDesc = '" + prodDesc + "', " +
                                                    "@ProdMarca = '" + prodMarca + "'," +
                                                    " @ProdPV = '" + prodPV + "'";
            dr = com.ExecuteReader();
            MessageBox.Show("Produto editado com sucesso!", "Edição", MessageBoxButton.OK, MessageBoxImage.Information);
            con.Close();
            dr.Close();

            txtCodigo.Clear();
            txtDescricao.Clear();
            txtMarca.Clear();
            txtPVenda.Clear();

        }

        // Abre a janela ConsultaProduto
        private void btnConsultar_Click(object sender, RoutedEventArgs e)
        {
            txtPVenda.Focus();
            Produto.ConsultaProduto consultaProduto = new Produto.ConsultaProduto();
            consultaProduto.ShowDialog();
        }

        // Limpa os campos
        private void btnLimpar_Click(object sender, RoutedEventArgs e)
        {
            txtCodigo.Clear();
            txtDescricao.Clear();
            txtMarca.Clear();
            txtPVenda.Clear();
        }

        // Atualiza os campos de acordo com o código selecionado
        private void txtCodigo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }

            con.Open();
            com.Connection = con;

            com.CommandText = "Select Produto_Desc," +
                             " Produto_Marca," +
                             " Produto_Preco" +
                             " from tblProduto" +
                             " where Produto_Cod = '" + txtCodigo.Text + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                string desc = dr.GetString(0);
                string marca = dr.GetString(1);
                string preco = Convert.ToString(dr.GetDecimal(2), new System.Globalization.CultureInfo("en-US"));

                txtDescricao.Text = desc;
                txtMarca.Text = marca;
                txtPVenda.Text = preco;
               
            }
            dr.Close();
            con.Close();
        }

        // Lista de caractéres permitidos

        private static readonly Regex _regex = new Regex(@"^[0-9]\d*(\.\d{0,2})?$");

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
        private void txtPVenda_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        // Previne que sejam inseridos espaços no campo
        private void txtPVenda_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space) e.Handled = true;
        }

        // Abre a janela de consultar produtos através da tecla Enter ou F2 no campo descrição
        private void txtDescricao_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.F2)
            {
                Produto.ConsultaProduto consultaProduto = new Produto.ConsultaProduto();
                consultaProduto.ShowDialog();
            }
        }

        // Define o foco no campo descrição ao carregar a janela

        private void ProdWindow_Loaded(object sender, RoutedEventArgs e)
        {
            txtDescricao.Focus();
        }
    }
}
