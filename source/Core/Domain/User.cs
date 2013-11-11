using System.ComponentModel.DataAnnotations;


namespace Core.Domain
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}