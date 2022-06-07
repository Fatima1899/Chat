using System.ComponentModel.DataAnnotations;

namespace Chat.View_Models
{
    public class LoginVM
    {
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
