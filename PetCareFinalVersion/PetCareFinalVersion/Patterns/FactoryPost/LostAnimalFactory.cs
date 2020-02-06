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

        public override IPost CreatePost(IPost aPost)
        {

            var Post = new LostAnimalPost()
            {
                Title = aPost.Title,
                Description = aPost.Description
            };

            return Post;
        }
    }
}
