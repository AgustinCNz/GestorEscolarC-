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

    string dni;
    while (true)
    {
        Console.WriteLine("0) Volver al menú anterior");
        dni = LeerDato("DNI (8 números): ");
        if (dni == "0") return;

        if (!dni.All(char.IsDigit))
        {
            Console.WriteLine("⚠ El DNI solo debe contener números.");
            continue;
        }
        if (dni.Length != 8)
        {
            Console.WriteLine("⚠ El DNI debe tener exactamente 8 dígitos.");
            continue;
        }
        if (estudiantes.Any(e => e.DNI == dni))
        {
            Console.WriteLine("⚠ Ya existe un estudiante con ese DNI.");
            continue;
        }
        break;
    }

    string apellido;
    while (true)
    {
        Console.WriteLine("0) Volver al menú anterior");
        apellido = LeerDato("Apellido: ");
        if (apellido == "0") return;

        if (!apellido.All(char.IsLetter))
        {
            Console.WriteLine("⚠ El apellido solo debe contener letras.");
            continue;
        }
        break;
    }

    string nombre;
    while (true)
    {
        Console.WriteLine("0) Volver al menú anterior");
        nombre = LeerDato("Nombre: ");
        if (nombre == "0") return;

        if (!nombre.All(char.IsLetter))
        {
            Console.WriteLine("⚠ El nombre solo debe contener letras.");
            continue;
        }
        break;
    }

    string correo;
    while (true)
    {
        Console.WriteLine("0) Volver al menú anterior");
        correo = LeerDato("Correo Electrónico: ");
        if (correo == "0") return;

        if (!correo.EndsWith("@gmail.com") && !correo.EndsWith("@hotmail.com"))
        {
            Console.WriteLine("⚠ Solo se aceptan correos @gmail.com o @hotmail.com.");
            continue;
        }
        break;
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

    if (opcion == "1")
    {
        estudiantes.Add(nuevoEstudiante);
        JsonService.Guardar(archivoEstudiantes, estudiantes);
        Console.WriteLine("✅ Estudiante agregado correctamente.");
    }
    else
    {
        Console.WriteLine("❌ Operación cancelada.");
    }

    Console.WriteLine("\nPresione una tecla para continuar...");
    Console.ReadKey();
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
            Console.Write("Ingrese el DNI (o 0 para volver): ");
            string dni = Console.ReadLine()?.Trim() ?? "";
            if (dni == "0")
            {
                Console.WriteLine("↩ Volviendo al menú de búsqueda...");
                continue;
            }
            encontrado = estudiantes.Find(e => e.DNI == dni);
        }
        else if (opcion == "2")
        {
            Console.Write("Ingrese el Apellido (o 0 para volver): ");
            string apellido = Console.ReadLine()?.Trim() ?? "";
            if (apellido == "0")
            {
                Console.WriteLine("↩ Volviendo al menú de búsqueda...");
                continue;
            }
            encontrado = estudiantes.Find(e => e.Apellido?.Equals(apellido, StringComparison.OrdinalIgnoreCase) ?? false);
        }
        else
        {
            Console.WriteLine("⚠ Opción inválida.");
            Console.WriteLine("↩ Volviendo al menú...");
        Console.WriteLine("Presione una tecla para continuar...");
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
            Console.Write("Ingrese el DNI (o 0 para volver): ");
            string dni = Console.ReadLine()?.Trim() ?? "";
            if (dni == "0")
            {
                Console.WriteLine("↩ Cancelando búsqueda...");
                return null;
            }
            return estudiantes.Find(e => e.DNI == dni);
        }
        else if (opcion == "2")
        {
            Console.Write("Ingrese el Apellido (o 0 para volver): ");
            string apellido = Console.ReadLine()?.Trim() ?? "";
            if (apellido == "0")
            {
                Console.WriteLine("↩ Cancelando búsqueda...");
                return null;
            }
            return estudiantes.Find(e => e.Apellido?.Equals(apellido, StringComparison.OrdinalIgnoreCase) ?? false);
        }
        else if (opcion == "3")
        {
            return null;
        }
        else
        {
            Console.WriteLine("⚠ Opción inválida.");
            Console.WriteLine("↩ Volviendo al menú...");
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }
    }
}



 public static void ModificarEstudiante()
{
    Console.Clear();
    var estudiantes = JsonService.Cargar<Estudiante>(archivoEstudiantes);

    Estudiante? estudianteOriginal = BuscarEstudiantePorDniOApellido("Modificar estudiante por:");
    if (estudianteOriginal == null)
    {
        Console.WriteLine("⚠ Estudiante no encontrado.");
        Console.WriteLine("↩ Volviendo al menú...");
        Console.WriteLine("Presione una tecla para continuar...");
        Console.ReadKey();
        return;
    }

    int index = estudiantes.FindIndex(e => e.DNI == estudianteOriginal.DNI);
    if (index == -1)
    {
        Console.WriteLine("⚠ No se pudo encontrar al estudiante en la lista original.");
        Console.WriteLine("↩ Volviendo al menú...");
        Console.WriteLine("Presione una tecla para continuar...");
        Console.ReadKey();
        return;
    }

    // Copia temporal del estudiante
    Estudiante copiaEstudiante = new Estudiante
    {
        DNI = estudianteOriginal.DNI,
        Apellido = estudianteOriginal.Apellido,
        Nombre = estudianteOriginal.Nombre,
        Correo = estudianteOriginal.Correo,
        CodigoGrupo = estudianteOriginal.CodigoGrupo,
        Participado = estudianteOriginal.Participado,
        Presente = estudianteOriginal.Presente
    };

    MostrarDatosEstudiante(estudianteOriginal);

    bool seguirModificando = true;
    while (seguirModificando)
    {
        Console.WriteLine("\nSeleccione el dato a modificar:");
        Console.WriteLine("1. CAMBIAR DNI");
        Console.WriteLine("2. CAMBIAR Apellido");
        Console.WriteLine("3. CAMBIAR Nombre");
        Console.WriteLine("4. CAMBIAR Correo");
        Console.WriteLine("5. CAMBIAR Código de Grupo");
        Console.WriteLine("6. Salir sin guardar");
        Console.Write("Opción: ");
        string opcion = Console.ReadLine()?.Trim() ?? "";

        switch (opcion)
        {
            case "1":
                while (true)
                {
                    Console.WriteLine("0) Volver");
                    string nuevoDni = LeerDato("Nuevo DNI (8 dígitos): ");
                    if (nuevoDni == "0") break;

                    if (!nuevoDni.All(char.IsDigit) || nuevoDni.Length != 8)
                    {
                        Console.WriteLine("⚠ El DNI debe tener exactamente 8 números.");
                        continue;
                    }
                    if (estudiantes.Any(e => e.DNI == nuevoDni && e != estudianteOriginal))
                    {
                        Console.WriteLine("⚠ Ya existe otro estudiante con ese DNI.");
                        continue;
                    }
                    copiaEstudiante.DNI = nuevoDni;
                    break;
                }
                break;

            case "2":
                while (true)
                {
                    Console.WriteLine("0) Volver");
                    string nuevoApellido = LeerDato("Nuevo Apellido: ");
                    if (nuevoApellido == "0") break;

                    if (!nuevoApellido.All(char.IsLetter))
                    {
                        Console.WriteLine("⚠ El apellido solo debe contener letras.");
                        continue;
                    }
                    copiaEstudiante.Apellido = nuevoApellido;
                    break;
                }
                break;

            case "3":
                while (true)
                {
                    Console.WriteLine("0) Volver");
                    string nuevoNombre = LeerDato("Nuevo Nombre: ");
                    if (nuevoNombre == "0") break;

                    if (!nuevoNombre.All(char.IsLetter))
                    {
                        Console.WriteLine("⚠ El nombre solo debe contener letras.");
                        continue;
                    }
                    copiaEstudiante.Nombre = nuevoNombre;
                    break;
                }
                break;

            case "4":
                while (true)
                {
                    Console.WriteLine("0) Volver");
                    string nuevoCorreo = LeerDato("Nuevo Correo Electrónico: ");
                    if (nuevoCorreo == "0") break;

                    if (!nuevoCorreo.EndsWith("@gmail.com") && !nuevoCorreo.EndsWith("@hotmail.com"))
                    {
                        Console.WriteLine("⚠ Solo se aceptan correos @gmail.com o @hotmail.com.");
                        continue;
                    }
                    copiaEstudiante.Correo = nuevoCorreo;
                    break;
                }
                break;

            case "5":
                while (true)
                {
                    Console.WriteLine("0) Volver");
                    string nuevoGrupo = LeerDato("Nuevo Código de Grupo (solo una letra A-Z): ").ToUpper();
                    if (nuevoGrupo == "0") break;

                    if (nuevoGrupo.Length == 1 && char.IsLetter(nuevoGrupo[0]))
                    {
                        copiaEstudiante.CodigoGrupo = nuevoGrupo;
                        break;
                    }
                    Console.WriteLine("⚠ El código de grupo debe ser una sola letra (A-Z).");
                }
                break;

            case "6":
                Console.WriteLine("❌ Modificación cancelada.");
                return;

            default:
                Console.WriteLine("⚠ Opción inválida.");
                continue;
        }

        Console.WriteLine("✅ Modificación temporal realizada.");
        Console.WriteLine("\n¿Desea seguir modificando datos?");
        Console.WriteLine("1. Sí");
        Console.WriteLine("2. No, guardar cambios");
        Console.WriteLine("3. Cancelar y descartar todos los cambios");
        string continuar = Console.ReadLine()?.Trim() ?? "";

        if (continuar == "3")
        {
            Console.WriteLine("❌ Modificaciones descartadas.");
            return;
        }

        if (continuar != "1")
        {
            seguirModificando = false;
        }
    }

    estudiantes[index] = copiaEstudiante;
    JsonService.Guardar(archivoEstudiantes, estudiantes);
    Console.WriteLine("✅ Cambios guardados correctamente.");
    Console.WriteLine("Presione una tecla para continuar...");
    Console.ReadKey();
}




   public static void EliminarEstudiante()
{
    Console.Clear();
    var estudiantes = JsonService.Cargar<Estudiante>(archivoEstudiantes);

    if (estudiantes.Count == 0)
    {
        Console.WriteLine("⚠ No hay estudiantes registrados.");
        Console.WriteLine("↩ Volviendo al menú...");
        Console.WriteLine("Presione una tecla para continuar...");
        Console.ReadKey();
        return;
    }

    Console.WriteLine("📋 Lista de estudiantes:");
    for (int i = 0; i < estudiantes.Count; i++)
    {
        var e = estudiantes[i];
        Console.WriteLine($"[{i + 1}] {e.Apellido}, {e.Nombre} (DNI: {e.DNI})");
    }

    Console.Write("\nIngrese los números de los estudiantes a eliminar (separados por coma) o 0 para volver: ");
    string entrada = Console.ReadLine()?.Trim() ?? "";

    if (entrada == "0")
    {
        Console.WriteLine("↩ Operación cancelada. Volviendo al menú...");
        Console.WriteLine("Presione una tecla para continuar...");
        Console.ReadKey();
        return;
    }

    var indices = entrada.Split(',')
                         .Select(n => int.TryParse(n.Trim(), out int i) ? i - 1 : -1)
                         .Where(i => i >= 0 && i < estudiantes.Count)
                         .Distinct()
                         .ToList();

    if (indices.Count == 0)
    {
        Console.WriteLine("❌ No se seleccionó ningún estudiante válido.");
        Console.WriteLine("↩ Volviendo al menú...");
        Console.WriteLine("Presione una tecla para continuar...");
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
    while (true)
    {
        Console.Clear();
        Console.WriteLine("=== Sorteo de Participación ===\n");
        Console.WriteLine("1. Realizar sorteo del día");
        Console.WriteLine("2. Ver estudiantes que ya participaron este mes");
        Console.WriteLine("3. Ver estudiantes que aún no participaron este mes");
        Console.WriteLine("4. Volver");
        Console.Write("Opción: ");
        string opcion = Console.ReadLine()?.Trim() ?? "";

        var estudiantes = JsonService.Cargar<Estudiante>("Datos/alumnos.json");
        var asistencias = JsonService.Cargar<Asistencia>("Datos/asistencias.json");

        var hoy = DateTime.Today;
        var mesActual = hoy.Month;
        var anioActual = hoy.Year;

        // Estudiantes presentes al menos una vez en el mes
        var dnisPresentesEsteMes = asistencias
            .Where(a => a.Fecha.Month == mesActual && a.Fecha.Year == anioActual && a.EstaPresente)
            .Select(a => a.DNI)
            .Distinct()
            .ToList();

        var estudiantesPresentesEsteMes = estudiantes
            .Where(e => dnisPresentesEsteMes.Contains(e.DNI))
            .ToList();

        var yaParticiparon = estudiantesPresentesEsteMes.Where(e => e.Participado).ToList();
        var noParticiparon = estudiantesPresentesEsteMes.Where(e => !e.Participado).ToList();

        switch (opcion)
        {
            case "1":
                // Solo se consideran los presentes HOY
                string fechaHoy = hoy.ToString("yyyy-MM-dd");

                var presentesHoy = asistencias
                    .Where(a => a.Fecha.ToString("yyyy-MM-dd") == fechaHoy && a.EstaPresente)
                    .Select(a => a.DNI)
                    .ToList();

                var candidatos = estudiantes.Where(e => presentesHoy.Contains(e.DNI)).ToList();

                if (candidatos.Count == 0)
                {
                    Console.WriteLine("⚠ No hay estudiantes presentes hoy para realizar el sorteo.");
                    Console.WriteLine("↩ Volviendo al menú...");
                    Console.WriteLine("Presione una tecla para continuar...");
                    Console.ReadKey();
                    continue;
                }

                if (candidatos.All(e => e.Participado))
                {
                    Console.WriteLine("🔁 Todos los estudiantes presentes ya participaron este mes. Reiniciando participación...");
                    foreach (var e in candidatos)
                        e.Participado = false;

                    JsonService.Guardar("Datos/alumnos.json", estudiantes);
                    noParticiparon = candidatos; // se reinicia, todos pueden participar de nuevo
                }

                var disponibles = candidatos.Where(e => !e.Participado).ToList();
                var random = new Random();
                var seleccionado = disponibles[random.Next(disponibles.Count)];

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
                break;

            case "2":
                Console.Clear();
                Console.WriteLine("📋 Estudiantes que ya participaron este mes:\n");
                foreach (var est in yaParticiparon.OrderBy(e => e.Apellido))
                {
                    Console.WriteLine($"- {est.Apellido}, {est.Nombre} (DNI: {est.DNI})");
                }
                Console.WriteLine("\nPresione una tecla para volver...");
                Console.ReadKey();
                break;

            case "3":
                Console.Clear();
                Console.WriteLine("📋 Estudiantes que aún no participaron este mes:\n");
                foreach (var est in noParticiparon.OrderBy(e => e.Apellido))
                {
                    Console.WriteLine($"- {est.Apellido}, {est.Nombre} (DNI: {est.DNI})");
                }
                Console.WriteLine("\nPresione una tecla para volver...");
                Console.ReadKey();
                break;

            case "4":
                return;

            default:
                Console.WriteLine("⚠ Opción inválida.");
                Console.WriteLine("↩ Volviendo al menú...");
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
                break;
        }
    }
}

    }
}
