using AnimeReviewsData.Data;
using Microsoft.EntityFrameworkCore;
using AnimeReviewsApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var conn = builder.Configuration.GetConnectionString("AnimeReviewsDbConnection");

builder.Services.AddDbContext<AnimeReviewDbContext>(options =>
{
    options.UseSqlServer(conn);
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy => policy.AllowAnyHeader()
    .AllowAnyOrigin().AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseCors("AllowAll");

app.MapAnimeEndpoints();

app.MapReviewEndpoints();

app.MapReviewerEndpoints();

app.MapCategoryEndpoints();

app.MapAuthorEndpoints();

app.Run();

