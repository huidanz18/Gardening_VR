using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using XRInteractionToolkit;

public class WateringCan : MonoBehaviour
{
    //public XRGrabInteractable can;
    public Transform rightHand;

    private Transform canOrientation;
    private Quaternion waterThreshold = Quaternion.Euler(90, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float rotation = Input.GetAxis("Horizontal");
        rotation *= 80 * Time.deltaTime;

        transform.Rotate(0, 0, rotation);
        GameObject water = transform.Find("water").gameObject;

        //check if rotation meets threshold
        if (transform.localEulerAngles.z >= 30 && transform.localEulerAngles.z <= 180) {
            if (!water.GetComponent<ParticleSystem>().isPlaying) {
                water.GetComponent<ParticleSystem>().Play();
            }
        }
        else {
            if (water.GetComponent<ParticleSystem>().isPlaying) {
                water.GetComponent<ParticleSystem>().Stop();
            }
        }

    }
}
