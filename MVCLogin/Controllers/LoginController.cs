using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCLogin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Authorize(MVCLogin.User userModel)
        {
            using(LoginDataBaseEntities db=new LoginDataBaseEntities())
            {
                var userDetails=db.Users.Where(x=>x.UserName==userModel.UserName && x.Password==userModel.Password).FirstOrDefault();
                if (userDetails == null)
                {
                    userModel.LoginErrorMessage = "Wrong UserName or Password";
                    return View("Index", userModel);
                }
                else
                    Session["userId"] = userDetails.UserId;
                Session["username"] = userDetails.UserName;
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult LogOut()
        {
            int userId = (int)Session["userID"];
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}