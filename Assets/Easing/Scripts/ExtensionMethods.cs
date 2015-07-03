using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Easing8000{
public static class ExtensionMethods{
		public static float Difference(this float f, float a, float b){
			if(a > b) return -Mathf.Abs(a-b);
			return Mathf.Abs(a-b);
		}
		public static Vector3 ease(this Vector3 v3, Func<float, float, float, float>easingFunction, Vector3 from, Vector3 to, float t){
			float newX = easingFunction(from.x, to.x, t);
			float newY = easingFunction(from.y, to.y, t);
			float newZ = easingFunction(from.z, to.z, t);

			return new Vector3(newX, newY, newZ);
		}
		public static AnimationCurve graph(this AnimationCurve graph, Func<float, float, float, float> EasingFunction , int steps, float from, float to, float length = 1f)
		{
			graph = new AnimationCurve();
			for(int i = 0; i <= steps; i++)
			{
				float time = (float)i/(float)steps;
				float value = EasingFunction(from,to,time);
				graph.AddKey(new Keyframe(time * length, value));
				graph.SmoothTangents(i, 0f);
			}
			return graph;
		}
		public static void reduceKeyframes(this AnimationCurve graph, Double tolerance)
		{
			if(Mathf.Approximately(0f, (float) tolerance)) return;
			List<Keyframe> keyframes = new List<Keyframe>(graph.keys);
			keyframes = DouglasPeuckerReduction(keyframes, tolerance);
			graph.keys = keyframes.ToArray();
		}

		// c# implementation of the Ramer-Douglas-Peucker-Algorithm by Craig Selbert
		//http://www.codeproject.com/Articles/18936/A-Csharp-Implementation-of-Douglas-Peucker-Line-Ap
		public static List<Keyframe> DouglasPeuckerReduction
			(List<Keyframe> Points, Double Tolerance)
		{
			if (Points == null || Points.Count < 3)
				return Points;
			
			Int32 firstPoint = 0;
			Int32 lastPoint = Points.Count - 1;
			List<Int32> pointIndexsToKeep = new List<Int32>();
			
			//Add the first and last index to the keepers
			pointIndexsToKeep.Add(firstPoint);
			pointIndexsToKeep.Add(lastPoint);
			
			//The first and the last point cannot be the same
			while (Points[firstPoint].Equals(Points[lastPoint]))
			{
				lastPoint--;
			}
			
			DouglasPeuckerReduction(Points, firstPoint, lastPoint, 
			                        Tolerance, ref pointIndexsToKeep);
			
			List<Keyframe> returnPoints = new List<Keyframe>();
			pointIndexsToKeep.Sort();
			foreach (Int32 index in pointIndexsToKeep)
			{
				returnPoints.Add(Points[index]);
			}
			
			return returnPoints;
		}

		private static void DouglasPeuckerReduction(List<Keyframe> 
		                                            points, Int32 firstPoint, Int32 lastPoint, Double tolerance, 
		                                            ref List<Int32> pointIndexsToKeep)
		{
			Double maxDistance = 0;
			Int32 indexFarthest = 0;
			
			for (Int32 index = firstPoint; index < lastPoint; index++)
			{
				Double distance = (Double)PerpendicularDistance
					(points[firstPoint], points[lastPoint], points[index]);
				if (distance > maxDistance)
				{
					maxDistance = distance;
					indexFarthest = index;
				}
			}
			
			if (maxDistance > tolerance && indexFarthest != 0)
			{
				//Add the largest point that exceeds the tolerance
				pointIndexsToKeep.Add(indexFarthest);
				
				DouglasPeuckerReduction(points, firstPoint, 
				                        indexFarthest, tolerance, ref pointIndexsToKeep);
				DouglasPeuckerReduction(points, indexFarthest, 
				                        lastPoint, tolerance, ref pointIndexsToKeep);
			}
		}

		public static float PerpendicularDistance
			(Keyframe Point1, Keyframe Point2, Keyframe Point)
		{
			float area = Mathf.Abs(.5f * (Point1.time * Point2.value + Point2.time * 
			                             Point.value + Point.time * Point1.value - Point2.time * Point1.value - Point.time * 
			                             Point2.value - Point1.time * Point.value));
			float bottom = Mathf.Sqrt(Mathf.Pow(Point1.time - Point2.time, 2f) + 
			                          Mathf.Pow(Point1.value - Point2.value, 2f));
			float height = area / bottom * 2f;
			
			return height;

		}

	}
}
