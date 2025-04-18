﻿using f1management.Methods;
using System;
using System.Collections.Generic;

namespace f1management.TeamManagement
{
    // Klasa reprezentująca kierowcę, dziedziczy po User i implementuje interfejs Information
    public class Driver : User, Information
    {
        // Konstruktor klasy Driver
        public Driver(string firstName, string lastName, string username)
            : base(firstName, lastName, Role.Driver, username)
        {
            try
            {
                // Wywołanie metody zapisującej dane użytkownika
                User.TriggerSave(this, Program.Drivers, Program.Mechanics, Program.Principals);
            }
            catch (Exception ex)
            {
                // Obsługa błędu przy dodawaniu kierowcy
                Console.WriteLine($"Błąd przy dodawaniu kierowcy: {ex.Message}");
            }
        }

        // Metoda wyświetlająca informacje o kierowcy
        public void DisplayInfo()
        {
            Console.WriteLine($"Kierowca: {FirstName} {LastName}");
        }

        // Metoda dodająca kierowcę do listy
        public void AddDriver(List<Driver> drivers)
        {
            try
            {
                if (!drivers.Contains(this))
                {
                    drivers.Add(this);
                    Console.WriteLine($"Kierowca {FirstName} {LastName} dodany.");
                }
                else
                {
                    Console.WriteLine($"Kierowca {FirstName} {LastName} już istnieje.");
                }
            }
            catch (Exception ex)
            {
                // Obsługa błędu przy dodawaniu kierowcy
                Console.WriteLine($"Błąd przy dodawaniu kierowcy: {ex.Message}");
            }
        }

        // Metoda usuwająca kierowcę z listy
        public void RemoveDriver(List<Driver> drivers)
        {
            try
            {
                if (drivers.Contains(this))
                {
                    drivers.Remove(this);
                    Console.WriteLine($"Kierowca {FirstName} {LastName} usunięty.");
                    // Wywołanie metody usuwającej dane użytkownika
                    User.TriggerRemove(this, Program.Drivers, Program.Mechanics, Program.Principals);
                }
                else
                {
                    Console.WriteLine($"Kierowca {FirstName} {LastName} nie istnieje.");
                }
            }
            catch (Exception ex)
            {
                // Obsługa błędu przy usuwaniu kierowcy
                Console.WriteLine($"Błąd przy usuwaniu kierowcy: {ex.Message}");
            }
        }
    }
}
