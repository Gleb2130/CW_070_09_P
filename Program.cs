using CW_070_09_P;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseMiddleware<NumberToTextMiddleware>();


app.Run();