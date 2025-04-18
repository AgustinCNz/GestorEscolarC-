using Clase3Tp1.Modelos;
using Clase3Tp1.Servicios; // Para acceder a JsonService
using System;
using System.Collections.Generic;
using System.Linq; // Este es para poder usar .Any() y .All() y el .OrderBy()


namespace Clase3Tp1.Servicios
{
    public static class EstudianteService
    {
        private static string archivoEstudiantes = "Datos/alumnos.json";

        private static string LeerDato(string mensaje)
        {
            string dato;
            do
            {
                Console.Write(mensaje);
                dato = Console.ReadLine()?.Trim() ?? "";

                if (string.IsNullOrEmpty(dato))
                {
                    Console.WriteLine("⚠ El campo no puede estar vacío. Intente de nuevo.");
                }

            } while (string.IsNullOrEmpty(dato));

            return dato;
        }

        private static void MostrarDatosEstudiante(Estudiante estudiante)
        {
            Console.WriteLine("\n📌 Información del estudiante:");
            Console.WriteLine($"📌 DNI: {estudiante.DNI}");
            Console.WriteLine($"📌 Apellido: {estudiante.Apellido}");
            Console.WriteLine($"📌 Nombre: {estudiante.Nombre}");
            Console.WriteLine($"📌 Correo: {estudiante.Correo}");
            Console.WriteLine($"📌 Código de Grupo: {estudiante.CodigoGrupo}");
        }

        public static void AltaEstudiante()
{
    Console.Clear();
    var estudiantes = JsonService.Cargar<Estudiante>(archivoEstudiantes);

    while (true)
    {
        string dni = LeerDato("DNI (ingrese solo numeros*): ");

        if (!dni.All(char.IsDigit))
        {
            Console.WriteLine("⚠ El DNI solo debe contener números.");
            continue;
        }

        if (estudiantes.Any(e => e.DNI == dni))
        {
            Console.WriteLine("⚠ Ya existe un estudiante con ese DNI. Intente con otro.");
            continue;
        }

        string apellido = LeerDato("Apellido: ");
        if (!apellido.All(char.IsLetter))
        {
            Console.WriteLine("⚠ El apellido solo debe contener letras.");
            continue;
        }

        string nombre = LeerDato("Nombre: ");
        if (!nombre.All(char.IsLetter))
        {
            Console.WriteLine("⚠ El nombre solo debe contener letras.");
            continue;
        }

        string correo = LeerDato("Correo Electrónico: ");
        if (!correo.EndsWith("@gmail.com") && !correo.EndsWith("@hotmail.com"))
        {
            Console.WriteLine("⚠ Solo se aceptan correos @gmail.com o @hotmail.com.");
            continue;
        }

        var nuevoEstudiante = new Estudiante
        {
            DNI = dni,
            Apellido = apellido,
            Nombre = nombre,
            Correo = correo
        };

        MostrarDatosEstudiante(nuevoEstudiante);

        Console.WriteLine("\n¿Desea guardar este estudiante?");
        Console.WriteLine("1. Sí");
        Console.WriteLine("2. No");
        Console.Write("Opción: ");
        string opcion = Console.ReadLine()?.Trim() ?? "";

        switch (opcion)
        {
            case "1":
                estudiantes.Add(nuevoEstudiante);
                JsonService.Guardar(archivoEstudiantes, estudiantes);
                Console.WriteLine("✅ Estudiante agregado correctamente.");
                return;

            case "2":
                Console.WriteLine("❌ Operación cancelada. Vuelva al menú principal.");
                return;

            default:
                Console.WriteLine("⚠ Opción inválida. Ingrese 1 para Sí o 2 para No.");
                Console.ReadKey();
                Console.Clear();
                break;
        }
    }
}



