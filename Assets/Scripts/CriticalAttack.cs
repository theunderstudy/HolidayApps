using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.EventSystems;

public class CriticalAttack : MonoBehaviour {

    
    public Sprite[] CritSprites;
    public Sprite[] ExplosionSprites;
    private int SpriteIndex=0;
    private int ExSpriteIndex = 0;
    private SpriteRenderer rend;

    private bool activated = false;

    public float timeBetweenSprites = 0.1f;
    private float currentTime = 0f;

	// Use this for initialization
	void Start () {
    }
    private void Update()
    {
        if (!activated)
        {
            if (currentTime > timeBetweenSprites)
            {
                currentTime = 0;
                if (!rend)
                    rend = GetComponent<SpriteRenderer>();

                SpriteIndex++;
                if (SpriteIndex == CritSprites.Length)
                {
                    StartCoroutine(DestroyObject(true));
                }
                rend.sprite = CritSprites[SpriteIndex];
            }
            else
            {
                currentTime += Time.deltaTime;
            }
        }
    }
    void OnMouseDown()
    {
        if (!activated)
        {
            activated = true;
            //Get crit damage amount
            int critDamage = PlayerCombat.Instance.GetCritDamage();

            //Do the damage
            CombatController.instance.DealDamageToEnemy(critDamage);

            StopAllCoroutines();
            StartCoroutine(DestroyObject(false));
        }
    }
    IEnumerator DestroyObject (bool fromStart)
    {
        if (fromStart)
        {
            yield return new WaitForSeconds(0.1f);
        }
        else
        {
            while (true)
            {
                if (!rend)                
                    rend = GetComponent<SpriteRenderer>();

                ExSpriteIndex++;
                if (ExSpriteIndex == ExplosionSprites.Length)
                {
                    break;
                }
                rend.sprite = ExplosionSprites[ExSpriteIndex];
                yield return new WaitForSeconds(0.1f);
            }
        }
        
        PlayerCombat.Instance.canCrit = true;
        Destroy(gameObject);
    }
}
