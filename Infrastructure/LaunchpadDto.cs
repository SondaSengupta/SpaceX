using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class LaunchpadDto
    {
        public int PadId { get; set; }
        public string Id { get; set; }
        public string Full_Name { get; set; }
        public string Status { get; set; }
        public string Details { get; set; }

        public Launchpad ToDomain() {
            return Launchpad.Reconstitute(Id, Full_Name, Status, Details);
        }

    }
}
