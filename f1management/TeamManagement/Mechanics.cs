﻿using f1management.Methods;
using System;
using System.Collections.Generic;

namespace f1management.TeamManagement
{
    // Klasa reprezentująca mechanika, dziedziczy po User i implementuje interfejs Information
    public class Mechanics : User, Information
    {
        // Konstruktor klasy Mechanics
        public Mechanics(string firstName, string lastName, string username)
            : base(firstName, lastName, Role.Mechanic, username)
        {
            try
            {
                // Wywołanie metody zapisującej dane użytkownika
                User.TriggerSave(this, Program.Drivers, Program.Mechanics, Program.Principals);
            }
            catch (Exception ex)
            {
                // Obsługa błędu przy dodawaniu mechanika
                Console.WriteLine($"Błąd przy dodawaniu mechanika: {ex.Message}");
            }
        }

        // Metoda wyświetlająca informacje o mechaniku
        public void DisplayInfo()
        {
            Console.WriteLine($"Mechanik: {FirstName} {LastName}");
        }

        // Metoda dodająca mechanika do listy
        public void AddMechanic(User user, List<Mechanics> mechanics)
        {
            try
            {
                if (!mechanics.Contains(this))
                {
                    mechanics.Add(this);
                    Console.WriteLine($"Mechanik {FirstName} {LastName} dodany.");
                }
                else
                {
                    Console.WriteLine($"Mechanik {FirstName} {LastName} już istnieje.");
                }
            }
            catch (Exception ex)
            {
                // Obsługa błędu przy dodawaniu mechanika
                Console.WriteLine($"Błąd przy dodawaniu mechanika: {ex.Message}");
            }
        }

        // Metoda usuwająca mechanika z listy
        public void RemoveMechanic(List<Mechanics> mechanics)
        {
            try
            {
                if (mechanics.Contains(this))
                {
                    mechanics.Remove(this);
                    Console.WriteLine($"Mechanik {FirstName} {LastName} usunięty.");
                    // Wywołanie metody usuwającej dane użytkownika
                    User.TriggerRemove(this, Program.Drivers, Program.Mechanics, Program.Principals);
                }
                else
                {
                    Console.WriteLine($"Mechanik {FirstName} {LastName} nie istnieje.");
                }
            }
            catch (Exception ex)
            {
                // Obsługa błędu przy usuwaniu mechanika
                Console.WriteLine($"Błąd przy usuwaniu mechanika: {ex.Message}");
            }
        }
    }
}
