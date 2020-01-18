using System;
using System.Collections.Generic;
using Bogus;
using BurgerMonkeys.Tools;
using DocsAPI.Entities;

namespace DocsAPI.Builders
{
    public class UserBuilder
    {
        public static UserBuilder New()
        {
            return new UserBuilder();
        }


        public User Build()
        {
            var user = new User();

            var faker = new Faker();
            user.Id = Generator.GetId(8);
            user.Email = faker.Person.Email;
            user.Name = faker.Person.FullName;

            return user;
        }

        public List<User> BuildList(int num)
        {
            List<User> users = new List<User>();

            for (int i = 0; i < num; i++)
            {
                users.Add(Build());
            }

            return users;
        }
    }
}
