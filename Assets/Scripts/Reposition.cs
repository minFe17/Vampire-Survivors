using UnityEngine;
using Utils;

public class Reposition : MonoBehaviour
{
    [SerializeField] float _xSize;
    [SerializeField] float _ySize;

    void RepositionTile()
    {
        Vector3 playerPos = SimpleSingleton<GameManager>.Instance.Player.transform.position;
        Vector3 tilePos = transform.position;

        float diffX = playerPos.x - tilePos.x;
        float diffY = playerPos.y - tilePos.y;

        if (Mathf.Abs(diffX) > _xSize)
        {
            float directionX = diffX < 0 ? -1 : 1;
            transform.Translate(Vector3.right * directionX * _xSize * 2);
        }

        if (Mathf.Abs(diffY) > _ySize)
        {
            float directionY = diffY < 0 ? -1 : 1;
            transform.Translate(Vector3.up * directionY * _ySize * 2);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Area"))
            RepositionTile();
    }
}