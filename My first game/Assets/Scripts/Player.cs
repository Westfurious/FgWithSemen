using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Controls")]
    public float speed;

    [Header("Health")]
    public float health;
    public int numOfHeart;
    public Canvas canvas;
    public GameObject heartEffect;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public List<Image> hearts;

    [Header("Shield")]
    public GameObject shield;
    public Shield shieldTimer;

    [Header("Key")]
    public GameObject keyIcon;
    public GameObject wallEffect;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator anim;

    private bool keyButtonedPush;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.sqrMagnitude);
    }
    private void FixedUpdate()
    {
        if (health > numOfHeart)
        {
            numOfHeart = Mathf.RoundToInt(health);
            if (health > 10)
                health = numOfHeart = 10;
            else
                canvas.gameObject.transform.Find("Heart" + " " + health).gameObject.SetActive(true);
        }
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < Mathf.RoundToInt(health))
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;
            if (i < numOfHeart)
                hearts[i].enabled = true;
            else
                hearts[i].enabled = false;
        }
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("NewHeart"))
        {
            ChangeHealth(1);
            Instantiate(heartEffect, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Shield"))
        {
            shield.SetActive(true);
            shieldTimer.gameObject.SetActive(true);
            shieldTimer.isCooldown = true;
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Key"))
        {
            keyIcon.SetActive(true);
            Destroy(other.gameObject);
        }
    }
    public void OnKeyButtonDown()
    {
        keyButtonedPush = !keyButtonedPush;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Door") && keyButtonedPush && keyIcon.activeInHierarchy)
        {
            Instantiate(wallEffect, other.transform.position, Quaternion.identity);
            keyIcon.SetActive(false);
            other.gameObject.SetActive(false);
            keyButtonedPush = false;
        }
    }
    public void ChangeHealth(int healthValue)
    {
        if (!shield.activeInHierarchy || shield.activeInHierarchy && healthValue > 0)
            health += healthValue;
        else if (shield.activeInHierarchy && healthValue < 0)
            shieldTimer.ReduceTime(healthValue);
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
