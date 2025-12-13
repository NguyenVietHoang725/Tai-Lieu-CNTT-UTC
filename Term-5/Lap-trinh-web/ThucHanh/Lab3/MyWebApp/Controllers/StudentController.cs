using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyWebApp.Interfaces;
using MyWebApp.Models;

namespace MyWebApp.Controllers
{
    public class StudentController : Controller
    {
        private static List<Student> _listStudents = new List<Student>();
        private readonly IBufferedFileUploadService _bufferedFileUploadService;

        public StudentController(IBufferedFileUploadService bufferedFileUploadService)
        {
            _bufferedFileUploadService = bufferedFileUploadService;

            if (!_listStudents.Any())
            {
                _listStudents = new List<Student>()
                {
                    new Student()
                    {
                        Id = 101,
                        Name = "Hải Nam",
                        Branch = Branch.IT,
                        Gender = Gender.Male,
                        IsRegular = true,
                        Address = "A1-2018",
                        Email = "nam@g.com",
                        Score = 8.5,
                        ImagePath = "/UploadedFiles/Students/101/Pic_1.jpg"
                    },
                    new Student()
                    {
                        Id = 102,
                        Name = "Minh Tú",
                        Branch = Branch.BE,
                        Gender = Gender.Female,
                        IsRegular = true,
                        Address = "A1-2019",
                        Email = "tu@g.com",
                        Score = 9.0,
                        ImagePath = "/UploadedFiles/Students/102/Pic_2.jpg"
                    },
                    new Student()
                    {
                        Id = 103,
                        Name = "Hoàng Phong",
                        Branch = Branch.CE,
                        Gender = Gender.Male,
                        IsRegular = false,
                        Address = "A1-2020",
                        Email = "phong@g.com",
                        Score = 7.5,
                        ImagePath = ""
                    },
                    new Student()
                    {
                        Id = 104,
                        Name = "Xuân Mai",
                        Branch = Branch.EE,
                        Gender = Gender.Female,
                        IsRegular = false,
                        Address = "A1-2021",
                        Email = "mai@g.com",
                        Score = 8.0,
                        ImagePath = ""
                    }
                };
            }
        }

        public IActionResult Index()
        {
            return View(_listStudents);
        }

        [HttpGet]
        public IActionResult Create(Student s)
        {
            var student = _listStudents.FirstOrDefault(s => s.Id == s.Id);
            if (student == null)
            {
                return NotFound();
            }

            ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
            ViewBag.AllBranches = new List<SelectListItem>()
            {
                new SelectListItem() { Text = "IT", Value = "1" },
                new SelectListItem() { Text = "BE", Value = "2" },
                new SelectListItem() { Text = "CE", Value = "3" },
                new SelectListItem() { Text = "EE", Value = "4" }
            };
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student s, IFormFile file)
        {
            var existing = _listStudents.FirstOrDefault(st => st.Id == s.Id);
            if (existing != null)
            {
                return NotFound();
            }

            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("ImagePath", "Vui lòng chọn ảnh đại diện");
            }

            if (ModelState.IsValid)
            {  
                    // Tạo ID mới
                    s.Id = _listStudents.Any() ? _listStudents.Max(st => st.Id) + 1 : 101;

                if (file != null && file.Length > 0)
                {
                    try
                    {
                        var imagePath = await _bufferedFileUploadService.UploadFile(file, s.Id);
                        if (!string.IsNullOrEmpty(imagePath))
                        {
                            s.ImagePath = imagePath;
                            ViewBag.Message = "File Upload Successful";
                        }
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "File Upload Failed: " + ex.Message;
                        return View(s);
                    }
                }
                else
                {
                    s.ImagePath = "/UploadedFiles/default-Pic_5.png";
                }

                _listStudents.Add(s);
                return RedirectToAction("Index"); // Chuyển hướng thay vì return View
            }

            // Nếu model invalid, return view với data
            ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
            return View(s);
        }
    }
}