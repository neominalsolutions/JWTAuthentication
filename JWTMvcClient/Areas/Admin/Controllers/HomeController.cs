﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTMvcClient.Areas.Admin.Controllers
{
  [Area("Admin")]
  public class HomeController : Controller
  {

    [Authorize]
    public IActionResult Index()
    {
      return View();
    }
  }
}
