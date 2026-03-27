using UnityEngine;
using UnityEngine.Events;

public class DistanceCheckHandler : MonoBehaviour
{
    [SerializeField] private Transform point1;
    [SerializeField] private Transform point2;
    [SerializeField] private float distance = 3f;
    [SerializeField] private UnityEvent onNear;

    bool onNearAlready = false;
    void Update()
    {
        if (Vector3.Distance(point1.position, point2.position) < distance && !onNearAlready)
        {
            onNear?.Invoke();
            onNearAlready = true;
        }
    }
}