     public static void BuscarYMostrarEstudiantePorDniOApellido()
{
    Console.Clear();
    var estudiantes = JsonService.Cargar<Estudiante>(archivoEstudiantes);

    while (true)
    {
        Console.WriteLine("Buscar estudiante por:");
        Console.WriteLine("1. DNI");
        Console.WriteLine("2. Apellido");
        Console.WriteLine("3. Ver todos los estudiantes");
        Console.WriteLine("4. Volver al menú anterior");
        Console.Write("Opción: ");
        string opcion = Console.ReadLine()?.Trim() ?? "";

        if (opcion == "4")
        {
            Console.WriteLine("↩ Volviendo al menú...");
            return;
        }

        if (opcion == "3")
        {
            Console.Clear();
            Console.WriteLine("📋 Lista de todos los estudiantes:\n");
            Console.WriteLine("{0,-15} {1,-15} {2,-20} {3,-10}", "DNI", "Apellido", "Nombre", "Grupo");
            Console.WriteLine(new string('-', 60));

            foreach (var est in estudiantes.OrderBy(e => e.Apellido))
            {
                Console.WriteLine("{0,-15} {1,-15} {2,-20} {3,-10}", est.DNI, est.Apellido, est.Nombre, est.CodigoGrupo);
            }

            Console.WriteLine("\nPresione una tecla para volver al menú de búsqueda...");
            Console.ReadKey();
            Console.Clear();
            continue;
        }

        Estudiante? encontrado = null;

        if (opcion == "1")
        {
            string dni = LeerDato("Ingrese el DNI: ");
            encontrado = estudiantes.Find(e => e.DNI == dni);
        }
        else if (opcion == "2")
        {
            string apellido = LeerDato("Ingrese el Apellido: ");
            encontrado = estudiantes.Find(e => e.Apellido?.Equals(apellido, StringComparison.OrdinalIgnoreCase) ?? false);
        }
        else
        {
            Console.WriteLine("⚠ Opción inválida.");
            Console.ReadKey();
            Console.Clear();
            continue;
        }

        if (encontrado != null)
        {
            Console.WriteLine("\n✅ Estudiante encontrado:");
            MostrarDatosEstudiante(encontrado);
            Console.WriteLine("\nPresione una tecla para volver al menú de búsqueda...");
            Console.ReadKey();
            Console.Clear();
        }
        else
        {
            Console.WriteLine("❌ No se encontró ningún estudiante con ese dato.");
            Console.WriteLine("¿Desea volver a intentarlo?");
            Console.WriteLine("1. Sí");
            Console.WriteLine("2. No");
            string continuar = Console.ReadLine()?.Trim() ?? "";
            if (continuar != "1") return;
            Console.Clear();
        }
    }
}

     public static Estudiante? BuscarEstudiantePorDniOApellido(string titulo = "Buscar estudiante por:")
{
    var estudiantes = JsonService.Cargar<Estudiante>(archivoEstudiantes);

    while (true)
    {
        Console.Clear();
        Console.WriteLine(titulo);
        Console.WriteLine("1. DNI");
        Console.WriteLine("2. Apellido");
        Console.WriteLine("3. Volver");
        Console.Write("Opción: ");
        string opcion = Console.ReadLine()?.Trim() ?? "";

        if (opcion == "1")
        {
            string dni = LeerDato("Ingrese el DNI: ");
            return estudiantes.Find(e => e.DNI == dni);
        }
        else if (opcion == "2")
        {
            string apellido = LeerDato("Ingrese el Apellido: ");
            return estudiantes.Find(e => e.Apellido?.Equals(apellido, StringComparison.OrdinalIgnoreCase) ?? false);
        }
        else if (opcion == "3")
        {
            return null;
        }
        else
        {
            Console.WriteLine("⚠ Opción inválida.");
            Console.ReadKey();
        }
    }
}


