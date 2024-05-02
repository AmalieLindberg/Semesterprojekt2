using Semesterprojekt2.Models;
using System.Text.Json;

namespace Semesterprojekt2.Service.UserService
{
	public class JsonFileUserService
	{
		public IWebHostEnvironment WebHostEnvironment { get; }

		public JsonFileUserService(IWebHostEnvironment webHostEnvironment)
		{
			WebHostEnvironment = webHostEnvironment;
		}

		private string JsonFileName
		{
			get { return Path.Combine(WebHostEnvironment.WebRootPath, "Data", "User.json"); }
		}

		public void SaveJsonUsers(List<Users> users)
		{
			using (FileStream jsonFileWriter = File.Create(JsonFileName))
			{
				Utf8JsonWriter jsonWriter = new Utf8JsonWriter(jsonFileWriter, new JsonWriterOptions()
				{
					SkipValidation = false,
					Indented = true
				});
				JsonSerializer.Serialize<Users[]>(jsonWriter,  users.ToArray());
			}
		}

		public IEnumerable<Users> GetJsonUsers()
		{
			using (StreamReader jsonFileReader = File.OpenText(JsonFileName))
			{
				return JsonSerializer.Deserialize<Users[]>(jsonFileReader.ReadToEnd());
			}
		}
	}
}
