using WebApiProject.Entities;

namespace WebApiProject.Contracts
{
    public class UpdateAdressRequest
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public User User { get; set; } // Navigation Property
        public int UserID { get; set; }
    }
}
