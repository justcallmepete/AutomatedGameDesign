using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMaterialplayer : MonoBehaviour
{

    Material activeMat;
    Transform player;

	// Use this for initialization
	void Start () {
        activeMat = GetComponent<Renderer>().material;
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        Vector4 localPoint = transform.InverseTransformPoint(player.position);
        localPoint.Scale(transform.localScale);
        localPoint.w = 1;

        activeMat.SetVector("_PlayerPos", localPoint);
    }
}
