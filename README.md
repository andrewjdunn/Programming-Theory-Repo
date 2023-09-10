# Programming-Theory-Repo
Unity Junior programmer - theory in action mission

## Ant hill simulator
Within the hill exists a number of types of ant each with their own behaviour, an abstract base class will represent the common functionality and each type will be a sub class demonstrating *Interitence*

## Behaviour and characteristics
The base class is caled AbstractAnt and has the virtual methods move and work that are overriden by the sub classes demonstrating *Polymorphism* and also some common behaviour Eat, Sleep demonstrating *abstraction* all of which are called on a schedule that is specific to the type of ant.

* Queen
* Digger
* Fighter
* Farmer

## Data
Data items like speed, activity, energy are private field demonstrating *encapsulation*

## Other objects
Some other objects exists that the ants can interact with such as
* Food
* Enemy
* Dirt

## Presentation
Objects are represented by simple primatives that differ in size and colour.