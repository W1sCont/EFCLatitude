namespace ClassLibrary
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int? Age { get; set; }
        public int JobTitleId { get; set; }
        public JobTitle? JobTitle_Id { get; set; }
    }
}
