using System;
using Clase3Tp1.Servicios;
using Clase3Tp1.Modelos;
using System.Security.Cryptography;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("[MENU PRINCIPAL]");
            Console.WriteLine("1) ESTUDIANTES");
            Console.WriteLine("2) GRUPOS");
            Console.WriteLine("3) ASISTENCIAS");
            Console.WriteLine("4) SALIR");
            Console.Write("Selecciona una opción: ");
            string? opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    MenuEstudiantes();
                    break;
                case "2":
                    MenuGrupos();
                    break;
                case "3":
                    MenuAsistencias();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Opción inválida.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void MenuEstudiantes()
    {
        Console.Clear();
        Console.WriteLine("1) Alta de Estudiante");
        Console.WriteLine("2) Buscar estudiante");
        Console.WriteLine("3) Mostrar datos del estudiante");
        Console.WriteLine("4) Modificar datos del estudiante");
        Console.WriteLine("5) Eliminar estudiante");
        Console.WriteLine("6) Sorteo");
        Console.WriteLine("7) Volver");

        string? opcion = Console.ReadLine();
        if (opcion == "7") return;
        switch (opcion)
            {
                case "1":
                     EstudianteService.AltaEstudiante();
                    break;
                case "2":
                    EstudianteService.BuscarEstudiantePorDniOApellido();
                    break;
                case "3":
                    EstudianteService.BuscarYMostrarEstudiante();
                    break;
                case "4":
                    EstudianteService.ModificarEstudiante();
                    break;
                case "5":
                    EstudianteService.EliminarEstudiante();
                    break;
                case "6":
                    EstudianteService.SorteoEstudiante();
                    break;
                default:
                    Console.WriteLine("Opción inválida.");
                    Console.ReadKey();
                    break;
            }

    }







    static void MenuGrupos()
    {
        Console.Clear();
        Console.WriteLine("1) Crear grupo");
        Console.WriteLine("2) Modificar grupo");
        Console.WriteLine("3) Eliminar grupo");
        Console.WriteLine("4) Ver grupos");
        Console.WriteLine("5) Sorteo por grupo");
        Console.WriteLine("6) Volver");

        string? opcion = Console.ReadLine();
        if (opcion == "6") return;
    }

    static void MenuAsistencias()
    {
        Console.Clear();
        Console.WriteLine("1) Registrar asistencia");
        Console.WriteLine("2) Ver asistencia por alumno");
        Console.WriteLine("3) Ver asistencia general");
        Console.WriteLine("4) Volver");

        string? opcion = Console.ReadLine();
        if (opcion == "4") return;
    }
}