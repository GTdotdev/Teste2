using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Teste2.Produto
{
    /// <summary>
    /// Interaction logic for ConsultaProduto.xaml
    /// </summary>
    public partial class ConsultaProduto : Window
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        public ConsultaProduto()
        {
            InitializeComponent();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["Teste2.Properties.Settings.ConnectionString"].ConnectionString.ToString();
        }

        // Preenche o data grid de acordo com a tabela produtos

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }

            con.Open();
            com.Connection = con;

            com.CommandText = "select * from tblProduto";
            DataTable dt = new DataTable("tblProduto");
            SqlDataAdapter dataAdp = new SqlDataAdapter(com);
            dataAdp.Fill(dt);
            DataGrid.ItemsSource = dt.DefaultView;
            dataAdp.Update(dt);
            con.Close();
        }

        // Construtor para armazenar o id do produto
        public class ProdCod
        {
            public string? id { get; set; }
        }

        // Método para enviar o id do produto para a janela produtos ou para a janela pedidos
        private void DataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                foreach (DataRowView row in DataGrid.SelectedItems)
                {
                    ProdCod codproduto = new ProdCod();
                    codproduto.id = row.Row.ItemArray[0].ToString();
                    foreach (Window item in Application.Current.Windows)
                    {
                        if (item.Name == "ProdWindow")
                        {
                            ((Produtos)item).txtCodigo.Text = codproduto.id;
                        }
                        else if (item.Name == "PedWindow")
                        {
                            ((Pedidos)item).txtProduto.Text = codproduto.id;
                        }
                    }
                }
                this.Close();
            }
        }
    }
}
