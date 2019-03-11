using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabableSkinRadomizer : MonoBehaviour
{
    public Sprite[] skins;
    void Start()
    {
        this.GetComponent<SpriteRenderer>().sprite = skins[Random.Range(0, skins.Length)]; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
