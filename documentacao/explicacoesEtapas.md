# Etapa 1: entender o domínio

Nesta fase inicial, eu li o enunciado de `exercicio.md` e identifiquei os conceitos que precisam virar classes no sistema.

O que fiz:

- Li o enunciado completo do exercício.
- Identifiquei as entidades obrigatórias: `Professor`, `Estudante`, `ObraLiteraria`, `EspacoEncontro`, `ClubeLeitura`, `MaterialApoio`, `EncontroLeitura` e `ContribuicaoLeitura`.
- Observei as relações principais:
  - `ClubeLeitura` precisa obrigatoriamente de `Professor`, `ObraLiteraria` e `EspacoEncontro`.
  - `ClubeLeitura` terá muitos `Estudante`.
  - O professor convidado é opcional.
  - Materiais são reutilizáveis e existem fora do clube.
  - Encontros existem somente dentro do clube.
- Defini que os dados de texto devem ser protegidos desde a criação, evitando `null`, vazio ou espaços apenas.

Por que isso é importante?

Entender o domínio primeiro ajuda a criar um modelo mais forte. Quando sabemos quais objetos existem e como eles se relacionam, podemos então escrever classes que representam corretamente o clube de leitura.

O código criado para esta etapa inclui:

- `Validacao.cs`: helper para garantir textos obrigatórios válidos.
- Entidades básicas com construtor e validação: `Professor`, `Estudante`, `ObraLiteraria`, `EspacoEncontro`, `MaterialApoio`, `EncontroLeitura` e `ContribuicaoLeitura`.
- `ClubeLeitura.cs`: classe com associação obrigatória a `Professor`, `ObraLiteraria` e `EspacoEncontro`, além de uma coleção inicial de participantes, materiais e encontros.
- `Program.cs`: exemplo simples de criação do clube para verificar a modelagem.

Com isso, a etapa 1 está concluída e o projeto já tem o esqueleto das entidades indispensáveis.

## Etapa 2: criar as classes básicas

Na segunda etapa, fiz a implementação das classes que representam as entidades do domínio.

O que foi feito:

- Criei `Professor`, `Estudante`, `ObraLiteraria`, `EspacoEncontro`, `MaterialApoio`, `EncontroLeitura` e `ContribuicaoLeitura`.
- Criei `ClubeLeitura` como o principal objeto que recebe `Professor`, `ObraLiteraria` e `EspacoEncontro` no construtor.
- Usei construtores para receber os dados obrigatórios de cada entidade.
- Mantive o modelo mínimo necessário para continuar a próxima etapa sem perder a estrutura do domínio.

Por que isso é importante?

Ter as classes básicas prontas permite que a aplicação tenha forma antes de aplicar regras de negócio adicionais. Nesse momento, o foco é tornar os objetos instanciáveis e começar a separar responsabilidades.

O código criado ou ajustado para essa etapa inclui:

- `Professor.cs`
- `Estudante.cs`
- `ObraLiteraria.cs`
- `EspacoEncontro.cs`
- `MaterialApoio.cs`
- `EncontroLeitura.cs`
- `ContribuicaoLeitura.cs`
- `ClubeLeitura.cs`
- `Validacao.cs`

A próxima etapa será proteger e validar melhor os dados de entrada.

## Etapa 3: proteger os dados e validar entradas

Nesta etapa, revisei cada entidade para garantir que dados obrigatórios sejam validados e que os objetos não aceitem estados inconsistentes.

O que foi feito:

- Usei `Validacao.TextoObrigatorio` em todos os construtores de classes que recebem texto.
- Garanti que `Professor`, `Estudante`, `ObraLiteraria`, `EspacoEncontro`, `MaterialApoio`, `EncontroLeitura` e `ContribuicaoLeitura` não sejam criados com `null`, vazio ou apenas espaços.
- Ajustei `ClubeLeitura` para expor coleções como `IReadOnlyCollection<T>`, evitando que listas internas sejam modificadas diretamente.
- Confirmei que os métodos de alteração, onde existem, também validam os novos valores.

Por que isso é importante?

Assim o modelo protege invariantes do domínio desde a criação dos objetos. Isso evita bugs e mantém os dados confiáveis ao longo do ciclo de vida do sistema.

O código desta etapa inclui:

- validações em todos os construtores de entidades de texto;
- coleções protegidas no `ClubeLeitura`;
- reforço do contrato de imutabilidade externa para listas internas.

A próxima etapa será implementar o `ClubeLeitura` com associações obrigatórias e garantir que ele não exista sem seus objetos essenciais.

