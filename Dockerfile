# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the solution file
COPY SalesAppointmentsApi.sln .

# Copy each project directory individually
COPY src/SalesAppointments.Data/ src/SalesAppointments.Data/
COPY src/SalesAppointments.Api/ src/SalesAppointments.Api/
COPY tests/SalesAppointments.UnitTests/ tests/SalesAppointments.UnitTests/

# Restore dependencies for the solution
RUN dotnet restore SalesAppointmentsApi.sln

# Copy the remaining files and publish the application
COPY . .
RUN dotnet publish SalesAppointmentsApi.sln -c Release -o /out

# Use the runtime-only image for a smaller, production-ready image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .

# Expose the port your application is configured to use
EXPOSE 8080

# Set the entry point to run the application
ENTRYPOINT ["dotnet", "SalesAppointments.Api.dll"]
