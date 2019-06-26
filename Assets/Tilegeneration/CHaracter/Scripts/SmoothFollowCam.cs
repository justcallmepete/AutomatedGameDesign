using UnityEngine;
using System.Collections;

public class SmoothFollowCam: MonoBehaviour
{
    Transform target;
    public float distance = -3.0f;
    public float height = 0f;
    public float damping = 5.0f;
    public float rotationDamping = 10.0f;

    void Update()
    {
        if(!target)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
            if(target == null ){
                return;
            }
        }

        Vector3 wantedPosition;
        wantedPosition = target.TransformPoint(0, height, distance);

        transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * damping);
    }
    
}
