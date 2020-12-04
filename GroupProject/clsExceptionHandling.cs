using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GroupProject
{
	public class clsExceptionHandling
	{
		/// <summary>
		/// Outputs the class, method, and message for the exception thrown
		/// </summary>
		/// <param name="errorClass"></param>
		/// <param name="errorMethod"></param>
		/// <param name="errorMessage"></param>
		public void HandleError(string errorClass, string errorMethod, string errorMessage)
		{
			try
			{
				MessageBox.Show(errorClass + "." + errorMethod + "->" + errorMessage);
			}
			catch (Exception ex)
			{
				System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
			}
		}
	}
}

