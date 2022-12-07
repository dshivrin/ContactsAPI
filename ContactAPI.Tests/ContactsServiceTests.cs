using ContactsAPI.Models;
using ContactsAPI.Service.Services;
using Moq;

namespace ContactAPI.tests
{
    public class ContactsServiceTests
    {

        private Contact GetMockContact()
        {
            return new Contact()
            {
                SocialNumber = 317449320,
                BirthDate = DateTime.Now,
                Email = "a@a.com",
                Gender = "Male",
                Name = "Test",
                Phone = "0544907782"
            };
        }

        private Contact GetMockContact2()
        {
            return new Contact()
            {
                SocialNumber = 317449320,
                BirthDate = DateTime.Now,
                Email = "b@b.com",
                Gender = "Female",
                Name = "Test 2",
                Phone = "0544907783"
            };
        }

        private Mock<IContactService> contactsServiceMock;

        [Fact]
        public async void TestAddAsync()
        {
            var contactsServiceMock = new Mock<IContactService>();

            var mockContact = GetMockContact();
            contactsServiceMock.Setup(c => c.AddAsync(mockContact)).Returns(Task.FromResult(mockContact));

            var result = await contactsServiceMock.Object.AddAsync(mockContact);

            Assert.Equivalent(mockContact, result);
        }

        [Fact]
        public async void TestGetAllContacts()
        {
            var contactsServiceMock = new Mock<IContactService>();

            var firstContact = GetMockContact();
            var secondContact = GetMockContact2();

            contactsServiceMock.Setup(c => c.AddAsync(firstContact)).Returns(Task.FromResult(firstContact));
            //adding 2 contacts to be queried
            await contactsServiceMock.Object.AddAsync(firstContact);
            await contactsServiceMock.Object.AddAsync(secondContact);

            var expected = new List<Contact>() { firstContact, secondContact };
            contactsServiceMock.Setup(c => c.GetAll()).Returns(expected);

            var result = contactsServiceMock.Object.GetAll();

            Assert.Equivalent(expected, result);
        }

        [Fact]
        public async void TestGetContactByIDAsync()
        {
            var contactsServiceMock = new Mock<IContactService>();
            var contact = GetMockContact();
            contactsServiceMock.Setup(c => c.AddAsync(contact)).Returns(Task.FromResult(contact));
            contactsServiceMock.Setup(c=>c.GetContactByIDAsync(contact.Id)).Returns(Task.FromResult(contact));

            //first add the contact
            var addResut = await contactsServiceMock.Object.AddAsync(contact);
            //query for it
            var queryResult = await contactsServiceMock.Object.GetContactByIDAsync(addResut.Id);

            //should be same
            Assert.Equivalent(addResut, queryResult);
        }

        [Fact]
        public async void TestUpdateAsync()
        {

        }

        [Fact]
        public async void DeleteAsync()
        {

        }
    }
}