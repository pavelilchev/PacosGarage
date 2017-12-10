namespace Autoshop.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static Autoshop.Common.Constants.CommonConstants;

    [Area(Administration)]
    [Authorize(Roles = Admin)]
    public class BaseAdminController : Controller
    {
    }
}
