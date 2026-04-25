using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.Commands;
using DevExpress.XtraRichEdit.Services;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Bifrost.Win;

namespace Bifrost.Office
{
    public partial class aWordViewer : MDIBase
    {
        public aWordViewer()
        {
            InitializeComponent();

            IRichEditCommandFactoryService service = richEditControl1.GetService(typeof(IRichEditCommandFactoryService)) as IRichEditCommandFactoryService;
            richEditControl1.ReplaceService<IRichEditCommandFactoryService>(new CustomCommandFactoryServise(service, richEditControl1));
        }

        private static string _FilePath = string.Empty;
        public string FilePath
        {
            get { return _FilePath; }
            set
            {
                _FilePath = value;
                string FileExtension = Path.GetExtension(_FilePath);

                switch (FileExtension)
                {
                    case ".doc":
                        richEditControl1.LoadDocument(_FilePath, DocumentFormat.Doc);
                        break;
                    case ".docx":
                        richEditControl1.LoadDocument(_FilePath, DocumentFormat.OpenXml);
                        break;
                }
            }
        }


        private static string _KeyFolder = string.Empty;

        public string KeyFolder
        {
            get { return _KeyFolder; }
            set
            {
                _KeyFolder = value;
            }
        }




        public class CustomCommandFactoryServise : RichEditCommandFactoryServiceWrapper
        {
            RichEditControl Control { get; set; }
            public CustomCommandFactoryServise(IRichEditCommandFactoryService service, RichEditControl _control) : base(service)
            {
                Control = _control;
            }

            public override RichEditCommand CreateCommand(RichEditCommandId id)
            {
                RichEditCommand command = base.CreateCommand(id);
                if (id == RichEditCommandId.FileSave)
                {
                    return new SaveCommand(Control);
                }
                if (id == RichEditCommandId.FileSaveAs)
                {
                    return new SaveAsCommand(Control);
                }
                return command;
            }
        }

        public class SaveCommand : SaveDocumentCommand
        {
            public SaveCommand(IRichEditControl control) : base(control) { }
            public override void Execute()
            {
                base.Execute();

                //string[] path = { _FilePath };
                //HttpFileHelper.MultiUpload(path, A.GetString(_KeyFolder));
                MessageBox.Show("저장 되었습니다");

                //MessageBox.Show(@"After Save method");
                // Your custom saving logic
            }
        }

        public class SaveAsCommand : SaveDocumentCommand
        {
            public SaveAsCommand(IRichEditControl control) : base(control) { }
            public override void Execute()
            {
                base.Execute();

                //string[] path = { _FilePath };
                //HttpFileHelper.MultiUpload(path, A.GetString(_KeyFolder));
                MessageBox.Show("저장 되었습니다");

                //MessageBox.Show(@"After Save method");
                // Your custom saving logic
            }
        }

    }
    
}
