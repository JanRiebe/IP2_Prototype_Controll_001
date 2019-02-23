using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JansTestInput : MonoBehaviour
{

	public float aimSpeed;

	public Hand[] hands;

	float aimingAngle = -90;




	

    // Start is called before the first frame update
    void Start()
    {
        
    }






    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
            hands[0].ActivateControl();
        if (Input.GetKey(KeyCode.DownArrow))
            hands[1].ActivateControl();
        if (Input.GetKey(KeyCode.LeftArrow))
            hands[2].ActivateControl();
        if (Input.GetKey(KeyCode.RightArrow))
            hands[3].ActivateControl();
		
		if (Input.GetKeyUp(KeyCode.UpArrow))
		{
            hands[0].DeactivateControl();
			hands[0].Fire(aimingAngle);
		}
        if (Input.GetKeyUp(KeyCode.DownArrow))
		{
            hands[1].DeactivateControl();
			hands[1].Fire(aimingAngle);
		}
        if (Input.GetKeyUp(KeyCode.LeftArrow))
		{
            hands[2].DeactivateControl();
			hands[2].Fire(aimingAngle);
		}
        if (Input.GetKeyUp(KeyCode.RightArrow))
		{
            hands[3].DeactivateControl();
			hands[3].Fire(aimingAngle);
		}
			
        if (Input.GetKey(KeyCode.A))
            aimingAngle -= aimSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
            aimingAngle += aimSpeed * Time.deltaTime;

		Debug.DrawLine(Vector3.zero, new Vector2(Mathf.Cos(aimingAngle), Mathf.Sin(aimingAngle)));
    }
}
