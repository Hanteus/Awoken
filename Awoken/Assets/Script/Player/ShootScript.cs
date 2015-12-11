using UnityEngine;

public class ShootScript : MonoBehaviour
{

    public Player player;
    public GameObject projectile;

    GameObject shootPositionObject;
    float speed = 4f;

    // VARIABILI //

    // Prefab del proiettile
    public Transform shootPrefab;
    // Cooldown
    public float cooldown = 0.2f;
    float waitingTime;

    // CODICE //

    // Inizializzo il cooldown
    void Start()
    {
        player = transform.parent.GetComponent<Player>();
        shootPositionObject = this.transform.FindChild("ShootPosition").gameObject;
    }

    // Aggiorno il tempo di cooldown
    void Update()
    {
        if (waitingTime > 0)
        {
            waitingTime -= Time.deltaTime;
        }
        else if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            Fire();
            waitingTime = cooldown;
        }
    }

    void Fire()
    {

        if (player.getFacingRight())
        {
            GameObject prj = (GameObject)Instantiate(projectile, shootPositionObject.transform.position, transform.rotation);
            prj.GetComponent<BulletScript>().setDirection(-1f);
        }
        else
        {
            Vector3 rotationVector = transform.rotation.eulerAngles;
            rotationVector.x = 180 - rotationVector.x;

            GameObject prj = (GameObject)Instantiate(projectile, shootPositionObject.transform.position, Quaternion.Euler(rotationVector));
            prj.GetComponent<BulletScript>().setDirection(1f);
        }
    }

}