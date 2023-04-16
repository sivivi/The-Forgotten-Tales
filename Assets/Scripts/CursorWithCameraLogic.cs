using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorWithCameraLogic : MonoBehaviour
{
    [SerializeField] private bool lockCursor = true;

    void Update() {
        if (lockCursor) {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
