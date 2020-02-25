using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriLookDirection : MonoBehaviour
{
    public Camera cam;
    public GameObject go;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(45, 45, 0);
        GetComponent<TextMesh>().text = "Energy: " + Mathf.RoundToInt(go.GetComponent<TriDestination>().energy);
    }
}
