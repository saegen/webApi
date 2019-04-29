using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;
using Common;
//using WebApi.UserServiceReference;
using WebApi.SubscriptionService;
//using WebApi.UserServiceReference;

namespace WebApi.Controllers
{
    public class SubscriptionsController : ApiController
    {
        private ISubscriptionRepo repo;
        public SubscriptionsController()
        {
            SubscriptionServiceClient client = new SubscriptionServiceClient();
            repo = new SubscriptionRepo(client);
        }
        //Get all subscriptions (GET -> /subscriptions)
        //todo: Gör om till Ayncron och Fixa den globala IExceptionHandler istället för try/catch
        public IEnumerable<ApiSubscription> Get()
        {
            try
            {
                return repo.GetSubscriptions();
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }      
        }

        //Get current subscription (GET -> /subscriptions/some-url-friendly-identifier)ie userId, borde vara Name, tex spiderman
        public ApiSubscription Get(Guid subscriptionId)
        {
            try
            {
                var sub = repo.GetSubscription(subscriptionId);
                if (sub == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                return sub;
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        //Create subscription (POST -> /subscriptions) -> Deprecated ;)
        //Create subscription (POST -> /subscriptions)
        public HttpResponseMessage Post(ApiSubscription subdata)
        {
            if (subdata == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, new ArgumentNullException("subdata"));
            }
            try
            {

                var saved = repo.CreateSubscription(subdata);
                var response = Request.CreateResponse<ApiSubscription>(HttpStatusCode.OK, saved);
                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.GetBaseException());
            }
            
        }

        //Update subscription data such  (PUT -> /subscriptions/some-url-friendly-identifier)
        //id is redundant because I update the subscription directly from subdata
        public HttpResponseMessage Put(ApiSubscription subdata)
        {
            if (subdata == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, new ArgumentNullException("subdata"));
            }
            try
            {
                //could have,should have used repo.AddUserSubscription(id, subdata) but I didn't. (Because I would like feedback on best approach) 
                var saved = repo.UpdateSubscription(subdata);
                if (saved == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NoContent, subdata.Id.ToString());
                }
                return Request.CreateResponse(HttpStatusCode.OK, saved);    
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.GetBaseException());      
            }
        }
        
        //Delete subscription (DELETE -> /subscriptions/some-url-friendly-identifier)
        public HttpResponseMessage Delete(int id, ApiSubscription subdata)
        {
            try
            {
                repo.DeleteSubscription(subdata.Id);
                return Request.CreateResponse(HttpStatusCode.OK,subdata);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.GetBaseException());
            }
        }
    }
}
