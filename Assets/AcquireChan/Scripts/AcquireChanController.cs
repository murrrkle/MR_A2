using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcquireChanController : MonoBehaviour
{
	// Inspector
	[SerializeField] private float	m_WalkSpeed		= 2.0f;
	[SerializeField] private float	m_RunSpeed		= 3.5f;
	[SerializeField] private float	m_RotateSpeed	= 8.0f;
	[SerializeField] private float	m_JumpForce		= 300.0f;
	[SerializeField] private float	m_RunningStart	= 1.0f;

	// member
	private Rigidbody	m_RigidBody	= null;
	private Animator	m_Animator	= null;
    private AudioSource m_AudioSource = null;
	private float		m_MoveTime	= 0;
	private float		m_MoveSpeed	= 0.0f;
	private bool		m_IsGround	= true;

    
	private void Awake()
	{
		m_RigidBody = this.GetComponentInChildren<Rigidbody>();
		m_Animator = this.GetComponentInChildren<Animator>();
		m_MoveSpeed = m_WalkSpeed;
	}

    private void Start()
    {/*
        m_AudioSource.clip = Microphone.Start(null, true, 1, 44100);
        m_AudioSource.loop = true;
        while (!(Microphone.GetPosition(null) > 0)) { }
        m_AudioSource.Play();*/
    }

    private void Update()
	{
		if( null == m_RigidBody ) return;
		if( null == m_Animator ) return;

		// check ground
		float rayDistance = 0.3f;
		Vector3 rayOrigin = (this.transform.position + (Vector3.up * rayDistance * 0.5f));
		bool ground = Physics.Raycast( rayOrigin, Vector3.down, rayDistance, LayerMask.GetMask( "Default" ) );
		if( ground != m_IsGround )
		{
			m_IsGround = ground;

			// landing
			if( m_IsGround )
			{
				m_Animator.Play( "landing" );
			}
		}

		// input
		Vector3 vel = m_RigidBody.velocity;
        //float h = Input.GetAxis( "Horizontal" );
        //float v = Input.GetAxis( "Vertical" );

        Vector3 acc = Input.acceleration;
        float h = acc.x;
        float v = acc.y;

        h = Remap(h, -0.2f, 0.2f, -1, 1);
        v = Remap(v, -0.8f, 0.0f, -1, 1);

        bool isMove = ((0 != h) || (0 != v));

		m_MoveTime = isMove? (m_MoveTime + Time.deltaTime) : 0;
		bool isRun = (m_RunningStart <= m_MoveTime);

		// move speed (walk / run)
		float moveSpeed = isRun? m_RunSpeed : m_WalkSpeed;
		m_MoveSpeed = isMove? Mathf.Lerp( m_MoveSpeed, moveSpeed, (8.0f * Time.deltaTime) ) : m_WalkSpeed;
//		m_MoveSpeed = moveSpeed;

		Vector3 inputDir = new Vector3( h, 0, v );

		if( 1.0f < inputDir.magnitude ) inputDir.Normalize();

		if( 0 != h ) vel.x = (inputDir.x * m_MoveSpeed);
		if( 0 != v ) vel.z = (inputDir.z * m_MoveSpeed);

		m_RigidBody.velocity = vel;

		if( isMove )
		{
			// rotation
			float t = (m_RotateSpeed * Time.deltaTime);
			Vector3 forward = Vector3.Slerp( this.transform.forward, inputDir, t );
			this.transform.rotation = Quaternion.LookRotation( forward );
		}

		m_Animator.SetBool( "isMove", isMove );
		m_Animator.SetBool( "isRun", isRun );

        MicManager m = GameObject.FindGameObjectWithTag("mic").GetComponent<MicManager>();

        if (m.LevelMax* 100000 > 10)
        {
            m_Animator.Play("jump");
            m_RigidBody.AddForce(Vector3.up * m_JumpForce);
        }

        /*
        float[] samples = new float[m_AudioSource.clip.samples * m_AudioSource.clip.channels];
        m_AudioSource.clip.GetData(samples, 0);

        float sum = 0;
        float average = 0;
        for (var i = 0; i < samples.Length; i++)
        {
            sum += samples[i];
        }
        average = sum / samples.Length;

        if (average > 500 && m_IsGround)
        {
            m_Animator.Play("jump");
            m_RigidBody.AddForce(Vector3.up * m_JumpForce);
        }*/
    }
    private float Remap(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }

    private void OnTriggerEnter(Collider other)
    {
        ParticleSystem.MainModule ps = ((ParticleSystem)this.GetComponent<ParticleSystem>()).main;
        switch (other.tag)
        {
            case "red":
                ps.startColor = Color.red;
                break;
            case "green":
                ps.startColor = Color.green;
                break;
            case "blue":
                ps.startColor = Color.blue;
                break;

        }
    }
}
