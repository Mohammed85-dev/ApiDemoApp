
/* urls:
 swaggerUI: https://localhost:7222/swagger/index.html
 openApi json: https://localhost:7222/openapi/v1.json 
*/

using ApiDemo.Core.Tokens;
using ApiDemo.DataBase.Classes;
using ApiDemo.DataBase.Classes.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

ITokenGenerator tokenGenerator = new TokenGenerator();
builder.Services.AddSingleton<ITokenGenerator>(tokenGenerator);
builder.Services.AddSingleton<IUsersDataManger>(new UsersDataManger(tokenGenerator));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.MapOpenApi();
    
    app.UseSwaggerUI(options => {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}

app.MapGet("/", () => "Hello World!");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();