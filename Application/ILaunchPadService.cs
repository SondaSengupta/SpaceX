using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface ILaunchpadService
    {
        Task<IEnumerable<Launchpad>> Get(string status, string nameContains);
        Task<Launchpad> GetById(string id);
    }
}
