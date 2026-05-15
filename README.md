# 📚 Clube de Leitura

Um projeto de exercício em Programação Orientada a Objetos (POO) desenvolvido com **C#** para gerenciar um clube de leitura.

## 📋 Descrição

Este projeto propõe uma solução completa para gerenciar um clube de leitura, incluindo funcionalidades como:
- Gestão de livros e autores
- Controle de membros do clube
- Registro de leituras e avaliações
- Gerenciamento de reuniões e discussões

## 🏗️ Estrutura do Projeto

```
ExercicioPOO/
├── 📁 src/                          # Código-fonte principal
│   ├── 📁 Modelos/                 # Classes de domínio (entidades)
│   ├── 📁 Servicos/                # Lógica de negócios
│   ├── 📁 Interface/               # Interface com usuário
│   └── 📁 Utilitarios/             # Classes auxiliares
├── 📁 testes/                       # Testes unitários
├── 📁 documentacao/                 # Documentaç��o do projeto
└── README.md                        # Este arquivo
```

## 🛠️ Tecnologias Utilizadas

- **Linguagem:** C#
- **Paradigma:** Programação Orientada a Objetos (POO)
- **.NET Framework / .NET Core**

## 🎯 Conceitos de POO Aplicados

- ✅ **Encapsulamento:** Proteção de dados através de propriedades e modificadores de acesso
- ✅ **Herança:** Reutilização de código através de hierarquias de classes
- ✅ **Polimorfismo:** Diferentes comportamentos para diferentes tipos
- ✅ **Abstração:** Interfaces e classes abstratas para contrato de comportamento

## 📝 Como Usar

1. Clone o repositório:
```bash
git clone https://github.com/patojubileu987-png/ExercicioPOO.git
```

2. Navegue até o diretório:
```bash
cd ExercicioPOO
```

3. Compile o projeto:
```bash
dotnet build
```

4. Execute:
```bash
dotnet run
```

## 📚 Estrutura de Pastas Detalhada

### `src/Modelos/`
Contém as classes principais que representam entidades do domínio:
- `Livro.cs`
- `Autor.cs`
- `Membro.cs`
- `Reuniao.cs`
- `Avaliacao.cs`

### `src/Servicos/`
Implementa a lógica de negócios:
- `GerenciadorLivros.cs`
- `GerenciadorMembros.cs`
- `GerenciadorReuniao.cs`

### `src/Interface/`
Interface com o usuário:
- `Menu.cs`
- `ProcessadorEntrada.cs`

### `src/Utilitarios/`
Classes auxiliares e utilitários:
- `Validadores.cs`
- `Conversores.cs`

## 👨‍💻 Autor

**patojubileu987-png**

## 📄 Licença

Este projeto é de código aberto e disponível sob licença MIT.

## 🤝 Contribuições

Sugestões e contribuições são bem-vindas! Sinta-se à vontade para abrir uma issue ou pull request.

---

**Última atualização:** 15/05/2026
