namespace ClassLibrary
{
    public class JobTitle
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
