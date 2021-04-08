using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesAttack : MonoBehaviour
{
    public Player player;
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public int damage;
    public GameObject deathEffects;
    //public Transform attackPos;
    //public float attackRange;
    public LayerMask layerPlayer;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (timeBtwAttack <= 0)
            {
                if (!player.shield.activeInHierarchy)
                {
                    Instantiate(deathEffects, player.transform.position, Quaternion.identity);
                    player.ChangeHealth(-damage);
                    timeBtwAttack = startTimeBtwAttack;
                }
                else
                    player.ChangeHealth(-damage);
            }
            else
                timeBtwAttack -= Time.deltaTime;
        }
    }
}
