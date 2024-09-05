using ENAE20240905Guia6.Endpoints;
using ENAE20240905Guia6.Models;
using ENAE20240905Guia6.Models.DAL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CRMContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Conn"))
);

builder.Services.AddScoped<ProductDAL>();

var app = builder.Build();

app.AddProductEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();