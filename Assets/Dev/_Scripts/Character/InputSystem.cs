using UnityEngine;

public class InputSystem
{
    public Vector3 GetMovementInput()
    {
        return new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    public float GetMouseX()
    {
        return Input.GetAxis("Mouse X");
    }

    public float GetMouseY()
    {
        return Input.GetAxis("Mouse Y");
    }

    public bool IsInteractPressed()
    {
        return Input.GetKeyDown(KeyCode.E);
    }

    public bool IsNextItemPressed()
    {
        return Input.GetKeyDown(KeyCode.Q);
    }

    public bool IsDropItemPressed()
    {
        return Input.GetMouseButtonDown(1);
    }
}