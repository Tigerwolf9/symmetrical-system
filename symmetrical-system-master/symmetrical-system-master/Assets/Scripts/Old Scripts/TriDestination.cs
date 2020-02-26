using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class TriDestination : MonoBehaviour
{
    //Energy & Reproduction Variables
    public float energy = 50f;
    public float hungerRate = 2f;

    public float reproThres = 150f;
    public GameObject[] tris;
    public GameObject triBaby;
    public Transform tranfe;
    public float lifeSpan;
    public float mature;

    //Finding and eating Food Variables
    public GameObject[] capsus;
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
        energy = Mathf.Clamp(energy, 0f, 500f);
        lifeSpan += Time.deltaTime;

        //Decide what the AI should be doing
        if (energy <= 100f)
        {
            //If energy is under 50, find food and eat it
            Hunger();
            capsus = null;
            tris = null;
            if (energy <= 0f)
            {
                //If energy is 0, die
                Destroy(gameObject);
            }
        }
        if (energy >= reproThres)
        {
            //If energy is above 100, find a partner and produce offspring
            Reproduction();
            capsus = null;
            tris = null;
        }
        if (energy < reproThres & energy >= 100)
        {
            //If energy isn't below 50 and not above 150, idle
            capsus = null;
            tris = null;
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Capsu"))
        {
            Debug.LogWarning("HIT");
            Destroy(collider.gameObject);;
            energy += 25;
        }
        if (collider.gameObject.CompareTag("Tri"))
        {
            if (lifeSpan >= mature & energy >= reproThres)
            {
                GameObject inst = Instantiate(triBaby, tranfe.position, Quaternion.identity);
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
        if (tris == null)
        {
            tris = GameObject.FindGameObjectsWithTag("Tri");
            Vector3 position = transform.position;
            foreach (GameObject tri in tris)
            {
                target = tri.transform;
                agent.destination = target.position;
            }
        }
    }
    void Hunger()
    {
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
}
