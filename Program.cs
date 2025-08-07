using EcommerceApi.Data;
using EcommerceApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<DbConnectionProvider>();
builder.Services.AddScoped<ProdutoRepository>();
builder.Services.AddSingleton<DbInitializer>();
builder.Services.AddScoped<DepartamentoRepository>();

var app = builder.Build();

app.UseMiddleware<EcommerceApi.Middleware.ExceptionMiddleware>();

app.UseCors("AllowAngular");

using (var scope = app.Services.CreateScope())
{
    var dbInit = scope.ServiceProvider.GetRequiredService<DbInitializer>();
    dbInit.Initialize();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();