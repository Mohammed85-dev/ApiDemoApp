/* urls:
 swaggerUI: https://localhost:7222/swagger/index.html
 openApi json: https://localhost:7222/openapi/v1.json
*/

using ApiDemo.Core.Tokens;
using ApiDemo.DataBase.Classes;
using ApiDemo.DataBase.Interfaces;
using ApiDemo.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(c => {
    c.OperationFilter<RequiredHeadersFromAttributesFilter>();
    c.DocumentFilter<RequiredHeadersFromAttributesFilter>();
});

ITokenGenerator tokenGenerator = new TokenGenerator();
builder.Services.AddSingleton<ITokenGenerator>(tokenGenerator);
builder.Services.AddSingleton<IUsersDataManger>(new UsersDataManger(tokenGenerator));
builder.Services.AddSingleton<IAuthDataManger>(new UsersDataManger(tokenGenerator));

builder.Services.AddCors(options => {
    options.AddPolicy("AllowLocalhost",
        policy => policy
            .WithOrigins(
                "http://localhost:5500",
                "https://127.0.0.1:7222",
                "null") // null is for file:// URLs
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

var app = builder.Build();

app.UseCors("AllowLocalhost");

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment()) {
// app.MapOpenApi();
app.UseSwagger();

app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"); });
// }

app.MapGet("/", () => "Hello World!");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();