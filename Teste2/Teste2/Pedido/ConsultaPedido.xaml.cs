using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Teste2.Pedido
{
    /// <summary>
    /// Interaction logic for ConsultaPedido.xaml
    /// </summary>
    public partial class ConsultaPedido : Window
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        public ConsultaPedido()
        {
            InitializeComponent();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["Teste2.Properties.Settings.ConnectionString"].ConnectionString.ToString();
        }

        // Preenche o data grid de acordo com a tabela pedidos

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }

            con.Open();
            com.Connection = con;

            com.CommandText = "select * from tblPedido";
            DataTable dt = new DataTable("tblPedido");
            SqlDataAdapter dataAdp = new SqlDataAdapter(com);
            dataAdp.Fill(dt);
            DataGrid.ItemsSource = dt.DefaultView;
            dataAdp.Update(dt);
            con.Close();
        }

        // Construtor para armazenar o id do pedido
        public class PedCod
        {
            public string? id { get; set; }
        }

        // Método para enviar o id do pedido para a janela pedidos

        private void DataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                foreach (DataRowView row in DataGrid.SelectedItems)
                {
                    PedCod codpedido = new PedCod();
                    codpedido.id = row.Row.ItemArray[0].ToString();
                    foreach (Window item in Application.Current.Windows)
                    {
                        if (item.Name == "PedWindow")
                        {
                            ((Pedidos)item).txtNumPedido.Text = codpedido.id;
                        }
                    }
                }
                this.Close();
            }
        }
    }
}
