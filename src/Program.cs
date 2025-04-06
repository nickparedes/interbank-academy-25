using System.Globalization;

namespace InterbankAcademy
{
    #region Clases y Propiedades
    // Definición de la clase Transaccion que representa una transacción con ID, Tipo y Monto.
    class Transaccion
    {
        public int Id { get; set; } // ID de la transacción
        public string Tipo { get; set; } = ""; // Tipo de transacción (Crédito o Débito)
        public decimal Monto { get; set; } // Monto de la transacción
    }
    #endregion

    #region Lógica del Programa
    static class Program
    {
        private const string SEPARATORTWOLINE = "\n====================================\n"; // Separador para mostrar en consola.

        static void Main()
        {
            // Bucle principal del menú que se repite hasta que el usuario decida salir.
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== Menú Principal ====\n");
                Console.WriteLine("1. Leer archivo CSV");
                Console.WriteLine("2. Salir\n");

                string option = ReadNumericOption("Seleccione una opción: ");

                switch (option)
                {
                    case "1":
                        ProcessCSV(); // Llama al método para procesar el archivo CSV
                        break;
                    case "2":
                        Console.WriteLine("¡Hasta luego!");
                        return; // Sale del programa
                    default:
                        Console.WriteLine("⚠️ Opción inválida.");
                        break;
                }

                Console.WriteLine("\nPresione una tecla para continuar...");
                Console.ReadKey();
            }
        }

        #region Procesamiento del CSV
        static void ProcessCSV()
        {
            // Solicita la ruta del archivo CSV al usuario
            Console.WriteLine("\nIngrese la ruta del archivo CSV (Enter para usar ruta por defecto '../data.csv'):");
            string? pathInput = Console.ReadLine();
            string filePath = string.IsNullOrWhiteSpace(pathInput) ? "../data.csv" : pathInput; // Ruta por defecto si no se ingresa nada.

            if (!File.Exists(filePath)) // Verifica si el archivo existe
            {
                Console.WriteLine($"⚠️ El archivo '{filePath}' no existe.");
                return;
            }

            var transactions = LoadTransactions(filePath); // Carga las transacciones desde el archivo

            if (transactions.Count == 0) // Verifica si no se cargaron transacciones
            {
                Console.WriteLine("⚠️ No se encontraron transacciones válidas.");
                return;
            }

            ShowReportMenu(transactions); // Muestra el menú de reportes
        }
        #endregion

        #region Carga de Transacciones
        static List<Transaccion> LoadTransactions(string filePath)
        {
            var list = new List<Transaccion>();
            var lines = File.ReadAllLines(filePath).Skip(1); // Omite la primera línea (cabecera del CSV)
            foreach (var line in lines)
            {
                var parts = line.Split(',');

                // Verifica si la línea tiene el formato correcto
                if (parts.Length == 3 &&
                    int.TryParse(parts[0], out int id) &&
                    decimal.TryParse(parts[2], NumberStyles.Any, CultureInfo.InvariantCulture, out decimal amount))
                {
                    // Agrega la transacción válida a la lista
                    list.Add(new Transaccion
                    {
                        Id = id,
                        Tipo = parts[1].Trim(),
                        Monto = amount
                    });
                }
            }

            return list;
        }
        #endregion

        #region Menú de Reportes
        static void ShowReportMenu(List<Transaccion> transactions)
        {
            // Menú para mostrar diferentes reportes
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== Opciones de Reporte ====\n");
                Console.WriteLine("1. Ver Balance Final");
                Console.WriteLine("2. Ver Transacción de Mayor Monto");
                Console.WriteLine("3. Ver Conteo de Transacciones");
                Console.WriteLine("4. Ver Reporte Completo");
                Console.WriteLine("5. Volver al Menú Principal\n");

                string option = ReadNumericOption("Seleccione una opción: ");

                switch (option)
                {
                    case "1":
                        Console.WriteLine(SEPARATORTWOLINE);
                        Console.WriteLine("Balance Final\n");
                        ShowBalance(transactions); // Muestra el balance final
                        Console.WriteLine(SEPARATORTWOLINE);
                        break;
                    case "2":
                        Console.WriteLine(SEPARATORTWOLINE);
                        Console.WriteLine("Transacción de Mayor Monto\n");
                        ShowHighestTransaction(transactions); // Muestra la transacción de mayor monto
                        Console.WriteLine(SEPARATORTWOLINE);
                        break;
                    case "3":
                        Console.WriteLine(SEPARATORTWOLINE);
                        Console.WriteLine("Conteo de Transacciones\n");
                        ShowTransactionCounts(transactions); // Muestra el conteo de transacciones
                        Console.WriteLine(SEPARATORTWOLINE);
                        break;
                    case "4":
                        Console.WriteLine(SEPARATORTWOLINE);
                        Console.WriteLine("Reporte del Balance Final\n");
                        ShowFullReport(transactions); // Muestra el reporte completo
                        Console.WriteLine(SEPARATORTWOLINE);
                        break;
                    case "5":
                        return; // Vuelve al menú principal
                    default:
                        Console.WriteLine(SEPARATORTWOLINE);
                        Console.WriteLine("⚠️ Opción inválida.");
                        break;
                }

                Console.WriteLine("\nPresione una tecla para continuar...");
                Console.ReadKey();
            }
        }
        #endregion

        #region Mostrar Reportes
        static void ShowBalance(List<Transaccion> list)
        {
            // Calcula el balance final considerando los créditos y débitos
            decimal credit = list.Where(t => t.Tipo.Equals("Crédito", StringComparison.OrdinalIgnoreCase)).Sum(t => t.Monto);
            decimal debit = list.Where(t => t.Tipo.Equals("Débito", StringComparison.OrdinalIgnoreCase)).Sum(t => t.Monto);
            Console.WriteLine($"Balance Final: {credit - debit}");
        }

        static void ShowHighestTransaction(List<Transaccion> list)
        {
            // Muestra la transacción con el mayor monto
            var max = list.OrderByDescending(t => t.Monto).First();
            Console.WriteLine($"Transacción de Mayor Monto: ID {max.Id} - {max.Monto}");
        }

        static void ShowTransactionCounts(List<Transaccion> list)
        {
            // Muestra la cantidad de transacciones de tipo Crédito y Débito
            int credit = list.Count(t => t.Tipo.Equals("Crédito", StringComparison.OrdinalIgnoreCase));
            int debit = list.Count(t => t.Tipo.Equals("Débito", StringComparison.OrdinalIgnoreCase));
            Console.WriteLine($"Conteo de Transacciones: Crédito: {credit} Débito: {debit}");
        }

        static void ShowFullReport(List<Transaccion> list)
        {
            // Muestra el reporte completo: balance, transacción más grande, y conteo de transacciones
            ShowBalance(list);
            ShowHighestTransaction(list);
            ShowTransactionCounts(list);
        }
        #endregion

        #region Funciones de Entrada
        static string ReadNumericOption(string message)
        {
            // Lee una opción numérica de la consola
            Console.Write(message);
            string input = "";
            char lastKeyChar = '\0';

            while (true)
            {
                var key = Console.ReadKey(true);

                if (char.IsDigit(key.KeyChar) && key.KeyChar != lastKeyChar)
                {
                    input = key.KeyChar.ToString();
                    Console.Write(key.KeyChar);
                    return input;
                }
                else if (key.Key == ConsoleKey.Backspace && input.Length > 0)
                {
                    input = input[..^1];
                    Console.Write("\b \b");
                }
                else
                {
                    Console.Beep();
                }
            }
        }
        #endregion
    }
    #endregion
}
