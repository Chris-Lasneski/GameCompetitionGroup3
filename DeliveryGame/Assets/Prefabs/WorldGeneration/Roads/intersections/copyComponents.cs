using UnityEngine;

public class CopyComponentsExample : MonoBehaviour
{
    public GameObject sourcePrefab; // Prefab with components to be copied
    public GameObject targetPrefab; // Prefab to copy components to

    void Start()
    {
        // Get the components from the source prefab
        Component[] componentsToCopy = sourcePrefab.GetComponents<Component>();

        // Copy the components to the target prefab
        foreach (Component component in componentsToCopy)
        {
            Component copiedComponent = targetPrefab.AddComponent(component.GetType());
            UnityEditorInternal.ComponentUtility.CopyComponent(component);
            UnityEditorInternal.ComponentUtility.PasteComponentValues(copiedComponent);
        }
    }
}
