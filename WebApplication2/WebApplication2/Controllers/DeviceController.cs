using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using WebApplication2.ViewModel;

namespace WebApplication1.Controllers
{
    public class DeviceController : Controller
    {
        private ApplicationDbContext _context;
        public DeviceController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            string userName = User.Identity.Name.Split('\\')[1].ToString();
            Users user = _context.Users.Where(x => x.FirstName == userName).FirstOrDefault();
            if (user == null)
            {
                Users addUser = new Users() { FirstName = userName, CreatedAt = DateTime.Now, IsAdmin = true };
                _context.Users.Add(addUser);
                _context.SaveChanges();
            };
            bool? isAdmin = _context.Users.Where(x => x.FirstName == userName).FirstOrDefault().IsAdmin;
            var viewModel = new DeviceViewModel
            {
                Devices = _context.Devices.ToList(),
                IsAdmin = isAdmin??false
            };
            return View(viewModel);

            //return View(_context.Devices.ToList());
        }
        public ActionResult CreateDevice()
        {
            var model = new Device
            {
                Status = StatusOption.IsRejected,
                CategoryList = new List<SelectListItem>
        {
            new SelectListItem { Value = "1", Text = "PC" },
            new SelectListItem { Value = "2", Text = "Monitor" },
            new SelectListItem { Value = "3", Text = "Scanner" },
            new SelectListItem { Value = "4", Text = "Printer" },
            new SelectListItem { Value = "5", Text = "3 in 1  Printer" },
            new SelectListItem { Value = "6", Text = "Laptop" },
            new SelectListItem { Value = "7", Text = "Tablet" },
            new SelectListItem { Value = "8", Text = "Mobile" },
            new SelectListItem { Value = "9", Text = "Telephon" },
            new SelectListItem { Value = "10", Text = "Camera" },
            new SelectListItem { Value = "11", Text = "Speaker" }
        }
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateDevice(Device device)
        {
            try
            {
                //if(device.Status == StatusOption.IsReserved)
                //{
                //    if (device.Staff.File == null)
                //    {
                //        ViewBag.ErrorMsg = "Attachment must be selected!";
                //        return View(device);
                //    }
                //}
                var category = GetCategoryNameById(device.CategoryId);
                //if (ModelState.IsValid)
                //{
                    device.CreatedAt = DateTime.Now;
                    device.Category = category;
                    device.StaffId = device.Staff.staff_id;
                    device.StaffName = device.Staff.staff_name_en;
                    device.Department = device.Staff.department_en;
                    device.DepartmentHead = device.Staff.department_head;
                    device.DepartmentHeadEmail = device.Staff.email;
                    _context.Devices.Add(device);
                    _context.SaveChanges();

                var userNameParts = User.Identity.Name.Split('\\');
                var userName = userNameParts.Length > 1 ? userNameParts[1] : User.Identity.Name;
                string fileName = string.Empty;
                if (device.Staff.File!=null)
                {
                    fileName = Path.GetFileName(device.Staff.File.FileName);
                    string extension = Path.GetExtension(fileName);
                    if (extension.ToLower() == ".pdf")
                    {
                        // Save file
                        string path = Server.MapPath("~/Uploads/");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        fileName = Guid.NewGuid().ToString() + extension;
                        device.Staff.File.SaveAs(Path.Combine(path, fileName));
                    }
                }
                var staff = new StaffChanges
                {
                    DeviceId = device.DeviceId,
                    StaffId = device.StaffId,
                    StaffName = device.StaffName,
                    Department = device.Department,
                    DepartmentHead = device.DepartmentHead,
                    DepartmentEmail = device.DepartmentHeadEmail,
                    ChangedBy = userName,
                    DateTime = DateTime.Now,
                    AttachmentPath = "/Uploads/"+ fileName
                };
                _context.StaffChanges.Add(staff);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
                //}
                //return View(device);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Device device = _context.Devices.FirstOrDefault(m => m.DeviceId == id);
            if (device == null)
            {
                return HttpNotFound();
            }
            var staffChanges = _context.StaffChanges.Where(sc => sc.DeviceId == id).ToList();
            var categoryList = new List<SelectListItem>
    {
            new SelectListItem { Value = "1", Text = "PC" },
            new SelectListItem { Value = "2", Text = "Monitor" },
            new SelectListItem { Value = "3", Text = "Scanner" },
            new SelectListItem { Value = "4", Text = "Printer" },
            new SelectListItem { Value = "5", Text = "3 in 1  Printer" },
            new SelectListItem { Value = "6", Text = "Laptop" },
            new SelectListItem { Value = "7", Text = "Tablet" },
            new SelectListItem { Value = "8", Text = "Mobile" },
            new SelectListItem { Value = "9", Text = "Telephon" },
            new SelectListItem { Value = "10", Text = "Camera" },
            new SelectListItem { Value = "11", Text = "Speaker" }
    };
            if(staffChanges.Count>0)
                device.AttachmentPath = staffChanges[0].AttachmentPath;
            var viewModel = new DeviceEditViewModel
            {
                Device = device,
                StaffChanges = staffChanges,
                CategoryList = categoryList
            };
            return View(viewModel);
        }
        public ActionResult View(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Device device = _context.Devices.FirstOrDefault(m => m.DeviceId == id);
            if (device == null)
            {
                return HttpNotFound();
            }
            var staffChanges = _context.StaffChanges.Where(sc => sc.DeviceId == id).ToList();
            var categoryList = new List<SelectListItem>
    {
            new SelectListItem { Value = "1", Text = "PC" },
            new SelectListItem { Value = "2", Text = "Monitor" },
            new SelectListItem { Value = "3", Text = "Scanner" },
            new SelectListItem { Value = "4", Text = "Printer" },
            new SelectListItem { Value = "5", Text = "3 in 1  Printer" },
            new SelectListItem { Value = "6", Text = "Laptop" },
            new SelectListItem { Value = "7", Text = "Tablet" },
            new SelectListItem { Value = "8", Text = "Mobile" },
            new SelectListItem { Value = "9", Text = "Telephon" },
            new SelectListItem { Value = "10", Text = "Camera" },
            new SelectListItem { Value = "11", Text = "Speaker" }
    };
            if (staffChanges.Count > 0)
                device.AttachmentPath = staffChanges[0].AttachmentPath;
            var viewModel = new DeviceEditViewModel
            {
                Device = device,
                StaffChanges = staffChanges,
                CategoryList = categoryList
            };
            return View(viewModel);
        }
        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Device device)
        {
            if (ModelState.IsValid)
            {
                var existingDevice = _context.Devices.Find(device.DeviceId);
                var category = GetCategoryNameById(device.CategoryId);

                if (existingDevice == null)
                {
                    return HttpNotFound();
                }
                string fileName = string.Empty;
                if (device.Staff.File != null)
                {
                    fileName = Path.GetFileName(device.Staff.File.FileName);
                    string extension = Path.GetExtension(fileName);
                    if (extension.ToLower() == ".pdf")
                    {
                        // Save file
                        string path = Server.MapPath("~/Uploads/");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        fileName = Guid.NewGuid().ToString() + extension;
                        device.Staff.File.SaveAs(Path.Combine(path, fileName));
                        existingDevice.AttachmentPath = "/Uploads/"+fileName;
                    }
                }
                
                if (existingDevice.StaffId != device.StaffId)
                {
                    var userNameParts = User.Identity.Name.Split('\\');
                    var userName = userNameParts.Length > 1 ? userNameParts[1] : User.Identity.Name;
                    var staffChange = new StaffChanges
                    {
                        DeviceId = device.DeviceId,
                        StaffId = device.StaffId,
                        StaffName = device.StaffName,
                        Department = device.Department,
                        ChangedBy = userName,
                        ChangeNote = device.Note,
                        DateTime = DateTime.Now,
                        AttachmentPath = existingDevice.AttachmentPath
                    };
                    _context.StaffChanges.Add(staffChange);
                }
                else if (device.Staff.File != null)
                {
                    var staff = _context.StaffChanges.Where(x => x.DeviceId == device.DeviceId).FirstOrDefault();
                    staff.AttachmentPath = existingDevice.AttachmentPath;
                    _context.SaveChanges();
                }
                device.Category = category;
                device.CreatedAt = DateTime.Now;
                _context.Entry(existingDevice).CurrentValues.SetValues(device);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(device);
        }
        public ActionResult Delete(int? id)
        {
            var device = _context.Devices
                .FirstOrDefault(m => m.DeviceId == id);

            if (device == null)
            {
                return null;
            }

            return View(device);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var device = _context.Devices.Find(id);
            if (device != null)
            {
                _context.Devices.Remove(device);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
        public ActionResult GetUsersForDevice(int deviceId)
        {
            var users = _context.Users.ToList();
            ViewData["DeviceId"] = deviceId;
            return PartialView("AssignDevice", users);
        }
        public ActionResult AssignDevice(string users)
        {
            return View(users);
        }
        [HttpPost]
        public ActionResult AssignDevice(int deviceId, int userId)
        {
            var device = _context.Devices.Find(deviceId);
            var user = _context.Users.Find(userId);

            if (device != null && user != null)
            {
                var association = new ProductAssociation
                {
                    UserId = userId,
                    DeviceId = deviceId,
                    AssignDate = DateTime.Now,
                    IsAssigned = true
                };

                _context.ProductAssociations.Add(association);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        private string GetCategoryNameById(int categoryId)
        {
            var categories = new List<SelectListItem>
    {
            new SelectListItem { Value = "1", Text = "PC" },
            new SelectListItem { Value = "2", Text = "Monitor" },
            new SelectListItem { Value = "3", Text = "Scanner" },
            new SelectListItem { Value = "4", Text = "Printer" },
            new SelectListItem { Value = "5", Text = "3 in 1  Printer" },
            new SelectListItem { Value = "6", Text = "Laptop" },
            new SelectListItem { Value = "7", Text = "Tablet" },
            new SelectListItem { Value = "8", Text = "Mobile" },
            new SelectListItem { Value = "9", Text = "Telephon" },
            new SelectListItem { Value = "10", Text = "Camera" },
            new SelectListItem { Value = "11", Text = "Speaker" }
    };

            var category = categories.FirstOrDefault(c => c.Value == categoryId.ToString());
            return category?.Text;
        }

    }
    public class DeviceViewModel
    {
        public IEnumerable<Device> Devices { get; set; }
        public bool IsAdmin { get; set; }
    }

}
