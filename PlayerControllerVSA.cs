using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Text;

public class PlayerControllerVSA : MonoBehaviour{

    // Transform for the drone rotation lock
    Transform t;

    public float streak = 0f;

    public float score = 0f;

    private TextMesh scoreObject;
    private TextMesh streakObject;

    public GameObject QuitButton;
    public GameObject PlayButton;
    public GameObject PlaceboButton;
    public GameObject Spawner;
    public GameObject PlaceboSpawner;
    // private StreamWriter file;

    public Material[] materialRef;

    private AudioManager audioManager;
    
    private List<string[]> data = new List<string[]>();


    // Start is called before the first frame update
    void Start(){
        t = transform;
        // Get Text object for scores
        scoreObject = GameObject.Find("ScoreText").GetComponent<TextMesh>();
        // Update score
        scoreObject.text = "Score: 0";

        // Get Text object for scores
        streakObject = GameObject.Find("StreakText").GetComponent<TextMesh>();
        // Update score
        streakObject.text = "Streak: 0";

        audioManager = AudioManager.instance;
        if(audioManager == null){
            Debug.Log("No Audio Manager");
        }

        // string fname = System.DateTime.Now.ToString("MM-dd-yyyy(h:mm:ss tt)") + ".csv";
        // string path = Path.Combine(Application.persistentDataPath, fname);
        // StreamWriter file = new StreamWriter(path);
        // Debug.Log(Application.persistentDataPath);

        // Debug.Log("Start");

        // gameObject.GetComponent<Renderer>().material = materialRef[Random.Range(0, 7)];
        // Debug.Log(gameObject.GetComponent<Renderer>().material.name);

        string[] temp = new string[2];

        temp[0] = "Score";
        temp[1] = "Highest Streak";
    
        data.Add(temp);
    }

    // Update is called once per frame
    void Update(){
        // Keep drone axis locked
        t.eulerAngles = new Vector3 (0, 180, 0);
        // scoreObject.text = "Score: " + score;
    }

    void OnCollisionEnter(Collision other) {
        Debug.Log((other.gameObject.tag + " (Instance)" ));
        
        if ((other.gameObject.tag + " (Instance)" ) == gameObject.GetComponent<Renderer>().material.name)
        {
            // Destroy the sign that was collided with
            Destroy(other.gameObject);
            // Prevent a streak being over 100, for scaling adaptive difficulty 
            if(streak <= 100f) streak += 1f;
            Debug.Log(streak);
            // Log
            Debug.Log("Correct sign");
            // Calculate score
            score = score + (10 * (streak/10));
            // Update score text
            scoreObject.text = "Score: " + score;
            streakObject.text = "Streak: " + streak;
            audioManager.PlaySound("Correct");
            AddData();
        }
        else if (other.gameObject.tag == "PlayButton")
        { 
            PlaceboButton.SetActive(false);
            PlayButton.SetActive(false);
            QuitButton.SetActive(false);
            Spawner.SetActive(true);
            Debug.Log("Play Start");
            audioManager.PlaySound("Game Start");
        }
        else if (other.gameObject.tag == "Placebo")
        { 
            PlaceboButton.SetActive(false);
            PlayButton.SetActive(false);
            QuitButton.SetActive(false);
            PlaceboSpawner.SetActive(true);
            Debug.Log("Placebo Start");
            audioManager.PlaySound("Start");
        }
        else if (other.gameObject.tag == "QuitButton")
        { 
            other.gameObject.SetActive(false);
            Application.Quit();
        }
        else if (other.gameObject.tag == "PlayAgain")
        {
            Debug.Log("Play Again");
            other.gameObject.SetActive(false);
            SceneManager.LoadScene( SceneManager.GetActiveScene().name);
        }
        else if (other.gameObject.tag == "QuitEnd")
        {
            Debug.Log("Final Quit");
            other.gameObject.SetActive(false);
            Application.Quit();
        }
        else if ((other.gameObject.tag + " (Instance)" ) != gameObject.GetComponent<Renderer>().material.name)
        {
            // Destroy the sign that was collided with
            Destroy(other.gameObject);
            // Reset streak
            streak = 0f;
            // Log
            Debug.Log("Wrong sign");
            audioManager.PlaySound("Wrong");
            AddData();
        }
        else{
            Debug.Log("Error");
        }

        gameObject.GetComponent<Renderer>().material = materialRef[Random.Range(0, 7)];
        Debug.Log(gameObject.GetComponent<Renderer>().material.name);
    }

    private void CreateCSV() {
        StringBuilder sb = new StringBuilder();
        string[][] output = new string[data.Count][];
        string seperator = ",";
        for(int i = 0; i <output.Length; i++){
            output[i] = data[i];
        }
        int l = output.GetLength(0);
        for(int index = 0; index < l; index++){
            sb.AppendLine(string.Join(seperator,output[index]));
        }
        string fname = System.DateTime.Now.ToString("MM-dd-yyyy(h:mm:ss tt)") + ".csv";
        string filePath = Application.persistentDataPath +"/"+fname;
        Debug.Log(filePath);
    
        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    
    }
 
    void AddData(){
 
       string[] temp = new string[2];
       temp[0] = score.ToString();
       temp[1] = streak.ToString();
       
 
       data.Add(temp);
    }
}

