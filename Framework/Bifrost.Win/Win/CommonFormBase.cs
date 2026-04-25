using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bifrost.Win
{
    #region FormBaseCollection

    /// <summary>
    /// Collection of FormBase objects
    /// </summary>
    public class FormBaseCollection : CollectionBase
    {
        public int Add(FormBase form)
        {
            return List.Add(form);
        }

        public void Insert(int index, FormBase form)
        {
            List.Insert(index, form);
        }

        public void Remove(FormBase form)
        {
            List.Remove(form);
        }

        public bool Contains(FormBase form)
        {
            return List.Contains(form);
        }

        public int IndexOf(FormBase form)
        {
            return List.IndexOf(form);
        }

        public void CopyTo(FormBase[] array, int index)
        {
            List.CopyTo(array, index);
        }

        public FormBase this[int index]
        {
            get { return (FormBase)List[index]; }
            set { List[index] = value; }
        }

        public FormBase FindByName(string formName)
        {
            foreach (Object objForm in List)
            {
                FormBase form = (FormBase)objForm;
                if (form.Name.Equals(formName))
                {
                    return form;
                }
            }

            return null;
        }

        public FormBase FindByFullName(string formNamespace)
        {
            foreach (Object objForm in List)
            {
                FormBase form = (FormBase)objForm;

                string fns = form.GetType().Namespace + "." + form.Name;
                if (fns.Equals(formNamespace))
                {
                    return form;
                }
            }

            return null;
        }
    }

    #endregion

    #region Validation Events

    public delegate void FormValidatingEventHandler(FormValidatingEventArgs e);
    public class FormValidatingEventArgs : EventArgs
    {
        public bool IsValid { get; set; }

        public FormValidatingEventArgs(bool isValid)
        {
            IsValid = isValid;
        }
    }

    #endregion
}
