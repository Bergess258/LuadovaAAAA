using BdOfResume.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace BdOfResume.Controllers
{
    public class ConsultController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        CoursEntities context = new CoursEntities();
        // GET: Resume
        public ConsultController()
        {

        }
        public ConsultController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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
        [Authorize(Roles = "K")]
        public ActionResult TempUnav()
        {
            return View();
        }
        [Authorize(Roles = "K")]
        public ActionResult EmpTastsView()
        {
            forEmpTask model = new forEmpTask();
            model.list = new List<EmpTask>();
            var db = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
            // find the user. I am skipping validations and other checks.
            var userid = User.Identity.GetUserId();
            var user = db.Users.Where(x => x.Id == userid).FirstOrDefault();
            var te = context.Employee.Find(user.IdEmp).Participation.Select(x => x.EmpTask).ToList();
            foreach (var item in te)
            {
                model.list.Add(item.Single());
            }
            model.list.Add(new EmpTask());
            //model.list.Add(new EmpTask());
            model.avProj = context.Employee.Find(user.IdEmp).Participation.Select(x => x.Project.Name).ToList();
            return View(model);
        }
        [HttpPost]
        public ActionResult EmpTastsView(forEmpTask model, bool create = false, int id = -1)
        {
            var db = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
            // find the user. I am skipping validations and other checks.
            var userid = User.Identity.GetUserId();
            var user = db.Users.Where(x => x.Id == userid).FirstOrDefault();
            if (create)
            {
                if (ModelState.IsValid && model.list[model.list.Count - 1].Task.Name != null && model.list[model.list.Count - 1].Participation.Project.Name!=null)
                {
                    Task temp = new Task() { Name = model.list[model.list.Count - 1].Task.Name};
                    int ProjId=context.Employee.Find(user.IdEmp).Participation.Where(x => x.Project.Name == model.list[model.list.Count - 1].Participation.Project.Name).Select(c=>c.Id).FirstOrDefault();
                    if (!context.Task.Contains(temp))
                    {
                        context.Task.Add(temp);
                        context.SaveChanges();
                    }
                    EmpTask t = new EmpTask() { IdParticipation = ProjId, IdTask = temp.Id };
                    context.EmpTask.Add(t);
                    context.SaveChanges();
                }
            }
            else
            {
                string s = @"Data Source=BERGESS;Initial Catalog=Cours;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                using (SqlConnection con = new SqlConnection(s))
                {
                    con.Open();
                    string SqlString = "DELETE FROM [EmpTask]  WHERE IdParticipation = @p1 AND IdTask=@p2";
                    using (SqlCommand com = new SqlCommand(SqlString, con))
                    {
                        com.Parameters.AddWithValue("@p1", context.EmpTask.Where(x=>x.Participation.Project.Name== model.list[model.list.Count - 1].Participation.Project.Name).Where(y=>y.Participation.IdEmp==user.IdEmp).Select(c=>c.IdParticipation).FirstOrDefault());
                        com.Parameters.AddWithValue("@p2", id);
                        com.ExecuteNonQuery();
                    }
                }
            }
            return RedirectToAction("EmpTastsView");
        }
        [Authorize(Roles = "K")]
        public ActionResult ProjEmp()
        {
            ListAndAvProj model = new ListAndAvProj();
            var db = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
            // find the user. I am skipping validations and other checks.
            var userid = User.Identity.GetUserId();
            var user = db.Users.Where(x => x.Id == userid).FirstOrDefault();
            model.list = context.Employee.Find(user.IdEmp).Participation.ToList();
            model.list.Add(new Participation());
            model.avProj = context.Project.Select(x => x.Name).ToList();
            return View(model);
        }
        [HttpPost]
        public ActionResult ProjEmp(ListAndAvProj model, bool create = false, int id = -1)
        {
            var db = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
            // find the user. I am skipping validations and other checks.
            var userid = User.Identity.GetUserId();
            var user = db.Users.Where(x => x.Id == userid).FirstOrDefault();
            if (create)
            {
                if (ModelState.IsValid && model.list[model.list.Count - 1].Project != null)
                {
                    Role temp = new Role() { Name = model.list[model.list.Count - 1].Role.Name,Id=-1 };
                    string s = @"Data Source=BERGESS;Initial Catalog=Cours;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                    using (SqlConnection con = new SqlConnection(s))
                    {
                        con.Open();
                        string SqlString = "SELECT Id FROM [Role] WHERE Name = @My";
                        using (SqlCommand com = new SqlCommand(SqlString, con))
                        {
                            com.Parameters.AddWithValue("@My", temp.Name);
                            using (SqlDataReader reader = com.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    temp.Id = Convert.ToInt32(reader["Id"]);
                                }

                            }
                        }
                    }
                    if (temp.Id == -1)
                    {
                        context.Role.Add(temp);
                        context.SaveChanges();
                    }
                    Participation t = new Participation() { IdEmp = user.IdEmp, IdRole = temp.Id };
                    context.Participation.Add(t);
                    context.SaveChanges();
                }
            }
            else
            {
                string s = @"Data Source=BERGESS;Initial Catalog=Cours;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                using (SqlConnection con = new SqlConnection(s))
                {
                    con.Open();
                    string SqlString = "DELETE FROM [Participation]  WHERE IdEmp = @p1 AND IdRole=@p2";
                    using (SqlCommand com = new SqlCommand(SqlString, con))
                    {
                        com.Parameters.AddWithValue("@p1", user.IdEmp);
                        com.Parameters.AddWithValue("@p2", id);
                        com.ExecuteNonQuery();
                    }
                }
            }
            return RedirectToAction("ProjEmp");
        }
        [Authorize(Roles = "K")]
        public ActionResult SkillsEmp()
        {
            var db = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
            // find the user. I am skipping validations and other checks.
            var userid = User.Identity.GetUserId();
            var user = db.Users.Where(x => x.Id == userid).FirstOrDefault();
            var list = context.Employee.Find(user.IdEmp).EmpSkills.Select(x => x.Skill).ToList();
            list.Add(new Skill());
            if (list[0] == null)
            {
                list.RemoveAt(0);
            }
            return View(list);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> SkillsEmp(List<Skill> myList, bool create = false, int id=-1)
        {
            var db = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
            // find the user. I am skipping validations and other checks.
            var userid = User.Identity.GetUserId();
            var user = db.Users.Where(x => x.Id == userid).FirstOrDefault();
            if (create)
            {
                if (string.IsNullOrEmpty(myList[myList.Count - 1].Name))
                {
                    ModelState.AddModelError("Name","Пустое значение при добавлении");
                }
                else
                    if(myList[myList.Count - 1].Name.Length < 3)
                {
                    ModelState.AddModelError("Name", "Недопустимая длина строки");
                }
                else
                {
                    for (int i = 0; i < myList.Count-1; i++)
                    {
                        if(myList[i].Name==myList[myList.Count - 1].Name)
                        {
                            ModelState.AddModelError("Name", "Данный навык уже имеется");
                            break;
                        }
                    }
                }
                if (ModelState.IsValid)
                {
                    Skill temp = new Skill() { Id = -1, Name = myList[myList.Count - 1].Name, Sap = myList[myList.Count - 1].Sap };
                    string s = @"Data Source=BERGESS;Initial Catalog=Cours;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                    using (SqlConnection con = new SqlConnection(s))
                    {
                        con.Open();
                        string SqlString = "SELECT Id FROM [Skill] WHERE Name = @My";
                        using (SqlCommand com = new SqlCommand(SqlString, con))
                        {
                            com.Parameters.AddWithValue("@My", myList[myList.Count - 1].Name);
                            using (SqlDataReader reader = com.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    temp.Id = Convert.ToInt32(reader["Id"]);
                                }

                            }
                        }
                    }
                    if (temp.Id == -1)
                    {
                        temp = myList[myList.Count - 1];
                        context.Skill.Add(temp);
                        context.SaveChanges();
                    }
                    EmpSkills t = new EmpSkills() { IdEmp = user.IdEmp, IdSkill = temp.Id };
                    context.EmpSkills.Add(t);
                    await context.SaveChangesAsync();
                    
                }
            }
            else
            {
                string s = @"Data Source=BERGESS;Initial Catalog=Cours;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                using (SqlConnection con = new SqlConnection(s))
                {
                    con.Open();
                    string SqlString = "DELETE FROM [EmpSkills]  WHERE IdEmp = @p1 AND IdSkill=@p2";
                    using (SqlCommand com = new SqlCommand(SqlString, con))
                    {
                        com.Parameters.AddWithValue("@p1", user.IdEmp);
                        com.Parameters.AddWithValue("@p2", id);
                        com.ExecuteNonQuery();
                    }
                }
            }
            ModelState.Clear();
            return RedirectToAction("SkillsEmp");
        }
        // GET: Consult
        [Authorize(Roles = "K")]
        public ActionResult ConsAccView()
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
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> ConsAccView(AccInfo model)
        {
            HttpPostedFileBase Profile = Request.Files["profile"];
            if (ModelState.IsValid)
            {
                if (Profile!=null)
                {
                    if (Regex.IsMatch(Profile.FileName,"(.jpg||.png||.jpeg||.ico)$"))
                    {
                        var db = HttpContext.GetOwinContext().Get<ApplicationDbContext>();

                        // find the user. I am skipping validations and other checks.
                        var userid = User.Identity.GetUserId();
                        var user = db.Users.Where(x => x.Id == userid).FirstOrDefault();
                        // convert image stream to byte array
                        byte[] image = new byte[Profile.ContentLength];
                        Profile.InputStream.Read(image, 0, Convert.ToInt32(Profile.ContentLength));
                        user.ProfilePicture = image;
                        // save changes to database
                        db.SaveChanges();
                    }
                }
                if (model.OldPassword != null && model.NewPassword != null)
                {
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
            }
            return View(model);
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
        public FileContentResult Photo(string userLogin)
        {
            var db = HttpContext.GetOwinContext().Get<ApplicationDbContext>();

            var user = db.Users.Where(x => x.UserName == userLogin).FirstOrDefault();
            if (user!=null&&user.ProfilePicture != null)
                return new FileContentResult(user.ProfilePicture, "image/jpeg");
            else
            {
                string imgPath = @"C:\Users\Администратор.BERGESS\source\repos\BdOfResume\BdOfResume\Content\imgs\DefaultCons.png";
                Image image = Image.FromFile(imgPath);
                System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
                image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                byte[] b = memoryStream.ToArray();
                return new FileContentResult(b, "image/jpeg");
            }
        }
    }
}