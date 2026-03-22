## 2 Picar rocas ✅

**Funcionalidad**
El jugador debe golpear la roca con el pico para extraer recursos

**Programación**
- [x] crear el manejador de la roca agregar el script TriggerDetector.cs y subcribirse al evento OnTriggerEnter. -> `RockView.cs`
- [x] crea un RockHandler.cs revisa la skill @.agents/skills/ufeture -> `RockController.cs` (patrón ufeature)
- [x] Crea una clase que se llame StoneHP donde manejemos la vida de la roca que NO herede del monobehaviour, que se cree en el manejador -> `StoneHP.cs`
- [x] deben instanciar particulas del golpe de la roca -> `RockView.PlayHitParticles()`
- [ ] deben saltar objetos brillantes o Esmeraldas(despues haremos el script de emeraldas comenta esta acción mientras) -> `SpawnEmerald()` comentado
- [ ] se deben almacenar en el inventario cuando recolectas la esmeralda (más adelante programamos esto, comenta) -> `StoreInInventory()` comentado

**Archivos creados**
```
Assets/Code/Scripts/Features/Mining/
├── StoneHP.cs
├── RockModel.cs
├── RockController.cs
├── RockView.cs
└── MiningInstaller.cs
```