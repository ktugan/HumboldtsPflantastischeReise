using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

namespace Easing8000{

	public class EasingAnimationWindow : EditorWindow
	{
		static private EasingAnimationWindow singleWindow;
		private static int windowWidth = 300;
		private int steps = 50;
		private float keyframeReductionTolerance = 0f;
		private float from;
		private float to = 10f;
		private float length = 3f;
		private string propertyName;
		private string AnimationName;
		private AnimationCurve curve;
		private List<string> properties = new List<string>();
		private List<string> typeNames = new List<string>();
		private List<Type> types = new List<Type>();
		private EasingTypes easeType = EasingTypes.Linear;
		private GameObject go = null;
		private GameObject oldgo = null;
		private Type type;
		private Animator anim = new Animator();
		private int propertiesindex;
		private int typeindex;

		[MenuItem ("Window/Create Easing Animation")]
		static void  ShowWindow () {
			EasingAnimationWindow window = EditorWindow.GetWindow<EasingAnimationWindow>(true ,"Easing", true);
			window.minSize = new Vector2(windowWidth, 50);
			window.maxSize = new Vector2(windowWidth, 400);
			window.Repaint();
		}
	
		void OnGUI () {
			//Header
			GUILayout.BeginArea(new Rect(7,0,this.position.width - 14,this.position.height));
			GUILayout.Space(5);
			GUILayout.Label("Create Animation with Easing", EditorStyles.boldLabel);
			go = (GameObject) EditorGUI.ObjectField(new Rect(0, 25 ,this.position.width -14, 17), "Gameobject:", go, typeof(GameObject), true);
			GUILayout.Space(20);
			EditorGUI.BeginChangeCheck();

			GUILayout.Space(5);
			GUILayout.Label("Name of the AnimationClip:", EditorStyles.label);
			AnimationName = EditorGUILayout.TextField(AnimationName, GUILayout.Height(20));
			GUILayout.Space(5);
			string path = getPath() + AnimationName + ".anim";
			if(File.Exists(path))
			{
				EditorGUILayout.HelpBox("Animation with that Name exists already! \n (Will be overwritten)", MessageType.Warning);
			}

			GUILayout.Space(5);
			GUILayout.Label("Easing:", EditorStyles.label);
			easeType = (EasingTypes)EditorGUILayout.EnumPopup(easeType, GUILayout.Height(30));
			GUILayout.Space(5);
			steps = EditorGUILayout.IntSlider("Curve Steps:" ,steps, 2, 100, GUILayout.Height(20));
			keyframeReductionTolerance = EditorGUILayout.FloatField("Reduction Tolerance:", keyframeReductionTolerance, GUILayout.Height(20));
			from = EditorGUILayout.FloatField("From:", from, GUILayout.Height(20));
			to = EditorGUILayout.FloatField("To:", to, GUILayout.Height(20));
			length = EditorGUILayout.FloatField("Length:", length, GUILayout.Height(20));
			if(go)
			{
				if(go != oldgo)
				{

				properties.Clear();
				types.Clear();
				typeNames.Clear();
				foreach(EditorCurveBinding b in AnimationUtility.GetAnimatableBindings(go, go))
				{
					properties.Add(b.propertyName);
				}
				foreach(Component c in go.GetComponentsInChildren<Component>())
				{
					Type t = c.GetType();
					types.Add(t);
					typeNames.Add(t.Name);
				}
					oldgo = go;
				}
				propertiesindex = EditorGUILayout.Popup("Property:", propertiesindex, properties.ToArray(), GUILayout.Height(30));
				typeindex = EditorGUILayout.Popup("Type:", typeindex, typeNames.ToArray(), GUILayout.Height(30));
				AnimationName = string.IsNullOrEmpty(AnimationName) ? go.name + "Animation" : AnimationName;
			}
			propertyName = properties.Count == 0 ? "" : properties[propertiesindex];
			type = types.Count == 0 ? typeof(Transform) : types[typeindex];

			curve = curve.graph(Easing.Function(easeType), steps, from, to, length);
			curve.reduceKeyframes(keyframeReductionTolerance);

			curve = EditorGUILayout.CurveField("Curve: ", curve, Color.magenta, new Rect() , GUILayout.Height(50));

			GUILayout.Box("",GUILayout.Width(this.position.width - 14), GUILayout.Height(3));


			if(GUILayout.Button("CreateAnimation", GUILayout.Height(25)) )
			{
				if(!go || string.IsNullOrEmpty(AnimationName)) EditorUtility.DisplayDialog("Not complete", "Select a Gameobject first and write an Animation Name ", "Ok");
				else
					makeAnimationInFolder();
			}

			GUILayout.Space(5);
			//Resize window
			if(Event.current.type == EventType.repaint)
			{	
				Rect rect = GUILayoutUtility.GetLastRect();
				this.minSize = new Vector2(windowWidth,rect.y + 8);
				this.maxSize = new Vector2(windowWidth,rect.y +8 );
			}
			EditorGUI.EndChangeCheck();
			GUILayout.EndArea();
			
		}

