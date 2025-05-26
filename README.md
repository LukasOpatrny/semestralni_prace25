# Evidence spotřeby v domácnosti

**Aplikace pro jednoduchou správu a sledování spotřeby domácnosti.**

---

## Popis

Tato aplikace umožňuje uživatelům zaznamenávat různé typy spotřeby (energie, voda, plyn, atd.), filtrovat je podle data, typu nebo poznámky, a zároveň nabízí možnost exportu a importu dat ve formátu CSV. Součástí je i grafické zobrazení statistik a souhrnů.

---

## Hlavní funkce

- Přidávání, úprava a mazání záznamů spotřeby  
- Filtrování záznamů podle data, typu a textu v poznámce  
- Export dat do CSV souboru  
- Import dat ze CSV souboru  
- Zobrazení souhrnných statistik a grafů  
- Uživatelsky přívětivé rozhraní s menu a dialogy

---

## Požadavky

- Windows OS
- Visual studio 2022
- SQLite

---

## Instalace a spuštění

1. Naklonujte nebo stáhněte tento repozitář.  
2. Otevřete projekt v Visual Studio.
4. Odblokujte třídy
4. Sestavte projekt (`Build`).  
5. Spusťte aplikaci (`Start Debugging` nebo `Ctrl + F5`).  

---

## Použití

- **Přidání záznamu:** Vyplňte datum, typ, částku a poznámku, klikněte na „Přidat záznam“.  
- **Úprava záznamu:** Dvojklik na řádek, upravte údaje, potvrďte tlačítkem „Upravit záznam“.  
- **Smazání záznamu:** Vyberte řádek a klikněte na „Smazat záznam“.  
- **Filtrování:** Vyberte požadované datum nebo rozsah, typ, a/nebo text poznámky a klikněte na „Filtrovat“.  
- **Obnovení filtrů:** Klikněte na „Obnovit filtry“.  
- **Export dat:** Pomocí menu „Soubor → Export CSV“ exportujte aktuální záznamy do CSV souboru.  
- **Import dat:** Pomocí menu „Soubor → Import CSV“ načtěte záznamy z CSV souboru (přidávají se do databáze).  
- **Grafy:** V záložce „Podrobnější grafy“ zobrazíte vizualizace dat.

---

## Struktura CSV souboru

CSV soubor používá oddělovač `;` a obsahuje tyto sloupce:  
`Id;Date;ConsumptionType;Amount;Note`  

Datum je ve formátu `YYYY-MM-DD`, částka s desetinnou tečkou (invariantní kultura).
