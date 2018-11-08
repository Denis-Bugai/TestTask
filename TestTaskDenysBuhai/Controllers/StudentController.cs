using System;
using Microsoft.AspNetCore.Mvc;
using TestTaskDenysBuhai.Models;
using TestTaskDenysBuhai.Services;

namespace TestTaskDenysBuhai.Controllers
{
	[Route("api/Student")]
	public class StudentController : ControllerBase
	{
		IStudentService _studentService;

		public StudentController(IStudentService studentService)
		{
			_studentService = studentService;
		}

		[HttpGet("GetStudent/{id}")]
		public IActionResult GetStudent(Guid id)
		{
			var student = _studentService.GetStudent(id);
			return Ok(student);
		}

		[HttpPost("CreateStudent")]
		public IActionResult CreateStudent([FromBody] Student student)
		{
			var result = _studentService.CreateStudent(student);
			return Ok(result);
		}

		[HttpPut("UpdateStudent")]
		public IActionResult UpdateStudent([FromBody]  Student student)
		{
			var result = _studentService.UpdateStudent(student);
			return Ok(result);
		}

		[HttpDelete("DeleteStudent/{id}")]
		public IActionResult DeleteStudent(Guid id)
		{
			var result = _studentService.DeleteSrudent(id);
			return Ok(result);
		}
		[HttpGet("GetAllStudents")]
		public IActionResult GetAllStudents()
		{
			var student = _studentService.GetAllStudents();
			return Ok(student);
		}
	}
}
