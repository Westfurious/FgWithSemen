using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Player player;
    public int health;
    public float speed;
    public GameObject deathEffects;
    public float startStopTime;

    [HideInInspector] public bool playerNotInRoom;

    private Animator anim;
    private bool stopped;
    private float stopTime;
    private AddRoom room;

    void Start()
    {
        anim = GetComponent<Animator>();
        room = GetComponentInParent<AddRoom>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerNotInRoom)
        {
            if (stopTime <= 0)
                stopped = false;
            else
            {
                stopped = true;
                stopTime -= Time.deltaTime;
            }
        }
        else
            stopped = true;
        if (health <= 0)
        {
            Instantiate(deathEffects, transform.position, Quaternion.identity);
            Destroy(gameObject);
            room.enemies.Remove(gameObject);
        }
        if (player.transform.position.x > transform.position.x)
            transform.eulerAngles = new Vector3(0, 180, 0);
        else
            transform.eulerAngles = new Vector3(0, 0, 0);
        if (!stopped)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            anim.SetBool("IsRunning", true);
        }
        else
            anim.SetBool("IsRunning", false);
    }
    public void TakeDamage(int damage)
    {
        stopTime = startStopTime;
        health -= damage;
    }
}
