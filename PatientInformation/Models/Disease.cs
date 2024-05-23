using System.ComponentModel.DataAnnotations.Schema;

namespace PatientInformation.Models
{
    [Table("Disease", Schema = "DropDown")]
    public class Disease
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
