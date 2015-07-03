using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.Dialog
{
    public class TextOverlayManager : UnitySingleton<TextOverlayManager>
    {

        public Text TalkManText;
        public Text DialogBox;
        public Canvas DialogCanvas;
        public AudioSource AudioSource;
        public Image Background;
        private UnityEvent _afterDialogueEvent;

        public void OnMouseUp()
        {
            ContinueDialog();
        }
        public void BlockInput(bool block)
        {

			#if UNITY_ANDROID || UNITY_IOS || UNITY_WP8 || UNITY_WP8_1
            var control = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<Platform2DMobileControl>();
			control.enabled = !block;
			control.Stop();
			#endif
			#if UNITY_STANDALONE || UNITY_EDITOR || UNITY_WEBPLAYER
			GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<Platform2DDesktopControl>().enabled = !block;
			#endif

        }



        private TextOverlayBase.OverlayText _currentOverlayText;
        private int _counter;
        public void StartDialogue(string sender)
        {
            StartDialogue(sender, null);
        }

        public void StartDialogue(string sender, UnityEvent afterDialogueEvent)
        {
            _afterDialogueEvent = afterDialogueEvent;
            _currentOverlayText = TextOverlayBase.Instance.GetDialog(sender);
            DialogCanvas.gameObject.SetActive(true);
            BlockInput(true);
            _counter = 0;
            Background.color = _currentOverlayText.OverlayColor;

            ContinueDialog();
        }

        public void ContinueDialog()
        {
            var items = _currentOverlayText.OverlayTextItems;
            if (_counter == items.Length)
            {
                foreach (var state in _currentOverlayText.PostAddStates)
                {
                    StateMachine.instance.Add(state);
                }
                foreach (var state in _currentOverlayText.PostRemoveStates)
                {
                    StateMachine.instance.Remove(state);
                }

                DialogCanvas.gameObject.SetActive(false);
                BlockInput(false);
                if(_afterDialogueEvent != null)_afterDialogueEvent.Invoke();
                return;
            }

            TalkManText.text = items[_counter].Talker;
            DialogBox.text = items[_counter].Text;
            string soundPath = items[_counter].Sound;
            if (!String.IsNullOrEmpty(soundPath))
            {
                AudioClip clip = Resources.Load<AudioClip>(soundPath);
                AudioSource.PlayOneShot(clip);
            }

            _counter++;
        }

        
    }
}
