using giadinhthoxinh.Entities;
using giadinhthoxinh.IService;
using giadinhthoxinh.Models;
using giadinhthoxinh.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<GiadinhthoxinhContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("MyShop")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
builder.Services.AddScoped<IHomeService, HomeService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Middleware hiển thị tài liệu Swagger trong môi trường phát triển
    app.UseSwaggerUI(); // Middleware hiển thị giao diện người dùng Swagger
}

app.UseHttpsRedirection(); // Middleware chuyển hướng HTTP sang HTTPS

app.UseAuthorization(); // Middleware kiểm tra quyền truy cập

app.MapControllers(); // Middleware định tuyến các yêu cầu đến các controller

app.Run(); // Chạy ứng dụng
