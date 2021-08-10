using ePay.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.BLL
{
    public class InsertionService
    {
        public InsertionService()
        {

        } 
        private static List<Person> people = new List<Person>();

        public static void Insert(Person person, List<Person> people)
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

        private static int Compare(Person personNew, Person peopleOld)
        {
            if (personNew.LastName.CompareTo(peopleOld.LastName) == 0)
            {
                return personNew.FirstName.CompareTo(peopleOld.FirstName);
            }
            return personNew.LastName.CompareTo(peopleOld.LastName);
        }
    }
}
