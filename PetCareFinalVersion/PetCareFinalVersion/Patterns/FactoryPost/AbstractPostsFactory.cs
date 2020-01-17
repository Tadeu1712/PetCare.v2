
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetCareFinalVersion.Data;

namespace PetCareFinalVersion.Patterns.FactoryPost
{
    public abstract class AbstractPostsFactory
    {
        public IPost CreatePostFromPostFactory(IPost aPost)
        {
            IPost post = CreatePost(aPost);
            return post;
        }
        public abstract IPost CreatePost(IPost aPost);

    }
}




