using FolhaFigital_Projeto.model.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using FolhaFigital_Projeto.view;
using System.Security.Policy;
using System.Windows.Forms;
using FolhaFigital_Projeto.model.bean;
using FolhaFigital_Projeto.model.bean.FolhaPagamento;

namespace FolhaFigital_Projeto.controller.FolhaPagamento
{
    internal class ValidaFolhaPag
    {
        public String mensagem = "";
        SqlCommand cmd = new SqlCommand();
        ConexaoDb url = new ConexaoDb();

        public string CalculaFolha(FolhaPagBean folpagEnt)
        {

            if (folpagEnt.beneficio == "SIM")
            {
                if (Convert.ToBoolean(folpagEnt.Hora_Extra == "SIM"))
                {
                    try
                    {
                        double HorasExtras, ValorFolha, ConvertInss, ConvertIrrf,INSS, IRRF, ValorBeficio;

                        ValorFolha = folpagEnt.Valor_Hora * folpagEnt.Horas_Trabalhada;
                        HorasExtras = (folpagEnt.Valor_Hora * folpagEnt.Qtd_HorasExtras) * 1.5;

                        folpagEnt.Salario_Bruto = ValorFolha + HorasExtras;

                        INSS = folpagEnt.Inss / 100;
                        IRRF = folpagEnt.Irrf / 100;
                        ValorBeficio = folpagEnt.Salario_Bruto * 0.16;
                        ConvertInss = folpagEnt.Salario_Bruto * INSS;
                        ConvertIrrf = folpagEnt.Salario_Bruto * IRRF;
                        folpagEnt.desconto = ConvertInss + ConvertIrrf + ValorBeficio;
                        folpagEnt.Salario_Liquido = folpagEnt.Salario_Bruto - folpagEnt.desconto;

                        MessageBox.Show($"HoraExtra:{HorasExtras}");
                        MessageBox.Show($"INSS:{INSS}");
                        MessageBox.Show($"IRFF:{ConvertIrrf}");
                        MessageBox.Show($"Desconto:{folpagEnt.desconto}");
                        MessageBox.Show($"valor bene:{ValorBeficio}");
                    }
                    catch
                    {
                        this.mensagem = "Erro no calculo";
                    }
                }
                else if (Convert.ToBoolean(folpagEnt.Hora_Extra == "NÃO"))
                {
                    try
                    {
                        Double ConvertInss, ConvertIrrf, INSS, IRRF, ValorBeficio;

                        folpagEnt.Salario_Bruto = folpagEnt.Valor_Hora * folpagEnt.Horas_Trabalhada;

                        INSS = folpagEnt.Inss / 100;
                        IRRF = folpagEnt.Irrf / 100;
                        ValorBeficio = folpagEnt.Salario_Bruto * 0.16;
                        ConvertInss = folpagEnt.Salario_Bruto * INSS;
                        ConvertIrrf = folpagEnt.Salario_Bruto * IRRF;
                        folpagEnt.desconto = ConvertInss + ConvertIrrf + ValorBeficio;
                        folpagEnt.Salario_Liquido = folpagEnt.Salario_Bruto - folpagEnt.desconto;

                        MessageBox.Show($"INSS:{INSS}");
                        MessageBox.Show($"IRFF:{ConvertIrrf}");
                        MessageBox.Show($"Desconto:{folpagEnt.desconto}");
                        MessageBox.Show($"valor bene:{ValorBeficio}");

                        this.mensagem = "Calculado com sucesso!";

                        MessageBox.Show("Controler de GerarFolha ok");

                    }
                    catch
                    {
                        this.mensagem = "Erro no calculo";
                    }
                }
                MessageBox.Show("Calculo com beneficio - SIM");
            }
            else if(folpagEnt.beneficio == "NÃO")
            {
                if (Convert.ToBoolean(folpagEnt.Hora_Extra == "SIM"))
                {
                    try
                    {
                        double HorasExtras, ValorFolha, ConvertInss, ConvertIrrf, INSS, IRRF;

                        ValorFolha = folpagEnt.Valor_Hora * folpagEnt.Horas_Trabalhada;
                        HorasExtras = (folpagEnt.Valor_Hora * folpagEnt.Qtd_HorasExtras) * 1.5;

                        folpagEnt.Salario_Bruto = ValorFolha + HorasExtras;

                        INSS = folpagEnt.Inss / 100;
                        IRRF = folpagEnt.Irrf / 100;
                        ConvertInss = folpagEnt.Salario_Bruto * INSS;
                        ConvertIrrf = folpagEnt.Salario_Bruto * IRRF;
                        folpagEnt.desconto = ConvertInss + ConvertIrrf;
                        folpagEnt.Salario_Liquido = folpagEnt.Salario_Bruto - folpagEnt.desconto;

                        MessageBox.Show($"HoraExtra:{HorasExtras}");
                        MessageBox.Show($"INSS:{INSS}");
                        MessageBox.Show($"IRFF:{ConvertIrrf}");
                        MessageBox.Show($"Desconto:{folpagEnt.desconto}");
                    }
                    catch
                    {
                        this.mensagem = "Erro no calculo";
                    }
                }
                else if (Convert.ToBoolean(folpagEnt.Hora_Extra == "NÃO"))
                {
                    try
                    {
                        Double ConvertInss, ConvertIrrf, INSS, IRRF;

                        folpagEnt.Salario_Bruto = folpagEnt.Valor_Hora * folpagEnt.Horas_Trabalhada;

                        INSS = folpagEnt.Inss / 100;
                        IRRF = folpagEnt.Irrf / 100;
                        ConvertInss = folpagEnt.Salario_Bruto * INSS;
                        ConvertIrrf = folpagEnt.Salario_Bruto * IRRF;
                        folpagEnt.desconto = ConvertInss + ConvertIrrf;
                        folpagEnt.Salario_Liquido = folpagEnt.Salario_Bruto - folpagEnt.desconto;

                        MessageBox.Show($"INSS:{INSS}");
                        MessageBox.Show($"IRFF:{ConvertIrrf}");
                        MessageBox.Show($"Desconto:{folpagEnt.desconto}");

                        this.mensagem = "Calculado com sucesso!";

                        MessageBox.Show("Controler de GerarFolha ok");

                    }
                    catch
                    {
                        this.mensagem = "Erro no calculo";
                    }
                }
                MessageBox.Show("Calculo com beneficio - Não");
            }
            return mensagem;
        }

