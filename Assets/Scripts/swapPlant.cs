using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swapPlant : MonoBehaviour
{
    // Start is called before the first frame update
    public string pType;
    public GameObject my_smallPlant, my_growingPlant, my_grownPlant, my_deadPlant;
    private GameObject currentPlant;

    void Start()
    {
        pType = "";
    }

    public void spawnPlants(string plantType) {
        //spawn four
        my_smallPlant = Instantiate(Resources.Load<GameObject>("plant_" + plantType + "_S1"), transform.parent, false);
        my_growingPlant = Instantiate(Resources.Load<GameObject>("plant_" + plantType + "_S2"), transform.parent, false);
        my_grownPlant = Instantiate(Resources.Load<GameObject>("plant_" + plantType + "_S3"), transform.parent, false);
        my_deadPlant = Instantiate(Resources.Load<GameObject>("plant_" + plantType + "_S4"), transform.parent, false);

        disableAllPlants();
        currentPlant = my_smallPlant;
    }

    // Update is called once per frame
    void Update()
    {

        if (gameObject.GetComponent<landManager>().isDead)
        {
            //disable all 
            currentPlant.SetActive(false);
        }
        else {
            int currentStage = gameObject.GetComponent<landManager>().plant_stage;
            switch (currentStage)//small
            {
                case 0:
                    currentPlant.SetActive(false);
                    currentPlant = my_smallPlant;
                    currentPlant.SetActive(true);
                    //give small plant
                    break;
                case 1:
                    currentPlant.SetActive(false);
                    currentPlant = my_growingPlant;
                    currentPlant.SetActive(true);
                    //give small plant
                    break;
                case 2:
                    currentPlant.SetActive(false);
                    currentPlant = my_grownPlant;
                    currentPlant.SetActive(true);
                    //give small plant
                    break;

            }
        }

        if(currentPlant != null)
            gameObject.GetComponent<landManager>().plantObj = currentPlant;
        
    }

    private void disableAllPlants() {

        my_smallPlant.SetActive(false);
        my_growingPlant.SetActive(false);
        my_grownPlant.SetActive(false);
        my_deadPlant.SetActive(false);

    }
}
