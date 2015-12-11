using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameController : MonoBehaviour {

    public static GameController control;

    private Player player;

    void Awake () {
        //print ( Application.persistentDataPath );

        if ( control == null ) {
            DontDestroyOnLoad ( this.gameObject );

            control = this;

            this.Load ();
        }
        else if ( control != null ) {
            Destroy ( gameObject );
        }
    }

    void Start () {
        player = GameObject.FindGameObjectWithTag ( "Player" ).GetComponent<Player> ();
    }

    //We need to understand if there are only one save or many saves, one for every player
    public void Save () {
        BinaryFormatter bf = new BinaryFormatter ();
        FileStream file = File.Create ( Application.persistentDataPath + "/Awoken.dat" );
        Vector3 tempPos = player.getPos ();
        PlayerData data = new PlayerData ( player.getHealth () , tempPos.x, tempPos.y, tempPos.z);

        bf.Serialize ( file , data );

        file.Close ();

        Debug.Log ( "Game Succesfully saved!" );
    }

    public void Load () {
        if ( File.Exists ( Application.persistentDataPath + "/Awoken.dat" ) ) {
            BinaryFormatter bf = new BinaryFormatter ();
            FileStream file = File.Open ( Application.persistentDataPath + "/Awoken.dat" , FileMode.Open );
            Vector3 tempPos;
            PlayerData data = ( PlayerData ) bf.Deserialize ( file );

            file.Close ();

            tempPos = new Vector3 ( data.getPosx () , data.getPosy () , data.getPosz () );

            player.setHealth ( data.getHealth () );
            player.setPos ( tempPos );

            Debug.Log ( "Game Succesfully loaded!" );
        }
    }
}

[System.Serializable]
class PlayerData {
    private int health;
    private float playerPosx;
    private float playerPosy;
    private float playerPosz;

    public PlayerData ( int h , float x, float y, float z ) {
        this.health = h;
        this.playerPosx = x;
        this.playerPosy = y;
        this.playerPosz = z;
    }

    public int getHealth () {
        return this.health;
    }

    public float getPosx () {
        return playerPosx;
    }

    public float getPosy () {
        return playerPosy;
    }

    public float getPosz () {
        return playerPosz;
    }

    /*public void setHealth ( float h ) {
        this.health = h;
    }

    public void setExperience ( Vector3 pos ) {
        this.playerPos = pos;
    }*/
}

