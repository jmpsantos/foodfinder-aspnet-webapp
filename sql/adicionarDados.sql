--Tabela Bloqueado.
INSERT INTO Bloqueado (motivo) VALUES ('Não respeitou as regras de não publicação de conteúdo com nudez.');
INSERT INTO Bloqueado (motivo) VALUES ('Publicou repetidamente comentários ofensivos para vários.');
INSERT INTO Bloqueado (motivo) VALUES ('Apresentou um comportamento suscetivel a questões e dúvidas.');

--Tabela Utilizador.
INSERT INTO Utilizador VALUES('cancelavelha', 'cancelavelha@gmail.com', '76D79BB961675020A5CBCB4AD1BD71C5F3148AFF7D62C63A95EA85D9DE51B1CE', 'Cancela Velha', 1, NULL);
INSERT INTO Utilizador VALUES('agrelha', 'agrelha@gmail.com', '22938D4D1672F76C7734DDCFAFA70C2F1BEFFE15ACFD5F3BF6C123E9E9C36', 'Restaurante A Grelha', 1, NULL);
INSERT INTO Utilizador VALUES('albufeira', 'albufeira@gmail.com', '9C4F72CDE93F7509C6B66877CEEB348FE6D2331AD1BDAB5B6E3FC4AD67FC668', 'Albufeira', 1, NULL);
INSERT INTO Utilizador VALUES('arquinhos', 'arquinhos@gmail.com', 'A385E6C927CA9D9D6E895EF14DE4A9393F6F866FB68AF9126F5B4A1358E24F', 'Arquinhos', 1, NULL);
INSERT INTO Utilizador VALUES('relaxburguer', 'relaxburguer@gmail.com', '32C13782FD2CD5B4AF8439738248514F6E2857AB7F78DCE346430BFBD9C1', 'Relax Burguer', 1, NULL);
INSERT INTO Utilizador VALUES('zedacalcada', 'zedacalcada@gmail.com', '5B815999EE235167F3ED1798F18D5E09DBDDC9AC891F34C78DCC79A3349', 'Zé da Calçada', 1, NULL);
INSERT INTO Utilizador VALUES('penhadouro', 'penhadouro@gmail.com', '13FBE6BD1D7B8B59E480AB2133A22B392E35257A5391B35EDD5AF1D3F6B33', 'Penhadouro', 1, NULL);
INSERT INTO Utilizador VALUES('carmenBistro', 'carmenBistro@gmail.com', 'BC2357D544FF1685EAF7E8C4B91D7CA1DD45AD86C73ADCAAAD59CBDA3E7693', 'Carmen Bistro e Restaurante', 1, NULL);
INSERT INTO Utilizador VALUES('pizzariaricardo', 'pizzariaricardo@gmail.com', '2570E250851073AEA5262DEC3C046CD6C17B815E5574A4FFBB460F4126699', 'Pizzaria Ricardo', 1, 1);
INSERT INTO Utilizador VALUES('tasquinhadofumo', 'tasquinhadofumo@gmail.com', '35122A76BCBE37CFAD859A848FCB2D7D140DE5A723A2CD77DF08A41328B8D5C', 'Tasquinha do Fumo', 1, NULL);


INSERT INTO Utilizador VALUES('miguelferreira', 'miguelferreira@gmail.com', '1561ACF87C436BBB9BBBCA863D7036581BDF2E23BC46E66CA4B525D4B87049C2', 'Miguel Ferreira', 1, NULL);
INSERT INTO Utilizador VALUES('alexsilva', 'alexsilva@gmail.com', 'C30236D0EA3E71771F6AEA65EB07DC5309BE497CCD1080E434257DBA6', 'Alexandre Silva', 1, NULL);
INSERT INTO Utilizador VALUES('antoniofreitas', 'antoniofreitas@gmail.com', '18C6346D4BF98DB3789B06440174B4978A304AD28C659A85217AF672C82B16', 'António Freitas', 1, NULL);
INSERT INTO Utilizador VALUES('mariaoliveira', 'mariaoliveira@gmail.com', '4984F61BD227DF807730E81ED0C2EE878BCFC3F35B93962626916D1F925D12', 'Maria Oliveira', 1, 2);
INSERT INTO Utilizador VALUES('alicesoares', 'alicesoares@gmail.com', '18DD57F8EE1EA5FBBC7DC38DA6C8957F84241A7B5E1FC15BFD7F78C4329746', 'Alice Soares', 1, NULL);


