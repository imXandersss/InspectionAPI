using System.ComponentModel.DataAnnotations;

namespace InspectionAPI.Models
{
    public class Status
    {
        public int id { get; set; }


        [StringLength(20)]
        public string statusOptions{ get; set; }=string.Empty;
    }
}
