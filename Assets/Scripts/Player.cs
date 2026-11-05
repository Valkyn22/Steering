using UnityEngine;


public class Drive : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 720.0f;
    public float currentSpeed = 0;

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");   // A / D
        float v = Input.GetAxis("Vertical");     // W / S

        // Clamp so diagonals aren't faster than straight movement
        Vector3 input = Vector3.ClampMagnitude(new Vector3(h, 0, v), 1f);

        transform.Translate(input * speed * Time.deltaTime, Space.World);

        // Speed in units per second
        currentSpeed = input.magnitude * speed;

        // Face the movement direction
        if (input.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(input);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}

