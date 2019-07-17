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

    //public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
    //{
    //    var todoItem = await _context.TodoItems.FindAsync(id);

    //    if (todoItem == null)
    //    {
    //        return NotFound();
    //    }

    //    return todoItem;
    //}
    [Route("Admin/Subscriptions/{userId}")]
        [HttpGet]
        public async Task<IEnumerable<ApiSubscription>> GetUserSubscriptionsAsync(int userId)
        {
            return await Task.Run(() => _adminRepo.GetUserSubscriptions(userId));
            //return await new Task<_adminRepo.GetUserSubscriptions(userId)>;
            //throw new NotImplementedException();
            //return new List<Common.ApiSubscription>();
        }
        
        //IEnumerable<ApiUser> GetSubscriptionUsers(Guid subscriptionId);
        [Route("Admin/Users/{subscriptionId}")]
        [HttpGet]
        public async Task<IEnumerable<ApiUser>> GetSubscriptionUsersAsync(Guid subscriptionId)
        {
            return new List<Common.ApiUser>();
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
