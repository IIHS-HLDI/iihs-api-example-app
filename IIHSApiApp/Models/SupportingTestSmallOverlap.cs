namespace IIHSApiApp.Models
{
    public class SupportingTestSmallOverlap
    {
        public string id { get; set; }
        public bool driverInjuryDataApplies { get; set; }
        public bool passengerInjuryDataApplies { get; set; }
        public bool intrusionDataApplies { get; set; }
        public string testedVin { get; set; }
        public string testedVehicleText { get; set; }
        public string lastModified { get; set; }
    }
}