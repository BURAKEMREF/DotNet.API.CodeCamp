namespace WebApiProject.Entities
{
    public class Adress
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Title { get; set; }
        public string body { get; set; }
        public User User { get; set; } // Navigation Property
        public int UserID { get; set; }
    }
}
