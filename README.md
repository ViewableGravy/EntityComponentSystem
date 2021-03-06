# C# Composition Library
A Simple Library that allows the use of composition over inheritance in c#. The library focuses heavily on retrieving components from an entity using Generics but also keeping the code simple, clean and consistent

  If you wish to read further into the full functionality of this library, you can find it [here](https://github.com/ViewableGravy/CSharp-Composition-Library/wiki)

# Primary Concepts
### 1. [ComponentFactory](https://github.com/ViewableGravy/CSharp-Composition-Library/wiki/ComponentFactory) 
  A Factory for creating new Components. It can either take a list of IComponents or an Assembly. Atleast one of these should be created in order to add IComponents to an Entity
  
### 2. [Entity](https://github.com/ViewableGravy/CSharp-Composition-Library/wiki/Entity)
  The Entity class is the base class for entities within an application. It contains the logic for storing, retrieving and utilising IComponents. The constructor Injects a/the ComponentFactory and uses it to create new internal components. Using it's method, you can query the Entity for a particular component, to either Add, Remove or Retrieve the component
  
### 3. [Component](https://github.com/ViewableGravy/CSharp-Composition-Library/wiki/Entity)
  This is the primary class for components to extend within an application. It allows a class to be used within the ECS system as a Component and added to Entities. There is also an Interface that should be used when the component doesn't need a reference to the parent Entity
  
  
# Example:
Let's consider a scenario where we have a Player and a Burger. Both are Entities that have components that allow them to interact

![image](https://user-images.githubusercontent.com/42259073/109454708-ea5d2780-7aa8-11eb-9366-91dc44e97248.png)

Taking that diagram into account, creating the component factory is simple (it can also take individual components but we will just use the executing assembly for now)

```cs
  new ComponentFactory(Assembly.GetExecutingAssembly());
```

now having the factory, creating the Player entity would look as follows: (Creating the burger is similar)
```cs
  public class Player : Entity
    {
        public Player(ComponentFactory componentFactory) : base(componentFactory)
        {
            AddComponents(new ComponentParameterList<IComponent>()
            {
                new ComponentParameters<Name>("ViewableGravy"),
                new ComponentParameters<Hunger>(this),
                new ComponentParameters<Eats>(this)
            });
        }
    }
```

Creating the Eats Component would look as follows
```cs
public class Eats : Component
    {
        public Eats(Entity _owningEntity) : base(_owningEntity)
        {
        }

        public string Eat(Entity toConsume)
        {
            ...
        }
    }
```

Then your code for a player eating a burger can look as follows
```cs
  player.GetComponent<Eats>().Eat(burger)
```

This is a simple example that perhaps doesn't fully benefit from this library due to the initial overhead, but as your program expands, being able to add, Remove and interact with components can simplify your code so that you are not limited by the mechanisms of inheritance.

# ToDo
  Handle default parameters from constructors for IComponents in the Factory (It currently fails epicly if you try to leave the default parameter blank (which becomes null and breaks))
  
  Work on Event handlers
  
  Note: Having a component that allows the interaction between the entity and another component has seemed fruitful, look into this further
