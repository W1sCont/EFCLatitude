namespace Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public char Gender { get; set; }
        public string Email { get; set; }
        public int CityID { get; set; }
    }
}
