using UnityEngine;

public class WinZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ��Ǩ�ͺ����ѵ�ط�誹��ͼ�����
        if (collision.CompareTag("Player"))
        {
            // ���¡�˵ء�ó쪹���
            GameManager.Instance.PlayerWin();
        }
    }
}