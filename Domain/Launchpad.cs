using System;

namespace Domain
{
    public class Launchpad
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string Status { get; set; }

        public string Details { get; set; }

        // More domain logic would be added here as launchpad becomes more complex. This class states the app's definition
        // of a launchpad regardless of api consumer ask or storage concerns.

        public static Launchpad Reconstitute(string id, string name, string status, string details) {
            return new Launchpad()
            {
                Id = id,
                Name = name,
                Status = status,
                Details = details
            };
        }


        //Example of more complexity
        public static Launchpad Create(string name, string status, string details)
        {

            //Domain validation can be added here to ensure launchpad is valid before creation

            return new Launchpad()
            {
                Name = name,
                Status = IsStatusValid(status) ? status : throw new DomainArgumentException("Status is not valid, you fool."),
                Details = details
            };
        }

        public static bool IsStatusValid(string status) {
            return Enum.IsDefined(typeof(LaunchpadStatus), status);
        }


    }
}
