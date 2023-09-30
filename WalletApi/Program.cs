using AutoMapper;
using Microsoft.AspNetCore.Builder;
using WalletApi.DataAccess.EF;
using WalletApi.Mappings;
using WalletApi.Model.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<WalletContext>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddSingleton<ICardPointsService, CardPointsService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddSingleton<IMapper, Mapper>(sp => new Mapper(new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new ModelsEntitiesProfile());
    cfg.AddProfile(new ModelsDTOsProfile());
})));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else 
{
    app.UseExceptionHandler(app =>
    {
        app.Run(async context =>
        {
            context.Response.StatusCode = 500;
        });
    });
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
