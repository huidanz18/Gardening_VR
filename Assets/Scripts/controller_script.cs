using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class controller_script : MonoBehaviour
{
    // Start is called before the first frame update
    private InputDevice device_l, device_r;
    void Start() {
        GetController();
    }



    // Update is called once per frame
    void Update()
    {
        if (device_l == null || device_r == null)
            GetController();

        device_l.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButton_l);
        if (primaryButton_l){
            Debug.Log("left hand pressing primary button!!!\n");
        }//primary as X and A
        

    }


    private void GetController() {

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

