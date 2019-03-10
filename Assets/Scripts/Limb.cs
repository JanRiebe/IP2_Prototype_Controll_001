using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Limb : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {		
		Initialise();
    }

	abstract protected void Initialise();

  
    



    public abstract void SwitchToIK(Limb sender);

    public abstract void SwitchToFK(Limb sender);


	

}
