using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(DisplaySprite))]
public abstract class EnemyBaseClass : MonoBehaviour
{
    public float AttackDamage;
    public float AttackCoolDown;
    public float MoralModifier;
    public int Health;
    protected DisplaySprite Sprites;
    public virtual void Start()
    {
        Sprites = GetComponent<DisplaySprite>();
    }

    public virtual void AttackSequence()
    {
        
    }
    public virtual void TakeDamage(int _damage)
    {
        StartCoroutine(TakeDamageSequence(_damage));
    }
    private IEnumerator TakeDamageSequence(int _damage)
    {

        yield return new WaitForSeconds(0.2f);
    }
}
