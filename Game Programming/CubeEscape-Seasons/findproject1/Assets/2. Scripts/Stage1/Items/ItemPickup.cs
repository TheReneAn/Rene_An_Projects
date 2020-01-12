using UnityEngine;

public class ItemPickup : Interactable {

    public Item item;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);

            if (hit.collider != null)
            {
                Interact();

                PickUp();
            }
        }
    }

    void PickUp()
    {
        Debug.Log("Picking up " + item.name);
        // Add to inventory
        bool wasPickeUp = Inventory.instance.Add(item);

        if(wasPickeUp)
            Destroy(gameObject);
    }

}
