using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ServiceTestTask.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

string id = string.Format("{0}.db", Guid.NewGuid().ToString());
var stringBuilder = new SqliteConnectionStringBuilder()
{
    DataSource = id,
    Mode = SqliteOpenMode.Memory,
    Cache = SqliteCacheMode.Shared
};

var connection = new SqliteConnection(stringBuilder.ConnectionString);
connection.Open();
connection.EnableExtensions(true);
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlite(connection));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ApplicationContext>();
    context.Database.EnsureCreated();

    using var command = connection.CreateCommand();
    command.CommandText = "PRAGMA encoding=utf16;";
    command.ExecuteNonQuery();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

connection.Dispose();
