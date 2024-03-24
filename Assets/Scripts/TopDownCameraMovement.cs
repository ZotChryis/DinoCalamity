using UnityEngine;

/// <summary>
/// This class provides basic WASD movement for the camera that is supplied.
/// Scroll wheel will zoom to a maximum and minimum.
/// The movement assumes the camera is built to be from the top-down perspective.
/// </summary>
public class TopDownCameraMovement : MonoBehaviour
{
    [SerializeField] private Camera m_camera;

    //  todo: Do we need a camera settings scriptable object?
    [SerializeField] private float m_speed = 5f;
    [SerializeField] private float m_zoomSpeed = 20f;

    private Vector3 m_direction = new Vector3(0, 0, 0);
    private float m_currentZoom = 0f;
    
    private void Update()
    {
        m_direction.x = 0;
        m_direction.y = 0;
        m_direction.z = 0;
        
        if (Input.GetKey(KeyCode.W))
        {
            m_direction += Vector3.forward * Time.deltaTime * m_speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            m_direction += Vector3.back * Time.deltaTime * m_speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            m_direction += Vector3.right * Time.deltaTime * m_speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            m_direction += Vector3.left * Time.deltaTime * m_speed;
        }
        
        m_direction += Vector3.down * Input.mouseScrollDelta.y * Time.deltaTime * m_zoomSpeed;
        
        m_camera.gameObject.transform.position += m_direction;
    }
}
