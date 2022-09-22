CREATE DATABASE Lab



CREATE TABLE Bloqueado
(
	id BIGINT NOT NULL,
	motivo VARCHAR(50) NOT NULL,

	PRIMARY KEY (ID)
	);

CREATE TABLE Utilizador
(
	username VARCHAR(15) NOT NULL,
	email VARCHAR(50) NOT NULL,
	password VARCHAR(20) NOT NULL,
	nome VARCHAR(50) NOT NULL,
	registo_Confirmado BIT NOT NULL,
	bloqueado_ID BIGINT,

	PRIMARY KEY (Username),
	FOREIGN KEY (Bloqueado_ID) REFERENCES Bloqueado
);

CREATE TABLE Localizacao
(
	localizacao_id BIGINT NOT NULL,
	codigo_postal VARCHAR(15) NOT NULL,
	morada VARCHAR(50) NOT NULL,
	localidade VARCHAR(50) NOT NULL,
	gps_Latitude FLOAT NOT NULL,
	gps_Longitude FLOAT NOT NULL,

	CHECK(codigo_postal LIKE '[0-9][0-9][0-9][0-9]-[0-9][0-9][0-9]'),

	PRIMARY KEY (localizacao_id),
);

CREATE TABLE Restaurante
(
	restaurante_id VARCHAR(15) NOT NULL,
	contacto_email VARCHAR(25) NOT NULL,
	contacto_telefone BIGINT NOT NULL,
	horario_funcionamento VARCHAR(50) NOT NULL,
	dia_de_descanso VARCHAR (10) ,
	tipo_de_servico VARCHAR(50) NOT NULL,
	localizacao_id BIGINT NOT NULL,
	rating INT NOT NULL,
	descricao VARCHAR(300) NOT NULL,
	
	CHECK(contacto_telefone > 0),

	PRIMARY KEY (restaurante_id),
	FOREIGN KEY (restaurante_id) REFERENCES Utilizador(username),
	FOREIGN KEY (localizacao_id) REFERENCES Localizacao(localizacao_id),
);

CREATE TABLE Cliente
(
	cliente_id VARCHAR(15) NOT NULL,

	PRIMARY KEY (cliente_id),
	FOREIGN KEY (cliente_id) REFERENCES Utilizador(username)
);

CREATE TABLE Administrador
(
	administrador_id VARCHAR(15) NOT NULL,

	PRIMARY KEY (administrador_id),
	FOREIGN KEY (administrador_id) REFERENCES Utilizador(username)
);

CREATE TABLE Comentario_Restaurante
(
	comentario_id BIGINT NOT NULL,
	data_comentario DATE NOT NULL,
	restaurante_id VARCHAR(15) NOT NULL,
	cliente_id VARCHAR(15) NOT NULL,
	corpo VARCHAR(300) NOT NULL,

	PRIMARY KEY (comentario_id),
	FOREIGN KEY (restaurante_id) REFERENCES Restaurante(restaurante_id),
	FOREIGN KEY (cliente_id) REFERENCES Cliente(cliente_id)
);


CREATE TABLE Prato_do_Dia
(
	prato_id BIGINT NOT NULL,
	descricao VARCHAR(300) NOT NULL,
	tipo VARCHAR(15) NOT NULL,

	PRIMARY KEY (prato_id)
);


CREATE TABLE Adicionar_Prato_do_Dia
(
	restaurante_id VARCHAR(15) NOT NULL,
	prato_id BIGINT NOT NULL,
	data_prato DATE NOT NULL,
	preco FLOAT NOT NULL,
	destacado BIT NOT NULL,

	PRIMARY KEY (prato_id, restaurante_id, data_prato),
	FOREIGN KEY (restaurante_id) REFERENCES Restaurante(restaurante_id),
	FOREIGN KEY (prato_id) REFERENCES Prato_do_Dia
);

CREATE TABLE Favoritar_Restaurante
(
	restaurante_id VARCHAR(15) NOT NULL,
	cliente_id VARCHAR(15) NOT NULL,

	PRIMARY KEY (restaurante_id, cliente_id),
	FOREIGN KEY (restaurante_id) REFERENCES Restaurante(restaurante_id),
	FOREIGN KEY (cliente_id) REFERENCES Cliente(cliente_id),
);

CREATE TABLE Favoritar_Prato_do_Dia
(
	prato_id BIGINT NOT NULL,
	cliente_id VARCHAR(15) NOT NULL,

	PRIMARY KEY (prato_id, cliente_id),
	FOREIGN KEY (prato_id) REFERENCES Prato_do_Dia(prato_id),
	FOREIGN KEY (cliente_id) REFERENCES Cliente(cliente_id),
);
