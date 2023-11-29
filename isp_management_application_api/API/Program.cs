using API.Extensions;
using Application.Extensions;
using Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services
    .AddApi(builder.Configuration)
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddJwtAuthentication(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{    
}

app.UseCors(options => options
      .AllowAnyOrigin()
      .AllowAnyMethod()
      .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGet("/", async context => await context.Response.WriteAsync("ISP Management Application is running."));

app.Run();
