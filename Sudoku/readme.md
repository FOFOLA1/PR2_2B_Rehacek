# Sudoku

Jednoduchá hra Sudoku vytvořená v jazyce C# pomocí WPF.

## Funkce

* **Více úrovní obtížnosti:** Neformální (6x6), lehký (9x9) a těžký (9x9).
* **Časovač hry:** Sledujte, jak dlouho trvá dokončit každou hru.
* **Statistiky:** Zaznamenává a zobrazuje nejlepší časy dokončení pro každou obtížnost.
* **Zvýraznění chyb:** Volitelně zvýrazní nesprávná čísla na hrací desce.
* **Poznámky:** Možnost přidat čísla jako poznámky do buněk s omezením na 3 čísla.
* **Vstup z klávesnice:** Použití číselných kláves (0-9 nebo NumPad 0-9), Backspace, 'P'/'N' nebo '/' (pro režim poznámek) a 'E'/'C' (pro zvýraznění chyb) pro rychlé zadávání.

## Jak hrát

1. **Spuštění hry:** Spusťte aplikaci a vyberte obtížnost (Neformální, Lehký, Těžký). Z tohoto hlavního menu si také můžete prohlédnout své statistiky.
2. **Výběr čísla:** Klikněte na tlačítko s číslem (1-9 nebo 1-6 v závislosti na obtížnosti) dole na obrazovce. Vybrané tlačítko bude zvýrazněno. Můžete také použít číselné klávesy na klávesnici.
3. **Umístění čísla:** Klikněte na prázdnou buňku v mřížce pro umístění vybraného čísla. Čísla lze zadávat pouze do buněk, které nebyly součástí původní hádanky.
4. **Smazání čísla:** Klikněte na tlačítko "Smazat" nebo stiskněte klávesu '0' nebo Backspace. Poté klikněte na buňku, kterou chcete vymazat.
5. **Používání poznámek:** Klikněte na tlačítko "Poznámky" nebo stiskněte 'P', 'N', nebo '/' pro přepnutí do režimu poznámek. V tomto režimu kliknutí na číslo a poté na buňku přidá nebo odebere číslo jako malou poznámku v buňce. Kliknutím na "Poznámky" znovu se vrátíte do normálního režimu.
6. **Zobrazení chyb:** Klikněte na tlačítko "Chyby" nebo stiskněte 'E' nebo 'C' pro zapnutí/vypnutí zvýraznění chyb. Nesprávná čísla (duplicitní v řádku, sloupci nebo bloku) budou zvýrazněna.
7. **Výhra:** Hra je dokončena, když jsou všechny buňky správně vyplněny. Váš čas bude zaznamenán a pokud je to nový rekord, bude uložen do statistik.
8. **Konečné menu:** Po dokončení hádanky se zobrazí menu s časem a informací, zda byl dosažen nový rekord. Kliknutím na "Zpět" se vrátíte na hlavní obrazovku.
9. ** Statistiky:** Z hlavního menu klikněte na "Statistiky" pro zobrazení nejlepších časů pro jednotlivé obtížnosti. Kliknutím na "Zpět" se vrátíte zpět.

## Struktura projektu

* `MainWindow.xaml`, `MainWindow.xaml.cs`: Definuje hlavní okno, logiku hry, ovládání UI, přechody mezi scénami, časovač a vstup z klávesnice a myši.
* `SudokuGen.cs`: Logika pro generování Sudoku podle zvolené obtížnosti.
* `AppData.cs`: Ukládání a načítání statistik hry (nejlepší časy) do lokálního JSON souboru.
* `Num.xaml`, `Num.xaml.cs`: Reprezentuje jednu buňku Sudoku, zajišťuje její zobrazení a interakci.

## Přehled kódu

### `MainWindow.xaml.cs` a `MainWindow.xaml`

