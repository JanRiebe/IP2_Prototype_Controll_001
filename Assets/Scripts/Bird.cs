using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public bool flying;
    public float speed;


    private void Start()
    {
        if(flying)
            GetComponent<Animator>().SetTrigger("fly");
    }

    private void Update()
    {
        if (flying)
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);

        if (transform.position.x > 15 || transform.position.x < -15)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GetComponent<Animator>().SetTrigger("fly");
            flying = true;
        }
    }
}
