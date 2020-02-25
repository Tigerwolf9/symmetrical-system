using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGrowth : MonoBehaviour
{
    public float min;
    public float max;
    private float reproduce = 0;
    public float maxReproduce;

    public Object cyubi;
    public Transform cyutran;

    public Vector3 size = new Vector3(.05f, .05f, .05f);
    private Vector3 growthRate = new Vector3(.01f, .01f, .01f);
    private Vector3 maxSize = new Vector3(.25f, .25f, .25f);

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Plant", 1f, Random.Range(0f,5f)/2f*10f);
        cyutran.localScale = size;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (cyutran.localScale != maxSize)
        {
            cyutran.localScale += growthRate;
        } else {
            cyutran.localScale = maxSize;
        }
    }
    

    void Plant()
    {
        if (reproduce <= maxReproduce - 1)
        {
            Instantiate(cyubi, new Vector3(Random.Range(min, max), 0.1f, Random.Range(min, max)), Quaternion.identity);
            reproduce++;
        } else {
            return;
        }
    }
}