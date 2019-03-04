using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleScript : MonoBehaviour {

    public Sprite[] textures;
    private Sprite chosenTexture;
    public float probability;
    private float dice;
    public bool randomizeprobability;

    // Use this for initialization
    void Start()
    {
        chosenTexture = textures[Random.Range(0, textures.Length)];
        this.GetComponent<SpriteRenderer>().sprite = chosenTexture;
        if (randomizeprobability)
        {
            dice = Random.Range(0, 100);
            if (probability < dice)
            {
                this.gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
