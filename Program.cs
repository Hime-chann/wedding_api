using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using HotChocolate;
using wedding_api.GraphQL;
using wedding_api.Services;
using wedding_api.Services.AdminServices;
using wedding_api.GraphQL.Mutations;
using wedding_api.GraphQL.Queries;
using wedding_api.GraphQL.Types;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// JWT Authentication Configuration
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// Add services
builder.Services.AddHttpContextAccessor(); // Only register HttpContextAccessor once

builder.Services.AddPooledDbContextFactory<WedDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")));

// Add DbContext as Scoped (correctly scoped for HTTP request)
builder.Services.AddDbContext<WedDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")),
    ServiceLifetime.Scoped);

// Register services as Scoped (ensure they are correctly scoped for DI)
builder.Services.AddScoped<PasswordHashService>();
builder.Services.AddScoped<ValidemailService>();
builder.Services.AddScoped<LoginEmailService>();
builder.Services.AddScoped<CreateWeddingProfileService>();
builder.Services.AddScoped<EditWeddingInfoService>();
builder.Services.AddScoped<GeneralMediaUploadingService>();
builder.Services.AddScoped<StoryReactionService>();
builder.Services.AddScoped<GenerateQrcodeService>();
builder.Services.AddScoped<CreateStoryService>();

// Configure GraphQL server
builder.Services.AddGraphQLServer()
    .AddAuthorization() // Add authorization to GraphQL
    .AddHttpRequestInterceptor((context, executor, builder, ct) =>
    {
        // This allows HTTP context (which has the authentication info) to be shared with GraphQL
        return ValueTask.CompletedTask;
    })
    .AddQueryType(q => q.Name("Query"))
        .AddTypeExtension<LoginQuery>()
        .AddTypeExtension<WeddingQuery>()
        .AddTypeExtension<GeneralMediaQuery>()
        .AddTypeExtension<StoryReactionQuery>()
        .AddTypeExtension<StoryQuery>()
        .AddTypeExtension<WeddingProfileQuery>()
    .AddMutationType(m => m.Name("Mutation"))
        .AddTypeExtension<SignupMutation>()
        .AddTypeExtension<CreateWeddingProfileMutation>()
        .AddTypeExtension<UpdateWeddingInfoMutation>()
        .AddTypeExtension<GeneralMediaUploadingMutation>()
        .AddTypeExtension<StoryReactionMutation>()
        .AddTypeExtension<CreateStoryMutation>()

        
    .AddType<GenMediaType>()
    .AddType<StoryReactionType>();

// CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Middleware
app.UseStaticFiles();
app.UseCors("AllowAll");

// Set up routing and GraphQL endpoint
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL(); // Maps the GraphQL endpoint
});

app.Run();