        public String GerarFolha(FolhaPagBean folpagEnt)
        {
            cmd.CommandText = "insert into folha_pagamento (Fk_FolhUsuario,horas_trabalhadas,valor_hora,inss,irrf,beneficio,desconto,horas_extras,qtd_HorasHextras,Falta,Motivo_Falta,data_pagamento,mes_referencia,ano_referencia) values (@Fk_FolhUsuario,@horas_trabalhadas,@valor_hora,@inss,@irrf,@beneficio,@desconto,@horas_extras,@qtd_HorasHextras,@Falta,@Motivo_Falta,@data_pagamento,@mes_referencia,@ano_referencia)";
            cmd.Parameters.AddWithValue("@Fk_FolhUsuario", folpagEnt.Id_Retorno);
            cmd.Parameters.AddWithValue("@horas_trabalhadas", folpagEnt.Horas_Trabalhada);
            cmd.Parameters.AddWithValue("@valor_hora", folpagEnt.Valor_Hora);
            cmd.Parameters.AddWithValue("@inss", folpagEnt.Inss);
            cmd.Parameters.AddWithValue("@irrf", folpagEnt.Irrf);
            cmd.Parameters.AddWithValue("@beneficio", folpagEnt.beneficio);
            cmd.Parameters.AddWithValue("@desconto", folpagEnt.desconto);
            cmd.Parameters.AddWithValue("@horas_extras", folpagEnt.Hora_Extra);
            cmd.Parameters.AddWithValue("@qtd_HorasHextras", folpagEnt.Qtd_HorasExtras);
            cmd.Parameters.AddWithValue("@Falta", folpagEnt.Falta);
            cmd.Parameters.AddWithValue("@Motivo_Falta", folpagEnt.Motivo_Falta);
            cmd.Parameters.AddWithValue("@data_pagamento",folpagEnt.Data_Pagamento);
            cmd.Parameters.AddWithValue("@mes_referencia",folpagEnt.Mes_Referente);
            cmd.Parameters.AddWithValue("@ano_referencia",folpagEnt.Ano_Referente);

            try
            {
                cmd.Connection = url.conectar();
                cmd.ExecuteNonQuery();
                url.desconectar();

                this.mensagem = "Gerado com sucesso!";
                MessageBox.Show("Conntroller de Banco  ok");

            }
            catch (SqlException)
            {
                this.mensagem = "Erro com o banco de dados Gerar Pag";
            }
            return mensagem;

        }
        public String GerarSalario(FolhaPagBean folpagEnt)
        {
            cmd.CommandText = "insert into salario (Fk_SalUsuario,cargo,salario_bruto,salario_liquido) values (@Fk_SalUsuario,@cargo,@salario_bruto,@salario_liquido)";
            cmd.Parameters.AddWithValue("@Fk_SalUsuario", folpagEnt.Id_Retorno);
            cmd.Parameters.AddWithValue("@cargo", folpagEnt.Cargo);
            cmd.Parameters.AddWithValue("@salario_bruto", folpagEnt.Salario_Bruto);
            cmd.Parameters.AddWithValue("@salario_liquido", folpagEnt.Salario_Liquido);

            try
            {
                cmd.Connection = url.conectar();
                cmd.ExecuteNonQuery();
                url.desconectar();

                this.mensagem = "Gerado com sucesso!";
                MessageBox.Show("Conntroller de Salario ok");

            }
            catch (SqlException)
            {
                this.mensagem = "Erro com o banco de dados Gerar Pag";
            }
            return mensagem;


        }
    }
}
