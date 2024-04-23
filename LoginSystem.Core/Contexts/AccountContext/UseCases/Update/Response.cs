using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginSystem.Core.Contexts.AccountContext.UseCases.Update
{
    public class Response : SharedContext.UseCases.Response
    {
        protected Response()
        {

        }

        public Response(string message, int status, IEnumerable<Notification>? notifications = null)
        {
            Message = message;
            Status = status;
            Notifications = notifications;
        }
        public ResponseData? Data { get; set; }

        public record ResponseData(Guid Id, string Name, string Email);

    }
}
