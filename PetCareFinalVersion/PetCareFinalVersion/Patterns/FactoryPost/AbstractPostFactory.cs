
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetCareFinalVersion.Data;

namespace PetCareFinalVersion.Patterns.FactoryPost
{
    public abstract class AbstractPostFactory
    {
        public IPost CreatePostFromPostFactory(string aTitle, string aDescription, int Association_id)
        {
            IPost post = CreatePost( aTitle, aDescription, Association_id);
            return post;
        }
        public abstract IPost CreatePost(string aTitle, string aDescription, int Association_id);

    }
}