## Etapa 4: implementar ClubeLeitura com associações obrigatórias

Nesta etapa, confirmei que `ClubeLeitura` depende de objetos reais e obrigatórios, e não de strings simples.

O que foi feito:

- Mantive o construtor de `ClubeLeitura` recebendo `Professor`, `ObraLiteraria` e `EspacoEncontro`.
- Validei que cada parâmetro seja diferente de `null` no momento da criação.
- Garanti que o clube armazene referências reais para os objetos, não apenas seus nomes ou títulos em texto.

Por que isso é importante?

Uma associação obrigatória garante que um clube de leitura não exista sem seu mediador, sua obra principal ou seu local de encontro. Isso reforça a modelagem 1:1 obrigatória do domínio.

O código-chave desta etapa está em `ClubeLeitura.cs`, no construtor:

```csharp
public ClubeLeitura(Professor professorMediador, ObraLiteraria obraPrincipal, EspacoEncontro espacoEncontro)
{
    ProfessorMediador = professorMediador ?? throw new ArgumentNullException(nameof(professorMediador));
    ObraPrincipal = obraPrincipal ?? throw new ArgumentNullException(nameof(obraPrincipal));
    EspacoEncontro = espacoEncontro ?? throw new ArgumentNullException(nameof(espacoEncontro));
}
```

A próxima etapa será adicionar o professor convidado opcional e validar essa associação.

## Etapa 5: associação opcional de professor convidado

Nesta etapa, implementei a possibilidade de um clube ter um professor convidado opcional, além do mediador.

O que foi feito:

- Adicionei a propriedade `Professor? ProfessorConvidado` em `ClubeLeitura`.
- Criei o método `DefinirProfessorConvidado(Professor professor)` para definir o convidado.
- Validei que o professor informado não seja `null` e que não seja o mesmo que o mediador.
- Garanti que a propriedade seja opcional, permitindo que o clube seja criado sem convidado.

Por que isso é importante?

Essa associação opcional mostra como modelar dependências que não são obrigatórias no domínio. O clube pode funcionar sem convidado, mas se houver, ele deve ser válido e diferente do mediador.

O código-chave desta etapa está em `ClubeLeitura.cs`:

```csharp
public Professor? ProfessorConvidado { get; private set; }

public void DefinirProfessorConvidado(Professor professor)
{
    if (professor is null)
    {
        throw new ArgumentNullException(nameof(professor));
    }

    if (ReferenceEquals(professor, ProfessorMediador) || professor.Nome == ProfessorMediador.Nome)
    {
        throw new InvalidOperationException("O professor convidado não pode ser o mesmo professor mediador.");
    }

    ProfessorConvidado = professor;
}
```

A próxima etapa será gerenciar os participantes do clube, adicionando estudantes e validando duplicatas.

## Etapa 6: associação 1:N com estudantes participantes

Nesta etapa, implementei a gestão de estudantes participantes no clube, representando uma associação 1:N.

O que foi feito:

- Mantive uma coleção privada `List<Estudante>` em `ClubeLeitura`.
- Expus a coleção como `IReadOnlyCollection<Estudante>` para leitura externa.
- Criei o método `AdicionarParticipante(Estudante estudante)` para incluir estudantes.
- Validei que o estudante não seja `null` e que não haja duplicatas (baseado no nome).

Por que isso é importante?

Essa associação 1:N permite que um clube tenha vários estudantes, mas cada estudante pode participar de diferentes clubes. Isso modela corretamente o domínio onde estudantes existem independentemente do clube.

O código-chave desta etapa está em `ClubeLeitura.cs`:

```csharp
private readonly List<Estudante> _participantes = new();
public IReadOnlyCollection<Estudante> Participantes => _participantes.AsReadOnly();

public void AdicionarParticipante(Estudante estudante)
{
    if (estudante is null)
    {
        throw new ArgumentNullException(nameof(estudante));
    }

    if (_participantes.Any(p => p.Nome == estudante.Nome))
    {
        throw new InvalidOperationException("Estudante já está participando do clube.");
    }

    _participantes.Add(estudante);
}
```

A próxima etapa será associar materiais de apoio por agregação, permitindo reutilização de materiais em diferentes clubes.

## Etapa 7: materiais de apoio como agregação

Nesta etapa, implementei a associação de materiais de apoio ao clube, usando o conceito de agregação.

O que foi feito:

- Mantive uma coleção privada `List<MaterialApoio>` em `ClubeLeitura`.
- Expus a coleção como `IReadOnlyCollection<MaterialApoio>` para leitura externa.
- Criei o método `AdicionarMaterialApoio(MaterialApoio materialApoio)` para associar materiais.
- Validei que o material não seja `null`.

