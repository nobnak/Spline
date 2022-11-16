using Gist2.Deferred;
using UnityEngine;

namespace SplineSys {

	[ExecuteAlways]
	public class SegmentFrom2Vectors : SegmentGenerator {

		[Range(0f, 1f)]
		public float tension = 0.5f;
		public float length = 1f;
		public Transform p1, p2;

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
			if (p1 == null || p2 == null) return false;

			seg = Spline.From2Vector(p1.position, p1.forward, p2.position, p2.forward,
				tension: tension, length: length);
			return true;
		}
	}
}
