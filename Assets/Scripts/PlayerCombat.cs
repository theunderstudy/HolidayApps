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
    public int StartingMorale;//current HP based on courage
    public int CurrenMorale;
    public static PlayerCombat Instance;
    private DisplaySprite Sprites;
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
        Sprites = GetComponent<DisplaySprite>();
	}
    public void PlayerStartCombat(float _time)
    {
        InvokeRepeating("PlayerAttackSequence", _time, _time);
    }
    public void PlayerEndCombat()
    {
        CancelInvoke();
    }
    public void SetStartingMorale(int _Morale)
    {
        StartingMorale = _Morale;
        CurrenMorale = StartingMorale;
        Sprites.DecreaseBar((float)CurrenMorale / StartingMorale);
    }
    public void takeDamage(int _damage)
    {
        CurrenMorale -= _damage;
        Sprites.DecreaseBar((float)CurrenMorale / StartingMorale);
        if (CurrenMorale <=0 )
        {
            CombatController.instance.EndCombat(false);
        }
    }
    private void PlayerAttackSequence()
    {
        int damage = Random.Range(Strength - StrengthRange, Strength + StrengthRange);
       
        CombatController.instance.DealDamageToEnemy(damage);
    }

    void Update ()
    {
		
	}
}
