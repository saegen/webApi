﻿using System;
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
        private IEnumerable<Guid> existingSubIDs = null;
        private CreateUserDTO testUser = new CreateUserDTO() { Email = "admin@service.se", FirstName = "TestUser", LastName = "AdminServiceTest" };

        /// <summary>
        /// Skapar en testanvändare och hämtar upp alla Subscriptions Id till en array. 
        /// När testanvändaren tas bort i Cleanup så försvinner eventuella prenumerationer.
        /// </summary>
        [TestInitialize]
        public void TestInit()
        {
            _SubClient = new SubscriptionServiceClient();
            existingSubIDs = _SubClient.GetSubscriptions().Select(item => item.Id).ToArray();
            _numSubs = existingSubIDs.Count();
            _SubClient.CloseOrAbortService();
            _UserClient = new UserServiceClient();
            _userId = _UserClient.CreateUser(testUser).Id;
            _UserClient.CloseOrAbortService();
            Assert.IsTrue(_userId > 0, "TestInit: Kunde inte skapa testuser! Inga unittest kommer att fungera");
            Assert.IsTrue(_numSubs > 0, "TestInit: Finns inga subscriptions att testa med! Inga unittest kommer att fungera");
        }
        /// <summary>
        /// Testanvändaren prenumererar på alla som finns.
        /// </summary>
        [TestMethod]
        public void TestSubscribe()
        {
            _AdminClient = new AdminServiceClient();
            _AdminClient.Subscribe(_userId,(Guid[])existingSubIDs);
            int userSubNum = _AdminClient.GetUserSubscriptions(_userId).Count();
            _AdminClient.CloseOrAbortService();
            Assert.IsTrue(userSubNum == _numSubs, "userId=" + _userId + ", userSubNum =" + userSubNum + ", _numSubs =" + _numSubs);
        }
        /// <summary>
        /// Detta test tillsammans med Init testar: 
        /// Subscribe(_userId,Guid[] SubIDs)
        /// GetSubscriptionUsers(Guid subId)
        /// GetUserSubscriptions(_userId)
        /// Unsubscribe(_userId, unsubscribeId)
        /// UnsubscribeAll(_userId)
        /// </summary>
        [TestMethod]
        public void TestUnsubscribe()
        {
            this.TestSubscribe(); //lite fulhack men ...
            var unsubscribeId = existingSubIDs.First();
            _AdminClient = new AdminServiceClient();
            var testuser = _AdminClient.GetSubscriptionUsers(unsubscribeId).FirstOrDefault(user => user.Id == _userId);
            Assert.IsNotNull(testUser, "GetSubscriptionUsers: Testanvändaren ska vara prenumerant!");
            _AdminClient.Unsubscribe(_userId, unsubscribeId);
            var userSubNum = _AdminClient.GetUserSubscriptions(_userId);
            var nonExisting = userSubNum.Select(sub => sub.Id).ToList();
            Assert.IsFalse(nonExisting.Contains(unsubscribeId), "_AdminClient.Unsubscribe:Testaren har prenumerationen kvar");
            Assert.IsTrue(userSubNum.Count() == _numSubs - 1, "Testaren borde ha alla prenumerationen -minus den vi tog bort");
            _AdminClient.UnsubscribeAll(_userId);
            userSubNum = _AdminClient.GetUserSubscriptions(_userId);
            _AdminClient.CloseOrAbortService();
            Assert.IsTrue(userSubNum.Count() == 0, "Testaren borde ha 0 prenumerationen");
            
        }
       
        
        /// <summary>
        /// Tar bort testanvändaren och hens prenumerationer
        /// </summary>
        [TestCleanup]
        public void CleanUp()
        {
            _UserClient = new UserServiceClient();
            _UserClient.DeleteUser(_userId);
            _UserClient.CloseOrAbortService();
        }
    }
}
