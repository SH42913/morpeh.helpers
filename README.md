# Morpeh Helpers
Here you'll find some helper utils for [Morpeh ECS framework](https://github.com/scellecs/morpeh).\
**THIS IS NOT PART OF MORPEH ECS, JUST FEW HELPERS BY ME, USE IT AT YOUR OWN RISK**

## How to install
The minimal checked Unity Version is 2020.3.* LTS\
Also, make sure you've already installed Morpeh, either you'll get compiler errors.

Open Package Manager and "Add package from git url..." using next string:
* `https://github.com/SH42913/morpeh.helpers.git?path=/Assets/Morpeh.Helpers`

## Content
### Extensions for Entities, Stashes and Filters
* `filter.RemoveAllEntities()` to remove all entities in filter
* `filter.RemoveComponentForAll<T>()` to remove component of type T from all entities in filter
* `entity.Exists()` to make sure entity is not null and not disposed (marked as Obsolete, use `entity.IsNullOrDisposed()` instead)
* `entity.GetOrCreate<T>()` to get component of type T if it exists or create a new one (marked as Obsolete, use `AddOrGet()` below instead)
* `stash.AddOrGet(entity)` to get existed component or add new one to entity using Stash
* `entity.AddOrGet()` to get existed component or add new one to Entity

### Simple update systems
They're abstract classes based on UpdateSystem, FixedUpdateSystem, or LateUpdateSystem. 
They'll let you reduce boilerplate in your code for simple systems, where you query through one filter with one/two/three components. 
No need to manually define the filter and iterate through that, just write your logic like you're processing one entity.

They leave you only one method to implement: `Process(entity, ref c1(, ref c2)(, ref c3), deltaTime)`. 
`Process()` also will be called during `OnAwake()` of system, you can use protected field `inAwake` to branch your logic for this case.

Available classes:
* `SimpleUpdateSystem<T>` based on `UpdateSystem` and `SimpleSystem<T>` based on `ISystem`
* `SimpleFixedUpdateSystem<T>` based on `FixedUpdateSystem` and `SimpleFixedSystem<T>` based on `IFixedSystem`
* `SimpleLateUpdateSystem<T>` based on `LateUpdateSystem` and `SimpleLateSystem<T>` based on `ILateSystem`
* All variants can handle up to 3 generic parameters, eg `SimpleSystem<T1,T2,T3>`
* You also can inherit non-generic abstract SimpleFixed/Late/UpdateSystem to implement your variation of Simple System

Example:
```
    public sealed class HealingSystem : SimpleUpdateSystem<HealthComponent> {
        public float healRate = 1f;
    
        protected override void Process(IEntity entity, ref HealthComponent health, float deltaTime) {
            health.amount += healRate * deltaTime;
        }
    }
```

### One-Frame Components
aka Auto-clean systems

That util will let you register a type of component to clean up all existing components during LateUpdate(based on `ICleanupSystem` in Morpeh). So you'll be able to write `World.RegisterOneFrame<T>()` once and fire components without the need to clean up after you. \
**How to use that:** Just call extension-method `World.RegisterOneFrame<T>()` in `OnAwake()` of your system to system's World property.

<details>
    <summary>How to migrate from previous SO-based OneFrames</summary>

* Remove OneFrameCleanSystem from your Installer
* Remove ScriptableObject assets of previously created OneFrameRegistry and OneFrameCleanSystem
* Replace calls `oneFrameRegister.RegisterOneFrame<T>()` with `World.RegisterOneFrame<T>()`
</details>

### Templates for Rider
If you're using JetBrains Rider, I also can offer [Morpeh Templates](https://gist.github.com/SH42913/dd905943872c25468b1aeab40d266a97) for you.\

It contains:
* Morpeh System file template (based on ISystem or ScriptableObject)
* Morpeh Component file template
* Morpeh Provider file template
* Morpeh Provider live template with shortcut `provider`
* Live template with attributes to disable IL2CPP checks with shortcut `il2cpp_nochecks`

How to import templates to your project:
* Download [Gist](https://gist.github.com/SH42913/dd905943872c25468b1aeab40d266a97) as a file
* Open your project in Rider
* Open settings and select desired layer(personal/team-shared/etc) in `Manage Layers` window
* Use `Import from File...`, select downloaded Gist-file and press `OK` in appeared window
* Now you've imported templates, but they're not available in `Add new...` yet
* Open layer with imported templates and open Editor -> File Templates -> C# and/or Unity context
* Find templates with `Morpeh` in name and switch `Add to "New..." menu` toggle
* ???
* Profit!
