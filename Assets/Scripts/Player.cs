using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Unit[] units;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public bool IsMyUnit(Unit unit) {
        foreach (Unit u in units) {
            if (u == unit) {
                return true;
            }
        }
        return false;
    }
}
