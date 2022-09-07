using System.ComponentModel.DataAnnotations;

namespace InspectionAPI.Models
{
    public class Inspection
    {
        public int id { get; set; }
     
        [StringLength(20)]
        public string  status { get; set; } = string.Empty;
    
        [StringLength(200)]
        public string comments { get; set; }=string.Empty;

        public int inspectionTypeId { get; set; }

        public InspectionType? inspectionType{ get; set; }
    }
}
