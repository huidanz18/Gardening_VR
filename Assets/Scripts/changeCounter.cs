using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class changeCounter : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject basket;
    public int dailyGoal;

    private void Update()
    {

        if (basket.GetComponent<basketCollect>().fruitCollect >= dailyGoal)
        {
            gameObject.GetComponent<TextMeshPro>().text = "Congrats!";
        }
        else
        {
            gameObject.GetComponent<TextMeshPro>().text = basket.GetComponent<basketCollect>().fruitCollect + "/" + dailyGoal;
        }
    }
}
