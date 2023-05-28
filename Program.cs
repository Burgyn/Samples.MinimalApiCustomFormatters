using Samples.MinimalApiCustomFormatters;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/products", () => Results.Extensions.NJson(new Product[]
{
    new("Keyboard", "Mechanical keyboard", 100),
    new("Mouse", "Gaming mouse", 50),
    new("Monitor", "4k monitor", 500)
}));

app.Run();

public record Product(string Name, string Description, decimal Price);