using System.ComponentModel.DataAnnotations;

namespace DotNetTalk.MorseMessenger.Common
{
    public class Message
    {
        [Required]
        [StringLength(10, ErrorMessage = "Name is too long.")]
        public string Body { get; set; }
    }
}
