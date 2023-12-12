using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolhaFigital_Projeto.model.bean.Relatorios
{
    internal class RelatFuncBean
    {   
        public string nome { get; set; }
        public int matricula { get; set; }
        public string email { get; set; }
        public string perfil { get; set; }
        public string status { get; set; }
        public string razao_social { get; set; }
        public int Num_Usuario { get; set; }

        public RelatFuncBean(string Nome, int Matricula, string Email, string Perfil, string Status, string RazaoSocial, int NumUsuario)
        {
            this.nome = Nome;
            this.matricula = Matricula;
            this.email = Email;
            this.perfil = Perfil;
            this.status = Status;
            this.razao_social = RazaoSocial;
            this.Num_Usuario = NumUsuario;
        }
    }
}
