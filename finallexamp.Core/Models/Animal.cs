namespace finallexamp.Core.Models
{

    public class Animal
    {
        public int Id { get; set; }
        public string? ScientificName { get; set; }
        public string? ConservationStatus { get; set; }
        public string? GroupName { get; set; }
        public string? CountryCode { get; set; }
        public string? Name { get; set; }

        public override string ToString()
        {
            return $"{Id}" +
                   $"\n{Name}" +
                   $"\n{ScientificName}" +
                   $"\n{GroupName}" +
                   $"\n{ConservationStatus}" +
                   $"\n{CountryCode}";
        }
    }
}
