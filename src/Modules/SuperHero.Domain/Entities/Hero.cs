using System;

namespace SuperHero.Domain.Entities
{
    public class Hero
    {
        public Hero(string name, 
                    string editor)
        {
            Id = Guid.NewGuid();
            Name = name;
            Editor = editor;
            Created = DateTime.Now;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Editor { get; private set; }
        public DateTime Created { get; private set; }

        public bool IsValid()
        {
            var valid = true;

            if (string.IsNullOrEmpty(Name) ||
                    string.IsNullOrEmpty(Editor))
            {
                valid = false;
            }

            return valid;
        }
    }
}
