using PetCareFinalVersion.Data;
using PetCareFinalVersion.Models;

namespace PetCareFinalVersion.Patterns.FactoryPost
{
    public sealed class PostFactory : AbstractPostsFactory
    {
        private static readonly PostFactory mInstance = new PostFactory();

        private PostFactory() { }
        static PostFactory() { }

        public static PostFactory Instance => mInstance;

        public override IPost CreatePost(IPost aPost)
        {

            var Post = new Post()
            {
                Title = aPost.Title,
                Description = aPost.Description,
            };

            return Post;
        }
    }
}






