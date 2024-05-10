using UnityEngine;

public class PersonScreen : MonoBehaviour
{
    Quaternion targetRotation;
    [SerializeField] Vector3 up;
    [SerializeField] Transform camera;
    [SerializeField] float rotSpeed = 1;
    [SerializeField] float sensX = 100;
    [SerializeField] float sensY = -100;
    float mouseX;
    float mouseY;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float deltaX = Input.GetAxis("Mouse X") * sensX * Time.deltaTime;
        mouseX += deltaX;
        mouseY += Input.GetAxis("Mouse Y") * sensY * Time.deltaTime;
        mouseY = Mathf.Clamp(mouseY, -90f, 90f);
        camera.localRotation = Quaternion.Euler(mouseY, 0, 0);

        Quaternion deltaRot = Quaternion.Euler(0, deltaX, 0);
        Quaternion absRot = Quaternion.Euler(0, mouseX, 0);

        targetRotation = Quaternion.FromToRotation(Vector3.up, up) * absRot;
        transform.rotation = Quaternion.RotateTowards(transform.rotation * deltaRot, targetRotation, rotSpeed);
    }
}
