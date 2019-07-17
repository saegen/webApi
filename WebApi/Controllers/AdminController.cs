using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using NLog;
using WebApi.Models;
using WebApi.SubscriptionService;
using WebApi.UserService;
using WebApi.AdminService;
namespace WebApi.Controllers
{
    public class AdminController : ApiController
    {
        private Logger log;
        private ISubscriptionRepo _subRepo;
        private IUserRepo _userRepo;
        private IAdminRepo _adminRepo;
        public AdminController()
        {
            log = LogManager.GetCurrentClassLogger();
            UserServiceClient userClient = new UserServiceClient();
            _userRepo = new UserRepo(userClient);
            SubscriptionServiceClient subClient = new SubscriptionServiceClient();
            _subRepo = new SubscriptionRepo(subClient);
            AdminServiceClient _adminClient = new AdminServiceClient();
            _adminRepo = new AdminRepo(_adminClient);
        }

  
        [Route("Admin/Subscriptions/{userId}")]
        [HttpGet]
        public async Task<IEnumerable<ApiSubscription>> GetUserSubscriptionsAsync(int userId)
        {
            return await Task.Run(() => _adminRepo.GetUserSubscriptions(userId));
        }
        
        //IEnumerable<ApiUser> GetSubscriptionUsers(Guid subscriptionId);
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
             await Task.Run(() => _adminRepo.Unsubscribe(userId,subscriptionId));
        }
        //void Unsubscribe(int userId, Guid subscriptionId);
        //void Subscribe(int userId, Guid[] subscriptionIds);
        //void UpdateUserSubscription(int userId, ApiSubscription subscriptionData);

        //[Route("Admin")]

        //public IEnumerable<ApiUser> GetUsers()
        //{
        //    log.Debug("WebApi GetUsers()");
        //    return repo.GetUsers();
        //}
    }
}
