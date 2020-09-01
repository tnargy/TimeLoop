using UnityEngine;

public class Ball : MonoBehaviour
{
    private bool isGrounded() => transform.position.y <= 0.2f;
    private float kickForce = 10;

    private void OnCollisionEnter(Collision other)
    {
        Vector3 direction = (other.transform.position - transform.position).normalized;
        if (isGrounded() && other.transform.CompareTag("Player"))
        {
            GetComponent<Rigidbody>().AddForce(-direction * kickForce, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 direction = (other.transform.position - transform.position).normalized;
        if (other.transform.CompareTag("Ghost"))
        {
            if (isGrounded())
                GetComponent<Rigidbody>().AddForce(-direction * kickForce, ForceMode.Impulse);
            else
                GetComponent<Rigidbody>().AddForce(-direction * kickForce / 2, ForceMode.Impulse);
        }
    }

    public static void Reset()
    {
        var ball = GameObject.FindGameObjectWithTag("Ball");
        ball.transform.position = new Vector3(0, 0.15f, 0);
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
}
