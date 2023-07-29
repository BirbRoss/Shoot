using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RemoveCount : MonoBehaviour
{
    public TextMeshProUGUI CountTxt;
    public LevelManager GameManager;

    public void RemoveOne()
    {
        GameManager.GoalCount--;
        CountTxt.text = GameManager.GoalCount.ToString();
        Debug.Log(GameManager.GoalCount);

        enabled = false;
    }
}
