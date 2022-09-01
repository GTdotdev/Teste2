using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Teste2.Vendedor
{
    /// <summary>
    /// Interaction logic for ConsultaVendedor.xaml
    /// </summary>
    public partial class ConsultaVendedor : Window
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        public ConsultaVendedor()
        {
            InitializeComponent();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["Teste2.Properties.Settings.ConnectionString"].ConnectionString.ToString();
        }

        // Preenche o data grid de acordo com a tabela vendedores

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }

            con.Open();
            com.Connection = con;

            com.CommandText = "select * from tblVendedor";
            DataTable dt = new DataTable("tblVendedor");
            SqlDataAdapter dataAdp = new SqlDataAdapter(com);
            dataAdp.Fill(dt);
            DataGrid.ItemsSource = dt.DefaultView;
            dataAdp.Update(dt);
            con.Close();
        }

        // Construtor para armazenar o id e nome do vendedor

        public class VendCod
        {
            public string? id { get; set; }
            public string? nome { get; set; }
        }

        // Método para enviar o id do vendedor para a janela vendedores ou o nome para a janela pedidos

        private void DataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                foreach (DataRowView row in DataGrid.SelectedItems)
                {
                    VendCod codvendedor = new VendCod();
                    codvendedor.id = row.Row.ItemArray[0].ToString();
                    codvendedor.nome = row.Row.ItemArray[1].ToString();
                    foreach (Window item in Application.Current.Windows)
                    {
                        if (item.Name == "VendWindow")
                        {
                            ((Vendedores)item).txtCodigo.Text = codvendedor.id;
                        }
                        else if (item.Name == "PedWindow")
                        {
                            ((Pedidos)item).txtVendedor.Text = codvendedor.nome;
                        }
                    }
                }
                this.Close();
            }
        }
    }
}
