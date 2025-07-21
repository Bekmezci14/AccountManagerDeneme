using AccountManager.Api.Data;
using AccountManager.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var ConnString = builder.Configuration.GetConnectionString("AccountManager");
builder.Services.AddSqlite<AccountManagerContext>(ConnString);

var app = builder.Build();

app.MapAccountEndpoints();
app.MapCategoriesEndpoints();

await app.MigrateDbAsync();

app.Run();
