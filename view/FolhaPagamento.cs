using FolhaFigital_Projeto.model.dao;
using FolhaFigital_Projeto.controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.CodeDom;
using FolhaFigital_Projeto.model.bean;
using System.Security.Policy;
using FolhaFigital_Projeto.model.bean.TelaUsuario;
using FolhaFigital_Projeto.model.bean.TelaUsuario.Banco;
using FolhaFigital_Projeto.model.bean.TelaUsuario.Empresa;
using System.Collections;
using FolhaFigital_Projeto.model.bean.TelaUsuario.Usuario;
using FolhaFigital_Projeto.model.bean.TelaUsuario.Endereco;
using FolhaFigital_Projeto.model.bean.FolhaPagamento;

namespace FolhaFigital_Projeto.view
{
    public partial class FolhaPagamento : Form
    {
        public FolhaPagamento()
        {
            InitializeComponent();
        }

        private void btnRelatorio_Click(object sender, EventArgs e)
        {
            Relatorio TelaRelatorio = new Relatorio();
            TelaRelatorio.Show();

            this.Hide();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            Menu TelaMenu = new Menu();
            TelaMenu.Show();

            this.Hide();
        }

        private void btnUsuario_Click(object sender, EventArgs e)
        {
            FormUsuario TelaUsuario = new FormUsuario();
            TelaUsuario.Show();

            this.Hide();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btnConsultaUse_Click(object sender, EventArgs e)
        {
             SqlCommand cmd = new SqlCommand();
            ConexaoDb url = new ConexaoDb();


            try
            {
                cmd.Connection = url.conectar();
                cmd.CommandText = "select * from ConsultPag;";
                
                SqlDataAdapter DbList = new SqlDataAdapter();
                DataTable DtList = new DataTable();

                DbList.SelectCommand = cmd;
                DbList.Fill(DtList);

                listUsuario.DataSource = DtList;

                url.desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }
        private void txtConsultaMatri_TextChanged(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            ConexaoDb url = new ConexaoDb();


            try
            {
                cmd.Connection = url.conectar();
                cmd.CommandText = $"select * from ConsultPag where matricula = {txtMatricula.Text};";

                SqlDataAdapter DbList = new SqlDataAdapter();
                DataTable DtList = new DataTable();

                DbList.SelectCommand = cmd;
                DbList.Fill(DtList);

                listUsuario.DataSource = DtList;

                url.desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        private void listUsuario_DoubleClick(object sender, EventArgs e)
        {
            txtIdRetorno.Text = listUsuario.SelectedRows[0].Cells[0].Value.ToString();
            txtNome.Text = listUsuario.SelectedRows[0].Cells[1].Value.ToString();
            txtMatricula.Text = listUsuario.SelectedRows[0].Cells[2].Value.ToString();
            txtPerfil.Text = listUsuario.SelectedRows[0].Cells[3].Value.ToString();
            txtEmpNome.Text = listUsuario.SelectedRows[0].Cells[4].Value.ToString();
            txtHoraTrabalho.Text = listUsuario.SelectedRows[0].Cells[5].Value.ToString();

            txtNome.Enabled = false;
            txtMatricula.Enabled = false;
            txtPerfil.Enabled = false;
            txtEmpNome.Enabled = false;
            txtHoraTrabalho.Enabled = false;

        }
        private void btnCalcular_Click_1(object sender, EventArgs e)
        {
            txtSalarioBruto.Enabled = false;
            txtSalarioLiq.Enabled = false;

            FolhaPagGerar FolhaPagCalcular = new FolhaPagGerar();

            if(ComHoraHextra.SelectedIndex == -1)
            {
                MessageBox.Show("Seleciona um valor no campo: Hora Extra?");
            }
         
            else if (ComHoraHextra.SelectedIndex == 0)
            {
                FolhaPagBean folpagEnt = new FolhaPagBean(Convert.ToDouble(txtHoraTrabalho.Text), Convert.ToDouble(txtValorHora.Text), ComHoraHextra.SelectedItem.ToString(), Convert.ToInt32(txtHoraExtra.Text), Convert.ToDouble(ComInss.SelectedItem.ToString()), Convert.ToDouble(ComIrrf.SelectedItem.ToString()),comBeneficio.SelectedItem.ToString());
                try
                {

                    String mensagem = FolhaPagCalcular.CalculaFolha(folpagEnt);
                    if (FolhaPagCalcular.FolhaPagamentoCalcular)
                    {
                        MessageBox.Show(mensagem, "Calcular", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show(FolhaPagCalcular.mensagem);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                txtSalarioBruto.Text = folpagEnt.Salario_Bruto.ToString("F2");
                txtSalarioLiq.Text = folpagEnt.Salario_Liquido.ToString("F2");
            }

            else if(ComHoraHextra.SelectedIndex == 1)
            {
                if(txtHoraExtra.Text == "")
                {
                    MessageBox.Show("Preencha o campo 'Hora Hextra' com o valor 0");
                }
                else
                {
                    FolhaPagBean folpagEnt = new FolhaPagBean(Convert.ToDouble(txtHoraTrabalho.Text),Convert.ToDouble(txtValorHora.Text),ComHoraHextra.SelectedItem.ToString(),Convert.ToInt32(txtHoraExtra.Text), Convert.ToDouble(ComInss.SelectedItem.ToString()), Convert.ToDouble(ComIrrf.SelectedItem.ToString()), comBeneficio.SelectedItem.ToString());
                    try
                    {
                        String mensagem = FolhaPagCalcular.CalculaFolha(folpagEnt);
                        if (FolhaPagCalcular.FolhaPagamentoCalcular) 
                        {
                            MessageBox.Show(mensagem, "Calcular", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else
                        {
                            MessageBox.Show(FolhaPagCalcular.mensagem);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    txtSalarioBruto.Text = folpagEnt.Salario_Bruto.ToString("F2");
                    txtDesconto.Text = folpagEnt.desconto.ToString("F2");
                    txtSalarioLiq.Text = folpagEnt.Salario_Liquido.ToString("F2");

                }
            }
        }
        private void btnGerarFolha_Click_1(object sender, EventArgs e)
        {
           FolhaPagGerar GerarFolha = new FolhaPagGerar();
            FolhaPagGerar GerarSalario = new FolhaPagGerar();

            if (txtNome.Text == ""|| txtMatricula.Text == ""|| txtPerfil.Text == ""|| txtEmpNome.Text == ""||txtHoraTrabalho.Text == "")
            {
                MessageBox.Show("Selecione um funcionátio para gerar a folha de pagamento");
            }
            else if(txtHoraTrabalho.Text == "" || txtValorHora.Text == "" || txtHoraExtra.Text == "" || txtAnoRef.Text == "" ) 
            {
                MessageBox.Show("Preencha os campos obrigatórios para o calculo da folha");
            }
            else
            {
                FolhaPagBean folpagEnt = new FolhaPagBean(Convert.ToInt32(txtIdRetorno.Text), Convert.ToDouble(txtHoraTrabalho.Text), Convert.ToDouble(txtValorHora.Text), ComHoraHextra.SelectedItem.ToString(), Convert.ToInt32(txtHoraExtra.Text), Convert.ToDouble(ComInss.SelectedItem.ToString()), Convert.ToDouble(ComIrrf.SelectedItem.ToString()), comBeneficio.SelectedItem.ToString(), Convert.ToDateTime(txtDataPagamento.Text), ComMesRef.SelectedItem.ToString(), ComFalta.SelectedItem.ToString(), ComMotFalta.SelectedItem.ToString(), Convert.ToInt32(txtAnoRef.Text), ComCargo.SelectedItem.ToString(), Convert.ToDouble(txtSalarioBruto.Text), Convert.ToDouble(txtDesconto.Text), Convert.ToDouble(txtSalarioLiq.Text));
                try
                {

                    String mensagem = GerarFolha.GerarFolha(folpagEnt);
                    if (GerarFolha.FolhaPagamentoGerar)
                    {
                        MessageBox.Show(mensagem, "Folha Gerada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show(GerarSalario.mensagem);
                    }
                    mensagem = GerarSalario.GerarSalario(folpagEnt);
                    if (GerarFolha.SalarioGerar)
                    {
                        MessageBox.Show(mensagem, "Folha Geerada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show(GerarSalario.mensagem);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void txtCargo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtConsultaMatri_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
