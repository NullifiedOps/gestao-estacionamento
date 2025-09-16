using GestaoEstacionamento.Core.Dominio.Compartilhado;

namespace GestaoEstacionamento.Core.Dominio.ModuloVeiculo;

public class Ticket : EntidadeBase<Ticket>
{
    public int Numero { get; set; }
    public Guid VeiculoId { get; set; }
    public Veiculo Veiculo { get; set; }
    public DateTime DataEntrada { get; set; }
    public DateTime? DataSaida { get; set; }
    public bool Encerrado => DataSaida.HasValue;

    public Ticket() { }

    public Ticket(Veiculo veiculo, int numero) : this()
    {
        Veiculo = veiculo;
        Numero = numero;
        DataEntrada = DateTime.UtcNow;
    }

    public bool Encerrar()
    {
        if (Encerrado)
            return false;
     
        DataSaida = DateTime.UtcNow;
        return true;
    }

    public override void AtualizarRegistro(Ticket registroEditado)
    {
        throw new NotImplementedException();
    }
}
