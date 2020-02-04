using PetCareFinalVersion.Data;

namespace PetCareFinalVersion.Patterns.FactoryPost
{
    public abstract class AbstractPostsFactory
    {
        public IPost CreatePostFromPostFactory(string Title, string Description)
        {
            IPost post = CreatePost(Title, Description);
            return post;
        }

        public abstract IPost CreatePost(string Title, string Description);


    }
}




