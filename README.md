# Archery XR - Zręcznościowa gra w technologii rozszerzonej rzeczywistości

Opracowanie projektu oraz implementacja gry zręcznościowej z wykorzystaniem technologii rozszerzonej rzeczywistości polegającej na strzelaniu z łuku do tarcz. Aplikacja ta nie tylko dostarcza użytkownikowi rozrywki, lecz także stanowi interaktywną platformę 
umożliwiającą zapoznanie się z technologią, która integruje świat rzeczywisty z elementami wirtualnymi.

## Rozpoczęcie rozgrywki

Na początku należy skonfigurować przestrzeń, w której będzie toczyć się rozgrywka. Od ustawienia ścian zależy kierunek, z którego będą pojawiać się tarcze. Konfigurację można przeprowadzić ręcznie (np. na zewnątrz) lub automatycznie, wykorzystując wykrywanie powierzchni.
Następnie należy określić wysokość podłogi – to ona decyduje o miejscu, w którym zatrzymają się strącone tarcze. Po zakończeniu konfiguracji można rozpocząć rozgrywkę.

## Rozgrywka

Rozgrywka została podzielona na pięć rund, z których każda posiada ściśle określone parametry:

- liczbę tarcz,
- czas generacji kolejnej tarczy,
- prędkość ich poruszania się,
- minimalną liczbę punktów wymaganą do przejścia do następnej rundy.

Punkty zdobywa się poprzez trafienie w różne obszary tarczy — im bliżej środka, tym więcej punktów zostaje przyznanych. Tarcze pojawiają się w portalu umieszczonym na jednej ze ścian i poruszają się w kierunku ściany naprzeciwległej. Jeżeli nie zostaną trafione, znikają po dotarciu do celu.

Ustrzelone tarcze spadają na poziom podłogi i pozostają tam przez kilka sekund, po czym są usuwane z przestrzeni gry w celu optymalizacji zasobów.


## Wykorzystane Technologie

- Śledzenie dłoni (Hand Tracking) – umożliwia wykrywanie ruchów rąk użytkownika, eliminując potrzebę korzystania z kontrolerów.
- Chwyt na dystans (Grab Distance) – pozwala na automatyczne przyciąganie obiektów z odległości, ułatwiając interakcję z elementami gry.
- Chwyt manualny (Grab) – umożliwia ręczne uchwycenie łuku w przestrzeni wirtualnej.
- Silnik fizyki – wykorzystany do obsługi kolizji, symulacji grawitacji oraz realistycznego zachowania obiektów w trakcie rozgrywki.
- Efekty dźwiękowe (Audio FX) – przestrzenne efekty audio wspomagają orientację użytkownika w przestrzeni i wzbogacają wrażenia z rozgrywki.

## Wykorzystane narzędzia
- C# – język programowania używany do tworzenia logiki gry i obsługi interakcji.
- Unity – silnik wykorzystywany do projektowania i implementacji środowiska gry oraz mechanik rozgrywki.
- Meta XR All-in-One SDK – zestaw narzędzi opracowany przez firmę Meta, wspierający tworzenie aplikacji XR na urządzenia z serii Quest.
- URP (Universal Render Pipeline) – zoptymalizowany potok renderowania w Unity, pozwalający na tworzenie grafiki o wysokiej jakości przy zachowaniu wydajności.
- Visual Studio – zintegrowane środowisko programistyczne (IDE) wykorzystywane do pisania i debugowania skryptów w C#.
- Meta Quest Link – aplikacja umożliwiająca połączenie gogli Meta Quest z komputerem w celu testowania i uruchamiania aplikacji bezpośrednio z edytora Unity.
- XR Plugin Management – wtyczka Unity służąca do zarządzania rozszerzeniami XR i konfiguracji projektów dla różnych platform XR.
- Meta Quest 3 – fizyczne urządzenie wykorzystywane do testowania i uruchamiania aplikacji w rzeczywistości rozszerzonej.

## Elementy gry

Do kluczowych elementów gry należą:

- Łuk – główne narzędzie interakcji, wykorzystywane do oddawania strzałów.
- Tarcze – ruchome cele, do których gracz oddaje strzały w celu zdobycia punktów.
- Strzały – wirtualne pociski wystrzeliwane z łuku, których trajektoria i siła zależą od naciągnięcia cięciwy.

> Na życzenie (najlepiej przesłane w wiadomości prywatnej) istnieje możliwość udostępnienia nagrania prezentującego przebieg rozgrywki.

## Galeria

<img src="https://github.com/user-attachments/assets/6193fb80-55ca-42f6-a36c-529118a65e60" height="300"/>
<img src="https://github.com/user-attachments/assets/37a47fdf-e833-4601-b2c1-551227251b1d" height="300"/>

<img src="https://github.com/user-attachments/assets/5cb4ab62-c057-4fa6-ac17-3fad8654ac30" height="300"/>
<img src="https://github.com/user-attachments/assets/df47a53e-f9a1-43e7-ad6a-81bf9c1e40c6" height="300"/>



## Testowanie i Wydajność
Aplikacja była testowana na fizycznym urządzeniu Meta Quest 3. Przeprowadzono testy zarówno w trybie gry, jak i w trybie edycji, co pozwoliło na weryfikację poprawności działania mechanik oraz interfejsów użytkownika w różnych warunkach.

Wydajność została oceniona za pomocą narzędzia Meta Quest Developer Hub (MQDH), które umożliwiło monitorowanie parametrów takich jak liczba klatek na sekundę (FPS), zużycie pamięci oraz obciążenie procesora i GPU.

![image](https://github.com/user-attachments/assets/365ec404-2373-490b-b379-53a56985b79c)
