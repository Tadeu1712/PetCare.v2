using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetCareFinalVersion.Data;
using PetCareFinalVersion.Models;
using PetCareFinalVersion.Patterns.StateMachine;

namespace PetCareFinalVersion.Patterns.FactoryPost
{
    public sealed class PostFactory : AbstractPostsFactory
    {
        private static readonly PostFactory mInstance = new PostFactory();

        private PostFactory() { }
        static PostFactory() { }

        public static PostFactory Instance => mInstance;

        public override IPost CreatePost(string aTitle,string aDescription)
        {

            var Post = new Post()
            {
                Title = aTitle,
                Description = aDescription,
            };

            return Post;
        }
    }
}






