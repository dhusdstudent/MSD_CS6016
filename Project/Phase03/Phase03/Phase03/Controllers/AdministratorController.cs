using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Phase03.Context;
using Phase03.Entities;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LMS.Controllers
{
    //TODO: add your controller as a "primary constructor" param:
    //eg: public class AdministratorController(MyContextType myContext) 
    public class AdministratorController (MyDbContext context): Controller
    {

        private readonly MyDbContext _context = context;
        
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Department(string subject)
        {
            ViewData["subject"] = subject;
            return View();
        }

        public IActionResult Course(string subject, string num)
        {
            ViewData["subject"] = subject;
            ViewData["num"] = num;
            return View();
        }

        /*******Begin code to modify********/

        /// <summary>
        /// Create a department which is uniquely identified by it's subject code
        /// </summary>
        /// <param name="subject">the subject code</param>
        /// <param name="name">the full name of the department</param>
        /// <returns>A JSON object containing {success = true/false}.
        /// false if the department already exists, true otherwise.</returns>
        public IActionResult CreateDepartment(string subject, string name)
        {
            if (_context.Departments.Any(d => d.Subject == subject))
            {
                return Json(new { success = false});  
            } else
            {
                Department department = new Department
                {
                    Subject = subject, Depname = name
                };
                _context.Departments.Add(department);
                _context.SaveChanges();
                return Json(new { success = true});  
            }
        }


        /// <summary>
        /// Returns a JSON array of all the courses in the given department.
        /// Each object in the array should have the following fields:
        /// "number" - The course number (as in 5530)
        /// "name" - The course name (as in "Database Systems")
        /// </summary>
        /// <param name="subjCode">The department subject abbreviation (as in "CS")</param>
        /// <returns>The JSON result</returns>
        public IActionResult GetCourses(string subject)
        {
            try
            {
                var courses = _context.Courses.Where(c => c.Coursenum == subject).Select(c => new
                {
                    number = c.Catalogid,
                    name = c.Coursename
                }).ToList();

                return Json(courses);
            }
            catch
            {
                return Json(new { success = false });
            }

        }

        /// <summary>
        /// Returns a JSON array of all the professors working in a given department.
        /// Each object in the array should have the following fields:
        /// "lname" - The professor's last name
        /// "fname" - The professor's first name
        /// "uid" - The professor's uid
        /// </summary>
        /// <param name="subject">The department subject abbreviation</param>
        /// <returns>The JSON result</returns>
        public IActionResult GetProfessors(string subject)
        {
            try 
            {
                var prof = _context.Professors.Where(c => c.Employer == subject).Select(c => new
                    {
                        firstname =  c.Firstname,
                        lastname = c.Lastname,
                        uid = c.Userid
                    }).ToList();

                    return Json(prof);
            }
            catch
            {
                return Json(new { success = false});
            }
        }



        /// <summary>
        /// Creates a course.
        /// A course is uniquely identified by its number + the subject to which it belongs
        /// </summary>
        /// <param name="subject">The subject abbreviation for the department in which the course will be added</param>
        /// <param name="number">The course number</param>
        /// <param name="name">The course name</param>
        /// <returns>A JSON object containing {success = true/false}.
        /// false if the course already exists, true otherwise.</returns>
        public IActionResult CreateCourse(string subject, int number, string name)
        {           
            if (_context.Courses.Any(d => d.Coursename == name))
            {
                return Json(new { success = false});  
            } else
            {
                Course course = new Course
                {
                    Coursenum = subject, Coursename = name, Catalogid = number
                };
                _context.Courses.Add(course);
                _context.SaveChanges();
                return Json(new { success = true});  
            }
        }



        /// <summary>
        /// Creates a class offering of a given course.
        /// </summary>
        /// <param name="subject">The department subject abbreviation</param>
        /// <param name="number">The course number</param>
        /// <param name="season">The season part of the semester</param>
        /// <param name="year">The year part of the semester</param>
        /// <param name="start">The start time</param>
        /// <param name="end">The end time</param>
        /// <param name="location">The location</param>
        /// <param name="instructor">The uid of the professor</param>
        /// <returns>A JSON object containing {success = true/false}. 
        /// false if another class occupies the same location during any time 
        /// within the start-end range in the same semester, or if there is already
        /// a Class offering of the same Course in the same Semester,
        /// true otherwise.</returns>
        public IActionResult CreateClass(string subject, int number, string season, short year, DateTime start, DateTime end, string location, string instructor)
        {            
            if (_context.Classes.Any(d => d.Classid == number))
            {
                return Json(new { success = false});  

            } else
            {
                Class newClass = new Class
                {
                    Catalogid = subject, Classid = number, Season = season, Year = year, Starttime = start,
                    Endtime = end, Location = location, Professorid = instructor
                };
                _context.Classes.Add(newClass);
                _context.SaveChanges();
                return Json(new { success = true});  
            }
        }


        /*******End code to modify********/

    }
}

