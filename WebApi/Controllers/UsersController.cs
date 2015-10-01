using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
// Get all users (GET -> /users)
//Get current user (GET -> /users/some-url-friendly-identifier)
//Create user (POST -> /users)
//Add subscription to user (PUT -> /users/subscriptionId)
//Delete user (DELETE -> /users/some-url-friendly-identifier)

    public class UsersController : ApiController
    {
        private IRepository repo;
        public UsersController()
        {
            Service1Client client = new Service1Client();
            repo = new ServiceRepository(client);
        }
        //some-url-friendly-identifier = int 
        //Get current user (GET -> /users/some-url-friendly-identifier)
        public ApiUser Get(int id)
        {
            var user =  repo.GetUser(id);
            if (user == null)
	        {	
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return user;
           
        }
           
        //Get all users (GET -> /users)
        public IEnumerable<ApiUser> Get()
        {
            return repo.GetUsers();
        }   
        
        public HttpResponseMessage PostUser([FromBody]ApiUser user)
        {
            try
            {
                int userId = repo.AddUser(user);
                return Request.CreateResponse(HttpStatusCode.Created, "User id: " + userId.ToString());
            }
            catch (Exception ex)
            {
               return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //Add subscription to user (PUT -> /users/subscriptionId) spec changed to:
        //Add subscription to user (PUT -> /users/userId) (major violation of spec)
        public HttpResponseMessage Put(int id, ApiSubscription subdata)
        {
            try
            {
                if (subdata == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, new ArgumentNullException("subdata"));
                }
                //get CurrentUser instead
                var user = repo.GetUser(id);
                if (user == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NoContent,"No user with id: " +  id.ToString());
                }

                var replySub = repo.AddUserSubscription(id, subdata);
                return Request.CreateResponse<ApiSubscription>(HttpStatusCode.OK, replySub);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.GetBaseException());
            }
            
        }

        //Delete user (DELETE -> /users/some-url-friendly-identifier)
        //Foreign Key with cascade on delete also deletes all of the users subscriptions
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                repo.DeleteUser(id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}