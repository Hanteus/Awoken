using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    public void LoadCredits () {
        SceneManager.LoadScene ( "Menu - Credits" );
    }

    public void LoadGame () {
        SceneManager.LoadScene ( "LetterScene" );
    }

    public void LoadMain () {
        SceneManager.LoadScene ( "Menu - Main" );
    }

    public void Quit () {
        Application.Quit ();
    }

}
