using System;
using Microsoft.AspNetCore.Mvc;

namespace ToDoAPI.Controllers
{
    public class ApIBaseController : Controller
    {
        protected Guid UserId => User?.Identity?.IsAuthenticated == true ?
            Guid.Parse(User.Identity.Name) :
            Guid.Empty;
    }
}