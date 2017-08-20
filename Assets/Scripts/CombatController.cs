using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    public static CombatController instance;

    public EnemyBaseClass CurrentEnemy;
    public EnemyBaseClass[] EnemyStack;
    public int FightIndex = 0;

    private void Start()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);
    }

    public void StartFight()
    {
        Debug.Log("Attack sequence started");
        float attackTime = PlayerCombat.Instance.BaseAttackSpeed - (PlayerCombat.Instance.Agility / 150);
        int StartingMorale = PlayerCombat.Instance.Courage - EnemyStack[FightIndex].MoraleModifier;
        PlayerCombat.Instance.SetStartingMorale(StartingMorale);
        EnemyStack[FightIndex].StartEnemyAttack();
        Debug.Log("Player attack cooldown is: " + attackTime);
        PlayerCombat.Instance.PlayerStartCombat(attackTime, CurrentEnemy);
    }
    public void ProgressFight()
    {
        Debug.Log("next fight");
        float attackTime = PlayerCombat.Instance.BaseAttackSpeed - (PlayerCombat.Instance.Agility / 1000);
        PlayerCombat.Instance.PlayerStartCombat(attackTime, CurrentEnemy);

    }

    public void DealDamageToEnemy(int damageToDeal)
    {
        EnemyStack[FightIndex].TakeDamage(damageToDeal);
    }

    public void DealDamageToPlayer(int DamageToDeal)
    {
        PlayerCombat.Instance.takeDamage(DamageToDeal);
    }

    private void ResetFight()
    {
        EnemyStack[FightIndex].ResetHP();
    }

    public void EndCombat(bool PlayerWin)
    {
        //move to the next enemy in the array
        EnemyStack[FightIndex].EndCombat();
        //probably just chill for now lol
        //FightIndex++;//increment

        PlayerCombat.Instance.PlayerEndCombat();
        if (FightIndex == EnemyStack.Length)//out of enemies
        {

        }
        else
        {
            //start next fight
        }
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
