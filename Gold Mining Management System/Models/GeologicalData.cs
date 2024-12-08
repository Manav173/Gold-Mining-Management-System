using System.ComponentModel.DataAnnotations;

namespace Gold_Mining_Management_System.Models
{
    public class GeologicalData
    {
        [Key]
        public int DataId { get; set; }

        [Required(ErrorMessage = "Site Id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Site Id must be a valid integer greater than 0.")]
        public int SiteId { get; set; }

        [Required(ErrorMessage = "Quartz in ppm is required to know the mineral composition.")]
        public int Quartz {  get; set; }

        [Required(ErrorMessage = "Limonite in ppm is required to know the mineral composition.")]
        public int Limonite { get; set; }

        [Required(ErrorMessage = "Pyrite in ppm is required to know the mineral composition.")]
        public int Pyrite { get; set; }

        [Required(ErrorMessage = "Arsenopyrite in ppm is required to know the mineral composition.")]
        public int Arsenopyrite { get; set; }

        [StringLength(255, ErrorMessage = "Mineral composition cannot be longer than 255 characters.")]
        public string? MineralComposition { get; set; } // For extra information about the mineral composition.

        [Required(ErrorMessage = "Sample date is required.")]
        public DateTime SampleDate { get; set; }

        public int? ReportId { get; set; }

        public Reports SurveyReport { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Geologist Id must be a valid integer greater than 0.")]
        public int? GeologistId { get; set; } 

        public Users? Geologist { get; set; }    

        public Sites Site { get; set; }
    }
}
