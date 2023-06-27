using DoctorWho.Db;
using DoctorWho.Db.Repositories;
using DoctorWho.web.Mappers;
using DoctorWho.web.Validators;
using DoctorWho.Web.Validators;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DoctorWhoCoreContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<DoctorRepository>();
builder.Services.AddScoped<AuthorRepository>();
builder.Services.AddScoped<EnemyRepository>();
builder.Services.AddScoped<CompanionRepository>();
builder.Services.AddScoped<EpisodeRepository>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

builder.Services.AddTransient<DoctorDtoValidator>();
builder.Services.AddTransient<EpisodeDtoValidator>();
builder.Services.AddTransient<EnemyDTOValidator>();
builder.Services.AddTransient<AuthorDTOValidator>();

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System
            .Text
            .Json
            .Serialization
            .ReferenceHandler
            .Preserve;
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
