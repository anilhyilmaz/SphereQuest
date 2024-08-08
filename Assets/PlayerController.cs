using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rb;
    public float speed;
    public TMP_Text WonText;
    public Vector3 LastPosition;
    public int Health;
    public TMP_Text HealthTxt;
    public Camera Camera;
    public Vector3 StartPosition;
    GameObject[] GroundGO;
    GameObject[] ErrorGO;
    public Material material;
    public GameObject[] Layers;
    public TMP_Text LevelText;
    private int counter = 0; // private counter
    public Vector3 input;

    void Start()
    {
        Debug.Log("Heelo");
        HealthTxt.text = Health.ToString();
        StartPosition = gameObject.transform.position;
        Layers = GameObject.FindGameObjectsWithTag("Grid");
        Array.Reverse(Layers);

        foreach (GameObject Layer in Layers)
        {
            Layer.SetActive(false);
        }

        if (Layers.Length > 0)
        {
            Layers[0].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            LastPosition = transform.position;
            rb.AddForce(Vector3.left * speed);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            LastPosition = transform.position;
            rb.AddForce(Vector3.forward * speed);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            LastPosition = transform.position;
            rb.AddForce(Vector3.back * speed);
        }

        GameOver();
        FollowPlayer();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            collision.gameObject.GetComponent<Renderer>().material.color = Color.green;
        }
        if (collision.gameObject.CompareTag("Final"))
        {
            collision.gameObject.GetComponent<Renderer>().material.color = Color.green;
            StopVelocity();
            ActivateLayer();
        }
        if (collision.gameObject.CompareTag("Error"))
        {
            collision.gameObject.GetComponent<Renderer>().material.color = Color.red;
            transform.position = LastPosition;
            StopVelocity();
            Health -= 1;
            HealthTxt.text = Health.ToString();

            if (Health <= 0)
            {
                gameObject.transform.position = StartPosition;
                Health = 3;
                HealthTxt.text = Health.ToString();
                GroundGO = GameObject.FindGameObjectsWithTag("Ground");

                foreach (GameObject i in GroundGO)
                {
                    i.gameObject.GetComponent<Renderer>().material.color = material.color;
                }

                ErrorGO = GameObject.FindGameObjectsWithTag("Error");

                foreach (GameObject j in ErrorGO)
                {
                    j.gameObject.GetComponent<Renderer>().material.color = material.color;
                }
            }
        }
    }

    void FollowPlayer()
    {
        Camera.transform.LookAt(this.transform);
    }

    void GameOver()
    {
        if (gameObject.transform.position.y <= 0)
        {
            Debug.Log("Game Over");
            StopVelocity();
            gameObject.transform.position = StartPosition;
        }
    }

    public void ActivateLayer()
    {
        if (Layers.Length == 0) return;

        // Deactivate the current layer
        Layers[counter].SetActive(false);

        // Update the counter to the next layer index
        counter = (counter + 1) % Layers.Length;

        // Activate the next layer
        Layers[counter].SetActive(true);
        LevelText.text = "Level " + counter.ToString();

        // Reset player position
        this.gameObject.transform.position = StartPosition;
        Health = 3;
        HealthTxt.text = Health.ToString();

        if(counter  == Layers.Length - 1)
        {
            Debug.Log("gg");
            WinGame();
        }
        Debug.Log("counter:" + counter);
        Debug.Log(Layers.Length);
    }
    private void StopVelocity()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
    private void WinGame()
    {
        WonText.gameObject.SetActive(true); 
    }
}
