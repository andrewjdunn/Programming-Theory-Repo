using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AI;

public abstract class AbstractAnt : MonoBehaviour
{
    private static readonly float foodBenefit = 0.45f;
    private static readonly float closeEnough = 1.6f;

    private NavMeshAgent navAgent;

    // Encapsulation
    [SerializeField]
    private float hungerLevel = 0;
    
    protected abstract void Move();
    protected abstract void Work();

    protected float HungerLevel { get { return hungerLevel; } }
    protected bool IsMoving { get; }

    protected Vector3 Target { set { navAgent.SetDestination(value); } }

    protected bool CloseToTarget 
    { 
        get 
        {
            var reached = !navAgent.pathPending && navAgent.destination != null && navAgent.remainingDistance < closeEnough;
            if(reached)
            {
                Debug.Log($"Distance to Target {navAgent.remainingDistance} Target {navAgent.destination} Position {transform.position}");
            }
            return reached;
        } 
    }

    
    // ABSTRACTION
    protected void Eat()
    {
        var allFood = GameObject.FindGameObjectsWithTag("Food");
        GameObject nearestFood = null;
        float nearestFoodDistance = float.MaxValue;
        foreach (var food in allFood)
        {
            var distance = (food.transform.position - gameObject.transform.position).magnitude;
            if(distance < nearestFoodDistance)
            {
                nearestFoodDistance = distance;
                nearestFood = food;
            }
        }

        if(nearestFood != null)
        {
            Destroy(nearestFood);
            hungerLevel -= foodBenefit;
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
    protected virtual void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // Sub classes will decide if they want to move and if not already done so set the nav mesh props
        Move();

        if (HungerLevel < 1)
        {
            Work();
        }
        else
        {
            Eat();
        }
    }
}
