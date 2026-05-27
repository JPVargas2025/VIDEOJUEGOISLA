# Plan de Implementación - Scene 1: Beach (Hub)

## 1. Objetivo y Contexto
El propósito de este documento es detallar la implementación exhaustiva para finalizar las tareas pendientes de la **Scene 1: Beach (Hub)**, según lo establecido en el documento de diseño (`README.md`).

A pesar de que el núcleo de la escena y el flujo entre niveles ya está construido, faltan dos mecánicas principales orientadas a la interfaz de usuario (UI) y al progreso del juego:
1. La interacción con el cartel (Billboard/Sign) para revelar el **Panel de Inventario**.
2. La culminación de la secuencia final en la **Nave Espacial (Spaceship)**.

---

## 2. Implementación del Panel de Inventario (Cartel Interactivo)

**Descripción del Requisito:**
El jugador debe poder acercarse a un cartel en la playa para abrir un Panel de Inventario tipo Scroll View. Este panel debe mostrar los 6 ítems del juego: en escala de grises si no han sido recogidos y a todo color si ya se recolectaron.

### 2.1. Desarrollo de Script: `CartelInventario.cs`
Se debe crear un nuevo script llamado `CartelInventario.cs` que gestione la apertura de la ventana de UI y la actualización de los gráficos.

*   **Variables y Referencias UI:**
    *   `public GameObject panelInventario;` (Referencia al panel principal).
    *   `public Image[] imagenesItems;` (Arreglo con las 6 imágenes de los slots).
    *   `public Material materialGris;` (Material opcional para el efecto de escala de grises, o usar manipulación de color `Color.gray` vs `Color.white`).
*   **Detección de Interacción:**
    *   `OnTriggerEnter(Collider other)`: Si detecta al jugador (`other.CompareTag("Player")`), debe mostrar el texto emergente de interacción o directamente activar el `panelInventario`.
    *   `OnTriggerExit(Collider other)`: Al alejarse, el panel de inventario se desactiva.
*   **Lógica de Actualización Visual (`ActualizarInventario()`):**
    *   Al abrir el panel, se leen los datos desde `GameManager.Instancia.datosJugador`.
    *   Se validan los valores booleanos: `cristal1Recogido`, `herramienta1Recogida`, `cristal2Recogido`, `herramienta2Recogida`, `cristal3Recogido`, `herramienta3Recogida`.
    *   Por cada valor evaluado, se actualiza el color de la UI correspondiente en el arreglo de imágenes (ej. cambiar de gris a color y mostrar su descripción).

### 2.2. Configuración en el Editor de Unity
1.  **Creación del Objeto en Escena:** Ubicar un modelo 3D de un cartel (Billboard) en la escena de la Playa.
2.  **Configuración de Colisiones:** Agregar un `BoxCollider` marcado como **Is Trigger** abarcando el área frente al cartel.
3.  **UI de Inventario:**
    *   Dentro del Canvas de la Escena 1, crear el `Panel Inventario` (GameObject con imagen de fondo).
    *   Añadir el componente **Scroll Rect** y configurar el área de visualización (Viewport) y el contenedor de ítems (Content).
    *   Añadir un componente **Grid Layout Group** o **Vertical Layout Group** al contenedor.
    *   Crear los 6 "Slots" de UI (conteniendo Icono, Nombre y Descripción).
4.  **Asignación de Referencias:** Vincular el script `CartelInventario.cs` al cartel y arrastrar el panel y los slots de imágenes al inspector.

---

## 3. Secuencia Final (Nave Espacial y Victoria)

**Descripción del Requisito:**
Cuando el jugador ha reunido los 6 ítems y se acerca a la Nave, debe aparecer un diálogo final. Al confirmar, el juego debe redirigir a la Escena 5 (Story Panel) para mostrar la secuencia final y, finalmente, activar el **Final Game Victory Panel** con opciones de Reiniciar y Salir.

### 3.1. Revisión y Refactorización de `ControlNave.cs`
El script actual ya cuenta con una excelente base. Sin embargo, para mayor robustez en base a las reglas de los "6 ítems":

*   **Validación Estricta:**
    Dentro de `ActualizarTextoSegunProgreso()`, aunque ya se evalúa `mision3Completada`, se recomienda un `if` de seguridad que garantice que todos los booleanos (cristales y herramientas de la clase `DatosJugador`) sean `true`.
*   **Transición Actual (Correcta):**
    Actualmente, `AlPresionarOK()` hace lo siguiente:
    ```csharp
    GameManager.Instancia.proximaHistoria = GameManager.ModoHistoria.Final;
    SceneManager.LoadScene(5);
    ```
    Esto cumple perfectamente con el requerimiento de enrutar al jugador a la Secuencia Final (Scene 5).

### 3.2. Integración con el Panel de Victoria (`SistemaHistoria.cs`)
El sistema final está alojado en `SistemaHistoria.cs` (Scene 5). Al finalizar la lectura de la historia final, el script desactiva el diálogo interactivo y muestra el `panelVictoriaFinal`.

*   **Verificación en la Escena 5:**
    1.  Asegurarse de que el prefab/escena de Scene 5 contenga el `panelVictoriaFinal` configurado.
    2.  Verificar que los botones **Replay** y **Exit** del Victory Panel llamen correctamente a las funciones asignadas (el código ya contiene los listeners `LoadScene(0)` para Replay y `Application.Quit` para Exit).

---

## 4. Resumen de Ejecución y Siguientes Pasos
1.  **Código:** Redactar `CartelInventario.cs`.
2.  **UI:** Construir el Panel del Inventario en Unity con Scroll View.
3.  **Auditoría Final:** Asegurarse de que `ControlNave.cs` y `SistemaHistoria.cs` interactúen correctamente con los botones de la interfaz de la Scene 5 mediante pruebas integrales.
4.  **Guardado:** Una vez implementado, se guardará el estado de progreso para confirmar que el inventario se mantenga incluso después de reiniciar el juego.
