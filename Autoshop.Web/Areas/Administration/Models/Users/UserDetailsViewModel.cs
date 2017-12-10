namespace Autoshop.Web.Areas.Administration.Models.Users
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;

    public class UserDetailsViewModel : UserListingViewModel
    {
        public IEnumerable<string> UserRoles { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
