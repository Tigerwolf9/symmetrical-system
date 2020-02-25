using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Reproduction : MonoBehaviour
{
    //Energy
    float energy;

    //Reproduction
    public float reproThres;
    GameObject[] partners;
    public string partnerTag;
    public GameObject child;
    public Transform tranfe;

    //Cooldown
    float timer;
    public float waitTime;

    //Navigation
    public NavMeshAgent agent;

    //MobTracker
    public GameObject mobTracker;
    float animalNum;
    float animalCap;

    // Start is called before the first frame update
    void Start()
    {
        Debug.DrawLine(transform.position, agent.destination);

        animalCap = mobTracker.GetComponent<MobCap>().animalCap;
    }

    // Update is called once per frame
    void Update()
    {
        //Reproduction cooldown
        timer += Time.deltaTime;
        energy = GetComponent<Stats>().energy;

        //Get mob number
        animalNum = mobTracker.GetComponent<MobCap>().animalNum;

        //If energy is high enough, reproduce
        Reproduce();
    }

    private void OnTriggerEnter(Collider collider)
    {
        //If the collided object is a partner
        if (collider.gameObject.CompareTag(partnerTag))
        {
            if (timer >= waitTime & energy >= reproThres & animalNum < animalCap)
            {
                //Produce new baby with certain variable setings
                GameObject inst = Instantiate(child, tranfe.position, Quaternion.identity);
                inst.GetComponent<Stats>().energy = 50;
                inst.GetComponent<Stats>().target = null;
                inst.GetComponent<Reproduction>().timer = 0;
                energy = GetComponent<Hunger>().hungry;
                timer = 0;
            }
        }
    }

    void Reproduce()
    {
        //Find partner
        if (energy >= reproThres & timer >= waitTime & animalNum < animalCap)
        {
            //Detect all gameobjects with the specified tag
            partners = GameObject.FindGameObjectsWithTag(partnerTag);
            //Find the closest gameobject in the array
            foreach (GameObject partner in partners)
            {
                if (partner.transform != transform)
                {
                    float closest = Mathf.Infinity;
                    float distance = (partner.transform.position - transform.position).sqrMagnitude;
                    //If the distance is less than the current closest gameobject 
                    //AND the specified gameobject has a high enough energy level
                    if (distance < closest)
                    {
                        //Set the gameobject as the new closest
                        closest = distance;
                        //Set the closest gameobject as the target
                        GetComponent<Stats>().target = partner.transform;
                        agent.destination = GetComponent<Stats>().target.position;
                    }
                }
            }
        }
    }
}
