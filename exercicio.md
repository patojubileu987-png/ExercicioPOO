# Enunciado

Uma instituição de ensino deseja desenvolver parte de um sistema para organizar clubes de leitura acadêmica.

O sistema deve controlar:

- o clube;
- o professor mediador;
- a obra principal escolhida;
- o espaço de encontro;
- os estudantes participantes;
- os materiais de apoio utilizados;
- os encontros planejados ao longo do período.

Seu objetivo é implementar, em C#, um modelo orientado a objetos para esse domínio, aplicando corretamente os conceitos de:

- associação 1:1;
- associação 1:N;
- dependência obrigatória;
- dependência opcional;
- encapsulamento;
- validação;
- proteção de invariantes;
- agregação;
- composição.

A implementação deve ser feita:

- sem banco de dados;
- sem interface gráfica;
- sem frameworks externos.

O foco da prova é a qualidade da modelagem orientada a objetos.

---

# Contexto do domínio

Um clube de leitura acadêmica representa uma atividade organizada para leitura, discussão e reflexão sobre uma obra selecionada.

Cada clube possui:

- um professor mediador;
- uma obra principal;
- um espaço de encontro;
- uma lista de estudantes participantes;
- uma lista de materiais de apoio;
- um conjunto de encontros planejados.

A instituição já possui estudantes cadastrados.

Esses estudantes podem participar de diferentes clubes de leitura ao longo do semestre. Portanto, um estudante:

- não nasce dentro de um clube;
- não deixa de existir quando o clube é encerrado.

A instituição também possui materiais de apoio cadastrados, como:

- artigos;
- resenhas;
- vídeos;
- capítulos complementares;
- roteiros de discussão;
- guias de leitura.

Esses materiais existem independentemente do clube e podem ser reutilizados em diferentes atividades acadêmicas.

Por outro lado, os encontros do clube são criados especificamente para aquele clube de leitura.

Um encontro como:

- “Discussão inicial da obra”;
- “Análise dos personagens”;
- “Debate sobre o contexto histórico”;
- “Síntese final”;

faz sentido apenas dentro do clube em que foi planejado.

Durante os encontros, participantes podem registrar contribuições de leitura.

Um clube somente pode ser encerrado se:

- possuir pelo menos um estudante participante;
- possuir pelo menos um encontro obrigatório;
- todos os encontros obrigatórios tiverem contribuição registrada.

---

# Classes mínimas esperadas

Você deve criar, no mínimo, as seguintes classes:

- `Professor`
- `Estudante`
- `ObraLiteraria`
- `EspacoEncontro`
- `ClubeLeitura`
- `MaterialApoio`
- `EncontroLeitura`
- `ContribuicaoLeitura`

Você pode criar outras classes auxiliares, enums ou métodos de apoio, caso julgue necessário.

---

# Requisitos de modelagem

## 1. Objetos válidos desde a criação

Todas as classes devem proteger seus dados internos.

Campos textuais obrigatórios não podem aceitar valores:

- nulos;
- vazios;
- compostos apenas por espaços.

Exemplos de dados que devem ser validados:

- nome do professor;
- nome do estudante;
- título da obra literária;
- identificação do espaço de encontro;
- descrição do material de apoio;
- tema do encontro de leitura;
- texto da contribuição de leitura.

Sempre que um valor inválido for informado, o objeto não deve ser criado ou alterado para um estado inconsistente.

Use exceções apropriadas, como:

- `ArgumentException`
- `InvalidOperationException`

quando necessário.

---

## 2. Associação 1:1 obrigatória

A classe `ClubeLeitura` deve possuir associação obrigatória com:

- um `Professor` mediador;
- uma `ObraLiteraria` principal;
- um `EspacoEncontro`.

Um clube de leitura não pode existir sem:

- professor mediador;
- obra principal;
- espaço de encontro.

Essas associações devem ser recebidas pelo construtor da classe `ClubeLeitura` e validadas no momento da criação.

Não é permitido representar essas relações usando apenas `string`, como:

- `NomeProfessor`
- `TituloObra`
- `NomeEspaco`

O correto é que o clube mantenha referências reais para objetos dos tipos:

- `Professor`
- `ObraLiteraria`
- `EspacoEncontro`

---

## 3. Associação 1:1 opcional

O clube pode possuir, opcionalmente, um professor convidado.

O professor convidado deve ser representado por um objeto do tipo `Professor`.

O clube deve poder ser criado sem professor convidado, mas deve permitir que ele seja definido posteriormente por meio de um método específico, por exemplo:

