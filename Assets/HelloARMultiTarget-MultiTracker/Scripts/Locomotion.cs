//
// Mecanimのアニメーションデータが、原点で移動しない場合の Rigidbody付きコントローラ
// サンプル
// 2014/03/13 N.Kobyasahi
//
using UnityEngine;
using System.Collections;

namespace UnityChan
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Rigidbody))]

    public class Locomotion : MonoBehaviour
    {
        private Rigidbody rb;
        private Animator anim;                       
        private AnimatorStateInfo currentBaseState;    
       
        
        static int idleState = Animator.StringToHash("Base Layer.Idle");
        static int locoState = Animator.StringToHash("Base Layer.Locomotion");
        static int jumpState = Animator.StringToHash("Base Layer.Jump");
        static int restState = Animator.StringToHash("Base Layer.Rest");
        static int locoBackState = Animator.StringToHash("Base Layer.WalkBack");
        
        void Start()
        {
            anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();

            rb.maxAngularVelocity = 7;
        }

        void FixedUpdate()
        { 

            Vector3 acc = Input.acceleration;
            float h = acc.x;
            float v = acc.z;
            currentBaseState = anim.GetCurrentAnimatorStateInfo(0);
            

            

            if (rb.velocity.magnitude < 4)
            {
                if (v <= -0.6)
                {
                    this.rb.AddForce(transform.forward * (Mathf.Abs(v) + 1), ForceMode.Impulse);
                    anim.SetFloat("Speed", 7);
                }
                else if (v > -0.4 )
                {
                    this.rb.AddForce(transform.forward * -(Mathf.Abs(v) + 1), ForceMode.Impulse);
                    anim.SetFloat("Speed", -2);
                }
                else
                {
                    this.rb.velocity = Vector3.zero;
                    anim.SetFloat("Speed", 0);
                }
            }

            if (rb.angularVelocity.magnitude < 1.5f)
            {
                rb.angularVelocity = rb.angularVelocity.normalized;
            }

            if (h < -0.1f) {
                this.rb.AddTorque(transform.up * (h-1), ForceMode.Impulse);
            }
            else if (h > 0.1f)
            {
                rb.AddTorque(transform.up * (h+1), ForceMode.Impulse);
            }
            else
            {
                rb.angularVelocity = Vector3.zero;
            }

            
        }
    }
}