using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    public float movespeed = 5f;
    public Rigidbody2D rb;
    Vector2 movement;

    public int pickUpCount = 0;
    public GameObject pickup;

    public Text Inventory;
    public Text Status;
    public Text WinText;
    public Text Firewoodcount;

    string myStatus;

    public int firewood;

    public int neededFirewood = 10;


    void Start()
    {
        SpawnPickUp();
        pickup = GameObject.FindGameObjectWithTag("PickUp");

        firewood = 0;

        WinText.enabled = false;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        Inventory.text = "Inventory: " + pickUpCount + "/1";
        Status.text = "Status: " + myStatus;
        Firewoodcount.text = firewood + " / " + neededFirewood;

        if (pickUpCount == 1)
        {
            Inventory.color = Color.red;
        }
        else
        {
            Inventory.color = Color.green;
        }

        if (firewood == neededFirewood)
        {
            WinText.enabled = true;
        }

    }

    public void SpawnPickUp()
    {
      
        Instantiate(pickup, new Vector3(UnityEngine.Random.Range(-10, 10), UnityEngine.Random.Range(-10, 10), 0), Quaternion.identity);
        
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * movespeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Obstacle")
        {
            Debug.Log("slow");
            movespeed = movespeed - 3;
            myStatus = "slower";
        }

        if (other.tag == "SpeedBoost")
        {
            Debug.Log("faster");
            movespeed = movespeed + 3;
            myStatus = "faster";
        }

        if (pickUpCount == 0)
        {
            if (other.tag == "PickUp")
            {
                Debug.Log("picked up");
                pickUpCount++;
                DestroyObject(other);
                SpriteRenderer sr = other.GetComponent<SpriteRenderer>();
                sr.enabled = false;
                Debug.Log("wurde vernichtet");

            }
        }
       

        if (other.tag == "TargetZone")
        {
            Debug.Log("Ablage Zone");

            if (pickUpCount == 1)
            {
                SpawnPickUp();
                pickUpCount--;
                firewood++;
            }

        }
    }
}
