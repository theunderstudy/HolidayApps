using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
[RequireComponent(typeof(DisplaySprite))]
public class PlayerCombat : MonoBehaviour
{
    public float Cunning=5;//crit % chance based on enemy knowledge :: (Cunning/2)*(Knowledge/2)+10
    public int Courage=50;//total health at the start of a fight
    public int Strength = 15;//attack damage
    public int StrengthRange = 2;
    public float BaseAttackSpeed=1f;
    public float Agility=5;// attack speed modifier + dodge chance
    public int StartingMorale;//current HP based on courage
    public int CurrenMorale;
    
    public static PlayerCombat Instance;
    private DisplaySprite Sprites;

    public GameObject Dogo;

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
        DontDestroyOnLoad(this);
        Sprites = GetComponent<DisplaySprite>();

        if (PlayerPrefs.HasKey("str"))
        {
            Strength = PlayerPrefs.GetInt("str");
            Agility = PlayerPrefs.GetFloat("agi");
            Cunning = PlayerPrefs.GetFloat("cunning");
            Courage = PlayerPrefs.GetInt("Courage");
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
    public void SetStartingMorale(int _Morale)
    {
        //need to display starting moral compared to max morale without the modifier
        StartingMorale = _Morale;
        CurrenMorale = StartingMorale;

        // startingMorale / courage should give me the % that starting morale is of the max
        float difference =  1-(StartingMorale / Courage);
        Debug.Log(difference);
        Sprites.DecreaseBar(((float)CurrenMorale / (float)StartingMorale)-difference);
    }
    public void takeDamage(int _damage)
    {
        CurrenMorale -= _damage;
        float difference = 1- (StartingMorale / Courage);
        
        Debug.Log((CurrenMorale / StartingMorale) - difference);
        Sprites.DecreaseBar((float)(CurrenMorale / StartingMorale)-difference);
        if (CurrenMorale <=0 )
        {
            CombatController.instance.EndCombat(false);
        }
    }
    private void PlayerAttackSequence()
    {
        int damage = Random.Range(Strength - StrengthRange, Strength + StrengthRange);
       
        CombatController.instance.DealDamageToEnemy(damage);

        Vector3 currentPosition = Dogo.transform.localPosition;
        currentPosition.x += 2;

        StartCoroutine(MoveBack(Dogo.transform.localPosition));

        Dogo.transform.DOLocalMove(currentPosition, 0.15f);
    }
    IEnumerator MoveBack(Vector3 position)
    {
        yield return new WaitForSeconds(0.15f);
        Dogo.transform.localPosition = position;
    }

    private void criticalAttack()
    {

    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("str", Strength);
        PlayerPrefs.SetFloat("agi", Agility);
        PlayerPrefs.SetFloat("cunning", Cunning);
        PlayerPrefs.SetInt("courage", Courage);
    }
}
