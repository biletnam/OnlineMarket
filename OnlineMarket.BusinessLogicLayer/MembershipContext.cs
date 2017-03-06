using OnlineMarket.DataAccessLayer.Entities;
using System.Security.Principal;

namespace OnlineMarket.BusinessLogicLayer
{
    public class MembershipContext
    {
        public IPrincipal Principal { get; set; }

        public User User { get; set; }

        public bool IsValid()
        {
            return Principal != null;
        }
    }
}
