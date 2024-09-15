using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform eyePos;
    [SerializeField] private float interpolation;
    
    
    public float Sensitivity 
    {
        get { return sensitivity; }
        set { sensitivity = value; }
    }
    [Range(0.1f, 9f)][SerializeField] float sensitivity = 2f;
    [Tooltip("Limits vertical camera rotation. Prevents the flipping that happens when rotation goes above 90.")]
    [Range(0f, 90f)][SerializeField] float yRotationLimit = 88f;

    Vector2 rotation = Vector2.zero;
    const string xAxis = "Mouse X"; //Strings in direct code generate garbage, storing and re-using them creates no garbage
    const string yAxis = "Mouse Y";
    
    private void Start()
    {
        
    }
    
    private void HandleCamera()
    {
        rotation.x += Input.GetAxis(xAxis) * sensitivity;
        rotation.y += Input.GetAxis(yAxis) * sensitivity;
        rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit);
        var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
        var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);

        transform.localRotation = xQuat * yQuat;
    }
    
    
    private void LateUpdate()
    {
        HandleCamera();
        transform.position = Vector3.Lerp(transform.position, eyePos.position, interpolation * Time.deltaTime);
    }
}
