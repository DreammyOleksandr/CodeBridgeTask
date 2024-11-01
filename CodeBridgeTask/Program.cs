using CodeBridgeTask.BusinessLogic.Managers.DogManager;
using CodeBridgeTask.DataAccess;
using CodeBridgeTask.DataAccess.Repositories.DogRepository;
using CodeBridgeTask.MiddleWare.RequestsHandle;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDbContext<ApplicationDbContext>(options => options
        .UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddSingleton<RequestsHandler>(_ => new RequestsHandler(
    builder.Services.BuildServiceProvider().GetRequiredService<RequestDelegate>()
));

builder.Services.AddScoped<IDogManager, DogManager>();
builder.Services.AddScoped<IDogRepository, DogRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<RequestsHandler>();

app.MapControllers();

app.Run();