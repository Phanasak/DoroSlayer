using UnityEngine;

public class LevelExit : MonoBehaviour
{
    [SerializeField] private string nextSceneName;
    private bool isExiting = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isExiting)
        {
            isExiting = true;
            SceneTransition.Instance.LoadScene(nextSceneName);
        }
    }
}