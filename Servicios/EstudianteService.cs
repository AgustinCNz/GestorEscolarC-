using Clase3Tp1.Modelos;
using Clase3Tp1.Servicios; // Para acceder a JsonService
using System;
using System.Collections.Generic;

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
            var estudiantes = JsonService.Cargar<Estudiante>(archivoEstudiantes); // CORRECTO: dentro del método

            while (true)
            {
                string dni = LeerDato("DNI: ");
                string apellido = LeerDato("Apellido: ");
                string nombre = LeerDato("Nombre: ");
                string correo = LeerDato("Correo Electrónico: ");
                //string codigoGrupo = LeerDato("Código de Grupo: ");

                var nuevoEstudiante = new Estudiante
                {
                    DNI = dni,
                    Apellido = apellido,
                    Nombre = nombre,
                    Correo = correo,
                  //  CodigoGrupo = codigoGrupo
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
                        Console.WriteLine("❌ Operación cancelada. Vuelva al menu principal.");
                      
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
        Console.WriteLine("3. Volver al menú anterior");
        Console.Write("Opción: ");
        string opcion = Console.ReadLine()?.Trim() ?? "";

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
        else if (opcion == "3")
        {
            Console.WriteLine("↩ Volviendo al menú...");
           
           // Console.ReadKey();
            return; // Salir del método y volver al menú anterior
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
          continue; // 🔁 vuelve al menú de búsqueda (no al principal)
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

    // Guardar el índice del estudiante en la lista
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
        Console.WriteLine("2. CAMBIAR  Apellido");
        Console.WriteLine("3. CAMBIAR Nombre");
        Console.WriteLine("4. CAMBIAR Correo");
        Console.WriteLine("5. CAMBIAR Código de Grupo");
        Console.WriteLine("");
        Console.WriteLine("6. Modificar todos");
        Console.WriteLine("7. Salir sin modificar");
        Console.Write("Opción: ");
        string opcion = Console.ReadLine()?.Trim() ?? "";

        switch (opcion)
        {
            case "1": estudiante.DNI = LeerDato("Nuevo DNI: "); break;
            case "2": estudiante.Apellido = LeerDato("Nuevo Apellido: "); break;
            case "3": estudiante.Nombre = LeerDato("Nuevo Nombre: "); break;
            case "4": estudiante.Correo = LeerDato("Nuevo Correo: "); break;
            case "5": estudiante.CodigoGrupo = LeerDato("Nuevo Código de Grupo: "); break;
            case "6":
                estudiante.DNI = LeerDato("Nuevo DNI: ");
                estudiante.Apellido = LeerDato("Nuevo Apellido: ");
                estudiante.Nombre = LeerDato("Nuevo Nombre: ");
                estudiante.Correo = LeerDato("Nuevo Correo: ");
                estudiante.CodigoGrupo = LeerDato("Nuevo Código de Grupo: ");
                break;
            case "7":
                Console.WriteLine("❌ Modificación cancelada.");
                return;
            default:
                Console.WriteLine("⚠ Opción inválida. Intente de nuevo.");
                continue;
        }

        Console.WriteLine("✅ Modificación realizada correctamente.");

        Console.WriteLine("\n¿Desea seguir modificando datos de este estudiante?");
        Console.WriteLine("1. Sí");
        Console.WriteLine("2. No, guardar cambios");
        Console.Write("Opción: ");
        string continuar = Console.ReadLine()?.Trim() ?? "";

        if (continuar != "1")
        {
            seguirModificando = false;
        }
    }

    // Actualizar en la lista original
    estudiantes[index] = estudiante;

    // Guardar la lista completa
    JsonService.Guardar(archivoEstudiantes, estudiantes);
    Console.WriteLine("✅ Cambios guardados correctamente.");
    Console.ReadKey();
}

    public static void EliminarEstudiante()
{
    Console.Clear();
    var estudiantes = JsonService.Cargar<Estudiante>(archivoEstudiantes);

    // Buscar estudiante
    Estudiante? estudiante = BuscarEstudiantePorDniOApellido("Eliminar estudiante por: ");

    if (estudiante == null)
    {
        Console.WriteLine("⚠ Estudiante no encontrado.");
        Console.ReadKey();
        return;
    }

    // Mostrar datos para confirmar
    MostrarDatosEstudiante(estudiante);
    Console.WriteLine("\n¿Está seguro que desea eliminar a este estudiante?");
    Console.WriteLine("1. Sí");
    Console.WriteLine("2. No");
    Console.Write("Opción: ");
    string opcion = Console.ReadLine()?.Trim() ?? "";

    if (opcion == "1")
    {
        // Eliminar y guardar
        estudiantes.RemoveAll(e => e.DNI == estudiante.DNI || e.Apellido == estudiante.Apellido);
        JsonService.Guardar(archivoEstudiantes, estudiantes);

        Console.WriteLine("✅ Estudiante eliminado correctamente.");
    }
    else
    {
        Console.WriteLine("❌ Operación cancelada.");
    }

    Console.ReadKey();
}


/*    public static void SorteoEstudiante()
{
    Console.Clear();
    var estudiantes = JsonService.Cargar<Estudiante>(archivoEstudiantes);

    // Filtrar estudiantes presentes que aún no han participado
    var estudiantesPresentes = estudiantes.FindAll(e => e.Presente && !e.Participado);

    if (estudiantesPresentes.Count == 0)
    {
        Console.WriteLine("Todos los estudiantes presentes ya participaron. Reiniciando participación...");

        foreach (var estudiante in estudiantes)
        {
            if (estudiante.Presente)
                estudiante.Participado = false;
        }

        // Volver a cargar la lista con los reiniciados
        estudiantesPresentes = estudiantes.FindAll(e => e.Presente);
    }

    if (estudiantesPresentes.Count == 0)
    {
        Console.WriteLine("❌ No hay estudiantes presentes para el sorteo.");
    }
    else
    {
        Random rand = new Random();
        Estudiante seleccionado = estudiantesPresentes[rand.Next(estudiantesPresentes.Count)];

        Console.WriteLine($"🎉 El estudiante seleccionado es: {seleccionado.Nombre} {seleccionado.Apellido}");
        Console.Write("¿Confirmar participación? (S/N): ");
        string respuesta = Console.ReadLine()?.Trim().ToUpper() ?? "N";

        if (respuesta == "S")
        {
            // Marcar como que ya participó
            var index = estudiantes.FindIndex(e => e.DNI == seleccionado.DNI);
            if (index != -1)
            {
                estudiantes[index].Participado = true;
                JsonService.Guardar(archivoEstudiantes, estudiantes);
                Console.WriteLine("✅ Participación registrada.");
            }
        }
        else
        {
            Console.WriteLine("ℹ El estudiante no participó. Seguirá en el próximo sorteo.");
        }
    }

    Console.WriteLine("\nPresione una tecla para continuar...");
    Console.ReadKey();
    }*/

    }
}
