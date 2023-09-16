using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DiggerAnt : AbstractAnt
{
    private static int TimeToDestroyRock = 7;
    private GameObject workRock;
    private bool workingOnrock;
    private GameObject foodPrefab;
    private GameObject foodParent;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        foodParent = GameObject.Find("Foods");
    }

    public void SetFoodPrefab(GameObject foodPrefab)
    {
        Debug.Log("Setting food prefab to " + foodPrefab);
        this.foodPrefab = foodPrefab;
    }

    protected override void Move()
    {
        if(workRock == null) 
        {
            GameObject closeRock = GameObject.FindGameObjectWithTag("Rock");
            var allRocks = GameObject.FindGameObjectsWithTag("Rock");
            
            foreach(var rock in allRocks)
            {
                var distanceToCloserock = (closeRock.transform.position - transform.position).magnitude;
                var distaanceToThisRock = (rock.transform.position - transform.position).magnitude;
                if(distaanceToThisRock < distanceToCloserock)
                {
                    closeRock = rock;
                }
            }
            if(closeRock != null)
            {
                Target = closeRock.transform.position;
                workRock = closeRock;
            }
        }
    }

    protected override void Work()
    {
        if(!workingOnrock && workRock != null && CloseToTarget)
        {
            Debug.Log("Reached the rock!");
            workingOnrock = true;
            StartCoroutine(DestroyTheWorkRock());
        }
    }

    IEnumerator DestroyTheWorkRock()
    {
        yield return new WaitForSeconds(TimeToDestroyRock);
        if(workRock != null)
        {
            Debug.Log("Destroying the Rock");
            var rockPosition = workRock.transform.position;
            Destroy(workRock);
            workingOnrock = false;
            var foodPosition = new Vector3(rockPosition.x, foodPrefab.transform.position.y, rockPosition.z);
            Instantiate(foodPrefab, foodPosition, foodPrefab.transform.rotation, foodParent.transform);
            
        }
    }

}
