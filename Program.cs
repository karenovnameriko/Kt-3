using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;

namespace UserRegistrationApp
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
    }

    public class AgeException : Exception
    {
        public AgeException(int age)
        {
            Console.WriteLine($"Регистрация запрещена для пользователей младше 14 лет. Возраст пользователя: {age} лет");
        }



    }

    class Program
    {
        static void Main(string[] args)
        {
            var faker = new Faker<User>();
            faker = faker
            .RuleFor(p => p.FirstName, f => f.Name.FirstName())
            .RuleFor(p => p.LastName, f => f.Name.LastName())
            .RuleFor(p => p.Age, f => f.Random.Number(3, 65))
            .RuleFor(p => p.Email, (f, p) => f.Internet.Email(p.FirstName, p.LastName));

            var people = faker.Generate(10);
            foreach (var person in people)
            {
                Console.WriteLine($"Имя: {person.FirstName} {person.LastName}");
                Console.WriteLine($"Email: {person.Email}");
                Console.WriteLine($"Возраст: {person.Age} лет");

                try
                {
                    if (person.Age < 14)
                    {
                        throw new AgeException(person.Age);
                    }
                    Console.WriteLine("Регистрация разрешена");
                }
                catch (AgeException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
            }


        }

    }
}