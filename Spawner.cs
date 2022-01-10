using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{



    public GameObject[] spawnObjects; // List of spawn objects

    
    private GameObject sp1 = null;
    private GameObject sp2 = null;
    private GameObject sp3 = null;
    private GameObject sp4 = null;


    public GameObject playerController;
    

    // Vectors to hold the object spawn positions
    public Vector3 spawnPosition;
    public Vector3 spawnPosition2;
    public Vector3 spawnPosition3;
    public Vector3 spawnPosition4;

    // Scale of objects being spawned
    public Vector3 objectScale;

    // Speed of object
    public float objectSpeed;


    // Difficulty multi
    public float difficultyRate;
    
    // Check if Objects are currently running
    public bool spawned = false;

    //int index; // index for spawn object
    //int index2; // index for second spawn object

    private TextMesh timerObject;

    // Spawn

    private bool flag = true;

    private float speed = 1;

    private float timer;

    private bool gameOver = false;

    public GameObject endGame;

    private AudioManager audioManager;

    private float timeStamp;

    private void Start() {
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

        if(timer/60 >= 3 && sp1 == null && sp2 == null && gameOver == false){
            Debug.Log("GAME OVER");
            gameOver = true;
            audioManager.PlaySound("Game Over");
            timeStamp = timer;
            Debug.Log(timeStamp);
        }

        if(timer >= timeStamp + 5 && gameOver == true){
            this.gameObject.SetActive(false);
            endGame.SetActive(true);
        }

        //Debug.Log(sp1);
        if(sp1 == null && gameOver == false){
            //Debug.Log("Spawn");
            Spawn();
        }

        // Debug.Log((int)timer%60);

        timerObject.text = "Timer: " + (int)timer/60 + ":" + (int)timer%60;
    }

    void Spawn()
    {
        // Spawns the signs
        sp1 = spawnObjects[Random.Range(0, 7)];
        sp2 = spawnObjects[Random.Range(0, 7)];
        sp3 = spawnObjects[Random.Range(0, 7)];
        sp4 = spawnObjects[Random.Range(0, 7)];

        // Logic to ensure objects aren't the same
        while (sp1 == sp2 || sp2 == sp3 || sp3 == sp4 || sp1 == sp4 || sp2 == sp4 || sp1 == sp3)
        {
            sp1 = spawnObjects[Random.Range(0, 7)];
            sp2 = spawnObjects[Random.Range(0, 7)];
            sp3 = spawnObjects[Random.Range(0, 7)];
            sp4 = spawnObjects[Random.Range(0, 7)];
        }

        // Instantiate meteors
        sp1 = Instantiate(sp1, spawnPosition, Quaternion.identity);
        sp2 = Instantiate(sp2, spawnPosition2, Quaternion.identity);
        sp3 = Instantiate(sp3, spawnPosition3, Quaternion.identity);
        sp4 = Instantiate(sp4, spawnPosition4, Quaternion.identity);

        // Scales the signs
        sp1.transform.localScale = objectScale;
        sp2.transform.localScale = objectScale;
        sp3.transform.localScale = objectScale;
        sp4.transform.localScale = objectScale;

        speed *= 1.01f;

        // Sets signs in motion 
        sp1.GetComponent<Rigidbody>().AddForce(transform.TransformDirection(Vector3.forward * -(objectSpeed + (difficultyRate))));
        //.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * -objectSpeed);
        sp2.GetComponent<Rigidbody>().AddForce(transform.TransformDirection(Vector3.forward * -(objectSpeed + (difficultyRate))));
        //.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * -objectSpeed);
        sp3.GetComponent<Rigidbody>().AddForce(transform.TransformDirection(Vector3.forward * -(objectSpeed + (difficultyRate))));
        sp4.GetComponent<Rigidbody>().AddForce(transform.TransformDirection(Vector3.forward * -(objectSpeed + (difficultyRate))));
        // flag
        spawned = true;
         // check player streak 
         difficultyRate = playerController.GetComponent<PlayerControllerVSA>().streak * 40;
    }

}
