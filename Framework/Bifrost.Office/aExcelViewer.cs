using DevExpress.Spreadsheet;
using DevExpress.XtraSpreadsheet;
using DevExpress.XtraSpreadsheet.Commands;
using DevExpress.XtraSpreadsheet.Services;
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
    public partial class aExcelViewer : MDIBase
    {
        public aExcelViewer()
        {
            InitializeComponent();
            //ISpreadsheetCommandFactoryService service = spreadsheetControl1.GetService(typeof(ISpreadsheetCommandFactoryService)) as ISpreadsheetCommandFactoryService;
            //spreadsheetControl1.ReplaceService<ISpreadsheetCommandFactoryService>(new CustomCommandFactoryServise(service, spreadsheetControl1));
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
                    case ".xls":
                        spreadsheetControl1.LoadDocument(_FilePath, DevExpress.Spreadsheet.DocumentFormat.Xls);
                        break;
                    case ".xlsx":
                        spreadsheetControl1.LoadDocument(_FilePath, DevExpress.Spreadsheet.DocumentFormat.Xlsx);
                        break;
                    case ".csv":
                        spreadsheetControl1.LoadDocument(_FilePath, DevExpress.Spreadsheet.DocumentFormat.Csv);
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



        //public class CustomCommandFactoryServise : SpreadsheetCommandFactoryServiceWrapper
        //{
        //    SpreadsheetControl Control { get; set; }
        //    public CustomCommandFactoryServise(ISpreadsheetCommandFactoryService service, SpreadsheetControl _control) : base(service)
        //    {
        //        Control = _control;
        //    }

        //    public override SpreadsheetCommand CreateCommand(SpreadsheetCommandId id)
        //    {
        //        SpreadsheetCommand command = base.CreateCommand(id);
        //        if (id == SpreadsheetCommandId.FileSave)
        //        {
        //            return new SaveCommand(Control);
        //        }
        //        if (id == SpreadsheetCommandId.FileSaveAs)
        //        {
        //            return new SaveAsCommand(Control);
        //        }
        //        return command;
        //    }
        //}

        //public class SaveCommand : SaveDocumentCommand
        //{
        //    public SaveCommand(ISpreadsheetControl control) : base(control) { }
        //    public override void Execute()
        //    {
        //        base.Execute();

        //        //string[] path = { _FilePath };
        //        //HttpFileHelper.MultiUpload(path, A.GetString(_KeyFolder));

                
        //        MessageBox.Show("저장 되었습니다");
        //        // Your custom saving logic
        //    }
        //}

        //public class SaveAsCommand : SaveDocumentCommand
        //{
        //    public SaveAsCommand(ISpreadsheetControl control) : base(control) { }
        //    public override void Execute()
        //    {
        //        base.Execute();

        //        //string[] path = { _FilePath };
        //        //HttpFileHelper.MultiUpload(path, A.GetString(_KeyFolder));
        //        MessageBox.Show("저장 되었습니다");

        //        //MessageBox.Show(@"After Save method");
        //        // Your custom saving logic
        //    }
        //}
    }
}