using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Dialog
{
    class LevelOneTexts : TextOverlayBase
    {
        //changed to protected to shut compiler up
        #region carldialogs

        private void InitCarlDialogs()
        {
            StateMachine.instance.Add("carl");
            Texts["Carl"] = new List<OverlayText>
            {
                new OverlayText()
                {
                    PreStates = new []{"carl"},
                    PostAddStates = new[] {"carl0"},
                    PostRemoveStates = new []{"carl"},
                    OverlayTextItems = new[]
                    {
                        new OverlayTextItem
                        {
                            Talker = "König Carl",
                            Text = "Hallo Humboldt! Ich habe schon eine Menge von dir gehört."
                        },
                        new OverlayTextItem
                        {
                            Talker = "Humboldt",
                            Text = "Eure Majestät, ich habe gehört, Sie suchen nach der seltenen Pflanze …"
                        },
                    }
                },
                //second, after flower
                new OverlayText()
                {
                    PreStates = new[] { "carl0", "plant1given" },
                    PostRemoveStates = new[] { "carl0", "withoutpaper"},
                    PostAddStates = new []{"withpaper"},
                    OverlayTextItems = new[]
                    {
                        new OverlayTextItem
                        {
                            Talker = "Humboldt",
                            Text = "Ich würde gerne in eurem Namen die Welt erkunden und seltene Pflanzen finden."
                        },
                        new OverlayTextItem
                        {
                            Talker = "König Carl",
                            Text = "Dann nimm diesen königlichen Reisepass. Er wird dir auf deiner weiteren Reise helfen.",
                            Sound = "Audio/aaah"
                        },
                        new OverlayTextItem {
                            Talker = "Humboldt", 
                            Text = "Oh tausend Dank, eure Majestät."
                        },
                    },
                },

                //last, nothing to say anymore
                new OverlayText()
                {
                    OverlayTextItems = new[]
                    {
                        new OverlayTextItem {Talker = "König Carl", Text = "Ich habe eine Menge zu tuen. Bitte lass mich alleine."},
                    }
                }
            };

        }
        #endregion

        #region

        private void InitCaptainDialog()
        {
            StateMachine.instance.Add("withoutpaper");
            Texts["Captain"] = new List<OverlayText>
            {
                new OverlayText()
                {
                    PreStates = new[] {"withoutpaper"},
                    OverlayTextItems = new[]
                    {
                        new OverlayTextItem
                        {
                            Talker = "Kapitän",
                            Text = "Arr, verschwindet! Wir legen gleich ab. Ohne Papiere keine Überfahrt!"
                        },
                    }
                },

                new OverlayText()
                {
                    PreStates = new[] {"withpaper"},
                    OverlayTextItems = new[]
                    {
                        //todo
                        new OverlayTextItem
                        {
                            Talker = "Kapitän",
                            Text = "Ooh. Willkommen auf der königlichen Korrrvette Pizarro...",
                            //Sound = "ueberascht"
                        },
                    }
                },
            };


            //withpaper


        }

        //        Kapitän:
        //*angry voice* 
        //“Arr, verschwindet! Wir legen gleich ab.”
        //Animation Reisepass
        //Kapitän:
        //*überrascht*
        //“Willkommen auf der königlichen Korrrvette Pizarro”
        #endregion


        #region info overlay texts
        private void InitInfodings()
        {

        }
        #endregion

        public void Start()
        {
            InitCarlDialogs();
            InitCaptainDialog();
            InitInfodings();
        }

    }
}
