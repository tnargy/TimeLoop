using Cinemachine;
using UnityEngine;

public class Cam : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CinemachineCore.GetInputAxis = GetAxisCustom;
    }

    public float GetAxisCustom(string axisName)
    {
        if (axisName == "Mouse X")
        {
            if (Input.GetMouseButton(1))
            {
                return Input.GetAxis("Mouse X");
            }
            else
            {
                return 0;
            }
        }

        if (axisName == "Mouse Y")
        {
            if (Input.GetMouseButton(1))
            {
                return Input.GetAxis("Mouse Y");
            }
            else
            {
                return 0;
            }
        }

        return Input.GetAxis(axisName);
    }
}
