using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationController : MonoBehaviour {
    public Transform destination;
    public NavMeshSurface surface;
    public GameObject ramp;
    public Camera cam;
    private NavMeshAgent agent;
    private bool isActive;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        isActive = true;
	}
	
    public IEnumerator RebuildNavmesh()
    {
        yield return null;
        surface.BuildNavMesh();
    }

	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            isActive = !isActive;
            ramp.SetActive(isActive);
            StartCoroutine(RebuildNavmesh());
        }

		if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                destination.position = hit.point + new Vector3(0,1,0);
                agent.SetDestination(destination.position);
            }
        }
	}
}
