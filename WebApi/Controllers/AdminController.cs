using Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using NLog;
using WebApi.Models;
using WebApi.AdminService;
namespace WebApi.Controllers
{
    public class AdminController : ApiController
    {
        private Logger log;
        private IAdminRepo _adminRepo;
        public AdminController()
        {
            log = LogManager.GetCurrentClassLogger();
            AdminServiceClient _adminClient = new AdminServiceClient();
            _adminRepo = new AdminRepo(_adminClient);
        }

        [Route("Admin/Subscriptions/{userId}")]
        [HttpGet]
        public async Task<IEnumerable<ApiSubscription>> GetUserSubscriptionsAsync(int userId)
        {
            return await Task.Run(() => _adminRepo.GetUserSubscriptions(userId));
        }

        [Route("Admin/Users/{subscriptionId}")]
        [HttpGet]
        public async Task<IEnumerable<ApiUser>> GetSubscriptionUsersAsync(Guid subscriptionId)
        {
            return await Task.Run(() => _adminRepo.GetSubscriptionUsers(subscriptionId));
        }

        [Route("Admin")]
        [HttpDelete]
        public async Task UnsubscribeAsync(int userId, Guid subscriptionId)
        {
            await Task.Run(() => _adminRepo.Unsubscribe(userId, subscriptionId));
        }

        [Route("Admin")]
        [HttpPost]
        public async Task SubscribeAsync(int userId, [FromUri] Guid[] subscriptionIds)
        {
            await Task.Run(() => _adminRepo.Subscribe(userId, subscriptionIds));
        }       
    }
}
