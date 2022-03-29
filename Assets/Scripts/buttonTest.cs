using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class buttonTest : MonoBehaviour
{
    private InputDevice leftController, rightController;
    // Start is called before the first frame update
    void Start()
    {
        //List<InputDevice> l_devices = new List<InputDevice>();
        List<InputDevice> r_devices = new List<InputDevice>();
        InputDeviceCharacteristics isRight = (InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller);
        //InputDeviceCharacteristics isLeft = (InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller);

        InputDevices.GetDevicesWithCharacteristics(isRight, r_devices);
        print(r_devices[0]);
        foreach (var item in r_devices)
        {
            print(item.name + item.characteristics);
            /*if (item.characteristics == isRight)
                rightController = item;
            else if (item.characteristics == isLeft)
                leftController = item;*/
        }

    }

    // Update is called once per frame
    void Update()
    {
      
        
    }
}
