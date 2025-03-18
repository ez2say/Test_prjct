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

    public bool IsDropItemPressed()
    {
        return Input.GetMouseButtonDown(1);
    }

    public bool IsMouseScrollUp()
    {
        return Input.GetAxis("Mouse ScrollWheel") > 0;
    }

    public bool IsMouseScrollDown()
    {
        return Input.GetAxis("Mouse ScrollWheel") < 0;
    }

    public int GetNumberKeyPressed()
    {
        for (int i = 0; i < 5; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                return i;
            }
        }
        return -1;
    }
}