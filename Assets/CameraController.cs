using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField]
    private float CameraSpeed = 1;
    [SerializeField]
    private float CameraZoom = 1;

    [SerializeField]
    private Vector2 min_max_delta_zoom = new Vector2(-3, 3);

    private float zoom_amount = 0;

    void Start()
    {

    }

    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 scroll_input = Input.mouseScrollDelta;
        //float rotation = (Input.GetKey(KeyCode.E) == true ? 1 : 0) - (Input.GetKey(KeyCode.Q) == true ? 1 : 0);

        Vector3 world_deltaPos = new Vector3(input.x * CameraSpeed, 0, input.y * CameraSpeed) * Time.deltaTime;
        Vector3 local_deltaPos = new Vector3(0, 0, scroll_input.y * CameraZoom) * Time.deltaTime;
        local_deltaPos = ClampZoom(local_deltaPos); //TODO Make nice with Mathf.Clamp





        transform.Translate(world_deltaPos, Space.World);
        transform.Translate(local_deltaPos, Space.Self);
        //transform.Rotate(Vector3.up, rotation, Space.World);
    }

    private Vector3 ClampZoom(Vector3 local_delta)
    {
        if (zoom_amount + local_delta.z > min_max_delta_zoom.y)
        {
            local_delta = Vector3.zero;
        }
        else if (zoom_amount + local_delta.z < min_max_delta_zoom.x)
        {
            local_delta = Vector3.zero;
        }
        else
        {
            zoom_amount += local_delta.z;
        }
        return local_delta;
    }


}