using FolhaFigital_Projeto.controller.FolhaPagamento;
using FolhaFigital_Projeto.controller.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FolhaFigital_Projeto.model.bean.FolhaPagamento
{
    internal class FolhaPagGerar
    {
        public bool FolhaPagamentoCalcular;
        public bool FolhaPagamentoGerar;
        public bool SalarioGerar;
        public String mensagem = "";
        public String CalculaFolha(FolhaPagBean folpagEnt)
        {
            ValidaFolhaPag CalcularFolha = new ValidaFolhaPag();
            this.mensagem = CalcularFolha.CalculaFolha(folpagEnt);

            MessageBox.Show("model de calcular folha ok");

            return mensagem;
        }

        public String GerarFolha(FolhaPagBean folpagEnt)
        {
            ValidaFolhaPag GerarFolhaPag = new ValidaFolhaPag();
            this.mensagem = GerarFolhaPag.GerarFolha(folpagEnt);

            MessageBox.Show("model de gerar folha ok");

            return mensagem;
        }
        public String GerarSalario(FolhaPagBean folpagEnt)
        {
            ValidaFolhaPag GerarSalario = new ValidaFolhaPag();
            this.mensagem = GerarSalario.GerarSalario(folpagEnt);

            MessageBox.Show("model de gerar folha ok");

            return mensagem;
        }
    }
}
    