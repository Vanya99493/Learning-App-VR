using System.Text;

namespace LearningAppVR
{
	public static class StringExtensions
	{
		public static int FromBaseTimeStringToInt(this string baseTimeString)
		{
			int timeInSeconds = 0;
			int previousIntegerValue = 0;
			StringBuilder value = new StringBuilder();

			foreach (var symbol in baseTimeString)
			{
				value.Append(symbol);
				if (int.TryParse(value.ToString(), out var integerValue))
				{
					previousIntegerValue = integerValue;
				}
				else
				{
					if (symbol == 'h')
					{
						timeInSeconds += previousIntegerValue * 3600;
					}
					else if (symbol == 'm')
					{
						timeInSeconds += previousIntegerValue * 60;
					}
					else if (symbol == 's')
					{
						timeInSeconds += previousIntegerValue;
					}

					previousIntegerValue = 0;
					value.Clear();
				}
			}

			return timeInSeconds; 
		}

		public static int ToInt(this string str)
		{
			if (int.TryParse(str, out var integer))
			{
				return integer;
			}

			return 0;
		}
	}
}