using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class DiscardLetter : MonoBehaviour, IPointerClickHandler{
    public bool destroy = false;
    public void OnPointerClick(PointerEventData eventData)
    {
        destroy = true;
    }

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
