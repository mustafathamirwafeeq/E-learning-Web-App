using ELearning.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ELearning.Models.Enum;
using System.IO;
using System.Diagnostics;

namespace ELearning.Repo
{
    public  class UserRepo
    {
        static UserRepo _instance;
        private UserRepo()
        {

        }
        public static UserRepo GetInstance()
        {
            if (_instance == null)
            {
                _instance = new UserRepo();
            }
            return _instance;
        }
        /// <summary>
        /// get user role by username
        /// </summary>
        public  string GetUserRole(string username)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var user = db.Users.Where(r => r.UserName == username).FirstOrDefault();
            var roleId = user.Roles.Where(u => u.UserId.Equals(user.Id)).FirstOrDefault().RoleId;
            return db.Roles.Where(r => r.Id.Equals(roleId)).FirstOrDefault().Name;

        }

        /// <summary>
        /// Update user role by user id
        /// </summary>
        public  string GetUserRole(int userId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var user = db.Users.Where(r => r.UserId == userId).FirstOrDefault();
            var roleId = user.Roles.Where(u => u.UserId.Equals(user.Id)).FirstOrDefault().RoleId;
            return db.Roles.Where(r => r.Id.Equals(roleId)).FirstOrDefault().Name;

        }

        /// <summary>
        /// Update user record
        /// </summary>
        public  string Update(UserViewModel model)
        {
            try
            {
                GreenLightFeaturesEntities db = new GreenLightFeaturesEntities();
                var user = db.AspNetUsers.FirstOrDefault(x => x.UserName == model.Username);

                user.FullName = model.FullName;
                user.Email = model.Email;
                user.DateOfBirth = model.DateOfBirth;
                user.Address = model.Address;
                user.PhoneNumber = model.PhoneNumber;
                user.Photo = model.Photo;
                user.IsAttendanceAuthorized = model.IsAttendanceAuthorized;
                user.Status = model.Status;
                user.CVPath = model.CVPath;
                db.SaveChanges();
                return "";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Add user record
        /// </summary>
        public  void Add(UserViewModel model)
        {
            Models.ApplicationDbContext context = new ApplicationDbContext();
            var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            ApplicationUser user = new ApplicationUser();

            user.FullName = model.FullName;
            user.Email = model.Email;
            user.DateOfBirth = model.DateOfBirth;
            user.Address = model.Address;
            user.PhoneNumber = model.PhoneNumber;
            user.Photo = model.Photo;

            userMgr.UpdateAsync(user);

            //var ctx = store.context;
            //ctx.saveChanges();

        }

        /// <summary>
        /// Delete user record
        /// </summary>
        public  void Delete(string username)
        {
            Models.ApplicationDbContext context = new ApplicationDbContext();
            var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            ApplicationUser user = userMgr.FindByName(username);

            userMgr.Delete(user);
            //var ctx = store.context;
            //ctx.saveChanges();

        }

        /// <summary>
        /// Return users list
        /// </summary>
        public  List<UserViewModel> GetUsers()
        {
            List<UserViewModel> users = new List<UserViewModel>();

            string roleId = string.Empty;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var _users = db.Users.OrderByDescending(r => r.UserId).ToList();
                var _roles = db.Roles.ToList(); 
                var _adminRoleId = _roles.Find(r=>r.Name.Equals("Admin")).Id;
                var _deletedRoleId = _roles.Find(r => r.Name.Equals("Visitor")).Id;

                foreach (var u in _users)
                {
                    roleId = u.Roles.Where(r => r.UserId.Equals(u.Id)).FirstOrDefault().RoleId;
                    if (!(roleId.Equals(_adminRoleId) || roleId.Equals(_deletedRoleId)))
                    {
                        users.Add(new UserViewModel()
                        {
                            FullName = u.FullName,
                            Username = u.UserName,
                            UserId = u.UserId,
                            Email = u.Email,
                            Address = u.Address,
                            DateOfBirth = u.DateOfBirth,
                            PhoneNumber = u.PhoneNumber,
                            Photo = u.Photo,
                            Role = _roles.Find(r => r.Id.Equals(roleId)).Name
                        });
                    }
                }
            }
            return users;
        }

        /// <summary>
        /// Return existing user record
        /// </summary>
        public  UserViewModel GetUser(int userId)
        {
            UserViewModel user = new UserViewModel();

            string roleId = string.Empty;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var _u = db.Users.Where(u => u.UserId == userId).FirstOrDefault();
                var _roles = db.Roles.ToList();

                roleId = _u.Roles.Where(r => r.UserId.Equals(_u.Id)).FirstOrDefault().RoleId;

                user.FullName = _u.FullName;
                user.Username = _u.UserName;
                user.UserId = _u.UserId;
                user.Email = _u.Email;
                user.Address = _u.Address;
                user.DateOfBirth = _u.DateOfBirth;
                user.PhoneNumber = _u.PhoneNumber;
                user.Photo = _u.Photo;
                user.Role = _roles.Find(r => r.Id.Equals(roleId)).Name;
                user.IsAttendanceAuthorized = _u.IsAttendanceAuthorized;
                user.Status = _u.Status;
                user.CVPath = _u.CVPath;
            }
            return user;
        }

        /// <summary>
        /// Check if user is in the provided role
        /// </summary>
        public  bool IsUserInCourse(int userId, int courseId)
        {
            int count = 0 ;
            using (GreenLightFeaturesEntities db = new GreenLightFeaturesEntities())
            {
                count = db.CourseClass.Where(r => r.StudentIds.Contains(userId.ToString()) && r.CourseId == courseId).Count();
            }

            return count > 0;
        }

        /// <summary>
        /// Return parameterized users list
        /// </summary>
        public  List<UserViewModel> GetUsersByRole(string roleName)
        {
            List<UserViewModel> users = new List<UserViewModel>();

            string roleId = string.Empty;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var _users = db.Users.OrderByDescending(r => r.UserId).ToList();
                var _roles = db.Roles.ToList();
                var _roleId = _roles.Find(r => r.Name.Equals("Moderator")).Id;


                foreach (var u in _users)
                {
                    roleId = u.Roles.Where(r => r.UserId.Equals(u.Id)).FirstOrDefault().RoleId;
                    if (roleId.Equals(_roleId))
                    {
                        users.Add(new UserViewModel()
                        {
                            FullName = u.FullName,
                            Username = u.UserName,
                            UserId = u.UserId,
                            Email = u.Email,
                            Address = u.Address,
                            DateOfBirth = u.DateOfBirth,
                            PhoneNumber = u.PhoneNumber,
                            Photo = u.Photo,
                            Role = _roles.Find(r => r.Id.Equals(roleId)).Name
                        });
                    }
                }
            }
            return users;
        }

    }
}