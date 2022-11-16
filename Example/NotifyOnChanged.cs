using Gist2.Deferred;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SplineSys {

	[ExecuteAlways]
	public class NotifyOnChanged : MonoBehaviour {

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
	}

}