using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hunger : MonoBehaviour
{
    //Energy-related Variables
    float energy;
    public float hungerRate;
    public float minEnergy, maxEnergy;
    public float hungry;

    //Finding food
    GameObject[] foods;
    public string foodTag;
    //variables for setting food as target
    public NavMeshAgent agent;
    bool eaten = true;
    float lastEaten;

    // Start is called before the first frame update
    private void Start()
    {
        //Get the energy from Stats
        energy = GetComponent<Stats>().energy;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position, agent.destination);

        //Energy depletion
        energy = GetComponent<Stats>().energy;
        energy -= hungerRate * Time.deltaTime;
        energy = Mathf.Clamp(energy, minEnergy, maxEnergy);
        GetComponent<Stats>().energy = energy;

        lastEaten += Time.deltaTime;

        //Die if energy is 0
        if (energy <= 0)
        {
            Destroy(gameObject);
        }

        //Finding Food
        if (energy <= hungry)
        {
            FindFood();
            
        }
        
    }
    //Detect collisions
    private void OnTriggerEnter(Collider collider)
    {
        //If the collided object is food
        if(collider.gameObject.CompareTag(foodTag))
        {
            //Destroy it and eat its energy
            Destroy(collider.gameObject);
            GetComponent<Stats>().energy += collider.GetComponent<Stats>().energy / 5;
            eaten = true;
            lastEaten = 0;
        }
    }
    
    void FindFood()
    {
        if (eaten == true || GetComponent<Stats>().target == null)
        {
            //Detect all gameobjects with the specified tag
            foods = GameObject.FindGameObjectsWithTag(foodTag);
            //Find the closest gameobject in the array
            foreach (GameObject food in foods)
            {
                float closest = Mathf.Infinity;
                float distance = (food.transform.position - transform.position).sqrMagnitude;
                //If the distance is less than the current closest gameobject 
                //AND the specified gameobject has a high enough energy level
                if (distance < closest)
                {
                    //Set the gameobject as the new closest
                    closest = distance;
                    //Set the closest gameobject as the target
                    GetComponent<Stats>().target = food.transform;
                    agent.destination = GetComponent<Stats>().target.position;
                    eaten = false;
                }
            }
        }
    }
}