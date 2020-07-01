using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{

    public float speed;
    public float distance;
    private bool movingRight = true;
    public Transform groundDetection;

    [Header("Player Death Sound")]
    [SerializeField] private GameObject deathVFX;
    public float durationOfExplosion;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.7f;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // allows enemy to move back and forth - only works on platforms not connected
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        if (groundInfo.collider == false)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }

    // if collided with player, kill player and play SFX
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == ("Player"))
        {
            AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSoundVolume);
            FindObjectOfType<Level>().LoadEnd();
            Destroy(collision.gameObject);
        }
        
    }
}
