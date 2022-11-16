using Gist2.Deferred;
using LLGraphicsUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SplineSys {

	[ExecuteAlways]
	public class Visualizer : MonoBehaviour {

		public DataSet data = new DataSet();

		protected Validator changed = new Validator();
		protected GLMaterial gl;
		protected List<Spline.Segment> segments = new List<Spline.Segment>();

		#region unity
		private void OnEnable() {
			gl = new GLMaterial();

			changed.Reset();
			changed.OnValidate += () => {
				segments.Clear();
				foreach (var l in data.splines) {
					if (!l.TryGetSegment(out var s)) continue;
					segments.Add(s);
				}
			};
		}
		private void OnDisable() {
			if (gl != null) {
				gl.Dispose();
				gl = null;
			}
		}
		private void OnValidate() {
			changed.Invalidate();
		}
		private void OnRenderObject() {
			changed.Validate();

			var c = Camera.current;
			if (c == null || (c.cullingMask & (1 << gameObject.layer)) == 0) return;

			var nsteps = 10;
			var dt = 1f / nsteps;

			var prop = new GLProperty() {
				Color = Color.white,
				ZTestMode = GLProperty.ZTestEnum.ALWAYS,
				ZWriteMode = false
			};

			using (new GLMatrixScope()) {
				GL.modelview = c.worldToCameraMatrix;
				GL.LoadProjectionMatrix(c.projectionMatrix);

				using (gl.GetScope(prop))
					foreach (var s in segments)
						using (new GLPrimitiveScope(GL.LINE_STRIP))
							for (var i = 0; i <= nsteps; i++)
								GL.Vertex(s.Point(dt * i));
			}
		}
		#endregion

		#region messages
		public void OnChildrenChanged() {
			changed.Invalidate();
		}
		#endregion

		#region declarations
		[System.Serializable]
		public class DataSet {
			public List<SegmentGenerator> splines = new List<SegmentGenerator>();
		}
		#endregion
	}
}
