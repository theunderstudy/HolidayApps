using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
[RequireComponent(typeof(DisplaySprite))]
public abstract class EnemyBaseClass : MonoBehaviour
{
    public int AttackDamage;
    public int DamageRange = 2;
    public float AttackCoolDown;
    public int MoraleModifier;
    public int MaxHealth;
    public int currentHealth;
    public int knowledge = 1;

    public GameObject DamageText;

    protected DisplaySprite Sprites;

    public virtual void Start()
    {
        currentHealth = MaxHealth;
        Sprites = GetComponent<DisplaySprite>();
    }

    public virtual void StartEnemyAttack()
    {
        InvokeRepeating("AttackSequence",AttackCoolDown,AttackCoolDown);
    }
    private void AttackSequence()
    {
        CombatController.instance.DealDamageToPlayer(Random.Range( AttackDamage -DamageRange , AttackDamage +DamageRange ));
    }

    public void EndCombat()
    {
        CancelInvoke();
    }
    public virtual void TakeDamage(int _damage)
    {
        currentHealth -= _damage;

        Sprites.DecreaseBar((float)currentHealth / MaxHealth);
        if (currentHealth <=0 )
        {
            CombatController.instance.EndCombat(true);
        }

        //Give feedback to hit
        Vector3 temp = Vector3.zero;
        temp.x = 0.1f;
        transform.DOPunchPosition(temp, 0.5f, 10, 10);
    }
   public void ResetHP()
    {
        currentHealth = MaxHealth;
        Sprites.DecreaseBar(1);
    }
}
