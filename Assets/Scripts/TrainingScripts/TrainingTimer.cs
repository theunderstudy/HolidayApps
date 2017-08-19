using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingTimer : MonoBehaviour {
    public float WaitTime;

	// Use this for initialization
	IEnumerator Start () {
        yield return new WaitForSeconds(WaitTime);
        GameManager.Instance.SwitchScene(1);
	}
}
