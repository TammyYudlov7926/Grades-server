using GradeDO;
using Server_Project.Configuration;
using Server_Project.Models;
using Server_Project.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSingleton<IStudents, Students>();
builder.Services.AddTransient<PasswordManager>();
builder.Services.AddTransient<GradeManager>();
builder.Services.Configure<PercentagesForExercise>(
builder.Configuration.GetSection("PercentagesForExercise"));

    var app = builder.Build();
app.UseExceptionHandler("/error");

app.MapGet("/", () => "Hello World!");
app.MapControllers();
app.Run();
