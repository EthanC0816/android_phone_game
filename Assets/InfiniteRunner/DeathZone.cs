using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DeathZone : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(0);


        }
    }
}
