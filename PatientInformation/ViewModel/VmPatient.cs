namespace PatientInformation.ViewModel
{
    public class VmPatient
    {
        public VmPatient()
        {
            Ncds = new List<VmNcds>();
            Allergies = new List<VmAllergies>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int AllergyDetailsId { get; set; }
        public int NcdDetailsId { get; set; }
        public int DiseaseId { get; set; }
        public string DiseaseName { get; set; }
        public string NcdIds { get; set; }
        public string AllergyIds { get; set; }
        public string Epilepsy {  get; set; }
        public List<VmNcds> Ncds { get; set; } 
        public List<VmAllergies> Allergies { get; set; }
    }
    public class VmNcds
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NcdId { get; set; }
    }
    public class VmAllergies
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AllergyId { get; set; }
    }

}
