using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Datamesh.Repository
{
    public class Program
    {
        public static void Main(string[] args)
        {   
        }
      
        public void ConfigureServices(IServiceCollection services)
            => services.AddDbContext<PortalDbContext>();
         
    }
}