```csharp
DefinirProfessorConvidado(Professor professor)
```

Esse método deve validar se o professor informado é válido.

O professor convidado não pode ser o mesmo professor definido como mediador.

A propriedade do professor convidado deve deixar claro que a associação é opcional.

---

## 4. Associação 1:N com estudantes participantes

Um clube de leitura pode possuir vários estudantes participantes.

Os participantes são objetos do tipo `Estudante`.

A classe `ClubeLeitura` deve manter internamente uma coleção privada de estudantes participantes.

A lista não pode ser exposta diretamente como:

```csharp
List<Estudante>
```

pública com `set`.

A exposição externa deve permitir apenas leitura, por exemplo com:

```csharp
IReadOnlyCollection<Estudante>
```

A alteração da coleção deve ser controlada por métodos da própria classe `ClubeLeitura`, como:

```csharp
AdicionarParticipante(Estudante estudante)

RemoverParticipante(Estudante estudante)
```

---

## 5. Invariantes dos estudantes participantes

A coleção de participantes deve obedecer às seguintes regras:

- não pode aceitar estudante nulo;
- não pode permitir estudante duplicado;
- o clube deve ter pelo menos um estudante participante para ser encerrado;
- estudantes não podem ser adicionados ou removidos depois que o clube estiver encerrado;
- a remoção não pode ocorrer se o estudante informado não estiver associado ao clube;
- a remoção não pode deixar o clube sem participantes caso ele já possua encontros obrigatórios cadastrados.

Caso alguma dessas regras seja violada, uma exceção deve ser lançada.

---

## 6. Associação 1:N por agregação com materiais de apoio

O clube pode utilizar vários materiais de apoio.

A classe `MaterialApoio` deve representar um material que existe independentemente do clube.

Um material pode estar cadastrado previamente pela instituição e ser reutilizado em diferentes clubes ou atividades acadêmicas.

Por isso, a relação entre `ClubeLeitura` e `MaterialApoio` deve ser tratada como agregação.

Os materiais devem ser criados fora do clube e passados para ele quando necessário.

Exemplo conceitual:

```csharp
var artigo = new MaterialApoio(
    "Artigo complementar sobre o contexto da obra");

clube.AdicionarMaterialApoio(artigo);
```

O clube não deve criar internamente os materiais de apoio.

A coleção de materiais deve ser privada internamente e exposta apenas como leitura.

---

## 7. Invariantes dos materiais de apoio

A coleção de materiais de apoio deve obedecer às seguintes regras:

- não pode aceitar material nulo;
- não pode permitir material duplicado no mesmo clube;
- materiais não podem ser adicionados depois que o clube estiver encerrado;
- materiais não podem ser removidos depois que o clube estiver encerrado;
- a remoção não pode ocorrer se o material informado não estiver associado ao clube.

---

## 8. Composição com encontros de leitura

Os encontros de leitura pertencem ao clube.

A classe `EncontroLeitura` deve representar um encontro específico do clube, por exemplo:

- “Discussão inicial da obra”;
- “Análise dos personagens”;
- “Debate sobre o contexto histórico”;
- “Relação da obra com a atualidade”;
- “Síntese final da leitura”.

Esses encontros devem ser criados pela própria classe `ClubeLeitura`, e não recebidos prontos de fora.

Portanto, o método de inclusão deve receber os dados necessários para criar o encontro internamente, por exemplo:

```csharp
AdicionarEncontro(
    string tema,
    DateTime data,
    bool obrigatorio)
```

A própria classe `ClubeLeitura` deve instanciar o objeto `EncontroLeitura`.

Essa decisão representa uma composição: o encontro pertence ao clube e não deve existir de forma independente no sistema.

---

## 9. Invariantes dos encontros

A classe `ClubeLeitura` deve controlar a coleção de encontros de leitura.

A coleção deve ser privada internamente e exposta apenas para leitura.

Os encontros devem obedecer às seguintes regras:

- o tema do encontro é obrigatório;
- a data do encontro não pode ser anterior à data de criação do clube;
- não pode haver dois encontros com o mesmo tema no mesmo clube;
- o clube precisa ter pelo menos um encontro obrigatório para ser encerrado;
- encontros não podem ser adicionados após o clube ser encerrado;
- todo encontro obrigatório deve receber pelo menos uma contribuição antes do encerramento do clube.

---

## 10. Composição com contribuições de leitura

A classe `ContribuicaoLeitura` deve representar o registro feito por um estudante participante sobre um encontro de leitura.

Cada contribuição deve estar associada a:

