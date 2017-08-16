using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;
    public int temp;
    public Collider mycollider;
	// Use this for initialization
	void Start ()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        mycollider = GetComponent<Collider>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
