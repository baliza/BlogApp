﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Core.Domain;
using Core.Tools;
using Core.Repositories;



namespace Infrastructure.Repositories
{
    public class PostMockRepository : IRepository<Post>
    {
        static List<Post> collection;
        
        CaseIgnoringStringComparer comparer = new CaseIgnoringStringComparer();

        static PostMockRepository()
        {
            collection = new Post[] 
            { 
                new Post { 
                    Id= "B982F7D5-490C-4A4D-A38B-4499DF858A50",
                    Title="Tomato-Soup", 
                    Body = "Tomato soup is a soup made with tomatoes as the primary ingredient. It may be served hot or cold, and can be made in many styles.[1] It may be smooth in texture, and there are also recipes which include chunks (or small pieces) of tomato, cream and chicken/vegetable stock. Popular toppings for tomato soup include sour cream or croutons. Tomato soup is one of the top comfort foods in Poland[2] and the United States.[3] It can be made fresh by blanching tomatoes, removing the skins, then blending into a puree.The first noted tomato soup was made by Maria Parloa in 1872, and Joseph A. Campbell's recipe for condensed tomato soup in 1897 further increased its popularity", 
                    Author = "TheCook", 
                    Permalink="TomatoSoup", 
                    Tags = new string[]{"tomato", "soup", "food"}, 
                    CreatedOn = new DateTime(2013,11,27,19,11,00),
                    Comments= new Comment[]{
                        new Comment{ Body= "this is a soup", Name ="Pi"},
                        new Comment{ Body= "super soup", Name ="Gigi", Likes=1},
                        new Comment{ Body= "I like potatoes soup too", Name ="john", Email="john@home.ie"}}},
                new Post {      
                    Id="E3A6D92C-BE7D-40FA-881A-66C9A0CCB11E",
                    Title= "Yo-yo-the-game", 
                    Body = "The yo-yo in its simplest form is an object consisting of an axle connected to two disks, and a length of string looped around the axle, similar to a slender spool. It is played by holding the free end of the string (usually by inserting one finger in a slip knot) allowing gravity or the force of a throw to spin the yo-yo and unwind the string (similar to how a pullstring works), then allowing the yo-yo to wind itself back to one's hand, exploiting its spin (and the associated rotational energy). This is often called yo-yoing. First made popular in the 1920s, yo-yoing remains a popular pastime of many generations and cultures. It was first invented in ancient Greece. In the simplest play, the string is intended to be wound on the spool by hand; The yo-yo is thrown downwards, hits the end of the string, then winds up the string toward the hand, and finally the yo-yo is grabbed, ready to be thrown again. One of the most basic tricks is called the sleeper, where the yo-yo spins at the end of the string for a noticeable amount of time before returning to the hand or foot.Many yo-yo tricks are done while the yo-yo is said to be sleeping.[1] One of the most famous tricks on the yo-yo is walk the dog.[citation needed] This is done by throwing a strong sleeper and allowing the yo-yo to roll across the floor, before tugging it back to the hand. English historical names for the yo-yo include bandalore (from French) and quiz. French historical terms include bandalore, incroyable, de Coblenz, emigrette, and joujou de Normandie (joujou meaning little toy)", 
                    Author = "Toystory", 
                    CreatedOn = new DateTime(2013,11,28,20,11,16),
                    Tags = new string[]{"yoyo", "toy", "stuff"}, 
                    Permalink ="Yoyo_the_game"},
                new Post { 
                    Id="91ca04f1-5f31-4bb8-bd0d-7641d357d487",
                    Title="Hammer",
                    Body = "A hammer is a tool meant to deliver an impact to an object. The most common uses for hammers are to drive nails, fit parts, forge metal and break apart objects. Hammers are often designed for a specific purpose, and vary in their shape and structure. The term hammer is also used for some devices that are designed to deliver blows, e.g., the caplock mechanism of firearms.The hammer is a basic tool of many professions. The usual features are a handle and a head, with most of the weight in the head. The basic design is hand-operated, but there are also many mechanically operated models, such as steam hammers, for heavier uses.", 
                    Author = "Handy Man", 
                    Permalink = "Hammer", 
                    CreatedOn = new DateTime(2013,11,18,10,5,35),
                    Tags = new string[]{"tool", "stuff"}, 
                    Comments= new Comment[]{
                        new Comment{ Body= "this is a tool", Name ="Pi"},
                        new Comment{ Body= "super tool", Name ="Gigi", Likes=1},
                        new Comment{ Body= "I like hammers too", Name ="mike", Email="mike@home.ie"}}
                    } 
            }.ToList();
        }
             
        public bool Insert(Post entity)
        {
            entity.Id = Guid.NewGuid().ToString();
            entity.CreatedOn = DateTime.Now;
            collection.Add(entity);
            return true;
        }

        public bool Update(Post entity)
        {
            var idx = collection
                .FindIndex(
                (item) => 
                    comparer
                    .Equals(item.Permalink, entity.Permalink));

            if (idx < 0) return false;
            
            collection[idx] = entity;
            return true;
        }

        public bool Delete(Post entity)
        {
            var idx = collection
                .FindIndex(
                (item) => 
                    comparer
                    .Equals(item.Permalink, entity.Permalink));

            if (idx < 0) return false;
            collection.RemoveAt(idx);
            return true;
        }

        public IEnumerable<Post> SearchFor(System.Linq.Expressions.Expression<Func<Post, bool>> predicate)
        {
            return collection
                .AsQueryable<Post>()
                .Where(predicate);
        }

        public IEnumerable<Post> GetAll()
        {
            return collection;
        }

        public Post GetById(string id)
        {
            return collection
               .FirstOrDefault((item) => item.Id == id);
        }
    }
}
