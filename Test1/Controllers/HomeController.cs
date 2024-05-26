using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Xml.Serialization;
using UserApplication.Models;

namespace UserApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string _filePath = "UserDetails.xml";
        private static List<User> _users;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        
        public IActionResult Index()
        {
            _users = ReadUsersFromFile();
            return View(_users);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    user.Id = _users.Any() ? _users.Max(u => u.Id) + 1 : 1;
                    _users.Add(user);
                    WriteUsersToFile();

                    return RedirectToAction("Index");
                }
                return View(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public IActionResult Edit(int id)
        {
            try
            {
                var user = _users.FirstOrDefault(u => u.Id == id);
                if (user == null)
                {
                    return NotFound();
                }
                return View(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userToEdit = _users.FirstOrDefault(u => u.Id == user.Id);
                    if (userToEdit == null)
                    {
                        return NotFound();
                    }

                    userToEdit.FirstName = user.FirstName;
                    userToEdit.LastName = user.LastName;
                    userToEdit.Cellphone = user.Cellphone;
                    WriteUsersToFile();

                    return RedirectToAction("Index");
                }
                return View(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var user = _users.FirstOrDefault(u => u.Id == id);
                if (user != null)
                {
                    _users.Remove(user);
                    WriteUsersToFile();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Common
        private List<User> ReadUsersFromFile()
        {
            try
            {
                if (System.IO.File.Exists(_filePath))
                {
                    using (var stream = new FileStream(_filePath, FileMode.Open))
                    {
                        var serializer = new XmlSerializer(typeof(List<User>));
                        return (List<User>)serializer.Deserialize(stream);
                    }
                }
                return new List<User>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        private void WriteUsersToFile()
        {
            try
            {
                using (var stream = new FileStream(_filePath, FileMode.Create))
                {
                    var serializer = new XmlSerializer(typeof(List<User>));
                    serializer.Serialize(stream, _users);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
        #endregion


    }
}