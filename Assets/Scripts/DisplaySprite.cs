using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DisplaySprite : MonoBehaviour {
    public SpriteRenderer MyDisplayImage;
    public Image MyHealthBar;
    public Image MyHealthBarShadow;
    void Start () {
       
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void DecreaseBar(float newFill)
    {

        StopAllCoroutines();
        DOTween.Kill(gameObject);

        StartCoroutine(DecreaseHealthBar(newFill));
    }

    private IEnumerator DecreaseHealthBar(float _newFill)
    {

        MyHealthBar.DOFillAmount(_newFill , 0.15f);
        yield return new WaitForSeconds(0.1f);
        MyHealthBarShadow.DOFillAmount(_newFill, 0.15f);

    }
}
