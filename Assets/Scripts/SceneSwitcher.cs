using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SceneSwitcher : MonoBehaviour, IPointerClickHandler{

    public int sceneIndexToLoad;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerClick(PointerEventData eventData) // 3
    {
        GameManager.Instance.SwitchScene(sceneIndexToLoad);
    }
}

