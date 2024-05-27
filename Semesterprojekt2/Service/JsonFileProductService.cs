using System.Text.Json;
using Semesterprojekt2.Models.Shop;

namespace Semesterprojekt2.Service
{
    public class JsonFileProductService
    {
        public IWebHostEnvironment WebHostEnvironment { get; }

        public JsonFileProductService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        //Returnerer filstien til JSON-filen
        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "Data", "Products.json"); }
        }


        public void SaveJsonProducts(List<Product> products)
        {
            // Opretter en filstream til at skrive til JSON-filen
            using (FileStream jsonFileWriter = File.Create(JsonFileName))
            {
                // Opretter en Utf8JsonWriter til at skrive JSON-data til filen
                Utf8JsonWriter jsonWriter = new Utf8JsonWriter(jsonFileWriter, new JsonWriterOptions()
                {
                    SkipValidation = false,// Validering af JSON-struktur er ikke deaktiveret
                    Indented = true// Indrykker JSON-data for bedre læsbarhed
                });
                // Serialiserer listen af produkter og skriver dem til filen som JSON
                JsonSerializer.Serialize<Product[]>(jsonWriter, products.ToArray());
            }
        }

        public IEnumerable<Product> GetJsonProducts()
        {
            // Åbner JSON-filen til læsning
            using (StreamReader jsonFileReader = File.OpenText(JsonFileName))
            {
                // Læser hele filens indhold som en string og deserialiserer det til en liste af produkter
                return JsonSerializer.Deserialize<Product[]>(jsonFileReader.ReadToEnd());
            }
        }
    }
}

