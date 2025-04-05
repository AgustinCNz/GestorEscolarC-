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

    // Calcular total de clases dictadas por fechas distintas
    int totalClases = asistencias
        .Select(a => a.Fecha.ToString("yyyy-MM-dd"))
        .Distinct()
        .Count();

    Console.WriteLine($"🧾 Total de clases dictadas: {totalClases}\n");

    Console.WriteLine("📋 Registro general de asistencias:\n");
    Console.WriteLine("DNI\tApellido\tNombre\tPresente\tAusente\t% Asistencia");

    foreach (var estudiante in estudiantes)
    {
        var asistenciasAlumno = asistencias.Where(a => a.DNI == estudiante.DNI).ToList();
        int presentes = asistenciasAlumno.Count(a => a.EstaPresente);
        int ausentes = asistenciasAlumno.Count(a => !a.EstaPresente);
        double porcentaje = asistenciasAlumno.Count > 0
            ? (presentes * 100.0 / asistenciasAlumno.Count)
            : 0;

        Console.WriteLine($"{estudiante.DNI}\t{estudiante.Apellido}\t{estudiante.Nombre}\t{presentes}\t\t{ausentes}\t{porcentaje:F1}%");
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

    Console.WriteLine($"\n📋 Asistencias de {estudiante.Nombre} {estudiante.Apellido} (DNI: {estudiante.DNI}):");
    Console.WriteLine($"✅ Presentes: {presentes}");
    Console.WriteLine($"❌ Ausentes: {ausentes}");
    Console.WriteLine($"📊 Porcentaje de asistencia: {porcentaje:F1}%");

    Console.WriteLine("\nPresione una tecla para continuar...");
    Console.ReadKey();
}



    }
}
