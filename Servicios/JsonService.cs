using Newtonsoft.Json;
using System.IO;

namespace Clase3Tp1.Servicios
{
    public static class JsonService
    {
        public static List<T> Cargar<T>(string archivo)
        {
            if (!File.Exists(archivo))
                return new List<T>();

            string json = File.ReadAllText(archivo);
            return JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
        }

        public static void Guardar<T>(string archivo, List<T> datos)
        {
            string json = JsonConvert.SerializeObject(datos, Formatting.Indented);
            File.WriteAllText(archivo, json);
        }
    }
}