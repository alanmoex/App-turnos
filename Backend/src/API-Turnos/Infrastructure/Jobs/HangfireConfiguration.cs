using Application.Interfaces;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infrastructure.Jobs
{
    public static class HangfireConfiguration
    {
        public static void ConfigureHangfireJobs(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var recurringJobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();
            var appointmentService = scope.ServiceProvider.GetRequiredService<IAppointmentService>();

            // Configurar el trabajo recurrente para ejecutar diariamente a las 3 AM
            recurringJobManager.AddOrUpdate(
                "VerificarYCrearTurnosAutomaticos",
                () => appointmentService.CreateAutomaticAppointments(),
                Cron.Daily(3, 0),  // Ejecutar diariamente a las 3 AM
                new RecurringJobOptions { TimeZone = TimeZoneInfo.Utc }  // Zona horaria UTC
            );
        }
    }
}
