using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Envision.Documents
{

    public delegate void OnSaveStateChange(object sender, EventArgs e);
    public class ScriptDocumentModel
    {

        public event OnSaveStateChange SaveStateChanged;

        public ScriptDocumentModel(string filename = "")
        {
            if (filename == "")
            {
                this.IsSaved = false;
            }
            else
            {
                this.IsSaved = true;
            }
            this.FileName = filename;
        }

        public void OnSaveStateChanged()
        {
            if (SaveStateChanged != null)
            {
                SaveStateChanged(this, new EventArgs());
            }
        }

        public string FileName { get; set; }

        private bool _isSaved = false;
        public bool IsSaved {
            get { return _isSaved; }
            set
            {
                _isSaved = value;
                OnSaveStateChanged();
            } 
        }

        public void Touch()
        {
            this.IsSaved = false;
        }

       
    }
}
