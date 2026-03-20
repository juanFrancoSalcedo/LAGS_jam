# 1️⃣ Features (Módulos Funcionales)

Una Feature es un módulo autocontenido que representa una funcionalidad del juego.
Ejemplo:

```
Features/Boss/
    BossController.cs
    BossView.cs
    BossModel.cs
    BossMovement.cs
    BossAttack.cs
    BossHealth.cs
    BossBindingInstaller.cs
```

## Regla clave

Una Feature debe poder entenderse y modificarse sin tocar otras partes del sistema.
**Comunicación**:
    Cada Feature debe vivir en su propia carpeta dentro de `Features/`:
    `Features/[Nombre]/` -> Contiene Model, Controller, View, sub-lógicas e Installer.

**Desacoplamiento de Unity**:
    - `Model`, `Controller` y lógicas específicas (como `BossAttack`) **NO** deben heredar de `MonoBehaviour`.
    - Solo la `View` interactúa con el motor de Unity.
**Comunicación**:
    - La `View` avisa al `Controller` de eventos de Unity.
    - El `Controller` consulta al `Model` y ejecuta acciones en las sub-lógicas.

---

##  Ejemplo: Boss Feature

### BossModel (Estado puro)

```csharp
public class BossModel
{
    public float Health;
    public float MoveSpeed;

    public BossModel(float health, float moveSpeed)
    {
        Health = health;
        MoveSpeed = moveSpeed;
    }
}
```

---

### BossMovement (Lógica específica)

```csharp
public class BossMovement
{
    private BossModel _model;
    private Transform _transform;

    public BossMovement(BossModel model, Transform transform)
    {
        _model = model;
        _transform = transform;
    }

    public void Move(Vector3 direction)
    {
        _transform.position += direction * _model.MoveSpeed * Time.deltaTime;
    }
}
```

### BossAttack

```csharp
public class BossAttack
{
    private IAudioService _audioService;
    private IPoolService<Bullet> _bulletPool;

    public BossAttack(
        IAudioService audioService,
        IPoolService<Bullet> bulletPool)
    {
        _audioService = audioService;
        _bulletPool = bulletPool;
    }

    public void Shoot()
    {
        var bullet = _bulletPool.Get();
        _audioService.Play("boss_shoot");
    }
}
```

---

### BossController (Orquestador)

```csharp
public class BossController
{
    private BossMovement _movement;
    private BossAttack _attack;
    private BossHealth _health;

    public BossController(
        BossMovement movement,
        BossAttack attack,
        BossHealth health)
    {
        _movement = movement;
        _attack = attack;
        _health = health;
    }

    public void Update()
    {
        _movement.Move(Vector3.forward);
    }

    public void Attack()
    {
        _attack.Shoot();
    }
}
```

---

### BossView (MonoBehaviour)

```csharp
public class BossView : MonoBehaviour
{
    private BossController _controller;

    public void Initialize(BossController controller)
    {
        _controller = controller;
    }

    private void Update()
    {
        _controller.Update();
    }
}
```


Componente,¿Qué hace?,¿Qué NO hace?
Model,Solo datos y estado (POCO).,No tiene lógica de Unity ni Update.
Controller,Orquesta la lógica entre clases.,No hereda de MonoBehaviour.
View,Recibe Input de Unity y muestra datos.,"No decide cuándo atacar, solo avisa al Controller."
Feature,Agrupa todo lo anterior.,No debe depender de otras Features directamente.