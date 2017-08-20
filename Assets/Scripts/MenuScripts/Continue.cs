using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Continue : MonoBehaviour, IPointerClickHandler
{
    public int sceneIndexToLoad;
    public void OnPointerClick(PointerEventData eventData) // 3
    {
        PlayerPrefs.DeleteAll();
        GameManager.Instance.SwitchScene(sceneIndexToLoad);
    }

}
