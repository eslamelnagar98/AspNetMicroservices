using Catalog.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
var builder = WebApplicationBuilderFactory.Create(args);
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthorization();
app.MapControllers();
app.MapGet("/", Run);
app.Run();

void Run(HttpRequest request, IProductRepository productRepository)
{
    var x = productRepository.GetProductByCategory("");
    Console.WriteLine(x);
}
