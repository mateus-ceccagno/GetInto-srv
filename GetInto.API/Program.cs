using GetInto.API.Helpers;
using GetInto.Application;
using GetInto.Application.Contracts;
using GetInto.Domain.Identity;
using GetInto.Persistence;
using GetInto.Persistence.Context;
using GetInto.Persistence.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
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

builder.Services.AddIdentityCore<User>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
})
    .AddRoles<Role>()
    .AddRoleManager<RoleManager<Role>>()
    .AddSignInManager<SignInManager<User>>()
    .AddRoleValidator<RoleValidator<Role>>()
    .AddEntityFrameworkStores<GetIntoContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        object Configuration = null;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"])),
                            ValidateIssuer = false,
                            ValidateAudience = false
                        };
                    });

// Add services to the container.

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<IHumanService, HumanService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ISocialLinkService, SocialLinkService>();
builder.Services.AddScoped<IUtilImage, UtilImage>();

builder.Services.AddScoped<IGeralPersist, GeralPersist>();
builder.Services.AddScoped<IProjectPersist, ProjectPersist>();
builder.Services.AddScoped<IJobPersist, JobPersist>();
builder.Services.AddScoped<IHumanPersist, HumanPersist>();
builder.Services.AddScoped<IUserPersist, UserPersist>();
builder.Services.AddScoped<ISocialLinkPersist, SocialLinkPersist>();

builder.Services.AddCors();

builder.Services.AddSwaggerGen(options =>
    {
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "GetInto.API", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using Bearer.
                        Enter 'Bearer' [space] then enter your token.
                        Example: 'Bearer eyJhbGciO...'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();



var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
app.Run();
