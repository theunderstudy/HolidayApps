using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
[RequireComponent(typeof(DisplaySprite))]
public class PlayerCombat : MonoBehaviour
{
    public float Cunning = 5;//crit % chance based on enemy knowledge :: (Cunning/2)*(Knowledge/2)+10
    public int Courage = 50;//total health at the start of a fight
    public int Strength = 15;//attack damage
    public int StrengthRange = 2;
    public float BaseAttackSpeed = 1f;
    public float Agility = 5;// attack speed modifier + dodge chance
    public int StartingMorale;//current HP based on courage
    public int CurrenMorale;
    public bool canCrit = true;

    public static PlayerCombat Instance;
    private DisplaySprite Sprites;

    public GameObject Dogo;
    private EnemyBaseClass currentEnemy;
    public GameObject Crit;

    void Start()
    {

        if (!Instance)
        {
            Instance = this;
        }
        //else
        //{
        //    Destroy(gameObject);
        //}
        //DontDestroyOnLoad(this);
        Sprites = GetComponent<DisplaySprite>();
        PlayerPrefs.SetInt("Courage", Courage);
        if (PlayerPrefs.HasKey("str"))
        {
          
            Strength = PlayerPrefs.GetInt("str");
            Agility = PlayerPrefs.GetFloat("agi");
            Cunning = PlayerPrefs.GetFloat("cunning");
            Courage = PlayerPrefs.GetInt("Courage");
        }
    }
    public void PlayerStartCombat(float _time, EnemyBaseClass _currentEnemy)
    {
        currentEnemy = _currentEnemy;
        InvokeRepeating("PlayerAttackSequence", _time, _time);
    }
    public void PlayerEndCombat()
    {
        CancelInvoke();
        GameManager.Instance.SwitchScene(2);
    }
    public void SetStartingMorale(int _Morale)
    {
        //need to display starting moral compared to max morale without the modifier
        StartingMorale = _Morale;
        CurrenMorale = StartingMorale;

        // startingMorale / courage should give me the % that starting morale is of the max
        float difference = 1 - ((float)StartingMorale / Courage);
        Debug.Log(difference);
        Sprites.DecreaseBar(((float)CurrenMorale / (float)StartingMorale) - difference);
    }
    public void takeDamage(int _damage)
    {
        CurrenMorale -= _damage;
        float difference = 1 - ((float)StartingMorale / Courage);


        Sprites.DecreaseBar(((float)CurrenMorale / StartingMorale) - difference);
        if (CurrenMorale <= 0)
        {
            CombatController.instance.EndCombat(false);
        }
    }
    private void PlayerAttackSequence()
    {
        int damage = Random.Range(Strength - StrengthRange, Strength + StrengthRange);
        attackCrit(); //Check for crit

        CombatController.instance.DealDamageToEnemy(damage, false);

        Vector3 vecone = Vector3.zero;
        vecone.x = 1;

        //StartCoroutine(MoveBack(Dogo.transform.localPosition));

        Dogo.transform.DOPunchPosition(vecone, 0.15f)   ;
    }
   // IEnumerator MoveBack(Vector3 position)
   //{
   //     yield return new WaitForSeconds(0.15f);
   //     Dogo.transform.localPosition = position;
   // }

    private void attackCrit()
    {
        float critChance = ((Cunning / 2f) * (currentEnemy.knowledge / 2f) + 10f)/100f;
        float randValue = Random.value;

        if (randValue < (critChance))
        {
            //Run Critical Strike script
            GameObject critObject = Instantiate(Crit);
            Vector3 enemyLocation = currentEnemy.transform.position;
            enemyLocation.z = 0;
            critObject.transform.position = enemyLocation;
        }
    }

    public int GetCritDamage()
    {
        float critDamage = (float)Strength + ((float)Strength * ((float)currentEnemy.knowledge / 3f));
        return Mathf.RoundToInt(critDamage);
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("str", Strength);
        PlayerPrefs.SetFloat("agi", Agility);
        PlayerPrefs.SetFloat("cunning", Cunning);
        PlayerPrefs.SetInt("Courage", Courage);
    }
}

