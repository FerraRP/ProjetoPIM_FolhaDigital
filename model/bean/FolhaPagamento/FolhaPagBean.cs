using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FolhaFigital_Projeto.model.bean.FolhaPagamento
{
    internal class FolhaPagBean
    {
        public int Id_FolhaPag { get; set; }
        public double Horas_Trabalhada { get; set; }
        public double Valor_Hora { get; set; }
        public string Hora_Extra { get; set; }
        public int Qtd_HorasExtras { get; set; }
        public double Inss { get; set; }
        public double Irrf { get; set; }
        public string beneficio { get;set; }
        public DateTime Data_Pagamento { get; set; }
        public string Mes_Referente { get; set; }
        public int Ano_Referente { get; set; }
        public string Falta { get; set; }
        public string Motivo_Falta { get; set; } 
        public double desconto {  get; set; }

        public int Id_Salario { get; set; }
        public string Cargo { get; set; }
        public double Salario_Bruto { get; set; }
        public double Salario_Liquido { get; set; }
        public int Id_Retorno { get; set; }

        public FolhaPagBean(int IdFolhaPag, int IdSalario)
        {
            this.Id_FolhaPag = IdFolhaPag;
            this.Id_Salario = IdSalario;
           
        }

        public FolhaPagBean(double HorasTrabalhadas, double ValorHora, string hora_Extra, int qtd_HorasExtras, double Inss, double Irrf, string Beneficios)
        {
            this.Horas_Trabalhada = HorasTrabalhadas;
            this.Valor_Hora = ValorHora;
            this.Hora_Extra = hora_Extra;
            this.Qtd_HorasExtras = qtd_HorasExtras;
            this.Inss = Inss;
            this.Irrf = Irrf;
            this.beneficio = Beneficios;
                        
        }

        public FolhaPagBean(int IdRetorno,double HorasTrabalhadas, double ValorHora, string hora_Extra, int qtd_HorasExtras, double Inss, double Irrf, string Beneficios, DateTime DataPagamento, string MesReferente,string Falta, string MotivoFalta, int AnoReferente,string Cargo, double SalarioBruto, double Desconto, double SalarioLiquido)
        {
            this.Id_Retorno = IdRetorno;
            this.Horas_Trabalhada = HorasTrabalhadas;
            this.Valor_Hora = ValorHora;
            this.Hora_Extra = hora_Extra;
            this.Qtd_HorasExtras = qtd_HorasExtras;
            this.Inss = Inss;
            this.Irrf = Irrf;
            this.beneficio = Beneficios;
            this.Data_Pagamento = DataPagamento;
            this.Mes_Referente = MesReferente;
            this.Ano_Referente = AnoReferente;
            this.Falta = Falta;
            this.Motivo_Falta = MotivoFalta;
            this.Cargo = Cargo;
            this.Salario_Bruto = SalarioBruto;
            this.desconto = Desconto;
            this.Salario_Liquido = SalarioLiquido;
        }
    }
}
