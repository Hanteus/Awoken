using UnityEngine;
using System.Collections;

public class LifeScript : MonoBehaviour {

    public int maxLife = 6;
    int currentLife;
    
    
	// Use this for initialization
	void Start () {
        // Update maxLife with the one in the save file, if needed
        currentLife = maxLife;
    }

    public void damagePlayer(int damage)
    {
        if (currentLife - damage < 0)
            killPlayer();
        else
            currentLife = currentLife - damage;

        updateGUI();
    }

    public void healPlayer(int heal)
    {
        currentLife = currentLife + heal;

        if (currentLife > maxLife)
            currentLife = maxLife;

        updateGUI();
    }

    public void updateGUI() {
        // Se currentLife è pari setto currentLife / 2 a 2 assieme a quelli precedenti, quelli sucessivi a 0
        // Se currentLife è dispari setto currentLife / 2 a 1, quelli precedenti a 2, quelli sucessivi a 0
    }

    public void killPlayer() {
        // Kill player
    }

}