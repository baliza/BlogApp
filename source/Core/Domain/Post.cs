using System;
using System.ComponentModel.DataAnnotations;


namespace Core.Domain
{
    public class Comment { 
        public string name { get; set; }
        public string body { get; set; }
        public int likes { get; set; }
        public string email { get; set; }
    }
    public class Post : Entity
    {     
        public string Body { get; set; }
        public string Permalink { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string[] Tags { get; set; }
        public Comment[] Comments { get; set; }
    }

}