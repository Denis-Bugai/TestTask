
namespace TestTaskDenysBuhai.Services
{
	public class ConfigureService : IConfigureService
	{
		private string _documentType;
		private int _maxLength;
		private string _path;
		public ConfigureService()
		{
			_documentType = "json";
			_maxLength = 10;
			_path = "/students.";
		}
		public string GetDocumentType()
		{
			return _documentType;
		}

		public string GetFullPath()
		{
			return $"{_path}{_documentType}";
		}

		public int GetMaxLength()
		{
			return _maxLength;
		}

		public string GetSettings()
		{
			return $"Max length: {_maxLength} Document type: {_documentType}";
		}

		public bool SetSettings(string documentType, int maxLength)
		{
			var docType = documentType.ToLower();
			if (docType.Equals("json"))
			{
				if (maxLength > 0)
				{
					_documentType = docType;
					_maxLength = maxLength;
					return true;
				}
			}

			if (docType.Equals("xml"))
			{
				if (maxLength > 0)
				{
					_documentType = docType;
					_maxLength = maxLength;
					return true;
				}
			}

			return false;

		}
	}
}
