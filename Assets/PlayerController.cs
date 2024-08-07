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
    void Start()
    {
        Debug.Log("Heelo");
        HealthTxt.text = Health.ToString();
        StartPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            LastPosition = transform.position;
            rb.AddForce(  Vector3.left * speed);
        }
        /*if (Input.GetKeyDown(KeyCode.S))
        {
            LastPosition = transform.position;
            rb.AddForce(Vector3.right * speed);
        }
        */
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
        if(gameObject.transform.position.y <= 0)
        {
            Debug.Log("Game Over");
        }
        FollowPlayer();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            collision.gameObject.GetComponent<Renderer>().material.color = Color.green;
        }
        if (collision.gameObject.tag == "Final")
        {
            collision.gameObject.GetComponent<Renderer>().material.color = Color.green;
            WonText.gameObject.SetActive(true);


        }
        if(collision.gameObject.tag == "Error")
        {
            collision.gameObject.GetComponent<Renderer>().material.color = Color.red;
            transform.position = LastPosition;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            Health = (Health - 1);
            HealthTxt.text = Health.ToString();
            if(Health == 0)
            {
                gameObject.transform.position = StartPosition;
                Health = 3;
                HealthTxt.text = Health.ToString();
                GroundGO = GameObject.FindGameObjectsWithTag("Ground");
                foreach(GameObject i in GroundGO)
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
}
