using System.ComponentModel.DataAnnotations.Schema;

namespace PatientInformation.Models
{
    [Table("Ncds", Schema = "DropDown")]
    public class Ncds
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
