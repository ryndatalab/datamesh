using Datamesh.API.BusinessLogic;
using Datamesh.API.Mutation;
using Datamesh.API.Query;
using Datamesh.API.Repositories;
using Datamesh.APIBusinessLogic;
using HotChocolate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Org.Eclipse.TractusX.Portal.Backend.PortalBackend.DBAccess;
using System;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var Configuration = builder.Configuration;
        builder.Services
            .AddScoped<IPortalRepositories, PortalRepositories>()
            .AddDbContext<PortalDbContext>(options =>
        options.UseNpgsql(Configuration.GetConnectionString("PortalDb"), 
        b => b.MigrationsAssembly("Datamesh.API")), ServiceLifetime.Transient

        )
            ; 
      
        //.By default, the migrations assembly is the assembly containing the DbContext...
       // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();



        builder.Services.AddGraphQLServer()
            .AddQueryType<RootQuery>()
            .AddMutationType<RootMutation>()

            .RegisterDbContext<PortalDbContext>(DbContextKind.Pooled)
            .AddFiltering();

        builder.Services
          .AddTransient<ITransactionBusinessLogic, TransactionBusinessLogic>()
          .AddTransient<IRegistrationBusinessLogic, RegistrationBusinessLogic>();

        //builder.Services.AddScoped<IPortalRepositories, PortalRepositories>()
        //   .AddDbContext<PortalDbContext>(o => o
        //           .UseNpgsql(Configuration.GetConnectionString("PortalDB"))) ;

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapGraphQL();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}