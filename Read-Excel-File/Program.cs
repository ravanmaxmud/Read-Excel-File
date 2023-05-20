//var builder = WebApplication.CreateBuilder(args);
//var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

//app.Run();

using Read_Excel_File.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

//Register services (IoC container)
builder.Services.ConfigureServices(builder.Configuration);

//setup
var app = builder.Build();

//Configuration of middleware pipeline
app.ConfigureMiddlewarePipeline();

//setup
app.Run();
