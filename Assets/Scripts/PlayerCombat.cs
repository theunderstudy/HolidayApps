using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(DisplaySprite))]
public class PlayerCombat : MonoBehaviour
{
    public float Cunning=5;//crit % chance based on enemy knowledge
    public int Courage=50;//total health at the start of a fight
    public int Strength = 15;//attack damage
    public int StrengthRange = 2;
    public float BaseAttackSpeed=1f;
    public float Agility=50;// attack speed modifier + dodge chance
    public float Moral;//current HP based on courage

    public static PlayerCombat Instance;

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
	}
    public void PlayerStartCombat(float _time)
    {
        InvokeRepeating("PlayerAttackSequence", _time, _time);
    }
    public void PlayerEndCombat()
    {
        CancelInvoke();
    }
    private void PlayerAttackSequence()
    {
        int damage = Random.Range(Strength - StrengthRange, Strength + StrengthRange);
        Debug.Log("Player did " + damage + (" damage"));
    }

    void Update ()
    {
		
	}
}
