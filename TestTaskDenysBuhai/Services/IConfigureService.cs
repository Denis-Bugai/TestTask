
namespace TestTaskDenysBuhai.Services
{
	public interface IConfigureService
	{
		string GetSettings();
		bool SetSettings(string documentType, int maxLenght);
		string GetDocumentType();
		int GetMaxLength();
	}
}
