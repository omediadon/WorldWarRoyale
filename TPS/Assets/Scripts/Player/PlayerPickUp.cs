using Framework;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerPickUp : MonoBehaviour
{
    private RaycastHit _closestHit;


    [SerializeField] private int numberOfRays = 8;

    [FormerlySerializedAs("coverMask")] [SerializeField]
    private LayerMask itemLayerMask;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            FindItemToPickUp();
        }
    }

    private void FindItemToPickUp()
    {
        _closestHit = new RaycastHit();
        float angleStep = 360 / numberOfRays;
        for(int i = 0; i < numberOfRays; i++) {
            Quaternion angle = Quaternion.AngleAxis(i * angleStep, transform.up);
            CheckClosestPoint(angle);
        }
        Debug.DrawLine(transform.position + Vector3.up * .3f, _closestHit.point, Color.blue, 2);
    }

    private void CheckClosestPoint(Quaternion angle)
    {
        Debug.DrawRay(transform.position + Vector3.up * .3f, angle * Vector3.forward * 5);

        if (!Physics.Raycast(transform.position + Vector3.up * .3f, angle * Vector3.forward, out var hit, 5,
                itemLayerMask)) return;
        if (_closestHit.distance == 0 || _closestHit.distance > hit.distance)
        {
            _closestHit = hit;
        }

        Debug.DrawLine(transform.position + Vector3.up * .3f, hit.point, Color.magenta, 1);
    }
}