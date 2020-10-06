using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    [SerializeField] GameObject hook;
    [SerializeField] GameObject hookHolder;
    [SerializeField] GameObject player;
    public GameObject hookedObj;

    Vector3 startPosition = Vector3.zero; //Position of player when grappling onto Object

    public float hookTravelSpeed;
    public float playerTravelSpeed;

    public static bool fired;
    public bool hooked;
    public bool metDist = false;

    public float maxDistance;
    private float currentDistance;

    void Update()
    {
        GrappleHook();
    }

    private void GrappleHook()
    {
        if (Input.GetButtonDown("Fire2") && fired == false)
        {
            fired = true;
        }

        if (fired == true && hooked == false)
        {
            hook.transform.Translate(Vector3.forward * hookTravelSpeed * Time.deltaTime);
            currentDistance = Vector3.Distance(transform.position, hook.transform.position);

            if (currentDistance >= maxDistance)
            {
                ReturnHook();
            }
        }

        if (hooked == true)
        {
            hook.transform.parent = hookedObj.transform;
            transform.position = Vector3.MoveTowards(transform.position, hook.transform.position, playerTravelSpeed * Time.deltaTime);
            float distToHook = Vector3.Distance(transform.position, hook.transform.position);

            this.GetComponent<Rigidbody>().useGravity = false;

            if (distToHook < 2)
            {
                ReturnHook();
                hook.transform.parent = hookHolder.transform;
                this.GetComponent<Rigidbody>().useGravity = true;
            }
            else
            {
                
            }
        }
    }

    void ReturnHook()
    {
        hook.transform.position = hookHolder.transform.position;
        fired = false;
        hooked = false;
    }
}
