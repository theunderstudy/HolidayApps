using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class MenuBackground : MonoBehaviour {
    public GameObject[] CamPos;
    public Vector3[] temp;
    private GameObject currentPos;
    public Color darkTheme;
    public Color lightTheme;
    public bool dark = false;
    public float MoveTimer=10;
    public float ColorTimer = 15;
    private float currentColorTime = 35;
    private float CurrentTime = 35;
    private SpriteRenderer Rend;
    // Update is called once per frame

    private void Start()
    {
        Camera.main.transform.position = CamPos[0].transform.position;
        Rend = GetComponent<SpriteRenderer>();
        temp = new Vector3[5];
        for (int i = 0; i < CamPos.Length; i++)
        {

            temp[i] = CamPos[i].transform.position;
        }
        temp[4] = temp[0];
        DOTween.Init(true, true, LogBehaviour.Verbose).SetCapacity(200, 10);
        Tweener tween = Camera.main.transform.DOPath(temp, 50, PathType.CatmullRom);
        tween.SetEase(Ease.Linear);
        
        tween.SetLoops(-1, LoopType.Incremental);
    }
    void Update ()
    {
        
        if (currentColorTime > ColorTimer)
        {
            currentColorTime = 0;
            if (!dark)
            {
                Rend.DOColor(darkTheme,ColorTimer).SetEase(Ease.Linear);

                dark = true;
            }
            else
            {
                Rend.DOColor(lightTheme,ColorTimer).SetEase(Ease.Linear);
            }
        }
        else
        {
            currentColorTime += Time.deltaTime;
        }
	}

}
