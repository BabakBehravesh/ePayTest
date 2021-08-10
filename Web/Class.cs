﻿using ePay.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web
{
    class temp
    { 
        private static List<Person> people = new List<Person>();

        //{
        //    new Person { Id=1, FirstName= "Ali",  LastName= "Ijadi" },
        //    new Person { Id = 2, FirstName = "Saeed", LastName = "Sadati" },
        //    new Person { Id = 3, FirstName = "Sara", LastName = "Hassani" }
        //};

        private void Insert(Person person, List<Person> people)
        {
            // [TODO] All to lower case
            int countItems = people.Count();

            for (int i = 0; i < countItems; i++)
            {

                if (Compare(person, people[i]) < 0)
                {
                    people.Add(new Person());
                    for (int j = people.Count() - 1; j > i; j--)
                    {
                        people[j] = people[j - 1];
                    }
                    people[i] = person;
                    break;
                }
            }

            // list is empty or the person should be placed at the end of the list
            if (people.Count() == 0 || countItems == people.Count())
            {
                people.Add(person);
            }
        }

        private int Compare(Person personNew, Person peopleOld)
        {
            if (personNew.LastName.CompareTo(peopleOld.LastName) == 0)
            {
                return personNew.FirstName.CompareTo(peopleOld.FirstName);
            }
            return personNew.LastName.CompareTo(peopleOld.LastName);
        }

        void Main(string[] args)
        {
            Random random = new Random();

            int age = random.Next(10, 90);

            List<string> firstNames = new List<string> { "Leia", "Sadie", "Jose", "Sara", "Frank", "Dewey", "Tomas", "Joel", "Lukas", "Carlos" };
            List<string> lastNames = new List<string> { "Liberty", "Ray", "Harrison", "Ronan", "Drew", "Powell", "Larsen", "Chan", "Anderson", "Lane" };

            for (int i = 0; i < 20; i++)
            {
                Person person = new Person { Id = i, FirstName = firstNames[random.Next(0, firstNames.Count)], LastName = lastNames[random.Next(0, lastNames.Count)], Age = random.Next(10, 90) };
                Insert(person, people);
            }


            foreach (var item in people)
                Console.WriteLine($"{item.Id} \t {item.LastName} \t {item.FirstName} \t {item.Age} ");

            Console.ReadLine();
            Console.WriteLine();
        }
    }
}