INSERT INTO Utilizador VALUES('ritaferreira', 'ritaferreira@gmail.com', 'A114309DC0DC95A98F11D36044251448E5452649FD45673B935827DED6EC25D', 'Rita Ferreira', 1, NULL);
INSERT INTO Utilizador VALUES('franciscomendes', 'franciscomendes@gmail.com', 'A156480C7744E7058AE951E903F5CC48CC9A14B2EFF2CAD662D09881EBC9', 'Francisco Mendes', 1, NULL);
INSERT INTO Utilizador VALUES('pauloribeiro', 'pauloribeiro@gmail.com', '9EA9628DA3D492CC15421888895A9CE81AC5C8C9384EE699853A49C9F4599', 'Paulo Ribeiro', 1, NULL);



-- Tabela Localização.
INSERT INTO Localizacao (codigo_postal, morada, localidade, gps_Latitude, gps_Longitude) VALUES('4630-224', 'Praça Movimento das Forças Armadas, 36', 'Marco de Canaveses', '41.1817', '-8.14951');
INSERT INTO Localizacao (codigo_postal, morada, localidade, gps_Latitude, gps_Longitude) VALUES('4500-411', 'Rua Manas, 188', 'Amarante', '41.2697', '-8.07834');
INSERT INTO Localizacao (codigo_postal, morada, localidade, gps_Latitude, gps_Longitude) VALUES('4630-259', 'Rua Rainha Dª Mafalda, 709', 'Marco de Canaveses', '41.1817', '-8.14951');
INSERT INTO Localizacao (codigo_postal, morada, localidade, gps_Latitude, gps_Longitude) VALUES('4630-201', 'Av. Dr. Francisco Sá Carneiro, 462', 'Marco de Canaveses', '41.1817', '-8.14951');
INSERT INTO Localizacao (codigo_postal, morada, localidade, gps_Latitude, gps_Longitude) VALUES('4630-205', 'Edificio Sonae Avenida Francisco Sa Carneiro 5º Piso Loja 1', 'Marco de Canaveses', '41.1817', '-8.14951');
INSERT INTO Localizacao (codigo_postal, morada, localidade, gps_Latitude, gps_Longitude) VALUES('4600-043', 'Rua 31 de Janeiro, 83', 'Amarante', '41.2697', '-8.07834');
INSERT INTO Localizacao (codigo_postal, morada, localidade, gps_Latitude, gps_Longitude) VALUES('4625-335', 'Ladário, N108', 'Marco de Canaveses', '41.1817', '-8.14951');
INSERT INTO Localizacao (codigo_postal, morada, localidade, gps_Latitude, gps_Longitude) VALUES('4630-200', 'Alameda Doutor Miranda da Rocha Alameda Doutor Miranda da Rocha', 'Marco de Canaveses', '41.1817', '-8.14951');
INSERT INTO Localizacao (codigo_postal, morada, localidade, gps_Latitude, gps_Longitude) VALUES('4560-853', 'Rua das Lages, 126', 'Marco de Canaveses', '41.172962', '-8.320448');
INSERT INTO Localizacao (codigo_postal, morada, localidade, gps_Latitude, gps_Longitude) VALUES('4640-101', 'Rua de Almofrela', 'Baião', '41.1614', '-8.03535');


