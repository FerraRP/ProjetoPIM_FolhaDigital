use db_HexTec;

create table [usuario]
(
	[id_usuario] integer PRIMARY KEY identity,
	[nome] nvarchar(255),
	[matricula] integer,
	[email] nvarchar(255),
	[telefone] nvarchar(12),
	[data_nascimento] datetime, 
	[cpf] nvarchar(12),
	[perfil] nvarchar(255),
	[senha] nvarchar(255),
	[status] nvarchar(255)
);

create table [endereco](
	[id_endereco] integer PRIMARY KEY identity,
	[Fk_EndUsuario] integer,
	[cep] nvarchar(12),
	[Logradouro] nvarchar(255),
	[numero] integer,
	[bairro] nvarchar(255),
	[cidade] nvarchar(255),
	[estado] nvarchar(255),
	[complemento] nvarchar(255)
	foreign key (Fk_EndUsuario) references usuario (id_usuario)
);

CREATE TABLE [banco] (
	[id_conta] integer PRIMARY KEY identity,
	[Fk_ContUsuario] integer,
	[nome_banco] nvarchar(255),
	[tipo_conta] nvarchar(255),
	[agencia] integer,
	[conta] integer,
	[data_pagamento] datetime,
	foreign key (Fk_ContUsuario) references usuario (id_usuario)
);
CREATE TABLE [empresa] (
	[id_empresa] integer PRIMARY KEY identity,
	[Fk_EmpUsuario] integer,
	[cnpj_empresa] nvarchar(20),
	[nome_empresa] nvarchar(255),
	[razao_social] nvarchar(255),
	[telefone_empresa] nvarchar(20),
	[logradouro_empresa] nvarchar(255),
	[numero_empresa] integer,
	[bairro_empresa] nvarchar(255),
	[cidade_empresa] nvarchar(255),
	[estado_empresa] nvarchar(255)
	foreign key (Fk_EmpUsuario) references usuario (id_usuario)
);

insert into empresa (Fk_EmpUsuario,cnpj_empresa,nome,razao_social,telefone_empresa,logradouro_empresa,numero_empresa,bairro_empresa,cidade_empresa,estado_empresa) 
values (1,46464,'ede','eded',65464,'dede',55,'fsdfsf','fsdfsf','fsdfsf'); 

insert into banco(Fk_ContUsuario,nome_banco,tipo_conta,agencia,conta) 
values (2,'d6564sd','dad', 454544, 46464);

insert into usuario (nome, matricula, email, telefone, data_nascimento, cpf, senha, perfil, status) 
values ('UserTeste222', 651651, 'adm', 454544, 18/12/2001,  46464, 'adm', 'jose', 'jose');
GO

insert into endereco (Fk_EndUsuario, cep, Logradouro, numero, bairro, cidade, estado, complemento) 
values (7, 8165, 'sdad', 66, 'adasa', 'dadasd', 'asda',  'daadad'); GO

insert into banco (Fk_ContUsuario, nome_banco,tipo_conta,agencia,conta,data_pagamento) values (1,'ssds','dfd',5656,5646,18/12/20001);


select * from  usuario;
select * from  endereco;
select * from banco;
select * from empresa;

delete from usuario where id_usuario = ();

select usuario.nome,usuario.matricula,usuario.email,usuario.telefone,usuario.data_nascimento,usuario.cpf,usuario.senha,usuario.perfil,usuario.status,
       banco.nome_banco,banco.tipo_conta,banco.agencia,banco.conta,banco.data_pagamento 
from usuario inner join banco on banco.FK_ContUsuario = usuario.id_usuario 
where usuario.nome like '%coisdd%';

update usuario set nome='Gustavo',matricula=525252,email='gustavo@' where id_usuario = 2;

update banco set nome_banco = 'bra',tipo_conta = 'corr',agencia = 515,conta = 6516 where Fk_ContUsuario = 3;

delete from banco where Fk_ContUsuario = 3;

delete from usuario where id_usuario = 3;

select * from usuario where email = 'Teste' and senha = 'Teste' and perfil = 'Administrativo' and status = 'Ativo';

update usuario set ativo = 1 where id_usuario = 1;

