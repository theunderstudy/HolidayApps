using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour {
    public static CombatController instance;

    public EnemyBaseClass CurrentEnemy;

	public void StartFight()
    {
        float attackTime = PlayerCombat.Instance.BaseAttackSpeed - (PlayerCombat.Instance.Agility / 1000);

        Debug.Log("Attack sequence started");
        Debug.Log("Player attack cooldown is: " + attackTime);
        PlayerCombat.Instance.PlayerStartCombat(attackTime);
    }

  

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartFight();
        }
    }

}
