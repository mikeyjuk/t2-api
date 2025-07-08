using FastEndpoints;
using FastEndpoints.Swagger;
using T2LifestyleChecker.Service;

var bld = WebApplication.CreateBuilder();
bld.Services.AddFastEndpoints().SwaggerDocument();
bld.Services.AddHttpClient();
bld.Services.AddScoped<IGenerateRiskService, GenerateRiskService>();
bld.Services.AddScoped<IPatientDetailsService, PatientDetailsService>();

// Add CORS policy
bld.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("*") // Typically you would make this more specific to your front end but for ease of running set this to any
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = bld.Build();

app.UseCors("AllowSpecificOrigin");

app.UseFastEndpoints().UseSwaggerGen();

app.Run();