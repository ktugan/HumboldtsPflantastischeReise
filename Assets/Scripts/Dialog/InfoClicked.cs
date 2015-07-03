using UnityEngine;

namespace Assets.Scripts.Dialog
{
    public class InfoClicked : MonoBehaviour
    {

        private void OnMouseUp()
        {
            GameObject characterGameObject = transform.parent.gameObject;
            TextOverlayManager.instance.StartDialogue(characterGameObject.name + "Info");
        }
    }
}
