using System.ComponentModel.DataAnnotations.Schema;

namespace PatientInformation.Models
{
    [Table("NcdDetails", Schema = "dbo")]
    public class NcdDetails
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int NcdId { get; set; }
    }
}
