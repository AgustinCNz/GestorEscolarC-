using Clase3Tp1.Modelos;
using Clase3Tp1.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Clase3Tp1.Servicios
{
    public static class AsistenciaService
    {
        public static void RegistrarAsistencia()
        {
            Console.Clear();
            var estudiantes = JsonService.Cargar<Estudiante>("Datos/alumnos.json");
            var asistencias = JsonService.Cargar<Asistencia>("Datos/asistencias.json");

            DateTime hoy = DateTime.Today;

            Console.WriteLine($"🗓 Registrando asistencia para el día: {hoy:yyyy-MM-dd}\n");

            var estudiantesOrdenados = estudiantes
                .Where(e => !string.IsNullOrWhiteSpace(e.CodigoGrupo))
                .OrderBy(e => e.Apellido)
                .ThenBy(e => e.Nombre)
                .ToList();

            foreach (var estudiante in estudiantesOrdenados)
            {
                Console.WriteLine($"👤 {estudiante.Apellido}, {estudiante.Nombre} (DNI: {estudiante.DNI})");
                Console.Write("Presione P si está Presente o A si está Ausente: ");

                var tecla = Console.ReadKey(true).Key;
                bool presente = false;

                if (tecla == ConsoleKey.P)
                {
                    presente = true;
                    Console.WriteLine("PRESENTE");
                }
                else if (tecla == ConsoleKey.A)
                {
                    presente = false;
                    Console.WriteLine("AUSENTE");
                }
                else
                {
                    Console.WriteLine("❌ Tecla inválida. Se marca como AUSENTE por defecto.");
                }

                asistencias.Add(new Asistencia
                {
                    DNI = estudiante.DNI,
                    Fecha = hoy,
                    EstaPresente = presente
                });
            }

            JsonService.Guardar("Datos/asistencias.json", asistencias);
            Console.WriteLine("\n✅ Asistencias registradas correctamente.");
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }



          public static void AsistenciasGeneral()
{
    Console.Clear();

    var estudiantes = JsonService.Cargar<Estudiante>("Datos/alumnos.json");
    var asistencias = JsonService.Cargar<Asistencia>("Datos/asistencias.json");

    // Obtener mes y año actual del sistema
    DateTime hoy = DateTime.Today;
    int mesActual = hoy.Month;
    int anioActual = hoy.Year;

    // Filtrar asistencias del mes y año actual
    var asistenciasMes = asistencias
        .Where(a => a.Fecha.Month == mesActual && a.Fecha.Year == anioActual)
        .ToList();

    // Calcular clases dictadas en el mes actual
    int clasesDelMes = asistenciasMes
        .Select(a => a.Fecha.ToString("yyyy-MM-dd"))
        .Distinct()
        .Count();

    Console.WriteLine($"📆 Reporte del mes: {mesActual:00}/{anioActual}");
    Console.WriteLine($"🧾 Total de clases dictadas este mes: {clasesDelMes}\n");

    Console.WriteLine("{0,-15} {1,-15} {2,-20} {3,10} {4,10} {5,15} {6,15}",
        "DNI", "Apellido", "Nombre", "Presente", "Ausente", "% Asistencia", "Clases del mes");
    Console.WriteLine(new string('-', 100));

    foreach (var estudiante in estudiantes)
    {
        var asistenciasAlumno = asistenciasMes
            .Where(a => a.DNI == estudiante.DNI)
            .ToList();

        int presentes = asistenciasAlumno.Count(a => a.EstaPresente);
        int ausentes = asistenciasAlumno.Count(a => !a.EstaPresente);
        double porcentaje = asistenciasAlumno.Count > 0
            ? (presentes * 100.0 / asistenciasAlumno.Count)
            : 0;

        Console.WriteLine("{0,-15} {1,-15} {2,-20} {3,10} {4,10} {5,15:F1}% {6,15}",
            estudiante.DNI, estudiante.Apellido, estudiante.Nombre,
            presentes, ausentes, porcentaje, clasesDelMes);
    }

    Console.WriteLine("\nPresione una tecla para continuar...");
    Console.ReadKey();
}


public static void AsistenciaPorDniOApellido()
{
    Console.Clear();

    var estudiantes = JsonService.Cargar<Estudiante>("Datos/alumnos.json");
    var asistencias = JsonService.Cargar<Asistencia>("Datos/asistencias.json");

    Console.WriteLine("📌 Buscar alumno por:");
    Console.WriteLine("1. DNI");
    Console.WriteLine("2. Apellido");
    Console.Write("Opción: ");
    string opcion = Console.ReadLine()?.Trim() ?? "";

    Estudiante? estudiante = null;

    if (opcion == "1")
    {
        Console.Write("Ingrese el DNI: ");
        string dni = Console.ReadLine()?.Trim() ?? "";
        estudiante = estudiantes.Find(e => e.DNI == dni);
    }
    else if (opcion == "2")
    {
        Console.Write("Ingrese el Apellido: ");
        string apellido = Console.ReadLine()?.Trim() ?? "";
        estudiante = estudiantes.Find(e => e.Apellido.Equals(apellido, StringComparison.OrdinalIgnoreCase));
    }
    else
    {
        Console.WriteLine("⚠ Opción inválida.");
        Console.ReadKey();
        return;
    }

    if (estudiante == null)
    {
        Console.WriteLine("❌ Estudiante no encontrado.");
        Console.ReadKey();
        return;
    }

    var asistenciasAlumno = asistencias.Where(a => a.DNI == estudiante.DNI).ToList();
    int presentes = asistenciasAlumno.Count(a => a.EstaPresente);
    int ausentes = asistenciasAlumno.Count(a => !a.EstaPresente);
    double porcentaje = asistenciasAlumno.Count > 0
        ? (presentes * 100.0 / asistenciasAlumno.Count)
        : 0;

/*Console.WriteLine("{0,-15} {1,-20} {2,-10}", "DNI", "Nombre", "Asistencia");
Console.WriteLine("{0,-15} {1,-20} {2,-10}", estudiante.DNI, estudiante.Nombre, estudiante.Presente);
   */

    // Console.WriteLine($"\n📋 Asistencias de {estudiante.Nombre} {estudiante.Apellido} (DNI: {estudiante.DNI}):");
    // Console.WriteLine($"✅ Presentes: {presentes}");
    // Console.WriteLine($"❌ Ausentes: {ausentes}");
    // Console.WriteLine($"📊 Porcentaje de asistencia: {porcentaje:F1}%");
 Console.WriteLine();
    Console.WriteLine("📋 Registro de asistencia del estudiante:\n");
    Console.WriteLine("{0,-15} {1,-15} {2,-20} {3,25} {4,10} {5,15}", "DNI", "Apellido", "Nombre", "Presente", "Ausente", "% Asistencia");
    Console.WriteLine(new string('-', 90));
    Console.WriteLine("{0,-15} {1,-15} {2,-20} {3,25} {4,10} {5,14:F1}%", 
        estudiante.DNI, estudiante.Apellido, estudiante.Nombre, presentes, ausentes, porcentaje);



    Console.WriteLine("\nPresione una tecla para continuar...");
    Console.ReadKey();
}


public static void VerAsistenciaPorMesAnterior()
{
    Console.Clear();
    var estudiantes = JsonService.Cargar<Estudiante>("Datos/alumnos.json");
    var asistencias = JsonService.Cargar<Asistencia>("Datos/asistencias.json");

    // Agrupar por Mes y Año
    var fechasDisponibles = asistencias
        .Select(a => a.Fecha)
        .GroupBy(f => new { f.Year, f.Month })
        .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month)
        .ToList();

    if (fechasDisponibles.Count == 0)
    {
        Console.WriteLine("⚠ No hay registros de asistencia disponibles.");
        Console.ReadKey();
        return;
    }

    Console.WriteLine("📅 Seleccione un mes para ver el reporte:");
    for (int i = 0; i < fechasDisponibles.Count; i++)
    {
        var fecha = fechasDisponibles[i].Key;
        string nombreMes = new DateTime(fecha.Year, fecha.Month, 1).ToString("MMMM").ToUpper();
        Console.WriteLine($"{i + 1}) {nombreMes} {fecha.Year}");
    }

    Console.Write("Opción: ");
    if (!int.TryParse(Console.ReadLine(), out int seleccion) || seleccion < 1 || seleccion > fechasDisponibles.Count)
    {
        Console.WriteLine("❌ Opción inválida.");
        Console.ReadKey();
        return;
    }

    var fechaSeleccionada = fechasDisponibles[seleccion - 1].Key;
    var asistenciasFiltradas = asistencias
        .Where(a => a.Fecha.Month == fechaSeleccionada.Month && a.Fecha.Year == fechaSeleccionada.Year)
        .ToList();

    var totalClases = asistenciasFiltradas
        .Select(a => a.Fecha.ToString("yyyy-MM-dd"))
        .Distinct()
        .Count();

    Console.WriteLine($"\n🧾 Total de clases dictadas en el mes: {totalClases}\n");
    Console.WriteLine("📋 Registro de asistencias:\n");
    Console.WriteLine("{0,-15} {1,-15} {2,-20} {3,10} {4,10} {5,14}", "DNI", "Apellido", "Nombre", "Presente", "Ausente", "% Asistencia");
    Console.WriteLine(new string('-', 90));

    foreach (var estudiante in estudiantes)
    {
        var asistenciasAlumno = asistenciasFiltradas.Where(a => a.DNI == estudiante.DNI).ToList();
        int presentes = asistenciasAlumno.Count(a => a.EstaPresente);
        int ausentes = asistenciasAlumno.Count(a => !a.EstaPresente);
        double porcentaje = asistenciasAlumno.Count > 0
            ? (presentes * 100.0 / asistenciasAlumno.Count)
            : 0;

        Console.WriteLine("{0,-15} {1,-15} {2,-20} {3,10} {4,10} {5,13:F1}%", 
            estudiante.DNI, estudiante.Apellido, estudiante.Nombre, presentes, ausentes, porcentaje);
    }

    Console.WriteLine("\nPresione una tecla para continuar...");
    Console.ReadKey();
}


    }
}
