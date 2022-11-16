using UnityEngine;

namespace SplineSys {

	public abstract class SegmentGenerator : MonoBehaviour {
		public abstract bool TryGetSegment(out Spline.Segment seg);
	}
}