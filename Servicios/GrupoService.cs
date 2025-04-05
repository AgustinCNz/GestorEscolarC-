using Clase3Tp1.Modelos;
using Clase3Tp1.Servicios;
using System;
using System.Collections.Generic;

namespace Clase3Tp1.Servicios
{
    public static class GrupoService
    {
        private static string archivoGrupos = "Datos/grupos.json";
        private static string archivoEstudiantes = "Datos/alumnos.json";

        public static void CrearGrupo()
        {
            Console.Clear();

            var grupos = JsonService.Cargar<Grupo>(archivoGrupos);
            var estudiantes = JsonService.Cargar<Estudiante>(archivoEstudiantes);

            string codigoGrupo = LeerDato("Ingrese el código del grupo: ");
            List<string> integrantes = new();

            while (integrantes.Count < 6)
            {
                string dni = LeerDato("Ingrese el DNI del estudiante (o escriba FINALIZAR): ");
                if (dni.ToUpper() == "FINALIZAR") break;

                Estudiante? estudiante = estudiantes.Find(e => e.DNI == dni);
                if (estudiante == null)
                {
                    Console.WriteLine("⚠ El estudiante no está registrado.");
                    continue;
                }

                if (!string.IsNullOrEmpty(estudiante.CodigoGrupo))
                {
                    Console.WriteLine($"⚠ El estudiante ya pertenece al grupo {estudiante.CodigoGrupo}.");
                    Console.Write("¿Desea cambiarlo a este grupo? (SI/NO): ");
                    string respuesta = Console.ReadLine()?.Trim().ToUpper() ?? "NO";
                    if (respuesta != "SI") continue;
                }

                estudiante.CodigoGrupo = codigoGrupo;
                integrantes.Add(dni);
            }

            grupos.Add(new Grupo { CodigoGrupo = codigoGrupo, EstudiantesDNI = integrantes });
            JsonService.Guardar(archivoGrupos, grupos);
            JsonService.Guardar(archivoEstudiantes, estudiantes);

            Console.WriteLine("✅ Grupo creado exitosamente.");
            Console.ReadKey();
        }
        

