using System.Collections.Generic;

namespace Assets.Scripts.Dialog
{
    class LevelTwoTexts : TextOverlayBase
    {
        #region indianerjunge
        private void InitKindDialogs()
        {
            Texts["Indianerjunge"] = new List<OverlayText>
            {
                new OverlayText()
                {
                    OverlayTextItems = new[]
                    {
                        new OverlayTextItem
                        {
                            Talker = "Indianerjunge",
                            Text = "Oh, hallo! Sucht ihr auch nach der mysteriösen Höhle?",
                            Sound = "Audio/indianerjunge"
                        },
                        new OverlayTextItem
                        {
                            Talker = "Humboldt",
                            Text = "Eine mysteriöse Höhle?"
                        },
                        new OverlayTextItem
                        {
                            Talker = "Indianerjunge",
                            Text = "Ja, jeder sucht sie."
                        },
                        new OverlayTextItem
                        {
                            Talker = "Bonpland",
                            Text = "Die Höhle ist von Menschen unberührt? Dann gibt es dort bestimmt unentdeckte Pflanzen!"
                        },
                    }
                },
            };

        }
        #endregion

        #region
        private void InitBewohnerOneDialog()
        {
            Texts["BewohnerOne"] = new List<OverlayText>
            {
                new OverlayText()
                {
                    OverlayTextItems = new[]
                    {
                        new OverlayTextItem
                        {
                            Talker = "Dorfbewohner",
                            Text = "Die Wachen halten Sklaven gefangen. Wie kann man nur so etwas machen."
                        },
                        new OverlayTextItem
                        {
                            Talker = "Humboldt",
                            Text = "Wo werden die Sklaven festgehalten?"
                        },
                        new OverlayTextItem
                        {
                            Talker = "Dorfbewohner",
                            Text = "In der Garnison außerhalb des Dorfes."
                        },
                    }
                }
            };
        }
        #endregion

        #region
        private void InitBewohnerTwoDialog()
        {
            Texts["BewohnerTwo"] = new List<OverlayText>
            {
                new OverlayText()
                {
                    OverlayTextItems = new[]
                    {
                        new OverlayTextItem
                        {
                            Talker = "Dorfbewohner",
                            Text = "Unser Dorf-Schamane ist sehr mächtig. Er kann alles regnen lassen."
                        },
                    }
                }
            };
        }
        #endregion

        #region
        private void InitIndianerGirlDialog()
        {
            StateMachine.instance.Add("girl");
            Texts["Indianermedchen"] = new List<OverlayText>
            {
                new OverlayText()
                {
                    PreStates = new [] {"girl"},
                    PostAddStates = new[] {"mitfeder"},
                    PostRemoveStates = new [] {"girl"},

                    OverlayTextItems = new[]
                    {
                        new OverlayTextItem
                        {
                            Talker = "Indianermädchen",
                            Text = "Oh hallo Fremder, wer bist du denn?",
                            Sound = "Audio/indianergirl1"
                        },
                        new OverlayTextItem
                        {
                            Talker = "Humboldt",
                            Text = "Meinst du mich?"
                        },
                        new OverlayTextItem
                        {
                            Talker = "Indianermädchen",
                            Text = "Nein, du Dummerchen. Aber deinen Freund würde ich gern gegen eine Feder tauschen.",
                            
                        },
                        new OverlayTextItem
                        {
                            Talker = "Indianermädchen",
                            Text = " Du kriegst ihn auch später zurück",
                            Sound = "Audio/indianergirl4"
                            // Sound = Audio/*lachen*
                        },
                    }
                }
            };
        }
        #endregion

        public void Start()
        {
            InitKindDialogs();
            InitBewohnerOneDialog();
            InitBewohnerTwoDialog();
            InitIndianerGirlDialog();
            // InitInfodings();
        }
    }
}