# BeFit - Dziennik Treningowy

Projekt zaliczeniowy z przedmiotu Programowanie Zaawansowane. Aplikacja webowa w technologii ASP.NET Core MVC służąca do monitorowania postępów treningowych.

##  Instrukcja uruchomienia

Aby poprawnie uruchomić projekt i załadować dane startowe, wykonaj następujące kroki:

1. Otwórz plik `HomeFinances.sln` w Visual Studio.
2. Otwórz **Package Manager Console** (Widok -> Inne okna -> Konsola menedżera pakietów).
3. Wpisz i zatwierdź polecenie, aby utworzyć bazę i załadować przykładowe ćwiczenia:
   ```powershell
   Update-Database
Uruchom aplikację (F5).

 WAŻNE: Jak uzyskać uprawnienia Administratora
Aby spełnić wymóg edycji "Bazy Ćwiczeń" (dostępnej tylko dla roli Administrator), zaimplementowano mechanizm szybkiego nadawania uprawnień.

Instrukcja krok po kroku:

Zarejestruj nowego użytkownika i zaloguj się.

W menu głównym kliknij czerwony przycisk: ADMIN SETUP (Kliknij raz). (Zobaczysz komunikat o nadaniu roli).

Wyloguj się i zaloguj ponownie. Uwaga: Jest to konieczne, aby odświeżyć ciasteczko sesji. Bez przelogowania przyciski "Edytuj" i "Usuń" w Bazie Ćwiczeń nie będą widoczne.

Po ponownym zalogowaniu masz pełne uprawnienia administracyjne.

## Realizacja wymagań (Kryteria oceny)
Projekt spełnia wszystkie punkty określone w zadaniu:

#### 1. Modele
Zaimplementowano trzy wymagane modele z pełną walidacją (atrybuty Required, Range, MaxLength) oraz polskimi etykietami (Display):

ExerciseType: Typ ćwiczenia (Nazwa).

TrainingSession: Sesja treningowa (Data start/stop, powiązanie z Użytkownikiem).

SessionExercise: Ćwiczenie w sesji (Obciążenie, serie, powtórzenia, powiązania).

#### 2. Typy ćwiczeń (Baza Ćwiczeń)
Kontroler: ExerciseTypesController

Widok: Lista ćwiczeń jest dostępna publicznie dla wszystkich.

Uprawnienia: Akcje Create, Edit, Delete są zabezpieczone atrybutem [Authorize(Roles = "Administrator")] i niewidoczne dla zwykłych użytkowników.

#### 3. Sesje treningowe (Moje Treningi)
Kontroler: TrainingSessionsController

Izolacja danych: Użytkownik ma dostęp (odczyt/edycja/usuwanie) wyłącznie do własnych sesji.

Automatyzacja: Podczas tworzenia sesji UserId jest pobierany dynamicznie z kontekstu zalogowanego użytkownika (brak pola w formularzu).

Bezpieczeństwo: Backend weryfikuje własność rekordu przy każdej operacji.

#### 4. Ćwiczenia w sesji (Dziennik Serii)
Kontroler: SessionExercisesController

UX: Formularze wykorzystują listy rozwijane z czytelnymi nazwami (zamiast ID).

Logika: Można dodać serię tylko do sesji należącej do zalogowanego użytkownika. Zabezpieczono przed edycją cudzych wpisów.

#### 5. Widoki i Layout
Projekt wykorzystuje framework Bulma dla nowoczesnego wyglądu.

Wszystkie widoki posiadają czytelne etykiety w języku polskim.

Nawigacja jest intuicyjna i wzbogacona o ikony.

#### 6. Statystyki
Kontroler: StatisticsController

Logika: Dane są pobierane i obliczane jednym zapytaniem LINQ.

Zakres: Uwzględniane są tylko sesje z ostatnich 4 tygodni.

Prezentowane dane:

Liczba wykonanych treningów danego typu.

Łączna suma powtórzeń (Serie × Powtórzenia).

Średnie oraz maksymalne użyte obciążenie.

### Technologie
Backend: C# .NET 8, ASP.NET Core MVC

Baza danych: SQLite, Entity Framework Core

Frontend: Razor Views, Bulma CSS, FontAwesome
