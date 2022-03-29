using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camaraKeyboard : MonoBehaviour
{
    public float sens = 100f;
    public float speed = 10f;
    float xR = 0;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mx = Input.GetAxis("Mouse X") * sens * Time.deltaTime;
        float my = Input.GetAxis("Mouse Y") * sens * Time.deltaTime;

        float x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float y = Input.GetAxis("Vertical") * speed * Time.deltaTime;


        /*xR -= my;
        xR = Mathf.Clamp(xR, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xR, transform.localRotation.y, 0f);
        transform.Rotate(Vector3.up * mx);*/

        transform.position += new Vector3(x, y, 0);

        //height
    }
}
