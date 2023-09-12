using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class QueenAnt : AbstractAnt
{
    private static readonly int TooManyBabyAnts = 5;
    private GameObject antParent;
    private float nextAntDueTime = float.MaxValue;
    private int antsMadeHere = 0;

    public GameObject diggerPrefab;
    public GameObject fighterPrefab;
    public GameObject farmerPrefab;

    public QueenAnt()
    {
        
    }

    protected override void Start()
    {
        base.Start();
        antParent = GameObject.Find("Ants");
        nextAntDueTime = Time.time + 20; // Magic number..
    }

    protected override void Move()
    {
        if(antsMadeHere >= TooManyBabyAnts)
        {
            // Pick a random target and move to it
            var sites = GameObject.FindGameObjectsWithTag("QueenArea");
            var index = Random.Range(0, sites.Length);
            Target = sites[index].transform.position;

            antsMadeHere = 0;
        }
    }

    protected override void Work()
    {
        if (!IsMoving)
        {
            if (HungerLevel == 0)
            {
                if (nextAntDueTime < Time.time)
                {
                    var prefab = GetRandomBabyAntPrefab();
                    var x = Random.Range(0, 5) + 5;
                    var y = Random.Range(0, 5) + 5;
                    var xOffset = Random.value > 0.5 ? +x : -x;
                    var yOffset = Random.value > 0.5 ? +y : -y;
                    // Clip to floor bounds (99?)

                    var position = new Vector3(transform.position.x + xOffset, transform.position.y, transform.position.z + yOffset);
                    Instantiate(prefab, position, prefab.transform.rotation, antParent.transform);
                    ++antsMadeHere;
                    if (HungerLevel > .5)
                    {
                        Eat();
                    }
                    nextAntDueTime = Time.time + 20;
                }
            }
            else if (HungerLevel == 1)
            {
                Eat();
            }
        }
    }

    private GameObject GetRandomBabyAntPrefab()
    {
        float diggerChance = OptionsManager.Instance.Options.DiggerRatio * Random.value;
        float fighterChance = OptionsManager.Instance.Options.FighterRatio * Random.value;
        float farmerChance = OptionsManager.Instance.Options.FarmerRatio * Random.value;
        if(diggerChance > fighterChance && diggerChance > farmerChance)
        {
            return diggerPrefab;
        }

        if(fighterChance > farmerChance)
        {
            return fighterPrefab;
        }

        return farmerPrefab;
    }
}
