using System;
using System.Collections.Generic;

namespace SuperHero.Domain.Entities
{
    public class Hero
    {
        public Hero() { }
        public Hero(string name,
            Editor editor,
            int age)
        {
            Name = name;
            Editor = editor;
            Age = age;
            Created = DateTime.Now;
        }

        public Hero(int id,
            string name,
            Editor editor,
            int age,
            DateTime created)
        {
            SetId(id);
            Name = name;
            Editor = editor;
            Age = age;
            Created = created;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public Editor Editor { get; private set; }
        public int Age { get; private set; }
        public DateTime Created { get; private set; }

        public bool IsValid()
        {
            var valid = true;

            if (string.IsNullOrEmpty(Name) ||
                    Editor.Id <= 0)
            {
                valid = false;
            }

            return valid;
        }

        public bool IsMaiority()
        {
            return Age >= 18;
        }

        public void SetId(int id)
        {
            Id = id;
        }

    }
}
