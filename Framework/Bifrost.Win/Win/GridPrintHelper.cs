using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows;
using System.Windows.Forms;

using NF.Framework.Win.Controls;

using Infragistics.Win;
using Infragistics.Win.Printing;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Shared;

namespace NF.Framework.Win
{
    /// <summary>
    /// GridPrintHelper
    /// </summary>
    public class GridPrintHelper
    {
        private const int VERTICAL = 1070;
        private const int HORIZONTAL = 720;
      
        /// <summary>
        /// 프린트 제목
        /// </summary>
        private string title = "";
        /// <summary>
        /// 출력조건
        /// </summary>
        private string condition = "";
        /// <summary>
        /// 제목 글자크기
        /// </summary>
        private int titleSize = 20;
        /// <summary>
        /// Header Height , 단위 : mm
        /// </summary>
        private int headerHeight = 40;
        /// <summary>
        /// 왼쪽 마진 , 단위 : mm
        /// </summary>
        private int leftMargin = 100;
        /// <summary>
        /// 오른쪽 마진 , 단위 : mm
        /// </summary>
        private int rightnMargin = 100;
        /// <summary>
        /// 하단 마진 , 단위 : mm
        /// </summary>
        private int bottomMargin = 200;
        /// <summary>
        /// 상단 마진 , 단위 : mm
        /// </summary>
        private int topMargin = 100;

        /// <summary>
        /// Property
        /// </summary>
        public int TitleSize
        {
            set { titleSize = value; }
        }
        public int HeaderHeight
        {
            set { headerHeight = value; }
        }
        
        /// <summary>
        /// 그리드의 넓이를 계산하여 비율자동 설정
        /// </summary>
        /// <param name="ultraGrid">UltraGrid</param>
        /// <param name="ultraGridPrintDocument">UltraGridPrintDocument</param>
        /// <param name="landscape">가로출력여부</param>
        /// <param name="title">출력제목</param>
        /// <param name="condition">출력조건</param>
        /// <returns></returns>
        public PrintSettings PrintSetting(UltraGrid ultraGrid, UltraGridPrintDocument ultraGridPrintDocument, bool preview, bool landscape, string title, string condition)
        {
            float gridsize = 0;
            int printscale = 0;

            gridsize = GetGridWidth(ultraGrid);

            if (landscape)
                printscale = (int)(VERTICAL / gridsize * 100);
            else
                printscale = (int)(HORIZONTAL / gridsize * 100);

            if (printscale > 100)
                printscale = 100;

            PrintSettings ps = PrintSetting(ultraGrid, ultraGridPrintDocument, preview, printscale, landscape, title, condition);

            return ps;
        }

        /// <summary>
        /// 그리드의 넓이가 계산되어지지 않는 (예 : 복수의 Row가 존재하는 그리드) 경우 직접 비율을 입력하여 설정
        /// </summary>
        /// <param name="ultraGrid">UltraGrid</param>
        /// <param name="ultraGridPrintDocument">UltraGridPrintDocument</param>
        /// <param name="printScale">출력비율</param>
        /// <param name="landscape">가로출력여부</param>
        /// <param name="title">출력제목</param>
        /// <param name="condition">출력조건</param>
        /// <returns></returns>
        public PrintSettings PrintSetting(UltraGrid ultraGrid, UltraGridPrintDocument ultraGridPrintDocument, bool preview, int printScale, bool landscape, string title, string condition)
        {
            PrintSettings ps = null;

            try
            {
                this.title = title;
                this.condition = condition;

                int left = PrinterUnitConvert.Convert(leftMargin, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display);
                int right = PrinterUnitConvert.Convert(rightnMargin, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display);
                int bottom = PrinterUnitConvert.Convert(bottomMargin, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display);
                int top = PrinterUnitConvert.Convert(topMargin, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display);

                ultraGridPrintDocument.DefaultPageSettings.Margins = new System.Drawing.Printing.Margins(left, right, bottom, top);

                ultraGridPrintDocument.Grid = ultraGrid;

                ultraGridPrintDocument.PageHeaderPrinting += new Infragistics.Win.Printing.HeaderFooterPrintingEventHandler(ultraGridPrintDocument_PageHeaderPrinting);
                ultraGrid.InitializeLogicalPrintPage += new Infragistics.Win.UltraWinGrid.InitializeLogicalPrintPageEventHandler(ultraGrid_InitializeLogicalPrintPage);

                ultraGridPrintDocument.Header.Appearance.FontData.SizeInPoints = titleSize;
                ultraGridPrintDocument.Header.Height = headerHeight;

                ultraGridPrintDocument.Footer.TextCenter = "[페이지[Page #]]";

                ps = new PrintSettings(ultraGrid, ultraGridPrintDocument, preview);
                ps.Landscape = landscape;
                ps.PrintScale = printScale;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ps;
        }

        /// <summary>
        /// 그리드의 Width 를 구함
        /// </summary>
        /// <param name="ultraGrid"></param>
        /// <returns></returns>
        private int GetGridWidth(UltraGrid ultraGrid)
        {
            int gridsize = 0;

            for (int i = 0; i < ultraGrid.DisplayLayout.Bands[0].Columns.Count; i++)
            {
                if (!ultraGrid.DisplayLayout.Bands[0].Columns[i].Hidden)
                {
                    gridsize += ultraGrid.DisplayLayout.Bands[0].Columns[i].Width;
                }
            }

			gridsize = gridsize + (ultraGrid.DisplayLayout.Bands[0].SortedColumns.Count * 18);

            return gridsize;
        }

        /// <summary>
        /// ultraGrid_InitializeLogicalPrintPage : 조건출력
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ultraGrid_InitializeLogicalPrintPage(object sender, CancelableLogicalPrintPageEventArgs e)
        {
            e.LogicalPageLayoutInfo.PageHeader = condition;
            e.LogicalPageLayoutInfo.PageHeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
        }

        /// <summary>
        /// ultraGridPrintDocument_PageHeaderPrinting : 제목 출력
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ultraGridPrintDocument_PageHeaderPrinting(object sender, HeaderFooterPrintingEventArgs e)
        {
            e.Section.TextCenter = title;
        }
        
    }
}
