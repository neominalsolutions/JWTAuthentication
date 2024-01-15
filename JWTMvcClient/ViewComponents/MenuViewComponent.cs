using JWTMvcClient.Models;
using Microsoft.AspNetCore.Mvc;

namespace JWTMvcClient.ViewComponents
{
  public class MenuViewComponent:ViewComponent
  {
    // Db bağlantı işlemleri

    List<MenuVM> menus = new List<MenuVM>();

    public async Task<IViewComponentResult> InvokeAsync()
    {
      menus.Add(new MenuVM { ActionName = "Index", ControllerName = "Home", Name = "Anasayfa", AreaName = "" });
      menus.Add(new MenuVM { ActionName = "Privacy", ControllerName = "Home", Name = "Privacy", AreaName = "" });
      menus.Add(new MenuVM { ActionName = "Index", ControllerName = "Home", Name = "Admin", AreaName = "Admin" });

      var model = await Task.FromResult(menus);

      return View(model);

    }

  }
}
