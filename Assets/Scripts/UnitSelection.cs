using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelection : MonoBehaviour {

    public RectTransform selectionBox;
    public LayerMask unitLayerMask;
    private List<Unit> selectedUnits = new List<Unit>();
    private Vector2 startPos;

    // Components
    private Camera cam;
    private Player player;

    void Awake() {
        cam = Camera.main;
        player = GetComponent<Player>();
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

        }

    }

    void TrySelect(Vector2 screenPos) {
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
    void UpdateSelectionBox(Vector2 currentMousePosition) {
        if (!selectionBox.gameObject.activeInHierarchy) {
            selectionBox.gameObject.SetActive(true);
        }

        float width = currentMousePosition.x - startPos.x;
        float height = currentMousePosition.y - startPos.y;

        selectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
        selectionBox.anchoredPosition = startPos + new Vector2(width / 2, height / 2);
    }

    void ToggleSelectionVisual(bool selected) {
        foreach (Unit unit in selectedUnits) {
            unit.ToggleSelectionVisual(selected);
        }
    }
}
