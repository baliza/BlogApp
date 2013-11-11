using System;
using System.ComponentModel.DataAnnotations;


namespace Core.Domain
{
    public class Post : Entity
    {     
        public string Body { get; set; }
        public string Permalink { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string[] Tags { get; set; }
        public string[] Comments { get; set; }
        public DateTime Date { get; set; }

    }

}