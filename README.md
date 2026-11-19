<h1 align="center"> ADVANCED BUSINESS DEVELOPMENT WITH .NET </h1>
<h3 align="center">Global - FIAP - CitieZen-Gb</h3>


<h2 align="center" >  👥 Integrante </h2>

- Letícia Santiago e Silva - RM565799 - 2TDSPW
- Iago Dias ainette -  RM565708 - 2TDSPW
- Gabriel Fachin de Oliveira- RM561551 - 2TDSPW
- Fernando Charlles Faustino Fernandes -  RM566482 - 2TDSPW

<h2> 📋 sobre Tema: </h2>

<p>Questões de melhoria da vida do cidadão sempre foram um tema central na nossa melhoria, afinal, na vida moderna sempre buscamos a maneira mais fácil ou que facilite o nosso dia a cada momento. É a ferramenta que mais utilizamos: a tecnologia em diversas formas, umbandas para nos beneficiar, e esse assunto ditado é tema da Global Solution deste ano.</p>

<p>Contudo, dentro dessa ideia de beneficiar o cidadão por meio de tecnologia que é criada a partir de necessidades humanas, às vezes alguns grupos da nossa sociedade são deixados de lado ou possuem dificuldade em utilizá-la, e, entre esses grupos, estão as pessoas idosas que precisam de cuidadores ou estão presentes em asilos, e essa questão será o tema do nosso projeto do grupo.</p>

<h2> 💡 Sobre o projeto do ajudante Familia</h2>

<p>Segundo dados de pesquisa pelo ELSI-Brasil, estima-se que cerca de 5 milhões de idosos possuem dificuldades para realizar atividades básicas da vida diária presentes no Brasil, e, por causa disso, é necessário que os familiares ou cuidadores de asilos realizem o cuidado presente para esse idoso.</p>

<p>Contudo, como sabemos, organizar uma rotina é difícil, especialmente para pessoas com dificuldades em realizar atividades básicas no dia. Por causa disso, necessita que realize anotações sobre os pacientes, como atividades, alimentação no dia a dia, consulta médica, doenças ou até consulta médica.
</p>

<p>
  Contudo, como sabemos, organizar uma rotina é difícil, especialmente para pessoas com dificuldades em realizar atividades básicas no dia. Por causa disso, necessita que realize anotações sobre os pacientes, como atividades, alimentação no dia a dia, consulta médica, doenças ou até consulta médica.
</p>

<h2> Como vai funcionar? </h2>

<p>A ideia em si é fazer um aplicativo que ajude os cuidadores ou responsáveis pelos idosos a organizar o seu dia a dia, como, por exemplo, organizar a alimentação, seu preparo, caso o idoso tenha necessidades, e suas atividades diárias para que eles se movimentam no dia a dia.
</p>

<p>Mas também presente no sistema, temos a parte que vai guardar dados do idoso, como informações básicas dele, medicamento que ele deve tomar, com sua data e hora de consumo. Contudo, também temos presente a questão da consulta, afinal, quando chegar a uma idade avançada, é necessário que o idoso vá para consulta com uma frequência maior e, por causa disso, temos a opção de consulta com suas informações básicas, como médicos, dia e se foi concluída, sim ou não.
</p>

<p>Resumindo, a ideia em si é ajudar as pessoas que trabalham nos asilos, ou são cuidadores ou familiares que podem ter um apoio na organização e, claro, cuidar de pessoas em necessidade que precisam de ajuda.
</p>

<h2>Como o dot net vai funcionar no projeto</h2>

<p>O objetivo do dotnet espeficicalmente é servir como api que conectar tanto no banco de dados da oracle com nosso sistema de front end para assim possar adicionar dados.</p>

<h2> 🛣️ Rotas da API: </h2>

🍽️ Alimentação — tabela Oracle: T_Ajudante_familiar_Alimentacao

•	GET - ALL: api/Alimentacao/alimentacao
•	GET - ID: api/Alimentacao/alimentacao/{id_alimentacao:int}
•	POST: api/Alimentacao/alimentacao/criar
•	PUT: api/Alimentacao/alimentacao/atualiza/{id_alimentacao:int}
•	DELETE: api/Alimentacao/alimentacao/deleta/{id_alimentacao:int}

