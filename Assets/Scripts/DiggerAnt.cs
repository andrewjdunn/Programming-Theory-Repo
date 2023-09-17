using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// Inheritance
public class DiggerAnt : AbstractAnt
{
    private static readonly float HungerIncreaseForBreakingRock = 0.15f;
    private static int TimeToDestroyRock = 7;
    private GameObject workRock;
    private bool workingOnrock;
    private GameObject soilPrefab;
    private GameObject soilParent;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        soilParent = GameObject.Find("Soils");
    }

    public void SetSoilPrefab(GameObject soilPrefab)
    {
        this.soilPrefab = soilPrefab;
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
            workingOnrock = true;
            StartCoroutine(DestroyTheWorkRock());
        }
    }

    IEnumerator DestroyTheWorkRock()
    {
        yield return new WaitForSeconds(TimeToDestroyRock);
        if(workRock != null)
        {
            var rockPosition = workRock.transform.position;
            Destroy(workRock);
            workingOnrock = false;
            var soilPosition = new Vector3(rockPosition.x, soilPrefab.transform.position.y, rockPosition.z);
            Instantiate(soilPrefab, soilPosition, soilPrefab.transform.rotation, soilParent.transform);
            IncreaseHunger(HungerIncreaseForBreakingRock);
        }
    }

}
