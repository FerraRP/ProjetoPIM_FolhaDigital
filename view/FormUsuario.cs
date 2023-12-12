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

namespace FolhaFigital_Projeto.view
{
    public partial class FormUsuario : Form
    {
        public FormUsuario()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            Menu MenuInicial = new Menu();
            MenuInicial.Show();

            this.Hide();
        }
        public void LimparCampos()
        {
            //Usuario
            txtNome.Clear();
            ComPerfil.SelectedIndex = -1;
            txtSenha.Clear();
            txtTelefone.Clear();
            txtMatricula.Clear();
            txtEmail.Clear();
            txtDataNascimento.CustomFormat = " ";
            txtCpf.Clear();
            ComStatusUser.SelectedIndex = -1;

            //Empresa
            txtEmpCnpj.Clear();
            txtEmpNome.Clear();
            txtEmpRazaoSocial.Clear();
            txtEmpTelefone.Clear();
            txtEmpLogradouro.Clear();
            TxtEmpNumero.Clear();
            txtEmpBairro.Clear();
            txtEmpCidade.Clear();
            txtEmpEstado.Clear();

            //Endereço
            txtEndCep.Clear();
            txtEndLogradouro.Clear();
            txtEndNumero.Clear();
            txtEndBairro.Clear();
            txtEndCidade.Clear();
            txtEndEstado.Clear();
            txtEndComplemento.Clear();
            ComTipoConta.SelectedIndex = -1;

            //Banco
            txtBanco.Clear();
            txtAgencia.Clear();
            txtConta.Clear();
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void btInserir_Click(object sender, EventArgs e)
        {
            UseCadastrar CadUsuario = new UseCadastrar();
            EnderecoCadastrar CadEnderec = new EnderecoCadastrar();
            BancoCadastrar CadBanc = new BancoCadastrar();
            EmpresaCadastrar CadEmpre = new EmpresaCadastrar();


            if (txtConfirmaSenha.Text == "")
            {
                MessageBox.Show("Obrigatório o preenchiemento do campo 'Confirmar Senha'", "Atenção!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if(txtConfirmaSenha.Text != txtSenha.Text)
            {
                MessageBox.Show("Os valores de Senha e Confirma senha devem ser iguais", "Atenção!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                UsuarioBean useEnt = new UsuarioBean(txtNome.Text, Convert.ToInt32(txtMatricula.Text), txtEmail.Text, Convert.ToInt64(txtTelefone.Text), Convert.ToDateTime(txtDataNascimento.Text), Convert.ToInt64(txtCpf.Text), ComPerfil.SelectedItem.ToString(), txtSenha.Text, ComStatusUser.SelectedItem.ToString());
                try
                {

                    String mensagem = CadUsuario.InserirUsuario(useEnt);
                    if (CadUsuario.logValidado)
                    {
                        MessageBox.Show(mensagem, "Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show(CadUsuario.mensagem);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


                EmpresaBean EmpEnt = new EmpresaBean(txtEmpCnpj.Text, txtEmpNome.Text, txtEmpRazaoSocial.Text, txtEmpTelefone.Text, txtEmpLogradouro.Text, Convert.ToInt32(TxtEmpNumero.Text), txtEmpBairro.Text, txtEmpCidade.Text, txtEmpEstado.Text);
                try
                {
                    String mensagem = CadEmpre.InserirEmpresa(EmpEnt, useEnt);
                    if (CadEmpre.EmpresaValidado)
                    {
                        MessageBox.Show(mensagem, "Cadastro dos dados da empresa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show(CadEmpre.mensagem);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                EnderecoBean EnderEnt = new EnderecoBean(Convert.ToInt32(txtEndCep.Text), txtEndLogradouro.Text, Convert.ToInt32(txtEndNumero.Text), txtEndBairro.Text, txtEndCidade.Text, txtEndEstado.Text, txtEndComplemento.Text);
                try
                {

                    String mensagem = CadEnderec.InserirEndereco(EnderEnt, useEnt);
                    if (CadEnderec.EnderValidado)
                    {
                        MessageBox.Show(mensagem, "Cadastro de endereço", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show(CadUsuario.mensagem);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                BancoBean BancEnt = new BancoBean(txtBanco.Text, ComTipoConta.SelectedItem.ToString(), Convert.ToInt32(txtAgencia.Text), Convert.ToInt32(txtConta.Text));
                try
                {

                    String mensagem = CadBanc.InserirBanco(BancEnt, useEnt);
                    if (CadBanc.BancoValidado)
                    {
                        MessageBox.Show(mensagem, "Cadastro dos dados bancarios", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(CadUsuario.mensagem);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                LimparCampos();

            }

        }

        private void btnConsultaUse_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            ConexaoDb url = new ConexaoDb();


            try
            {
                cmd.Connection = url.conectar();
                cmd.CommandText = "select * from buscarUser;";
                
                SqlDataAdapter DbList = new SqlDataAdapter();
                DataTable DtList = new DataTable();

                DbList.SelectCommand = cmd;
                DbList.Fill(DtList);

                listFormUsuario.DataSource = DtList;

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


            if (txtConsultaMatri.Text != "")
            {
                try
                {
                    cmd.Connection = url.conectar();
                    cmd.CommandText = "select * from buscarUser where matricula like  ('%" + txtConsultaMatri.Text + "%')";
                    SqlDataAdapter DbList = new SqlDataAdapter();
                    DataTable DtList = new DataTable();

                    DbList.SelectCommand = cmd;
                    DbList.Fill(DtList);

                    listFormUsuario.DataSource = DtList;

                    url.desconectar();
                }
                catch (Exception erro)
                {
                    MessageBox.Show(erro.Message);
                }
            }
            else
            {
                listFormUsuario.DataSource = null;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CarregarCampos()
        {
            //Usuario
            txtIdUser.Text = listFormUsuario.SelectedRows[0].Cells[0].Value.ToString();
            txtNome.Text = listFormUsuario.SelectedRows[0].Cells[1].Value.ToString();
            txtMatricula.Text = listFormUsuario.SelectedRows[0].Cells[2].Value.ToString();
            txtEmail.Text = listFormUsuario.SelectedRows[0].Cells[3].Value.ToString();
            txtTelefone.Text = listFormUsuario.SelectedRows[0].Cells[4].Value.ToString();
            txtDataNascimento.Text = listFormUsuario.SelectedRows[0].Cells[5].Value.ToString();
            txtCpf.Text = listFormUsuario.SelectedRows[0].Cells[6].Value.ToString();
            string Perfil = listFormUsuario.SelectedRows[0].Cells[7].Value.ToString();
            if(Perfil == "Administrativo")
            {
                ComPerfil.SelectedIndex = 0;
            }
            else
            {
                ComPerfil.SelectedIndex = 1;
            }
            txtSenha.Text = listFormUsuario.SelectedRows[0].Cells[8].Value.ToString();
            string StatusUser = listFormUsuario.SelectedRows[0].Cells[9].Value.ToString();
            if (StatusUser == "Ativo")
            {
                ComStatusUser.SelectedIndex = 0;
            }
            else
            {
                ComStatusUser.SelectedIndex = 1;
            }
            //Banco
            txtBanco.Text = listFormUsuario.SelectedRows[0].Cells[10].Value.ToString();
            string TipoConta = listFormUsuario.SelectedRows[0].Cells[11].Value.ToString();
            if (TipoConta == "Conta corrente")
            {
                ComTipoConta.SelectedIndex = 0;
            }

            else if (TipoConta == "Conta poupança")
            {
                ComTipoConta.SelectedIndex = 1;
            }
            else
            {
                ComTipoConta.SelectedIndex = 2;
            }
            txtAgencia.Text = listFormUsuario.SelectedRows[0].Cells[12].Value.ToString();           
            txtConta.Text = listFormUsuario.SelectedRows[0].Cells[13].Value.ToString();

            //Endereço
            txtEndCep.Text = listFormUsuario.SelectedRows[0].Cells[14].Value.ToString();
            txtEndLogradouro.Text = listFormUsuario.SelectedRows[0].Cells[15].Value.ToString();
            txtEndNumero.Text = listFormUsuario.SelectedRows[0].Cells[16].Value.ToString();
            txtEndBairro.Text = listFormUsuario.SelectedRows[0].Cells[17].Value.ToString();
            txtEndCidade.Text = listFormUsuario.SelectedRows[0].Cells[18].Value.ToString();
            txtEndEstado.Text = listFormUsuario.SelectedRows[0].Cells[19].Value.ToString();
            txtEndComplemento.Text = listFormUsuario.SelectedRows[0].Cells[20].Value.ToString();

            //Empresa
            txtEmpCnpj.Text = listFormUsuario.SelectedRows[0].Cells[21].Value.ToString();
            txtEmpNome.Text = listFormUsuario.SelectedRows[0].Cells[22].Value.ToString();
            txtEmpRazaoSocial.Text = listFormUsuario.SelectedRows[0].Cells[23].Value.ToString();
            txtEmpTelefone.Text = listFormUsuario.SelectedRows[0].Cells[24].Value.ToString();
            txtEmpLogradouro.Text = listFormUsuario.SelectedRows[0].Cells[25].Value.ToString();
            TxtEmpNumero.Text = listFormUsuario.SelectedRows[0].Cells[26].Value.ToString();
            txtEmpBairro.Text = listFormUsuario.SelectedRows[0].Cells[27].Value.ToString();
            txtEmpCidade.Text = listFormUsuario.SelectedRows[0].Cells[28].Value.ToString();
            txtEmpEstado.Text = listFormUsuario.SelectedRows[0].Cells[29].Value.ToString();




            btInserir.Enabled = false;
            btnAlterar.Enabled = true;
            btnExcluir.Enabled = true;

        }
        private void listUsuario_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            CarregarCampos();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            UseEditar EditUsuario = new UseEditar();
            EnderecoCadastrar CadEnderec = new EnderecoCadastrar();
            BancoEditar EditBanc = new BancoEditar();
            EmpresaCadastrar CadEmpre = new EmpresaCadastrar();

            UsuarioBean useEnt = new UsuarioBean(txtNome.Text, Convert.ToInt32(txtMatricula.Text), txtEmail.Text, Convert.ToInt64(txtTelefone.Text), Convert.ToDateTime(txtDataNascimento.Text), Convert.ToInt64(txtCpf.Text), ComPerfil.SelectedItem.ToString(), txtSenha.Text, ComStatusUser.SelectedItem.ToString(), Convert.ToInt32(txtIdUser.Text));
            try
            {

                String mensagem = EditUsuario.AlterarUsuario(useEnt);
                if (EditUsuario.EditarUserValidado)
                {
                    MessageBox.Show(mensagem, "Etitado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show(EditUsuario.mensagem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            BancoBean BancEnt = new BancoBean(txtBanco.Text, ComTipoConta.SelectedItem.ToString(), Convert.ToInt32(txtAgencia.Text), Convert.ToInt32(txtConta.Text));
            try
            {

                String mensagem = EditBanc.AlterarBanco(BancEnt, useEnt);
                if (EditBanc.BancoEditValidado)
                {
                    MessageBox.Show(mensagem, "Cadastro dos dados bancarios", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(EditBanc.mensagem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            UserExcluir userExcluir = new UserExcluir();
            BancoEsxcluir bancExcluir = new BancoEsxcluir();
            EmpresaExcluir empExcluir = new EmpresaExcluir();
            EnderecoExcluir enderExcluir = new EnderecoExcluir();

            int FkUsuario = Convert.ToInt32(listFormUsuario.SelectedRows[0].Cells[0].Value.ToString());

            try
            {

                String mensagem = bancExcluir.ExcluirBanco(FkUsuario);
                if (bancExcluir.ExcluirBancoValidado)
                {
                    MessageBox.Show(mensagem, "Excluir", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show(userExcluir.mensagem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            try
            {

                String mensagem = empExcluir.ExcluirEmpresa(FkUsuario);
                if (empExcluir.ExcluirEmpresaValidado)
                {
                    MessageBox.Show(mensagem, "Excluir", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show(empExcluir.mensagem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            try
            {

                String mensagem = enderExcluir.ExcluirEndereco(FkUsuario);
                if (enderExcluir.ExcluirEnderecoValidado)
                {
                    MessageBox.Show(mensagem, "Excluir", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show(enderExcluir.mensagem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            try
            {

                String mensagem = userExcluir.ExcluirUsuario(FkUsuario);
                if (userExcluir.ExcluirUserValidado)
                {
                    MessageBox.Show(mensagem, "Excluir", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show(userExcluir.mensagem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnSenha_MouseDown(object sender, MouseEventArgs e)
        {
            txtSenha.UseSystemPasswordChar = false;
        }

        private void btnSenha_MouseUp(object sender, MouseEventArgs e)
        {
            txtSenha.UseSystemPasswordChar = true;
        }

        private void btnVoltar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnSenha_Click(object sender, EventArgs e)
        {

        }

        private void ComPerfil_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtIdUser_Click(object sender, EventArgs e)
        {

        }

        private void txtEmpTelefone_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void TxtEmpNumero_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void txtEndNumero_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void ComTipoConta_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtAgencia_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void ComStatusUser_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtMatricula_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtEmpBairro_TextChanged(object sender, EventArgs e)
        {

        }

        private void label40_Click(object sender, EventArgs e)
        {

        }

        private void label41_Click(object sender, EventArgs e)
        {

        }

        private void txtEmpLogradouro_TextChanged(object sender, EventArgs e)
        {

        }

        private void label32_Click(object sender, EventArgs e)
        {

        }

        private void txtEmpCidade_TextChanged(object sender, EventArgs e)
        {

        }

        private void label33_Click(object sender, EventArgs e)
        {

        }

        private void txtEmpNome_TextChanged(object sender, EventArgs e)
        {

        }

        private void label34_Click(object sender, EventArgs e)
        {

        }

        private void txtEmpCnpj_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void txtEmpRazaoSocial_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEmpEstado_TextChanged(object sender, EventArgs e)
        {

        }

        private void label35_Click(object sender, EventArgs e)
        {

        }

        private void label36_Click(object sender, EventArgs e)
        {

        }

        private void label37_Click(object sender, EventArgs e)
        {

        }

        private void label38_Click(object sender, EventArgs e)
        {

        }

        private void label39_Click(object sender, EventArgs e)
        {

        }

        private void txtEndComplemento_TextChanged(object sender, EventArgs e)
        {

        }

        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void txtEndCidade_TextChanged(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void txtEndLogradouro_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtEndCep_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void txtEndBairro_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEndEstado_TextChanged(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void txtConta_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void txtBanco_TextChanged(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void txtTelefone_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void txtCpf_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void txtSenha_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listUsuario_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void txtDataNascimento_ValueChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            txtConfirmaSenha.UseSystemPasswordChar = false;
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            txtConfirmaSenha.UseSystemPasswordChar = true;
        }
    }
}