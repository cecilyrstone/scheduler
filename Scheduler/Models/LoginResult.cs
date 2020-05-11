namespace Scheduler.Models
{
    public class LoginResult
    {
        public User User { get; set; }
        public bool Successful { get; set; }
        public string Message { get; set; }
    }
}
