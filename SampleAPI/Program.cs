using Microsoft.OpenApi.Models;
using SampleAPI.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//SWAGGER CONFIGURATION
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Sample API",
        Version = "v1",
        Description = "Aplikacja testowa stworzona na potrzeby rekrutacji",
        Contact = new OpenApiContact
        {
            Name = "Jakub Sarecki",
            Email = "jakub.sarecki.developer@gmail.com",
        }
    });
});

builder.Services.AddSwaggerGen();

//LAYERS REGISTRATION
builder.Services.RegisterApi(builder.Configuration);

var app = builder.Build();

app.UseRouting();

//DEVELOPMENT SETTINGS
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//CONTROLERS MAPPING
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapGet("/", ctx => ctx.Response.WriteAsync("Sample API"));
});

app.Run();
