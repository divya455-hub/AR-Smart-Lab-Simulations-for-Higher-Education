using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLook : MonoBehaviour
{
    public GameObject target;

    void Start()
    {
        if (target == null) 
		{
			target = this.gameObject;
			Debug.Log ("ChangeLookAt target not specified. Defaulting to parent GameObject");
		}
        
    }

    void OnMouseDown () {

        Camera.main.fieldOfView = Mathf.Clamp(60 * target.transform.localScale.x, 1, 100);
	}
}
