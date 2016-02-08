using UnityEngine;
using Fungus;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    public static GameController control;
    public GameObject pausePanel;
    public GameObject [] levels;
    public GameObject [] weapons;
    public Texture2D loadTexture;

    public Player player;

    private Flowchart fc;

    private Dictionary<string, string> LVLS = new Dictionary<string, string>();

    void Start() {
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        fc = GameObject.FindGameObjectWithTag("Flowchart").GetComponent<Flowchart>();

        LVLS.Add("LVL0", "Level 0 - Room");
        LVLS.Add("LVL1", "Level 1 - Dream Room");
        LVLS.Add("LVL2", "Level 2 - Temple");
        LVLS.Add("LVL3", "Level 3 - Gate");

        if (control == null) {
            DontDestroyOnLoad(this.gameObject);

            control = this;

            this.Load();
        } else if (control != null) {
            Destroy(gameObject);
        }
    }

    void Update() {
        if (Input.GetButtonDown("Cancel")) {
            this.TogglePauseMenu();
        }
    }

    //We need to understand if there are only one save or many saves, one for every player
    public void Save() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath + "/Awoken.dat");
        Vector3 tempPos = player.getPos();
        string tempCL = null;

        foreach (KeyValuePair<string, string> lvl in LVLS) {

            if (GameObject.FindGameObjectWithTag(lvl.Key) != null) {
                tempCL = lvl.Key;
            }
        }

        PlayerData data = new PlayerData(player.getHealth(), tempPos.x, tempPos.y, tempPos.z, tempCL);
        bf.Serialize(file, data);

        file.Close();

        Debug.Log("Game Succesfully saved!");
    }

    public void Load() {
        if (File.Exists(Application.dataPath + "/Awoken.dat")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.dataPath + "/Awoken.dat", FileMode.Open);
            Vector3 tempPos;
            PlayerData data = (PlayerData)bf.Deserialize(file);
            string tempCL = null;
            GameObject tempLVL = null;

            file.Close();

            tempPos = new Vector3(data.getPosx() + 1.0f, data.getPosy(), data.getPosz());

            tempCL = data.getCurrentLevel();

            tempLVL = levels[dictionaryIndex(tempCL)];

            if ( tempLVL != null ) {

                tempLVL.SetActive ( true );
                Debug.Log ( tempLVL.tag );

                foreach ( KeyValuePair<string , string> lvl in LVLS ) {

                    if ( !(lvl.Key).Equals(tempLVL.tag) && GameObject.FindGameObjectWithTag ( lvl.Key ) != null ) {
                        GameObject.FindGameObjectWithTag ( lvl.Key ).SetActive (false);
                    }
                }
            }
                
            else
                Debug.Log("Error during loading level " + tempCL);

            if ( tempLVL.name.Equals ( "Level 3 - Gate" ) ) {

                weapons [ 0 ].gameObject.SetActive ( false );
                weapons [ 1 ].gameObject.SetActive ( false );
                weapons [ 2 ].gameObject.SetActive ( true );
                weapons [ 3 ].gameObject.SetActive ( true );
            }

            player.setHealth(data.getHealth());
            player.setPos(tempPos);

            Debug.Log("Game Succesfully loaded!");
        }
    }

    int dictionaryIndex(string key) {
        int index = 0;

        foreach (KeyValuePair<string, string> lvl in LVLS) {

            if (lvl.Key.Equals(key))
                break;
            else
                index++;
        }

        return index;
    }

    public void TogglePauseMenu() {
        // not the optimal way but for the sake of readability
        if (!pausePanel.activeInHierarchy) {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        } else
            ResumeGame();
    }

    public void ResumeGame() {
        pausePanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void LoadMainMenu() {
        ResumeGame();
        SceneLoader.LoadScene("Menu - Main", loadTexture);
    }
}

[System.Serializable]
class PlayerData {
    private int health;
    private float playerPosx;
    private float playerPosy;
    private float playerPosz;
    private string currentLevel;

    public PlayerData(int h, float x, float y, float z, string cl) {
        this.health = h;
        this.playerPosx = x;
        this.playerPosy = y;
        this.playerPosz = z;
        this.currentLevel = cl;
    }

    public int getHealth() {
        return this.health;
    }

    public float getPosx() {
        return playerPosx;
    }

    public float getPosy() {
        return playerPosy;
    }

    public float getPosz() {
        return playerPosz;
    }

    public string getCurrentLevel() {
        return currentLevel;
    }

    /*public void setHealth ( float h ) {
        this.health = h;
    }

    public void setExperience ( Vector3 pos ) {
        this.playerPos = pos;
    }*/
}

