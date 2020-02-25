using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class destination : MonoBehaviour
{
    //Energy & Reproduction Variables
    public float energy = 50f;
    public float hungerRate = 2f;

    public float reproThres = 150f;
    public GameObject[] capsus;
    public GameObject capsuBaby;
    public Transform tranfe;
    public float lifeSpan;
    public float mature;

    //Finding and eating Food Variables
    public GameObject[] partners;
    public NavMeshAgent agent;
    public Transform target;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Lower energy by a fixed rate constantly
        energy -= hungerRate * Time.deltaTime;
        energy = Mathf.Clamp(energy, 0f, 200f);
        lifeSpan += Time.deltaTime;

        //Decide what the AI should be doing
        if (energy <= 50f)
        {
            //If energy is under 50, find food and eat it
            Hunger();
            partners = null;
            capsus = null;
            if (energy <= 0f)
            {
                //If energy is 0, die
                Destroy(gameObject);
            }
        } 
        if (energy >= reproThres) {
            //If energy is above 100, find a partner and produce offspring
            Reproduction();
            partners = null;
            capsus = null;
        } 
        if (energy < reproThres & energy >= 50) {
            //If energy isn't below 50 and not above 150, idle
            partners = null;
            capsus = null;
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Food"))
        {
            Destroy(collider.gameObject);
            energy += 15;
        }
        if (collider.gameObject.CompareTag("Capsu"))
        {
            if (lifeSpan >= mature & energy >= reproThres) {
                GameObject inst = Instantiate(capsuBaby, tranfe.position, Quaternion.identity);
                inst.GetComponent<destination>().energy = 30;
                inst.GetComponent<destination>().target = null;
                inst.GetComponent<destination>().lifeSpan = 0;
                energy = 50;
                target = null;
                lifeSpan = 0;
            } 
        }
    }
    void Reproduction()
    {
        //Find mate
        if (capsus == null)
        {
            capsus = GameObject.FindGameObjectsWithTag("Capsu");
            Vector3 position = transform.position;
            foreach (GameObject capsu in capsus)
            {
                target = capsu.transform;
                agent.destination = target.position;
            }
        }
    }
    void Hunger()
    {
        if (partners == null)
        {
            partners = GameObject.FindGameObjectsWithTag("Food");
            Vector3 position = transform.position;
            foreach (GameObject food in partners)
            {
                target = food.transform;
                agent.destination = target.position;
            }
        }
    }
}