-- Tabela Restaurante.
INSERT INTO Restaurante VALUES('cancelavelha', 'cancelavelha@gmail.com', 910000001, '11:00 - 01:00', 'segunda-feira', 'Local;Take-away;Esplanada;', 1, 4.8, 'Considerado como o baluarte da Gastronomia da Região do Douro e do Tâmega o restaurante Cancela Velha fica no coração da Cidade do Marco de Canaveses a poucos minutos da Cidade do Porto. Situado numa casa do século XVIII, anteriormente, conhecida por “Solar dos Mourões”, sendo um dos edifícios mais importantes para a região. No restaurante Cancela Velha poderá apreciar a excelente cozinha tradicional portuguesa juntamente com entradas e pastelaria regional, possui uma diversificada ementa gastronómica e vinícola.');
INSERT INTO Restaurante VALUES('agrelha', 'agrelha@gmail.com', 910000002, '11:00 - 01:00', 'terça-feira', 'Local;Take-away;Esplanada;', 2, 3.8, 'Com experiência adquirida desde 1975, o Restaurante A Grelha é um dos restaurantes mais conhecidos da cidade de Amarante. Localizado perto do seu centro, apresenta um menu de comida regional e com disponibilidade para oferecer o serviço de catering para o seu evento.');
INSERT INTO Restaurante VALUES('albufeira', 'albufeira@gmail.com', 910000003, '11:00 - 24:00', 'quarta-feira', 'Local;Esplanada;', 3, 4.1, 'Com varanda para o rio Tâmega, apresenta um espaço moderno e um ambiente agradável. Pode encontrar peixe fresco para deliciosos pratos de peixe grelhado assim como suculentos pratos de carne. Logo à entrada da cidade do Marco.');
INSERT INTO Restaurante VALUES('arquinhos', 'arquinhos@gmail.com', 910000004, '11:00 - 01:00', 'quinta-feira', 'Local;Take-away;', 4, 4.2, 'Culinária regional, o restaurante Arquinhos apresenta um espaço bastante acolhedor no centro da cidade. É possível realizar cá os seus jantares de empresa, familia ou aniversário.');
INSERT INTO Restaurante VALUES('relaxburguer', 'relaxburguer@gmail.com', 910000005, '10:00 - 03:00', 'sexta-feira', 'Local;Take-away;', 5, 3.5, 'Numa das zonas principais da cidade do Marco de Canaveses, apresenta um espaço com vistas panorâmicas e com uma variedade de pratos. O lugar ideal para quando não se tem a certeza do que se quer comer.');
INSERT INTO Restaurante VALUES('zedacalcada', 'zedacalcada@gmail.com', 910000006, '11:00 - 01:00', 'sábado', 'Local;', 6, 4.8, 'O restaurante Zé da Calçada é, ao nível da gastronomia autêntica da região, a casa mais célebre da cidade de Amarante. O restaurante Zé da Calçada é conhecido pela sua localização na zona histórica da cidade, pela varanda sobre o rio Tâmega, pelo requinte no atendimento e, especialmente, pela sua gastronomia.');
INSERT INTO Restaurante VALUES('penhadouro', 'penhadouro@gmail.com', 910000007, '11:00 - 01:00', 'domingo', 'Local;Take-away;', 7, 3.9, 'Junto à nacional N108, perto do rio Douro, é uma paragem obrigatória por esta região. Apresenta um menu completo de gastronomia regional, onde reina o arroz no forno com anho assado, possui também amplos espaços e zona para estacionamento.');
INSERT INTO Restaurante VALUES('carmenBistro', 'carmenBistro@gmail.com', 910000008, '09:00 - 03:00', 'segunda-feira', 'Local;Take-away;Esplanada;', 8, 4.5, 'Um restaurante que também é um bar, a sua especialidade é o sushi. Excelente para quem não quer apenas jantar, mas também passar um bom momento pela noite fora. Num espaço interior moderno, dispõe também esplanada numa das zonas mais vivas da cidade.');
INSERT INTO Restaurante VALUES('pizzariaricardo', 'pizzariaricardo@gmail.com', 910000009, '10:00 - 02:00', 'terça-feira', 'Local;Take-away;', 9, 4.0, 'A marca Pizzarias Ricardo é referência em restauração no Vale do Sousa. Criada em 1998, desde logo ficou associada à qualidade pelo sabor das suas pizzas e pastas, impulsionando a abertura de mais restaurantes, contando agora com presença em quatro concelhos da região.');
INSERT INTO Restaurante VALUES('tasquinhadofumo', 'tasquinhadofumo@gmail.com', 910000010, '11:00 - 01:00', 'quarta-feira', 'Local;Esplanada;', 10, 4.0, 'Quem por lá passa nunca esquece o local, a comida e as pessoas que fazem daquele espaço um verdadeiro espaço de convívio e riqueza gastronómica. A comida é, ainda hoje, confecionada em potes de barro e panelas de ferro nas quais  a Dona Isabel, proprietária do espaço, cozinha como poucos.');


-- Tabela Cliente.
INSERT INTO Cliente VALUES('miguelferreira', NULL);
INSERT INTO Cliente VALUES('alexsilva', NULL);
INSERT INTO Cliente VALUES('antoniofreitas', NULL);
INSERT INTO Cliente VALUES('mariaoliveira', NULL);
INSERT INTO Cliente VALUES('alicesoares', NULL);

