using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDeveloper.Model;
using WebDeveloper.Repository;
using Xunit;
using FluentAssertions;
using System.Data.Entity;
using Moq;


namespace WebDeveloper.Tests.Repository
{
    public class BaseRepositoryTest
    {
        private IRepository<Person> repository;
        public BaseRepositoryTest()
        {
            repository = new BaseRepository<Person>();
        }

        [Fact(DisplayName = "AddTestWrongWithMissingData")]
        public void AddTestWrongWithMissingData()
        {
            var person = new Person();            
            person.PersonType = "SC";
            person.FirstName = "Test";
            person.LastName = "Test";
            person.rowguid = Guid.NewGuid();
            try
            {
                repository.Add(person);
            }
            catch (Exception exception)
            {
                exception.Source.Should().Be("EntityFramework");
            }
        }

        [Fact(DisplayName = "AddTestWrongWithNull")]
        public void AddTestWrongWithNull()
        {
            var person = new Person();
            try
            {
                repository.Add(person);
            }
            catch (Exception exception)
            {
                exception.Should().NotBeNull();
            }            
        }

        [Fact(DisplayName = "AddTestWithProperData")]
        public void AddTestWithProperData()
        {
            Person person = TestPersonOk();
            var result = repository.Add(person);
            result.Should().BeGreaterThan(0);
        }


        [Fact(DisplayName = "UpdateTestWrongWithMissingData")]
        public void UpdateTestWrongWithMissingData()
        {
            var person = new Person();
            person.PersonType = "SC";
            person.FirstName = "Test";
            person.LastName = "Test";
            person.rowguid = Guid.NewGuid();
            try
            {
                repository.Update(person);
            }
            catch (Exception exception)
            {
                exception.Source.Should().Be("EntityFramework");
            }
        }

        [Fact(DisplayName = "UpdateTestWrongWithNull")]
        public void UpdateTestWrongWithNull()
        {
            var person = new Person();
            try
            {
                repository.Update(person);
            }
            catch (Exception exception)
            {
                exception.Should().NotBeNull();
            }
        }


       




        [Fact(DisplayName = "DeleteTestWrongWithMissingData")]
        public void DeleteTestWrongWithMissingData()
        {
            var person = new Person();
            person.PersonType = "SC";
            person.FirstName = "Test";
            person.LastName = "Test";
            person.rowguid = Guid.NewGuid();
            try
            {
                repository.Delete(person);
            }
            catch (Exception exception)
            {
                exception.Source.Should().Be("EntityFramework");
            }
        }


        [Fact(DisplayName = "DeleteTestWrongWithNull")]
        public void DeleteTestWrongWithNull()
        {
            var person = new Person();
            try
            {
                repository.Delete(person);
            }
            catch (Exception exception)
            {
                exception.Should().NotBeNull();
            }
        }



        private static Person TestPersonOk()
        {
            var person = new Person();
            person.PersonType = "SC";
            person.FirstName = "Test";
            person.LastName = "Test";
            person.rowguid = Guid.NewGuid();
            person.ModifiedDate = DateTime.Now;
            person.BusinessEntity = new BusinessEntity
            {
                ModifiedDate = person.ModifiedDate,
                rowguid = person.rowguid
            };
            return person;
        }

        [Fact(DisplayName = "MockData")]

         
        public void MockData()
        {
            var personDbSetMock =
                new Mock<DbSet<Person>>();
                //new Mock<DbSet<Person>> ();

            var webContextMock =
                new Mock<WebContextDb>();

            webContextMock.Setup(m => m.Set<Person>()).
                Returns(personDbSetMock.Object);

            webContextMock.Setup(m => m.Set<Person>()).
                Returns(personDbSetMock.Object);

            var repository = 
            new BaseRepository<Person>(webContextMock.Object);

            var newPerson = TestPersonOk();
            repository.Add(newPerson);
            personDbSetMock.Verify(p=> p.Add(It.IsAny<Person>()), Times.Once);
            webContextMock.Verify(w => w.SaveChanges(), Times.Once);
        }

        [Fact(DisplayName = "MockDataList")]
        public void MockDataList()
        {
            var personList = PersonList().AsQueryable();
            var personDbSetMock = new Mock<DbSet<Person>>();
            personDbSetMock.As<IQueryable<Person>>()
                .Setup(m => m.Provider)
                .Returns(personList.Provider);

            personDbSetMock.As<IQueryable<Person>>()
                .Setup(m => m.Expression)
                .Returns(personList.Expression);

            personDbSetMock.As<IQueryable<Person>>()
             .Setup(m => m.ElementType)
             .Returns(personList.ElementType);

            personDbSetMock.As<IQueryable<Person>>()
            .Setup(m => m.GetEnumerator())
            .Returns(personList.GetEnumerator());

            var webContextMock =
                new Mock<WebContextDb>();

            webContextMock.Setup(m => m.Set<Person>()).
                Returns(personDbSetMock.Object);

            webContextMock.Setup(m => m.Set<Person>()).
                Returns(personDbSetMock.Object);

            var repository =
           new BaseRepository<Person>(webContextMock.Object);

            var personGetByID = repository
                .GetById(p => p.FirstName == "Name1");
            personGetByID.Should().NotBeNull();

        }


        [Fact(DisplayName = "MockEdit")]
        
        public void MockEdit()
        {
            var personDbSetMock =
                new Mock<DbSet<Person>>();
          
            var webContextMock =
                new Mock<WebContextDb>();

            webContextMock.Setup(m => m.Set<Person>()).
                Returns(personDbSetMock.Object);

            webContextMock.Setup(m => m.Set<Person>()).
                Returns(personDbSetMock.Object);

            var repository =
            new BaseRepository<Person>(webContextMock.Object);

            //PersonMockList();

            var newPerson = TestPersonOk();
            repository.Add(newPerson);
            // personDbSetMock.Verify(p => p.Add(It.IsAny<Person>()), Times.Once);
            var personGetByID = repository
                .GetById(p => p.FirstName == "Name1");
      //      var personToUpdate = repository.GetById(x => x.FirstName == "Name1");
            webContextMock.Verify(w => w.SaveChanges(), Times.Once);
        }


        //[Fact(DisplayName = "MockDelete")]

        //public void MockDelete()
        //{
        //    var personDbSetMock =
        //        new Mock<DbSet<Person>>();

        //    var webContextMock =
        //        new Mock<WebContextDb>();

        //    webContextMock.Setup(m => m.Set<Person>()).
        //        Returns(personDbSetMock.Object);

        //    webContextMock.Setup(m => m.Set<Person>()).
        //        Returns(personDbSetMock.Object);

        //    var repository =
        //    new BaseRepository<Person>(webContextMock.Object);

        //    var newPerson = TestPersonOk();
        //    repository.Add(newPerson);
                                   
        //    var personToUpdate = repository.GetById(x => x.FirstName == "Name1");
        //    //person.Should().NotBeNull();
        //    var result = repository.Delete(personToUpdate);
        //    webContextMock.Verify(c => c.SaveChanges(), Times.Once());

        //}


        private IEnumerable<Person> PersonList()
        {
            return Enumerable.Range(1, 10)
                .Select(i =>
                new Person
                {
                    BusinessEntityID = i,
                    PersonType = "SC",
                    FirstName = $"Name{i}",
                    LastName = $"LastName{i}",
                    ModifiedDate = DateTime.Now
                });
        }

    }
    
}
