using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int GoalCount = 4;
    public TextMeshProUGUI moveOnText;
    public Animator transition;
    public float transitionTime = 1f;

    public CountdownTimer Timer;
    public PlayerShoot shootScript;
    public bool gameOver = false;
    public Animator gameOverTxt;

    private void Start()
    {
        shootScript.enabled = false;
        Timer.enabled = false;
        Invoke("StartTimer", transitionTime);

    }

    // Update is called once per frame
    void Update()
    {
        if (GoalCount <= 0 && !gameOver)
        {
            if (!moveOnText.IsActive())
            {
                moveOnText.enabled = true;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                LoadNext();
            }

        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && gameOver)
            {
                Debug.Log("doing thing");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    public void LoadNext()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

    void StartTimer()
    {
        Timer.enabled = true;
    }

    public void killPlayer()
    {
        //play sound
        shootScript.enabled = false;
        transition.SetTrigger("Start");
        gameOverTxt.SetTrigger("Start");
        gameOver = true;
    }
}
