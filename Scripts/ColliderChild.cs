using UnityEngine;

public class ColliderChild : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Conection"))
        {
            if (transform.parent.TryGetComponent(out Cell cell))
            {
                cell.OnChildTriggerEnter(other);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Conection"))
        {
            if (transform.parent.TryGetComponent(out Cell cell))
            {
                cell.OnChildTriggerExit(other);
            }
        }
    }
}