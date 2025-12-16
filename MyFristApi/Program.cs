// 👇👇👇 新增这段代码：注册数据库服务 👇👇👇
// 1. 引入命名空间（如果报错，鼠标悬停选 Quick Fix -> using ...Data; 和 ...EntityFrameworkCore;）
using MyFirstApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer(); // ⭐必需
builder.Services.AddSwaggerGen();           // ⭐必需
builder.Services.AddOpenApi(); // 这是 .NET 8 的 OpenAPI JSON，可留可删

// 1.添加这一行：注册控制器服务
builder.Services.AddControllers();

// 2. 告诉系统：我们要用 DataContext
// 3. 告诉系统：我们要用 SQLite，文件名叫 "bank.db"
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlite("Data Source=bank.db");
});
// 👆👆👆 新增结束 👆👆👆

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();     // ⭐启用 Swagger JSON
    app.UseSwaggerUI();   // ⭐启用 Swagger UI
    app.MapOpenApi();     // .NET 8 默认的 openapi endpoints
}

app.UseHttpsRedirection();

// 2.添加这一行：启动控制器映射
app.MapControllers();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
