using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AI;

public abstract class AbstractAnt : MonoBehaviour
{

    private static readonly float foodBenifit = 0.25f;
    private static readonly float maxFoodDistance = 2.5f;

    private NavMeshAgent navAgent;

    // Huger level increase from 0 to 1 for each activity over time, - at 1 we want to eat eating makes hunger go down by 0.25f.
    private float hungerLevel = 0;

    protected abstract void Move();
    protected abstract void Work();

    protected float HungerLevel { get { return hungerLevel; } }
    protected bool IsMoving { get; }

    protected Vector3 Target { set { navAgent.SetDestination(value); } }

    
    // ABSTRACTION - Called by subclasses to reduce hunger
    protected void Eat()
    {
        // Look for things tagged with Food.
        var allFood = GameObject.FindGameObjectsWithTag("Food");
        foreach (var food in allFood)
        {
            var distance = food.transform.position - gameObject.transform.position;
            if(distance.magnitude < maxFoodDistance)
            {
                Destroy(food);
                hungerLevel -= foodBenifit;
                break;
            }
        }
    }

    protected void IncreaseHunger(float hunger)
    {
        hungerLevel += hunger;
        if(hungerLevel > 1 )
        {
            hungerLevel = 1;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // Sub classes will decide if they want to move and if not already done so set the nav mesh props
        Move();

        // SUb classes decide if they need and can work and do the work
        Work();
    }
}
