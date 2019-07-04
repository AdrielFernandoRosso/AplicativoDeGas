using AppGas.Modelo;
using System.Collections.Generic;
using AppGas.Dal;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppGas.Dal
{
    public class DalCarrgaTudoParaAplicacao
    {
        DalPrincipalBotijao dalPrincipalBotijao = new DalPrincipalBotijao();
        DalCidade dalCidade = new DalCidade();
        DalEstado dalEstado = new DalEstado();
        DalItemCarrinho dalCarrinho = new DalItemCarrinho();
        DalCadastroCliente dalCadastro = new DalCadastroCliente();


        List<BotijaoPagPrincipal> BotijoesList = new List<BotijaoPagPrincipal>();
        List<Cidade> CidadeList = new List<Cidade>();
        List<Estado> EstadoList = new List<Estado>();



        public void InformacoesDeCidades()
        {
            Cidade cid1 = new Cidade { Descricao = "Medianeira", EstadoID = 1 };
            Cidade cid2 = new Cidade { Descricao = "Matelandia", EstadoID = 1 };
            Cidade cid3 = new Cidade { Descricao = "Sao Miguel", EstadoID = 1 };
            Cidade cid4 = new Cidade { Descricao = "Sao Paulo", EstadoID = 2 };
            Cidade cid5 = new Cidade { Descricao = "Sorocaba", EstadoID = 2 };
            Cidade cid6 = new Cidade { Descricao = "Santos", EstadoID = 2 };
            Cidade cid7 = new Cidade { Descricao = "Cocal", EstadoID = 3 };
            Cidade cid8 = new Cidade { Descricao = "Balneario", EstadoID = 3 };
            Cidade cid9 = new Cidade { Descricao = "Blumenau", EstadoID = 3 };
            CidadeList.Add(cid1);
            CidadeList.Add(cid2);
            CidadeList.Add(cid3);
            CidadeList.Add(cid4);
            CidadeList.Add(cid5);
            CidadeList.Add(cid6);
            CidadeList.Add(cid7);
            CidadeList.Add(cid8);
            CidadeList.Add(cid9);
        }


        public void InformacoesDeItens()
        {
            BotijaoPagPrincipal botijaoPag1 = new BotijaoPagPrincipal { Descricao = "Botijao P20 20KG", Preco = 150, Quantidade = 1000, Imagem = "BT3.png", HorasCarga = 346 };
            BotijaoPagPrincipal botijaoPag2 = new BotijaoPagPrincipal { Descricao = "Botijao P13 13KG", Preco = 85, Quantidade = 900, Imagem = "BT2.png", HorasCarga = 226 };
            BotijaoPagPrincipal botijaoPag3 = new BotijaoPagPrincipal { Descricao = "Botijao P45 45KG", Preco = 300, Quantidade = 800, Imagem = "BT1.png", HorasCarga = 791 };
            BotijoesList.Add(botijaoPag1);
            BotijoesList.Add(botijaoPag2);
            BotijoesList.Add(botijaoPag3);
        }

        public void InformacoesDeEstados()
        {
            Estado est1 = new Estado { Descricao = "Parana", UK = "PR" };
            Estado est2 = new Estado { Descricao = "Sao Paulo", UK = "SP" };
            Estado est3 = new Estado { Descricao = "Santa Catarina", UK = "SC" };
            Estado est4 = new Estado { Descricao = "Rio De Janeiro", UK = "RJ" };

            EstadoList.Add(est1);
            EstadoList.Add(est2);
            EstadoList.Add(est3);
            EstadoList.Add(est4);
        }




            public void UploadBotijao()
        {
            InformacoesDeItens();
            foreach (BotijaoPagPrincipal botigaoPInserir in BotijoesList)
            {
                
                bool temNoBanco = false;
                foreach (BotijaoPagPrincipal botijaoBanco in dalPrincipalBotijao.GetAllBotijao())
                {
                    if (botijaoBanco.Descricao == botigaoPInserir.Descricao)
                    {
                        temNoBanco = true;
                        break;
                    }
                }

                if (temNoBanco == false)
                {
                    dalPrincipalBotijao.Add(botigaoPInserir);
                }
            }
        }


        public void UploadEstado()
        {
            InformacoesDeEstados();
            foreach (Estado estadoPInserir in EstadoList)
            {
                bool temNoBanco = false;
                foreach (Estado estadoBanco in dalEstado.GetEstado())
                {
                    if(estadoPInserir.Descricao == estadoBanco.Descricao)
                    {
                        temNoBanco = true;break;
                    } 
                }
                if (temNoBanco == false)
                {
                    dalEstado.Add(estadoPInserir);
                }
            }
        }

        public void UploadCidade()
        {
            InformacoesDeCidades();
            foreach (Cidade cidadePInserir in CidadeList)
            {
                bool temNoBanco = false;
                foreach (Cidade cidadeBanco in dalCidade.GetCidade())
                {
                    if(cidadePInserir.Descricao == cidadeBanco.Descricao)
                    {
                        temNoBanco = true;break;
                    }
                }

                if(temNoBanco == false)
                {
                    dalCidade.Add(cidadePInserir);
                }
            }
        }
    }
}
