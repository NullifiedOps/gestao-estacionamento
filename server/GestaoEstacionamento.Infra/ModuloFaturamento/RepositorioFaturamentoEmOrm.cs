using GestaoEstacionamento.Core.Dominio.ModuloFaturamento;
using GestaoEstacionamento.Infra.Orm.Compartilhado;

namespace GestaoEstacionamento.Infra.Orm.ModuloFaturamento;

public class RepositorioFaturamentoEmOrm(AppDbContext context) 
    : RepositorioBaseEmOrm<Faturamento>(context), IRepositorioFaturamento;
