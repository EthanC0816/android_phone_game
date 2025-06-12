using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    [SerializeField] private float grappleLength;
    [SerializeField] private LayerMask grappleLayer;
    [SerializeField] private LineRenderer rope;

    private Vector3 grapplePoint;
    private DistanceJoint2D joint;

    private void Start()
    {
        joint = gameObject.GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        rope.enabled = false;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            RaycastHit2D hit = Physics2D.Raycast(
            origin: Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10f)),
            direction: Vector2.zero,
            distance: Mathf.Infinity,
            layerMask: grappleLayer);

            if (touch.phase == TouchPhase.Began)

                if (hit.collider != null)
                {
                    grapplePoint = hit.point;
                    grapplePoint.z = 0;
                    joint.connectedAnchor = grapplePoint;
                    joint.enabled = true;
                    joint.distance = grappleLength;
                    rope.SetPosition(0, grapplePoint);
                    rope.SetPosition(1, transform.position);
                    rope.enabled = true; 
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    joint.enabled = false;
                    rope.enabled = false;
                }

        }

        if(rope.enabled == true)
        {
            rope.SetPosition(1,transform.position);
        }

    }
}