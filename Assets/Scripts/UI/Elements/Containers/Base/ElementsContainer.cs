using System.Collections.Generic;
using UnityEngine;

namespace LearningAppVR.UI
{
	public class ElementsContainer<TElement, TData> : MonoBehaviour where TElement : MonoBehaviour
	{
		[SerializeField]
		protected TElement _elementPrefab;

		[SerializeField]
		protected Transform _parent;

		protected List<TElement> _elements = new();

		public virtual void FillContainer(List<TData> dataCollection)
		{
			ResetElements();
			foreach (var dataCollectionElement in dataCollection)
			{
				AddElement(dataCollectionElement);
			}
		}

		public virtual TElement AddElement(TData dataElement)
		{
			var element = InstantiateElement(dataElement);
			_elements.Add(element);
			return element;
		}

		protected virtual void ResetElements()
		{
			foreach (var element in _elements)
			{
				Destroy(element.gameObject);
			}
			_elements.Clear();
		}

		protected virtual TElement InstantiateElement(TData dataElement)
		{
			var element = Instantiate(_elementPrefab, _parent);
			return element;
		}
	}
}