using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Teste2
{
    /// <summary>
    /// Interaction logic for Vendedores.xaml
    /// </summary>
    public partial class Vendedores : Window
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader? dr;
        public Vendedores()
        {
            InitializeComponent();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["Teste2.Properties.Settings.ConnectionString"].ConnectionString.ToString();
        }

        // Cadastra o vendedor e verifica se já existe para não permitir duplicatas
        private void btnCadastrar_Click(object sender, RoutedEventArgs e)
        {
            if (txtNome.Text.Length == 0)
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

            com.CommandText = "select COUNT(*) from tblVendedor where Vendedor_Nome = '" + txtNome.Text + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                int nomeExiste = dr.GetInt32(0);
                if (nomeExiste > 0)
                {
                    MessageBox.Show("Já existe um vendedor com esse nome");
                    return;

                }
            }
            dr.Close();

            string vendNome = txtNome.Text;

            com.CommandText = "EXEC sp_Cadastrar_Vendedor @VendNome = '" + vendNome + "'";

            dr = com.ExecuteReader();
            MessageBox.Show("Vendedor cadastrado com sucesso!", "Cadastro", MessageBoxButton.OK, MessageBoxImage.Information);            
            dr.Close();

            com.CommandText = "select MAX(Vendedor_Cod) from tblVendedor";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                txtCodigo.Text = Convert.ToString(dr.GetInt32(0));
            }
            dr.Close();
            con.Close();
        }

        // Exclui o vendedor selecionado

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            if (txtCodigo.Text.Length == 0)
            {
                MessageBox.Show("Selecione um Vendedor para excluir.");
                return;
            }

            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }

            con.Open();
            com.Connection = con;

            com.CommandText = "delete from tblVendedor " +
                              "where Vendedor_Cod = '" + txtCodigo.Text + "'";
            dr = com.ExecuteReader();
            MessageBox.Show("Vendedor excluído com sucesso!", "Exclusão", MessageBoxButton.OK, MessageBoxImage.Information);
            con.Close();

            txtCodigo.Clear();
            txtNome.Clear();
        }

        // Edita o vendedor selecionado e valida para não haver duplicatas
        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (txtCodigo.Text.Length == 0)
            {
                MessageBox.Show("Selecione um Vendedor para editar.");
                return;
            }

            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }

            con.Open();
            com.Connection = con;

            com.CommandText = "select COUNT(*) from tblVendedor" +
                             " where Vendedor_Nome = '" + txtNome.Text + "'" +
                             " and not Vendedor_Cod = '" + txtCodigo.Text + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                int nomeExiste = dr.GetInt32(0);
                if (nomeExiste > 0)
                {
                    MessageBox.Show("Já existe um vendedor com esse nome");
                    return;

                }
            }
            dr.Close();

            string vendCod = txtCodigo.Text;
            string vendNome = txtNome.Text;

            com.CommandText = "EXEC sp_Editar_Vendedor @VendCod = '" + vendCod + "'," +
                                                    " @VendNome = '" + vendNome + "'";
            dr = com.ExecuteReader();
            MessageBox.Show("Vendedor editado com sucesso!", "Edição", MessageBoxButton.OK, MessageBoxImage.Information);
            con.Close();
            dr.Close();

            txtCodigo.Clear();
            txtNome.Clear();
        }

        // Abre a janela de consulta vendedores
        private void btnConsultar_Click(object sender, RoutedEventArgs e)
        {
            txtNome.Focus();
            Vendedor.ConsultaVendedor consultaVendedor = new Vendedor.ConsultaVendedor();
            consultaVendedor.ShowDialog();
        }

        // Limpa os campos
        private void btnLimpar_Click(object sender, RoutedEventArgs e)
        {
            txtCodigo.Clear();
            txtNome.Clear();
        }

        // Atualiza os campos de acordo com o vendedor selecionado
        private void txtCodigo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }

            con.Open();
            com.Connection = con;

            com.CommandText = "Select Vendedor_Nome " +
                             "from tblVendedor " +
                             "where Vendedor_Cod = '" + txtCodigo.Text + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                string nome = dr.GetString(0);
               
                txtNome.Text = nome;
                
            }
            dr.Close();
            con.Close();
        }

        // Abre a janela de consultar vendedores através da tecla Enter ou F2 no campo nome
        private void txtNome_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.F2)
            {
                Vendedor.ConsultaVendedor consultaVendedor = new Vendedor.ConsultaVendedor();
                consultaVendedor.ShowDialog();
            }
        }

        // Define o foco no campo nome ao carregar a janela

        private void VendWindow_Loaded(object sender, RoutedEventArgs e)
        {
            txtNome.Focus();
        }
    }
}
