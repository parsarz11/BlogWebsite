using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WeblogApp.BlogData.Context;
using WeblogApp.Data.Context;
using WeblogApp.Data.Entities;
using WeblogApp.Data.Repositories.Blog;
using WeblogApp.Data.Repositories.category;
using WeblogApp.Data.Repositories.Category;
using WeblogApp.Data.Repositories.Photo;
using WeblogApp.MiddleWares;
using WeblogApp.MiddleWares.ExceptionHandlerMiddleWare;
using WeblogApp.Services.Blog;
using WeblogApp.Services.category;
using WeblogApp.Services.JwtServices;
using WeblogApp.Services.mailService;
using WeblogApp.Services.Photo;
using WeblogApp.Services.userAccounting;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddScoped<IBlogsData,BlogRepository>();
builder.Services.AddScoped<ICategoriesData, CategoryRepository>();
builder.Services.AddScoped<IPhotoFile,PhotoRepository>();

builder.Services.AddScoped<IBlogServices, BlogServices>();
builder.Services.AddScoped<IPhotoServices, PhotoServices>();
builder.Services.AddScoped<ICategoryServices, CategoryServices>();

builder.Services.AddScoped<IUserAccounting, Accounting>();

builder.Services.AddScoped<mailServices>();


builder.Services.AddDbContext<BlogDatabaseContext>(config =>
{
    config.UseSqlServer(builder.Configuration.GetConnectionString("default"));
});

builder.Services.AddDbContext<IdentityDatabaseContext>(config =>
{
    config.UseSqlServer(builder.Configuration.GetConnectionString("Identity"));
});

builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<IdentityDatabaseContext>().AddDefaultTokenProviders();
//////////
var bindJwtSettings = new JwtSettings();

builder.Configuration.Bind("JsonWebTokenKeys", bindJwtSettings);

builder.Services.AddSingleton(bindJwtSettings);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateIssuerSigningKey = bindJwtSettings.ValidateIssuerSigningKey,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(bindJwtSettings.IssuerSigningKey)),
        ValidateIssuer = bindJwtSettings.ValidateIssuer,
        ValidIssuer = bindJwtSettings.ValidIssuer,
        ValidateAudience = bindJwtSettings.ValidateAudience,
        ValidAudience = bindJwtSettings.ValidAudience,
        //RequireExpirationTime = bindJwtSettings.RequireExpirationTime,
        //ValidateLifetime = bindJwtSettings.RequireExpirationTime,
        //ClockSkew = TimeSpan.FromDays(1),
    };
}).AddGoogle(option =>
{
    option.ClientId = "175659099233-ehnv6u5aml7hbam2agibaitsll1usvf2.apps.googleusercontent.com ";
    option.ClientSecret = "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";
});







builder.Services.AddAuthorization();





builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                            Id = "Bearer"
                    }
                },
                new string[] {}
        }
    });
});

var app = builder.Build();


// Configure the HTTP request pipeline.
app.UseCustomExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(x => x
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true) // allow any origin
                                                        //.WithOrigins("https://localhost:44351")); // Allow only this origin can also have multiple origins separated with comma
                    .AllowCredentials()); // allow credentials
//app.Use(cors({
//origin: ['http://localhost:3000'],
//    credentials: true,
//    sameSite: 'none'
//}));



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
