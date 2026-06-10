namespace ClassLibrary
{
    public enum GameMode
    {
        Singleplayer,
        Multiplayer,
        Cooperative,
        MMO
    }
    public class Game
    {
        public int Id { get; set; }
        public string? Name {  get; set; }
        public string? Style { get; set; }
        public DateTime Date { get; set; }
        public GameMode? GameMode { get; set; }
        public int? Sold { get; set; }
        public int? StudioId { get; set; }
        public virtual Studio Studio { get; set; }
    }
}
