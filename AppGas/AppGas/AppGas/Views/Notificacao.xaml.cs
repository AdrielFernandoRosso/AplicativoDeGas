using AppGas.Dal;
using AppGas.Modelo;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppGas.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Notificacao : ContentPage
	{

        DalPrincipalBotijao dalBotijao = new DalPrincipalBotijao();
        MNotificacao notificacao = new MNotificacao();
        DalNotificacao dalNotificacao = new DalNotificacao();
        Cliente clienteLogado = new Cliente();
        
        
        public Notificacao (Cliente cliente)
		{
			InitializeComponent ();
            CarregarPikerBotijao();
            clienteLogado = cliente;
            NotificacaoInicial(cliente);
        }

        //======================================================================================
        //CARREGA DADOS DE NOTIFICACAO DO BANCO
        public void NotificacaoInicial(Cliente cliente)
        {
            string formatoDataTime = string.Empty;
            
            foreach (MNotificacao dadosNotificacao in dalNotificacao.GetAllID(cliente))
            {
                formatoDataTime = dadosNotificacao.CauculoData.ToString("dd/MM/yyyy");
                lblInfHoras.Text = dadosNotificacao.MediaHoras.ToString();
                lblInfQuantidadeBocas.Text = dadosNotificacao.QuantidadeBocas.ToString();
                lblInfProximaTrocaData.Text = formatoDataTime;
                break;
            }
        }


        //======================================================================================
        //CARREGA TODOS OS BOTIJOES NO PIKER 
        public void CarregarPikerBotijao()
        {
            foreach (BotijaoPagPrincipal item in dalBotijao.GetAllBotijao())
            {
                PikerBotijao.Items.Add(item.Descricao);
            }
        }


        int HorasCarga;
        //======================================================================================
        //SELECIONA ITEM DO PIKER
        public void SelecionaItemBotijao_Cliked(object sender, EventArgs e)
        {
            var tipoBotijao = PikerBotijao.Items[PikerBotijao.SelectedIndex];

            foreach (BotijaoPagPrincipal botijoesBanco in dalBotijao.GetAllBotijao())
            {
                if (tipoBotijao == botijoesBanco.Descricao)
                {
                    HorasCarga = botijoesBanco.HorasCarga;
                }
            }
        }

        //======================================================================================
        //BOTAO CALCULA TEMPO DE DURACAO DO BOTIJAO
        public void BtCalcular_Clicked(object sender, EventArgs e)
        {
            // botijaoHoras/(horasdia*quantidade)

            string formatoDataHora = string.Empty;

            double CalculoDiasConsumo = HorasCarga / (Convert.ToInt16(entHoras.Text) * Convert.ToInt16(entQuantidade.Text));

            formatoDataHora = DateTime.Now.Date.AddDays(CalculoDiasConsumo).ToString("dd/MM/yyyy");

            lblDias.Text = Convert.ToString(formatoDataHora);
        }


        //======================================================================================
        //BOTAO SALVA NOTIFICACAO
        public async void BtSalvar_Clicked(object sender, EventArgs e)
        {
            var Status = await DisplayAlert("Receber Notificacao", "Esta operacao ira substituir o notificacao anterior", "Confirmar", "Cancelar");

            if (Status)
            {
                dalNotificacao.DeleteAll();
                double CalculoDiasConsumo = HorasCarga / (Convert.ToInt16(entHoras.Text) * Convert.ToInt16(entQuantidade.Text));

                notificacao.MediaHoras = Convert.ToInt16(entHoras.Text);
                notificacao.QuantidadeBocas = Convert.ToInt16(entQuantidade.Text);
                notificacao.CauculoData = DateTime.Now.Date.AddDays(CalculoDiasConsumo);
                notificacao.ClienteID = clienteLogado.ID;
                dalNotificacao.Add(notificacao);
                await DisplayAlert("Salvar Notificacao", "Salvo com sucesso", "OK");
                NotificacaoInicial(clienteLogado);
            }
        }
    }
}