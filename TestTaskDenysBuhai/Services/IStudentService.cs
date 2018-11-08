using System;
using System.Collections.Generic;
using TestTaskDenysBuhai.Models;

namespace TestTaskDenysBuhai.Services
{
	public interface IStudentService
	{
		LinkedList<Student> GetAllStudents();
		Student GetStudent(Guid id);
		Guid? CreateStudent(Student student);
		Student UpdateStudent(Student student);
		Student DeleteSrudent(Guid id);

	}
}
