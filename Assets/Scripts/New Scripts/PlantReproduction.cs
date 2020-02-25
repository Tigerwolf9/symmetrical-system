using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantReproduction : MonoBehaviour
{
    //Reproduction limit
    public float maxReproduction;
    float reproduction;

    //Spawning new plant
    public GameObject plant;
    public Transform plantTransform;
    public float minVariation, maxVariation;

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
            GameObject inst = Instantiate(plant, plantTransform.position + new Vector3(Random.Range(minVariation, maxVariation), 0f, Random.Range(minVariation, maxVariation)), Quaternion.identity);
            
            //Add one to the counter
            reproduction++;
        }
    }
}
