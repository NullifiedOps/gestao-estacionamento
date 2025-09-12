using GestaoEstacionamento.Core.Dominio.ModuloVeiculo;
using GestaoEstacionamento.Infra.Orm.Compartilhado;

namespace GestaoEstacionamento.Infra.Orm.ModuloVeiculo;

public class RepositorioVeiculoEmOrm(AppDbContext contexto)
    : RepositorioBaseEmOrm<Veiculo>(contexto), IRepositorioVeiculo;
