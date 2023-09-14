using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody m_Rigidbody;

    AudioSource audioSource;

    public AudioClip bounce;

    

    

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        GameManager.Instance.maxSpeed = 3.0f;
        audioSource = GetComponent<AudioSource>();
    }
    
    private void OnCollisionExit(Collision other)
    {
        var velocity = m_Rigidbody.velocity;
        
        //after a collision we accelerate a bit
        velocity += velocity.normalized * 0.01f;
        
        //check if we are not going totally vertically as this would lead to being stuck, we add a little vertical force
        if (Vector3.Dot(velocity.normalized, Vector3.up) < 0.1f)
        {
            velocity += velocity.y > 0 ? Vector3.up * 0.5f : Vector3.down * 0.5f;
        }

        //max velocity
        if (velocity.magnitude > GameManager.Instance.maxSpeed)
        {
            velocity = velocity.normalized * GameManager.Instance.maxSpeed;
        }

        m_Rigidbody.velocity = velocity;
        if(other.gameObject.tag != "Brick")
        {
        audioSource.PlayOneShot(bounce, 1.0f);
        }
    }
}
