# Coffee Slot Machine (Entity Framework)

## Lehrziele

* Entity Framework
* Eigene Unittests


Für den allseits beliebten Kaffeeautomaten ist ein Persistencelayer zu implementieren.

Die Geschäftslogik und die Entities werden im Corelayer verwaltet. Die Anforderungen des Corelayers an den DAL werden wieder über entsprechende Contracts definiert.

![Klassendiagramm](.\images\00_classdiagram.png)

Die im Automaten vorhandenen Münzen (5, 10, 20, 50, 100, 200 Cent) werden in der Datenbank (Coin) gespeichert. Am Beginn des Programms sind von jedem Münztyp 3 Münzen vorhanden (Implementierung über SeedDatabase-Migration). Der Einwurf und die Rückgabe verändern natürlich das Münzdepot!

## BusinessLogic im Core.Logic-Layer

Der `OrderController` stellt sowohl statistische Abfragen (`GetCoinDepot`, `GetProducts`) auf den
Kaffeeautomaten als auch die Nutzmethoden zur Abwicklung einer Bestellung zur Verfügung.

Die richtige Implementierung wird über Unittests geprüft, die teilweise noch zu erstellen sind.

### Nutzmethoden für die Abwicklung von Bestellungen

Der `OrderController` implementiert den Ablauf einer Bestellung in Zusammenarbeit mit dem
Persistencelayer:

![Prozess](.\images\01_process.png)

Die Bestellung beginnt mit der Auswahl eines Kaffeetyps (`OrderController.OrderCoffee(..);` siehe
Tabelle `Products`). Daraufhin erfolgt der Einwurf der Münzen, bis der Preis des Produkts zumindest
erreicht wird (`OrderController.InsertCoin(..)`). Die Münzen werden erst in das Münzdepot des
Kaffeeautomaten übernommen, wenn der Einwurf abgeschlossen ist. Bis dahin verbleiben sie in einem
temporären Puffer (`ThrownInCoins`).

Nach dem Einwurf wird das Restgeld in möglichst großen Münzen zurückgegeben (`ReturnCoinValues`).

Sollten im Depot die benötigten Münzen nicht mehr vorhanden sein, wird der Rest als Spende verbucht
(`DonationCents`).

Jener Teil des Ablaufs, der nur die Logik (nicht die Persistenz) des Bestellablaufs betrifft (`InsertCoin`,
`FinishPayment`), wird in der Domainklasse `Order` abgewickelt (Kapselung der Verantwortlichkeiten).

### Randbedingungen

* Die Rückgabe der Münzen soll so erfolgen, dass möglichst wenige Münzen zurückgegeben
werden!

* Das Property `Order.ThrownInCoinValues` ist entsprechend untenstehendem Muster zu
implementieren:

   ```csharp
   /// <summary>
   /// Werte der eingeworfenen Münzen als Text. Die einzelnen
   /// Münzwerte sind durch ; getrennt (z.B. "10;20;10;50")
   /// </summary>
   public String ThrownInCoinValues { get; set; }
   ```


* Auch `Order.ReturnCoinValues` wird als `String` mit den zurückgegebenen Münzen gespeichert
* Die tatsächlichen Centwerte des Einwurfs und der Rückgabe lassen sich aus den jeweiligen
`Strings` berechnen
* Die `DonationCents` müssen nicht in der Datenbank persistiert werden, da sie ebenfalls berechenbar sind.
* Ein Abbruch des Einwurfs ist nicht vorzusehen.

## Unittest des Controllers inklusive Persistenz

Die korrekte Implementierung der Logik ist mittels Unittests zu prüfen. Es ist kein isolierter Test der Domainlogik vorgesehen (mit Mock-DAL), sondern es wird unter Verwendung des Persistencelayers und einer Testdatenbank getestet. Die relativ einfache Umsetzung bringt als Nachteil lange Laufzeiten der Unittests mit sich.

Einige Unittests sind bereits implementiert:

```csharp
public void T01_GetCoinDepot_CoinTypesCount_ShouldReturn6Types_3perType_SumIs1155Cents()
public void T02_GetProducts_9Products_FromCappuccinoToRistretto()
public void T03_BuyOneCoffee_OneCoinIsEnough_CheckCoinsAndOrders()
```

Die restlichen Unittests sind von Ihnen zu implementieren:

```csharp
public void T04_BuyOneCoffee_ExactThrowInOneCoin_CheckCoinsAndOrders()
public void T05_BuyOneCoffee_MoreCoins_CheckCoinsAndOrders()
public void T06_BuyMoreCoffees_OneCoins_CheckCoinsAndOrders()
public void T07_BuyMoreCoffees_UntilDonation_CheckCoinsAndOrders()
```