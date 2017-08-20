using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CombatText : MonoBehaviour {

    public float timeToDisplay = 0.2f;
    public float speed = 0.5f;
    private float currentTime = 0f;

    private Text damageNumber;

    // Use this for initialization
    void Start () {
        transform.DOMoveY(transform.position.y + 1, timeToDisplay);
        damageNumber = GetComponent<Text>();
        damageNumber.color = Color.blue;
        if (CombatController.instance.lastDamage < 0)
        {
            damageNumber.color = Color.red;
            CombatController.instance.lastDamage *= -1;
        }
        damageNumber.text = CombatController.instance.lastDamage.ToString();

    }
	
	// Update is called once per frame
	void Update () {
        if (currentTime > timeToDisplay)
        {
            Destroy(gameObject);
        }
        else
        {
            currentTime += Time.deltaTime;
        }
    }
}
