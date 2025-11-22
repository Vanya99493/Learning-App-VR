using PaintIn3D;
using UnityEngine;
using UnityEngine.UI;

namespace LearningAppVR.UI.SeparateComponents
{
	[RequireComponent(typeof(Button))]
	public class ResetBoardButton : MonoBehaviour
	{
		[SerializeField]
		private Button _resetBoardButton;

		[Space(10)]
		[SerializeField]
		private CwPaintableMesh _paintableMesh;
		
		private void Awake()
		{
			_resetBoardButton ??= GetComponent<Button>();
		}

		private void OnEnable()
		{
			_resetBoardButton.onClick.AddListener(OnResetBoardButtonClickEventHandler);
		}

		private void OnDisable()
		{
			_resetBoardButton.onClick.RemoveListener(OnResetBoardButtonClickEventHandler);
		}

		[ContextMenu("Clear All")]
		private void OnResetBoardButtonClickEventHandler()
		{
			_paintableMesh.ClearAll(Color.white);
		}
	}
}