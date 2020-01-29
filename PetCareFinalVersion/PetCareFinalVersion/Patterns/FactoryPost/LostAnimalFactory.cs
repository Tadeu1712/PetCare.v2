using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetCareFinalVersion.Data;
using PetCareFinalVersion.Models;


namespace PetCareFinalVersion.Patterns.FactoryPost
{
   
    public sealed class LostAnimalFactory : AbstractPostsFactory
    {
        private static readonly LostAnimalFactory mInstance = new LostAnimalFactory();

        private LostAnimalFactory() { }
        static LostAnimalFactory() { }

        public static LostAnimalFactory Instance => mInstance;

        public override IPost CreatePost(string aTitle, string aDescription)
        {

            var Post = new LostAnimalPost()
            {
                Title = aTitle,
                Description = aDescription
            };

            return Post;
        }
    }
}
