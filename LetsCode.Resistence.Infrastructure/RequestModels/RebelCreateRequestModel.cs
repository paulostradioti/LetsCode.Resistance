namespace LetsCode.Resistance.Infrastructure.RequestModels
{
    public class RebelCreateRequestModel
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string BaseName { get; set; }
    }
}
