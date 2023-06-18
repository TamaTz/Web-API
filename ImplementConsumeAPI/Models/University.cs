namespace ImplementConsumeAPI.Models
{
    public class University
    {
        public Guid Guid { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
