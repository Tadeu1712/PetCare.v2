using System;
using PetCareFinalVersion.Models;
using PetCareFinalVersion.Patterns.FactoryPost;

namespace PetCareFinalVersion.Patterns.Observer
{
    public class LostAnimalObserver : IObserver
    {
        private readonly AppDbContext _context;
        private readonly AbstractPostsFactory lost_factory = LostAnimalFactory.Instance;

        public LostAnimalObserver(AppDbContext aContext)
        {
            _context = aContext;
        }

        public void update(string title, string description ,string location, string contact, string image)
        {
            var postLostAnimal = (LostAnimalPost)lost_factory.CreatePostFromPostFactory(title, description);
            postLostAnimal.Contact = contact;
            postLostAnimal.Location = location;
            postLostAnimal.Date = DateTime.Now;
            _context.LostAnimalPosts.AddAsync(postLostAnimal);
            _context.SaveChangesAsync();
        }
    }
}
