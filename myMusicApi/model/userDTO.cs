using System.ComponentModel.DataAnnotations;

namespace myMusicApi.model
{
    public class userDTO
    {
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string phoneNumber { get; set; }
        [Required]
        public string passwordHash { get; set; }
        [Required]
        public string userName { get; set; }

    }
}
