using System;
using System.Collections.Generic;
using System.Xml.Linq;
using BusinessEntities;
using Common;

namespace Core.Services.Users
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class UpdateUserService : IUpdateUserService
    {
        public void Update(User user, string name, string email, UserTypes type, int age, decimal? annualSalary, IEnumerable<string> tags)
        {

            user.SetEmail(email);
            user.SetName(name);
            user.SetType(type);
            user.SetAge(age);
            user.SetMonthlySalary(annualSalary.HasValue ? annualSalary / 12 : null);
            user.SetTags(tags);
        }


        public IEnumerable<string> ValidateModelInputs(string name, string email, UserTypes type, int age, decimal? annualSalary, IEnumerable<string> tags)
        {
            var validationErrors = new List<string>();
            if (string.IsNullOrEmpty(name))
            {
                validationErrors.Add("Name was not provided.");
            }
            if (string.IsNullOrEmpty(email))
            {
                validationErrors.Add("Email was not provided.");
            }
            return validationErrors;
        }
    }
}