using Microsoft.Extensions.Diagnostics.HealthChecks;
using MailSystem.Core.Resources;
using System.Threading;
using System.Threading.Tasks;

namespace MailSystem.API
{
    /// <summary>
    /// Class responsible to check the application health
    /// </summary>
    public class CustomHealthCheck : IHealthCheck
    {
        private readonly IResourceLocalizer _resource;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="resource">Object that contains all static messages</param>
        public CustomHealthCheck(IResourceLocalizer resource)
        {
            _resource = resource;
        }

        //TO DO: REALLY CHECK IF APP IS HEALTHY

        /// <summary>
        /// Method to handle the health check process
        /// </summary>
        /// <param name="context">Health context</param>
        /// <param name="cancellationToken">Thread cancellation token</param>
        /// <returns></returns>
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var healthCheckResult = true;

            if (healthCheckResult)
            {
                return Task.FromResult(
                    HealthCheckResult.Healthy(_resource.GetString("HEALTHY_STATUS")));
            }

            return Task.FromResult(
                HealthCheckResult.Unhealthy(_resource.GetString("UNHEALTHY_STATUS")));
        }
    }
}
