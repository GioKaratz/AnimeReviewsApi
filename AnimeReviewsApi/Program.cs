using AnimeReviewsData.Data;
using Microsoft.EntityFrameworkCore;
using AnimeReviewsApi.Endpoints;
using AnimeReviewsApi.Configuration;
using AnimeReviewsData.Contracts;
using AnimeReviewsData.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(MapperConfig));

// Add services to the container.

var conn = builder.Configuration.GetConnectionString("AnimeReviewsDbConnection");

builder.Services.AddDbContext<AnimeReviewDbContext>(options =>
{
    options.UseSqlServer(conn);
});

builder.Services.AddScoped<IAnimeRepository, AnimeRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IReviewerRepository, ReviewerRepository>();

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

