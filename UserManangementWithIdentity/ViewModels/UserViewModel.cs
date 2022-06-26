using System.Collections;
using System.Collections.Generic;

namespace UserManangementWithIdentity.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string FirstNmae { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
