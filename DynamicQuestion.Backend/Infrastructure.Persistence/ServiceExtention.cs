using Application.Interface;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public static class ServiceExtention
    {
        public static void AddPersistenceLayer(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(option =>
                                                 option.UseSqlServer(configuration.GetConnectionString("DBConnect"),
                                                 builder => builder.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)
                                                 ));

            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepository<>));
            services.AddTransient<IAssessmentRespository, AssessmentRepository>();
            services.AddTransient<IAssessmentQueRepository, AssessmentQuestionRepository>();
        }
    }
}
