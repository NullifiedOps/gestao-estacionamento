﻿using GestaoEstacionamento.Infra.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace GestaoEstacionamento.WebApi.Orm;

public static class DatabaseOperations
{
    public static void ApplyMigrations(this IHost app)
    {
        var scope = app.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        dbContext.Database.Migrate();
    }
}
