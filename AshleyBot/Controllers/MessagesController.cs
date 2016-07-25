using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using System.Xml;

namespace CampsiteBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                // get state code
                int length = (activity.Text ?? string.Empty).Length;

                if (length == 2)
                {
                    string stateCode = (activity.Text ?? string.Empty);
                    Activity reply = activity.CreateReply($"Looking for campsites in {stateCode}" );
                    await connector.Conversations.ReplyToActivityAsync(reply);
                }
                else
                {
                    Activity reply = activity.CreateReply($"Im sorry but I can't find that state. Please use the two letter state code. Example: Colorado = CO.");
                    await connector.Conversations.ReplyToActivityAsync(reply);

                }

            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }

       /* static public string ProcessResponse(response)
        {
            //Create namespace manager
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(response.NameTable);
            nsmgr.AddNamespace("rest", "http://schemas.microsoft.com/search/local/ws/rest/v1");

            //Get all facilities in the state
            XmlNodeList locationElements = response.SelectNodes("//rest:facilityName", nsmgr);
            foreach (XmlNode location in locationElements)
            {
                string facility = location.InnerText;
                string campsites = facility;
                return campsites;
            }

            if (campsites = null)
            {

            }

        }*/
    }


}