# Formula-1-Management
F1 Management System to aplikacja w C# umożliwiająca zarządzanie zespołami Formuły 1. System obsługuje role użytkowników (administrator, mechanik, kierowca) oraz funkcje związane z zespołami, wyścigami i personelem.

## 🚀 Kluczowe funkcje  
- **RBAC** – trzy role: dyrektor, mechanik, kierowca  
- **Zarządzanie zespołami** – dodawanie/usuwanie, budżet, mechanicy i kierowcy  
- **Zarządzanie wyścigami** – dodawanie/usuwanie, klasyfikacja  
- **Obsługa użytkowników** – zarządzanie kierowcami, mechanikami i administratorami  
- **Logowanie i logi systemowe**  


Dokumentacja programu F1 Management
Wprowadzenie
Program F1 Management to kompleksowe narzędzie do zarządzania zespołem Formuły 1, umożliwiające kontrolę nad kierowcami, mechanikami, szefami zespołów oraz budżetem. System implementuje Role-Based Access Control (RBAC), co oznacza, że różni użytkownicy mają różne uprawnienia w zależności od swojej roli.

Role użytkowników
Principal (Szef zespołu) - ma pełne uprawnienia do zarządzania zespołem

Mechanic (Mechanik) - może przeglądać informacje i zarządzać częścią techniczną

Driver (Kierowca) - ma podstawowe uprawnienia do przeglądania informacji

Instalacja i uruchomienie
Wymagane środowisko: .NET Core 3.1 lub nowsze

Kompilacja: dotnet build

Uruchomienie: dotnet run

Rejestracja i logowanie
Rejestracja nowego użytkownika
Wybierz opcję "1. Zarejestruj użytkownika" z menu głównego

Podaj imię, nazwisko i unikalny login

Wybierz rolę (1-Principal, 2-Mechanic, 3-Driver)

Ustaw hasło

Logowanie
Wybierz opcję "2. Zaloguj się" z menu głównego

Podaj login i hasło

Po pomyślnym zalogowaniu zostaniesz przekierowany do menu odpowiedniego dla Twojej roli

Przewodnik po funkcjonalnościach
Dla Principal (Szefa zespołu)
Dodawanie zespołu

Wybierz opcję "2. Dodaj zespół" z menu

Podaj nazwę zespołu

Zespół zostanie utworzony z zerowym budżetem

Zarządzanie budżetem

Wybierz opcję "3. Zwiększ budżet zespołu" lub "4. Zmniejsz budżet zespołu"

Wybierz zespół z listy

Podaj kwotę i powód zmiany budżetu

Dodawanie członków zespołu

Najpierw dodaj kierowcę/mechanika do systemu (opcje 11, 12)

Następnie przypisz do zespołu (opcje 8, 9)

Jako Principal możesz przypisać siebie do zespołu (opcja 13)

Zarządzanie wyścigami

Dodaj nowy wyścig (opcja 10)

Przeglądaj informacje o wyścigach (opcja 14)

Dla Mechanic (Mechanika)
Przeglądanie informacji

Możesz wyświetlić informacje o zespole (opcja 1)

Przeglądać listę kierowców (opcja 5)

Przeglądać listę mechaników (opcja 7)

Dla Driver (Kierowcy)
Przeglądanie informacji

Możesz wyświetlić informacje o zespole (opcja 1)

Przeglądać listę kierowców (opcja 5)

Przeglądać informacje o wyścigach (opcja 14)

Funkcje specjalne
Logowanie zdarzeń

Wszystkie istotne działania są logowane do pliku "Lista.txt"

Logowane są zarówno operacje dodawania jak i usuwania użytkowników, zmiany w zespołach i budżecie

Bezpieczeństwo

Hasła są przechowywane w postaci zahashowanej (SHA256)

Każda rola ma przypisane konkretne uprawnienia

Zarządzanie użytkownikami

Usunięcie użytkownika powoduje również usunięcie jego danych logowania

Przykładowe scenariusze użycia
Scenariusz 1: Tworzenie nowego zespołu
Zaloguj się jako Principal

Wybierz "2. Dodaj zespół"

Podaj nazwę zespołu (np. "Red Bull Racing")

Dodaj siebie jako szefa zespołu (opcja 13)

Dodaj kierowców i mechaników

Scenariusz 2: Zarządzanie budżetem
Zaloguj się jako Principal

Wybierz zespół

Zwiększ budżet o 1 000 000$ z powodu "Nowy sponsor"

Sprawdź aktualny budżet w informacjach o zespole

Scenariusz 3: Przygotowanie do wyścigu
Dodaj nowy wyścig (nazwa i data)

Przypisz kierowców do zespołu

Przypisz mechaników do zespołu

Sprawdź informacje o wyścigu```

Struktura plików
Program korzysta z kilku plików do przechowywania danych:

userPasswords.txt - przechowuje dane logowania użytkowników

Lista.txt - logi wszystkich działań w systemie

Bezpieczeństwo i uwagi
Hasła są przechowywane w postaci zahashowanej i nie można ich odzyskać

Tylko Principal może dodawać nowych użytkowników
