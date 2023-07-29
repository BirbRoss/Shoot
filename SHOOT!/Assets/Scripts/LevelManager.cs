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

    private void Start()
    {
        shootScript.enabled = false;
        Timer.enabled = false;
        Invoke("StartTimer", transitionTime);

    }

    // Update is called once per frame
    //Checks if any targets are left
    //Should make it so when time.time exceeds a limit the player loses?
    //Or maybe have that as per enemy so faster enemies will trigger a game over
    void Update()
    {
        if (GoalCount <= 0)
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
}
