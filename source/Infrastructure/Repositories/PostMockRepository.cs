using System;
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
                    Title="Tomato-Soup", 
                    Body = "Tomato soup is a soup made with tomatoes as the primary ingredient. It may be served hot or cold, and can be made in many styles.[1] It may be smooth in texture, and there are also recipes which include chunks (or small pieces) of tomato, cream and chicken/vegetable stock. Popular toppings for tomato soup include sour cream or croutons. Tomato soup is one of the top comfort foods in Poland[2] and the United States.[3] It can be made fresh by blanching tomatoes, removing the skins, then blending into a puree.The first noted tomato soup was made by Maria Parloa in 1872, and Joseph A. Campbell's recipe for condensed tomato soup in 1897 further increased its popularity", 
                    Author = "TheCook", 
                    Permalink="TomatoSoup", 
                    Tags = new string[]{"tomato", "soup", "food"}, 
                    Comments= new Comment[]{
                        new Comment{ body= "this is a soup", name ="Pi"},
                        new Comment{ body= "super soup", name ="Gigi", likes=1},
                        new Comment{ body= "I like potatoes soup too", name ="john", email="john@home.ie"}}},
                new Post {                     
                    Title= "Yo-yo-the-game", 
                    Body = "The yo-yo in its simplest form is an object consisting of an axle connected to two disks, and a length of string looped around the axle, similar to a slender spool. It is played by holding the free end of the string (usually by inserting one finger in a slip knot) allowing gravity or the force of a throw to spin the yo-yo and unwind the string (similar to how a pullstring works), then allowing the yo-yo to wind itself back to one's hand, exploiting its spin (and the associated rotational energy). This is often called yo-yoing. First made popular in the 1920s, yo-yoing remains a popular pastime of many generations and cultures. It was first invented in ancient Greece. In the simplest play, the string is intended to be wound on the spool by hand; The yo-yo is thrown downwards, hits the end of the string, then winds up the string toward the hand, and finally the yo-yo is grabbed, ready to be thrown again. One of the most basic tricks is called the sleeper, where the yo-yo spins at the end of the string for a noticeable amount of time before returning to the hand or foot.Many yo-yo tricks are done while the yo-yo is said to be sleeping.[1] One of the most famous tricks on the yo-yo is walk the dog.[citation needed] This is done by throwing a strong sleeper and allowing the yo-yo to roll across the floor, before tugging it back to the hand. English historical names for the yo-yo include bandalore (from French) and quiz. French historical terms include bandalore, incroyable, de Coblenz, emigrette, and joujou de Normandie (joujou meaning little toy)", 
                    Author = "Toystory", 
                    Tags = new string[]{"yoyo", "toy", "stuff"}, 
                    Permalink ="Yoyo_the_game"},
                new Post { 
                    Body = "A hammer is a tool meant to deliver an impact to an object. The most common uses for hammers are to drive nails, fit parts, forge metal and break apart objects. Hammers are often designed for a specific purpose, and vary in their shape and structure. The term hammer is also used for some devices that are designed to deliver blows, e.g., the caplock mechanism of firearms.The hammer is a basic tool of many professions. The usual features are a handle and a head, with most of the weight in the head. The basic design is hand-operated, but there are also many mechanically operated models, such as steam hammers, for heavier uses.", 
                    Author = "Hardman", 
                    Permalink = "Hammer", 
                    Tags = new string[]{"tool", "stuff"}, 
                    Comments= new Comment[]{
                        new Comment{ body= "this is a tool", name ="Pi"},
                        new Comment{ body= "super tool", name ="Gigi", likes=1},
                        new Comment{ body= "I like hammers too", name ="mike", email="mike@home.ie"}}
                    } 
            }.ToList();
        }
             
        public bool Insert(Post entity)
        {
            collection.Add(entity);
            return true;
        }

        public bool Update(Post entity)
        {
            var idx = collection.FindIndex((item) => comparer.Equals(item.Permalink, entity.Permalink));
            if (idx < 0) return false;
            collection[idx] = entity;
            return true;
        }

        public bool Delete(Post entity)
        {
            var idx = collection.FindIndex((item) => comparer.Equals(item.Permalink, entity.Permalink));
            if (idx < 0) return false;
            collection.RemoveAt(idx);
            return true;
        }

        public IList<Post> SearchFor(System.Linq.Expressions.Expression<Func<Post, bool>> predicate)
        {
            return collection.AsQueryable<Post>()
                       .Where(predicate)
                       .ToList();
        }

        public IList<Post> GetAll()
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
