using Microsoft.EntityFrameworkCore; 
using PollsApi.Data; 
 
var builder = WebApplication.CreateBuilder(args); 
builder.Services.AddControllers(); 
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen(); 
builder.Services.AddCors(options => options.AddPolicy("AllowAll", policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod())); 
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Host=postgres;Port=5432;Database=pollsdb;Username=postgres;Password=postgres"; 
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString)); 
var app = builder.Build(); 
using (var scope = app.Services.CreateScope()) { var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>(); dbContext.Database.EnsureCreated(); } 
if (app.Environment.IsDevelopment()) { app.UseSwagger(); app.UseSwaggerUI(); } 
app.UseCors("AllowAll"); app.UseAuthorization(); app.MapControllers(); app.Run(); 
