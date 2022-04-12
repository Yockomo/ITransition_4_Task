using Itransition.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Itransition.Data;
using Microsoft.AspNetCore.Identity;
using Itransition.Areas.Identity.ApplicationIdentityUser;

namespace Itransition.Controllers
{   [Authorize]
    public class HomeController : Controller
    {
        SqlCommand command = new SqlCommand();
        SqlDataReader dataReader;
        SqlConnection connection = new SqlConnection();

        List<People> peoples = new List<People>();

        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly SignInManager<AppIdentityUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<AppIdentityUser> userManager,SignInManager<AppIdentityUser> signInManager)
        {
            _userManager = userManager;
            _context = context;
            _logger = logger;
            _signInManager = signInManager;
            connection.ConnectionString = "Server=DESKTOP-IRIGQQM\\SQLEXPRESS;Database=Itransition;Trusted_Connection=True;MultipleActiveResultSets=true";
        }

        public IActionResult Index()
        {
            FetchData();
            return View(peoples);
        }

        private void FetchData()
        {
            if (peoples.Count > 0)
                peoples.Clear();
            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT TOP 1000 [Id],[Email],[LastSeenOnSite],[Name],[RegistrationDate],[Status] FROM [Itransition].[dbo].[AspNetUsers]";
                dataReader = command.ExecuteReader();
                while(dataReader.Read())
                {
                    peoples.Add(new() { Id = dataReader["Id"].ToString() 
                        ,Email = dataReader["Email"].ToString()
                        ,Name = dataReader["Name"].ToString()
                        ,Status = dataReader["Status"].ToString()
                        ,RegistrationDate = Convert.ToDateTime(dataReader["RegistrationDate"])
                        ,LastSeenOnSite = Convert.ToDateTime(dataReader["LastSeenOnSite"])
                    });
                }
                connection.Close();
            }
            catch (Exception)
            {
                throw new Exception("DataFetch Exception");
            }
        }

        public async Task<ActionResult> DeleteSelectedUsers(string[] usersIds)
        {
            Users currentUser = new Users(_userManager);
            string[] getId = null;
            if (usersIds != null)
            {   
                getId = new string[usersIds.Length];
                int j = 0;
                foreach (var user in usersIds)
                    getId[j++] = user;
                foreach (var id in getId)
                {
                    var user = await _userManager.FindByIdAsync(id);
                    if (user != null)
                    {
                        await _userManager.UpdateSecurityStampAsync(user);
                        IdentityResult result = await _userManager.DeleteAsync(user);
                        _context.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> BlockSelectedUsers(string[] usersIds)
        {
            Users currentUser = new Users(_userManager);
            string[] getId = null;
            if (usersIds != null)
            {
                getId = new string[usersIds.Length];
                int j = 0;
                foreach (var user in usersIds)
                    getId[j++] = user;
                foreach (var id in getId)
                {
                    var user = await _userManager.FindByIdAsync(id);
                    if (user != null)
                    {
                        await _userManager.UpdateSecurityStampAsync(user);
                        currentUser.LockUser(user.Email, DateTime.Now.AddDays(360));
                        user.Status = "Blocked";
                        _context.SaveChanges();
                    }
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }     
        
        public async Task<ActionResult> UnBlockSelectedUsers(string[] usersIds)
        {
            Users currentUser = new Users(_userManager);
            string[] getId = null;
            if (usersIds != null)
            {
                getId = new string[usersIds.Length];
                int j = 0;
                foreach (var user in usersIds)
                    getId[j++] = user;
                foreach (var id in getId)
                {
                    var user = await _userManager.FindByIdAsync(id);
                    if (user != null)
                    {
                        currentUser.UnlockUser(user.Email);
                        user.Status = "Unblocked";
                        _context.SaveChanges();
                    }
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
