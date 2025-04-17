using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

namespace LearningAppVR
{
	public class CustomXRGrabInteractable : XRGrabInteractable
	{
		private Transform _lastParent;
		
		protected override void OnSelectEntered(SelectEnterEventArgs args)
		{
			base.OnSelectEntered(args);
			SetParentToXRRig();
		}

		private void SetParentToXRRig()
		{
			IXRSelectInteractor firstSelectingInteractor = interactorsSelecting.First();
			_lastParent = transform.parent;
			transform.SetParent(firstSelectingInteractor.transform);
		}

		protected override void OnSelectExited(SelectExitEventArgs args)
		{
			SetParentToWorld();
			base.OnSelectExited(args);
		}

		private void SetParentToWorld()
		{
			if (transform.parent != null && transform.parent.gameObject.activeInHierarchy)
			{
				transform.SetParent(_lastParent);
				_lastParent = null;
			}
		}
	}
}