Por que isso é importante?

Materiais de apoio existem independentemente do clube e podem ser reutilizados em diferentes atividades. Essa agregação permite que um material seja associado a vários clubes sem ser "possuído" por nenhum deles.

O código-chave desta etapa está em `ClubeLeitura.cs`:

```csharp
private readonly List<MaterialApoio> _materiaisApoio = new();
public IReadOnlyCollection<MaterialApoio> MateriaisApoio => _materiaisApoio.AsReadOnly();

public void AdicionarMaterialApoio(MaterialApoio materialApoio)
{
    if (materialApoio is null)
    {
        throw new ArgumentNullException(nameof(materialApoio));
    }

    _materiaisApoio.Add(materialApoio);
}
```

A próxima etapa será planejar encontros de leitura e registrar contribuições, implementando composição.

## Etapa 8: encontros de leitura e composição

Nesta etapa, implementei a gestão de encontros de leitura no clube, usando o conceito de composição.

O que foi feito:

- Mantive uma coleção privada `List<EncontroLeitura>` em `ClubeLeitura`.
- Expus a coleção como `IReadOnlyCollection<EncontroLeitura>` para leitura externa.
- Criei o método `PlanejarEncontro(EncontroLeitura encontro)` para adicionar encontros.
- Validei que o encontro não seja `null`.

Por que isso é importante?

Encontros de leitura existem somente dentro do clube e fazem sentido apenas naquele contexto. Essa composição garante que encontros sejam criados especificamente para o clube e não sejam reutilizados em outros.

O código-chave desta etapa está em `ClubeLeitura.cs`:

```csharp
private readonly List<EncontroLeitura> _encontros = new();
public IReadOnlyCollection<EncontroLeitura> Encontros => _encontros.AsReadOnly();

public void PlanejarEncontro(EncontroLeitura encontro)
{
    if (encontro is null)
    {
        throw new ArgumentNullException(nameof(encontro));
    }

    _encontros.Add(encontro);
}
```

A próxima etapa será registrar contribuições de leitura e ligar cada contribuição ao encontro responsável.

## Etapa 9: contribuições de leitura

Nesta etapa, implementei o registro de contribuições dentro dos encontros de leitura.

O que foi feito:

- Adicionei uma coleção privada `List<ContribuicaoLeitura>` em `EncontroLeitura`.
- Expus as contribuições como `IReadOnlyCollection<ContribuicaoLeitura>`.
- Criei o método `AdicionarContribuicao(ContribuicaoLeitura contribuicao)` em `EncontroLeitura`.
- Criei o método `RegistrarContribuicao(EncontroLeitura encontro, ContribuicaoLeitura contribuicao)` em `ClubeLeitura`, garantindo que o encontro pertença ao clube.

Por que isso é importante?

Contribuições de leitura fazem sentido como parte do encontro específico. Registrar a contribuição dentro do encontro mantém a lógica de domínio coesa e permite validar que cada encontro obrigatório tenha suas contribuições.

O código-chave desta etapa está em `EncontroLeitura.cs` e `ClubeLeitura.cs`:

```csharp
private readonly List<ContribuicaoLeitura> _contribuicoes = new();
public IReadOnlyCollection<ContribuicaoLeitura> Contribuicoes => _contribuicoes.AsReadOnly();

public void AdicionarContribuicao(ContribuicaoLeitura contribuicao)
{
    if (contribuicao is null)
    {
        throw new ArgumentNullException(nameof(contribuicao));
    }

    _contribuicoes.Add(contribuicao);
}
```

```csharp
public void RegistrarContribuicao(EncontroLeitura encontro, ContribuicaoLeitura contribuicao)
{
    if (encontro is null)
    {
        throw new ArgumentNullException(nameof(encontro));
    }

    if (contribuicao is null)
    {
        throw new ArgumentNullException(nameof(contribuicao));
    }

    if (!_encontros.Contains(encontro))
    {
        throw new InvalidOperationException("O encontro informado não pertence a este clube.");
    }

    encontro.AdicionarContribuicao(contribuicao);
}
```

A próxima etapa será implementar as regras de encerramento do clube, protegendo invariantes do domínio.

## Etapa 10: regras de encerramento do clube

Nesta etapa, implementei o método `Encerrar()` em `ClubeLeitura` para proteger as condições que devem existir antes de encerrar o clube.

O que foi feito:

