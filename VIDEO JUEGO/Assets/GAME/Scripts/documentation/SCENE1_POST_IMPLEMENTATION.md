# Scene 1 Hub Implementation Summary

**Created at:** 2026-05-25 14:50

## Mission Statement

This document explains the technical implementation of the pending features for Scene 1 (Beach/Hub) of the video game project, specifically the Inventory Panel interaction and the Final Victory checking logic.

## What Was Implemented

- Creation of the interactive Inventory Billboard logic.
- Refactoring of the Spaceship interaction logic to strictly verify the player's inventory before concluding the game.
- Resolution of an indexing bug in `ItemsData.json` and `ItemRecolectable.cs` where the scene assignments were incorrectly offset by +1.

### Deliverables
- [x] `CartelInventario.cs` created and integrated.
- [x] `ControlNave.cs` refactored to check for all 6 items.
- [x] Scene ID assignment fixed in `ItemRecolectable.cs` and `ItemsData.json`.

## Why It Was Done

### Business Reasons
- Fulfills the design specifications outlined in the project's README.
- Gives the player visual feedback regarding their progress across the different game levels.
- Ensures the game can only be beaten if all collectibles are found.

### Technical Reasons
- The `ControlNave.cs` script originally assumed that completing the 3rd mission automatically meant all items were collected. A strict validation (`TieneTodosLosItems()`) ensures data consistency.
- A critical bug was found during code review where the JSON configuration for items pointed to Scenes 3, 4, 5 instead of 2, 3, 4 (Level 1, Level 2, Level 3 respectively), causing the `VictoriaMision` trigger to fail.

## How It Was Done

### Architecture
The implementation revolves around querying the existing `GameManager.Instancia.datosJugador` Singleton, which serves as the single source of truth for the game state.

### Technology Choices
- **Unity C#**: Standard Monobehaviour triggers (`OnTriggerEnter`).
- **UI Elements**: Used standard `UnityEngine.UI.Image` color properties (`Color.gray` vs `Color.white`) to indicate item acquisition dynamically.

### Implementation Steps
1. Developed `CartelInventario.cs` to handle UI toggling and item state visualization based on the 6 boolean flags in the `GameManager`.
2. Replaced the `mision3Completada` check in `ControlNave.cs` with a comprehensive `TieneTodosLosItems()` evaluation.
3. Corrected `escenaAsignada` values in `ItemsData.json`.
4. Corrected `escenaActual` checks in `ItemRecolectable.cs` to match the real Unity build indices (2, 3, and 4).

## Why Not Alternative Approaches?

### Approach A: Local Inventory Manager
**Considered**: Yes
**Reason rejected**: Since the `GameManager` is already designed to persist data and serialize to JSON (`PartidaGuardada.json`), introducing a secondary inventory manager would cause synchronization issues. Centralizing data reading via `GameManager.Instancia` is the cleanest approach.

## Benefits and Advantages
- Tightly integrated with the existing save/load architecture.
- Reuses existing UI components and standard practices.

## Disadvantages
- Direct coupling to `GameManager` means that testing `CartelInventario.cs` requires the `GameManager` to be active in the scene.

## Risks and Mitigations
| Risk | Probability | Impact | Mitigation |
|------|-------------|--------|------------|
| NullReferenceException if testing Hub alone | High | Med | Added null-checks for `GameManager.Instancia` in `CartelInventario.cs` |

## Results

### Validation
- Code has been statically verified to ensure that the JSON data matches the scene indices evaluated in the collision scripts.
- The required checks for all 6 items now serve as a hard gate for the final cinematic.

## Lessons Learned

### What Went Well
- Building upon the existing `DatosJugador` class was extremely efficient.

### Key Takeaways
- **Scene Indexing is Error-Prone:** The mismatch between the human-readable "Level 1" (which is Scene 2) and the data configuration led to a silent bug in `ItemRecolectable.cs`. Always double-check build indices when mapping configurations.

## Recommendations

### For Next Phase
- Implement the actual gameplay loop and countdown timers for Scene 2 (Level 1 - Jungle), Scene 3 (Level 2 - Cave), and Scene 4 (Level 3 - Volcano).
- Ensure that the level timers properly trigger the `GameOver` condition if time runs out before collecting both items.

## References
- [SCENE1_IMPLEMENTATION](/documentation/SCENE1_IMPLEMENTATION.md)
- [README](/README.md)