- um estudante participante;
- um encontro de leitura;
- uma descrição textual da contribuição;
- uma indicação se a contribuição foi validada ou não.

A contribuição deve ser registrada por meio de um método da classe `ClubeLeitura`, por exemplo:

```csharp
RegistrarContribuicao(
    Estudante estudante,
    EncontroLeitura encontro,
    string texto,
    bool validada)
```

A classe `ClubeLeitura` deve validar:

- se o estudante informado participa do clube;
- se o encontro pertence ao clube;
- se o texto da contribuição é obrigatório;
- se o mesmo estudante não registrou duas contribuições para o mesmo encontro;
- se não é possível registrar contribuição depois que o clube foi encerrado.

As contribuições devem ser controladas internamente pelo clube.

---

## 11. Encerramento do clube de leitura

A classe `ClubeLeitura` deve possuir um método:

```csharp
Encerrar()
```

Esse método deve verificar todas as invariantes necessárias antes de alterar o estado do clube para encerrado.

O clube só pode ser encerrado se:

- possuir professor mediador válido;
- possuir obra literária válida;
- possuir espaço de encontro válido;
- possuir pelo menos um estudante participante;
- possuir pelo menos um encontro obrigatório;
- todos os encontros obrigatórios tiverem pelo menos uma contribuição registrada;
- não houver estudante participante duplicado;
- não houver material de apoio duplicado;
- não houver encontro duplicado;
- o professor convidado, se existir, for diferente do professor mediador.

Depois de encerrado, o clube não pode mais receber:

- novos participantes;
- novos materiais;
- novos encontros;
- novas contribuições.

---

## 12. Cálculo do percentual de encontros com contribuição validada

A classe `ClubeLeitura` deve permitir consultar o percentual de encontros com contribuição validada.

O percentual deve considerar os encontros que possuem pelo menos uma contribuição marcada como validada.

A consulta do percentual de encontros com contribuição validada só deve ser permitida se o clube estiver encerrado.

Caso o percentual seja solicitado antes do encerramento, uma exceção deve ser lançada.

Exemplo conceitual:

- 5 encontros cadastrados;
- 4 encontros com pelo menos uma contribuição validada;
- percentual de encontros com contribuição validada: 80%.

---

## 13. Demonstração obrigatória

Crie um pequeno trecho de código de demonstração, em `Program.cs`, que mostre:

1. criação de professor mediador, obra literária, espaço de encontro, estudantes e materiais de apoio;
2. criação de um clube de leitura válido;
3. definição opcional de professor convidado;
4. adição de estudantes participantes;
5. adição de materiais de apoio;
6. adição de encontros de leitura;
7. registro de contribuições;
8. encerramento do clube;
9. exibição do percentual de encontros com contribuição validada.

Também demonstre pelo menos três tentativas inválidas, como:

- definir o mediador como professor convidado;
- adicionar estudante participante duplicado;
- adicionar material de apoio duplicado;
- registrar contribuição para estudante que não participa do clube;
- registrar contribuição para encontro que não pertence ao clube;
- encerrar clube sem contribuição em encontro obrigatório;
- adicionar encontro depois do clube encerrado;
- criar objeto com texto obrigatório vazio;
- remover material que não está associado ao clube.

As tentativas inválidas devem ser tratadas com `try/catch`, exibindo mensagens adequadas no console.

---

# Regras técnicas obrigatórias

A solução deve respeitar os seguintes critérios:

- usar propriedades com `private set` ou somente leitura quando adequado;
- evitar atributos públicos modificáveis diretamente;
- não expor listas internas como `List<T>` pública;
- usar `private readonly List<T>` para coleções internas;
- expor coleções como `IReadOnlyCollection<T>`;
- validar objetos nulos antes de associá-los;
- usar objetos reais em associações, e não apenas dados primitivos;
- distinguir corretamente associação obrigatória e opcional;
- aplicar agregação quando os objetos têm ciclo de vida independente;
- aplicar composição quando o objeto parte pertence ao objeto todo;
- manter as regras de negócio dentro das classes responsáveis;
- impedir que código externo corrompa o estado interno dos objetos;
- garantir que as invariantes permaneçam válidas durante toda a vida do objeto.

---

# Conhecimentos prévios pressupostos

Esta prova pressupõe que o aluno já domina:

- criação de classes;
- atributos e métodos;
- construtores;
- encapsulamento;
- validação de dados;
- uso de exceções;
- listas em C#;
- propriedades;
- tipos nullable;
- métodos com parâmetros;
- instanciação de objetos;
- fundamentos de orientação a objetos.