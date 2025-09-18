using GestaoEstacionamento.Core.Dominio.Compartilhado;
using GestaoEstacionamento.Core.Dominio.ModuloVeiculo;

namespace GestaoEstacionamento.Core.Dominio.ModuloVaga;

public class Vaga : EntidadeBase<Vaga>
{
    public string Zona { get; set; }
    public string Identificador { get; set; }
    public Veiculo? Veiculo { get; set; }
    public Guid? VeiculoId { get; set; }
    public bool Ocupada => Veiculo != null;

    public Vaga() { }

    public Vaga(string zona, string identificador) : this()
    {
        Zona = zona;
        Identificador = identificador;
    }

    public bool EstacionarVeiculo(Veiculo veiculo)
    {
        if (Ocupada)
            return false;
        Veiculo = veiculo;
        VeiculoId = veiculo.Id;
        return true;
    }

    public bool RemoverVeiculo()
    {
        if (!Ocupada)
            return false;
        Veiculo = null;
        VeiculoId = null;
        return true;
    }

    public override void AtualizarRegistro(Vaga registroEditado)
    {
        Zona = registroEditado.Zona;
        Identificador = registroEditado.Identificador;
    }
}