        public static void ModificarGrupo()
{
    Console.Clear();
    var estudiantes = JsonService.Cargar<Estudiante>("Datos/alumnos.json");

    string codigoGrupo = LeerDato("Ingrese el código del grupo a modificar: ");
    List<Estudiante> grupo = estudiantes.FindAll(e => e.CodigoGrupo == codigoGrupo);

    if (grupo.Count == 0)
    {
        Console.WriteLine("⚠ No hay estudiantes en este grupo o el grupo no existe.");
        Console.ReadKey();
        return;
    }

    Console.WriteLine("\n📌 Estudiantes en el grupo:");
    int index = 1;
    foreach (var estudiante in grupo)
    {
        Console.WriteLine($"{index}. {estudiante.Nombre} {estudiante.Apellido} - DNI: {estudiante.DNI}");
        index++;
    }

    Console.WriteLine("\nSeleccione una opción:");
    Console.WriteLine("1. Agregar estudiante");
    Console.WriteLine("2. Eliminar estudiante");
    Console.Write("Opción: ");
    string opcion = Console.ReadLine()?.Trim() ?? "";

    if (opcion == "1")
    {
        AgregarEstudianteAGrupo(estudiantes, codigoGrupo);
    }
    else if (opcion == "2")
    {
        EliminarEstudianteDeGrupo(estudiantes, codigoGrupo);
    }
    else
    {
        Console.WriteLine("⚠ Opción no válida.");
    }

    JsonService.Guardar("Datos/alumnos.json", estudiantes);
    Console.ReadKey();
}


private static void AgregarEstudianteAGrupo(List<Estudiante> estudiantes, string codigoGrupo)
{
    while (true)
    {
        string dni = LeerDato("Ingrese el DNI del estudiante (o escriba FINALIZAR): ");
        if (dni.ToUpper() == "FINALIZAR") break;

        var estudiante = estudiantes.Find(e => e.DNI == dni);
        if (estudiante == null)
        {
            Console.WriteLine("⚠ El estudiante no está registrado.");
            continue;
        }

        if (!string.IsNullOrEmpty(estudiante.CodigoGrupo))
        {
            Console.WriteLine($"⚠ El estudiante ya pertenece al grupo {estudiante.CodigoGrupo}.");
            Console.Write("¿Desea cambiarlo a este grupo? (SI/NO): ");
            string respuesta = Console.ReadLine()?.Trim().ToUpper() ?? "NO";
            if (respuesta != "SI") continue;
        }

        estudiante.CodigoGrupo = codigoGrupo;
        Console.WriteLine("✅ Estudiante agregado al grupo.");
    }
}

private static void EliminarEstudianteDeGrupo(List<Estudiante> estudiantes, string codigoGrupo)
{
    string dni = LeerDato("Ingrese el DNI del estudiante a eliminar: ");
    var estudiante = estudiantes.Find(e => e.DNI == dni && e.CodigoGrupo == codigoGrupo);
    
    if (estudiante == null)
    {
        Console.WriteLine("⚠ El estudiante no pertenece a este grupo.");
        return;
    }

    Console.WriteLine($"\n📌 ¿Desea eliminar a {estudiante.Nombre} {estudiante.Apellido} del grupo {codigoGrupo}?");
    Console.WriteLine("1. Sí");
    Console.WriteLine("2. No");
    Console.Write("Opción: ");
    string ? confirmacion = Console.ReadLine()?.Trim();

    if (confirmacion == "1")
    {
        estudiante.CodigoGrupo = "";
        Console.WriteLine("✅ Estudiante eliminado del grupo.");
    }
    else
    {
        Console.WriteLine("❌ Operación cancelada.");
    }
}



public static void MoverEstudianteDeGrupo()
{
    Console.Clear();
    var estudiantes = JsonService.Cargar<Estudiante>("Datos/alumnos.json");

    string dni = LeerDato("Ingrese el DNI del estudiante a mover: ");
    Estudiante? estudiante = estudiantes.Find(e => e.DNI == dni);

    if (estudiante == null || string.IsNullOrEmpty(estudiante.CodigoGrupo))
    {
        Console.WriteLine("⚠ Estudiante no encontrado o no pertenece a un grupo.");
        Console.ReadKey();
        return;
    }

    Console.WriteLine("\n📌 Datos del estudiante:");
    Console.WriteLine($"📌 Nombre: {estudiante.Nombre}");
    Console.WriteLine($"📌 Apellido: {estudiante.Apellido}");
    Console.WriteLine($"📌 DNI: {estudiante.DNI}");
    Console.WriteLine($"Actualmente en el grupo: {estudiante.CodigoGrupo}");

    string nuevoGrupo = LeerDato("Ingrese el código del nuevo grupo: ");
    List<Estudiante> grupoDestino = estudiantes.FindAll(e => e.CodigoGrupo == nuevoGrupo);

    if (grupoDestino.Count >= 6)
    {
        Console.WriteLine("⚠ El grupo ya tiene 6 integrantes. No se puede realizar el cambio.");
    }
    else
    {
        Console.Write("¿Está seguro de mover a este estudiante al nuevo grupo? (SI/NO): ");
        string confirmacion = Console.ReadLine()?.Trim().ToUpper() ?? "NO";

        if (confirmacion == "SI")
        {
            estudiante.CodigoGrupo = nuevoGrupo;
            JsonService.Guardar("Datos/alumnos.json", estudiantes);
            Console.WriteLine("✅ Estudiante movido correctamente.");
        }
        else
        {
            Console.WriteLine("❌ Operación cancelada.");
        }
    }

    Console.WriteLine("\nPresione una tecla para continuar...");
    Console.ReadKey();
}


public static void EliminarGrupo()
{
    Console.Clear();
    var estudiantes = JsonService.Cargar<Estudiante>("Datos/alumnos.json");

    string codigoGrupo = LeerDato("Ingrese el código del grupo a eliminar: ");

    var estudiantesDelGrupo = estudiantes.FindAll(e => e.CodigoGrupo == codigoGrupo);

    if (estudiantesDelGrupo.Count == 0)
    {
        Console.WriteLine("⚠ No se encontraron estudiantes en ese grupo.");
        Console.ReadKey();
        return;
    }

    Console.WriteLine("\n📌 Estudiantes que serán removidos del grupo:");
    foreach (var estudiante in estudiantesDelGrupo)
    {
        Console.WriteLine($"- {estudiante.Nombre} {estudiante.Apellido} (DNI: {estudiante.DNI})");
    }

    Console.Write("\n¿Está seguro de eliminar el grupo y remover a estos estudiantes? (1. Sí / 2. No): ");
    string ? confirmacion = Console.ReadLine()?.Trim();

    if (confirmacion == "1")
    {
        foreach (var estudiante in estudiantesDelGrupo)
        {
            estudiante.CodigoGrupo = ""; // Dejar sin grupo
        }

        JsonService.Guardar("Datos/alumnos.json", estudiantes);
        Console.WriteLine("✅ Grupo eliminado y estudiantes sin grupo.");
    }
    else
    {
        Console.WriteLine("❌ Operación cancelada.");
    }

    Console.WriteLine("\nPresione una tecla para continuar...");
    Console.ReadKey();
}


public static void MostrarEstudiantesSinGrupo()
{
    Console.Clear();
    var estudiantes = JsonService.Cargar<Estudiante>("Datos/alumnos.json");

    var sinGrupo = estudiantes.FindAll(e => string.IsNullOrWhiteSpace(e.CodigoGrupo));

    if (sinGrupo.Count == 0)
    {
        Console.WriteLine("✅ Todos los estudiantes están asignados a algún grupo.");
    }
    else
    {
        Console.WriteLine("📋 Estudiantes sin grupo asignado:\n");
        foreach (var e in sinGrupo)
        {
            Console.WriteLine($"- {e.Apellido}, {e.Nombre} (DNI: {e.DNI})");
        }
    }

    Console.WriteLine("\nPresione una tecla para continuar...");
    Console.ReadKey();
}




public static void MostrarGruposConEstudiantes()
{
    Console.Clear();
    var estudiantes = JsonService.Cargar<Estudiante>("Datos/alumnos.json");

    // Obtener todos los grupos distintos
    var grupos = estudiantes
        .Where(e => !string.IsNullOrWhiteSpace(e.CodigoGrupo))
        .GroupBy(e => e.CodigoGrupo)
        .OrderBy(g => g.Key);

    if (!grupos.Any())
    {
        Console.WriteLine("⚠ No hay grupos asignados aún.");
    }
    else
    {
        foreach (var grupo in grupos)
        {
            Console.WriteLine($"\n📘 Grupo {grupo.Key}:");

            foreach (var estudiante in grupo.OrderBy(e => e.Apellido))
            {
                Console.WriteLine($"- {estudiante.Apellido}, {estudiante.Nombre} (DNI: {estudiante.DNI})");
            }
        }
    }

    Console.WriteLine("\nPresione una tecla para continuar...");
    Console.ReadKey();
}


        private static string LeerDato(string mensaje)
        {
            string dato;
            do
            {
                Console.Write(mensaje);
                dato = Console.ReadLine()?.Trim() ?? "";

                if (string.IsNullOrEmpty(dato))
                    Console.WriteLine("⚠ El campo no puede estar vacío.");

            } while (string.IsNullOrEmpty(dato));
            return dato;
        }
    }
}
