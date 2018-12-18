using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using Raven.Client.Document;

namespace Framework_not_migrated
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var persons = new List<Person>();
            using (IDocumentStore store = new DocumentStore()
            {
                Url = "http://localhost:8080"
            }.Initialize())
            {
                var session = store.OpenSession(new OpenSessionOptions()
                {
                    Database = "PersonsCore"
                });

                session.Store(new Person
                {
                    FirstName = "Ariel" + DateTime.UtcNow.Second,
                    LastName = "Ornstein"
                });

                session.SaveChanges();
                persons = session.Query<Person>().ToList();

                foreach (var person in persons)
                {
                    Console.WriteLine("Frist name {0} \nLast name: {1} ", person.FirstName, person.LastName);
                }
            }
               

                Console.ReadKey();
            
        }
    }

    public class Person
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

