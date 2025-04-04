using Modelos;
using Servicios;

string ruta = "Datos/alumnos.json";
JsonService<Estudiante> jsonService = new JsonService<Estudiante>();
List<Estudiante> estudiantes = jsonService.Leer(ruta);

while (true)
{
    Console.Clear();
    Console.WriteLine("== MENU PRINCIPAL ==");
    Console.WriteLine("1) Agregar Estudiante");
    Console.WriteLine("2) Ver Estudiantes");
    Console.WriteLine("3) Salir");
    Console.Write("Opcion: ");
    string opcion = Console.ReadLine() ?? "" ;

    switch (opcion)
    {
        case "1": Agregar(estudiantes, jsonService, ruta);
        break;
        case "2": Ver(estudiantes);
        break;
        case "3": return;
        default: 
        Console.WriteLine("Opcion invalida.");
        break;

    }

    Console.WriteLine("Presione una tecla para continuar...");
    Console.ReadKey();
}

void Agregar(List<Estudiante> lista, JsonService<Estudiante> servicio, string ruta)
{
    Console.Write("DNI: ");
    string dni = Console.ReadLine() ?? "" ;
    Console.Write("Apellido: ");
    string apellido = Console.ReadLine() ?? "" ;
    Console.Write("Nombre: ");
    string nombre = Console.ReadLine() ?? "" ;
    Console.Write("Correo: ");
    string correo = Console.ReadLine() ?? "" ;

    Console.Write(" Guardar estudiante? (SI/NO): ");
    if ((Console.ReadLine() ?? "") .ToUpper() == "SI")
    { 
        lista.Add(new Estudiante 
        {
            Dni = dni,
            Apellido = apellido,
            Nombre = nombre,
            Correo = correo
        });

        servicio.Guardar(ruta, lista);
        Console.WriteLine("Guardado correctamente.");
    }
    else 
    {
        Console.WriteLine("Carga cancelada.");
    }
}

void Ver(List<Estudiante> lista)
{
    foreach (var e in lista)
    {
        Console.WriteLine($"{e.Apellido}, {e.Nombre} - DNI: {e.Dni} - {e.Correo} - Grupo: {e.CodigoGrupo}");
    }
}
