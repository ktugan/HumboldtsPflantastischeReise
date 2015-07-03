using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Dialog
{
    public abstract class TextOverlayBase : UnitySingleton<TextOverlayBase>
    {
        public static TextOverlayBase Instance { get; private set; }
        public Dictionary<string, List<OverlayText>> Texts = new Dictionary<string, List<OverlayText>>();

        protected TextOverlayBase()
        {
            Instance = this;
        }


        public class OverlayTextItem
        {
            public string Talker { get; set; }
            public string Text { get; set; }

            public string Sound { get; set; }
            //public Image Image { get; protected set; }
            //public Event DialogEvent { get; protected set; }

            //public bool IsText => Text != "" || Text != null;
            //public bool IsImage => Image != null;
        }

        public class OverlayText
        {
            public OverlayTextItem[] OverlayTextItems { get; set; }
            public Color OverlayColor { get; set; }
            public string[] PreStates { get; set; }
            public string[] PostRemoveStates { get; set; }
            public string[] PostAddStates { get; set; }

            public OverlayText()
            {
                OverlayColor = new Color(0.662f, 0.229f, 0.229f, 0.584f);
                PreStates = new string[0];
                PostRemoveStates = new string[0];
                PostAddStates = new string[0];
            }
        }

        public OverlayText GetDialog(string sender)
        {
            List<OverlayText> texts = Texts[sender];
            foreach (var text in texts)
            {
                var match = StateMachine.instance.ContainsAll(text.PreStates);
                if (match)
                {
                    return text;
                }
            }

            return null;
        }


    }
}
