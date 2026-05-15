using ProgramaClubeLeitura;

var professorMediador = new Professor("Dra. Helena Souza");
var obraPrincipal = new ObraLiteraria("O Primo Basílio");
var espaco = new EspacoEncontro("Sala de Leitura 4");
var professorConvidado = new Professor("Prof. Carlos Lima");

var clube = new ClubeLeitura(professorMediador, obraPrincipal, espaco);
clube.DefinirProfessorConvidado(professorConvidado);
clube.AdicionarParticipante(new Estudante("Ana"));
clube.AdicionarParticipante(new Estudante("Bruno"));
clube.AdicionarMaterialApoio(new MaterialApoio("Guia de discussão"));
clube.AdicionarMaterialApoio(new MaterialApoio("Artigo sobre o autor"));

var encontro1 = new EncontroLeitura("Discussão inicial", obrigatorio: true, descricao: "Primeira reunião para introdução da obra.");
var encontro2 = new EncontroLeitura("Debate de personagens", obrigatorio: false, descricao: "Discussão livre sobre personagens e motivações.");

clube.PlanejarEncontro(encontro1);
clube.PlanejarEncontro(encontro2);

clube.RegistrarContribuicao(encontro1, new ContribuicaoLeitura("A obra aborda temas sociais importantes."));
clube.RegistrarContribuicao(encontro2, new ContribuicaoLeitura("Os personagens possuem conflitos bem construídos."));

Console.WriteLine("Clube de leitura criado e preenchido com dados de exemplo:");
Console.WriteLine($"- Professor mediador: {clube.ProfessorMediador.Nome}");
Console.WriteLine($"- Professor convidado: {clube.ProfessorConvidado?.Nome}");
Console.WriteLine($"- Obra principal: {clube.ObraPrincipal.Titulo}");
Console.WriteLine($"- Espaço de encontro: {clube.EspacoEncontro.Identificacao}");
Console.WriteLine($"- Participantes: {string.Join(", ", clube.Participantes.Select(p => p.Nome))}");
Console.WriteLine($"- Materiais: {string.Join(", ", clube.MateriaisApoio.Select(m => m.Descricao))}");
Console.WriteLine($"- Encontros planejados: {clube.Encontros.Count}");

clube.Encerrar();
Console.WriteLine($"Clube encerrado? {clube.Encerrado}");
