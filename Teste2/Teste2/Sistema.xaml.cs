using System;
using System.Windows;

namespace Teste2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Sistema : Window
    {
        public Sistema()
        {
            InitializeComponent();
        }
               
        private void MenuSair_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Clientes_Click(object sender, RoutedEventArgs e)
        {
            Clientes clientes = new Clientes();
            clientes.ShowDialog();
        }

        private void Produtos_Click(object sender, RoutedEventArgs e)
        {
            Produtos produtos = new Produtos();
            produtos.ShowDialog();
        }

        private void Vendedores_Click(object sender, RoutedEventArgs e)
        {
            Vendedores vendedores = new Vendedores();
            vendedores.ShowDialog();
        }

        private void Pedidos_Click(object sender, RoutedEventArgs e)
        {
            Pedidos pedidos = new Pedidos();
            pedidos.ShowDialog();
        }
    }   
}
