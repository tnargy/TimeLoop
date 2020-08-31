using UnityEngine;

public class Ball : MonoBehaviour
{
    private float kickForce = 10;

    private void OnCollisionEnter(Collision other)
    {
        Vector3 direction = (other.transform.position - transform.position).normalized;
        if (other.transform.CompareTag("Player") || other.transform.CompareTag("Ghost"))
        {
            GetComponent<Rigidbody>().AddForce(-direction * kickForce, ForceMode.Impulse);
        }
    }
}
