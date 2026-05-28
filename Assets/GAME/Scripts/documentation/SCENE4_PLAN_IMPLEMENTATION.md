# Plan de Implementación: Scene 4 - Level 3 (Volcano)

## 1. Visión General
Este documento detalla el diseño y los pasos requeridos para completar la **Escena 4: Nivel 3 (Volcán)**, basándose en los parámetros del `README.md`. En este último nivel de recolección, el peligro principal es el entorno dinámico: objetos (enemigos/meteoritos) caerán desde el cielo aleatoriamente e impactarán en el suelo o en el jugador.

---

## 2. Requerimientos del Nivel 3 (Volcán)
Según el documento de diseño:
- **Enemigos (Meteoritos / Bolas de fuego)**:
  - Caen aleatoriamente desde el cielo.
  - Al hacer contacto con el suelo: el enemigo desaparece.
  - Al hacer contacto con el jugador: el enemigo desaparece y el jugador pierde una (1) vida.
- **Mecánicas Generales**:
  - Cronómetro activo visible (UI Timer Panel).
  - Recolección de 2 ítems: `cristal_3` (Cristal Volcánico) y `herramienta_3` (Tanque de Gasolina) antes de que el tiempo se acabe.
  - Victoria de nivel si se recogen ambos ítems $\rightarrow$ Panel de Victoria $\rightarrow$ Panel de Historia (ModoHistoria.Mision3).
  - Game Over si se acaba el tiempo o las vidas.

---

## 3. Plan de Desarrollo Paso a Paso

### Paso 1: Script del Enemigo (`EnemigoVolcan.cs`)
Este script se asignará al prefab de la bola de fuego o meteorito.
- **Movimiento**: No requiere manipulación de posición manual compleja si usamos un `Rigidbody` en Unity que utilice la gravedad (`Use Gravity = true`), pero podemos optar por hacer que caiga a velocidad constante mediante `transform.Translate(Vector3.down * velocidad * Time.deltaTime)`.
- **Colisiones (`OnTriggerEnter` o `OnCollisionEnter`)**:
  - Si colisiona con un objeto que tiene la etiqueta `"Player"` (o si detecta el componente `SistemaDanoJugador`), llama a `SistemaDanoJugador.RecibirDano()`, luego usa `Destroy(gameObject)`.
  - Si colisiona con el entorno (podemos usar una etiqueta `"Ground"` o `"Suelo"`), simplemente se destruye: `Destroy(gameObject)`.

### Paso 2: Script del Generador (`GeneradorVolcan.cs`)
Necesitamos un Spawner continuo para estos enemigos, distinto del `GeneradorMonedas` (que instancia todo en el Start).
- **Variables**: `GameObject enemigoPrefab`, `float intervaloGeneracion`, `float minX, maxX, minZ, maxZ, alturaY`.
- **Lógica**: Utilizar una Corrutina (`IEnumerator`) que contenga un ciclo `while(true)`. Dentro del ciclo:
  1. Seleccionar coordenadas aleatorias en X y Z dentro de los rangos especificados, manteniendo una Y alta (el cielo).
  2. Instanciar el `enemigoPrefab`.
  3. Esperar `intervaloGeneracion` segundos usando `yield return new WaitForSeconds(...)`.

### Paso 3: Configuración de la Escena 4 (Unity Editor)
- **Entorno**: Construir el terreno volcánico. Asignar la etiqueta `"Ground"` a los pisos para que los meteoritos se destruyan al tocarlos.
- **Jugador**: Colocar el prefab del `Exploradora`.
- **Generador**: Crear un GameObject vacío llamado "SpawnerVolcan" y asignarle `GeneradorVolcan.cs`. Ajustar los límites (X, Z) para que cubran el área jugable.
- **UI y Cronómetro**: 
  - Colocar el Canvas estándar de misión.
  - Asignar `CronometroMision.cs` a un GameManager local o Controlador y configurar `nivelActual = 3`.
- **Coleccionables**: Asegurarse de que el script `SpawnerItems.cs` esté activo en la escena para que emita el Cristal y Herramienta 3 según las posiciones dictadas en `ItemsData.json`.

### Paso 4: Transición y Final de Juego
- `ItemRecolectable.cs` ya tiene programada la verificación de victoria para la Escena 4 (`escenaActual == 4`). 
- Tras ganar el nivel 3 y pasar por el Panel de Victoria, el juego nos enviará al Story Panel (Escena 5) con `proximaHistoria = ModoHistoria.Mision3`, y tras completar las 5 imágenes, regresaremos a la Playa (Hub), listos para la interacción final con la nave espacial.

---

## 4. Pruebas y Validación (QA)
1. **Generación de Meteoritos**: Al dar Play, observar el cielo para asegurarse de que instancian repetidamente sobre el área del jugador y caen hacia el suelo.
2. **Impacto con el Suelo**: Comprobar que los meteoritos desaparecen al tocar el piso para no saturar la memoria RAM.
3. **Impacto con el Jugador**: Ponerse bajo un meteorito, confirmar la pérdida de vida (vibración y color rojo) y la destrucción inmediata de la bola de fuego.
4. **Condición de Victoria**: Recoger los 2 objetos (`cristal_3` y `herramienta_3`), verificar que salte el Panel de Victoria y envíe a la historia correcta.
