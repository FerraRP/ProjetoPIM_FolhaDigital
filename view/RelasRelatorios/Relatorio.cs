using FolhaFigital_Projeto.model.dao;
using FolhaFigital_Projeto.view.RelasRelatorios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FolhaFigital_Projeto.view
{
    public partial class Relatorio : Form
    {
        public Relatorio()
        {
            InitializeComponent();

            btnImprimirRelatFunc.Enabled = false;
            btnImprimirRelatEmpre.Enabled = false;
            btnImprimirFolha.Enabled = false;
        }

        private void btnRelatorio_Click(object sender, EventArgs e)
        {

        }

        private void btnFolhaPagamento_Click(object sender, EventArgs e)
        {
            FolhaPagamento TelaPagamento = new FolhaPagamento();
            TelaPagamento.Show();

            this.Hide();
        }

        private void btnUsuario_Click(object sender, EventArgs e)
        {
            FormUsuario TelaUsuario = new FormUsuario();
            TelaUsuario.Show();

            this.Hide();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            Menu TelaMenu = new Menu();
            TelaMenu.Show();

            this.Hide();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }
        private void btnRltFuncionario_Click(object sender, EventArgs e)
        {
            btnImprimirRelatFunc.Enabled = true;
            btnImprimirRelatEmpre.Enabled = false;
            btnImprimirFolha.Enabled = false;

            SqlCommand cmd = new SqlCommand();
            ConexaoDb url = new ConexaoDb();
            dtContador.Visible = true;


            try
            {
                cmd.Connection = url.conectar();
                cmd.CommandText = "select usuario.nome,usuario.matricula,usuario.email,usuario.perfil,usuario.status,empresa.razao_social\r\nfrom usuario inner join empresa on empresa.Fk_EmpUsuario = usuario.id_usuario;";
                
                SqlDataAdapter DbList = new SqlDataAdapter();
                DataTable DtList = new DataTable();

                DbList.SelectCommand = cmd;
                DbList.Fill(DtList);

                tabelaReladorio.DataSource = DtList;

                url.desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }

            try
            {
                cmd.Connection = url.conectar();
                cmd.CommandText = "select count (id_usuario) as Funcionários_Cadastrados from usuario;";

                SqlDataAdapter DbList = new SqlDataAdapter();
                DataTable DtList = new DataTable();

                DbList.SelectCommand = cmd;
                DbList.Fill(DtList);

                dtContador.DataSource = DtList;

                url.desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnImprimirRelatFunc_Click(object sender, EventArgs e)
        {
            var dt = GerarDadosRelatorioUser();
            using (var rltFuncionario = new RelatFuncionario(dt))
            {
                rltFuncionario.ShowDialog();
            }
        }

        public DataTable GerarDadosRelatorioUser()
        {
            var dt = new DataTable();
            dt.Columns.Add("nome");
            dt.Columns.Add("matricula", typeof (int));
            dt.Columns.Add("perfil");
            dt.Columns.Add("email");
            dt.Columns.Add("status");
            dt.Columns.Add("razao_social");
            
            foreach (DataGridViewRow item in tabelaReladorio.Rows)
            {
                dt.Rows.Add(item.Cells["nome"].Value,item.Cells["matricula"].Value,item.Cells["perfil"].Value, item.Cells["email"].Value,item.Cells["status"].Value,item.Cells["razao_social"].Value);
            }
            return dt;
        }

        private void Relatorio_Load(object sender, EventArgs e)
        {

        }

        private void btnRltEmpresa_Click(object sender, EventArgs e)
        {
            btnImprimirRelatFunc.Enabled = false;
            btnImprimirRelatEmpre.Enabled = true;
            btnImprimirFolha.Enabled = false;

            SqlCommand cmd = new SqlCommand();
            ConexaoDb url = new ConexaoDb();
            
            try
            {
                cmd.Connection = url.conectar();
                cmd.CommandText = "select nome_empresa as Nome_Empresa,razao_social as Razão_Social,cnpj_empresa as CNPJ,telefone_empresa as Telefone,estado_empresa as Estado from empresa;";

                SqlDataAdapter DbList = new SqlDataAdapter();
                DataTable DtList = new DataTable();

                DbList.SelectCommand = cmd;
                DbList.Fill(DtList);

                tabelaReladorio.DataSource = DtList;

                url.desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        private void btnImprimirRelatEmpre_Click(object sender, EventArgs e)
        {
            var dt = GerarDadosRelatorioEmpresa();
            using (var rltFuncionarioEmpresa = new RelatEmpresa(dt))
            {
                rltFuncionarioEmpresa.ShowDialog();
            }
        }

        public DataTable GerarDadosRelatorioEmpresa()
        {
            var dt = new DataTable();
            dt.Columns.Add("nome_empresa");
            dt.Columns.Add("razao_social");
            dt.Columns.Add("cnpj_empresa");
            dt.Columns.Add("estado_empresa");
            dt.Columns.Add("telefone_empresa");

            foreach (DataGridViewRow item in tabelaReladorio.Rows)
            {
                dt.Rows.Add(item.Cells["Nome_Empresa"].Value, item.Cells["Razão_Social"].Value, item.Cells["CNPJ"].Value, item.Cells["Estado"].Value, item.Cells["Telefone"].Value);
            }
            return dt;
        }

        private void btnBuscarUser_Click(object sender, EventArgs e)
        {
            btnImprimirRelatFunc.Enabled = false;
            btnImprimirRelatEmpre.Enabled = false;
            btnImprimirFolha.Enabled = true;

            SqlCommand cmd = new SqlCommand();
            ConexaoDb url = new ConexaoDb();

            try
            {
                cmd.Connection = url.conectar();
                cmd.CommandText = "select * from ConsultFolha;";
                //cmd.CommandText = "select * from ConsultFolha where matricula = @matricula and mes_referencia = @mes_referencia and ano_referencia = @ano_referencia;";
                //cmd.Parameters.AddWithValue("@matricula",txtMatricula.Text);
                //cmd.Parameters.AddWithValue("@mes_referencia", ComMesRef.SelectedItem);
                //cmd.Parameters.AddWithValue("@ano_referencia", txtAnoRef.Text);
                SqlDataAdapter DbList = new SqlDataAdapter();
                DataTable DtList = new DataTable();

                DbList.SelectCommand = cmd;
                DbList.Fill(DtList);

                tabelaReladorio.DataSource = DtList;

                url.desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        private void btnImprimirFolha_Click(object sender, EventArgs e)
        {
            var dt = GerarDadosRelatorioFolhaPagamento();
            using (var rltFuncionarioFolhaPag = new RelatFolhaPagamento(dt))
            {
                rltFuncionarioFolhaPag.ShowDialog();
            }
        }

        public DataTable GerarDadosRelatorioFolhaPagamento()
        {
            var dt = new DataTable();
            dt.Columns.Add("nome");
            dt.Columns.Add("matricula", typeof(int));
            dt.Columns.Add("cpf");
            dt.Columns.Add("nome_empresa");
            dt.Columns.Add("cnpj_empresa");
            dt.Columns.Add("cargo");
            dt.Columns.Add("Mes_Referente");
            dt.Columns.Add("Ano_Referente", typeof(int));

            dt.Columns.Add("Horas_Trabalhada");
            dt.Columns.Add("Falta");
            dt.Columns.Add("Valor_Hora");
            dt.Columns.Add("Hora_Extra");
            dt.Columns.Add("Qtd_HorasExtras");
            dt.Columns.Add("Salario_Bruto");
            dt.Columns.Add("Irrf");
            dt.Columns.Add("Inss");
            dt.Columns.Add("beneficio");
            dt.Columns.Add("desconto");
            dt.Columns.Add("Salario_Liquido");

            foreach (DataGridViewRow item in tabelaReladorio.Rows)
            {
                dt.Rows.Add(item.Cells["nome"].Value, item.Cells["matricula"].Value, item.Cells["cpf"].Value, item.Cells["razao_social"].Value, item.Cells["cnpj_empresa"].Value, item.Cells["Cargo"].Value, item.Cells["mes_referencia"].Value, item.Cells["ano_referencia"].Value, item.Cells["horas_trabalhadas"].Value, item.Cells["Falta"].Value, item.Cells["valor_hora"].Value, item.Cells["horas_extras"].Value, item.Cells["qtd_HorasHextras"].Value, item.Cells["salario_bruto"].Value, item.Cells["irrf"].Value, item.Cells["inss"].Value, item.Cells["beneficio"].Value, item.Cells["desconto"].Value, item.Cells["salario_liquido"].Value);
            }
            return dt;
        }
    }
}
