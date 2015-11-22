using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LifeScript : MonoBehaviour {

    int currentLife;
    int maxLife = 6;

    // Variables for the life
    public GameObject lifeGUI;
    List<Animator> lifeAnimatorArray;
    int childNumber = 0;

    // Variables for the life-bar
    public GameObject lifeBar;
    float lifeBarPositionX;
    float lifeBarSizeX;

    // Use this for initialization
    void Start () {
        // Update maxLife with the one in the save file, if needed
        currentLife = maxLife;

        // Initialize life-bar variables
        lifeBarPositionX = lifeBar.transform.localPosition.x;
        lifeBarSizeX = lifeBar.transform.localScale.x;

        // Pupulate lifeArray    
        lifeAnimatorArray = new List<Animator>();

        foreach (Transform child in lifeGUI.transform)
            childNumber++;

        for (int i = 1; i <= childNumber; i++) {
            foreach (Transform child in lifeGUI.transform) {
                if (child.name == "Eye (" + i + ")")
                {
                    lifeAnimatorArray.Add(child.GetComponent<Animator>());
                }
            }
         }

        updateLifeGUI();
    }

    public void damagePlayer(int damage)
    {
        if (currentLife - damage < 0)
            killPlayer();
        else
            currentLife = currentLife - damage;

        updateLifeGUI();
        updateLifeBar();
    }

    public void healPlayer(int heal)
    {
        currentLife = currentLife + heal;

        if (currentLife > maxLife)
            currentLife = maxLife;

        updateLifeGUI();
        updateLifeBar();
    }

    public void updateLifeGUI() {
        int displayedLife = 0;

        if (currentLife % 2 == 0) {
            // Se currentLife è pari setto currentLife / 2 a 2 assieme a quelli precedenti, quelli sucessivi a 0
            displayedLife = currentLife / 2;

            for (int i = 0; i < displayedLife; i++) {
                lifeAnimatorArray[i].SetInteger("state", 2);
            }

            for (int i = displayedLife; i < maxLife / 2; i++)
            {
                lifeAnimatorArray[i].SetInteger("state", 0);
            }
        }
        else {
            // Se currentLife è dispari setto currentLife / 2 a 1, quelli precedenti a 2, quelli sucessivi a 0
            displayedLife = currentLife / 2;

            for (int i = 0; i < displayedLife; i++)
            {
                lifeAnimatorArray[i].SetInteger("state", 2);
            }

            lifeAnimatorArray[displayedLife].SetInteger("state", 1);

            for (int i = displayedLife + 1; i < maxLife / 2; i++)
            {
                lifeAnimatorArray[i].SetInteger("state", 0);
            }
        }
    }

    public void updateLifeBar() {
        lifeBar.transform.localScale = new Vector3((currentLife * 1f) / maxLife * lifeBarSizeX, lifeBar.transform.localScale.y, lifeBar.transform.localScale.x);
        lifeBar.transform.localPosition = new Vector3(lifeBarPositionX - (1 - (currentLife * 1f) / maxLife) * lifeBarSizeX * 2, lifeBar.transform.localPosition.y, lifeBar.transform.localPosition.x);
    }


    public void killPlayer() {
        // Kill player
    }

}