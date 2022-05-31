using AutoMapper;
using GetInto.Application;
using GetInto.Application.Contracts;
using GetInto.Persistence;
using GetInto.Persistence.Context;
using GetInto.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<GetIntoContext>(
        c => c.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
    );

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
    )
    .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling =
        Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );

// Add services to the container.

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<IHumanService, HumanService>();
builder.Services.AddScoped<ISocialLinkService, SocialLinkService>();

builder.Services.AddScoped<IGeralPersist, GeralPersist>();
builder.Services.AddScoped<IProjectPersist, ProjectPersist>();
builder.Services.AddScoped<IJobPersist, JobPersist>();
builder.Services.AddScoped<IHumanPersist, HumanPersist>();
builder.Services.AddScoped<ISocialLinkPersist, SocialLinkPersist>();

builder.Services.AddCors();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
app.Run();

