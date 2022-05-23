using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public void OnTriggerEnter2D (Collider2D collider)
    {
        if (collider.transform.tag == "Player")
            GameManager.instance.GameOver();
    }
}
