using GestaoEstacionamento.Core.Dominio.Compartilhado;

namespace GestaoEstacionamento.Core.Dominio.ModuloFaturamento;

public class Faturamento : EntidadeBase<Faturamento>
{
    public string PlacaVeiculo { get; set; }
    public decimal Valor { get; set; }
    public DateTime DataFaturamento { get; set; }

    public Faturamento() { }

    public Faturamento(string placaVeiculo, decimal valor) : this()
    {
        PlacaVeiculo = placaVeiculo;
        Valor = valor;
        DataFaturamento = DateTime.UtcNow;
    }

    public override void AtualizarRegistro(Faturamento registroEditado)
    {
        throw new NotImplementedException();
    }
}
