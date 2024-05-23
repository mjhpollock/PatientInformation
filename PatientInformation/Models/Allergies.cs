using System.ComponentModel.DataAnnotations.Schema;

namespace PatientInformation.Models
{
    [Table("Allergies", Schema = "DropDown")]
    public class Allergies
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
