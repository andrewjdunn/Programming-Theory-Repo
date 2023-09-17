using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FarmerAnt : AbstractAnt
{
    public GameObject FoodPrefab { set; private get; }
    private static readonly float HungerIncreaseForNewFood = 0.05f;

    private GameObject workSoil;
    private bool workingOnSoil;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Move()
    {
        if(workSoil == null)
        {
            // Find the closest empty soil
            GameObject closeSoil = GameObject.FindGameObjectWithTag("Soil");
            var allSoil = GameObject.FindGameObjectsWithTag("Soil");

            if(closeSoil != null && DoesSoilHaveFood(closeSoil))
            {
                closeSoil = null;
            }


            foreach (var soil in allSoil)
            {
                if (!DoesSoilHaveFood(soil))
                {

                    if (closeSoil == null)
                    {
                        closeSoil = soil;
                    }
                    else
                    {

                        var distanceToCloseSoil = (closeSoil.transform.position - transform.position).magnitude;
                        var distaanceToThisSoil = (soil.transform.position - transform.position).magnitude;
                        if (distaanceToThisSoil < distanceToCloseSoil)
                        {
                            closeSoil = soil;
                        }
                    }
                }
                if (closeSoil != null)
                {
                    Target = closeSoil.transform.position;
                    workSoil = closeSoil;
                }
            }
        }
    }

    protected override void Work()
    {
        if(!workingOnSoil && workSoil != null && CloseToTarget)
        {
            workingOnSoil = true;
            StartCoroutine(FarmSomeFood());
        }
    }

    private IEnumerator FarmSomeFood()
    {
        yield return new WaitForSeconds(15);
        // Add a helper function for checking for food
        if(!DoesSoilHaveFood(workSoil))
        {
            var pos = new Vector3(workSoil.transform.position.x, 0.25f, workSoil.transform.position.z);
            Instantiate(FoodPrefab, pos, FoodPrefab.transform.rotation, workSoil.transform);
            IncreaseHunger(HungerIncreaseForNewFood);
        }
        workingOnSoil = false;
        workSoil = null;
    }

    private static bool DoesSoilHaveFood(GameObject soil)
    {
        return soil.transform.Find("Food(Clone)");
    }
}
