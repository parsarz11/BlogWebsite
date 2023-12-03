using Microsoft.EntityFrameworkCore;
using WeblogApp.BlogData.Context;
using WeblogApp.Data.Entities;
using WeblogApp.Data.Repositories.Blog;
using WeblogApp.Data.Repositories.category;
using WeblogApp.Data.Repositories.Category;
using WeblogApp.Data.Repositories.Photo;
using WeblogApp.MiddleWares;
using WeblogApp.MiddleWares.ExceptionHandlerMiddleWare;
using WeblogApp.Services.Blog;
using WeblogApp.Services.category;
using WeblogApp.Services.Photo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IBlogsData,BlogRepository>();
builder.Services.AddScoped<ICategoriesData, CategoryRepository>();
builder.Services.AddScoped<IPhotoFile,PhotoRepository>();

builder.Services.AddScoped<IBlogServices, BlogServices>();
builder.Services.AddScoped<IPhotoServices, PhotoServices>();
builder.Services.AddScoped<ICategoryServices, CategoryServices>();

builder.WebHost.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    //logging.AddEventLog();
    logging.AddConsole();
    //logging.AddDebug();

});


builder.Services.AddDbContext<BlogDatabaseContext>(config =>
{
    config.UseSqlServer(builder.Configuration.GetConnectionString("default"));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
