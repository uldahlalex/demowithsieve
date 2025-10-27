using demowithsieve;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApiDocument();

var postgresContainer = new PostgreSqlBuilder().Build();
postgresContainer.StartAsync().GetAwaiter().GetResult();

builder.Services.AddDbContext<MyDbContext>((provider, optionsBuilder) =>
{
    optionsBuilder.UseNpgsql(postgresContainer.GetConnectionString());
});
builder.Services.AddScoped<IPostService, PostService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();
    dbContext.Database.EnsureCreated();
    dbContext.Posts.Add(new Post()
    {
        CommentCount = 10,
        LikeCount = 7,
        Id = 2,
        Title = "bla bla my vacation photo",
        DateCreated = DateTime.UtcNow
    });
    dbContext.Posts.Add(new Post()
    {
        CommentCount = 10000,
        LikeCount = 70000,
        Id = 1,
        Title = "chili eating",
        DateCreated = DateTime.UtcNow
    });
    dbContext.SaveChanges();
}

app.MapControllers();
app.UseOpenApi();
app.UseSwaggerUi();

app.Run();
