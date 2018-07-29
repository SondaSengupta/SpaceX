using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class LaunchpadModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string Status { get; set; }

        public LaunchpadModel(string id, string name, string status) {
            Id = id;
            Name = name;
            Status = status;
        }
    }
}
