using System.Collections.Generic;
using UnityEngine;

namespace LearningAppVR.UI
{
	public class VariantsAnswersContainer : MonoBehaviour
	{
		[SerializeField]
		private AdditionalButtonController _additionalButtonController;

		[SerializeField]
		private GridPositionReset _gridPositionReset;

		[SerializeField]
		private List<VariantElement> _variantsElements = new();

		[SerializeField]
		private Transform _parent;

		[Space(10)]
		[SerializeField]
		private int _variantsCountByDefault = 2;

		[SerializeField]
		private int _minCountOfElements = 2;

		private Queue<VariantElement> _unsetVariants = new();
		private List<Pair<VariantElement, bool>> _setVariants = new();

		public void Initialize()
		{
			foreach (var variantElement in _variantsElements)
			{
				variantElement.RemoveAction += OnRemoveVariant;
				variantElement.ChangeCorrectStateEvent += OnChangeCorrectVariant;
				
				variantElement.Initialize();
			}
			_additionalButtonController.Initialize(OnAddVariantButtonClick);
		}
		
		public void Setup(List<string> variants, string correctAnswer)
		{
			ResetVariants();
			FillVariants(variants, correctAnswer);
		}

		private void ResetVariants()
		{
			_unsetVariants.Clear();
			foreach (var variantElement in _variantsElements)
			{
				variantElement.gameObject.SetActive(false);
				_unsetVariants.Enqueue(variantElement);
			}
			_setVariants.Clear();
		}

		private void FillVariants(List<string> variants, string correctAnswer)
		{
			if (variants.Count > 0)
			{
				bool hasCorrect = false;
				foreach (var variant in variants)
				{
					bool isCorrect = false;
					if (variant == correctAnswer)
					{
						hasCorrect = true;
						isCorrect = true;
					}
					
					AddVariantElement(variant, isCorrect);
				}

				if (!hasCorrect)
				{
					_setVariants[0].Value = true;
					_setVariants[0].Key.SetCorrect(false);
				}
			}
			else
			{
				for (int i = 0; i < _variantsCountByDefault; i++)
				{
					AddVariantElement("", i == 0);
				}
			}
			
			_additionalButtonController.UpdateAddButtonPosition(_parent);
		}

		private void OnRemoveVariant(VariantElement variantElement)
		{
			if (_setVariants.Count > _minCountOfElements)
			{
				int index = 0;
				foreach (var variantPair in _setVariants)
				{
					if (variantPair.Key == variantElement)
					{
						_unsetVariants.Enqueue(variantElement);
						variantElement.gameObject.SetActive(false);
						_setVariants.RemoveAt(index);

						if (variantElement.IsCorrect)
						{
							_setVariants[0].Key.SetCorrect(true);
						}

						return;
					}

					index++;
				}
			}
			else
			{
				variantElement.Setup("", variantElement.IsCorrect);
			}
		}

		private void OnAddVariantButtonClick()
		{
			AddVariantElement("", false);
		}

		private void AddVariantElement(string variant, bool isCorrect)
		{
			var variantElement = _unsetVariants.Dequeue();
			variantElement.Setup(variant, isCorrect);
			_setVariants.Add(new Pair<VariantElement, bool>(variantElement, isCorrect));
			_gridPositionReset.ResetPosition(variantElement.gameObject, _parent);
			_additionalButtonController.UpdateAddButtonPosition(_parent);
		}

		private void OnChangeCorrectVariant(VariantElement variantElement)
		{
			if (variantElement.IsCorrect)
			{
				foreach (var variant in _setVariants)
				{
					if (variant.Key != variantElement)
					{
						variant.Key.SetCorrect(false);
					}
				}
			}
			else
			{
				variantElement.SetCorrect(true);
			}
		}
	}
}