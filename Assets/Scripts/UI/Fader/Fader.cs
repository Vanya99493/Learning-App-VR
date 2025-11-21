using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace LearningAppVR.UI
{
	public class Fader : MonoBehaviour
	{
		[SerializeField]
		private float _fadeDuration;

		[SerializeField]
		private Image _fadeImage;

		[Space(10)]
		[SerializeField]
		private bool _fadeOutOnAwake = false;

		private Tween _fadeTween;
		
		private void Awake()
		{
			FadeIn(0f);
			
			if (_fadeOutOnAwake)
			{
				FadeOut();
			}
		}

		[Button]
		public void FadeIn(float duration = -1f)
		{
			_fadeTween?.Kill();
			FadeImage(_fadeImage, 1f, duration < 0 ? _fadeDuration : duration);
		}

		[Button]
		public void FadeOut(float duration = -1f)
		{
			_fadeTween?.Kill();
			FadeImage(_fadeImage, 1f, duration < 0 ? _fadeDuration : duration);
		}

		private void FadeImage(Image targetImage, float targetAlpha, float fadeDurationInSeconds)
		{
			Color currentColor = targetImage.color;
			_fadeTween = targetImage.DOColor(new Color(currentColor.r, currentColor.g, currentColor.b, targetAlpha), fadeDurationInSeconds);
		}
	}
}