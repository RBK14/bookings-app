## API — format danych

### Waluta
- W API waluta jest przekazywana **zgodnie z kodami ISO 4217 (numeric)**.  
- Przykłady:  
  - `985` → PLN (złoty polski)
  - `978` → EUR (euro) 
  - `840` → USD (dolar amerykański)  

W kodzie aplikacji wartości te są mapowane na `enum Currency` z przypisanymi kodami ISO.

---
### Czas trwania wizyty
- Czas trwania (`Duration`) jest przekazywany w API w standardowym formacie `hh:mm:ss`.  
- Przykłady:  
  - `"00:15:00"` → 15 minut  
  - `"01:30:00"` → 1 godzina 30 minut  
  - `"04:00:00"` → 4 godziny  

W aplikacji jest on mapowany na `TimeSpan` (`Duration` jako Value Object) z walidacją:  
- minimalny czas: **15 minut**  
- maksymalny czas: **8 godzin**  