namespace Gold_Mining_Management_System.DTO
{
    public class SafetyIncidentCounts
    {
        public int TotalSafetyIncidents { get; set; }
        public int ResolvedIncidents { get; set; }
        public int PendingIncidents { get; set; }
        public int LowSeverity { get; set; }
        public int MediumSeverity { get; set; }
        public int HighSeverity { get; set; }
    }
}