* **Struktura UI:** Struktura aplikace pomocí `Grid` rozvržení. Různé "scény" (`ConfigScene`, `GameScene`, `EndScene`, `Stats`) se zobrazují/skrývají pomocí `Visibility`. Definuje styly tlačítek a dalších prvků. `GameScene` obsahuje hlavní `Board` pro dynamické přidání buněk Sudoku a `buttons` pro výběr čísel a ovládací prvky.
* **Code:** Obsahuje logiku UI a herního stavu.

  * **Fáze hry:** `enum GameStage` (`Config`, `Game`, `Finished`, `Stats`) pro správu stavu aplikace a přepínání scén.
  * **Obtížnost:** `enum Difficulty` (`Informal`, `Easy`, `Hard`) pro definování velikosti mřížky a počtu prázdných buněk.
  * **Inicializace UI:** Konstruktor nastavuje eventy a načítá styly.
  * **ConfigScene:** `ConfigSceneButtonClick` zpracovává výběr obtížnosti, generování mřížky (`PreGenBoard`) a přechod do hry. `Stats_Button_Click` přechod do statistik.
  * **Generování mřížky:** Dynamicky vymaže a znovu vytvoří `Board` a `buttons` podle obtížnosti. Volá `SudokuGen.GenerateSudoku`, vytvoří ovládací prvky `Num` a nastaví počáteční čísla.
  * **Herní logika:**

    * `NumBtnClick`: Zpracovává kliknutí na číslo/smazání, nastaví `_selectedNum` které slouží pro ukládání aktuálně zakliklého čísla (popřípadě mazání).
    * `NumMouseDown`: Kliknutí na buňku Sudoku k přidání/mazání čísel nebo poznámek.
    * `comments_Click`: Přepíná režim poznámek (`_isCommentsEditMode`).
    * `errors_button_Click`: Přepíná zvýraznění chyb (`_showErrors`) a volá `CheckErrors`.
    * `CheckErrors`: Kontroluje duplicity v řádcích, sloupcích a blocích. Pokud je `_showErrors` aktivní, zvýrazní chybné buňky. Pokud je deska bez chyb a zcela vyplněna, vrací `true` (výhra).
  * **Časovač:** `DispatcherTimer` (`_timer`) sleduje čas. `Timer_Tick` přičítá čas a aktualizuje `timeLabel` v UI.
  * **Konec hry:** `renderFinishStage` připraví zprávu o čase a rekordu. `EndScene_BackButton_Click` návrat do konfigurace.
  * **Statistiky:** `renderStats` vykreslí nejlepší časy pro obtížnosti. `StatsBackButtonClick` návrat.
  * **Klávesové vstupy:** Zpracuje klávesy pro čísla, smazání, poznámky a chyby.

### `SudokuGen.cs`

* Statická třída pro vytváření řešitelných hádanek Sudoku.
* `GenerateSudoku`: Podle obtížnosti nastaví velikost mřížky a počet prázdných polí, vytvoří kompletní řešení (`FillBoard`) a odstraní čísla (`CreatePuzzle`).
* `FillBoard`: Rekurzivní backtracking pro vytvoření platného řešení Sudoku s náhodným pořadím čísel.
* `IsSafe`: Kontrola, zda lze číslo umístit (podle pravidel).
* `CreatePuzzle`: Náhodně nastaví buňky na 0 (prázdné) podle zadaného počtu.
* `ShuffleList`: Pomocná metoda pro náhodné promíchání seznamu.
* `ConvertBoardToList`: Převede 2D pole na List Listů.

### `AppData.cs`

* Statická třída pro ukládání statistik (nejlepší časy).
* Používá `Dictionary<Difficulty, TimeSpan>` (`_timestamps`) pro ukládání.
* Ukládá do JSON souboru `stats.json` ve složce lokálních dat uživatele.
* Statický konstruktor načítá data při spuštění. Pokud soubor neexistuje, vytvoří adresář.
* `save`: Serializace a uložení.
* `CheckThenEdit`: Pokud nový čas překoná rekord, aktualizuje a uloží.
* `Get`: Vrací nejlepší čas pro danou obtížnost, nebo `TimeSpan.Zero`.

### `Num.xaml` a `Num.xaml.cs`

* `UserControl` reprezentující buňku Sudoku.
* Zobrazuje číslo, poznámky a stav buňky (výchozí, needitovatelná, chybná).
* Vystavuje vlastnosti jako `Symbol`, `NumType` a metody `SwitchComment`, `RemoveAllComments`.

