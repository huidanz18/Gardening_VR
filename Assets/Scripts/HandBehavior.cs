using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    private InputDevice device_l, device_r;
    private bool hasL, hasR;
    void Start()
    {
        hasL = false;
        hasR = false;
        GetController();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasL|| !hasR)
            GetController();

        device_l.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButton_l);
        if (primaryButton_l)
        {
            //Debug.Log("left hand pressing primary button!!!\n");
        }//primary as X and A

    }

    void OnTriggerStay(Collider other)
    {

        if (other.tag == "WateringCan")
        {
           // Debug.Log("collisionssssssssssssssss");
            device_l.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButton_l);
            //print(device_l);
            if (primaryButton_l)
            {
                other.transform.position = transform.position;
                //other.transform.rotation = transform.rotation;

                //disable rigidbody
                other.GetComponent<Rigidbody>().useGravity = false;
            }//primary as X and A
            else {
                other.GetComponent<Rigidbody>().useGravity = true;
            }

        }

        if (other.tag == "Bug")
        {
            Debug.Log("tttttttoooouch buuuuuuuuug");
            device_l.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButton_l);
            
            if (primaryButton_l)
            {
                //kill bug
                Destroy(other.gameObject);
                
            }//primary as X and A
            
        }

    }


    private void GetController()
    {

        List<InputDevice> devices_l = new List<InputDevice>();
        List<InputDevice> devices_r = new List<InputDevice>();
        InputDeviceCharacteristics charac_l = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDeviceCharacteristics charac_r = InputDeviceCharacteristics.Right & InputDeviceCharacteristics.Controller;

        InputDevices.GetDevicesWithCharacteristics(charac_l, devices_l);
        InputDevices.GetDevicesWithCharacteristics(charac_r, devices_r);

        foreach (var item in devices_l)
        {
            Debug.Log("left hand!!\n");
            Debug.Log(item.name + ":" + item.characteristics);
            hasL = true;
        }

        foreach (var item in devices_r)
        {
            Debug.Log("right hand!!\n");
            Debug.Log(item.name + ":" + item.characteristics);
            hasR = true;
        }

        if (devices_l.Count > 0)
        {
            device_l = devices_l[0];
        }
        if (devices_r.Count > 0)
        {
            device_r = devices_r[0];
        }
    }

}

