using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static SplineSys.Spline;

namespace SplineSys {

	public static class Spline {

		public static Segment From2Vector(
			float3 p1, float3 p2, float3 v1, float3 v2,
			float alpha = 0.5f, float tension = 0f, float length = 1f) {

			float t12 = math.pow(math.distance(p1, p2), alpha);

			float3 m1 = (1.0f - tension) * t12 * length * v1;
			float3 m2 = (1.0f - tension) * t12 * length * v2;

			var a = 2.0f * (p1 - p2) + m1 + m2;
			var b = -3.0f * (p1 - p2) - m1 - m1 - m2;
			var c = m1;
			var d = p1;

			return new Segment { a = a, b = b, c = c, d = d };
		}
		public static Segment From4Points(
			float3 p0, float3 p1, float3 p2, float3 p3,
			float alpha = 0.5f, float tension = 0f) {

			float t01 = math.pow(math.distance(p0, p1), alpha);
			float t12 = math.pow(math.distance(p1, p2), alpha);
			float t23 = math.pow(math.distance(p2, p3), alpha);

			float3 m1 = (1.0f - tension) *
				(p2 - p1 + t12 * ((p1 - p0) / t01 - (p2 - p0) / (t01 + t12)));
			float3 m2 = (1.0f - tension) *
				(p2 - p1 + t12 * ((p3 - p2) / t23 - (p3 - p1) / (t12 + t23)));

			var a = 2.0f * (p1 - p2) + m1 + m2;
			var b = -3.0f * (p1 - p2) - m1 - m1 - m2;
			var c = m1;
			var d = p1;

			return new Segment { a = a, b = b, c = c, d = d };
		}

		public struct Segment {
			public float3 a, b, c, d;

			public float3 Point(float t) {
				return t * (t * (t * a + b) + c) + d;
			}
		}
	}
}
