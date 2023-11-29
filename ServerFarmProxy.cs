using System;
using System.Collections.Generic;

public class ServerFarmProxy
{
    private static ServerFarmProxy instance;
    private List<string> serverAddresses;

    private ServerFarmProxy()
    {
        // Inizializzazione degli indirizzi IP dei server (costruiti casualmente per l'esempio)
        serverAddresses = new List<string>
        {
            GenerateRandomIPAddress(),
            GenerateRandomIPAddress(),
            GenerateRandomIPAddress(),
            GenerateRandomIPAddress()
        };
    }

    public static ServerFarmProxy Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ServerFarmProxy();
            }
            return instance;
        }
    }

    public string ServerRequest()
    {
        // Simula una chiamata al server restituendo un indirizzo IP casuale dalla lista
        Random random = new Random();
        return serverAddresses[random.Next(serverAddresses.Count)];
    }

    private string GenerateRandomIPAddress()
    {
        // Simula la generazione di un indirizzo IP casuale (esempio semplificato)
        Random random = new Random();
        return $"{random.Next(256)}.{random.Next(256)}.{random.Next(256)}.{random.Next(256)}";
    }
}

class Program
{
    static void Main()
    {
        int NInstance = 3; // Numero di istanze del proxy
        int NCall = 4;     // Numero di chiamate per istanza

        for (int i = 0; i < NInstance; i++)
        {
            ServerFarmProxy proxy = ServerFarmProxy.Instance;

            for (int j = 0; j < NCall; j++)
            {
                string ipAddress = proxy.ServerRequest();
                Console.WriteLine($"Proxy {i + 1}, Chiamata {j + 1}: {ipAddress}");
                if (i > 0 && ipAddress == ServerFarmProxy.Instance.ServerRequest())
                {
                    Console.WriteLine("Il proxy è OK");
                }
            }
        }
    }
}