📅 Atividades — tabela Oracle: T_Ajudante_familiar_atividades

•	GET - ALL: api/Atividades/atividades
•	GET - ID: api/Atividades/atividades/{id_atividade:int}
•	POST: api/Atividades/atividades/criar
•	PUT: api/Atividades/atividades/atualiza/{id_atividade:int}
•	DELETE: api/Atividades/atividades/deleta/{id_atividade:int}

💼 Cargo — tabela Oracle: T_Ajudante_familiar_Cargo

•	GET - ALL: api/Cargo/cargo
•	GET - ID: api/Cargo/cargo/{id_cargo:int}
•	POST: api/Cargo/cargo/criar
•	PUT: api/Cargo/cargo/atualiza/{id_cargo:int}
•	DELETE: api/Cargo/cargo/deleta/{id_cargo:int}

🔗 Consulta_Medicamento — tabela Oracle: T_Ajudante_familiar_Consulta_Medicamento

•	GET - ALL: api/Consulta_Medicamento/consulta_medicamento
•	GET - ID: api/Consulta_Medicamento/consulta_medicamento/{id_consulta_medicamento:int}
•	POST: api/Consulta_Medicamento/consulta_medicamento/criar
•	PUT: api/Consulta_Medicamento/consulta_medicamento/atualiza/{id_consulta_medicamento:int}
•	DELETE: api/Consulta_Medicamento/consulta_medicamento/deleta/{id_consulta_medicamento:int}

🧑‍⚕️ Cuidador — tabela Oracle: T_Ajudante_familiar_Cuidador

•	GET - ALL: api/Cuidador/cuidador
•	GET - ID: api/Cuidador/cuidador/{id_cuidador:int}
•	POST: api/Cuidador/cuidador/criar
•	PUT: api/Cuidador/cuidador/atualiza/{id_cuidador:int}
•	DELETE: api/Cuidador/cuidador/deleta/{id_cuidador:int}

🩺 Doenças — tabela Oracle: T_Ajudante_familiar_Doencas

•	GET - ALL: api/Doencas/doencas
•	GET - ID: api/Doencas/doencas/{id_doenca:int}
•	POST: api/Doencas/doencas/criar
•	PUT: api/Doencas/doencas/atualiza/{id_doenca:int}
•	DELETE: api/Doencas/doencas/deleta/{id_doenca:int}

👵 Idoso — tabela Oracle: T_Ajudante_familiar_Idoso

•	GET - ALL: api/Idoso/idoso
•	GET - ID: api/Idoso/idoso/{id_idoso:int}
•	POST: api/Idoso/idoso/criar
•	PUT: api/Idoso/idoso/atualiza/{id_idoso:int}
•	DELETE: api/Idoso/idoso/deleta/{id_idoso:int}

📝 Lembrete_Consulta — tabela Oracle: T_Ajudante_familiar_Lembrete_Consulta

•	GET - ALL: api/Lembrete_Consulta/lembrete_consulta
•	GET - ID: api/Lembrete_Consulta/lembrete_consulta/{id_lembrete_consulta:int}
•	POST: api/Lembrete_Consulta/lembrete_consulta/criar
•	PUT: api/Lembrete_Consulta/lembrete_consulta/atualiza/{id_lembrete_consulta:int}
•	DELETE: api/Lembrete_Consulta/lembrete_consulta/deleta/{id_lembrete_consulta:int}

💊 Medicamentos — tabela Oracle: T_Ajudante_familiar_Medicamentos

•	GET - ALL: api/Medicamentos/medicamentos
•	GET - ID: api/Medicamentos/medicamentos/{id_medicamentos:int}
•	POST: api/Medicamentos/medicamentos/criar
•	PUT: api/Medicamentos/medicamentos/atualiza/{id_medicamentos:int}
•	DELETE: api/Medicamentos/medicamentos/deleta/{id_medicamentos:int}

👨‍👩‍👧 Parente — tabela Oracle: T_Ajudante_familiar_Parente

