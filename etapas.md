# Etapas para aprender a modelar o clube de leitura

## Checklist de conclusão

- [x] Ler e compreender o enunciado de `exercicio.md`
- [x] Identificar as entidades principais do domínio
- [x] Criar as classes básicas e seus construtores
- [x] Validar valores obrigatórios em todas as classes
- [x] Implementar `ClubeLeitura` com associações obrigatórias
- [x] Adicionar professor convidado opcional
- [x] Gerenciar participantes do clube
- [x] Associar materiais de apoio por agregação
- [x] Planejar encontros e registrar contribuições
- [x] Implementar regras de encerramento do clube
- [x] Testar o modelo em `Program.cs`

Este documento apresenta um roteiro passo a passo para implementar o exercício em `exercicio.md` usando classes, atributos e métodos em C#.

## Etapa 1: entender o domínio

1. Leia o enunciado com atenção.
2. Identifique as entidades principais: `Professor`, `Estudante`, `ObraLiteraria`, `EspacoEncontro`, `ClubeLeitura`, `MaterialApoio`, `EncontroLeitura` e `ContribuicaoLeitura`.
3. Observe as relações entre essas entidades:
   - `ClubeLeitura` depende obrigatoriamente de `Professor`, `ObraLiteraria` e `EspacoEncontro`.
   - `ClubeLeitura` possui muitos `Estudante`.
   - `ClubeLeitura` pode ter um `Professor` convidado opcional.
   - `MaterialApoio` existe fora do clube e pode ser reutilizado.
   - `EncontroLeitura` existe somente dentro do clube.
4. Entenda que o objetivo é modelar com qualidade, sem banco de dados, sem interface gráfica e sem frameworks externos.

## Etapa 2: criar as classes básicas

1. Comece criando uma classe para cada entidade esperada.
2. Defina os atributos principais de cada classe como propriedades privadas e somente leitura quando apropriado.
3. Use construtores para receber os dados obrigatórios.
4. Garanta que as classes sejam válidas desde a criação, evitando valores nulos, vazios ou compostos apenas por espaços.

### Exemplo de atributos por classe

- `Professor`: `Nome`.
- `Estudante`: `Nome`.
- `ObraLiteraria`: `Titulo`.
- `EspacoEncontro`: `Identificacao`.
- `MaterialApoio`: `Descricao`.
- `EncontroLeitura`: `Tema`, `Obrigatorio`.
- `ContribuicaoLeitura`: `Texto`.

## Etapa 3: proteger os dados e validar entradas

1. Em cada classe, valide textos obrigatórios no construtor e em métodos de alteração.
2. Use uma validação comum, por exemplo:
   - se string for `null`, lance `ArgumentNullException`;
   - se string for vazia ou só espaços, lance `ArgumentException`.
3. Evite expor campos mutáveis diretamente. Use propriedades somente leitura ou coleções protegidas.
4. Se um valor inválido for informado, o objeto não deve ser criado em estado inconsistente.

## Etapa 4: implementar `ClubeLeitura` com associações obrigatórias

1. Crie a classe `ClubeLeitura` com construtor que recebe:
   - `Professor professorMediador`
   - `ObraLiteraria obraPrincipal`
   - `EspacoEncontro espacoEncontro`
2. Valide no construtor que cada parâmetro não seja `null`.
3. Armazene as referências reais aos objetos, não apenas strings.
4. Esse passo aborda o conceito de associação 1:1 obrigatória.

## Etapa 5: associação opcional de professor convidado

1. Adicione uma propriedade opcional `Professor ProfessorConvidado` em `ClubeLeitura`.
2. Crie um método como `DefinirProfessorConvidado(Professor professor)`.
3. Valide que:
   - o professor informado não seja `null`;
   - não seja o mesmo que o mediador.
4. Esse método mostra como modelar uma dependência opcional.

## Etapa 6: associação 1:N com estudantes participantes

1. No `ClubeLeitura`, mantenha uma coleção privada de `Estudante`.
2. Utilize tipos de coleção imutáveis ou exposições controladas, por exemplo:
   - `IReadOnlyCollection<Estudante>` para leitura externa;
   - `List<Estudante>` internamente.
3. Crie métodos como:
   - `AdicionarParticipante(Estudante estudante)`;
   - `RemoverParticipante(Estudante estudante)`.
4. Valide que o estudante não seja `null` e que não haja duplicatas.

## Etapa 7: materiais de apoio como agregação

1. Crie a classe `MaterialApoio` com seus atributos.
2. No `ClubeLeitura`, mantenha uma lista de materiais usados.
3. A relação é de agregação: os materiais existem independentemente do clube.
4. Crie métodos para associar materiais ao clube, como `AdicionarMaterialApoio(MaterialApoio material)`.

## Etapa 8: encontros de leitura e composição

1. Crie a classe `EncontroLeitura` com dados como `Tema`, `Descricao`, `Data` e se é obrigatório.
2. Em `ClubeLeitura`, mantenha a coleção de encontros planejados.
3. Cada encontro pertence ao clube e faz sentido apenas naquele clube: isso é composição.
4. Crie métodos para gerenciar encontros, como:
   - `PlanejarEncontro(EncontroLeitura encontro)`;
   - `RegistrarContribuicao(EncontroLeitura encontro, ContribuicaoLeitura contribuicao)`.

## Etapa 9: contribuições de leitura

1. Crie a classe `ContribuicaoLeitura` com um texto obrigatório.
2. Registre contribuições ligadas a um `EncontroLeitura`.
3. Garanta que a contribuição seja válida e não possa ser criada com texto inválido.
4. Use métodos do encontro para adicionar contribuições, mantendo a lógica de domínio coesa.

## Etapa 10: regras de encerramento do clube

1. Implemente um método em `ClubeLeitura` para encerrar o clube, por exemplo `Encerrar()`.
2. Verifique as regras:
   - pelo menos um estudante participante;
   - pelo menos um encontro obrigatório;
   - todos os encontros obrigatórios têm contribuição registrada.
3. Se alguma regra falhar, lance `InvalidOperationException`.
4. Esse passo mostra como proteger invariantes do domínio.

## Etapa 11: usar `Program.cs` para testar o modelo

1. Crie alguns objetos em `Program.cs` para testar a modelagem:
   - um `Professor` mediador;
   - uma `ObraLiteraria`;
   - um `EspacoEncontro`;
   - um `ClubeLeitura`.
2. Adicione estudantes, materiais e encontros.
3. Registre contribuições para encontros obrigatórios.
4. Tente encerrar o clube e observe se as regras funcionam.

## Etapa 12: revisar e refinar

1. Reveja se todas as validações estão implementadas.
2. Confirme que nenhum campo obrigatório aceita valores inválidos.
3. Verifique se as coleções não são expostas diretamente.
4. Ajuste nomes de métodos e propriedades para ficar claro e legível.

---

### Dica final

O aprendizado aqui é sobre modelagem orientada a objetos: pense em cada classe como um conceito real, use construtores para obrigar estados válidos, e mantenha o controle das relações entre objetos.
