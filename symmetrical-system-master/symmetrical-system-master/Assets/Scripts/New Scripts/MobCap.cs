using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobCap : MonoBehaviour
{
    //Max number of mob type
    public float plantCap;
    public float animalCap;

    //List of all existing instances of mob type
    public GameObject[] plants;
    public GameObject[] animals;

    //Tag for mob type
    public string plantTag;
    public string animalTag;

    //Number of mob type
    public float plantNum;
    public float animalNum;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        plants = GameObject.FindGameObjectsWithTag(plantTag);
        plantNum = plants.Length;

        animals = GameObject.FindGameObjectsWithTag(animalTag);
        animalNum = animals.Length;
    }
}
