﻿using FolhaFigital_Projeto.model.bean;
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

namespace FolhaFigital_Projeto.controller.Login
{
    internal class ValidaLogin
    {
        public bool logValidado;
        public String mensagem = "";
        SqlCommand cmd = new SqlCommand();
        ConexaoDb url = new ConexaoDb();
        SqlDataReader LerDados;

        public bool verificarlogin(String email, String senha)
        {
            cmd.CommandText = "select * from usuario where email = @email and senha = @senha and perfil = 'Administrativo' and status = 'Ativo'";
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@senha", senha);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable tabela = new DataTable();

            try
            {
                cmd.Connection = url.conectar();
                LerDados = cmd.ExecuteReader();
                if (LerDados.HasRows)
                {
                    logValidado = true;
                }
                url.desconectar();

            }
            catch (Exception ex)
            {
                this.mensagem = "Erro de acesso";
            }

            return logValidado;
        }
    }
}
