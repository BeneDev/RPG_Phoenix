using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraRaycaster))]

public class CursorAffordance : MonoBehaviour {

    [SerializeField] Texture2D standardCursor = null;
    [SerializeField] Texture2D attackCursor = null;
    private CameraRaycaster raycaster;
    [SerializeField] Vector2 cursorHotspot = new Vector2(0f, 0f);
    [SerializeField] const int walkableLayerNumber = 8;
    [SerializeField] const int enemyLayerNumber = 9;

	void Start () {
        raycaster = GetComponent<CameraRaycaster>();
        raycaster.notifyLayerChangeObservers += OnLayerChangeEnter;
	}

    void OnLayerChangeEnter(int newLayer)
    {
        switch (newLayer)
        {
            default:
                Debug.Log("Don't know what cursor to show");
                return;

            case walkableLayerNumber:
                Cursor.SetCursor(standardCursor, cursorHotspot, CursorMode.Auto);
                print("Cursor set to standard");
                break;

            case enemyLayerNumber:
                Cursor.SetCursor(attackCursor, cursorHotspot, CursorMode.Auto);
                print("Cursor set to Attack");
                break;
        }
    }
}
