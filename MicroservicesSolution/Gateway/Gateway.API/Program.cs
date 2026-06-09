using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

// Add services to the container.
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gateway API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new string[] { }
        }
    });
});

// JWT Authentication Configuration
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]!);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"]
        // ClockSkew removed to allow for small timing differences
    };

    x.Events = new JwtBearerEvents
    {
        OnTokenValidated = async context =>
        {
            // Extract the raw token string
            if (context.SecurityToken is System.IdentityModel.Tokens.Jwt.JwtSecurityToken jwtToken)
            {
                var tokenString = jwtToken.RawData;
                
                // Ask the Identity API if this token is blacklisted
                using var httpClient = new System.Net.Http.HttpClient();
                
                // Assuming Identity API is running on this port (from Gateway appsettings.json)
                var identityUrl = "http://localhost:49693/auth/check-blacklist?token=" + tokenString;
                
                try 
                {
                    var response = await httpClient.GetAsync(identityUrl);
                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        // The Identity API says it's in the garbage bin!
                        context.Fail("This token has been blacklisted (Logged out).");
                    }
                }
                catch (Exception)
                {
                    // Ignore connection errors if Identity API is temporarily down during test
                }
            }
        },
        OnChallenge = context =>
        {
            context.HandleResponse();
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";
            
            // Get the specific error message if it exists
            var errorMessage = context.AuthenticateFailure?.Message ?? "Invalid or missing token.";
            
            var result = System.Text.Json.JsonSerializer.Serialize(new 
            { 
                message = errorMessage,
                suggestion = "Please log in again and ensure you copy the token correctly without quotes."
            });
            return context.Response.WriteAsync(result);
        }
    };
});

// Define Authorization Policies for YARP
builder.Services.AddAuthorization(options =>
{
    // Policy 1: General Authentication (Requires any valid JWT token)
    options.AddPolicy("Authenticated", policy => 
    {
        policy.RequireAuthenticatedUser();
        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
    });

    // Policy 2: Role-Based Authentication (Requires a JWT token AND the "Citizen" role)
    options.AddPolicy("CitizenOnly", policy => 
    {
        policy.RequireAuthenticatedUser();
        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
        // This line explicitly checks the token for 'role: Citizen'
        policy.RequireRole("Citizen");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Gateway API");
        options.SwaggerEndpoint("/api/identity/swagger/v1/swagger.json", "Identity Service");
        options.SwaggerEndpoint("/api/user/swagger/v1/swagger.json", "User Service");
        options.SwaggerEndpoint("/api/order/swagger/v1/swagger.json", "Order Service");
        options.SwaggerEndpoint("/api/product/swagger/v1/swagger.json", "Product Service");
        options.RoutePrefix = "swagger";
    });
}

app.UseAuthentication();
app.UseAuthorization();

app.MapReverseProxy();

app.Run();
