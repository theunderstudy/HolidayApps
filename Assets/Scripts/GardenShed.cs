﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GardenShed : MonoBehaviour,IPointerClickHandler {

	// Use this for initialization
	void Start () {
		
	}
	
    public void OnPointerClick(PointerEventData eventData) // 3
    {
        GameManager.Instance.SwitchGardens();
    }
}
