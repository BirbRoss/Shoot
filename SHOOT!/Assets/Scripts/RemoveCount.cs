using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RemoveCount : MonoBehaviour
{
    public TextMeshProUGUI CountTxt;
    public LevelManager GameManager;
    bool done = false;

    private void Awake()
    {
        done = false;
    }

    public void RemoveOne()
    {
        if (!done)
        {
            GameManager.GoalCount--;
            CountTxt.text = GameManager.GoalCount.ToString();
            Debug.Log(GameManager.GoalCount);

            done = true;
        }
    }
}
