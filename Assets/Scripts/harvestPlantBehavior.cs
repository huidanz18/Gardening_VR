using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class harvestPlantBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public bool grabbed;
    void Start()
    {
        grabbed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (grabbed)
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        
    }
}
