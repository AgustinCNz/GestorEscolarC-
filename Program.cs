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
                    //MenuAsistencias();
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
    while (true)
    {
        Console.Clear();
        Console.WriteLine("1) Alta de Estudiante");
        Console.WriteLine("2) Buscar estudiante");
        Console.WriteLine("3) Modificar datos del estudiante");
        Console.WriteLine("4) Eliminar estudiante");
        Console.WriteLine("5) Sorteo");
        Console.WriteLine("6) Volver");

        string? opcion = Console.ReadLine();

        if (opcion == "7") break; // ← ahora rompe el bucle y vuelve al menú principal

        switch (opcion)
        {
            case "1":
                EstudianteService.AltaEstudiante();
                break;
            case "2":
                EstudianteService.BuscarYMostrarEstudiantePorDniOApellido();
                break;
            case "3":
               EstudianteService.ModificarEstudiante();
                break;
            case "4":
                 EstudianteService.EliminarEstudiante();
                break;
            case "5":
                
                break;
            case "6":
                return; // Volver al menú principal
            default:
                Console.WriteLine("Opción inválida.");
                Console.ReadKey();
                break;
        }
    }
}







    static void MenuGrupos()
    {
        while (true){
            Console.Clear();
            Console.WriteLine("[MENU GRUPOS]");
            Console.WriteLine("1) Crear grupo");
            Console.WriteLine("2) Modificar grupo");
            Console.WriteLine("3) Mover estudiante entre grupos");
            Console.WriteLine("4) Eliminar grupo");
            Console.WriteLine("5) ver grupos");
            Console.WriteLine("6) Estudiantes sin grupos");
            Console.WriteLine("7) Grupos con menos de 6 estudiantes");
            Console.WriteLine("8) Sorteo por grupo");
            Console.WriteLine("9) Volver al menu principal");

        string? opcion = Console.ReadLine();
        if (opcion == "9") return;

        switch (opcion)
        {
            case "1":
                GrupoService.CrearGrupo();
                break;
            case "2":
                GrupoService.ModificarGrupo();
                break;
            case "3":
              GrupoService.MoverEstudianteDeGrupo();
                break;
            case "4":
                 GrupoService.EliminarGrupo();
                break;
            case "5":
            GrupoService.MostrarGruposConEstudiantes();
                break;
            case "6":
                GrupoService.MostrarEstudiantesSinGrupo();
              break; // Volver al menú principal
            default:
                Console.WriteLine("Opción inválida.");
                Console.ReadKey();
                break;
    }
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
}}