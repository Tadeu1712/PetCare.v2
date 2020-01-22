using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetCareFinalVersion.Data;
using PetCareFinalVersion.Models;


namespace PetCareFinalVersion.Patterns.FactoryPost
{
    public sealed class EventFactory : AbstractPostsFactory
    {
        private static readonly EventFactory mInstance = new EventFactory();

        private EventFactory() { }
        static EventFactory() { }

        public static EventFactory Instance => mInstance;
        public override IPost CreatePost(IPost aPost)
        {
            var Post = new Event()
            {
                Title = aPost.Title,
                Description = aPost.Description
            };

            return Post;
        }
    }
}
