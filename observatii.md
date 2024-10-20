1. Cu coordonatele `GL.Ortho(-5.0, 5.0, -5.0, 1.0, 0.0, 4.0)`, scena va fi vizibilă într-un volum cu un câmp de vedere de 10 unități pe axa X (între -5 și 5) și 6 unități pe axa Y (între -5 și 1), iar adâncimea pe axa Z va fi între 0 și 4. Triunghiul ar putea apărea redimensionat sau translatat, depinzând de cum se încadrează în acest nou volum.
   Triunghiul apare mai centrat cu varful in jos.

3)

1. Un viewport reprezintă o zonă a ferestrei (sau a ecranului) în care se va desena conținutul grafic. Dimensiunea viewport-ului este specificată în coordonate de pixel, iar conținutul 3D este proiectat pe acest viewport. Funcția glViewport(x, y, width, height) este utilizată pentru a seta viewport-ul.

2. Frames per second (FPS) reprezintă numărul de cadre sau imagini (frames) generate și afișate într-o secundă. În OpenGL, un FPS mai mare indică o performanță grafică mai bună, permițând animații mai fluide. FPS-ul este adesea utilizat pentru a evalua eficiența aplicațiilor grafice și a jocurilor.

3. Metoda OnUpdateFrame() este rulată în fiecare cadru (frame) al aplicației, înainte de procesarea evenimentelor de randare. Aceasta este folosită pentru a actualiza logica aplicației, cum ar fi mișcarea obiectelor, actualizarea stării jocului sau gestionarea intrărilor utilizatorului.

4. Modul imediat de randare (Immediate Mode Rendering) este un mod de a desena geometrie în OpenGL prin trimiterea de comenzi de randare direct în timp real, fără a folosi un buffer de vertex-uri. Acesta permite dezvoltatorilor să definească și să afișeze forme direct în cadrul funcțiilor de randare, dar este mai puțin eficient comparativ cu metodele mai recente, cum ar fi buffer-urile de vertex-uri sau shader-urile.

5. Ultima versiune de OpenGL care acceptă modul imediat de randare este OpenGL 3.3. De la versiuni ulterioare, modul imediat a fost depreciat și este recomandat să se utilizeze metode mai eficiente, cum ar fi vertex buffer objects (VBOs).

6. Metoda OnRenderFrame() este rulată după ce logica aplicației a fost actualizată (OnUpdateFrame) și este responsabilă pentru desenarea efectivă a scenei. Aceasta se ocupă cu randarea obiectelor grafice pe ecran.

7. Metoda OnResize() este esențială pentru a actualiza viewport-ul și, de obicei, perspectivele de proiecție atunci când dimensiunea ferestrei se schimbă. Aceasta trebuie să fie apelată cel puțin o dată la început pentru a asigura că scena este redimensionată corect și că toate aspectele grafice se adaptează noilor dimensiuni ale ferestrei.

8. Metoda CreatePerspectiveFieldOfView() este utilizată pentru a genera o matrice de proiecție în perspectivă, având patru parametri principali: fieldOfView, aspectRatio, nearPlane și farPlane. FieldOfView reprezintă unghiul de vizibilitate verticală, de obicei exprimat în radiani, și are un interval recomandat de la 0.0 la π/2 (90 de grade), permițând ajustarea câmpului vizual al camerei. AspectRatio este raportul dintre lățimea și înălțimea feestrei și trebuie să fie un număr pozitiv și diferit de zero pentru a evita distorsiunile imaginii. NearPlane reprezintă distanța dintre cameră și planul apropiat de vizualizare, având valori pozitive, iar FarPlane definește distanța la care se află planul îndepărtat, care trebuie să fie mai mare decât valoarea lui nearPlane. Acești parametri sunt esențiali pentru stabilirea corectă a perspectivei și a profunzimii în randarea scenei.