- Adicionei a propriedade de estado `Encerrado` em `ClubeLeitura`.
- Criei o método `Encerrar()` que valida:
  - pelo menos um estudante participante;
  - pelo menos um encontro obrigatório;
  - todos os encontros obrigatórios têm contribuição registrada.
- Lancei `InvalidOperationException` quando alguma regra falha.

Por que isso é importante?

Encerrar um clube é uma ação de negócio que só deve ocorrer em um estado válido. Ao verificar as invariantes antes do encerramento, o modelo impede que o clube seja finalizado em condição incompleta.

O código-chave desta etapa está em `ClubeLeitura.cs`:

```csharp
public void Encerrar()
{
    if (_encerrado)
    {
        throw new InvalidOperationException("O clube já foi encerrado.");
    }

    if (!_participantes.Any())
    {
        throw new InvalidOperationException("Um clube só pode ser encerrado se possuir pelo menos um estudante participante.");
    }

    var encontrosObrigatorios = _encontros.Where(encontro => encontro.Obrigatorio).ToList();
    if (!encontrosObrigatorios.Any())
    {
        throw new InvalidOperationException("Um clube só pode ser encerrado se possuir pelo menos um encontro obrigatório.");
    }

    if (encontrosObrigatorios.Any(encontro => !encontro.Contribuicoes.Any()))
    {
        throw new InvalidOperationException("Todos os encontros obrigatórios devem ter contribuição registrada.");
    }

    _encerrado = true;
}
```

A próxima etapa será testar o modelo no `Program.cs`.

## Etapa 10: regras de encerramento do clube

Nesta etapa, implementei o método `Encerrar()` em `ClubeLeitura` para proteger as condições que devem existir antes de encerrar o clube.

O que foi feito:

- Adicionei a propriedade de estado `Encerrado` em `ClubeLeitura`.
- Criei o método `Encerrar()` que valida:
  - pelo menos um estudante participante;
  - pelo menos um encontro obrigatório;
  - todos os encontros obrigatórios têm contribuição registrada.
- Lancei `InvalidOperationException` quando alguma regra falha.

Por que isso é importante?

Encerrar um clube é uma ação de negócio que só deve ocorrer em um estado válido. Ao verificar as invariantes antes do encerramento, o modelo impede que o clube seja finalizado em condição incompleta.

O código-chave desta etapa está em `ClubeLeitura.cs`:

```csharp
public void Encerrar()
{
    if (_encerrado)
    {
        throw new InvalidOperationException("O clube já foi encerrado.");
    }

    if (!_participantes.Any())
    {
        throw new InvalidOperationException("Um clube só pode ser encerrado se possuir pelo menos um estudante participante.");
    }

    var encontrosObrigatorios = _encontros.Where(encontro => encontro.Obrigatorio).ToList();
    if (!encontrosObrigatorios.Any())
    {
        throw new InvalidOperationException("Um clube só pode ser encerrado se possuir pelo menos um encontro obrigatório.");
    }

    if (encontrosObrigatorios.Any(encontro => !encontro.Contribuicoes.Any()))
    {
        throw new InvalidOperationException("Todos os encontros obrigatórios devem ter contribuição registrada.");
    }

    _encerrado = true;
}
```

A próxima etapa será testar o modelo no `Program.cs`.
## Etapa 11: usar Program.cs para testar o modelo

Nesta etapa, criei um exemplo em `Program.cs` que verifica o modelo completo do clube de leitura.

O que foi feito:

- Criei as entidades necessárias: mediador, obra, espaço e professor convidado.
- Adicionei estudantes participantes.
- Associei materiais de apoio ao clube.
- Planejei encontros obrigatórios e opcionais.
- Registrei contribuições para os encontros.
- Tentei encerrar o clube e confirmei que as regras de domínio foram atendidas.

Por que isso é importante?

Testar o modelo em código real mostra se as regras de negócio funcionam na prática. Essa etapa é a prova de que as classes e métodos foram implementados de forma consistente.

O código-chave desta etapa está em `Program.cs`:

```csharp
clube.DefinirProfessorConvidado(professorConvidado);
clube.AdicionarParticipante(new Estudante("Ana"));
clube.AdicionarParticipante(new Estudante("Bruno"));
clube.AdicionarMaterialApoio(new MaterialApoio("Guia de discussão"));
clube.PlanejarEncontro(encontro1);
clube.RegistrarContribuicao(encontro1, new ContribuicaoLeitura("A obra aborda temas sociais importantes."));
clube.Encerrar();
```

A próxima etapa será revisar o modelo e documentar o resultado.
