using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Infrastructure;

namespace Application
{
    public class LaunchpadService : ILaunchpadService
    {

        private ILaunchpadRepository _launchPadRepository;

        public LaunchpadService(ILaunchpadRepository launchrepo) {
            _launchPadRepository = launchrepo ?? throw new ArgumentNullException("LaunchpadRepository is Required");
        }

        public async Task<IEnumerable<Launchpad>> Get(string status, string nameContains)
        {
            //Simple implementation following YAGNI. As filter becomes more complex, maybe a filter that dynamically builds predicates using expressions
            //If capturing domain logic matching then move check to domain and perhaps use specification pattern.

            var results = await _launchPadRepository.Get();

            if (!String.IsNullOrWhiteSpace(status)) {
                results = results.Where(x => x.Status == status);
            }

            if (!String.IsNullOrWhiteSpace(nameContains)) {
                results = results.Where(x => x.Name.Contains(nameContains));
            }

            return results;
        }

        public async Task<Launchpad> GetById(string id) {
            return await _launchPadRepository.GetById(id);
        }
    }
}