      public static void ModificarEstudiante()
{
    Console.Clear();
    var estudiantes = JsonService.Cargar<Estudiante>(archivoEstudiantes);

    Estudiante? estudiante = BuscarEstudiantePorDniOApellido("Modificar estudiante por:");

    if (estudiante == null)
    {
        Console.WriteLine("⚠ Estudiante no encontrado.");
        Console.ReadKey();
        return;
    }

    int index = estudiantes.FindIndex(e => e.DNI == estudiante.DNI);
    if (index == -1)
    {
        Console.WriteLine("⚠ No se pudo encontrar al estudiante en la lista original.");
        Console.ReadKey();
        return;
    }

    MostrarDatosEstudiante(estudiante);

    bool seguirModificando = true;
    while (seguirModificando)
    {
        Console.WriteLine("\nSeleccione el dato a modificar:");
        Console.WriteLine("1. CAMBIAR DNI");
        Console.WriteLine("2. CAMBIAR Apellido");
        Console.WriteLine("3. CAMBIAR Nombre");
        Console.WriteLine("4. CAMBIAR Correo");
        Console.WriteLine("5. CAMBIAR Código de Grupo");
        Console.WriteLine("6. Modificar todos");
        Console.WriteLine("7. Salir sin modificar");
        Console.Write("Opción: ");
        string opcion = Console.ReadLine()?.Trim() ?? "";

        switch (opcion)
        {
            case "1":
                string nuevoDni = LeerDato("Nuevo DNI: ");
                if (!nuevoDni.All(char.IsDigit))
                {
                    Console.WriteLine("⚠ El DNI solo debe contener números.");
                    continue;
                }
                if (estudiantes.Any(e => e.DNI == nuevoDni && e != estudiante))
                {
                    Console.WriteLine("⚠ Ya existe otro estudiante con ese DNI.");
                    continue;
                }
                estudiante.DNI = nuevoDni;
                break;

            case "2":
                string nuevoApellido = LeerDato("Nuevo Apellido: ");
                if (!nuevoApellido.All(char.IsLetter))
                {
                    Console.WriteLine("⚠ El apellido solo debe contener letras.");
                    continue;
                }
                estudiante.Apellido = nuevoApellido;
                break;

            case "3":
                string nuevoNombre = LeerDato("Nuevo Nombre: ");
                if (!nuevoNombre.All(char.IsLetter))
                {
                    Console.WriteLine("⚠ El nombre solo debe contener letras.");
                    continue;
                }
                estudiante.Nombre = nuevoNombre;
                break;

            case "4":
                string nuevoCorreo = LeerDato("Nuevo Correo: ");
                if (!nuevoCorreo.EndsWith("@gmail.com") && !nuevoCorreo.EndsWith("@hotmail.com"))
                {
                    Console.WriteLine("⚠ Solo se aceptan correos @gmail.com o @hotmail.com.");
                    continue;
                }
                estudiante.Correo = nuevoCorreo;
                break;

            case "5":
                estudiante.CodigoGrupo = LeerDato("Nuevo Código de Grupo: ");
                break;

            case "6":
                goto case "1"; // Reutilizamos validaciones
            case "7":
                Console.WriteLine("❌ Modificación cancelada.");
                return;
            default:
                Console.WriteLine("⚠ Opción inválida.");
                continue;
        }

        Console.WriteLine("✅ Modificación realizada correctamente.");
        Console.WriteLine("\n¿Desea seguir modificando datos de este estudiante?");
        Console.WriteLine("1. Sí");
        Console.WriteLine("2. No, guardar cambios");
        string continuar = Console.ReadLine()?.Trim() ?? "";
        if (continuar != "1") seguirModificando = false;
    }

    estudiantes[index] = estudiante;
    JsonService.Guardar(archivoEstudiantes, estudiantes);
    Console.WriteLine("✅ Cambios guardados correctamente.");
    Console.ReadKey();
}


