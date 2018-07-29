using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public interface ILaunchpadRepository
    {
        Task<IEnumerable<Launchpad>> Get();
        Task<Launchpad> GetById(string id);
    }
}
