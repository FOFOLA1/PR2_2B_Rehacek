# Prohlížeč obchodní sítě

Program napsaný v jazyce C# který umožňuje uživateli procházet strom obchodní sítě a ukládat si vybrané pracovníky do .txt souboru.

## Použití

<details><summary><b>Ve vlastním kódu</b></summary>

1. Stáhni zdrojový kód a vlož všechny tyto soubory do svého programu
2. Vytvoř nový objekt App
3. Nezapomeň na parametr __path__, pro vybrání cesty/souboru můžeš použít moji metodu SelectFile() z třídy [Utils](./Utils.cs). Ukázkové použití najdeš v souboru [Program.cs](./Program.cs).

</details>

<details><summary><b>Spuštění již připravené aplikace</b></summary>

1. [Stáhni a nainstaluj dotnet](https://dotnet.microsoft.com/en-us/download)
2. Stáhni zdrojový kód a v jeho kořenové složce otevři terminál
3. Proveď build pomocí commandu 
	```cmd
	dotnet build
	```
4. Spusť nově vytvořený .exe soubor který nalezneš v ```./bin/Debug/net8.0```

</details>

## Popis

Celé přepínání mezi pohledy funguje tak, že v hlavní třídě [App](./App.cs) jsou instance všech pohledů s proměnnou DisplayedMenu typu [Menu](./Menu.cs), tedy abstraktní třídy z který vychází dědí všechny použité pohledy, a díky dědění je do ní možné uložit pointer na jakýkoliv pohled. Kdy se má přepnout se vždy dozví z funkce Invoke, v případě návratové hodnoty true si DisplayedMenu uloží pointer na jiné menu.  
[App](./App.cs) také dále jistí i vstup z klávesnice a tyto data dále posílá do zvoleného menu.

Dědící třídy [Menu](./Menu.cs) ([ListMenu](./ListMenu.cs) a [BrowserMenu](./BrowserMenu.cs)) obsahují metody jako Display pro zobrazení a Invoke pro vykonání činnosti po výběru položky. Tyto dvě můžeme považovat za hlavní metody na kterých celé menu stojí, ostatní jsou jen pomocné.  
Celé menu se vykresluje postupně v metodě `Display`. Nejprve se vypíše prostý text dokud se nenarazí na vybratelné pole, tam proběhne logika zda není vybrané a podle toho se vypíše toto pole.  
Metoda `Invoke` v menu na základě indexu hledá jaké pole je vybraný a podle toho nadále vykonává akci.  
Dále tu můžeme najít metody jako `PreviousOption` a `NextOption` které posouvají mění vybranou položku o index vpřed nebo zpět.

Zbytek už jsou metody typické pro to dané menu, pro [ListMenu](./ListMenu.cs) je to načítání a ukládání dat z textového souboru, v [BrowserMenu](./BrowserMenu.cs) naopak práce s vybranými daty, dále pak obsahuje list History který ukládá postupným zanořováním do stromu všechny nadřízené.

## Popis tříd

###	[App](./App.cs)

* Slouží jako Main class celé aplikaci
* Stará se o vstup klávesnice
* Přepíná menu mezi sebou za využití abstrakce
* Má uložený pointer na strom obchodní sítě

### [Menu](./Menu.cs)

* Abstraktní třída z které dědí ostatní menu pro jednoduché přepínání mezi nimi
* Obsahuje statický list vybraných pracovníků které se tak stává společný pro všechny dědící třídy
* Zavádí metody a vlastnosti, které musí každé menu obsahovat, které jsou pak použity v třídě [App](./App.cs)
  
#### Vlastnosti

  * **OptionsCount** - Počet "tlačítek" mezi kterými se listuje
  * **SelectedOption** - Číslo indexu aktuálně zvolené možnosti
  * **Options** - List neměnných tlačítek v menu
  * **_data** - Seznam vybraných pracovníků

#### Metody

  * **Display** - Vykresluje menu
  * **NextOption** - Změní vybranou možnost na další
  * **PreviousOption** - Změní vybranou možnost na předchozí
  * **Invoke** - Provede akci na vybrané možnosti
    * Návratová hodnota je nyní boolean která určuje, zda se mají menu přepnout, v případě potřeby se to dalo upravit na jiný datový typ, pokud by to mělo počítat s více dědícími třídami než 2
  * **GetSelectedOption** - Vrátí aktuálně vybranou možnost



###	[BrowserMenu](./BrowserMenu.cs)

* Interaktivně zobrazuje seznam pracovníků
* Umožňuje jednotlivé pracovníky přidat nebo odebrat ze seznamu

### [ListMenu](./ListMenu.cs)

* Vyobrazuje aktuálně vybrané pracovníky
* Umožňuje vybrané pracovníky uložit do souboru
* Umožňuje načíst již vytvořený soubor s pracovníky a provádět na něm změny

### [Option](./Option.cs)

* Třída pro tvorbu objektů které slouží v menu na interakci s uživatelem
* Může být tvořen společně s pointerem na objekt daného pracovníka nebo prostým textem
  * Pokud se objekt vytvoří s parametrem typu string, jeho vlastnost DisplayText se rovná tomuto stringu
  * Pokud se objekt vytvoří s parametrem typu Salesman, jeho vlastnost DisplayText se bude vždy rovnat jménu a příjmení obchodníka

### [Utils](./Utils.cs)

* Obsahuje pomocné statické funkce
  * **Write** - Vypíše text v určené barvě (výchozí bílá) do console
  * **WriteSelected** - Vypíše text jako vybranou položku => černý text na žlutým podkladu
  * **WriteOption** - Vypíše objekt [Option](./Option.cs) s kontrolou zda není vybraný
  * **WriteMsg** - Vypíše uživateli zadanou zrpávu, bude zobrazena dokud nestiskne libovolnou klávesu
  * **ListSubmenu** - Interaktivní výběr z položek v consoli s možným nadpisem
  * **SelectFile** - Pomocí ListSubmenu metody nechává uživatele vybrat soubor s určitou příponou z aktuální složky včetně podsložek

