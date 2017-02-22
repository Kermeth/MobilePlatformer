using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraForRob : MonoBehaviour {

    public GameObject pared;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 posicionVertical = new Vector3(0,1,0); // la posicion de la camara vertical
        Vector3 posicionNormal = new Vector3(1, 2, 3); // la posicion de la camara normal
        float distance = pared.transform.position.z - this.transform.position.z; //obtenemos la distancia
        float position = Mathf.Clamp(distance, 0, 10); 
        Vector3 targetPosition = Vector3.zero;
        if (position >= 10) { // si la distancia es mayor o igual a 10
            targetPosition = posicionNormal; // asignamos la posicion normal como target
        } else {
            targetPosition = posicionVertical; //sino asignamos la posicion vertical
        }
        this.transform.position=Vector3.Slerp(this.transform.position, targetPosition, Time.deltaTime);
        //interpolamos la posicion actual hasta adquirir la que queremos
	}
}
