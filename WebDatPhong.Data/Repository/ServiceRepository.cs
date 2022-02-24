using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDatPhong.Data.Infrastructures;
using WebDatPhong.Data.IRepositories;
using WebDatPhong.Model.Models;

namespace WebDatPhong.Data.Repository
{
    public class ServiceRepository : GenericRepository<ServiceNews>, IServiceRepository
    {
        public ServiceRepository(WebDatPhongDbContext context) : base(context)
        {
        }

        public IEnumerable<ServiceNews> Get4Service(int top)
        {
            return (IEnumerable<ServiceNews>)context.ServiceNews.OrderBy(x=>x.Id).ToList().Take(top);
        }
    }
}
