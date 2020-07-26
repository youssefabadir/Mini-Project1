using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float tilt = 10;
    bool FlipMood = false;
    int Health = 3;
    public Image HealthBar;
    public Text HealthScore;
    public Text Score;
    int Sco = 0;
    public GameObject GameOver;
    public Rigidbody Player;
    public GameObject FPV;
    public GameObject TPV;
    bool C = false;
    float Timer = 15;
    public Material[] RandomColor;
    public AudioSource Collect;
    public AudioSource Incorrect;
    public AudioSource ChangeColor;
    public Light Color;
    public Light ColorDown;
    public GameObject ColorD;
    public GameObject ColorUp;
    public ConstantForce Force;
    public AudioSource Jump;

    void Start()
    {
        Color.GetComponent<Light>().color = RandomColor[Random.Range(0, RandomColor.Length)].color;
        ColorDown.color = Color.color;
        Player.AddForce(0, 0, 400);
    }

    // Update is called once per frame
    void Update()
    {
        Timer -= 1 * Time.deltaTime;
        if(Timer <= 0)
        {
            ChangeColor.Play();
            Timer = 15;
            Color.color = RandomColor[Random.Range(0, RandomColor.Length)].color;
            ColorDown.color = Color.color;
        }
        Movement();
        if (Input.GetKeyDown(KeyCode.C))
            Camera();
    }

    void Movement()
    {
        transform.Translate(Input.acceleration.x, 0, 0);
        if (Health <= 0)
        {
            Player.isKinematic = true;
            GameOver.SetActive(true);
        }
        else
        {
            float distance = tilt * Time.deltaTime;
            transform.Translate(Input.GetAxis("Horizontal") * distance, 0, 0);
            if(Player.transform.position.x > 2.45)
            {
                Player.transform.position = new Vector3(2.45f, Player.transform.position.y, Player.transform.position.z);
            }
            if (Player.transform.position.x < -2.45)
            {
                Player.transform.position = new Vector3(-2.45f, Player.transform.position.y, Player.transform.position.z);
            }


            if (Input.GetButtonDown("Jump") && !FlipMood)
            {
                Jump.Play();
                transform.Translate(0, 1.95f, 0);
                FlipMood = true;
                TPV.transform.Translate(0, -1.95f, 0);
                ColorD.SetActive(true);
                ColorUp.SetActive(false);
            }
            else if (Input.GetButtonDown("Jump") && FlipMood)
            {
                Jump.Play();
                TPV.transform.Translate(0, 1.95f, 0);
                transform.Translate(0, -1.95f, 0);
                FlipMood = false;
                TPV.transform.Translate(0, 0, 0);
                ColorD.SetActive(false);
                ColorUp.SetActive(true);
            }

        }
    }

    public void Camera()
    {
        if(!C)
        {
            FPV.SetActive(false);
            TPV.SetActive(true);
            C = true;
        }
        else
        {
            FPV.SetActive(true);
            TPV.SetActive(false);
            C = false;
        }
    }

/*    public void OnJump()
    {
        if (!FlipMood)
        {
            Jump.Play();
            transform.Translate(0, 1.95f, 0);
            FlipMood = true;
            TPV.transform.Translate(0, -1.95f, 0);
            ColorD.SetActive(true);
            ColorUp.SetActive(false);
        }
        else
        {
            Jump.Play();
            TPV.transform.Translate(0, 1.95f, 0);
            transform.Translate(0, -1.95f, 0);
            FlipMood = false;
            TPV.transform.Translate(0, 0, 0);
            ColorD.SetActive(false);
            ColorUp.SetActive(true);
        }

    }
*/
    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "CD") | (other.gameObject.tag == "CDL") | (other.gameObject.tag == "CDR")
        | (other.gameObject.tag == "CU") | (other.gameObject.tag == "CUR") | (other.gameObject.tag == "CUL"))
        {
            if (!FlipMood)
            {
                if (Color.color == other.gameObject.GetComponent<Renderer>().material.color)
                {
                    Collect.Play();
                    GameObject.Destroy(other.gameObject);
                    Sco += 10;
                }

                if (Color.color != other.gameObject.GetComponent<Renderer>().material.color)
                {
                    Incorrect.Play();
                    GameObject.Destroy(other.gameObject);
                    Sco -= 5;
                }

            }
            else if (FlipMood)
            {
                if (Color.color == other.gameObject.GetComponent<Renderer>().material.color)
                {
                    Incorrect.Play();
                    GameObject.Destroy(other.gameObject);
                    Sco -= 5;
                }

                if (Color.color != other.gameObject.GetComponent<Renderer>().material.color)
                {
                    Collect.Play();
                    GameObject.Destroy(other.gameObject);
                    Sco += 10;
                }

            }
            if(Sco < 0)
            {
                Sco = 0;
            }
            Score.text = "Score: " + Sco;
            int AddSpeed = Sco / 50;
            GetComponent<ConstantForce>().force = new Vector3(0, 0, 0 + AddSpeed);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if((collision.gameObject.tag == "F1") | (collision.gameObject.tag == "F1R") | (collision.gameObject.tag == "F1L") |
        (collision.gameObject.tag == "F2R") | (collision.gameObject.tag == "F2L") | (collision.gameObject.tag == "H3U") |
        (collision.gameObject.tag == "H3D") | (collision.gameObject.tag == "H2RU") | (collision.gameObject.tag == "H2LU") |
        (collision.gameObject.tag == "H2RD") | (collision.gameObject.tag == "H2LD"))
        {
            Incorrect.Play();
            GameObject.Destroy(collision.gameObject);
            Health --;
            HealthBar.fillAmount = Health / 3f;
            HealthScore.text = "Health: " + Health;
            Player.AddForce(0, 0, 300);
        }
    }

}
