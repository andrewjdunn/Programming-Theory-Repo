using UnityEngine;

public class QueenAnt : AbstractAnt // INHERITANCE
{
    private static readonly float HungerIncreaseForNewAnt = 0.2f;
    private static readonly int TooManyBabyAnts = 5;
    private GameObject antParent;
    private float nextAntDueTime = float.MaxValue;
    private int antsMadeHere = 0;

    [SerializeField] private GameObject diggerPrefab;
    [SerializeField] private GameObject farmerPrefab;
    [SerializeField] private GameObject foodPrefab;
    [SerializeField] private GameObject soilPrefab;

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
            if (nextAntDueTime < Time.time)
            {
                var prefab = GetRandomBabyAntPrefab();
                var x = Random.Range(0, 5) + 5;
                var y = Random.Range(0, 5) + 5;
                var xOffset = Random.value > 0.5 ? +x : -x;
                var yOffset = Random.value > 0.5 ? +y : -y;
                    
                var position = new Vector3(transform.position.x + xOffset, transform.position.y, transform.position.z + yOffset);
                var newAnt = Instantiate(prefab, position, prefab.transform.rotation, antParent.transform);
                // TODO: Scruffy - we can already know what the type is..
                var diggerAntScript = newAnt.GetComponent<DiggerAnt>();
                if(diggerAntScript != null)
                {
                    diggerAntScript.SetSoilPrefab(soilPrefab);
                }
                var farmerAntScript = newAnt.GetComponent<FarmerAnt>();
                if (farmerAntScript != null)
                {
                    farmerAntScript.FoodPrefab = foodPrefab;
                }
                    
                ++antsMadeHere;
                    
                IncreaseHunger(HungerIncreaseForNewAnt);
                    
                if (HungerLevel > .5)
                {
                    Eat();
                }
                nextAntDueTime = Time.time + 20;
            }
            
        }
    }

    private GameObject GetRandomBabyAntPrefab()
    {
        float diggerChance = OptionsManager.Instance.Options.DiggerRatio * Random.value;
        float farmerChance = OptionsManager.Instance.Options.FarmerRatio * Random.value;
        if(diggerChance > farmerChance)
        {
            return diggerPrefab;
        }        

        return farmerPrefab;
    }
}
