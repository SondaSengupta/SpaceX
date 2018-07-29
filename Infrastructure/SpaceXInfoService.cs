using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using RestSharp;

namespace Infrastructure
{
    public class SpaceXInfoService : ILaunchpadRepository
    {
        //When switching to a database instead of an api, as long as it meets the 
        //criteria of the ILaunchpadRepository, this would be the only project file that would need modification

        private readonly IRestClient _restClient;

        public SpaceXInfoService()
        {
            _restClient = new RestClient("https://api.spacexdata.com/v2/");
        }

        public async Task<IEnumerable<Launchpad>> Get()
        {

            var request = new RestRequest("launchpads", Method.GET);

            var response = await _restClient.ExecuteTaskAsync<IEnumerable<LaunchpadDto>>(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(response.ErrorMessage);
            }

            var domainModels = response.Data.Select(x => x.ToDomain());

            return domainModels;
        }

        public async Task<Launchpad> GetById(string id)
        {

            var request = new RestRequest($"launchpads/{id}", Method.GET);

            var response = await _restClient.ExecuteTaskAsync<LaunchpadDto>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return null;
            }

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new ApiCallFailedException(response);
            }

            var domainModel = response.Data.ToDomain();

            return domainModel;
        }
    }
}
