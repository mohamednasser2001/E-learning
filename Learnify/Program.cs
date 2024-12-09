using Data;
using Data.IRepository;
using Data.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Models;
using Stripe;
using Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<ApplicationDbContext>(
            options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefualtConection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
      .AddEntityFrameworkStores<ApplicationDbContext>();
// Assuming AssignmentRepository is the implementation of IAssignmentRepository
builder.Services.AddScoped<IAssignmentRepository, AssignmentRepository>();


builder.Services.AddSwaggerGen();


var app = builder.Build();

//builder.Services.Configure<StripeSettings>(builder.Configuration["Stripe"]);
StripeConfiguration.ApiKey = "sk_test_51QRubhGC8zICyOJupUvJyvZnBqR7QOJZZSAirnxELjIxJdi5rioxjk2n3XIZLom1CbHRkMUDRKh0vqtmQqK7CzLB00r0JowPQU";

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
