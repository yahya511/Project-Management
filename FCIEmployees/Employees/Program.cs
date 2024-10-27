//using Application.IServices;
using Application.Services;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Application.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// Add DbContext for EmployeesDbContext
builder.Services.AddDbContext<EmployeesDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeesDatabase"), 
        b => b.MigrationsAssembly("Infrastructure"))); 

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); // إضافة Unit of Work
builder.Services.AddScoped<ITownRepository, TownRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// Add Mediator
builder.Services.AddMediatR(typeof(GetAllAddressesRequest).Assembly);//Start Point --> all handlers in the assembly
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
var mediator = builder.Services.BuildServiceProvider().GetService<IMediator>();
if (mediator == null)
{
    Console.WriteLine("Mediator is not registered.");
}

// إضافة خدمات gRPC
builder.Services.AddGrpc();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IDepartmentGrpcService, DepartmentGrpcService>();

builder.WebHost.UseKestrel(options =>
{
    // السماح فقط  لـ HTTP/2
   // options.ConfigureEndpointDefaults(o => o.Protocols = HttpProtocols.Http2);
    // السماح لـ HTTP/1.1 و HTTP/2
    options.ConfigureEndpointDefaults(o => o.Protocols = HttpProtocols.Http1AndHttp2);
    options.ListenLocalhost(7157, listenOptions =>
    {
        listenOptions.UseHttps(); // تأكد من أن HTTPS مفعّل هنا
    });

    options.ListenLocalhost(5131, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2; // gRPC على HTTP/2
    });

});



// Configure the HTTP request pipeline.
var app = builder.Build(); // تأكد من أنك قد أضفت هذا السطر قبل استخدام 'app'

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers(); // إذا كنت تستخدم API controllers

// تعريف نقطة نهاية gRPC
app.MapGrpcService<EmployeeService>();
app.MapGrpcService<DepartmentGrpcService>();

app.MapGet("/grpc-status", () => "gRPC server is running");

// إعادة توجيه الصفحة الرئيسية إلى Swagger
app.MapGet("/", () => Results.Redirect("/swagger/index.html"));

app.Run();