•	GET - ALL: api/Parente/parente
•	GET - ID: api/Parente/parente/{id_parente:int}
•	POST: api/Parente/parente/criar
•	PUT: api/Parente/parente/atualiza/{id_parente:int}
•	DELETE: api/Parente/parente/deleta/{id_parente:int}

🪪 Perfil — tabela Oracle: T_Ajudante_familiar_Perfil

•	GET - ALL: api/Perfil/perfil
•	GET - ID: api/Perfil/perfil/{id_perfil:int}
•	POST: api/Perfil/perfil/criar
•	PUT: api/Perfil/perfil/atualiza/{id_perfil:int}
•	DELETE: api/Perfil/perfil/deleta/{id_perfil:int}

<h2>🛠️ Tecnologias utilizadas</h2>

### ⚙️ Backend
- **C#** e **.NET 10**
- **ASP.NET Core Web API** com Controllers
- **Entity Framework Core 10**

### 🗄️ Banco de dados
- **Oracle Database**
- Provedor `Oracle.EntityFrameworkCore`

### 🔐 Segurança e confiabilidade
- Autenticação e autorização com **JWT**
- **Rate Limiting** para limitar requisições
- **Idempotência** para evitar operações duplicadas
- **Health Checks** para monitoramento

### 📚 Documentação e testes
- **Swagger/OpenAPI** e **Scalar** para documentar a API
- **xUnit** para testes unitários e de integração
- **Moq** para simular dependências nos testes

### 📊 Logs e telemetria
- **Serilog** para registro de logs
- **OpenTelemetry** para telemetria
<h2>🚀 Instruções de instalação e execução: </h2>

<h3>Migration</h3>

<p>Na parte de Infrastruture deve fazer isso para fazer o seu migration</p>

```
Add-Migration {nome que deseja} -StartupProject Ajudante_Familiar_Global -Context AppDbContext
```

<p>Apos disso precisa aplicar isso para aplicar no seu Oracle</p>

```
Update-Database -StartupProject Ajudante_Familiar_Global -Context AppDbContext
```

<h2>🩺 Health Checks</h2>

A API possui verificações de saúde para monitorar a disponibilidade da aplicação e a conexão com o banco de dados Oracle.

Foram implementados dois endpoints:

/health/live: verifica se a aplicação está em execução.

/health/ready: verifica se a aplicação está pronta para receber requisições, incluindo a disponibilidade do banco de dados Oracle.

```
/health/live
```

```
/health/ready
```

<h2>🚦 Rate Limiting</h2>

<p>Essa parte funcionar como um limitado de tentativas, ele funcionando quando tenta varias tentativas e recomendo voce fazer apos do jwt</p>


<h2> idempotência </h2>

<p>Essa parte funcionar como um limitado de nao acontecer duplicidades e pode ser vista tanto no controller por meio desses dois : </p>

```
builder.Services.AddDistributedMemoryCache();
builder.Services.AddIdempotentAPI();
builder.Services.AddIdempotentAPIUsingDistributedCache();
```

<h3>Mas tambem temos dentro do controllers</h3

```
[Produces("application/json")]

[Idempotent]
```                                            

<h4>🛠️ Instalações necessarias: </h4>

•	.NET: 10 (net10.0)

•	Banco de dados: Oracle

•	ORM: Entity Framework Core 10.0.0

•	Oracle: Oracle.EntityFrameworkCore 10.23.26400

•	JWT: Microsoft.AspNetCore.Authentication.JwtBearer 10.0.0

•	Swagger/OpenAPI: Swashbuckle.AspNetCore 10.0.1 e Microsoft.AspNetCore.OpenApi 10.0.0

•	Idempotência: IdempotentAPI 2.6.0 e IdempotentAPI.Cache.DistributedCache 2.2.0

•	Health Checks: AspNetCore.HealthChecks.Oracle 9.0.0

•	Testes: xUnit 2.9.3

•	Testes de integração: Microsoft.AspNetCore.Mvc.Testing 10.0.0

•	Mocks para testes: Moq 4.21.0

•	Logs e telemetria: Serilog.AspNetCore 10.0.0 e OpenTelemetry 1.20.1
