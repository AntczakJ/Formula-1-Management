﻿using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace f1management.Methods
{
    // Klasa odpowiadająca za zarządzanie hasłami użytkowników
    public class PasswordManager
    {
        // Ścieżka do pliku z hasłami użytkowników
        private const string _passwordFilePath = "userPasswords.txt";

        // Zdarzenie wywoływane po weryfikacji hasła
        public static event Action<string, bool> PasswordVerified;

        // Statyczny konstruktor klasy - tworzy plik z hasłami, jeśli nie istnieje
        static PasswordManager()
        {
            if (!File.Exists(_passwordFilePath))
            {
                File.Create(_passwordFilePath).Dispose();
            }
        }

        // Metoda zapisująca nowego użytkownika i jego zaszyfrowane hasło
        public static void SavePassword(string username, string password)
        {
            if (File.ReadLines(_passwordFilePath).Any(line => line.Split(',')[0] == username))
            {
                Console.WriteLine($"Użytkownik {username} już istnieje w systemie");
                return;
            }

            string hashedPassword = HashPassword(password);
            File.AppendAllText(_passwordFilePath, $"{username},{hashedPassword}\n");
            Console.WriteLine($"Użytkownik {username} został zapisany");
        }

        // Metoda weryfikująca poprawność hasła
        public static bool VerifyPassword(string username, string password)
        {
            string hashedPassword = HashPassword(password);

            foreach (var line in File.ReadLines(_passwordFilePath))
            {
                var parts = line.Split(',');
                if (parts[0] == username && parts[1] == hashedPassword)
                {
                    PasswordVerified?.Invoke(username, true); // wywołanie zdarzenia z powodzeniem
                    return true;
                }
            }

            PasswordVerified?.Invoke(username, false); // wywołanie zdarzenia z niepowodzeniem
            return false;
        }

        // Prywatna metoda do hashowania hasła przy użyciu SHA256
        private static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        // Metoda sprawdzająca, czy login już istnieje
        public static bool DoesLoginExist(string username)
        {
            return File.ReadLines(_passwordFilePath)
                .Any(line => line.Split(',')[0] == username);
        }

        // Metoda usuwająca użytkownika z pliku haseł
        public static void RemoveLogin(string username)
        {
            var lines = File.ReadAllLines(_passwordFilePath).ToList();
            lines = lines.Where(line => !line.StartsWith(username + ",")).ToList();
            File.WriteAllLines(_passwordFilePath, lines);
        }
    }
}
