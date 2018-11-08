using System.Collections.Generic;
using TestTaskDenysBuhai.Models;

namespace TestTaskDenysBuhai.Services
{
	public interface IFileService
	{
		LinkedList<Student> ReadFromFile();
		void WriteToFile(LinkedList<Student> students);
	}
}