-- Tabela Administrador.
INSERT INTO Administrador VALUES('ritaferreira');
INSERT INTO Administrador VALUES('franciscomendes');
INSERT INTO Administrador VALUES('pauloribeiro');


-- Tabela Comentario_Restaurante
INSERT INTO Comentario_Restaurante (data_comentario, restaurante_id, cliente_id, corpo, rating) VALUES('2020/06/04 12:00:00:00', 'cancelavelha', 'miguelferreira', 'Adoro este restaurante. É a minha paragem obrigatória na cidade de Marco de Canaveses. Os funcionário são bastantes familiares e simpáticos. A comida é fantástica.', 5);
INSERT INTO Comentario_Restaurante (data_comentario, restaurante_id, cliente_id, corpo, rating) VALUES('2020/12/06 12:00:00:00', 'cancelavelha', 'alexsilva', 'A comida é muito boa. Vim especialmente para experimentar o famoso arroz no forno com anho assado, o prato da cidade.', 4.5);
INSERT INTO Comentario_Restaurante (data_comentario, restaurante_id, cliente_id, corpo, rating) VALUES('2020/06/04 12:00:00:00', 'cancelavelha', 'antoniofreitas', 'O restaurante encontra-se num antigo solar. Facilmente percebe-se que há um esforço na preservação do espaço interior com o que ele haveria de ter sido no passado. Gostei muito.', 5);
INSERT INTO Comentario_Restaurante (data_comentario, restaurante_id, cliente_id, corpo, rating) VALUES('2020/12/02 12:00:00:00', 'agrelha', 'mariaoliveira', 'Gostei bastante da comida e dos funcionários. Aconselho a todos os meus amigos e familia.', 3);
INSERT INTO Comentario_Restaurante (data_comentario, restaurante_id, cliente_id, corpo, rating) VALUES('2020/06/04 12:00:00:00', 'albufeira', 'mariaoliveira', 'Gostei bastante da comida e dos funcionários. Aconselho a todos os meus amigos e familia.', 4);
INSERT INTO Comentario_Restaurante (data_comentario, restaurante_id, cliente_id, corpo, rating) VALUES('2020/05/04 12:00:00:00', 'albufeira', 'alexsilva', 'Adoro comer na esplanada com vistas para o rio tâmega. Paragem obrigatória!', 4);
INSERT INTO Comentario_Restaurante (data_comentario, restaurante_id, cliente_id, corpo, rating) VALUES('2020/06/19 12:00:00:00', 'arquinhos', 'mariaoliveira', 'Funcionários muito simpáticos e comida bastante deliciosa.', 4);
INSERT INTO Comentario_Restaurante (data_comentario, restaurante_id, cliente_id, corpo, rating) VALUES('2020/04/04 12:00:00:00', 'relaxburguer', 'mariaoliveira', 'Funcionários muito simpáticos e comida bastante deliciosa. Aconselho.', 3);
INSERT INTO Comentario_Restaurante (data_comentario, restaurante_id, cliente_id, corpo, rating) VALUES('2020/06/12 12:00:00:00', 'zedacalcada', 'alicesoares', 'Espaço bastante luxuoso numa das ruas mais pitorescas de Amarante.', 4.3);
INSERT INTO Comentario_Restaurante (data_comentario, restaurante_id, cliente_id, corpo, rating) VALUES('2020/07/04 12:00:00:00', 'zedacalcada', 'mariaoliveira', 'Aconselho a qualquer pessoa que venha a Amarante. Não se irão arrepender.', 3.5);
INSERT INTO Comentario_Restaurante (data_comentario, restaurante_id, cliente_id, corpo, rating) VALUES('2020/09/18 12:00:00:00', 'penhadouro', 'alexsilva', 'Encontrei este restaurante por acaso, enquanto fazia a viagem para a Régua. Foi uma supresa agradável.', 3.5);



-- Tabela Prato_do_Dia.

