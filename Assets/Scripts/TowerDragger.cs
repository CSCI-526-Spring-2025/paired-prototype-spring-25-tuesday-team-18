using UnityEngine;
using UnityEngine.EventSystems;  // Required for handling UI events

public class TowerDragger : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // Reference to the tower we want to build
    public GameObject towerPrefab;

    // Used to show semi-transparent preview while dragging
    private GameObject currentTowerPreview;

    // Called when player starts dragging from the UI button
    public void OnBeginDrag(PointerEventData eventData)
    {
        // Create a preview instance of the tower
        currentTowerPreview = Instantiate(towerPrefab);
        Destroy(currentTowerPreview.GetComponent<AutoAttack>());
        Destroy(currentTowerPreview.GetComponent<Health>());
        Destroy(currentTowerPreview.GetComponent<Rigidbody2D>());
        Destroy(currentTowerPreview.GetComponent<BoxCollider2D>());

        // Make the preview semi-transparent to distinguish it from actual towers
        SpriteRenderer renderer = currentTowerPreview.GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            Color color = renderer.color;
            color.a = 0.5f;  // Set to 50% opacity
            renderer.color = color;
        }
    }

    // Called every frame while dragging
    public void OnDrag(PointerEventData eventData)
    {
        if (currentTowerPreview != null)
        {
            // Convert screen position (mouse) to world position
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0;  // Ensure tower stays in 2D plane
            currentTowerPreview.transform.position = worldPos;
        }
    }

    // Called when player releases the mouse button
    public void OnEndDrag(PointerEventData eventData)
    {
        if (currentTowerPreview != null)
        {
            // Get the final position where player wants to place the tower
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0;

            // Create the actual tower at the chosen position
            Instantiate(towerPrefab, worldPos, Quaternion.identity);

            // Clean up by destroying the preview
            Destroy(currentTowerPreview);
        }
    }
}