using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCloud : MonoBehaviour
{

    // Use this for initialization
    public GameObject letterDropA, letterDropS, letterDropW, letterDropD;
    List<GameObject> letterDrops = new List<GameObject>();

    bool hasWon = false;
    bool hasLost = false;
    int letters = 0;
    int currentTime = 0;
    int position;
    public int missCounter = 0;
    float dropRate = 25.0f;
    public float y = 350f;
    float[] xValues;
    int min = 120;
    int max = 150;

    void Start()
    {
        xValues = new float[4] { 180, 330, 480, 610 };
    }
    /*
     * private void printY()
    {
        Debug.Log(letterDrops[0].transform.position.y);
    } 
    */
    public bool hit()
    {

        if ((letterDrops[0].transform.position.y > min) && (letterDrops[0].transform.position.y < max))
            return true;
        else
            return false;
    }
    private void discardLetter()
    {
        Destroy(letterDrops[0]);
        letterDrops.Remove(letterDrops[0]);
        letters--;
    }
    private void keyPress()
    {   
        if (Input.GetKeyDown("a"))
        {
            Debug.Log("A Pressed");
            hit();
            switch (hit())
            {
                case true:
                    {
                        Debug.Log("hit");
                        discardLetter();
                        break;
                    }
                case false:
                    {
                        Debug.Log("miss");
                        discardLetter();
                        missCounter++;
                        break;
                    }
            }
        }
        if (Input.GetKeyDown("s"))
        {
            Debug.Log("S Pressed");
            hit();
            switch (hit())
            {
                case true:
                    {
                        Debug.Log("hit");
                        discardLetter();
                        break;
                    }
                case false:
                    {
                        Debug.Log("miss");
                        discardLetter();
                        missCounter++;
                        break;
                    }
            }
        }
        if (Input.GetKeyDown("w"))
        {
            Debug.Log("W Pressed");
            hit();
            switch (hit())
            {
                case true:
                    {
                        Debug.Log("hit");
                        discardLetter();
                        break;
                    }
                case false:
                    {
                        Debug.Log("miss");
                        discardLetter();
                        missCounter++;
                        break;
                    }
            }
        }
        if (Input.GetKeyDown("d"))
        {
            Debug.Log("D Pressed");
            hit();
            switch (hit())
            {
                case true:
                    {
                        Debug.Log("hit");
                        discardLetter();
                        break;
                    }
                case false:
                    {
                        Debug.Log("miss");
                        discardLetter();
                        missCounter++;
                        break;
                    }
            }
        }
    }
    // Update is called once per frame

    void Update()
    {
        if (!hasWon)
        {
            if (Time.time > 30)
            {
                hasWon = true;
            }
            else
            {
                if (checkMissCounter())
                {
                    hasLost = true;
                    Debug.Log("You Lost");
                    gameObject.SetActive(false);
                }

                if (checkDropTime())
                {
                    dropLetter();
                }
                if (letters > 0)
                {
                    keyPress();
                    if (checkLimit())
                    {
                        discardLetter();
                        missCounter++;
                    }
                    transition();
                    //printY();
                }
            }
        }
        else
        {
            gameObject.SetActive(false);
        }

    }
    private void OnDisable()
    {
        hasWon = false;
    }
    void transition()
    {
        for (int x = 0; x < letterDrops.Count; x++)
            letterDrops[x].transform.Translate(0, -1.2f, 0);
    }
    void dropLetter()
    {
        Debug.Log(Time.time);
        Debug.Log(" Dropped Letter");
        letters++;
        int random;
        random = Random.Range(1,5);
        switch (random)
        {
            case 1:
                Debug.Log("A");
                GameObject temp = Instantiate( letterDropA, this.gameObject.transform);
                temp.transform.position = new Vector2(xValues[0], y);
                letterDrops.Add(temp);
                break;
            case 2:
                Debug.Log("S");
                GameObject temp2 = Instantiate(letterDropS, this.gameObject.transform);
                temp2.transform.position = new Vector2(xValues[1], y);
                letterDrops.Add(temp2);
                break;
            case 3:
                Debug.Log("W");
                GameObject temp3 = Instantiate(letterDropW, this.gameObject.transform);
                temp3.transform.position = new Vector2(xValues[2], y);
                letterDrops.Add(temp3);
                break;
            case 4:
                Debug.Log("D");
                GameObject temp4 = Instantiate(letterDropD, this.gameObject.transform);
                temp4.transform.position = new Vector2(xValues[3], y);
                letterDrops.Add(temp4);
                break;
        }
    }
    bool checkDropTime()
    {
        if (Time.time - currentTime > 5)
        {
            currentTime = (int)Time.time;
            return true;
        }
        return false;
    }
    bool checkMissCounter()
    {
        if (missCounter == 5)
            return true;
        else
            return false;
    }
    bool checkLimit()
    {
        if (letterDrops[0].transform.position.y < 120)
            return true;
        else
            return false;
    }
}
