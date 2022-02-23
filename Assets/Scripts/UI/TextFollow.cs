using UnityEngine;
public class TextFollow : MonoBehaviour
{
    private void LateUpdate()
    {
        Vector3 camToLookAt = new Vector3(
            x: (Camera.main.transform.position.x * -1), y: 0, z: Camera.main.transform.position.z * -1);       

        transform.LookAt(camToLookAt, Vector3.up);
    }
}
