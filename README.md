# Morpeh Helpers
Here you'll find some helper utils for [Morpeh ECS framework](https://github.com/scellecs/morpeh).

## How to install
The minimal checked Unity Version is 2019.4.\
Also, make sure you've already installed Morpeh, either you'll get compiler errors.

Open Package Manager and "Add package from git url..." using next string:
* `https://github.com/SH42913/morpeh.helpers.git?path=/Assets/Morpeh.Helpers`

## Content
### Extensions for Entities and Filters
* `entity.Exists()` to make sure entity is not null and not disposed
* `entity.GetOrCreate<T>()` to get component of type T if it exists or create a new one
* `filter.IsEmpty()` to make sure there are no entities in filter
* `filter.RemoveAllEntities()` to remove all entities in filter
* `filter.RemoveComponentForAll<T>()` to remove component of type T from all entities in filter

### Simple update systems
It is abstract classes based on UpdateSystem, FixedUpdateSystem, or LateUpdateSystem. 
They'll let you reduce boilerplate in your code for simple systems, where you query through one or two components filters. 
No need to manually define the filter and iterate through that, just write your logic like you're processing one entity.

They leave you only one method to implement: `Process(entity, ref c1(, ref c2), deltaTime)`. 
`Process()` also will be called during `OnAwake()` of system, you can use field `inAwake` to branch your logic for this case.

Available classes:
* `SimpleUpdateSystem<T>`/`SimpleUpdateSystem<T1,T2>`
* `SimpleFixedUpdateSystem<T>`/`SimpleFixedUpdateSystem<T1,T2>`
* `SimpleLateUpdateSystem<T>`/`SimpleLateUpdateSystem<T1,T2>`

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

That util will let you register a type of component to clean up all existing components during LateUpdate(based on LateUpdateSystem). 
So you'll be able to write `oneFrameRegister.RegisterOneFrame<T>()` once and fire components without the need to clean up after you.

How to work with that:
* Create ScriptableObject `OneFrameRegister` using `Create/Helpers/OneFrameRegister`. That's container where you will register one-frame types.
* Create `OneFrameCleanSystem` using `ECS/Helpers/OneFrameCleanSystem` and assign `OneFrameRegister` to system's public field `register`
* Add `OneFrameCleanSystem` to your Morpeh `Installer`
* Add field `public OneFrameRegister oneFrameRegister;` to the system where you want to register one-frame type and assign `OneFrameRegister` to system's field
* Call `oneFrameRegister.RegisterOneFrame<T>()` in `OnAwake()` of your system
* ???
* Profit!

### Templates for Rider
This repo also contains Morpeh.Helpers.sln.DotSettings with useful File and Live Templates for Rider.\
You can copy-paste `/Default/PatternsAndTemplates/` section to your DotSettings-file.

It contains:
* Morpeh System file template
* Morpeh Component file template
* Morpeh Provider live template with shortcut `provider`
