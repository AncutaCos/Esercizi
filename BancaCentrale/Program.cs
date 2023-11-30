using System;

// Interfaccia per il controllo dei diritti umani
interface IHumanRightsChecker
{
    string CheckHumanRights(string country);
}

// Classe astratta per la banca centrale
abstract class CentralBank
{
    public abstract double CalculateSpread(string country);
}

// Classe EuroCentralBank che eredita da CentralBank e implementa IHumanRightsChecker
class EuroCentralBank : CentralBank, IHumanRightsChecker
{
    public override double CalculateSpread(string country)
    {
        double baseSpread = 1.5;

        if (IsEuroZoneCountry(country))
        {
            return baseSpread;
        }
        else
        {
            // Aggiungi logica personalizzata qui per calcolare lo spread per paesi non nell'Eurozona
            double nonEuroZoneSpread = baseSpread + 0.5; // Esempio di calcolo personalizzato
            return nonEuroZoneSpread;
        }
    }

    public string CheckHumanRights(string country)
    {
        if (IsEuroZoneCountry(country))
        {
            return "Il paese dell'Eurozona rispetta i diritti umani.";
        }
        else if (IsEUCountry(country))
        {
            return "Il paese EU rispetta i diritti umani.";
        }
        else if (IsONUCountry(country))
        {
            return "Il paese ONU rispetta i diritti umani.";
        }
        else
        {
            return "Il paese non appartiene né all'Eurozona, né all'EU, né all'ONU.";
        }
    }

    private bool IsEuroZoneCountry(string country)
    {
        // Implementazione fittizia per determinare se il paese fa parte dell'Eurozona
        // In questo esempio, consideriamo solo due paesi a titolo illustrativo
        return country.Equals("Francia") || country.Equals("Spagna");
    }

    private bool IsEUCountry(string country)
    {
        // Implementazione fittizia per determinare se il paese fa parte dell'EU
        // In questo esempio, consideriamo solo due paesi a titolo illustrativo
        return country.Equals("Italia") || country.Equals("Germania");
    }

    private bool IsONUCountry(string country)
    {
        // Implementazione fittizia per determinare se il paese fa parte dell'ONU
        // In questo esempio, consideriamo solo due paesi a titolo illustrativo
        return country.Equals("Stati Uniti") || country.Equals("Cina");
    }
}

class Program
{
    static void Main()
    {
        EuroCentralBank euroBank = new EuroCentralBank();

        // Esempio di calcolo dello spread per un paese
        double spreadItaly = euroBank.CalculateSpread("Italia");
        double spreadSpain = euroBank.CalculateSpread("Spagna");

        Console.WriteLine($"Lo spread per l'Italia è: {spreadItaly}");
        Console.WriteLine($"Lo spread per la Spagna è: {spreadSpain}");

        // Esempio di controllo dei diritti umani per un paese
        string humanRightsCheckFrance = euroBank.CheckHumanRights("Francia");
        string humanRightsCheckGermany = euroBank.CheckHumanRights("Germania");
        string humanRightsCheckUSA = euroBank.CheckHumanRights("Stati Uniti");
        string humanRightsCheckUnknown = euroBank.CheckHumanRights("PaeseSconosciuto");

        Console.WriteLine("\nRisultati del controllo dei diritti umani:");
        Console.WriteLine($"Francia: {humanRightsCheckFrance}");
        Console.WriteLine($"Germania: {humanRightsCheckGermany}");
        Console.WriteLine($"Stati Uniti: {humanRightsCheckUSA}");
        Console.WriteLine($"Paese Sconosciuto: {humanRightsCheckUnknown}");
    }
}