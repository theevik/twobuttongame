using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class TimedInput : MonoBehaviour
{
   
     * public GameObject SelectionIndicator;
    public GameObject OptionButton1;
    public GameObject OptionButton2;
    public GameObject OptionButton3;

    public int selectionChangeDelay = 2;
    public int currentSelection = 0;
    public float timer;
    public GameObject Player;
   

    // Start is called before the first frame update
    void Start()
    {
        timer = selectionChangeDelay;
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer - Time.deltaTime;
        if (timer <= 0.0f)
        {
            timer = selectionChangeDelay;

            currentSelection = currentSelection + 1;
            if (currentSelection > 2)
            {
                currentSelection = 0;
            }
        }

        if (currentSelection == 0)
        {
            SelectionIndicator.transform.localPosition = new Vector3(-100, 40, 0);
        }
        if (currentSelection == 1)
        {
            SelectionIndicator.transform.localPosition = new Vector3(-100, 0, 0);
        }
        if (currentSelection == 2)
        {
            SelectionIndicator.transform.localPosition = new Vector3(-100, -40, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentSelection == 0)
            {
               


            }
            if (currentSelection == 1)
            {
                Debug.Log("you selected the second option");
            }
            if (currentSelection == 2)
            {
                Debug.Log("you selected the third option");
            }

            // Hide the panel
            //gameObject.SetActive(false);
        }
    }
}
*/