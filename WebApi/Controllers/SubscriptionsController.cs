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
        //GetSubscriptionById all subscriptions (GET -> /subscriptions)
        [Route("Subscriptions")]
        [HttpGet]
        public IEnumerable<ApiSubscription> GetSubscriptions()
        {
            try
            {
                return repo.GetSubscriptions();
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage() { StatusCode = HttpStatusCode.InternalServerError, ReasonPhrase=ex.GetBaseException().Message});
            }      
        }

        //GetSubscriptionById current subscription (GET -> /subscriptions/some-url-friendly-identifier)ie userId, borde vara Name, tex spiderman
        [Route("Subscriptions/{subscriptionId}")]
        [HttpGet]
        public ApiSubscription GetSubscriptionById(Guid subscriptionId)
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
        [Route("Subscriptions")]
        [HttpPost]
        public HttpResponseMessage CreateSubscription(ApiSubscription subdata)
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
        [Route("Subscriptions")]
        [HttpPut]
        public HttpResponseMessage UpdateSubscription(ApiSubscription subdata)
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
        [Route("Subscriptions")]
        [HttpDelete]
        public HttpResponseMessage DeleteSubscription(Guid subId)
        {
            try
            {
                repo.DeleteSubscription(subId);
                return Request.CreateResponse(HttpStatusCode.OK,subId);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.GetBaseException());
            }
        }
    }
}
