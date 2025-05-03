using Hangfire;
using InventoryManagmentSystem.Business.Interfaces;
using InventoryManagmentSystem.Business.Services;
using InventoryManagmentSystem.DataAccess.Data;
using InventoryManagmentSystem.DataAccess.UnitOfWork;
using InventoryManagmentSystem.Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductServices,ProductServices>();
builder.Services.AddScoped<IReportServices, ReportServices>();
builder.Services.AddScoped<ITransactionServices, TransactionServices>();
builder.Services.AddScoped<IAccoutSevices, AccountServices>();

//----------------------------------------------------------Hangfire------------------------------------------------------------------------/
builder.Services.AddHangfire(option => option.UseSqlServerStorage(builder.Configuration["ConnectionStrings:cs"]));
builder.Services.AddHangfireServer();



//----------------------------------------------------------Serilog------------------------------------------------------------------------/
Serilog.Log.Logger = new LoggerConfiguration()
    .WriteTo.Seq(builder.Configuration["Seq:SeqUrl"])
    .WriteTo.MSSqlServer(connectionString: builder.Configuration["ConnectionStrings:cs"],
   // restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Warning,
    sinkOptions: new MSSqlServerSinkOptions { TableName = "Logs " , AutoCreateSqlTable =true }

).CreateLogger();

builder.Host.UseSerilog();
//--------------------------------------------------------AddDbContext---------------------------------------------------------------------------/


builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("cs"))
    );

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

//---------------------------------------------------------Add Memory Cache---------------------------------------------------------------------------/

builder.Services.AddMemoryCache();
//----------------------------------------------------------------------------------------------------------------------------------------/

//setting Auth MiddleWare check using jwt
builder.Services.AddAuthentication(
    option =>
    {
        option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }
    )
    .AddJwtBearer(options =>
    {
        options.SaveToken=true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JWT:Iss"],

            ValidateAudience = true,
            ValidAudience= builder.Configuration["JWT:Aud"],

            IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
        };
    });
//----------------------------------------------------------------------------------------------------------------------------------------/

var app = builder.Build();


//----------------------------------------------------------------------------------------------------------------------------------------/
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//-------------------------------------------------Hangfire---------------------------------------------------------------------------------/

app.UseHangfireDashboard("/dashboard");

RecurringJob.AddOrUpdate<LowStockCheckerJobServices>(
    "CheckLowStockDaily",
    job => job.ExecuteAsync(),
    Cron.Daily
);
//----------------------------------------------------------------------------------------------------------------------------------------/

app.UseAuthorization();

app.MapControllers();

app.Run();