CREATE VIEW buscarUser as
select usuario.id_usuario,usuario.nome,usuario.matricula,usuario.email,usuario.telefone,usuario.data_nascimento,usuario.cpf,usuario.perfil,usuario.senha,usuario.status,
banco.nome_banco,banco.tipo_conta,banco.agencia,banco.conta,
endereco.cep,endereco.Logradouro,endereco.numero,endereco.bairro,endereco.cidade,endereco.estado,endereco.complemento,
empresa.cnpj_empresa,empresa.nome_empresa,empresa.razao_social,empresa.telefone_empresa,empresa.logradouro_empresa,empresa.numero_empresa,empresa.bairro_empresa,empresa.cidade_empresa,empresa.estado_empresa

from usuario inner join banco on banco.FK_ContUsuario = usuario.id_usuario inner join endereco on endereco.Fk_EndUsuario = usuario.id_usuario inner join empresa on empresa.Fk_EmpUsuario = usuario.id_usuario;

select * from buscarUser;


-- FOLHA DE PAGAMENTO --
CREATE TABLE [folha_pagamento] (
	[id_folhaPagamento] integer PRIMARY KEY identity,
	[Fk_FolhUsuario] integer,
	[horas_trabalhadas] float(30),
	[valor_hora] money,
	[inss] float(10),
	[irrf] float(10),
	[beneficio] varchar(5),
	[desconto] float (10),
	[horas_extras] varchar(5),
	[qtd_HorasHextras] float(10),
	[Falta] varchar(5),
	[Motivo_Falta] varchar(80),
	[data_pagamento] datetime,
	[mes_referencia] varchar(15),
	[ano_referencia] integer,
	foreign key (Fk_FolhUsuario) references usuario (id_usuario)
);

CREATE TABLE [salario] (
	[id_salario] integer PRIMARY KEY identity,
	[Fk_SalUsuario] integer,
	[cargo] nvarchar(255),
	[salario_bruto] money,
	[salario_liquido] money,
	foreign key (Fk_SalUsuario) references usuario (id_usuario)
);
create table [ponto_usuario]
(
	[id_ponto] integer PRIMARY KEY identity,
	[FkPontUsuario] integer,
	[horario_entrada] float(6),
	[horario_saida] float(6),
	[data_ponto] nvarchar (12)
);

select * from salario;
select * from folha_pagamento;



CREATE VIEW ConsultFolha as
select usuario.nome,usuario.matricula,usuario.cpf,
empresa.cnpj_empresa,empresa.razao_social,
salario.cargo,salario.salario_bruto,salario.salario_liquido,
folha_pagamento.data_pagamento,folha_pagamento.mes_referencia,folha_pagamento.ano_referencia,folha_pagamento.qtd_HorasHextras,folha_pagamento.horas_extras,folha_pagamento.horas_trabalhadas,folha_pagamento.valor_hora,folha_pagamento.inss,folha_pagamento.irrf,folha_pagamento.Falta,folha_pagamento.Motivo_Falta,folha_pagamento.beneficio,folha_pagamento.desconto
from usuario inner join empresa on empresa.Fk_EmpUsuario = usuario.id_usuario inner join salario on salario.Fk_SalUsuario = usuario.id_usuario inner join folha_pagamento on folha_pagamento.Fk_FolhUsuario =  usuario.id_usuario;

insert into folha_pagamento (Fk_FolhUsuario,horas_trabalhadas,valor_hora,inss,irrf,horas_extras,qtd_HorasHextras,Falta,Motivo_Falta,data_pagamento,mes_referencia,ano_referencia)
values (2,220,15,7.5,14,'NÂO',0,'ND','Nada',18/12/2023,'Fevereiro',2023);

select * from  ConsultFolha;


insert into ponto_usuario (FkPontUsuario, horario_entrada, horario_saida, data_ponto)  values (1,5,4,'04/02/2023');
select *  from ponto_usuario;

select FkPontUsuario, sum (horario_entrada + horario_saida) as TotalHoras from ponto_usuario group by FkPontUsuario;

CREATE VIEW ConsultPag as
select usuario.id_usuario,usuario.nome,usuario.matricula,usuario.perfil,empresa.nome_empresa, sum (ponto_usuario.horario_entrada + ponto_usuario.horario_saida) as HorasTotal from usuario inner join empresa on empresa.Fk_EmpUsuario = usuario.id_usuario inner join ponto_usuario on ponto_usuario.FkPontUsuario = usuario.id_usuario  group by usuario.id_usuario,usuario.nome,usuario.matricula,usuario.perfil,empresa.nome_empresa;

select * from ConsultPag where matricula = 