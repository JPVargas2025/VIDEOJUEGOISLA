# Plan de Implementación: Scene 3 - Level 2 (Cave)

## 1. Visión General
Este documento detalla los pasos necesarios para implementar y finalizar completamente la **Escena 3: Nivel 2 (Cueva)**, basándose en los requerimientos del `README.md` y la estructura actual del proyecto en Unity. El objetivo principal es establecer el comportamiento de los enemigos específicos de la cueva, configurar los coleccionables, integrar el cronómetro y asegurar el flujo de victoria/derrota.

---

## 2. Requerimientos del Nivel 2 (Cueva)
Según el documento de diseño (`README.md`):
- **Enemigos**: 
  - Se mueven a lo largo de rutas predefinidas a diferentes velocidades (rápidos y lentos).
  - Se comportan como "bombas" moviéndose en líneas rectas o rutas específicas.
  - **No reaccionan** a la proximidad del jugador (a diferencia del enemigo de la selva).
  - Al entrar en contacto con el jugador, este pierde una (1) vida y el enemigo desaparece.
- **Mecánicas Generales**:
  - Un temporizador (Cronómetro) visible en pantalla.
  - Recolección de la herramienta y el cristal correspondientes al Nivel 2 antes de que el tiempo se acabe.
  - Si el tiempo llega a cero o las vidas llegan a cero -> Game Over.
  - Si se recolectan ambos ítems a tiempo -> Victoria -> Pasa al Panel de Historia (ModoHistoria.Mision2).

---

## 3. Plan de Desarrollo Paso a Paso

### Paso 1: Creación del Script del Enemigo (`EnemigoCueva.cs`)
A diferencia de `EnemigoSelva.cs`, este enemigo no perseguirá al jugador. Solo patrullará ciegamente.

**Funcionalidades a programar:**
- `Transform[] puntosRuta`: Array para definir los puntos de patrulla en línea recta o circuito.
- `float velocidadMovimiento`: Variable pública para configurar diferentes velocidades desde el Inspector de Unity (lento/rápido).
- `int indicePuntoActual`: Para llevar el control del objetivo actual.
- Método `Update()`: Movimiento constante usando `Vector3.MoveTowards` hacia el `puntoRuta` actual. Al llegar, cambiar al siguiente punto.
- Método `OnTriggerEnter(Collider other)`: 
  - Detectar colisión con el jugador (etiqueta "Player" o el componente `SistemaDanoJugador`).
  - Invocar `SistemaDanoJugador.RecibirDano()` (o `GameManager.Instancia.PerderVida()`).
  - Destruir el GameObject del enemigo usando `Destroy(gameObject)`.

### Paso 2: Configuración de la Escena 3 (Unity Editor)
- **Terreno y Entorno**: Asegurar que la estructura de la cueva esté construida con sus respectivos colliders.
- **Jugador**: Colocar el prefab del jugador (`Exploradora`) en el punto de inicio de la cueva.
- **Enemigos**: 
  - Crear prefabs para el Enemigo de la Cueva.
  - Instanciar múltiples enemigos en la escena.
  - Asignarles el script `EnemigoCueva.cs`.
  - Crear GameObjects vacíos para usarlos como `puntosRuta` para cada enemigo.
  - Configurar algunos enemigos con velocidad alta y otros con velocidad baja en el Inspector.

### Paso 3: Configuración del Cronómetro y UI (`CronometroMision.cs`)
- Asegurarse de que el Canvas del nivel tenga el texto y los paneles UI (Victoria, Game Over, Notificación de recolección).
- Asignar el script `CronometroMision` a un objeto controlador (ej. `GameManagerNivel2`).
- Configurar en el Inspector:
  - `nivelActual = 2`.
  - Referencias a los textos (TextMeshProUGUI) y Paneles (Victoria/Derrota).
- Verificar que el archivo `NivelesData.json` (en `StreamingAssets`) tenga un bloque para `"numeroNivel": 2` con su respectivo `"tiempoSegundos"`.

### Paso 4: Configuración de Coleccionables (Cristal y Herramienta)
- Colocar los dos objetos coleccionables específicos del Nivel 2 en la escena.
- Asegurarse de que tengan el componente Collider en modo "Is Trigger".
- Asignarles el script `ItemRecolectable.cs`.
- Configurar sus IDs para que se correspondan con los elementos de la cueva (esto activará `cristal2Recogido` y `herramienta2Recogida` en el `GameManager`).

### Paso 5: Flujo de Victoria y Transición de Escenas
- Una vez que el jugador recolecta los dos ítems, el sistema de juego llamará a `CronometroMision.ActivarVictoria()`.
- Esto actualizará el `GameManager.Instancia.VictoriaMision(2)`.
- El jugador presionará "Siguiente" en el Panel de Victoria, lo cual invocará `CronometroMision.SiguienteNivel()`.
- Este método debe cambiar `GameManager.Instancia.proximaHistoria = GameManager.ModoHistoria.Mision2` y cargar la Escena 5 (Panel de Historia). Todo esto ya está preparado en el código actual, solo requiere que las referencias del Canvas estén bien conectadas en la Escena 3.

---

## 4. Pruebas y Validación (QA)
Al finalizar la implementación, se deberán realizar las siguientes pruebas en la Escena 3:
1. **Comportamiento del Enemigo**: Observar que los enemigos patrullen en sus rutas sin perseguir al jugador, algunos rápidos y otros lentos.
2. **Daño**: Chocar intencionalmente con un enemigo. Verificar que el jugador pierda 1 vida, el enemigo desaparezca, y se active el efecto de daño/vibración.
3. **Cronómetro**: Dejar que el tiempo llegue a cero y confirmar que aparece el panel de Game Over y se puede salir o reiniciar.
4. **Recolección**: Recoger la herramienta y el cristal de la cueva, verificar que aparezcan los mensajes emergentes de "¡Recolectado!".
5. **Victoria**: Al recoger ambos ítems, verificar que el temporizador se detenga, aparezca el Panel de Victoria y, al continuar, se cargue la Escena de Historia apuntando a la narrativa del Nivel 2.
