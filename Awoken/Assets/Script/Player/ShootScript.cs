using UnityEngine;

public class ShootScript : MonoBehaviour
{

	// VARIABILI //

	// Prefab del proiettile
	public Transform shootPrefab;
	// Velocità di fuoco
	public float shootingRate = 0.2f;
	// Cooldown
	private float shootCooldown;

	// CODICE //

	// Inizializzo il cooldown
	void Start()
	{
		shootCooldown = 0f;
	}

	// Aggiorno il tempo di cooldown
	void Update()
	{
		if (shootCooldown > 0)
		{
			shootCooldown -= Time.deltaTime;
		}
	}

	// Sparo
	public void Attack()
	{
		if (CanAttack)
		{
			shootCooldown = shootingRate;
			
			// Creo un proiettile
			var shootTransform = Instantiate(shootPrefab) as Transform;
			
			// Assegno la posizione
			shootTransform.position = transform.position;

			// Lo faccio muovere (MoveForwardScript serve a qualcosa?)
			// MoveForwardScript move = shootTransform.gameObject.GetComponent<MoveForwardScript>();
		}
	}

	// Controllo se posso sparare
	public bool CanAttack
	{
		get
		{
			return shootCooldown <= 0f;
		}
	}

}