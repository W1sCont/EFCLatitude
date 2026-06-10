namespace ClassLibrary
{
    public class Studio
    {
        public int Id {  get; set; }
        public string? Name { get; set; }
        ICollection<Game>  Game_Id {  get; set; } = new HashSet<Game>();
    }
}
