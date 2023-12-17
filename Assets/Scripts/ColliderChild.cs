using UnityEngine;

public class ColliderChild : MonoBehaviour
{
    [SerializeField] private LayerMask layer;
    [SerializeField] private GameObject effect;

    private Cell _cell;

    private void Awake()
    {
        _cell = transform.parent.GetComponent<Cell>();
    }

    private void OnTriggerEnter(Collider other)
    {
        int foundLayer = other.gameObject.layer;

        if (((1 << foundLayer) & layer) != 0)
        {
            _cell.OnChildTriggerEnter(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        int foundLayer = other.gameObject.layer;

        if (((1 << foundLayer) & layer) != 0)
        {
            _cell.OnChildTriggerExit(other);
        }
    }

    public void CheckForConnection()
    {
        Vector3 endPoint = transform.position + transform.forward * 1f;
        bool connectionFound = Physics.Raycast(transform.position, endPoint, 1f);
        //effect.SetActive(!connectionFound);
        print(gameObject.name + " CheckForConnection: "+ connectionFound);
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 endPoint = transform.position + transform.forward * 1f;

        Gizmos.DrawLine(transform.position, endPoint);
    }
}