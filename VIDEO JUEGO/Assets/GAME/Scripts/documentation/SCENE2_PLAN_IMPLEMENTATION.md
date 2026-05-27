# Scene 2 (Level 1: Jungla) Implementation Plan

**Created at:** 2026-05-25 18:40
**Updated at:** 2026-05-25 21:55

## Mission Statement

Este documento define el plan de implementación para finalizar la **Scene 2: Level 1 (Jungla)**. Tras una lectura exhaustiva del código existente (`GameManager`, `CronometroMision`, `ItemRecolectable`, `MovimientoExploradora`), se ha trazado la estrategia definitiva para unificar el comportamiento de UI (Cronómetro, HUD), el control de las animaciones del jugador (Muerte, Victoria), y el sistema de Game Over y Victoria del nivel.

## Flujo Lógico a Implementar

### 1. Refactorización en `GameManager.cs`
Actualmente, las funciones de ganar o perder están **forzando** la carga inmediata de escenas, cortando abruptamente el juego sin animaciones ni paneles.
- **`PerderVida()`:** Al llegar a 0 vidas, se eliminará el reseteo forzado de la escena (`SceneManager.LoadScene(0)`). En su lugar, el GameManager buscará el componente `CronometroMision` y disparará su lógica de fallo.
- **`VictoriaMision()`:** Se removerá el salto a la escena 1 o 5. Su única tarea será actualizar el booleano `mision1Completada = true` y guardar. Dejaremos que el nivel mismo despache la carga de la Escena 5 desde su panel.

### 2. Expansión de `CronometroMision.cs` (Nuevo Controlador de Flujo)
Dado que ya gestiona el tiempo de la misión, asimilará el control total de ganar y perder el nivel:
- **Game Over (`MisionFallida`):** 
  - Se ejecuta cuando el tiempo se agota o el GameManager reporta 0 vidas.
  - Llamará a `MovimientoExploradora.ActivarMuerte()`.
  - Hará visible un **Panel de Perdiste** con 2 botones:
    - **Reintentar:** Ejecuta `SceneManager.LoadScene(nivel_actual)`.
    - **Salir:** Ejecuta `SceneManager.LoadScene(1)` (Playa).
- **Victoria (`ActivarVictoria`):**
  - Detiene el cronómetro.
  - Llamará a `MovimientoExploradora.ActivarVictoria()`.
  - Hará visible el **Panel de Victoria** ("¡Ganaste!").
  - Botón **Siguiente**: Modificará `proximaHistoria` en GameManager a `ModoHistoria.Mision1` y ejecutará `SceneManager.LoadScene(5)`.

### 3. Ajustes en `ItemRecolectable.cs`
Cuando se recogen todos los elementos:
- Llamará a `GameManager` para que marque la misión completada localmente, **pero** en lugar de dejar que el GameManager cierre el nivel, buscará el `CronometroMision` en escena y llamará al nuevo método `ActivarVictoria()`.

### 4. Configuración Visual Constante (UI en Unity)
- Se debe asegurar que el Prefab del `ControladorHUD` (el cual muestra el ícono de las vidas, la batería y la cantidad de monedas) esté siempre instanciado y activo en el Canvas del nivel 1.
- El panel de tiempo de `CronometroMision` debe comenzar habilitado desde el `Awake/Start`.

## Referencias
- Código revisado: `GameManager.cs`, `ItemRecolectable.cs`, `MovimientoExploradora.cs`, `CronometroMision.cs`.
