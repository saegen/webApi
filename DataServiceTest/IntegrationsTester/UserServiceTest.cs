using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ServiceModel;

namespace DataServiceTest.IntegrationsTester
{
    using DataServiceTest.UserService;
    using DataService;
    
    [TestClass]
    public class UserServiceTest
    {
        private UserServiceClient _client = null;
        private CreateUserDTO _testUserDTO = null;
        private ApiUser _testUser = null;
        private int _testUserId = -1;
        [TestMethod]
        public void TestUserService()
        {
            _client = new UserServiceClient();
            _testUserDTO = new CreateUserDTO() { FirstName = "Testarn", LastName = "Testsson", Email="förnamn.efternamn@server.se" };
            try
            {
                //skapa
                _testUser = _client.CreateUser(_testUserDTO);
                Assert.IsTrue(_testUser.Id > 0,"Kunde inte skapa användaren! id < 1");
                _testUserId = _testUser.Id;
                //Hämta
                var fromDb =_client.GetUser(_testUserId);
                Assert.IsNotNull(fromDb, "Kunde inte hitta användaren!med = {0}",_testUserId);
                //uppdatera
                fromDb.FirstName = "TestarnUpd";
                fromDb.Email = "testarn.testsson@server.upd";
                UpdateUserDTO updUser = new UpdateUserDTO() {Id=fromDb.Id, FirstName = fromDb.FirstName, LastName = fromDb.LastName, Email = fromDb.Email };
                _testUser = _client.UpdateUser(updUser);
                Assert.IsTrue(_testUser.LastName == "Testsson", "Emailen har uppdaterats!");
                Assert.IsTrue(_testUser.FirstName == "TestarnUpd", "FirstName har inte uppdaterats!");
                Assert.IsTrue(_testUser.Email == "testarn.testsson@server.upd", "Emailen har uppdaterats!");
                _client.DeleteUser(_testUserId);
                var deletedUser = _client.GetUser(_testUserId);
                Assert.IsNull(deletedUser);
                _client.CloseOrAbortService();
            }
            finally
            {
                _client.CloseOrAbortService();
                
            }
        }

        

    }
}
