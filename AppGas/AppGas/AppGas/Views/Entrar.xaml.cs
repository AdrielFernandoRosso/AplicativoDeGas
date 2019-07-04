using AppGas.Dal;
using AppGas.Modelo;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppGas.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Entrar : ContentPage
	{
        
        DalCadastroCliente dalCadastro = new DalCadastroCliente();
        DalLogin login = new DalLogin();
        LoginTemporario loginTemp = new LoginTemporario();

		public Entrar()
		{
			InitializeComponent ();
        }

        //==============================================================================
        //FAZER LOGIN
        private void Btentrar_Clicked(object sender, EventArgs e)
        {
            bool LoginEfetuado = false;
            foreach (Cliente clienteBanco in dalCadastro.GetClietes())
            {
                //VERIFICA DE DADOS DOS ENTRY SAO COMPRATIVEL COM ALGUM USUARIO DO BANCO
                if(clienteBanco.Usuario == EntUsuario.Text && clienteBanco.Senha == EntSenha.Text)
                {
                    loginTemp.Usuario = EntUsuario.Text;
                    loginTemp.Senha = EntSenha.Text;
                    login.Add(loginTemp);
                    LoginEfetuado = true;
                    Navigation.PushAsync(new Principal());
                    //Navigation.PopToRootAsync();
                    break;
                }
            }
            if (LoginEfetuado == false)
            {
                DisplayAlert("Erro ao logar", "Senha ou Usuario invalido", "OK");
            }
        }
    }
}