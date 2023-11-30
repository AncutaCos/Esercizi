using System;
using System.Collections.Generic;

namespace PublicAdministration
{
    public class EUID
    {
        public string Name;
        public string Surname;
        public string Birthdate;
        public string BirthPlace;
        public string Citizenship;
    }

    public class Person
    {
        public string Name;
        public string Age;
    }

    public class State : EUPublicAdministration
    {
        public string Name { get; set; }
        public List<Regione> Regioni { get; set; }

        public State(string NomePaese)
        {
            Name = NomePaese;
            Regioni = new List<Regione>();
        }

        public void CreateRegione(string NomeRegione)
        {
            Regioni.Add(new Regione(NomeRegione));
        }

        public void CreateProvincia(string Regione, string NomeProvincia)
        {
            Regioni[0].CreateProvincia(NomeProvincia);
        }

        public void CreateComune(string Regione, string Provincia, string Comune)
        {
            Regioni[0].Province[0].CreateComune(Comune);
        }

        public void ShowComune(string Comune)
        {
            Console.WriteLine($"Il comune di {Comune} appartiene a:");
            Console.WriteLine($"Paese: {Name}");
            Regioni[0].Province[0].Comuni[0].ShowComune(Comune);
        }

        public override void Welfare()
        {
            Console.WriteLine($"Welfare implemented by the state: {Name}");
        }

        public override void TerritoryManagement(State Claimer, State Dest)
        {
            EUParlament.ManageTerritoryConflict(Claimer, Dest);
        }
    }

    public class Regione
    {
        public string Name { get; set; }
        public List<Provincia> Province { get; set; }

        public Regione(string Name)
        {
            this.Name = Name;
            Province = new List<Provincia>();
        }

        public void CreateProvincia(string ProvinciaName)
        {
            Province.Add(new Provincia(ProvinciaName));
        }

        public void CreateComune(string NomeComune, string NomeProvincia)
        {
            Province[0].CreateComune(NomeComune);
        }

        public void ShowComune(string Comune)
        {
            Console.WriteLine($"Regione: {Name}");
            Province[0].Comuni[0].ShowComune(Comune);
        }

        public void Welfare()
        {
            Console.WriteLine($"Implemented by region : {Name}");
        }
    }

    public class Provincia
    {
        public string Name { get; set; }
        public List<Comune> Comuni { get; set; }

        public Provincia(string ProvinciaName)
        {
            Name = ProvinciaName;
            Comuni = new List<Comune>();
        }

        public void CreateComune(string ComuneName)
        {
            Comuni.Add(new Comune(ComuneName));
        }

        public void ShowComune(string Comune)
        {
            Console.WriteLine($"Provincia: {Name}");
        }

        public void Welfare()
        {
            Console.WriteLine($"Implemented by local entity Provincia {Name}");
        }
    }

    public class Comune
    {
        public string Name { get; set; }
        public List<EUID> Residents { get; set; }

        public Comune(string Name)
        {
            this.Name = Name;
            Residents = new List<EUID>();
        }

        public void TerritoryManagement()
        {
            throw new NotImplementedException();
        }

        public void Welfare()
        {
            Console.WriteLine($"Implemented by local entity Comune {Name}");
        }

        public EUID CreateCitizen(Person Person)
        {
            EUID EUCitizen = new EUID
            {
                Surname = Person.Name,
                Name = Person.Name
            };
            Residents.Add(EUCitizen);
            return EUCitizen;
        }

        public void ShowComune(string Comune)
        {
            Console.WriteLine($"Comune: {Name}, Popolazione: {Residents.Count}");
        }
    }

    public static class EUParlament
    {
        public static void ManageTerritoryConflict(State Claimer, State issuer)
        {
            Console.WriteLine($"Issue between {Claimer.Name} and  {issuer.Name}");
        }
    }

    public abstract class EUPublicAdministration
    {
        public void PA_Education()
        {
            throw new NotImplementedException();
        }

        public void PA_HNS()
        {
            throw new NotImplementedException();
        }

        public void PA_Justice()
        {
            throw new NotImplementedException();
        }

        public abstract void TerritoryManagement(State Claimer, State Dest);

        public abstract void Welfare();
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // Esempio di utilizzo del codice
            TestPopulationManagement();
        }

        public static void TestPopulationManagement()
        {
            State italy = new State("Italy");

            italy.CreateRegione("Lombardy");

            italy.CreateProvincia("Lombardy", "Milan");

            italy.CreateComune("Lombardy", "Milan", "Milano");

            Person person1 = new Person { Name = "John", Age = "30" };
            Person person2 = new Person { Name = "Alice", Age = "25" };
            Person person3 = new Person { Name = "Bob", Age = "40" };

            EUID citizen1 = italy.Regioni[0].Province[0].Comuni[0].CreateCitizen(person1);
            EUID citizen2 = italy.Regioni[0].Province[0].Comuni[0].CreateCitizen(person2);
            EUID citizen3 = italy.Regioni[0].Province[0].Comuni[0].CreateCitizen(person3);

            italy.ShowComune("Milano");

            Console.WriteLine($"Citizen 1: {citizen1.Name} {citizen1.Surname}, Age: {person1.Age}");
            Console.WriteLine($"Citizen 2: {citizen2.Name} {citizen2.Surname}, Age: {person2.Age}");
            Console.WriteLine($"Citizen 3: {citizen3.Name} {citizen3.Surname}, Age: {person3.Age}");

            italy.Welfare();
            italy.TerritoryManagement(italy, italy);
        }
    }
}