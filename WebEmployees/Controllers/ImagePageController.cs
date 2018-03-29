using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebEmployees.Controllers
{
	public class PathsArrayAndPositiveImageNumberForm
	{
		public string[] pathsArray { get; set; }
		public string positiveImageNumberForm { get; set; }
	}

	public class ImagePageController : Controller
	{

		private int GetNumber(int maxSize)
		{
			Random rnd = new Random();
			return rnd.Next(1, maxSize);
		}

		private int[] GetNegativeNumbersImages()
		{
			Random rnd = new Random();
			int[] negativeNumbersImages = new int[8];
			int currentNumber = 0;
			int nextValue;
			while (true)
			{
				nextValue = rnd.Next(1, 18);
				if (negativeNumbersImages.Where(a => a == nextValue).Count() != 0)
				{
					continue;
				}

				negativeNumbersImages[currentNumber] = nextValue;
				currentNumber++;
				if (currentNumber == 8)
				{
					break;
				}
			}

			return negativeNumbersImages;
		}

		public PathsArrayAndPositiveImageNumberForm GetImagesPathsAndPositiveImageNumberForm()
		{
			var pathsArray = new string[9];

			var positiveImageNumberForm = GetNumber(9);
			int positiveImageNumberFolder = GetNumber(15);
			pathsArray[positiveImageNumberForm-1] = GetSourseImage(true, positiveImageNumberForm, positiveImageNumberFolder);

			int[] negativeNumbersImages = GetNegativeNumbersImages();
			int currentNumber = 0;
			for (int i = 1; i <= 9; i++)
			{
				if (i == positiveImageNumberForm)
				{
					continue;
				}

				pathsArray[i-1] = GetSourseImage(false, i, negativeNumbersImages[currentNumber]);
				currentNumber++;
			}

			return
				new PathsArrayAndPositiveImageNumberForm
				{
					pathsArray = pathsArray,
					positiveImageNumberForm = "Image" + positiveImageNumberForm.ToString()
				};
		}


		private string GetSourseImage(bool positivePicture, int imageNumberForm, int imageNumberFolder)
		{
			string fullPath = "/Images" + ((positivePicture) ? "/Positive pictures/" : "/Negative pictures/") + "Image" + imageNumberFolder.ToString() + ".jpg";

			return fullPath;

		}
	}

}