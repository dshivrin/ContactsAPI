using ContactsAPI.Data.Models;
using ContactsAPI.Data.Repositories;
using ContactsAPI.Models;
using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;

namespace ContactsAPI.Tests
{
    public class ContactsRepositoryTests
    {

        private readonly DbContextMock<ContactContext> dbContextMock;
        private readonly ContactRepository repository;

        public ContactsRepositoryTests()
        {
            var initialEntities = GetInitialDbEntities();
            dbContextMock = new DbContextMock<ContactContext>(new DbContextOptionsBuilder<ContactContext>()
                .UseInMemoryDatabase(databaseName: "Contacts")
                .Options);
            dbContextMock.CreateDbSetMock(x => x.Contact, initialEntities);

            repository = ContactRepositoryInit(dbContextMock);
        }

        //private static Mock<IConfiguration> GetMockConfig()
        //{
        //    Mock<IConfiguration> mockConfig = new Mock<IConfiguration>();
        //    mockConfig.SetupGet(x => x[It.Is<string>(s => s == "ConnectionStrings:contacts")])
        //        .Returns("Server=(LocalDb)\\cardcom;Database=Contacts;Trusted_Connection=True;TrustServerCertificate=True;");
        //    return mockConfig;
        //}

        private static ContactRepository ContactRepositoryInit(DbContextMock<ContactContext> dbContextMock)
        {
            return new ContactRepository(dbContextMock.Object);
        }

        private static Contact[] GetInitialDbEntities()
        {
            return new Contact[]
            {
                new Contact{
                Id = 1,
                SocialNumber = 317449320,
                BirthDate = DateTime.Now.AddYears(-18),
                Email = "a@a.com",
                Gender = "Female",
                Name = "Test 1",
                Phone = "0544907783"},
                new Contact{
                Id = 2,
                SocialNumber = 123456789,
                BirthDate = DateTime.Now.AddYears(-20),
                Email = "b@b.com",
                Gender = "Male",
                Name = "Test 2",
                Phone = "0544907784"},
            };
        }

        private static Contact GetOneContact()
        {
            return new Contact
            {
                SocialNumber = 987654321,
                BirthDate = DateTime.Now.AddYears(-25),
                Email = "c@c.com",
                Gender = "Other",
                Name = "Test 3",
                Phone = "0544907784"
            };
        }

        [Fact]
        private void GetAllTest()
        {
            var result = repository.GetAll().ToList();

            Assert.True(result.Any());
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void GetAllContactsAsyncTest()
        {
            var result = await repository.GetAllContactsAsync();

            Assert.True(result.Any());
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void GetContactByIDAsyncTest()
        {
            var result = await repository.GetContactByIDAsync(2);

            Assert.Equal(2, result.Id);
        }

        //having trouble with in memory EF
        [Fact]
        public async void AddAsyncTest()
        {
            var contact = GetOneContact();

            var result = await repository.AddAsync(contact);

            var search = await repository.GetAllContactsAsync();

            var newAdded = search.FirstOrDefault(c => c.Email == contact.Email);
            Assert.NotNull(newAdded);
            //Assert.Equal(contact.Email, result.Email);

        }

        [Fact]
        public async void UpdateAsyncTest()
        {
            var contactToUpdate = await repository.GetContactByIDAsync(1);

            contactToUpdate.Name = "Updated Name";

            var result = await repository.UpdateAsync(contactToUpdate);
            var search = await repository.GetContactByIDAsync(1);

            Assert.NotNull(search);
            Assert.Equal(contactToUpdate.Name, search.Name);
        }

        [Fact]
        public async void DeleteAsyncTest()
        {
            var contactTodelete = await repository.GetContactByIDAsync(2);

            var deleted = await repository.DeleteAsync(contactTodelete);
            try
            {
                var test = await repository.GetContactByIDAsync(2);
            }
            catch (Exception ex)
            {
                //should throw an exception
                Assert.NotNull(ex);
                return;
            }

            Assert.Fail("Should have caught an exception");
        }

    }
}
