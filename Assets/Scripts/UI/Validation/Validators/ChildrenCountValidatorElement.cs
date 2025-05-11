using UnityEngine;

namespace LearningAppVR.UI.Validators
{
	public class ChildrenCountValidatorElement : ValidatorElement
	{
		[SerializeField]
		private Transform _container;
		
		protected override bool CheckValidation()
		{
			foreach (var validationType in _validationTypes)
			{
				switch (validationType)
				{
					case ValidationType.HasChildren:
						if (_container.childCount <= 0)
						{
							return false;
						}
						break;
					case ValidationType.HasMoreThenOneChild:
						if (_container.childCount <= 1)
						{
							return false;
						}
						break;
					default:
						Debug.LogError($"[Validator] Children count validator does not support {validationType}");
						return false;
				}
			}

			return true;
		}
	}
}