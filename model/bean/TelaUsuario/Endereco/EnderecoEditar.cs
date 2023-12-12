using FolhaFigital_Projeto.controller.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FolhaFigital_Projeto.model.bean.TelaUsuario.Banco
{
    internal class EnderecoEditar
    {
        public bool EnderecoEditValidado;
        public String mensagem = "";

        public String AlterarEndereco(EnderecoBean Endrent, UsuarioBean useEnt)
        {
            ValidaEndereco EnderecoEditarValida = new ValidaEndereco();
            this.mensagem = EnderecoEditarValida.AlterarEndereco(Endrent, useEnt);


            MessageBox.Show("modelo de endereço ok");


            return mensagem;
        }
    }
}
