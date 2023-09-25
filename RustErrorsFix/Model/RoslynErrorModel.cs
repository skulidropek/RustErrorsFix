using RustErrorsFix.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustErrorsFix.Model
{
    internal class RoslynErrorModel : ViewModelBase
    {
        private string text;
        private bool isActive;

        public string Text
        {
            get { return text; }
            set
            {
                if (text != value)
                {
                    text = value;
                    OnPropertyChanged("Text");
                }
            }
        }
        public bool IsActive
        {
            get { return isActive; }
            set
            {
                if (isActive != value)
                {
                    isActive = value;
                    OnPropertyChanged("IsActive");
                }
            }
        }

        public bool IsAnalise;

       // public List<AbstractFactory> Errors = new List<AbstractFactory>();
    }
}
