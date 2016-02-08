using UnityEngine;
using System.Collections;
using System.IO;

public class FileManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if ( File.Exists ( Application.dataPath + "/Awoken.dat" ) )
            File.Delete ( Application.dataPath + "/Awoken.dat" );

    }
}
