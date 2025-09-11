using GestaoEstacionamento.Core.Aplicacao;
using GestaoEstacionamento.Infra.Orm;
using GestaoEstacionamento.WebApi.AutoMapper;
using GestaoEstacionamento.WebApi.Identity;
using GestaoEstacionamento.WebApi.Orm;
using GestaoEstacionamento.WebApi.Swagger;

namespace GestaoEstacionamento.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services
            .AddCamadaAplicacao(builder.Logging, builder.Configuration)
            .AddCamadaInfraestruturaOrm(builder.Configuration);

        builder.Services.AddAutoMapperProfiles(builder.Configuration);

        builder.Services.AddIdentityProviderConfig(builder.Configuration);

        builder.Services.AddControllers();

        // Swagger/OpenAPI https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddSwaggerConfig();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.ApplyMigrations();

            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
