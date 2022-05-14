using UnityEngine;

public class ObjectRotate : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(
            x: Random.Range(0, 30),
            y: Random.Range(0, 30),
            z: Random.Range(0, 30))*Random.Range(5,10) * Time.deltaTime);
    }
}
