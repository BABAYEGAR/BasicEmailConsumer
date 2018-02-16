namespace EmailConsumer.Models.Entities
{
    public class Received
    {
        public AppUser AppUser { get; set; }
        public string Role { get; set; }
        public AppUserAccessKey AppUserAccessKey { get; set; }
    }
}
