using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatGauge : MonoBehaviour {
    public GameObject lowHeat, midHeat, highHeat;
    public GameObject Gauge;
    public GameObject cursor;
    List<GameObject> heat = new List<GameObject>();
    List<GameObject> Gauges = new List<GameObject>();
    List<GameObject> Cursors = new List<GameObject>();

    bool hasWon = false;
    public int y = 215;
    int cursorY = 165;
    int[] xValues;
    int[] gaugeStatus;
    int[]currentTime;
    int[] heatTimer;
    int[] cooldownTimer;
    int currentCursor = 0;
    int initialChange = 0;
    // Use this for initialization
    void Start()
    {
        xValues = new int[3] {250,425,600};
        gaugeStatus = new int[3] {0,0,0};
        currentTime = new int[3];
        currentTime[0] = (int)Time.time;
        currentTime[1] = (int)Time.time;
        currentTime[2] = (int)Time.time;
        heatTimer = new int[3] { 0, 0, 0 };
        cooldownTimer = new int[3] { 0, 0, 0 };
        MakeGauge();
        makeHeatMeters();
        ghostMeters();
        makeCursors();

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

            blinkCursor();
            keypress();
            for (int x = 0; x < 3; x++)
            {
                heatTimer[x] = (int)Time.time - currentTime[x];

            }
            for (int x = 0; x < 3; x++)
            {
                CurrentHeat(x);
                if (heatTimer[x] == 5)
                {
                    initialChange++;

                    if (initialChange > 1)
                    {
                        heatTimer[x] = 0;
                        currentTime[x] = (int)Time.time;
                        gaugeStatus[x]++;

                    }
                    else
                    {
                        //GameObject loHeatA = Instantiate(lowHeat, this.gameObject.transform);
                        //loHeatA.transform.position = new Vector2(xValues[0], y);
                        // heat.Add(loHeatA);
                        heatTimer[x] = 0;
                        currentTime[x] = (int)Time.time;
                        gaugeStatus[x]++;
                    }
                }
            }
        }
        else
        {
            gameObject.SetActive(false);
         }
    }
    private void MakeGauge()
    {
        GameObject Gauge1 = Instantiate(Gauge, this.gameObject.transform);
        Gauge1.transform.position = new Vector2(xValues[0], y);
        Gauges.Add(Gauge1);

        GameObject Gauge2 = Instantiate(Gauge, this.gameObject.transform);
        Gauge2.transform.position = new Vector2(xValues[1], y);
        Gauges.Add(Gauge2);

        GameObject Gauge3 = Instantiate(Gauge, this.gameObject.transform);
        Gauge3.transform.position = new Vector2(xValues[2], y);
        Gauges.Add(Gauge3);

    }
    private void makeHeatMeters()
    {
        GameObject loHeatA = Instantiate(lowHeat, this.gameObject.transform);
        loHeatA.transform.position = new Vector2(xValues[0], y-15);
        heat.Add(loHeatA);
        GameObject midHeatA = Instantiate(midHeat, this.gameObject.transform);
        midHeatA.transform.position = new Vector2(xValues[0], y-5);
        heat.Add(midHeatA);
        GameObject hiHeatA = Instantiate(highHeat, this.gameObject.transform);
        hiHeatA.transform.position = new Vector2(xValues[0], y);
        heat.Add(hiHeatA);
        GameObject loHeatB = Instantiate(lowHeat, this.gameObject.transform);
        loHeatB.transform.position = new Vector2(xValues[1], y-15);
        heat.Add(loHeatB);
        GameObject midHeatB = Instantiate(midHeat, this.gameObject.transform);
        midHeatB.transform.position = new Vector2(xValues[1], y-5);
        heat.Add(midHeatB);
        GameObject hiHeatB = Instantiate(highHeat, this.gameObject.transform);
        hiHeatB.transform.position = new Vector2(xValues[1], y);
        heat.Add(hiHeatB);
        GameObject loHeatC = Instantiate(lowHeat, this.gameObject.transform);
        loHeatC.transform.position = new Vector2(xValues[2], y-15);
        heat.Add(loHeatC);
        GameObject midHeatC = Instantiate(midHeat, this.gameObject.transform);
        midHeatC.transform.position = new Vector2(xValues[2], y-5);
        heat.Add(midHeatC);
        GameObject hiHeatC = Instantiate(highHeat, this.gameObject.transform);
        hiHeatC.transform.position = new Vector2(xValues[2], y);
        heat.Add(hiHeatC);
    }
    private void ghostMeters()
    {
        for (int x =0; x<heat.Count;x++)
        heat[x].gameObject.SetActive(false);
    }
    private void makeCursors()
    {
        GameObject CursorA = Instantiate(cursor, this.gameObject.transform);
        CursorA.transform.position = new Vector2(xValues[0], cursorY);
        Cursors.Add(CursorA);
        GameObject CursorB = Instantiate(cursor, this.gameObject.transform);
        CursorB.transform.position = new Vector2(xValues[1], cursorY);
        Cursors.Add(CursorB);
        GameObject CursorC = Instantiate(cursor, this.gameObject.transform);
        CursorC.transform.position = new Vector2(xValues[2], cursorY);
        Cursors.Add(CursorC);
    }
    private void blinkCursor()
    {
        if ((int)Time.time % 2 == 0)
        Cursors[currentCursor].gameObject.SetActive(false);
        else
        Cursors[currentCursor].gameObject.SetActive(true);
    }
    private void keypress()
    {
        if (Input.GetKeyDown("a"))
        {
            currentCursor--;
            Cursors[(currentCursor+1)].gameObject.SetActive(true);
            if (currentCursor < 0)
                currentCursor = 0;
        }
        if (Input.GetKeyDown("s"))
        {
            currentCursor++;
            Cursors[(currentCursor-1)].gameObject.SetActive(true);
            if (currentCursor > 2)
                currentCursor = 2;
        }
        if (Input.GetKeyDown("space"))
            cooldownGauge();

    }
    private void cooldownGauge()
    {
        gaugeStatus[currentCursor] = 0;
        switch (currentCursor)
        {
            case 0:
                {
                    for (int x = 0; x < 3; x++)
                        heat[x].gameObject.SetActive(false);
                    break;
                }
            case 1:
                {
                    for (int x = 3; x < 6; x++)
                        heat[x].gameObject.SetActive(false);
                    break;
                }
            case 2:
                {
                    for (int x = 6; x < 9; x++)
                        heat[x].gameObject.SetActive(false);
                    break;
                }
        }
    }
    private void CurrentHeat(int x)
    {
        switch (x)
        {
            case 0:
                {
                    switch (gaugeStatus[x])
                    {
                        case 1:
                            {
                                heat[0].gameObject.SetActive(true);
                                break;
                            }
                        case 2:
                            {
                                heat[0].gameObject.SetActive(false);
                                heat[1].gameObject.SetActive(true);
                                Debug.Log("Gauge Change");
                                break;
                            }
                        case 3:
                            {
                                heat[1].gameObject.SetActive(false);
                                heat[2].gameObject.SetActive(true);
                                Debug.Log("Gauge Change");
                                break;
                            }
                        case 4:
                            {
                                heat[2].gameObject.SetActive(false);
                                Gauges[0].gameObject.SetActive(false);
                                Cursors[0].gameObject.SetActive(false);
                                break;
                            }
                    }
                            break;
                    }
                
                  case 1:
                {
                    switch (gaugeStatus[x])
                    {
                        case 1:
                            {
                                heat[3].gameObject.SetActive(true);
                                break;
                            }
                        case 2:
                            {
                                heat[3].gameObject.SetActive(false);
                                heat[4].gameObject.SetActive(true);
                                Debug.Log("Gauge Change");
                                break;
                            }
                        case 3:
                            {
                                heat[4].gameObject.SetActive(false);
                                heat[5].gameObject.SetActive(true);
                                Debug.Log("Gauge Change");
                                break;
                            }
                        case 4:
                            {
                                heat[5].gameObject.SetActive(false);
                                Gauges[1].gameObject.SetActive(false);
                                Cursors[1].gameObject.SetActive(false);
                                break;
                            }
                    }
                            break;
                    }
                case 2:
                {
                    switch (gaugeStatus[x])
                    {
                        case 1:
                            {
                                heat[6].gameObject.SetActive(true);
                                break;
                            }
                        case 2:
                            {
                                heat[6].gameObject.SetActive(false);
                                heat[7].gameObject.SetActive(true);
                                Debug.Log("Gauge Change");
                                break;
                            }
                        case 3:
                            {
                                heat[7].gameObject.SetActive(false);
                                heat[8].gameObject.SetActive(true);
                                Debug.Log("Gauge Change");
                                break;
                            }
                        case 4:
                            {
                                heat[8].gameObject.SetActive(false);
                                Gauges[2].gameObject.SetActive(false);
                                Cursors[2].gameObject.SetActive(false);
                                break;
                            }
                    }
                            break;
                    }
                    
        }
    }           
}
