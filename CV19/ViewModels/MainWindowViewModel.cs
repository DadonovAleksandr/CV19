using CV19.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV19.ViewModels
{
    internal class MainWindowViewModel : ViewModel 
    {
        #region Заголовок окна
        private string _title = "Анализ статиcтики CV19";
		/// <summary>
		/// Заголовок окна
		/// </summary>
		public string Title
		{
			get => _title;
			set => Set(ref _title, value);
		}
        #endregion

    }
}
