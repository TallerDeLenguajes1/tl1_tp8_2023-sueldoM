using System;
using System.Collections.Generic;
using System.IO;

struct Tarea
{
    public int TareaID;
    public string Descripcion;
    public int Duracion;
}

class Program
{
    static List<Tarea> tareasPendientes = new List<Tarea>();
    static List<Tarea> tareasRealizadas = new List<Tarea>();

    static void Main(string[] args)
    {
        Console.WriteLine("Bienvenido al módulo ToDo");

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Menú:");
            Console.WriteLine("1. Crear nuevas tareas");
            Console.WriteLine("2. Mover tarea pendiente a realizada");
            Console.WriteLine("3. Buscar tareas pendientes por descripción");
            Console.WriteLine("4. Guardar sumario de horas trabajadas");
            Console.WriteLine("5. Salir");
            Console.WriteLine();

            Console.Write("Ingrese el número de opción: ");
            string ? opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Console.Write("Ingrese la cantidad de tareas pendientes a crear: ");
                    if (int.TryParse(Console.ReadLine(), out int cantidad))
                    {
                        CrearTareasPendientes(cantidad);
                        Console.WriteLine("Tareas pendientes creadas exitosamente.");
                    }
                    else
                    {
                        Console.WriteLine("Error: La cantidad ingresada no es válida.");
                    }
                    break;

                case "2":
                    if (tareasPendientes.Count > 0)
                    {
                        MostrarTareasPendientes();
                        Console.Write("Ingrese el ID de la tarea que desea mover a realizada: ");
                        if (int.TryParse(Console.ReadLine(), out int tareaID))
                        {
                            MoverTareaPendienteARealizada(tareaID);
                        }
                        else
                        {
                            Console.WriteLine("Error: El ID ingresado no es válido.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No hay tareas pendientes.");
                    }
                    break;

                case "3":
                    Console.Write("Ingrese la descripción a buscar: ");
                    string ? descripcion = Console.ReadLine()!;
                    BuscarTareasPendientesPorDescripcion(descripcion);
                    break;

                case "4":
                    GuardarSumarioHorasTrabajadas();
                    Console.WriteLine("Sumario guardado exitosamente.");
                    break;

                case "5":
                    Console.WriteLine("¡Hasta luego!");
                    return;

                default:
                    Console.WriteLine("Opción inválida. Por favor, ingrese una opción válida.");
                    break;
            }
        }
    }

    static void CrearTareasPendientes(int cantidad)
    {
        Random random = new Random();

        for (int i = 0; i < cantidad; i++)
        {
            Console.WriteLine($"Escriba tarea {i+1}:");
            Tarea tarea = new Tarea
            {
                TareaID = i + 1,
                Descripcion = Console.ReadLine()!,
                Duracion = random.Next(10, 101)
            };

            tareasPendientes.Add(tarea);
        }
    }

    static void MostrarTareasPendientes()
    {
        Console.WriteLine("Tareas pendientes:");
        foreach (var tarea in tareasPendientes)
        {
            Console.WriteLine($"ID: {tarea.TareaID}, Descripción: {tarea.Descripcion}, Duración: {tarea.Duracion}min");
        }
    }

    static void MoverTareaPendienteARealizada(int tareaID)
    {
        Tarea tarea = tareasPendientes.Find(t => t.TareaID == tareaID);

        if (tarea.TareaID != 0)
        {
            tareasPendientes.Remove(tarea);
            tareasRealizadas.Add(tarea);
            Console.WriteLine("Tarea movida a realizada correctamente.");
        }
        else
        {
            Console.WriteLine("No se encontró una tarea con el ID especificado.");
        }
    }

    static void BuscarTareasPendientesPorDescripcion(string descripcion)
    {
        List<Tarea> tareasEncontradas = tareasPendientes.FindAll(t => t.Descripcion.Contains(descripcion));

        if (tareasEncontradas.Count > 0)
        {
            Console.WriteLine("Tareas pendientes encontradas:");
            foreach (var tarea in tareasEncontradas)
            {
                Console.WriteLine($"ID: {tarea.TareaID}, Descripción: {tarea.Descripcion}, Duración: {tarea.Duracion}");
            }
        }
        else
        {
            Console.WriteLine("No se encontraron tareas pendientes con la descripción especificada.");
        }
    }

        static void GuardarSumarioHorasTrabajadas()
    {
        int sumatoriaDuracion = 0;

        foreach (var tarea in tareasRealizadas)
        {
            sumatoriaDuracion += tarea.Duracion;
        }

        string sumario = $"Horas trabajadas: {sumatoriaDuracion}";

        using (StreamWriter sw = new StreamWriter("sumario.txt"))
        {
            sw.WriteLine(sumario);
            sw.Close();
        }
    }

}
