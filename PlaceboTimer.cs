using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceboTimer : MonoBehaviour
{

    private float timer;
    private TextMesh timerObject;
    public GameObject endGame;
    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;

        // Get Text object for scores
        timerObject = GameObject.Find("TimerText").GetComponent<TextMesh>();
        // Update score
        timerObject.text = "Timer: 0";
        audioManager = AudioManager.instance;
        if(audioManager == null){
            Debug.Log("No Audio Manager");
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        // Debug.Log("Seconds" + timer);

        if(timer/60 >= 1){
            Debug.Log("GAME OVER");
            this.gameObject.SetActive(false);
            endGame.SetActive(true);
            //gameOver = true;
            audioManager.PlaySound("Game Over");
        }

        // Debug.Log((int)timer%60);

        timerObject.text = "Timer: " + (int)timer/60 + ":" + (int)timer%60;
    }
}
