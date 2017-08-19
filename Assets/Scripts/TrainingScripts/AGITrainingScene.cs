using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AGITrainingScene : MonoBehaviour
{
    public int trainingAmount = 3;
    // Use this for initialization
    void Start()
    {
        FindObjectOfType<TrainingStatStorage>().AgiTraining = trainingAmount;
    }
}
