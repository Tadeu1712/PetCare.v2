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

        public void update(Animal aAnimal)
        {
            var postLostAnimal = (LostAnimalPost)lost_factory.CreatePostFromPostFactory(aAnimal.Name, aAnimal.Description);
            var assoc = _context.Associations.Find(aAnimal.Association_id);
            postLostAnimal.Contact = assoc.PhoneNumber;
            postLostAnimal.Location = assoc.Adress;
            postLostAnimal.Date = DateTime.Now;
            _context.LostAnimalPosts.AddAsync(postLostAnimal);
            _context.SaveChangesAsync();
        }
    }
}
