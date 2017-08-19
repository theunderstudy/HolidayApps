using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerStatsUI : MonoBehaviour {

    public Image StrengthBar;
    public Image AgilityBar;
    public Image CritBar;

    public int MaxCunning;
    public int MaxCourage;
    public int MaxStrength;
    public int MaxAgility;
    private TrainingStatStorage statStorage;

	// Use this for initialization
	IEnumerator Start () {
        yield return new WaitForEndOfFrame();
        StrengthBar.fillAmount = (float)((float)PlayerCombat.Instance.Strength / MaxStrength);
        AgilityBar.fillAmount = (float)(PlayerCombat.Instance.Agility / MaxAgility);
        CritBar.fillAmount = (float)(PlayerCombat.Instance.Cunning / MaxCunning);
        yield return new WaitForSeconds(0.5f);

        CheckTraining();
    }
    private void CheckTraining()
    {
        statStorage = FindObjectOfType<TrainingStatStorage>();

        //Check to see if player has gained skill from training
        if (statStorage.StrTraining != 0)
        {
            StartCoroutine(IncrementBar(StrengthBar));
        } else if (statStorage.AgiTraining != 0)
        {
            StartCoroutine(IncrementBar(AgilityBar));
        } else if (statStorage.CritTraining != 0)
        {
            StartCoroutine(IncrementBar(CritBar));
        }
    }

    private IEnumerator IncrementBar(Image bar)
    {
        Vector3 startPosition = bar.transform.localPosition;
        Vector3 centerScreen = Vector3.zero;
        centerScreen.x = Screen.width / 2;
        centerScreen.y = Screen.width / 2;

        bar.transform.DOMove(centerScreen, 1.5f);
        bar.transform.DOScale(Vector3.one * 3, 1.5f);

        yield return new WaitForSeconds(1.8f);

        //Check to see if player has gained skill from training
        if (statStorage.StrTraining != 0)
        {
            PlayerCombat.Instance.Strength += statStorage.StrTraining;
            StrengthBar.DOFillAmount((float)PlayerCombat.Instance.Strength / MaxStrength, 0.5f);
            statStorage.StrTraining = 0;
        }
        else if (statStorage.AgiTraining != 0)
        {
            PlayerCombat.Instance.Agility += statStorage.AgiTraining;
            AgilityBar.DOFillAmount(PlayerCombat.Instance.Agility / MaxAgility, 0.5f);
            statStorage.AgiTraining = 0;
        }
        else if (statStorage.CritTraining != 0)
        {
            PlayerCombat.Instance.Cunning += statStorage.CritTraining;
            CritBar.DOFillAmount(PlayerCombat.Instance.Cunning / MaxCunning, 0.5f);
            statStorage.CritTraining = 0;
        }
        yield return new WaitForSeconds(2.8f);
        bar.transform.DOLocalMove(startPosition, 1.5f);
        bar.transform.DOScale(Vector3.one * 1, 1.5f);
    }
}