INSERT INTO Prato_do_Dia (descricao, tipo, nome) VALUES ('A carne de Raça Arouquesa foi consagrada com a denominazação de origem protegida em 1994. Disposta nas brasas, pincelada com um preparado de azeite, sal, alho e um pouco de vinagre e acompanhada com batata a murro, grelos e arroz branco.', 'Carne', 'Posta Arouquesa');
INSERT INTO Prato_do_Dia (descricao, tipo, nome) VALUES ('Prato regional da cidade de Marco de Canaveses, é uma das suas atrações principais. É uma supresa agrável para muitas pessoas que descobrem o sabor do anho assado enraizado no próprio arroz.', 'Carne', 'Anho Assado com Arroz');
INSERT INTO Prato_do_Dia (descricao, tipo, nome) VALUES ('As suas carnes tenras, acompanhadas com rodelas de ananás, arroz com feijão preto e batatas fritas são uma proposta excelente, não só no verão, como para o ano inteiro.', 'Carne', 'Picanha Assada');
INSERT INTO Prato_do_Dia (descricao, tipo, nome) VALUES ('Uma boa opção para qualquer pessoa.', 'Carne', 'Prego no Prato');
INSERT INTO Prato_do_Dia (descricao, tipo, nome) VALUES ('Um prato refrescante para qualquer altura. Uma supresa para o paladar.', 'Peixe', 'Lulas Fritas');
INSERT INTO Prato_do_Dia (descricao, tipo, nome) VALUES ('O principal prato da cidade, é apresentado aqui no seu paladar regional e original.', 'Carne', 'Anho Assado com Arroz');
INSERT INTO Prato_do_Dia (descricao, tipo, nome) VALUES ('Hambúrguer, bacon grelhado, cebola frita, queijo brie, manteiga de alho, batata.', 'Carne', 'Hambúrguer em Bolo do Caco');
INSERT INTO Prato_do_Dia (descricao, tipo, nome) VALUES ('Queijo, fiambre, salsicha, molho especial', 'Carne', 'Cachorro  Especial');
INSERT INTO Prato_do_Dia (descricao, tipo, nome) VALUES ('Kebab, cebola frita, salada, batata frita, molho kebap.', 'Carne', 'Kebab no Prato');
INSERT INTO Prato_do_Dia (descricao, tipo, nome) VALUES ('Alface, tomate, panado', 'Carne', 'Baguete de Panado');
INSERT INTO Prato_do_Dia (descricao, tipo, nome) VALUES ('Bife de atum, pimento assado, cogumelos shitake, queijo, molho especial, batata frita.', 'Peixe', 'Francesinha com Bife de Atum');
INSERT INTO Prato_do_Dia (descricao, tipo, nome) VALUES ('Um prato com sabor supreendente, cozinhado com os maiores padrões de qualidade e produtos frescos.', 'Peixe', 'Filetes de Peixe');
INSERT INTO Prato_do_Dia (descricao, tipo, nome) VALUES ('Sabor inigualável com as melhores carnes.', 'Carne', 'Anho Assado com Arroz');
INSERT INTO Prato_do_Dia (descricao, tipo, nome) VALUES ('Entre numa verdadeira aventura dos sabores, com os melhores enchidos e queijos.', 'Carne', 'Tábua de Enchidos e Queijos');



-- Tabela Adicionar_Prato_do_Dia.

INSERT INTO Adicionar_Prato_do_Dia VALUES ('cancelavelha', 1, '2020/12/24 12:00:00:00', 15, 0, 1);
INSERT INTO Adicionar_Prato_do_Dia VALUES ('cancelavelha', 2, '2020/12/24 12:00:00:00', 12, 1, 1);
INSERT INTO Adicionar_Prato_do_Dia VALUES ('cancelavelha', 3, '2020/12/24 12:00:00:00', 20, 0, 1);
INSERT INTO Adicionar_Prato_do_Dia VALUES ('agrelha', 4, '2020/12/24 12:00:00:00', 13.20, 1, 1);
INSERT INTO Adicionar_Prato_do_Dia VALUES ('albufeira', 5, '2020/12/24 12:00:00:00', 12.00, 1, 1);
INSERT INTO Adicionar_Prato_do_Dia VALUES ('arquinhos', 6, '2020/12/24 12:00:00:00', 16.10, 0, 1);
INSERT INTO Adicionar_Prato_do_Dia VALUES ('relaxburguer', 7, '2020/12/24 12:00:00:00', 8.90, 0, 1);
INSERT INTO Adicionar_Prato_do_Dia VALUES ('relaxburguer', 8, '2020/12/24 12:00:00:00', 4.20, 0, 1);
INSERT INTO Adicionar_Prato_do_Dia VALUES ('relaxburguer', 9, '2020/12/24 12:00:00:00', 7.00, 0, 1);
INSERT INTO Adicionar_Prato_do_Dia VALUES ('relaxburguer', 10, '2020/12/24 12:00:00:00', 6.80, 0, 1);
INSERT INTO Adicionar_Prato_do_Dia VALUES ('relaxburguer', 11, '2020/12/24 12:00:00:00', 11.10, 0, 1);
INSERT INTO Adicionar_Prato_do_Dia VALUES ('zedacalcada', 12, '2020/12/24 12:00:00:00', 22.10, 0, 1);
INSERT INTO Adicionar_Prato_do_Dia VALUES ('penhadouro', 13, '2020/12/24 12:00:00:00', 15.10, 0, 1);
INSERT INTO Adicionar_Prato_do_Dia VALUES ('tasquinhadofumo', 14, '2020/12/24 12:00:00:00', 7.90, 0, 1);



