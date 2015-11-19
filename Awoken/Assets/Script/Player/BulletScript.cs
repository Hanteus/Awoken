using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour
{

	// VARIABILI //

	// Danno
	public int damage = 1;
	// Dice se il proiettile è del nemico o del giocatore
	public bool isEnemyShot = false;
	// Variabili per la gestione della collisione
	public Transform impactCheck;
	public LayerMask whatCanCollide;
	float checkRadius = 0.0001f;
	bool impact = true;
	
	// CODICE //

	void Start ()
	{
		Destroy(gameObject, 20);
	}

	void FixedUpdate ()
	{
		impact = Physics2D.OverlapCircle(impactCheck.position, checkRadius, whatCanCollide);

		if (impact == true)
		{
			Destroy(gameObject);
			// Eventuale animazione
		}
	}

}