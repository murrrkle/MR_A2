using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    public List<GameObject> checkpoints;
    int state; // 0 = idle, 1 = moving
    public int cardsDetected;
    int nextCheckpoint;

    static int idleState = Animator.StringToHash("Base Layer.Idle");
    static int locoState = Animator.StringToHash("Base Layer.Locomotion");
    static int jumpState = Animator.StringToHash("Base Layer.Jump");
    static int restState = Animator.StringToHash("Base Layer.Rest");

    private CapsuleCollider col;
    private Rigidbody rb;
    private AudioSource asce;
    private Animator anim;                          
    private AnimatorStateInfo currentBaseState;
    private float orgColHight;
    private Vector3 orgVectColCenter;
    

    // Start is called before the first frame update
    void Start()
    {
        state = 0;
        cardsDetected = 0;
        nextCheckpoint = 0;
        
        anim = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();

        orgColHight = col.height;
        orgVectColCenter = col.center;
        asce = GetComponent<AudioSource>();
    }

    
    void FixedUpdate()
    {
        /*
        if (cardsDetected >= 3)
        {
            state = 1;
        } else
        {
            state = 0;
        }

        if (state == 1)
        {
            if (nextCheckpoint <=3)
            {
                gameObject.transform.LookAt(checkpoints[nextCheckpoint].transform);
                anim.SetBool("Locomotion", true);
                if (rb.velocity.magnitude <= 0 || rb.velocity.magnitude > 1)
                {

                    this.rb.AddForce(Vector3.Normalize(this.transform.forward)*1.8f, ForceMode.Impulse);
                }
                anim.SetBool("Jump", false);

            } else
            {
                anim.SetBool("Locomotion", false);
            }
        }
        */
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "jump")
        {
            anim.SetBool("Jump", true);
            nextCheckpoint += 1;
        }
        else if (other.tag == "Finish")
        {
            asce.Play();
            GetComponent<ParticleSystem>().Play();
        }
    }

    public void UpdateNumOfTargets(int value)
    {
        cardsDetected += value;

    }
}
