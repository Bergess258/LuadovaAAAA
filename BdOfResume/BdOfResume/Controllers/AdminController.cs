using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.CodeDom.Compiler;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BdOfResume.Models;
using System.Net;
using System.Net.Mail;
using Task = BdOfResume.Models.Task;
using System.Data.SqlClient;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace BdOfResume.Controllers
{
    [Authorize]
    public class AdminController : Controller,IDisposable
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        CoursEntities context = new CoursEntities();
        // GET: Resume
        public AdminController()
        {
            
        }
        public AdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
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
        [Authorize(Roles = "A")]
        public ActionResult AdminAccView()
        {
            AccInfo model = new AccInfo();
            string userId = User.Identity.GetUserId();
            string s = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=aspnet-BdOfResume-20190523124105;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection con = new SqlConnection(s))
            {
                con.Open();
                string SqlString = "SELECT UserName,IdEmp FROM [AspNetUsers] WHERE Id = @My";
                using (SqlCommand com = new SqlCommand(SqlString, con))
                {
                    com.Parameters.AddWithValue("@My", userId);
                    using (SqlDataReader reader = com.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            model.Login = reader["UserName"].ToString();
                            model.emp = context.Employee.Find(Convert.ToInt32(reader["IdEmp"]));
                        }
                            
                    }
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> AdminAccView(AccInfo model)
        {
            if (model.OldPassword!=null && model.NewPassword!=null){
                var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                }
                AddErrors(result);
            }
            context.Entry(model.emp).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
            return View(model);
        }
        [Authorize(Roles = "A")]
        public ActionResult AddResume()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddResume(RegisterViewModel model)
        {
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "A,N")]
        public ActionResult AddProject()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddProject(Project pro)
        {
            context.Project.Add(pro);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "A,N")]
        public ActionResult Catalog()
        {
            BdCount temp = new BdCount();
            temp.DepCount = context.Department.Count();
            temp.EduCount = context.Education.Count();
            temp.PostCount = context.Post.Count();
            temp.RoleCount = context.Role.Count();
            temp.SkillCount = context.Skill.Count();
            temp.SpecialityCount = context.Speciality.Count();
            temp.TaskCount = context.Task.Count();
            temp.UnivCount = context.University.Count();
            return View(temp);
        }

        [Authorize(Roles = "K")]
        public ActionResult EditResume(int id)
        {
            return View(context.Employee.Find(id));
        }
        
        [HttpPost]
        public ActionResult EditResume(Employee model,int id)
        {
            context.Entry(model).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "N")]
        public ActionResult DeleteResume(int id)
        {
            Employee emp = new Employee() { Id = id };
            context.Entry(emp).State = System.Data.Entity.EntityState.Deleted;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "A")]
        public ActionResult CatalogSelected(string catalogName)
        {
            switch (catalogName)
            {
                case "Образования":
                    return View(new SelectedCat(context.Education.ToList(),catalogName));
                case "Департаменты":
                    return View(new SelectedCat(context.Department.ToList(),catalogName));
                case "Посты сотрудников":
                    return View(new SelectedCat(context.Post.ToList(), catalogName));
                case "Роли в проектах":
                    return View(new SelectedCat(context.Role.ToList(), catalogName));
                case "Навыки":
                    return View(new SelectedCat(context.Skill.ToList(), catalogName));
                case "Специальности":
                    return View(new SelectedCat(context.Speciality.ToList(), catalogName));
                case "Университеты":
                    return View(new SelectedCat(context.University.ToList(), catalogName));
                default:
                    return View(new SelectedCat(context.Task.ToList(), catalogName));
            }
        }
        [HttpPost]
        public ActionResult CatalogSelected(string addedName,string curCatalog,int id=-1)
        {
            if (id == -1)
            {
                if (string.IsNullOrEmpty(addedName))
                {
                    ModelState.AddModelError("addedName", "Пустое значение при добавлении");
                }
                else
                    if (addedName.Length < 3)
                {
                    ModelState.AddModelError("addedName", "Недопустимая длина строки");
                }
                if (ModelState.IsValid)
                    switch (curCatalog)
                    {
                        case "Департаменты":
                            context.Department.Add(new Department() { Name = addedName });
                            context.SaveChanges();
                            return View(new SelectedCat(context.Department.ToList(), curCatalog));
                        case "Образования":
                            context.Education.Add(new Education() { Name = addedName });
                            context.SaveChanges();
                            return View(new SelectedCat(context.Education.ToList(), curCatalog));
                        case "Посты сотрудников":
                            context.Post.Add(new Post() { Name = addedName });
                            context.SaveChanges();
                            return View(new SelectedCat(context.Post.ToList(), curCatalog));
                        case "Роли в проектах":
                            context.Role.Add(new Role() { Name = addedName });
                            context.SaveChanges();
                            return View(new SelectedCat(context.Role.ToList(), curCatalog));
                        case "Навыки":
                            context.Skill.Add(new Skill() { Name = addedName });
                            context.SaveChanges();
                            return View(new SelectedCat(context.Skill.ToList(), curCatalog));
                        case "Специальности":
                            context.Speciality.Add(new Speciality() { Name = addedName });
                            context.SaveChanges();
                            return View(new SelectedCat(context.Speciality.ToList(), curCatalog));
                        case "Университеты":
                            context.University.Add(new University() { Name = addedName });
                            context.SaveChanges();
                            return View(new SelectedCat(context.University.ToList(), curCatalog));
                        default:
                            context.Task.Add(new Task() { Name = addedName });
                            context.SaveChanges();
                            return View(new SelectedCat(context.Task.ToList(), curCatalog));
                    }
                else
                    return RedirectToAction("CatalogSelected",new { catalogName = curCatalog});
            }
            else
                switch (curCatalog)
                {
                    case "Департаменты":
                        Department b = new Department { Id = id };
                        context.Entry(b).State = System.Data.Entity.EntityState.Deleted;
                        context.SaveChanges();
                        return View(new SelectedCat(context.Department.ToList(), curCatalog));
                    case "Образования":
                        Education b1 = new Education { Id = id };
                        context.Entry(b1).State = System.Data.Entity.EntityState.Deleted;
                        context.SaveChanges();
                        return View(new SelectedCat(context.Education.ToList(), curCatalog));
                    case "Посты сотрудников":
                        Post b2 = new Post { Id = id };
                        context.Entry(b2).State = System.Data.Entity.EntityState.Deleted;
                        context.SaveChanges();
                        return View(new SelectedCat(context.Post.ToList(), curCatalog));
                    case "Роли в проектах":
                        Role b3 = new Role { Id = id };
                        context.Entry(b3).State = System.Data.Entity.EntityState.Deleted;
                        context.SaveChanges();
                        return View(new SelectedCat(context.Role.ToList(), curCatalog));
                    case "Навыки":
                        Skill b4 = new Skill { Id = id };
                        context.Entry(b4).State = System.Data.Entity.EntityState.Deleted;
                        context.SaveChanges();
                        return View(new SelectedCat(context.Skill.ToList(), curCatalog));
                    case "Специальности":
                        Speciality b5 = new Speciality { Id = id };
                        context.Entry(b5).State = System.Data.Entity.EntityState.Deleted;
                        context.SaveChanges();
                        return View(new SelectedCat(context.Speciality.ToList(), curCatalog));
                    case "Университеты":
                        University b6 = new University { Id = id };
                        context.Entry(b6).State = System.Data.Entity.EntityState.Deleted;
                        context.SaveChanges();
                        return View(new SelectedCat(context.University.ToList(), curCatalog));
                    default:
                        Task b7 = new Task { Id = id };
                        context.Entry(b7).State = System.Data.Entity.EntityState.Deleted;
                        context.SaveChanges();
                        return View(new SelectedCat(context.Task.ToList(), curCatalog));
                }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
                if (context != null)
                {
                    context.Dispose();
                    context = null;
                }
            }

            base.Dispose(disposing);
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}