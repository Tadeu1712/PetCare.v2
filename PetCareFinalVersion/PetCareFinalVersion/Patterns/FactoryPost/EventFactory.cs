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
        public override IPost CreatePost(string aTitle, string aDescription)
        {
            var Post = new Event()
            {
                Title = aTitle,
                Description = aDescription
            };

            return Post;
        }
    }
}
