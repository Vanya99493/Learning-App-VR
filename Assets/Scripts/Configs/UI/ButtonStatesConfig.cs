using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LearningAppVR.Configs
{
	[CreateAssetMenu(fileName = "ButtonStatesConfig", menuName = "Learning App VR/Configs/UI/Button States Config")]
	public class ButtonStatesConfig : ScriptableObject
	{
		[SerializeField]
		private List<Pair<ButtonStateType, Color>> _buttonStates;

		public Color GetColor(ButtonStateType stateType) => _buttonStates.FirstOrDefault(x => x.Key == stateType).Value;
		
		public Color PassiveColor => _buttonStates.FirstOrDefault(x => x.Key == ButtonStateType.Passive).Value;
		public Color ActiveColor => _buttonStates.FirstOrDefault(x => x.Key == ButtonStateType.Active).Value;
		public Color DoneColor  => _buttonStates.FirstOrDefault(x => x.Key == ButtonStateType.Answered).Value;
	}
}