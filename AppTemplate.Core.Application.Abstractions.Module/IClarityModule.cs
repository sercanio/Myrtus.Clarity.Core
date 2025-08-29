using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AppTemplate.Core.Application.Abstractions.Module;

public interface IClarityModule
{
    void ConfigureServices(IServiceCollection services, IConfiguration configuration);
    void Configure(IApplicationBuilder app);
}
