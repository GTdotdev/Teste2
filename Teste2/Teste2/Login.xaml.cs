using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace Teste2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {

        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader? dr;
        public Login()
        {
            InitializeComponent();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["Teste2.Properties.Settings.ConnectionString"].ConnectionString.ToString();
        }

        // Executa o Método Verificador no Clique de Login
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }

            if (VerifyUser(textBoxUser.Text, textBoxPassword.Password.ToUpper()))
            {
                con.Close();
                Sistema sistema = new Sistema();
                sistema.lblUser.Content = $"Usuário: {textBoxUser.Text}";
                sistema.Show();                
                this.Close();

            }
            else
            {
                MessageBox.Show("Usuário ou Senha inválidos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }              

        // Método para Verificar as Credenciais
        private bool VerifyUser(string username, string password)
        {
            con.Open();
            com.Connection = con;
            com.CommandText = "select Usuario_Cod from tblUsuario where Usuario_Nome='" + username + "' and Usuario_Senha='" + password + "'";
            dr = com.ExecuteReader();

            if (dr.Read())
            {
                if (Convert.ToBoolean(dr["Usuario_Cod"]) == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }        

        // Encerra a Aplicação no Clique de Sair
        private void SairButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        
        // Envia um clique de login ao pressionar enter ou um clique de sair ao pressionar esc

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoginButton_Click(sender, e);
            }
            else if (e.Key == Key.Escape)
            {
                SairButton_Click(sender, e);
            }
        }

        // Define o foco no campo Usuário ao abrir a janela de login

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            textBoxUser.Focus();
        }
    }
}
