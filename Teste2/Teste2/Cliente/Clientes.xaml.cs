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
    /// Interaction logic for Clientes.xaml
    /// </summary>
    public partial class Clientes : Window
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader? dr;
        public Clientes()
        {
            InitializeComponent();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["Teste2.Properties.Settings.ConnectionString"].ConnectionString.ToString();
        }

        // Cadastra o cliente e valida para não permitir duplicatas
        private void btnCadastrar_Click(object sender, RoutedEventArgs e)
        {
            if (txtNome.Text.Length == 0 || txtIdade.Text.Length == 0 || txtCelular.Text.Length == 0 || txtEndereco.Text.Length == 0)
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

            com.CommandText = "select COUNT(*) from tblCliente where Cliente_Nome = '" + txtNome.Text + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                int nomeExiste = dr.GetInt32(0);
                if (nomeExiste > 0)
                {
                    MessageBox.Show("Já existe um cliente com esse nome");
                    return;

                }
            }
            dr.Close();

            string cliNome = txtNome.Text;
            string cliIdade = txtIdade.Text;
            string cliCel = txtCelular.Text;
            string cliEnd = txtEndereco.Text;

            com.CommandText = "EXEC sp_Cadastrar_Cliente @CliNome = '" + cliNome + "'," +
                                                       " @CliIdade = '" + cliIdade + "'," +
                                                       " @CliCel = '" + cliCel + "'," +
                                                       " @CliEnd = '" + cliEnd + "'";
            dr = com.ExecuteReader();
            MessageBox.Show("Cliente cadastrado com sucesso!", "Cadastro", MessageBoxButton.OK, MessageBoxImage.Information);            
            dr.Close();

            com.CommandText = "select MAX(Cliente_Cod) from tblCliente";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                txtCodigo.Text = Convert.ToString(dr.GetInt32(0));
            }
            dr.Close();
            con.Close();
        }

        // Exclui o cliente selecionado

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            if (txtCodigo.Text.Length == 0)
            {
                MessageBox.Show("Selecione um Cliente para excluir.");
                return;
            }

            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }

            con.Open();
            com.Connection = con;

            com.CommandText = "delete from tblCliente " +
                              "where Cliente_Cod = '" + txtCodigo.Text + "'";
            dr = com.ExecuteReader();
            MessageBox.Show("Cliente excluído com sucesso!", "Exclusão", MessageBoxButton.OK, MessageBoxImage.Information);
            con.Close();

            txtCodigo.Clear();
            txtNome.Clear();
            txtIdade.Clear();
            txtCelular.Clear();
            txtEndereco.Clear();
        }

        // Edita o cliente selecionado e valida para não haver duplicatas

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (txtCodigo.Text.Length == 0)
            {
                MessageBox.Show("Selecione um Cliente para editar.");
                return;
            }
            if (txtNome.Text.Length == 0 || txtIdade.Text.Length == 0 || txtCelular.Text.Length == 0 || txtEndereco.Text.Length == 0)
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

            com.CommandText = "select COUNT(*) from tblCliente" +
                             " where Cliente_Nome = '" + txtNome.Text + "'" +
                             " and not Cliente_Cod = '" + txtCodigo.Text + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                int nomeExiste = dr.GetInt32(0);
                if (nomeExiste > 0)
                {
                    MessageBox.Show("Já existe um Cliente com esse nome");
                    return;

                }
            }
            dr.Close();

            string cliCod = txtCodigo.Text;
            string cliNome = txtNome.Text;
            string cliIdade = txtIdade.Text;
            string cliCel = txtCelular.Text;
            string cliEnd = txtEndereco.Text;

            com.CommandText = "EXEC sp_Editar_Cliente @CliCod = '" + cliCod + "'," +
                                                    " @CliNome = '" + cliNome + "'," +
                                                    " @CliIdade = '" + cliIdade + "'," +
                                                    " @CliCel = '" + cliCel + "'," +
                                                    " @CliEnd = '" + cliEnd + "'";
            dr = com.ExecuteReader();
            MessageBox.Show("Cliente editado com sucesso!", "Edição", MessageBoxButton.OK, MessageBoxImage.Information);
            con.Close();
            dr.Close();

            txtCodigo.Clear();
            txtNome.Clear();
            txtIdade.Clear();
            txtCelular.Clear();
            txtEndereco.Clear();
        }

        // Abre a janela de consultar clientes

        private void btnConsultar_Click(object sender, RoutedEventArgs e)
        {
            txtEndereco.Focus();
            Cliente.ConsultaCliente consultaCliente = new Cliente.ConsultaCliente();
            consultaCliente.ShowDialog();
        }

        // Limpa os campos
        private void btnLimpar_Click(object sender, RoutedEventArgs e)
        {
            txtCodigo.Clear();
            txtNome.Clear();
            txtIdade.Clear();
            txtCelular.Clear();
            txtEndereco.Clear();
        }

        // Atualiza os campos de acordo com o cliente selecionado

        private void txtCodigo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }

            con.Open();
            com.Connection = con;

            com.CommandText = "Select Cliente_Nome," +
                             " Cliente_Idade," +
                             " Cliente_Cel," +
                             " Cliente_End" +
                             " from tblCliente" +
                             " where Cliente_Cod = '" + txtCodigo.Text + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                string nome = dr.GetString(0);
                string idade = Convert.ToString(dr.GetInt32(1));
                string celular = dr.GetString(2);
                string endereco = dr.GetString(3);

                txtNome.Text = nome;
                txtIdade.Text = idade;
                txtCelular.Text = celular;
                txtEndereco.Text = endereco;

            }
            dr.Close();
            con.Close();
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

        // Previne que sejam inseridos espaços no campo
        private void txtInt_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space) e.Handled = true;
        }

        // Abre a janela de consultar clientes através da tecla Enter ou F2 no campo nome
        private void txtNome_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.F2)
            {
                Cliente.ConsultaCliente consultaCliente = new Cliente.ConsultaCliente();
                consultaCliente.ShowDialog();
            }
        }

        // Define o foco no campo nome ao carregar a janela
        private void CliWindow_Loaded(object sender, RoutedEventArgs e)
        {
            txtNome.Focus();
        }
    }
}
