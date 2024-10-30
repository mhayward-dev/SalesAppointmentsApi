using SalesAppointments;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureApi();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapControllers();

app.Run();