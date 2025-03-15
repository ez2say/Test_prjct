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
}