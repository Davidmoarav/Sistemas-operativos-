public class Proceso
{
    public string Nombre { get; set; }
    public int TiempoInicio { get; set; }
    public int Duracion { get; set; }
    public int Tamanio { get; set; }

    public Proceso(string nombre, int tiempoInicio, int duracion, int tamanio)
    {
        Nombre = nombre;
        TiempoInicio = tiempoInicio;
        Duracion = duracion;
        Tamanio = tamanio;
    }
}

public class Memoria
{
    private int[] espaciosLibres;
    private int[] procesos;

    public Memoria(int tamanio)
    {
        espaciosLibres = new int[tamanio];
        procesos = new int[tamanio];
    }

    public void AsignarProceso(Proceso proceso)
    {
        int espacio = EncontrarEspacioLibre(proceso.Tamanio);
        if (espacio != -1)
        {
            for (int i = 0; i < proceso.Tamanio; i++)
            {
                espaciosLibres[espacio + i] = -1;
                procesos[espacio + i] = proceso.Nombre;
            }
        }
    }

    public int EncontrarEspacioLibre(int tamanio)
    {
        for (int i = 0; i < espaciosLibres.Length; i++)
        {
            if (espaciosLibres[i] >= 0 && espaciosLibres[i] >= tamanio)
            {
                return i;
            }
        }
        return -1;
    }

    public void CompactarMemoria()
    {
        int[] espaciosLibresTemp = new int[espaciosLibres.Length];
        int i = 0;
        for (int j = 0; j < espaciosLibres.Length; j++)
        {
            if (espaciosLibres[j] == 0)
            {
                espaciosLibresTemp[i] = j;
                i++;
            }
        }
        espaciosLibres = espaciosLibresTemp;

        for (int j = 0; j < espaciosLibres.Length; j++)
        {
            if (espaciosLibres[j] != -1)
            {
                for (int k = j + 1; k < espaciosLibres.Length; k++)
                {
                    if (espaciosLibres[k] == 0)
                    {
                        espaciosLibres[j] += espaciosLibres[k];
                        espaciosLibres[k] = -1;
                        break;
                    }
                }
            }
        }
    }
}

public class MainClass
{
    public static void Main(string[] args)
    {
        int tamanioMemoria = 1000;
        Memoria memoria = new Memoria(tamanioMemoria);

        // Leer archivo de entrada
        string archivoEntrada = "entrada.txt";
        StreamReader reader = new StreamReader(archivoEntrada);
        string linea;
        while ((linea = reader.ReadLine()) != null)
        {
            string[] datos = linea.Split(",");
            Proceso proceso = new Proceso(datos[0], int.Parse(datos[1]), int.Parse(datos[2]), int.Parse(datos[3]));
            memoria.AsignarProceso(proceso);
        }
        reader.Close();

        // Imprimir estado de la memoria
        Console.WriteLine("Memoria:");
        for (int i = 0; i < memoria.espaciosLibres.Length; i++)
        {
            Console.Write(memoria.espaciosLibres[i]);
        }
        Console.WriteLine();

        // Compactar memoria
        memoria.CompactarMemoria();

        // Imprimir estado de la memoria compactada
        Console.WriteLine("Memoria compactada:");
        for (int i = 0; i < memoria.espaciosLibres.Length; i++)
        {
            Console.Write(memoria.espaciosLibres[i]);
        }
    }
}
