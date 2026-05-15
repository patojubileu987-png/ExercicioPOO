namespace ProgramaClubeLeitura;

public class ClubeLeitura
{
    private readonly List<Estudante> _participantes = new();
    private readonly List<MaterialApoio> _materiaisApoio = new();
    private readonly List<EncontroLeitura> _encontros = new();
    private bool _encerrado;

    public Professor ProfessorMediador { get; }
    public ObraLiteraria ObraPrincipal { get; }
    public EspacoEncontro EspacoEncontro { get; }
    public Professor? ProfessorConvidado { get; private set; }

    public ClubeLeitura(Professor professorMediador, ObraLiteraria obraPrincipal, EspacoEncontro espacoEncontro)
    {
        ProfessorMediador = professorMediador ?? throw new ArgumentNullException(nameof(professorMediador));
        ObraPrincipal = obraPrincipal ?? throw new ArgumentNullException(nameof(obraPrincipal));
        EspacoEncontro = espacoEncontro ?? throw new ArgumentNullException(nameof(espacoEncontro));
    }

    public IReadOnlyCollection<Estudante> Participantes => _participantes.AsReadOnly();
    public IReadOnlyCollection<MaterialApoio> MateriaisApoio => _materiaisApoio.AsReadOnly();
    public IReadOnlyCollection<EncontroLeitura> Encontros => _encontros.AsReadOnly();
    public bool Encerrado => _encerrado;
    public double PercentualEncontrosValidos => _encontros.Count == 0 ? 0 : _encontros.Count(encontro => encontro.Contribuicoes.Any()) * 100.0 / _encontros.Count;

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

    public void RemoverParticipante(Estudante estudante)
    {
        if (estudante is null)
        {
            throw new ArgumentNullException(nameof(estudante));
        }

        if (!_participantes.Remove(estudante))
        {
            throw new InvalidOperationException("O estudante informado não está participando do clube.");
        }
    }

    public void AdicionarMaterialApoio(MaterialApoio materialApoio)
    {
        if (materialApoio is null)
        {
            throw new ArgumentNullException(nameof(materialApoio));
        }

        _materiaisApoio.Add(materialApoio);
    }

    public void RemoverMaterialApoio(MaterialApoio materialApoio)
    {
        if (materialApoio is null)
        {
            throw new ArgumentNullException(nameof(materialApoio));
        }

        if (!_materiaisApoio.Remove(materialApoio))
        {
            throw new InvalidOperationException("O material de apoio informado não está associado a este clube.");
        }
    }

    public void PlanejarEncontro(EncontroLeitura encontro)
    {
        if (encontro is null)
        {
            throw new ArgumentNullException(nameof(encontro));
        }

        _encontros.Add(encontro);
    }

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
}
