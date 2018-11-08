using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using TestTaskDenysBuhai.Models;

namespace TestTaskDenysBuhai.Services
{
	public class FileService : IFileService
	{
		private readonly string path;
		IConfigureService _configureService;
		public FileService(IConfigureService configureService)
		{
			_configureService = configureService;
			path = AppDomain.CurrentDomain.BaseDirectory + "students.";
		}
		public LinkedList<Student> ReadFromFile()
		{
			if (File.Exists(GetFullPath()))
			{
				using (StreamReader streamReader = new StreamReader(GetFullPath()))
				{
					if (_configureService.GetDocumentType().Equals("json"))
					{
						string file = streamReader.ReadToEnd();
						var readedStudents = JsonConvert.DeserializeObject<LinkedList<Student>>(file);
						var result = readedStudents ?? new LinkedList<Student>();
						var itemsToDelete = result.Count - _configureService.GetMaxLength();
						while (itemsToDelete-- > 0)
						{
							result.RemoveFirst();
						}
						return result;
					}
					else
					{
						var serializer = new XmlSerializer(typeof(List<Student>));
						using (var xmlReader = new XmlTextReader(streamReader))
						{
							if (!serializer.CanDeserialize(xmlReader))
							{
								return new LinkedList<Student>();
							}
							var readedStudents = serializer.Deserialize(xmlReader) as List<Student>;
							var temporaryResult = readedStudents ?? new List<Student>();
							var result = new LinkedList<Student>(temporaryResult);
							var itemsToDelete = result.Count - _configureService.GetMaxLength();
							while (itemsToDelete-- > 0)
							{
								result.RemoveFirst();
							}
							return result;
						}
					}
				}

			}
			else
			{
				if (_configureService.GetDocumentType().Equals("json"))
				{
					using (FileStream fstream = File.Create(GetFullPath()))
					{
					}
				}
				if (_configureService.GetDocumentType().Equals("xml"))
				{
					new XDocument(new XElement("root")).Save(GetFullPath());
				}
				return new LinkedList<Student>();
			}
		}

		public void WriteToFile(LinkedList<Student> students)
		{
			var incomingStudents = students;
			if (File.Exists(GetFullPath()))
			{
				if (students.Count > 0)
				{
					var itemsToRemove = students.Count - _configureService.GetMaxLength();
					while (itemsToRemove-- > 0)
					{
						incomingStudents.RemoveFirst();
					}
					using (StreamWriter streamWriter = new StreamWriter(GetFullPath()))
					{
						if (_configureService.GetDocumentType().Equals("json"))
						{
							var text = JsonConvert.SerializeObject(students);
							streamWriter.Write(text);
						}
						else
						{
							XmlSerializer serializer = new XmlSerializer(typeof(List<Student>));
							using (XmlTextWriter textWriter = new XmlTextWriter(streamWriter))
							{
								serializer.Serialize(textWriter, students.ToList());
							}


						}
					}
				}

			}
			else
			{
				if (_configureService.GetDocumentType().Equals("json"))
				{
					using (FileStream fstream = File.Create(GetFullPath()))
					{
					}
				}
				if (_configureService.GetDocumentType().Equals("xml"))
				{
					new XDocument(new XElement("root")).Save(GetFullPath());
				}
			}
		}
		private string GetFullPath()
		{
			return path + _configureService.GetDocumentType();
		}
	}
}
