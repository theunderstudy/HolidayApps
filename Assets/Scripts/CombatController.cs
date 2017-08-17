using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    public static CombatController instance;

    public EnemyBaseClass CurrentEnemy;

    private void Start()
    {
        instance = this;
    }
    public void StartFight()
    {
        Debug.Log("Attack sequence started");
        float attackTime = PlayerCombat.Instance.BaseAttackSpeed - (PlayerCombat.Instance.Agility / 1000);
        int StartingMorale = PlayerCombat.Instance.Courage - CurrentEnemy.MoraleModifier;
        PlayerCombat.Instance.SetStartingMorale(StartingMorale);
        CurrentEnemy.StartEnemyAttack();
        Debug.Log("Player attack cooldown is: " + attackTime);
        PlayerCombat.Instance.PlayerStartCombat(attackTime);
    }
    public void DealDamageToEnemy(int damageToDeal)
    {
        CurrentEnemy.TakeDamage(damageToDeal);
    }
    public void DealDamageToPlayer(int DamageToDeal)
    {
        PlayerCombat.Instance.takeDamage(DamageToDeal);
    }
    private void ResetFight()
    {
        CurrentEnemy.ResetHP();
    }
    public void EndCombat(bool PlayerWin)
    {
        CurrentEnemy.EndCombat();
        PlayerCombat.Instance.PlayerEndCombat();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ResetFight();
            StartFight();
        }
    }

}
