// ** library **
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text;
// ** architecture **
using locy_api.Data;
using locy_api.Models.Response;
using locy_api.Interfaces;
using locy_api.Services;
using locy_api.Helpers;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Config server
var origin = builder.Configuration["Origin"] ?? "";
var sqlConnectionString = builder.Configuration.GetConnectionString("LocyApiConnectionString");
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "";
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? "";
var jwtKey = builder.Configuration["Jwt:Key"] ?? "";

// Add services to the container.
// Add cors
builder.Services.AddCors(option =>
{
    option.AddPolicy(name: MyAllowSpecificOrigins, policy =>
    {
        policy.WithOrigins(origin).AllowAnyHeader().AllowAnyMethod()
                                  .AllowCredentials();
    });
});

// Add controller
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add db context
builder.Services.AddDbContext<BestCareDbContext>(option => option.UseSqlServer(sqlConnectionString));

// Add authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = jwtIssuer,
                        ValidAudience = jwtAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                    };
                });

// Add service for use
builder.Services.AddScoped<IAuthHelper, AuthHelpers>();
builder.Services.AddScoped<IEmployeeHelper, EmployeeHelpers>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IPortService, PortService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerInfoService, CustomerInfoService>();

// Add authorization role
builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("ManageEmployee", policy => policy.RequireAssertion(context =>
        context.User.HasClaim(claim => claim.Type == "Permission" && (claim.Value.Contains("1048576") || claim.Value.Contains("5000")))
    ));
    option.AddPolicy("ManageCategory", policy => policy.RequireAssertion(context =>
        context.User.HasClaim(claim => claim.Type == "Permission" && (claim.Value.Contains("1048576") || claim.Value.Contains("6000")))
    ));
    option.AddPolicy("ManageCustomer", policy => policy.RequireAssertion(context =>
        context.User.HasClaim(claim => claim.Type == "Permission" && (claim.Value.Contains("1048576") || claim.Value.Contains("7000") || claim.Value.Contains("7020")))
    ));
    option.AddPolicy("DeliveryCustomer", policy => policy.RequireAssertion(context =>
        context.User.HasClaim(claim => claim.Type == "Permission" && (claim.Value.Contains("1048576") || claim.Value.Contains("7000") || claim.Value.Contains("7080")))
    ));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

// Handle event when push status error code
app.Use(async (context, next) =>
{
    await next();

    if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden || context.Response.StatusCode == 405)
    {
        var response = new Response()
        {
            Status = false,
            Message = "Bạn không có quyền thực hiện chức năng này!",
            Data = null,
        };

        await context.Response.WriteAsJsonAsync(response);
    }

    if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
    {
        var response = new Response()
        {
            Status = false,
            Message = "Bạn cần đăng nhập để thực hiện chức năng này!",
            Data = null,
        };

        await context.Response.WriteAsJsonAsync(response);
    }
});

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
