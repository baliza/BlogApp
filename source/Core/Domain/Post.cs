using System;
using System.ComponentModel.DataAnnotations;


namespace Core.Domain
{
    public class Comment
    {


        public string Name { get; set; }
        [Required]
        public string Body { get; set; }
        public int Likes { get; set; }
        [Required]
        public string Email { get; set; }
    }
    public class Post : Entity
    {
        [Required]
        public string Body { get; set; }
        public string Permalink { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Title { get; set; }
        public string[] Tags { get; set; }
        public Comment[] Comments { get; set; }
    }

}