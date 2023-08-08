-- CRIANDO O BANCO DE DADOS
CREATE DATABASE FUNEC2023_DAII;
--@ CHAVE PRIM�RIA
--# CHAVE ESTRANGEIRA

-- SEXO = {@CODSEXO, NOMESEXO}
CREATE TABLE SEXO(
	CODSEXO INTEGER IDENTITY(1,1) PRIMARY KEY,
	NOMESEXO VARCHAR(9) NOT NULL UNIQUE
);

--CIDADE={@CODCIDADE, NOMECIDADE}
CREATE TABLE CIDADE(
	CODCIDADE INTEGER IDENTITY(1,1) PRIMARY KEY,
	NOMECIDADE VARCHAR(80) NOT NULL UNIQUE
);

--RUA={@CODRUA, NOMERUA}
CREATE TABLE RUA(
	CODRUA INTEGER IDENTITY(1,1) PRIMARY KEY,
	NOMERUA VARCHAR(80) NOT NULL UNIQUE
);

--BAIRRO={@CODBAIRRO, NOMEBAIRRO}
CREATE TABLE BAIRRO(
	CODBAIRRO INTEGER IDENTITY(1,1) PRIMARY KEY,
	NOMEBAIRRO VARCHAR(80) NOT NULL UNIQUE
);

--CEP={@CODCEP,NUMEROCEP}
CREATE TABLE CEP(
	CODCEP INTEGER IDENTITY(1,1) PRIMARY KEY,
	NUMEROCEP CHAR(9) NOT NULL UNIQUE
);

--ESTADO={@CODESTADO, NOMESTADO, SIGLA}
CREATE TABLE ESTADO(
	CODESTADO INTEGER IDENTITY(1,1) PRIMARY KEY,
	NOMEESTADO VARCHAR(80) NOT NULL UNIQUE,
	SIGLA CHAR(2) NOT NULL UNIQUE
);

--CLIENTE=(@CODCLIENTE, NOMECLIENTE, DATANASC, CPF, CODSEXOFK,
-- CODCIDADE_FK, CODRUA_FK, CODBAIRRO_FK, CODCEP_FK, NUMEROCASA,
-- CODESTADO_FK)
CREATE TABLE CLIENTE(
	CODCLIENTE INTEGER IDENTITY(1,1) PRIMARY KEY,
	NOMECLIENTE VARCHAR(80) NOT NULL,
	DATANASC DATE NOT NULL,
	CPF CHAR(14) NOT NULL UNIQUE,
	CODSEXO_FK INTEGER REFERENCES SEXO(CODSEXO) ON DELETE CASCADE
	           ON UPDATE CASCADE,
	CODCIDADE_FK INTEGER REFERENCES CIDADE(CODCIDADE) ON DELETE CASCADE
	           ON UPDATE CASCADE,
	CODRUA_FK INTEGER REFERENCES RUA(CODRUA) ON DELETE CASCADE
	           ON UPDATE CASCADE,
	CODBAIRRO_FK INTEGER REFERENCES BAIRRO(CODBAIRRO) ON DELETE CASCADE
	           ON UPDATE CASCADE,
	CODCEP_FK INTEGER REFERENCES CEP(CODCEP) ON DELETE CASCADE
	           ON UPDATE CASCADE,
	CODESTADO_FK INTEGER REFERENCES ESTADO(CODESTADO) ON DELETE CASCADE
	           ON UPDATE CASCADE
);

-- TELEFONE={@CODTELEFONE, NUMEROTELEFONE}
CREATE TABLE TELEFONE(
	CODTELEFONE INTEGER IDENTITY(1,1) PRIMARY KEY,
	NUMEROTELEFONE VARCHAR(14) NOT NULL UNIQUE
);

--TELEFONECLIENTE={@(CODTELEFONE_FK, CODCLIENTE_FK)}
CREATE TABLE TELEFONECLIENTE(
	CODTELEFONE_FK INTEGER REFERENCES TELEFONE(CODTELEFONE) ON DELETE CASCADE 
	               ON UPDATE CASCADE,
	CODCLIENTE_FK INTEGER REFERENCES CLIENTE(CODCLIENTE) ON DELETE CASCADE
	               ON UPDATE CASCADE,
	PRIMARY KEY(CODTELEFONE_FK, CODCLIENTE_FK)
);

-- VENDA = {@CODVENDA, DATAVENDA, CODCLIENTE_FK}
CREATE TABLE VENDA(
	CODVENDA INTEGER IDENTITY(1,1) PRIMARY KEY,
	DATAVENDA DATE NOT NULL,
	CODCLIENTE_FK INTEGER REFERENCES CLIENTE(CODCLIENTE) ON DELETE CASCADE
	              ON UPDATE CASCADE
);

-- MARCA = {@CODMARCA, NOMEMARCA}
CREATE TABLE MARCA(
	CODMARCA INTEGER IDENTITY(1,1) PRIMARY KEY,
	NOMEMARCA VARCHAR(80) NOT NULL UNIQUE
);

