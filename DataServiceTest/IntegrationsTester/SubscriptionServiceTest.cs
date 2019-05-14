using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataServiceTest.IntegrationsTester
{
    using DataServiceTest.SubscriptionService;
    
    [TestClass]
    public class SubscriptionServiceTest
    {
        private SubscriptionServiceClient _client = null;
        private CreateSubscriptionDTO _testSub = null;
        private ApiSubscription _testApiSub = null;
        private Guid _subId = Guid.Empty;

        [TestMethod]
        public void TestSubscriptionService()
        {
            _client = new SubscriptionServiceClient();
            _testSub = new CreateSubscriptionDTO() { Name="UnitTest Sub", CallMinutes=999,Price=55.89m };
            int oldCount = -1, newCount = -1;
            
            try
            {
                oldCount = _client.GetSubscriptions().GetUpperBound(0);
                //skapa0
                _testApiSub = _client.CreateSubscription(_testSub);
                newCount = _client.GetSubscriptions().GetUpperBound(0);
                Assert.AreEqual(oldCount + 1, newCount, "Det saknas en prenumeration!");
                Assert.IsNotNull(_testApiSub, "CreateSubscription svarade inte!");
                Assert.IsTrue(_testApiSub.Id != Guid.Empty, "Id är Guid.Empty!");
                _subId = _testApiSub.Id;
                //Hämta
                var fromDb = _client.GetSubscription(_subId);
                Assert.IsNotNull(fromDb, "Kunde inte hitta prenumerationen !med id = {0}", _subId);
                //uppdatera
                fromDb.Name = "Testprenumeration";
                fromDb.CallMinutes = 10;
                fromDb.Price = 10;
                ApiSubscription updSub = new ApiSubscription() {
                    Id = fromDb.Id,
                    CallMinutes = fromDb.CallMinutes,
                    Price = fromDb.Price };
                _testApiSub = _client.UpdateSubscription(fromDb);
                Assert.IsTrue(_testApiSub.Name == "Testprenumeration", "Namnet har inte uppdaterats!");
                Assert.IsTrue(_testApiSub.Price == 10, "Priset har inte uppdaterats!");
                Assert.IsTrue(_testApiSub.Id == _subId, "Fel Id!");
                _client.DeleteSubscription(_subId);
                var deletedUser = _client.GetSubscription(_subId);
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
