using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MLAgents;
using MLAgents.Sensors;

public class AI : Agent//MonoBehaviour
{
    public static AI robot;

    private Rigidbody rig;
    public GameObject imageobject;
    public Text score;

    public GameObject rock;
    public GameObject red;
    public GameObject scale_;

    public bool is_rock = false;
    private float time_count;
    private Vector2[] safe = new Vector2[3];
    private int mode = 3;

    public bool is_fall = false;

    public float speed;

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
        robot = this;
        
    }

    private void Update()
    {
        time_count -= Time.deltaTime;

        if (rig.velocity.x > 1)
        {
            imageobject.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (rig.velocity.x < -1)
        {
            imageobject.transform.localScale = new Vector3(-1, 1, 1);
        }

    }

    //ai

    public override void OnEpisodeBegin()
    {
        time_count = 6.5f;
        is_rock = false;
        is_fall = false;

        do
        {
            for (int i = 0; i < safe.Length; i++)
            {
                if ((i + 1) <= mode)
                {
                    safe[i] = new Vector2(Random.Range(1, 5), Random.Range(1, 5));
                }
                else
                {
                    safe[i] = new Vector2(100, 100);
                }
            }
        } while ((safe[0] == safe[1] || safe[0] == safe[2] || safe[1] == safe[2]) && mode > 1);


        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                bool is_safe = false;
                for (int i = 0; i < safe.Length; i++)
                {
                    if ((safe[i].x == (x + 1)) && (safe[i].y == (y + 1)))
                    {
                        is_safe = true;
                    }
                }

                if (!is_safe)
                {
                    //Instantiate(red, new Vector3(-6 + (x * 4), 0, -6 + (y * 4)), Quaternion.Euler(90, 0, 0));
                    GameObject target = Instantiate(red, scale_.transform);
                    target.transform.localPosition = new Vector3(-6 + (x * 4), 0, -6 + (y * 4));
                    target.transform.localRotation = Quaternion.Euler(90, 0, 0);
                }
            }
        }

        Invoke("rockbegin", 5);
    }

    private void rockbegin()
    {
        for(int x = 0; x < 4; x++)
        {
            for(int y = 0; y < 4; y++)
            {
                bool is_safe = false;
                for(int i = 0; i < safe.Length; i++)
                {
                    if ((safe[i].x == (x + 1)) && (safe[i].y == (y + 1)))
                    {
                        is_safe = true;
                    }
                }

                if (!is_safe)
                {
                    //Instantiate(rock, new Vector3(-6 + (x * 4), 0, -6 + (y * 4)), new Quaternion());
                    GameObject target = Instantiate(rock, scale_.transform);
                    target.transform.localPosition = new Vector3(-6 + (x * 4), 0, -6 + (y * 4));
                    target.transform.localRotation = new Quaternion();
                }
            }
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(new Vector2(transform.position.x, transform.position.z));

        sensor.AddObservation(new Vector2(rig.velocity.x,rig.velocity.z));

        for (int i = 0; i < safe.Length; i++) 
        {
            sensor.AddObservation(new Vector2(-6 + (safe[i].x * 4), -6 + (safe[i].y * 4)) * 2);
        }

        sensor.AddObservation(mode);
        sensor.AddObservation(is_rock);
        sensor.AddObservation(time_count);
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        rig.velocity = new Vector3(vectorAction[0] * speed, 0, vectorAction[1] * speed);

        if (time_count <= 0)
        {
            if (is_rock)
            {
                SetReward(-1);
                Debug.Log("-1");
                score.text = "-1";
                mode = 3;
            }
            else
            {
                SetReward(4 - mode);
                Debug.Log((4 - mode).ToString());
                score.text = (4 - mode).ToString();
                if (mode > 1)
                {
                    mode -= 1;
                }
            }

            EndEpisode();
        }

        if (is_fall)
        {
            transform.position = new Vector3(0, 2, 0);
            SetReward(-2);
            Debug.Log("-2");
            score.text = "-1";
            mode = 3;
            CancelInvoke();
            rig.velocity = Vector3.zero;
            EndEpisode();
        }
    }

    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = Input.GetAxis("Horizontal");
        actionsOut[1] = Input.GetAxis("Vertical");
    }
}
