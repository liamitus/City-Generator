using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitSelectionManager : MonoBehaviour {

    public RectTransform selectionBox;
    public LayerMask unitLayerMask;
    private List<Unit> selectedUnits;
    private Vector2 startPos;

    // Components
    private Camera cam;
    private Player player;

    void Awake() {
        cam = Camera.main;
        player = GetComponent<Player>();
        selectedUnits = new List<Unit>();
    }

    // Update is called once per frame
    void Update() {
        // Left mouse button click
        if (Input.GetMouseButtonDown(0)) {
            ToggleSelectionVisual(false);
            selectedUnits = new List<Unit>();

            TrySelect(Input.mousePosition);
            startPos = Input.mousePosition;
        }

        // Left mouse button held down
        if (Input.GetMouseButton(0)) {
            UpdateSelectionBox(Input.mousePosition);
        }

        // Left mouse button release
        if (Input.GetMouseButtonUp(0)) {
            ReleaseSelectionBox();
        }

        // Right mouse button click
        if (Input.GetMouseButtonDown(1)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                foreach (Unit unit in selectedUnits) {
                    unit.SetDestination(hit.point);

                }
            }
        }

    }

    private void TrySelect(Vector2 screenPos) {
        Ray ray = cam.ScreenPointToRay(screenPos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, unitLayerMask)) {
            Unit unit = hit.collider.GetComponent<Unit>();

            if (player.IsMyUnit(unit)) {
                selectedUnits.Add(unit);
                unit.ToggleSelectionVisual(true);
            }
        }
    }

    // Called when we are creating a selection box
    private void UpdateSelectionBox(Vector2 currentMousePosition) {
        if (!selectionBox.gameObject.activeInHierarchy) {
            selectionBox.gameObject.SetActive(true);
        }

        float width = currentMousePosition.x - startPos.x;
        float height = currentMousePosition.y - startPos.y;

        selectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
        selectionBox.anchoredPosition = startPos + new Vector2(width / 2, height / 2);
    }

    // Called when we release the selection box
    private void ReleaseSelectionBox() {
        selectionBox.gameObject.SetActive(false);

        Vector2 min = selectionBox.anchoredPosition - (selectionBox.sizeDelta / 2);
        Vector2 max = selectionBox.anchoredPosition + (selectionBox.sizeDelta / 2);

        foreach (Unit unit in player.units) {
            Vector3 screenPosition = cam.WorldToScreenPoint(unit.transform.position);
            if (
                screenPosition.x > min.x && screenPosition.x < max.x
                && screenPosition.y > min.y && screenPosition.y < max.y
            ) {
                selectedUnits.Add(unit);
                unit.ToggleSelectionVisual(true);
            }
        }
    }

    private void ToggleSelectionVisual(bool selected) {
        foreach (Unit unit in selectedUnits) {
            unit.ToggleSelectionVisual(selected);
        }
    }
}
