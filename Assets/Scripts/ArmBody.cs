using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmBody : MonoBehaviour
{
    //Data

    public Rigidbody2D leftBody;

    public Rigidbody2D rightBody;

    Rigidbody2D mainBody;

    public float power = 2f;

    public float score = 0;

    //Sound Effect

    public AudioSource mySource;

    public AudioClip jumpClip;

    //Text

    public GameObject Instruction;

    public GameObject Win;

    public GameObject Lose;

    //Phase

    bool beforeGame = true;

    bool inGame = false;

    bool gameLose = false;

    bool gameWin = false;

    // Start is called before the first frame update
    void Start()
    {
        mainBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Before Game Interaction

        if (beforeGame && Input.GetKeyDown(KeyCode.Q))
        {
            Instruction.SetActive(false);
            inGame = true;
        }

        //Body Interaction

        if (inGame)
        {
            //Sound Effect

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                //mySource.clip = jumpClip;
                //mySource.Play();
                mySource.PlayOneShot(jumpClip);
            }

            //Uplifting Continuation

            mainBody.velocity = new Vector3(0, power, 0);

            if (Input.GetKey(KeyCode.Space))
            {
                mainBody.velocity = new Vector3(0, -power, 0);
            }

            if (Input.GetKey(KeyCode.D))
            {
                rightBody.AddForce(transform.up * power, ForceMode2D.Impulse);
                mainBody.velocity = new Vector3(power, 0, 0);
            }
            else if (Input.GetKey(KeyCode.A))
            {

                leftBody.AddForce(transform.up * power, ForceMode2D.Impulse);
                mainBody.velocity = new Vector3(-power, 0, 0);
            }

            if(score >= 3)
            {
                gameWin = true;
            }
        }

        //Wining Condition

        if(gameLose)
        {
            inGame = false;
            Lose.SetActive(true);
        }
        else if(gameWin)
        {
            inGame = false;
            Win.SetActive(true);
        }

    }

    private void OnCollisionEnter2D(Collision2D somecollision)
    {
        Debug.Log(somecollision.gameObject.name);

        if (somecollision.gameObject.name == "Welkin")
        {
            gameLose = true;
        }

        if (somecollision.gameObject.name == "Star")
        {
            Destroy(somecollision.gameObject);
            score++;
        }

    }

}