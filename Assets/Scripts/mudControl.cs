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
        //transform.localScale = (Vector3.one / 4f) * 0.4f;
    }

    // Update is called once per frame
    void Update()
    {
        digCount = Mathf.Min(3, digCount);
        transform.localScale = (Vector3.one / 4f *(digCount + 1)) * 0.5f;

        //dig three times is enough
        if (digCount == 3)
            mudReady = true;
    }

    private void OnCollisionEnter(Collision collision)
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

    }
    //hit by seed
    void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Seed" && mudReady && land.GetComponent<landManager>().plant_stage < 0)
        {
            land.GetComponent<landManager>().plant_stage = 0;
            //clear timer
            land.GetComponent<landManager>().timer = 0;
            return;
        }
    }
}
