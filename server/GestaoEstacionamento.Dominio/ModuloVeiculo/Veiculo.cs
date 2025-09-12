using GestaoEstacionamento.Core.Dominio.Compartilhado;

namespace GestaoEstacionamento.Core.Dominio.ModuloVeiculo;

public class Veiculo : EntidadeBase<Veiculo>
{
    public string Placa { get; set; }
    public string Modelo { get; set; }
    public string Cor { get; set; }
    public string? Detalhes { get; set; }
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public string Telefone { get; set; }

    public Veiculo() { }

    public Veiculo(
        string placa, 
        string modelo, 
        string cor, 
        string? detalhes, 
        string nome, 
        string cpf, 
        string telefone) : this()
    {
        Placa = placa;
        Modelo = modelo;
        Cor = cor;
        Detalhes = detalhes;
        Nome = nome;
        Cpf = cpf;
        Telefone = telefone;
    }

    public override void AtualizarRegistro(Veiculo registroEditado)
    {
        Placa = registroEditado.Placa;
        Modelo = registroEditado.Modelo;
        Cor = registroEditado.Cor;
        Detalhes = registroEditado.Detalhes;
        Nome = registroEditado.Nome;
        Cpf = registroEditado.Cpf;
        Telefone = registroEditado.Telefone;
    }
}
