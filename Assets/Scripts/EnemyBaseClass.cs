using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(DisplaySprite))]
public abstract class EnemyBaseClass : MonoBehaviour
{
    public int AttackDamage;
    public int DamageRange = 2;
    public float AttackCoolDown;
    public int MoraleModifier;
    public int MaxHealth;
    public int currentHealth;

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
    }
   public void ResetHP()
    {
        currentHealth = MaxHealth;
        Sprites.DecreaseBar(1);
    }
}
