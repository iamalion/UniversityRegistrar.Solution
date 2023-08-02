using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UniversityRegistrar.Models;
using System.Collections.Generic;
using System.Linq;

namespace UniversityRegistrar.Controllers
{
  public class CoursesController : Controller
  {
    private readonly UniversityRegistrarContext _db;

    public CoursesController(UniversityRegistrarContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Course> model = _db.Courses.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(Course course)
    {
        _db.Courses.Add(course);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
    public ActionResult Details(int id)
    {
        Course thisCourse = _db.Courses
                            .Include(course => course.JoinEntities)
                            .ThenInclude(join => join.Student)
                            .FirstOrDefault(course => course.CourseId == id);
        return View(thisCourse);
    }
    public ActionResult Edit(int id)
    {
        Course thisCourse = _db.Courses.FirstOrDefault(course => course.CourseId == id);
       
        return View(thisCourse);
    }

    [HttpPost]
    public ActionResult Edit(Course course)
    {
        _db.Courses.Update(course);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
    public ActionResult Delete(int id)
    {
        Course thisCourse = _db.Courses.FirstOrDefault(course => course.CourseId == id);
        ViewBag.PageTitle = "Delete Course";
        return View(thisCourse);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
        Course thisCourse = _db.Courses.FirstOrDefault(course => course.CourseId == id);
        _db.Courses.Remove(thisCourse);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
    public ActionResult AddStudent(int id)
    {
        Course thisCourse = _db.Courses.FirstOrDefault(courses => courses.CourseId == id);
        List<Student> students = _db.Students.ToList();
        SelectList studentList = new SelectList(students, "StudentId", "StudentName");
        ViewBag.StudentId = studentList;
        return View(thisCourse);
    }

    [HttpPost]
    public ActionResult AddStudent(Course course, int studentId)
    {
      #nullable enable
      CourseStudent? joinEntity = _db.CourseStudents.FirstOrDefault(join => (join.StudentId == studentId && join.CourseId == course.CourseId));
      #nullable disable
      if (joinEntity == null && studentId != 0)
      {
        _db.CourseStudents.Add(new CourseStudent() { StudentId = studentId, CourseId = course.CourseId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = course.CourseId });
    }
    
  }
}

