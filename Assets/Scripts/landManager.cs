using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class landManager : MonoBehaviour
{
    // Start is called before the first frame update
    //water interval
    public float watering_interval, BP_interval;
    private int plant_stage;
    private bool needWater, checkingWater;
    private bool needBP, checkingBP;

    private float timer;
    private Vector3 initScale;
    private float currentScale, targetScale;
    public float scaleStep;
    public float growingTime;

    public GameObject FruitPrefab;
    private GameObject myFruit;
    private bool hasFruit;

    public GameObject icon;
    public GameObject plantObj;

    //public int p;
    void Start()
    {
        //p = 0;
        //spawn a fruit
        myFruit = Instantiate(FruitPrefab, transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity);
        //disable it for now
        myFruit.SetActive(false);

        plant_stage = -1;//start with no plants
        icon.SetActive(false);//disable icon

        //tasks related params
        watering_interval = 1;
        BP_interval = 1;
        needWater = false;
        checkingWater = false;
        needWater = false;
        checkingWater = false;

        initScale = plantObj.transform.localScale;
        currentScale = 0.5f;

        scaleStep = 0.002f;
        growingTime = 2;

        timer = 0;
        //seed == 0
        //small plant == 1;
        //grown plant == 2;
        hasFruit = false;
    }

    // Update is called once per frame
    void Update()
    {
        targetScale = (float)plant_stage + 0.5f;

        timer += Time.deltaTime;
        if (timer < growingTime && isGrowing())
        {
            currentScale += scaleStep;
        }

        //if not checking water and is growing, go checking water
        //either check water or bp
        int flip = Random.Range(0, 1);

        if (!checkingWater && !checkingBP)
        {
            if (flip == 0)
            {
                //heads, check water
                if (!checkingWater && isGrowing())
                {
                    StartCoroutine(CheckWater());
                    checkingWater = true;
                }
            }
            else
            {
                //tails, check bp
                if (!checkingWater && isGrowing())
                {
                    StartCoroutine(CheckWater());
                    checkingWater = true;
                }
            }
        }

        //matching plant appearance
        if (isEmpty())
           plantObj.SetActive(false);
        else
        {
            plantObj.SetActive(true);
            plantObj.transform.localScale = initScale * (currentScale);//(targetScale);
        }

        //show fruit if grown
        if (isGrown())
        {
            if (!hasFruit)
            {
                myFruit.SetActive(true);
            }

        }

        //matching icons
        if (needWater)
        {
            icon.GetComponent<Renderer>().material = Resources.Load<Material>("water-icon");//blue for needWater
            icon.SetActive(true);
        }
        else if (needBP)
        {
            icon.GetComponent<Renderer>().material = Resources.Load<Material>("bug-icon");
            icon.SetActive(true);
        }
        else
        {
            if (isGrown())
            {
                icon.GetComponent<Renderer>().material = Resources.Load<Material>("harvest-icon");
                icon.SetActive(true);
            }
            else//still growing or empty
                icon.SetActive(false);
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

    private IEnumerator CheckBP()
    {
        yield return new WaitForSeconds(watering_interval);
        print("needs water");

        transform.Find("bug").gameObject.SetActive(true);
        needBP = true;
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
        if (collision.gameObject.tag == "Seed" && isEmpty())
        {
            print("plant");
            //destroy seeds
            Destroy(collision.gameObject);
            plant_stage = 0;
            //clear timer
            timer = 0;
            return;
        }

    }


    void OnParticleCollision(GameObject other)
    {
        //print("hitting");
        //p++;
        if (needWater)
        {
            //water at the right time
            print("watering right!!!!!");
            needWater = false;
            checkingWater = false;
            //change stage
            plant_stage++;
            //clear timer
            timer = 0;


        }
    }
}
