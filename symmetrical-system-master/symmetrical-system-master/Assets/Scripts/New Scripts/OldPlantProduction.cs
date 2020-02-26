using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldPlantProduction : MonoBehaviour
{
    //Reproduction limit
    public float maxReproduction;
    float reproduction;

    //Spawning new plant
    public GameObject plant;
    public Transform plantTransform;
    public float minLimit, maxLimit;

    //Spawn cooldown
    float timer;
    public float reproductionCooldownMin, reproductionCooldownMax;

    //Mob cap
    public GameObject mobTracker;
    float plantCap;
    float plantNum;

    void Start()
    {
        //Get max number of plants
        plantCap = mobTracker.GetComponent<MobCap>().plantCap;
    }

    void Update()
    {
        //Get current number of plants
        plantNum = mobTracker.GetComponent<MobCap>().plantNum;

        //Cooldown
        timer += Time.deltaTime;

        //If cooldown complete, reproduce and reset cooldown
        if (timer >= Random.Range(reproductionCooldownMin, reproductionCooldownMax) & plantNum < plantCap)
        {
            Plant();
            timer = 0;
        }
    }

    void Plant()
    {
        //If can still reproduce and current number is smaller than the cap
        if (reproduction <= maxReproduction & plantNum < plantCap)
        {
            //Instantiate new object nearby
            GameObject inst = Instantiate(plant, new Vector3(Random.Range(minLimit, maxLimit), 0.1f, Random.Range(minLimit, maxLimit)), Quaternion.identity);

            //Add one to the counter
            reproduction++;
        }
    }
}