-- TIPO_PRODUTO={@CODTIPOPRODUTO, NOMETIPOPRODUTO}
CREATE TABLE TIPOPRODUTO(
	CODTIPOPRODUTO INTEGER IDENTITY(1,1) PRIMARY KEY,
	NOMETIPOPRODUTO VARCHAR(80) NOT NULL UNIQUE
);

--PRODUTO={@CODPRODUTO, NOMEPRODUTO, QUANT, VALOR, CODMARCA_FK, CODTIPOPRODUTO_FK}
CREATE TABLE PRODUTO(
	CODPRODUTO INTEGER IDENTITY(1,1) PRIMARY KEY,
	NOMEPRODUTO VARCHAR(80) NOT NULL,
	QUANT NUMERIC(10,2) NOT NULL,
	VALOR NUMERIC(10,2) NOT NULL,
	CODMARCA_FK INTEGER REFERENCES MARCA(CODMARCA) ON DELETE CASCADE
	            ON UPDATE CASCADE,
	CODTIPOPRODUTO_FK INTEGER REFERENCES TIPOPRODUTO(CODTIPOPRODUTO) 
	                  ON DELETE CASCADE ON UPDATE CASCADE
);

-- VENDAPRODUTO={@(CODVENDA_FK, CODPRODUTO_FK), QUANTV, VALORV}
CREATE TABLE VENDAPRODUTO(
	CODVENDA_FK INTEGER REFERENCES VENDA(CODVENDA) ON DELETE CASCADE
	            ON UPDATE CASCADE,
	CODPRODUTO_FK INTEGER REFERENCES PRODUTO(CODPRODUTO) ON DELETE CASCADE
	            ON UPDATE CASCADE,
	QUANTV NUMERIC(10,2) NOT NULL,
	VALORV NUMERIC(10,2) NOT NULL,
	PRIMARY KEY(CODVENDA_FK, CODPRODUTO_FK)
);

--FOTOPRODUTO={@CODFOTO, DESCRICAO, IMAGEM, CODPRODUTO_FK}
CREATE TABLE FOTOPRODUTO(
	CODFOTO INTEGER IDENTITY(1,1) PRIMARY KEY,
	DESCRICAO VARCHAR(255) NOT NULL,
	IMAGEM VARBINARY(MAX) NOT NULL,
	CODPRODUTO_FK INTEGER REFERENCES PRODUTO(CODPRODUTO) ON DELETE CASCADE
	              ON UPDATE CASCADE
);

--SITUACAO={CODSITUACAO, NOMESITUACAO}
CREATE TABLE SITUACAO(
	CODSITUACAO INTEGER IDENTITY(1,1) PRIMARY KEY,
	NOMESITUACAO VARCHAR(40) NOT NULL UNIQUE
);
--PARCELAVENDA={@CODPARCELA, DATAVENCIMENTO, VALORPARCELA, CODSITUACAO_FK
--CODVENDA_FK}
CREATE TABLE PARCELAVENDA(
	CODPARCELA INTEGER IDENTITY(1,1) PRIMARY KEY,
	DATAVENCIMENTO DATE NOT NULL,
	VALORPARCELA NUMERIC(10,2) NOT NULL,
	CODSITUACAO_FK INTEGER REFERENCES SITUACAO(CODSITUACAO) ON DELETE CASCADE
	               ON UPDATE CASCADE, 
	CODVENDA_FK INTEGER REFERENCES VENDA(CODVENDA) ON DELETE CASCADE
	               ON UPDATE CASCADE
);


-- FORNECEDOR = {@CODFORNECEDOR, NOMEFORNECEDOR, CNPJ}
CREATE TABLE FORNECEDOR(
	CODFORNECEDOR INTEGER IDENTITY(1,1) PRIMARY KEY,
	NOMEFORNECEDOR VARCHAR(80) NOT NULL,
	CNPJ CHAR(18) NOT NULL UNIQUE --XX.XXX.XXX/0001-XX.
);

--COMPRA={@CODCOMPRA, DATACOMPRA, CODFORNECEDOR_FK}
CREATE TABLE COMPRA(
	CODCOMPRA INTEGER IDENTITY(1,1) PRIMARY KEY,
	DATACOMPRA DATE NOT NULL,
	CODFORNECEDOR_FK INTEGER REFERENCES FORNECEDOR (CODFORNECEDOR) ON DELETE
	                 CASCADE ON UPDATE CASCADE
);

--COMPRAPRODUTO={@(CODCOMPRA_FK,CODPRODUTO_FK), QUANTC, VALORC}
CREATE TABLE COMPRAPRODUTO(
	CODCOMPRA_FK INTEGER REFERENCES COMPRA(CODCOMPRA)  ON DELETE
	             CASCADE ON UPDATE CASCADE,
	CODPRODUTO_FK INTEGER REFERENCES PRODUTO(CODPRODUTO)  ON DELETE
	             CASCADE ON UPDATE CASCADE,
	QUANTC NUMERIC(10,2) NOT NULL,
	VALORC NUMERIC(10,2) NOT NULL,
	PRIMARY KEY(CODCOMPRA_FK, CODPRODUTO_FK)
);

