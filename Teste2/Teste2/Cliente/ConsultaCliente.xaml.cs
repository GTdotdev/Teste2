using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Teste2.Cliente
{
    /// <summary>
    /// Interaction logic for ConsultaCliente.xaml
    /// </summary>
    public partial class ConsultaCliente : Window
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        public ConsultaCliente()
        {
            InitializeComponent();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["Teste2.Properties.Settings.ConnectionString"].ConnectionString.ToString();
        }

        // Preenche o data grid de acordo com a tabela clientes

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }

            con.Open();
            com.Connection = con;

            com.CommandText = "select * from tblCliente";
            DataTable dt = new DataTable("tblCliente");
            SqlDataAdapter dataAdp = new SqlDataAdapter(com);
            dataAdp.Fill(dt);
            DataGrid.ItemsSource = dt.DefaultView;
            dataAdp.Update(dt);
            con.Close();
        }

        // Construtor para armazenar o id e nome do cliente
        public class CliCod
        {
            public string? id { get; set; }
            public string? nome { get; set; }
        }

        // Método para enviar o id do cliente para a janela clientes ou o nome para a janela pedidos
        private void DataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                foreach (DataRowView row in DataGrid.SelectedItems)
                {
                    CliCod codcliente = new CliCod();
                    codcliente.id = row.Row.ItemArray[0].ToString();
                    codcliente.nome = row.Row.ItemArray[1].ToString();
                    foreach (Window item in Application.Current.Windows)
                    {
                        if (item.Name == "CliWindow")
                        {
                            ((Clientes)item).txtCodigo.Text = codcliente.id;
                        }
                        else if (item.Name == "PedWindow")
                        {
                            ((Pedidos)item).txtCliente.Text = codcliente.nome;
                        }
                    }
                }
                this.Close();
            }
        }
    }
}
