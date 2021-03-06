using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour {

    public GameObject selectionVisual;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start() {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update() {

    }

    public void ToggleSelectionVisual(bool selected) {
        selectionVisual.gameObject.SetActive(selected);
    }

    public void SetDestination(Vector3 point) {
        agent.SetDestination(point);
    }
}
