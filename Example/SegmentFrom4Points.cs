using Gist2.Deferred;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SplineSys {

	[ExecuteAlways]
	public class SegmentFrom4Points : SegmentGenerator {

		[Range(0f, 1f)]
		public float tension = 0.5f;
		public Transform p0, p1, p2, p3;

		protected Validator changed = new Validator();

		#region unity
		private void OnEnable() {
			changed.Reset();
			changed.CheckValidity += () => !transform.hasChanged;
			changed.OnValidate += () => {
				transform.hasChanged = false;
				this.NotifyUpward();
			};
		}
		private void OnValidate() {
			changed.Invalidate();
		}
		private void Update() {
			changed.Validate();
		}
		#endregion

		public override bool TryGetSegment(out Spline.Segment seg) {
			seg = default;
			if (p0 == null || p1 == null || p2 == null || p3 == null) return false;

			seg = Spline.From4Points(p0.position, p1.position, p2.position, p3.position, tension: tension);
			return true;
		}
	}
}
