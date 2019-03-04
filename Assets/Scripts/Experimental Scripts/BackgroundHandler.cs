using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundHandler : MonoBehaviour {

    public GameObject cameraObject;
    public bool ismanager;
    public bool isbackground;
    public float transition;
    public GameObject[] Backgrounds;

    private Animation anim;

	void Start () {
        

    }

    // Update is called once per frame
    void Update()
    {
        if (ismanager)
        {
            this.transform.position = new Vector3(cameraObject.transform.position.x, cameraObject.transform.position.y, transform.position.z);
        }
        for(int i=0; i < Backgrounds.Length; i++)
        {
            anim = Backgrounds[i].GetComponent<Animation>();
            if(this.transform.position.y > transition * (i + 1)&& Backgrounds[i].GetComponent<SpriteRenderer>().color.a > 0)
            {
                if (!anim.isPlaying)
                {
                    anim.Play();
                }
            }
        }
    }
}
