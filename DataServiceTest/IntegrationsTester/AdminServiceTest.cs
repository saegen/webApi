using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataServiceTest.IntegrationsTester
{
    using DataServiceTest.AdminService;
    using DataServiceTest.SubscriptionService;
    using DataServiceTest.UserService;
    using System.Collections.Generic;
    using System.Linq;

    [TestClass]
    public class AdminServiceTest
    {

        private AdminServiceClient _AdminClient = null;
        private UserServiceClient _UserClient = null;
        private SubscriptionServiceClient _SubClient = null;
        private int _userId = -1, _numSubs = -1;
        private Guid _subId = Guid.Empty;
        private List<Common.ApiSubscription> subs = new List<Common.ApiSubscription>();
        private IEnumerable<Guid> subIDs = null;
        private CreateUserDTO testUser = new CreateUserDTO() { Email = "admin@service.se", FirstName = "TestUser", LastName = "AdminServiceTest" };

        [TestInitialize]
        public void TestInit()
        {
            _SubClient = new SubscriptionServiceClient();
            subIDs = _SubClient.GetSubscriptions().Select(item => item.Id).ToArray();
            _numSubs = subIDs.Count();
            _SubClient.CloseOrAbortService();
            _UserClient = new UserServiceClient();
            _userId = _UserClient.CreateUser(testUser).Id;
            _UserClient.CloseOrAbortService();
        }
        [TestMethod]
        public void TestAdminService()
        {
            Assert.IsTrue(_userId > 0,"Kunde inte skapa testuser!");
            Assert.IsTrue(_numSubs > 0, "Finns inga subscriptions att testa med!");
            _AdminClient = new AdminServiceClient();
            _AdminClient.Subscribe(_userId,(Guid[])subIDs);
            int userSubNum = _AdminClient.GetUserSubscriptions(_userId).Count();
            _AdminClient.CloseOrAbortService();
            Assert.IsTrue(userSubNum == _numSubs, "userId=" + _userId + ", userSubNum =" + userSubNum + ", _numSubs =" + _numSubs);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _UserClient = new UserServiceClient();
            _UserClient.DeleteUser(_userId);
            _UserClient.CloseOrAbortService();
        }
    }
}