		//Helper Functions

		private void addClipToAnimator(AnimationClip clip)
		{
			string path = getPath() + go.name + "Controller.controller";
			AnimatorController controller;
			if(File.Exists(path))
			{
				controller = (AnimatorController)AssetDatabase.LoadAssetAtPath(path, typeof(AnimatorController));
				Undo.RecordObject(controller, "Adding Motion to Controller");
			}else{
				controller = UnityEditor.Animations.AnimatorController.CreateAnimatorControllerAtPath (path);
				Undo.RegisterCreatedObjectUndo(controller, "Created new Controller");
			}
			AnimatorControllerLayer baselayer = controller.layers[0];
			AnimatorStateMachine statemachine = baselayer.stateMachine;
			AnimatorState state = null;
			foreach(ChildAnimatorState st in statemachine.states)
			{
				if(st.state.name.ToLower() == AnimationName.ToLower())
				{
					state = st.state;
					break;
				}
			}
			if(state != null) state.motion = clip;
			else
				state = controller.AddMotion(clip);

			state.name = AnimationName;
			statemachine.defaultState = state;
			anim = go.GetComponent<Animator>() ? go.GetComponent<Animator>() : (Animator)Undo.AddComponent(go, (typeof(Animator)));
			Undo.RecordObject(anim, "Assigned Controller");
			anim.runtimeAnimatorController = controller;
		}
		private string getDirectoryPath(){
			string basePath = AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(this));
			string[] folders = basePath.Split('/');
			List<string> path2EditorFolder = new List<string>();
			foreach(string foldername in folders)
			{	
				if(foldername.Contains("Editor"))
				{
					basePath = string.Join("/", path2EditorFolder.ToArray()) +"/";
					break;
				}
				path2EditorFolder.Add(foldername);
			}
			return basePath;
		}
		private string getPath()
		{
			string directory = getDirectoryPath() + "EasedAnimations";
			string path = directory +"/";
			return path;
		}
		private void makeAnimationInFolder()
		{
			string directory = getDirectoryPath() + "EasedAnimations";
			if(!Directory.Exists(directory))
			{
				Directory.CreateDirectory(directory);
			}
			string path = getPath() + AnimationName + ".anim";
			AnimationClip newClip = easedAnimation();
			Undo.RegisterCreatedObjectUndo(newClip, "Created new Animation");
			AssetDatabase.CreateAsset(newClip, path );
		}
		private AnimationClip easedAnimation()
		{
			AnimationClip clip = new AnimationClip();
			EditorCurveBinding curveBinding = new EditorCurveBinding();
			curveBinding.type = type;

			curveBinding.path = "";

			curveBinding.propertyName = propertyName;

			AnimationUtility.SetEditorCurve(clip, curveBinding,curve);
			clip.EnsureQuaternionContinuity();
			addClipToAnimator(clip);
			return clip;
		}
		void OnInspectorUpdate()
		{
			this.Repaint();
		}

	}
}

