
using System.Web.Mvc;
using WebDeveloper.Areas.Personnel.Controllers;
using Xunit;
using FluentAssertions;
using System.Collections;
using WebDeveloper.Model;
using System.Collections.Generic;
using System.Linq;
using WebDeveloper.Repository;
using System;

namespace WebDeveloper.Tests.Controllers
{
    public class PersonControllerTest
    {
        private PersonController controller;
        public PersonControllerTest()
        {
            controller = new PersonController(new BaseRepository<Person>());
        }

        [Fact(DisplayName = "ListActionWithEmptyParameters")]
        public void ListActionWithEmptyParameters()
        {
            var result = controller.List(null, null) as PartialViewResult;
            result.ViewName.Should().Be("_List");

            var modelCount = (IEnumerable<Person>)result.Model;
            modelCount.Count().Should().Be(15);
        }

        [Fact(DisplayName = "CreateActionWithParameters")]
        public void CreateActionWithParameters()
        {
            var person = new Person();
            person.PersonType = "SC";
            person.FirstName = "Miguel";
            person.LastName = "Serna";
            person.rowguid = Guid.NewGuid();
            try
            {
                var result = controller.Create(person);
            }
            catch (Exception exception)
            {
                exception.Source.Should().Be("EntityFramework");
            }

        }

        [Fact(DisplayName = "DeleteActionWithEmptyParameters")]
        public void DeleteActionWithEmptyParameters()
        {
            var person = new Person();
            person.PersonType = "SC";
            person.FirstName = "Claudia";
            person.LastName = "Pereda";
            person.rowguid = Guid.NewGuid();
            try
            {
                controller.Delete(person);
            }
            catch (Exception exception)
            {
                exception.Source.Should().Be("EntityFramework");
            }
        }

    }
}
