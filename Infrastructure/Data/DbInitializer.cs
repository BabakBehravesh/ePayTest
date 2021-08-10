using ePay.ApplicationCore.Models;
using System;
using System.Linq;

namespace ePay.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ePayContext context)
        {
            context.Database.EnsureCreated();

            
            //if (context.Persons.Any())
            //{
            //    return;   
            //}

            //var persons = new Person[]
            //{
            //    new Person{Id=1,FirstName="Carson",LastName="Alexander",Age=12},
            //    new Person{Id=2,FirstName="Carson",LastName="Alexander",Age=12},
            //    new Person{Id=3,FirstName="Carson",LastName="Alexander",Age=12},
            //};
            //foreach (var s in persons)
            //{
            //    context.Persons.Add(s);
            //}
            //context.SaveChanges();
        }
    }
}
