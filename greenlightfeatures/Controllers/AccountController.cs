using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Owin;
using ELearning.Models;
using System.Web.Security;
using System.Configuration;
using System.Data.Entity;
using ELearning.Repo;
using System.IO;

namespace ELearning.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AccountController : Controller
    {
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.Username, model.Password);
                if (user != null)
                {
                    if (user.Status)
                    {
                        if (UserRepo.GetInstance().GetUserRole(model.Username) != "Visitor")
                        {
                            await SignInAsync(user, model.RememberMe);
                            //return RedirectToLocal(returnUrl);
                            return RedirectToActionPermanent("Dashboard", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError("", "The user either does not exist or is not confirmed.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "The user account is not active.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        //[Authorize(Roles = "Admin")]
        public ActionResult Register()
        {
            if (UserManager.Users.Count() > 0)
                return RedirectToAction("Login", "Account");

            return View();
        }

        //
        // POST: /Account/Register [student only]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    FullName = model.FullName,
                    UserName = model.Username,
                    Email = model.Email,
                    DateOfBirth = model.DateOfBirth,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                    Photo = model.Photo,
                    EmailConfirmed = true,
                    IsAttendanceAuthorized = false,
                    Status = true
                };
                if (UserManager.Users.Count() == 0)
                {

                    // Adding System Data
                    var roleManager = new RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
                    roleManager.Create(new IdentityRole("Admin"));
                    roleManager.Create(new IdentityRole("Management"));
                    roleManager.Create(new IdentityRole("Moderator"));
                    roleManager.Create(new IdentityRole("Student"));
                    roleManager.Create(new IdentityRole("Visitor"));

                    try
                    {
                        GreenLightFeaturesEntities db = new GreenLightFeaturesEntities();
                        var testUser1 = new ApplicationUser()
                        {
                            FullName = "Student 1",
                            UserName = "student1",
                            Email = "student1@email.com",
                            DateOfBirth = model.DateOfBirth,
                            Address = model.Address,
                            PhoneNumber = model.PhoneNumber,
                            Photo = model.Photo,
                            EmailConfirmed = true,
                            Status = true
                        };
                        await UserManager.CreateAsync(testUser1, model.Password);
                        UserManager.AddToRole(testUser1.Id, "Student");

                        var testUser2 = new ApplicationUser()
                        {
                            FullName = "Student 2",
                            UserName = "student2",
                            Email = "student2@email.com",
                            DateOfBirth = model.DateOfBirth,
                            Address = model.Address,
                            PhoneNumber = model.PhoneNumber,
                            Photo = model.Photo,
                            EmailConfirmed = true,
                            Status = true
                        };
                        await UserManager.CreateAsync(testUser2, model.Password);
                        UserManager.AddToRole(testUser2.Id, "Student");


                        var testUser3 = new ApplicationUser()
                        {
                            FullName = "Student 3",
                            UserName = "student3",
                            Email = "student3@email.com",
                            DateOfBirth = model.DateOfBirth,
                            Address = model.Address,
                            PhoneNumber = model.PhoneNumber,
                            Photo = model.Photo,
                            EmailConfirmed = true,
                            Status = true
                        };
                        await UserManager.CreateAsync(testUser3, model.Password);
                        UserManager.AddToRole(testUser3.Id, "Student");


                        var testUser4 = new ApplicationUser()
                        {
                            FullName = "Student 4",
                            UserName = "student4",
                            Email = "student4@email.com",
                            DateOfBirth = model.DateOfBirth,
                            Address = model.Address,
                            PhoneNumber = model.PhoneNumber,
                            Photo = model.Photo,
                            EmailConfirmed = true,
                            Status = true
                        };
                        await UserManager.CreateAsync(testUser4, model.Password);
                        UserManager.AddToRole(testUser4.Id, "Student");


                        var testUser5 = new ApplicationUser()
                        {
                            FullName = "Student 5",
                            UserName = "student5",
                            Email = "student5@email.com",
                            DateOfBirth = model.DateOfBirth,
                            Address = model.Address,
                            PhoneNumber = model.PhoneNumber,
                            Photo = model.Photo,
                            EmailConfirmed = true,
                            Status = true
                        };
                        await UserManager.CreateAsync(testUser5, model.Password);
                        UserManager.AddToRole(testUser5.Id, "Student");

                        var testUser6 = new ApplicationUser()
                        {
                            FullName = "Instructor 1",
                            UserName = "instructor1",
                            Email = "instructor1@email.com",
                            DateOfBirth = model.DateOfBirth,
                            Address = model.Address,
                            PhoneNumber = model.PhoneNumber,
                            Photo = model.Photo,
                            EmailConfirmed = true,
                            Status = true
                        };
                        await UserManager.CreateAsync(testUser6, model.Password);
                        UserManager.AddToRole(testUser6.Id, "Moderator");

                        var testUser7 = new ApplicationUser()
                        {
                            FullName = "Instructor 2",
                            UserName = "instructor2",
                            Email = "instructor2@email.com",
                            DateOfBirth = model.DateOfBirth,
                            Address = model.Address,
                            PhoneNumber = model.PhoneNumber,
                            Photo = model.Photo,
                            EmailConfirmed = true,
                            Status = true
                        };
                        await UserManager.CreateAsync(testUser7, model.Password);
                        UserManager.AddToRole(testUser7.Id, "Moderator");

                        var testUser8 = new ApplicationUser()
                        {
                            FullName = "Instructor 3",
                            UserName = "instructor3",
                            Email = "instructor3@email.com",
                            DateOfBirth = model.DateOfBirth,
                            Address = model.Address,
                            PhoneNumber = model.PhoneNumber,
                            Photo = model.Photo,
                            EmailConfirmed = true,
                            Status = true
                        };
                        await UserManager.CreateAsync(testUser8, model.Password);
                        UserManager.AddToRole(testUser8.Id, "Moderator");


                        TrainingBase base1 = new TrainingBase { Address = "test address", ContactDetail = "contact person", Name = "Base1" };
                        TrainingBase base2 = new TrainingBase { Address = "test address", ContactDetail = "contact person", Name = "Base2" };
                        TrainingBase base3 = new TrainingBase { Address = "test address", ContactDetail = "contact person", Name = "Base3" };
                        db.TrainingBase.Add(base1);
                        db.TrainingBase.Add(base2);
                        db.TrainingBase.Add(base3);
                        Course crs1 = new Course { Title = "course1", Description = "", IsPublished = true, IsDeleted = false };
                        Course crs2 = new Course { Title = "course2", Description = "", IsPublished = true, IsDeleted = false };
                        Course crs3 = new Course { Title = "course3", Description = "", IsPublished = true, IsDeleted = false };
                        db.Course.Add(crs1);
                        db.Course.Add(crs2);
                        db.Course.Add(crs3);
                        CMSPage page1 = new CMSPage { Title = "Home/Welcome", Body = "Welcome text" };
                        CMSPage page2 = new CMSPage { Title = "About Us", Body = "Aboutus text" };
                        CMSPage page3 = new CMSPage { Title = "Contact Us", Body = "Contactus text" };
                        db.CMSPage.Add(page1);
                        db.CMSPage.Add(page2);
                        db.CMSPage.Add(page3);

                        db.SaveChanges();
                    }
                    catch { }
                }

                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    result = UserManager.AddToRole(user.Id, "Admin");
                    if (result.Succeeded)
                    {
                        await SignInAsync(user, isPersistent: true);

                        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                        // Send an email with this link
                        //string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        //await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                        GreenLightFeaturesEntities db = new GreenLightFeaturesEntities();
                        var users = db.AspNetUsers.FirstOrDefault(x=> x.UserId == user.UserId);
                        users.Status = true;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
                else
                {
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult EditUser(int Id)
        {
            return View(UserRepo.GetInstance().GetUser(Id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult EditUser(UserViewModel model, FormCollection form, HttpPostedFileBase fileuploadCV)
        {
            ModelState.Remove("fileuploadCV");
            if (ModelState.IsValid)
            {
                if(fileuploadCV != null)
                {
                    string path = Server.MapPath("~/CVs/");
                    if(!System.IO.Directory.Exists(path))
                        System.IO.Directory.CreateDirectory(path);
                    fileuploadCV.SaveAs(path + fileuploadCV.FileName);
                    model.CVPath = fileuploadCV.FileName;
                }
                string result = UserRepo.GetInstance().Update(model);
                if (string.IsNullOrEmpty(result))
                    return RedirectToAction("ManageUsers");
                else
                {
                    ModelState.AddModelError("", result);
                }
            }

            return View(UserRepo.GetInstance().GetUser(model.UserId));
        }


        //
        // GET: /Account/CreateAccount [student & course moderators]

        //[Authorize(Roles="Admin")]
        public ActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAccount(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    FullName = model.FullName,
                    UserName = model.Username,
                    Email = model.Email,
                    DateOfBirth = model.DateOfBirth,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                    Photo = model.Photo,
                    EmailConfirmed = true,
                    Status = true
                };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    UserManager.AddToRole(user.Id, model.Role);


                    if (result.Succeeded)
                    {
                        //await SignInAsync(user, isPersistent: false);

                        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                        // Send an email with this link
                        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        //await UserManager.SendEmailAsync(
                        //    user.Id, 
                        //    ConfigurationManager.AppSettings["UserWelcome"] ,
                        //    NewUserCredientialsMailBody(user.FullName,user.UserName, model.Password));

                        return RedirectToAction("ManageUsers");
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
                else
                {
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }

            IdentityResult result = await UserManager.ConfirmEmailAsync(userId, code);
            if (result.Succeeded)
            {
                return View("ConfirmEmail");
            }
            else
            {
                AddErrors(result);
                return View();
            }
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var user = await UserManager.FindByEmailAsync(model.Email);
                    if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                    {
                        ModelState.AddModelError("", "The user either does not exist or is not confirmed.");
                        return View();
                    }

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                    var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                    return RedirectToAction("ForgotPasswordConfirmation", "Account");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }

            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            if (code == null)
            {
                return View("Error");
            }
            return View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Username);
                if (user == null)
                {
                    ModelState.AddModelError("", "No user found.");
                    return View();
                }
                IdentityResult result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("ResetPasswordConfirmation", "Account");
                }
                else
                {
                    AddErrors(result);
                    return View();
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/Disassociate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            ManageMessageId? message = null;
            IdentityResult result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                await SignInAsync(user, isPersistent: false);
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("Manage", new { Message = message });
        }

        //
        // GET: /Account/Manage
        [AllowAnonymous]
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        public ActionResult ManageUsers()
        {
            return View(UserRepo.GetInstance().GetUsers());
        }

        public async Task<List<string>> GetRolesForUser(string userId)
        {
            IList<string> rolesForUser;
            using (
                var userManager =
                    new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
            {
                rolesForUser = await userManager.GetRolesAsync(userId);


            }
            return rolesForUser.ToList();
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                        await SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var user = await UserManager.FindAsync(loginInfo.Login);
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // If the user does not have an account, then prompt the user to create an account
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }

        //
        // GET: /Account/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
            }
            IdentityResult result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            if (result.Succeeded)
            {
                return RedirectToAction("Manage");
            }
            return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };
                IdentityResult result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInAsync(user, isPersistent: false);

                        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                        // Send an email with this link
                        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        // SendEmail(user.Email, callbackUrl, "Confirm your account", "Please confirm your account by clicking this link");

                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private void SendEmail(string email, string callbackUrl, string subject, string message)
        {
            // For information on sending mail, please visit http://go.microsoft.com/fwlink/?LinkID=320771
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion

        /// <summary>
        /// Generate email body content while creating new use
        /// </summary>
        /// <returns></returns>
        private string NewUserCredientialsMailBody(string fullName, string username, string password)
        {
            var reader = new StreamReader(Server.MapPath(ConfigurationManager.AppSettings["NewUserCredientialPath"]));
            string body = reader.ReadToEnd();
            reader.Close();
            reader.Dispose();
            body = body.Replace("<FullName>", fullName);
            body = body.Replace("<Username>", username);
            body = body.Replace("<password>", password);
            body = body.Replace("<regards>", ConfigurationManager.AppSettings["RegardsText"]);
            return body;
        }

        public JsonResult DeleteAccount(string roleName, string userName)
        {
            bool res = false;
            //IdentityResult IdRoleResult;

            //var context = new ApplicationDbContext();
            //var roleStore = new RoleStore<IdentityRole>(context);

            //var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            //var roleMgr = new RoleManager<IdentityRole>(roleStore);
            //if (!roleMgr.RoleExists("Deleted"))
            //{
            //    IdRoleResult = roleMgr.Create(new IdentityRole { Name = "Deleted" });
            //}

            try
            {
                var user = UserManager.FindByName(userName);
                UserManager.RemoveFromRole(user.Id, roleName);
                UserManager.AddToRole(user.Id, "Visitor");

                user.EmailConfirmed = false;
                UserManager.Update(user);

                //using (GreenLightFeaturesEntities db = new GreenLightFeaturesEntities())
                //{
                //    // delete exam questions
                //    var equestions = db.Evaluations.Where(r => r.StudentId == userId).ToList();
                //    db.Evaluations.RemoveRange(equestions);
                //    //db.SaveChanges();

                //    // delete evalutions
                //    var evalutions = db.Evaluations.Where(r => r.StudentId == userId).ToList();
                //    db.Evaluations.RemoveRange(evalutions);
                //    //db.SaveChanges();

                //    // delete exams
                //    var exams = db.Exams.Where(r => r.UserId == userId).ToList();
                //    db.Exams.RemoveRange(exams);
                //    //db.SaveChanges();

                //    // delete forum question
                //    var fquestions = db.ForumQuestions.Where(r => r.UserId == userId).ToList();
                //    db.ForumQuestions.RemoveRange(fquestions);
                //    //db.SaveChanges();

                //    // delete forum replies
                //    var freplies = db.ForumReplies.Where(r => r.UserId == userId).ToList();
                //    db.ForumReplies.RemoveRange(freplies);
                //    //db.SaveChanges();

                //    // delete forum
                //    var forum = db.Forums.Where(r => r.UserId == userId).ToList();
                //    db.Forums.RemoveRange(forum);
                //    //db.SaveChanges();

                //    // delete questions
                //    var questions = db.Questions.Where(r => r.UserId == userId).ToList();
                //    db.Questions.RemoveRange(questions);
                //    //db.SaveChanges();

                //    // delete courses
                //    var courses = db.Courses.Where(r => r.ModeratorId == userId).ToList();
                //    db.Courses.RemoveRange(courses);
                //    //db.SaveChanges();

                //    //
                //    db.SaveChanges();
                //    

                //}

                res = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (res)
                return Json(new { status = "0", data = "User has been deleted" }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = "1", data = "Please contact system administrator" }, JsonRequestBehavior.AllowGet);
        }
    }
}