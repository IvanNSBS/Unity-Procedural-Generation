using UnityEngine;

namespace Player
{
    public class PlayerCamera : MonoBehaviour
    {
        #region Inspector Fields
        [SerializeField] private Camera m_camera;
        [SerializeField] private float cameraSensitivity = 90;
        [SerializeField] private float climbSpeed = 4;
        [SerializeField] private float normalMoveSpeed = 10;
        [SerializeField] private float slowMoveFactor = 0.25f;
        [SerializeField] private float fastMoveFactor = 3;
        #endregion Inspector Fields
        
        #region Fields
        private float rotationX = 0.0f;
        private float rotationY = 0.0f;
        #endregion Fields

        
        #region MonoBehaviour Methods
        void Update ()
        {
            rotationX += Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
            rotationY += Input.GetAxis("Mouse Y") * cameraSensitivity * Time.deltaTime;
            rotationY = Mathf.Clamp (rotationY, -90, 90);
 
            transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
            transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);
 
            if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift))
            {
                transform.position += transform.forward * (normalMoveSpeed * fastMoveFactor) * Input.GetAxis("Vertical") * Time.deltaTime;
                transform.position += transform.right * (normalMoveSpeed * fastMoveFactor) * Input.GetAxis("Horizontal") * Time.deltaTime;
            }
            else if (Input.GetKey (KeyCode.LeftControl) || Input.GetKey (KeyCode.RightControl))
            {
                transform.position += transform.forward * (normalMoveSpeed * slowMoveFactor) * Input.GetAxis("Vertical") * Time.deltaTime;
                transform.position += transform.right * (normalMoveSpeed * slowMoveFactor) * Input.GetAxis("Horizontal") * Time.deltaTime;
            }
            else
            {
                transform.position += transform.forward * normalMoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
                transform.position += transform.right * normalMoveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
            }
 
 
            if (Input.GetKey (KeyCode.Q)) {transform.position += transform.up * climbSpeed * Time.deltaTime;}
            if (Input.GetKey (KeyCode.E)) {transform.position -= transform.up * climbSpeed * Time.deltaTime;}
 
            if (Input.GetKeyDown (KeyCode.End))
            {
                Screen.lockCursor = (Screen.lockCursor == false) ? true : false;
            }
        }
        #endregion MonoBehaviour Methods
    }
}