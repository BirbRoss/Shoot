using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class banditShoot : MonoBehaviour
{

    public bool timerOn;
    public float TimeLeft = 3.0f;
    public LevelManager gameManager;

    [SerializeField]
    private float animationtimer;
    [SerializeField]
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        //Divides the time it takes for the animation to play by the time it'll take them to kill the player
        TimeLeft += 3f;
        animationtimer = 1 / (TimeLeft * 2);
        timerOn = true;
        

        anim = gameObject.GetComponent<Animator>();
        anim.speed = animationtimer;
        anim.SetTrigger("shoot");
    }

    // Update is called once per frame
    void Update()
    {
        if (timerOn)
        {
            if (TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
            }
            else
            {
                timerOn = false;
                TimeLeft = 0;
                gameManager.killPlayer();
            }
        }
    }
}
