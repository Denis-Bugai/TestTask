using System;
using System.Collections.Generic;
using System.Linq;
using TestTaskDenysBuhai.Models;

namespace TestTaskDenysBuhai.Services
{
	public class StudentService : IStudentService
	{
		private IConfigureService _configureService;
		private IFileService _fileService;
		public StudentService(IConfigureService configureService, IFileService fileService)
		{
			_configureService = configureService;
			_fileService = fileService;
		}
		public Guid? CreateStudent(Student student)
		{
			if (student != null)
			{
				var incomingStudent = student;
				var students = GetAllStudents();
				if (students.Count() >= _configureService.GetMaxLength())
				{
					if (students.Count() == _configureService.GetMaxLength())
					{
						students.RemoveFirst();
						incomingStudent.Id = Guid.NewGuid();
						students.AddLast(incomingStudent);
					}
				}
				else
				{
					incomingStudent.Id = Guid.NewGuid();
					students.AddLast(incomingStudent);
				}
				_fileService.WriteToFile(students);
				return incomingStudent.Id;
			}
			return null;
		}

		public Student DeleteSrudent(Guid id)
		{
			var students = _fileService.ReadFromFile();
			var removingStudent = students.FirstOrDefault(simpleStudent => simpleStudent.Id == id);
			if (removingStudent != null)
			{
				students.Remove(removingStudent);
				_fileService.WriteToFile(students);
			}
			return removingStudent;
		}

		public LinkedList<Student> GetAllStudents()
		{
			return _fileService.ReadFromFile();
		}

		public Student GetStudent(Guid id)
		{
			var students = _fileService.ReadFromFile();
			var student = students.FirstOrDefault(simpleStudent => simpleStudent.Id == id);
			if (student != null)
			{
				students.Remove(student);
				students.AddLast(student);
				_fileService.WriteToFile(students);
			}
			return student;
		}

		public Student UpdateStudent(Student student)
		{
			if(student != null)
			{
				var students = _fileService.ReadFromFile();
				var requiredStudent = students.FirstOrDefault(simpleStudent => simpleStudent.Id == student.Id);
				if (requiredStudent != null)
				{
					students.Remove(requiredStudent);
					students.AddLast(student);
					_fileService.WriteToFile(students);
					return requiredStudent;
				}
			}
			return null;
			
		}
	}
}
