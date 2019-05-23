using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabableSkinRadomizer : MonoBehaviour
{
    static float rotationMax = 45;

    public float probability = 100f;

    public Sprite[] skins;
    void Start()
    {
        this.GetComponent<SpriteRenderer>().sprite = skins[Random.Range(0, skins.Length)];
        transform.rotation = Quaternion.Euler(0,0,Random.value*rotationMax-rotationMax/2);
        float chance = Random.Range(0, 100);
        if (chance > probability)
        {
            this.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
