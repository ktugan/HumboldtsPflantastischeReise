using UnityEngine;

namespace Assets.Scripts.Dialog
{
    public class ContinueDialog : MonoBehaviour {

        private void OnMouseUp()
        {
            TextOverlayManager.instance.OnMouseUp();
        }
    }
}