-- Tabela Favoritar_Restaurante.
INSERT INTO Favoritar_Restaurante VALUES ('cancelavelha', 'miguelferreira');
INSERT INTO Favoritar_Restaurante VALUES ('cancelavelha', 'alexsilva');
INSERT INTO Favoritar_Restaurante VALUES ('cancelavelha', 'antoniofreitas');
INSERT INTO Favoritar_Restaurante VALUES ('agrelha', 'miguelferreira');
INSERT INTO Favoritar_Restaurante VALUES ('albufeira', 'miguelferreira');
INSERT INTO Favoritar_Restaurante VALUES ('albufeira', 'alicesoares');
INSERT INTO Favoritar_Restaurante VALUES ('relaxburguer', 'miguelferreira');
INSERT INTO Favoritar_Restaurante VALUES ('relaxburguer', 'mariaoliveira');
INSERT INTO Favoritar_Restaurante VALUES ('relaxburguer', 'alexsilva');
INSERT INTO Favoritar_Restaurante VALUES ('zedacalcada', 'miguelferreira');
INSERT INTO Favoritar_Restaurante VALUES ('zedacalcada', 'antoniofreitas');
INSERT INTO Favoritar_Restaurante VALUES ('penhadouro', 'alicesoares');
INSERT INTO Favoritar_Restaurante VALUES ('penhadouro', 'antoniofreitas');
INSERT INTO Favoritar_Restaurante VALUES ('pizzariaricardo', 'miguelferreira');
INSERT INTO Favoritar_Restaurante VALUES ('pizzariaricardo', 'alexsilva');



-- Tabeça Favoritar_Prato_do_Dia.
INSERT INTO Favoritar_Prato_do_Dia VALUES (1, 'miguelferreira');
INSERT INTO Favoritar_Prato_do_Dia VALUES (1, 'alexsilva');
INSERT INTO Favoritar_Prato_do_Dia VALUES (1, 'antoniofreitas');
INSERT INTO Favoritar_Prato_do_Dia VALUES (1, 'alicesoares');
INSERT INTO Favoritar_Prato_do_Dia VALUES (2, 'miguelferreira');
INSERT INTO Favoritar_Prato_do_Dia VALUES (3, 'alexsilva');
INSERT INTO Favoritar_Prato_do_Dia VALUES (4, 'antoniofreitas');
INSERT INTO Favoritar_Prato_do_Dia VALUES (5, 'alicesoares');
INSERT INTO Favoritar_Prato_do_Dia VALUES (6, 'miguelferreira');
INSERT INTO Favoritar_Prato_do_Dia VALUES (7, 'alexsilva');
INSERT INTO Favoritar_Prato_do_Dia VALUES (8, 'antoniofreitas');
INSERT INTO Favoritar_Prato_do_Dia VALUES (3, 'alicesoares');
INSERT INTO Favoritar_Prato_do_Dia VALUES (4, 'miguelferreira');
INSERT INTO Favoritar_Prato_do_Dia VALUES (5, 'alexsilva');
INSERT INTO Favoritar_Prato_do_Dia VALUES (6, 'antoniofreitas');
INSERT INTO Favoritar_Prato_do_Dia VALUES (6, 'alicesoares');

