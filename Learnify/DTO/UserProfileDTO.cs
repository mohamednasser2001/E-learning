namespace Learnify.DTO
{
    public class UserProfileDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public IFormFile ProfilePicture { get; set; }
    }
}
