using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SplineSys {

	public static class NotifyExtension {
		public const string MSG_CHANGED = "OnChildrenChanged";
		public static void NotifyUpward(this MonoBehaviour mono) 
			=> mono.SendMessageUpwards(MSG_CHANGED, SendMessageOptions.DontRequireReceiver);
	}
}