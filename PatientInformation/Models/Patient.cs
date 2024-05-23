using PatientInformation.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientInformation.Models
{
    [Table("Patient", Schema ="dbo")]
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DiseaseId {  get; set; }
        public Epilepsy Epilepsy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
