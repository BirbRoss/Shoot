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
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            shootScript.enabled = false;
            Timer.enabled = false;
        }

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
        else if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                LoadNext();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && gameOver)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
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

        if (levelIndex == 6)
        {
            Destroy(FindObjectOfType<musicHaunter>().gameObject);
            SceneManager.LoadScene(0);    
        }
        else
        {
            SceneManager.LoadScene(levelIndex);
        }
        
    }

    void StartTimer()
    {
        Timer.enabled = true;
    }

    public void killPlayer()
    {
        //play sound

        if (!gameOver)
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(gameObject.GetComponent<AudioSource>().clip);
        }

        shootScript.enabled = false;
        transition.SetTrigger("Start");
        gameOverTxt.SetTrigger("Start");
        gameOver = true;
    }
}
