using System.Runtime;
using UnityEditor;
// this script handles the GUI tickbox for interactables to apply changes to Gameobjects without writing scripts 
[CustomEditor(typeof(Interactable), true)]
public class InteractableEditor : Editor
{
    public override void OnInspectorGUI() 
    {
        Interactable interactable = (Interactable)target;
        if (target.GetType() == typeof(EventOnlyInteractable))
        {
            interactable.promptMessage = EditorGUILayout.TextField("Promp Message", interactable.promptMessage);
            EditorGUILayout.HelpBox("EventOnlyInteract can only use UnityEvents", MessageType.Info);
            if (interactable.GetComponent<InteractionEvent>() == null)
            {
                interactable.useEvents = true;
                interactable.gameObject.AddComponent<InteractionEvent>();
            }
        }else {
        base.OnInspectorGUI();
        if (interactable.useEvents)
        {
            if (interactable.GetComponent<InteractionEvent>() == null) 
            {
                interactable.gameObject.AddComponent<InteractionEvent>();
            }
        }
        else {
            if(interactable.GetComponent<InteractionEvent>() != null) {
                DestroyImmediate(interactable.GetComponent<InteractionEvent>());
            }
        }
        }
        
    }
}
