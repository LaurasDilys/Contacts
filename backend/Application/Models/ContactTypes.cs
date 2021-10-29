using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class ContactTypes
    {
        public const string Other = "OTHER"; // User's created contacts that aren't shared with anyone
        public const string Shared = "SHARED";
        public const string Received = "RECEIVED"; // Received and accepted
        public const string Unaccepted = "UNACCEPTED"; // Received, but not accepted
    }
}
