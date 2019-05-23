using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public Animator camAnim;
    public Transform cam;
    public Vector3 initialpos;

    public float shakeduration = 0f;

    private bool isShaking = false;

    public void CamShake()
    {
        camAnim.SetTrigger("Shake");
    }

    public void Start()
    {
        cam = GetComponent<Transform>();
        initialpos = cam.localPosition;
    }

    public void Update()
    {
        if (shakeduration > 0 && !isShaking)
        {
            StartCoroutine(ActiveShake());
        }
        
    }

    IEnumerator ActiveShake()
    {
        isShaking = true;

        var startTime = Time.realtimeSinceStartup;
        while(Time.realtimeSinceStartup < startTime + shakeduration)
        {
            var randomPoint = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), cam.position.z);
            cam.localPosition = randomPoint;
            yield return null;
        }

        shakeduration = 0f;
        cam.localPosition = initialpos;
        isShaking = false;
    }

    public void Shaker(float time)
    {
        if (time > 0)
        {

            shakeduration += time;
        }
       
    }

}
