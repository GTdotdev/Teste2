#pragma checksum "..\..\..\Sistema.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "61203E6F40D77406829A3305AFE1D095FAAC0208"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using Teste2;


namespace Teste2 {
    
    
    /// <summary>
    /// Sistema
    /// </summary>
    public partial class Sistema : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 32 "..\..\..\Sistema.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem Clientes;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\Sistema.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem Produtos;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\Sistema.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem Vendedores;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\Sistema.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem Pedidos;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\Sistema.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem MenuSair;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\Sistema.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblUser;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.5.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Teste2;V1.0.0.0;component/sistema.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Sistema.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.5.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Clientes = ((System.Windows.Controls.MenuItem)(target));
            
            #line 34 "..\..\..\Sistema.xaml"
            this.Clientes.Click += new System.Windows.RoutedEventHandler(this.Clientes_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Produtos = ((System.Windows.Controls.MenuItem)(target));
            
            #line 38 "..\..\..\Sistema.xaml"
            this.Produtos.Click += new System.Windows.RoutedEventHandler(this.Produtos_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Vendedores = ((System.Windows.Controls.MenuItem)(target));
            
            #line 42 "..\..\..\Sistema.xaml"
            this.Vendedores.Click += new System.Windows.RoutedEventHandler(this.Vendedores_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Pedidos = ((System.Windows.Controls.MenuItem)(target));
            
            #line 47 "..\..\..\Sistema.xaml"
            this.Pedidos.Click += new System.Windows.RoutedEventHandler(this.Pedidos_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.MenuSair = ((System.Windows.Controls.MenuItem)(target));
            
            #line 52 "..\..\..\Sistema.xaml"
            this.MenuSair.Click += new System.Windows.RoutedEventHandler(this.MenuSair_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.lblUser = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

