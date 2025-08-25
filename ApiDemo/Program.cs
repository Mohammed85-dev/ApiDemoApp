/* urls:
 swaggerUI: https://localhost:7222/swagger/index.html
 openApi json: https://localhost:7222/openapi/v1.json
*/

using ApiDemo.Core.Tokens;
using ApiDemo.DataBase.CassandraConfiguration;
using ApiDemo.DataBase.Classes;
using ApiDemo.DataBase.Interfaces;
using ApiDemo.Mangers.Classes;
using ApiDemo.Mangers.Interfaces;
using ApiDemo.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();
await addSingletons(builder.Services, LoggerFactory.Create(log => log.AddConsole()).CreateLogger("StartUp"));

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

app.UseStaticFiles();

requiredServices(app.Services);

//For a mapping file if wanted
// MappingConfiguration.Global.Define<MyMappings>();

app.Run();


return;

void requiredServices(IServiceProvider services) {
    // _ = services.GetRequiredService<IUsersManger>();
}

async Task addSingletons(IServiceCollection services, ILogger startupLogger) {
    services.AddControllers();
    services.AddSwaggerGen(c => {
        c.OperationFilter<RequiredHeadersFromAttributesFilter>();
        c.DocumentFilter<RequiredHeadersFromAttributesFilter>();
    });

    await services.AddCassandraAsync(startupLogger);

    services.AddSingleton<IFileInfoDB, FileInfoDB>();
    services.AddSingleton<ITokenDataDB, TokenDataDB>();
    services.AddSingleton<IUsersDataDB, UsersDataDB>();
    services.AddSingleton<IAccountDataDB, AccountDataDB>();
    services.AddSingleton<ICoursesDataDB, CoursesDataDB>();
    services.AddSingleton<ICourseChaptersDB, CourseChaptersDB>();
    services.AddSingleton<IPlayListDataDB, PlayListDataDB>();
    services.AddSingleton<IFileServer, FileServer>();

    services.AddSingleton<IFileManger, FileManger>();
    services.AddSingleton<ITokenGenerator, TokenGenerator>();
    services.AddSingleton<ITokenAuthorizationManger, TokenAuthorizationManger>();
    services.AddSingleton<ICoursesManger, CoursesManger>();
    services.AddSingleton<IUsersManger, UsersManger>();
    services.AddSingleton<IAccountManger, AccountManger>();
}