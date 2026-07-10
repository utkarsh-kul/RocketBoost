using UnityEngine;
using UnityEngine.UIElements;

public class Oscillatory : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    [SerializeField] float speed;

    Vector3 startingPos;
    Vector3 endingPos;
    float movementFactor;

    private void Start()
    {
        startingPos = transform.position;
        endingPos = startingPos + movementVector;
    }

    private void Update()
    {
        movementFactor = Mathf.PingPong(Time.time * speed, 1);
        transform.position = Vector3.Lerp(startingPos, endingPos, movementFactor);
    }
}
