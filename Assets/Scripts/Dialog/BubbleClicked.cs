using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Dialog
{
    public class BubbleClicked : MonoBehaviour
    {
        public UnityEvent AfterDialogue;
        private void OnMouseUp()
        {
            GameObject characterGameObject = transform.parent.parent.gameObject;
            TextOverlayManager.instance.StartDialogue(characterGameObject.name, AfterDialogue);
        }
    }
}
