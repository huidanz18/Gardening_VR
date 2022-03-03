using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class landStatus : MonoBehaviour
{
    // Start is called before the first frame update
    //water interval
    public float watering_interval;
    public int plant_stage;
    public bool needWater, checkingWater;

    private float timer;
    private Vector3 initScale;
    void Start()
    {
        plant_stage = -1;//start with no plants
        watering_interval = 30;
        needWater = false;
        checkingWater = false;

        initScale = transform.Find("Small Plant").localScale;
        //seed == 0
        //small plant == 1;
        //grown plant == 2;
        //InvokeRepeating("CheckWater", 2f, watering_interval);
    }

    // Update is called once per frame
    void Update()
    {
        //if not checking water and is growing, go checking water
        if (!checkingWater && isGrowing()) {
            StartCoroutine(CheckWater());
            checkingWater = true;
        }

        //matching plant appearance
        if (isEmpty())
            transform.Find("Small Plant").gameObject.SetActive(false);
        else
        {
            transform.Find("Small Plant").gameObject.SetActive(true);
            transform.Find("Small Plant").localScale = initScale *((float)plant_stage+ 0.5f);
        }

        //matching indicator colors
        if (needWater)
            transform.Find("status").gameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 1);//blue for needWater
        else { 
            if(isGrowing())
                transform.Find("status").gameObject.GetComponent<Renderer>().material.color = new Color(0, 1, 0);//green as growning
            else if(isGrown())
                transform.Find("status").gameObject.GetComponent<Renderer>().material.color = new Color(1.0f, 0.64f, 0.0f);//orange as harvest
            else if(isEmpty())
                transform.Find("status").gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1);//white as empty
        }


    }
    //check water invoked every watering_interval
    private IEnumerator CheckWater()
    {
        yield return new WaitForSeconds(watering_interval);
        print("needs water");
        //change indicator appearance
        needWater = true;
    }

    private bool isGrowing()
    {
        return plant_stage == 0 || plant_stage == 1;
    }

    private bool isEmpty()
    {
        return plant_stage == -1;
    }
    private bool isGrown()
    {
        return plant_stage == 2;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //print("plant");
        if (collision.gameObject.tag == "Seed" && isEmpty())
        {
            //seed!
            print("plant");
            //destroy seeds
            Destroy(collision.gameObject);
            plant_stage = 0;
            return;
        }

        //watering
        if (collision.gameObject.tag == "WateringCan" && isGrowing())
        {
            if (needWater)
            {
                //water at the right time
                print("watering right!!!!!");
                needWater = false;
                checkingWater = false;
                //change stage
                plant_stage++;

                if (isGrowing())
                    transform.Find("status").gameObject.GetComponent<Renderer>().material.color = new Color(0, 1, 0);//green as growning
                else//can harvest
                    transform.Find("status").gameObject.GetComponent<Renderer>().material.color = new Color(255, 165, 0);

            }
            else
            {
                print("watering wrong!!!!!");
                //transform.Find("status").gameObject.GetComponent<Renderer>().material.color = new Color(0, 1, 0);//green as growning
            }

            return;
        
        }
    }
}