    public static void EliminarEstudiante()
{
    Console.Clear();
    var estudiantes = JsonService.Cargar<Estudiante>(archivoEstudiantes);

    if (estudiantes.Count == 0)
    {
        Console.WriteLine("⚠ No hay estudiantes registrados.");
        Console.ReadKey();
        return;
    }

    Console.WriteLine("📋 Lista de estudiantes:");
    for (int i = 0; i < estudiantes.Count; i++)
    {
        var e = estudiantes[i];
        Console.WriteLine($"[{i + 1}] {e.Apellido}, {e.Nombre} (DNI: {e.DNI})");
    }

    Console.Write("\nIngrese los números de los estudiantes a eliminar (separados por coma): ");
    string entrada = Console.ReadLine()?.Trim() ?? "";
    var indices = entrada.Split(',')
                         .Select(n => int.TryParse(n.Trim(), out int i) ? i - 1 : -1)
                         .Where(i => i >= 0 && i < estudiantes.Count)
                         .Distinct()
                         .ToList();

    if (indices.Count == 0)
    {
        Console.WriteLine("❌ No se seleccionó ningún estudiante válido.");
        Console.ReadKey();
        return;
    }

    Console.WriteLine("\n📌 Estudiantes seleccionados para eliminar:");
    foreach (var i in indices)
    {
        var e = estudiantes[i];
        Console.WriteLine($"- {e.Apellido}, {e.Nombre} (DNI: {e.DNI})");
    }

    Console.Write("\n¿Desea confirmar la eliminación? (1. Sí / 2. No): ");
    string confirmacion = Console.ReadLine()?.Trim() ?? "";

    if (confirmacion == "1")
    {
        foreach (var i in indices.OrderByDescending(i => i))
        {
            estudiantes.RemoveAt(i);
        }

        JsonService.Guardar(archivoEstudiantes, estudiantes);
        Console.WriteLine("✅ Estudiantes eliminados correctamente.");
    }
    else
    {
        Console.WriteLine("❌ Operación cancelada.");
    }

    Console.WriteLine("\nPresione una tecla para continuar...");
    Console.ReadKey();
}


public static void SorteoEstudiante()
{
    Console.Clear();
    var estudiantes = JsonService.Cargar<Estudiante>("Datos/alumnos.json");
    var asistencias = JsonService.Cargar<Asistencia>("Datos/asistencias.json");

    string fechaHoy = DateTime.Today.ToString("yyyy-MM-dd");

    // Filtrar estudiantes presentes hoy
    var presentesHoy = asistencias
        .Where(a => a.Fecha.ToString("yyyy-MM-dd") == fechaHoy && a.EstaPresente)
        .Select(a => a.DNI)
        .ToList();

    var candidatos = estudiantes
        .Where(e => presentesHoy.Contains(e.DNI))
        .ToList();

    if (candidatos.Count == 0)
    {
        Console.WriteLine("⚠ No hay estudiantes presentes hoy para realizar el sorteo.");
        Console.ReadKey();
        return;
    }

    // Verificar si todos ya participaron
    if (candidatos.All(e => e.Participado))
    {
        Console.WriteLine("🔁 Todos los estudiantes ya participaron. Reiniciando participación...");
        foreach (var e in candidatos)
        {
            e.Participado = false;
        }
        JsonService.Guardar("Datos/alumnos.json", estudiantes);
    }

    // Buscar estudiantes que no participaron aún
    var noParticiparon = candidatos
        .Where(e => !e.Participado)
        .ToList();

    // Mostrar uno aleatorio
    var random = new Random();
    var seleccionado = noParticiparon[random.Next(noParticiparon.Count)];

    Console.WriteLine($"\n🎯 Estudiante seleccionado: {seleccionado.Nombre} {seleccionado.Apellido} (DNI: {seleccionado.DNI})");

    Console.Write("\n¿Confirmar participación? (1. Sí / 2. No): ");
    string? confirmacion = Console.ReadLine()?.Trim();

    if (confirmacion == "1")
    {
        seleccionado.Participado = true;
        JsonService.Guardar("Datos/alumnos.json", estudiantes);
        Console.WriteLine("✅ Participación registrada.");
    }
    else
    {
        Console.WriteLine("❌ No se registró la participación.");
    }

    Console.WriteLine("\nPresione una tecla para continuar...");
    Console.ReadKey();
}

    }
}
