using UnityEngine;
public class DeathZone : MonoBehaviour
{
    public void OnTriggerEnter2D (Collider2D collider)
    {
        if (collider.transform.tag == CONSTSTRINGS.Player)
            GameManager.Instance.GameOver();
    }
}
