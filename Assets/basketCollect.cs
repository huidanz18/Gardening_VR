using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basketCollect : MonoBehaviour
{
    // Start is called before the first frame update
    public int fruitCollect;

    private void Start()
    {
        fruitCollect = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fruit")
            fruitCollect++;

    }
}
