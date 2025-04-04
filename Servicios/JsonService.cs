using System.Text.Json;

namespace Servicios;

public class JsonService<T>
{
    public List<T> Leer(string archivo)
    {
        if (!File.Exists(archivo)) return new List<T>();
        string json = File.ReadAllText(archivo);
        return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
    }

    public void Guardar(string archivo, List<T> datos)
    {
        string json = JsonSerializer.Serialize(datos, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(archivo, json);
    }
}