using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class landManager : MonoBehaviour
{
    // Start is called before the first frame update
    //status stuff
    public float watering_interval, BP_interval, tillgrow_interval;
    public int plant_stage;
    public bool isDead;
    private bool needWater, checkingWater;
    private bool needBP, checkingBP;

    //scale stuff
    public float timer;
    private Vector3 initScale;
    private float currentScale, targetScale;
    public float scaleStep;
    public int growingTime;
    public float maxScale;
    private bool growingAnim;

    //fruit stuff
    public GameObject FruitPrefab;
    private GameObject myFruit;
    private bool hasFruit;

    //bug gameobjects
    public GameObject BugPrefab;
    private int bugNumb;
    private List<GameObject> bugs;//use list to hold the references to bugs

    public GameObject icon;
    public GameObject plantObj;
    public GameObject mud;

    public int digCount;

    private IEnumerator coroutine;

    void Start()
    {
        //p = 0;
        //spawn a fruit
        myFruit = Instantiate(FruitPrefab, transform.position + new Vector3(0, 0.3f, 0), Quaternion.identity);
        //disable it for now
        myFruit.SetActive(false);

        plant_stage = -1;//start with no plants
        isDead = false;
        icon.SetActive(false);//disable icon

        //tasks related params
        watering_interval = 10;
        BP_interval = 10;
        tillgrow_interval = 1; 

        needWater = false;
        checkingWater = false;
        needWater = false;
        checkingWater = false;

        initScale = plantObj.transform.localScale;
        currentScale = 0.5f;

        scaleStep = 0.0003f;
        growingTime = 10;
        maxScale = 1.5f;

        timer = 0;
        //seed == 0
        //small plant == 1;
        //grown plant == 2;
        hasFruit = false;

        bugs = new List<GameObject>();//instantiate bug list
        bugNumb = 3;
        growingAnim = false;

        digCount = 0;
        StartCoroutine(CleanDig());
    }

    // Update is called once per frame
    void Update()
    {
        targetScale = (float)plant_stage + 0.5f;


        //if not checking water and is growing, go checking water
        //either check water or bp
        /*int flip = Random.Range(0,2);

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
                if (!checkingBP && isGrowing())
                {
                    StartCoroutine(CheckBP());
                    checkingBP = true;
                }
            }
        }*/

        //matching plant appearance
        if (isEmpty())
           plantObj.SetActive(false);
        else
        {
            plantObj.SetActive(true);
            if (!growingAnim && plantObj.transform.localScale.x < maxScale && !isGrown())
            {
                coroutine = StartGrowing();
                StartCoroutine(coroutine);
            }
               
            //plantObj.transform.localScale = initScale * (currentScale);//(targetScale);
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

        //check bug pick
        onBugPicked();

        //check dig count
        if (digCount > 3)
            cleanLand();
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
        //print("needs water");

        //spawn BP
        for (int i = 0; i < bugNumb; i++)
        {
            Vector3 bugPos = transform.position + new Vector3(Random.Range(-.2f, .2f), 0.2f, Random.Range(-.2f, .2f));
            bugs.Add(Instantiate(BugPrefab, bugPos, Quaternion.identity));
        }//add all bugs to bug list
        needBP = true;
    }

    private IEnumerator CleanDig() {
        while (true) {
            yield return new WaitForSeconds(5);
            digCount = 0;
            //clean dig time every 5 seconds;
        }
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

    private IEnumerator StartGrowing() {
        growingAnim = true;
        //wait for a while then grow
        yield return new WaitForSeconds(tillgrow_interval);

        //growing
        float scaleUp = 1;
        while (scaleUp < maxScale)
        {
            yield return new WaitForSeconds(0.0001f);
            scaleUp += scaleStep;
            plantObj.transform.localScale = Vector3.one * scaleUp;
            
        }
        //go check for water/bp
        int flip = Random.Range(0, 2);

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
                if (!checkingBP && isGrowing())
                {
                    StartCoroutine(CheckBP());
                    checkingBP = true;
                }
            }
        }

        growingAnim = false;
            
    }

    //when watered
    void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Water" && needWater)
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

    //when bugs are picked
    void onBugPicked() {
        if (needBP)
        {
            //check the null pointer in list
            foreach (GameObject bug in bugs) {
                if (bug == null)
                    bugs.Remove(bug);
            }

            //check if all the bugs are picked
            if (bugs.Count == 0) 
            {
                needBP = false;
                checkingBP = false;
                //change stage
                plant_stage++;
                //clear timer
                timer = 0;
            }
        }
    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Shovel")
        {
            //do dig count plus
            digCount++;
        }
    }

    private void cleanLand() {
        for (int i = 0; i < transform.parent.childCount; i++) {
            GameObject c = transform.parent.GetChild(i).gameObject;
            if (c.name.Substring(0, 6) == "plant_")
            {
                Destroy(c);
                //i = 0;
            }
        }
        //plantObj = transform.parent.Find("Small Plant").gameObject;
        plant_stage = -1;
        digCount = 0;
        needWater = false;
        checkingWater = false;
        needWater = false;
        checkingWater = false;
        growingAnim = false;

        //clean mud
        transform.parent.Find("soil_pile").gameObject.GetComponent<mudControl>().mudReady = false;
        transform.parent.Find("soil_pile").gameObject.GetComponent<mudControl>().digCount = 0;
        
    }
}
