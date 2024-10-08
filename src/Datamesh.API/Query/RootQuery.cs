using Microsoft.EntityFrameworkCore;
using PortalBackend.PortalEntities.EntitiesIsoPay;

namespace Datamesh.API.Query
{
   
    public class RootQuery
    {
        public DBDirectQuery DBDirectQuery { get; set; }

        public string isLive() => "server is live ";

        public RootQuery(PortalDbContext context)
        {
            DBDirectQuery = new DBDirectQuery();
        }

    }
}
 