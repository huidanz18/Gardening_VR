using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mudControl : MonoBehaviour
{
    // Start is called before the first frame update
    public bool mudReady;
    public int digCount;
    public GameObject land;
    void Start()
    {
        mudReady = false;
        digCount = 0;
        land.GetComponent<swapPlant>().spawnPlants("cucumber");
    }

    // Update is called once per frame
    void Update()
    {
        digCount = Mathf.Min(3, digCount);
        transform.localScale = Vector3.one * 0.25f + (Vector3.one  *0.1f *(digCount + 1));

        //dig three times is enough
        if (digCount == 3)
            mudReady = true;
    }

    //hit by seed
    void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Seed" && mudReady && land.GetComponent<landManager>().plant_stage < 0)
        {
            GameObject bag = other.transform.parent.gameObject;
            string plantType = bag.name.Split("_")[1];
            land.GetComponent<swapPlant>().spawnPlants(plantType);
            land.GetComponent<landManager>().plant_stage = 0;
            //clear timer
            land.GetComponent<landManager>().timer = 0;
            return;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Shovel" && land.GetComponent<landManager>().plant_stage < 0)
        {
            //do dig count plus
            digCount++;
        }
    }
}

//trash code

/*private void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.tag == "Seed" && mudReady && land.GetComponent<landManager>().plant_stage < 0)
    {
        if (collision.gameObject.GetComponent<Rigidbody>().velocity.y < -0.2)
            return;//seed falling too fast

        Destroy(collision.gameObject);
        land.GetComponent<landManager>().plant_stage = 0;
        //clear timer
        land.GetComponent<landManager>().timer = 0;
        return;
    }

}*/
