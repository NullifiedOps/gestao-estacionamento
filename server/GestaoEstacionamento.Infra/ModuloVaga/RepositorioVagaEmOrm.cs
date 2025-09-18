using GestaoEstacionamento.Core.Dominio.ModuloVaga;
using GestaoEstacionamento.Infra.Orm.Compartilhado;

namespace GestaoEstacionamento.Infra.Orm.ModuloVaga;

public class RepositorioVagaEmOrm(AppDbContext contexto) 
    : RepositorioBaseEmOrm<Vaga>(contexto), IRepositorioVaga;
