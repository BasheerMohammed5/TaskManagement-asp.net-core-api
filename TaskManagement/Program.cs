using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(); // ÅÖÇİÉ ÏÚã áÜ MVC æ Razor Pages
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TaskManagement API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskManagement API V1");
        c.RoutePrefix = "swagger"; // ÊÃßÏ ãä Ãä Swagger ãÊÇÍ İí /swagger ÈÏáÇğ ãä ÇáÚäæÇä ÇáÑÆíÓí
    });
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Tasks}/{action=Index}/{id?}"); // ÅÚÏÇÏ ÇáÊæÌíå ÇáÇİÊÑÇÖí áÜ Tasks
    endpoints.MapControllers(); // ÊÃßÏ ãä ÅÖÇİÉ åĞå ÇáÓØÑ áÊæÌíå ÇáÜ API Controllers
});

app.Run();
