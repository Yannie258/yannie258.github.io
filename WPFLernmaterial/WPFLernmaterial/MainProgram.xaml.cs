using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using Microsoft.Win32;

namespace WPFLernmaterial
{
    /// <summary>
    /// Interaktionslogik für Window1.xaml
    /// </summary>
    public partial class MainProgram : Excel.Window
    {
        //am Anfang wuerden alle Dateien in Database angezeigt
        public MainProgram()
        {
            InitializeComponent();
            showData();
        }
        //Sqldatabase Addresse
        private string sql = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\coanh\OneDrive\Documents\c#\Abschlussarbeit Muster\WPFLernmaterial\WPFLernmaterial\Database1.mdf;Integrated Security = True";
        
        //Methode für Add Button
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //connect to Database
            SqlConnection connect = new SqlConnection(sql);
            //fuegen eingetippten Datei aus Textbox ins Database ein
            SqlCommand command = new SqlCommand("insert into TBLData(Id,Fachname,Dozentvorname,Dozentnachname,Seminar,Datum,Lernmaterial) values('" + id.Text + "','" + fachName.Text + "','" + dozentVorname.Text + "','" + dozentNachname.Text + "','" + seminar.Text + "','" + datum.Text + "', '" + material.Text + "')", connect);
 
            // wenn Befehl kann ausfuehren -> Message und loeschen Data in Textbox
            // sonst Fehler ausgeben
            //Zum Schluss "close connect" und Data in Datagrid anzeigen
            try
            {

                connect.Open(); //open Verbindung
                command.ExecuteNonQuery(); //setzen Befehl durch
                MessageBox.Show("Einfügen erfolgreich!"); 
                clear();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                connect.Close();
                showData();
            }

        }


        //Methode für Ẽdit Button aehnlich wie Add Button nur anderen Befehl update statt insert getnutzt wird
        
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connect = new SqlConnection(sql);
            SqlCommand command = new SqlCommand("update TBLData set Fachname='" + fachName.Text + "',Dozentvorname='" + dozentVorname.Text + "', Dozentnachname='" + dozentNachname.Text + "', Seminar='" + seminar.Text + "',Datum='" + datum.Text + "',Lernmaterial='" + material.Text + "' where Id='" + id.Text + "'", connect);
            try
            {

                connect.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("Änderung erfolgreich!");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                connect.Close();
                showData();
            }
        }


        //Methode für Delete Button aehnlich wie Add Button nur anderen Befehl delete statt insert getnutzt wird
       // Datei nach dem geloescht werden, werden auch in TextBox verschwunden
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connect = new SqlConnection(sql);
            SqlCommand command = new SqlCommand("delete from TBLData where Id='" + id.Text + "'", connect);
            try
            {
                connect.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("Löschen erfolgreich!");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                connect.Close();
                clear();
                showData();
            }
        }
        //Methode für Show Button
        //zeigen alle Datei in Database an
        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            showData();

        }
        // clear alle TextBoxes
        private void clear()
        {
            id.Text=fachName.Text = fachName.Text = dozentVorname.Text = dozentNachname.Text = seminar.Text = material.Text = null;
        }

        // Hiftmethode fuer Show Button
        private void search()
        {
            SqlConnection connect = new SqlConnection(sql);
            SqlDataAdapter da = new SqlDataAdapter("select *from TBLData where Fachname='" + fachName.Text + "'", connect);
            DataTable dt = new DataTable();
            da.Fill(dt);
        }

        //Methode für Find Id Button
        //Suchen Data nach Id Nummer
        //'select' Abfrage nutzen
        //
        private void btnFindid_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connect = new SqlConnection(sql);
            SqlDataAdapter da = new SqlDataAdapter("select *from TBLData where Id='" + id.Text + "'", connect);


            try
            {
                // Data in Datatable aktualisieren
                DataTable dt = new DataTable();
                da.Fill(dt);

                //nur die Daten nach ID angezeigt werden
               fachName.Text = dt.Rows[0][1].ToString();
                dozentVorname.Text = dt.Rows[0][2].ToString();
                dozentNachname.Text = dt.Rows[0][3].ToString();
                seminar.Text = dt.Rows[0][4].ToString();
                datum.Text = dt.Rows[0][5].ToString();
                material.Text = dt.Rows[0][6].ToString();
                filterIdData();
                clear();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Kein Ergebnis gefunden!");
            }
            finally
            {
                connect.Close();
                
            }
        }
        //Methode für Find nach Fachname Button
        //Suchen Data nach Fachname
        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connect = new SqlConnection(sql);
            SqlDataAdapter da = new SqlDataAdapter("select *from TBLData where Fachname='" + fachName.Text + "'", connect);


            try
            {
                DataTable dt = new DataTable();
                da.Fill(dt);

                id.Text = dt.Rows[0][0].ToString();
                dozentVorname.Text = dt.Rows[0][2].ToString();
                dozentNachname.Text = dt.Rows[0][3].ToString();
                seminar.Text = dt.Rows[0][4].ToString();
                datum.Text = dt.Rows[0][5].ToString();
                material.Text = dt.Rows[0][6].ToString();
                filterNameData();
                clear();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Kein Ergebnis gefunden!");
            }
            finally
            {
                connect.Close();
               
            }
        }


        //Methode für Ẽxit Button
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Wollen Sie das Programm schließen?", "", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                Environment.Exit(0);

            }
        }
        // Methode alle Daten in Database anzeigen 
        private void showData()
        {
            SqlConnection connect = new SqlConnection(sql);

            //Verbindung mit Source Data durch aktuelle Connection
            SqlDataAdapter da = new SqlDataAdapter("select *from TBLData", connect);
            DataTable dt = new DataTable();
            try
            {

                da.Fill(dt);
                gridData.ItemsSource = dt.DefaultView; //in Datagrid ausgegeben
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                connect.Close();
            }
        }
        //Methode fur Suchen Button und zeigt in Datagrid nach ID
         private void filterNameData()
        {
            SqlConnection connect = new SqlConnection(sql);
            SqlDataAdapter da = new SqlDataAdapter("select *from TBLData where Fachname='" + fachName.Text + "'", connect);
            DataTable dt = new DataTable();
            try
            {

                da.Fill(dt);
                gridData.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                connect.Close();
            }
        }


        //Methode fur Suchen nach Fachname Button und zeigt in Datagrid
        private void filterIdData()
        {
            SqlConnection connect = new SqlConnection(sql);
            SqlDataAdapter da = new SqlDataAdapter("select *from TBLData where Id='" + id.Text + "'", connect);
            DataTable dt = new DataTable();
            try
            {

                da.Fill(dt);
                gridData.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                connect.Close();
            }
        }


        //Wenn DatagridsZeile geklickt wird, werden alle Datei in TextBox anzeigen
        private void gridData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView rowSelected = dg.SelectedItem as DataRowView;
            if (rowSelected != null)
            {
                id.Text = rowSelected["Id"].ToString();
                fachName.Text = rowSelected["Fachname"].ToString();
                dozentVorname.Text = rowSelected["Dozentvorname"].ToString();
                dozentNachname.Text = rowSelected["Dozentnachname"].ToString();
                seminar.Text = rowSelected["Seminar"].ToString();
                datum.Text = rowSelected["Datum"].ToString();
                material.Text = rowSelected["Lernmaterial"].ToString();

            }
        }

        //exportieren Methode
        private bool exportxlx()
        {
            bool xlx = true;
            Excel.Application excel = new Excel.Application(); //Microsoft Excel Bibliothek  
            excel.Visible = true;
            Workbook wb = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Worksheet sheet1 = (Excel.Worksheet)wb.Sheets[1];

            for (int i = 0; i < gridData.Columns.Count; i++)
            {
                Range rg = (Range)sheet1.Cells[1, i + 1];
                sheet1.Cells[1, i + 1].Font.Bold = true;


                sheet1.Columns[i + 1].ColumnWidth = 35;
                rg.Value2 = gridData.Columns[i].Header;
            }
            for (int j = 0; j < gridData.Columns.Count; j++)
            {
                for (int i = 0; i < gridData.Items.Count; i++)
                {
                    TextBlock b = gridData.Columns[j].GetCellContent(gridData.Items[i]) as TextBlock;
                    Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[i + 2, j + 1];
                    range.Value2 = b.Text;
                }
            }
            return xlx;
        }

        private void btnImp_Click(object sender, RoutedEventArgs e)
        {
           
            OpenFileDialog open = new OpenFileDialog();
            open.DefaultExt = ".xlsx";
            open.Filter = "(.xlsx)|*.xlsx";
            var brownser = open.ShowDialog();

            if (brownser == true)
            {
                path.Text = open.FileName;
                Excel.Application exc = new Excel.Application();
                Excel.Workbook workbook =
                    exc.Workbooks.Open(AppDomain.CurrentDomain.BaseDirectory + "test.xlsx", 0,true,5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, 0, true, 1, 0);
                Excel.Worksheet sheet = (Excel.Worksheet)workbook.Worksheets.get_Item(1); ;
                Excel.Range rang = sheet.UsedRange;
                string strCellData = "";
                double douCellData;
                int rowCnt = 0;
                int colCnt = 0;

                DataTable dt = new DataTable();
                for (colCnt = 1; colCnt <= rang.Columns.Count; colCnt++)
                {
                    string strColumn = "";
                    strColumn = (string)(rang.Cells[1, colCnt] as Microsoft.Office.Interop.Excel.Range).Value2;
                    dt.Columns.Add(strColumn, typeof(string));
                }

                for (rowCnt = 2; rowCnt <= rang.Rows.Count; rowCnt++)
                {
                    string strData = "";
                    for (colCnt = 1; colCnt <= rang.Columns.Count; colCnt++)
                    {
                        try
                        {
                            strCellData = (string)(rang.Cells[rowCnt, colCnt] as Excel.Range).Value2;
                            strData += strCellData + "|";
                        }
                        catch (Exception ex)
                        {
                            douCellData = (rang.Cells[rowCnt, colCnt] as Excel.Range).Value2;
                            strData += douCellData.ToString() + "|";
                        }
                    }
                    strData = strData.Remove(strData.Length - 1, 1);
                    dt.Rows.Add(strData.Split('|'));
                }

                gridData.ItemsSource = dt.DefaultView;

                workbook.Close(true, null, null);
                exc.Quit();

            }
            
        }
        private async void btnExp_Click(object sender, RoutedEventArgs e)
        {
            Task<bool> task = new Task<bool>(exportxlx);
            task.Start();
            bool excel = await task;

        }
       // private async void btnImp_Click(object sender, RoutedEventArgs e)
      //  {
       //     Task<bool> task = new Task<bool>(importxlx);
       //     task.Start();
         //   bool excel = await task;

       // }


        dynamic Excel.Window.Activate()
        {
            throw new NotImplementedException();
        }

        public dynamic ActivateNext()
        {
            throw new NotImplementedException();
        }

        public dynamic ActivatePrevious()
        {
            throw new NotImplementedException();
        }

        public bool Close(object SaveChanges, object Filename, object RouteWorkbook)
        {
            throw new NotImplementedException();
        }

        public dynamic LargeScroll(object Down, object Up, object ToRight, object ToLeft)
        {
            throw new NotImplementedException();
        }

        public Excel.Window NewWindow()
        {
            throw new NotImplementedException();
        }

        public dynamic _PrintOut(object From, object To, object Copies, object Preview, object ActivePrinter, object PrintToFile, object Collate, object PrToFileName)
        {
            throw new NotImplementedException();
        }

        public dynamic PrintPreview(object EnableChanges)
        {
            throw new NotImplementedException();
        }

        public dynamic ScrollWorkbookTabs(object Sheets, object Position)
        {
            throw new NotImplementedException();
        }

        public dynamic SmallScroll(object Down, object Up, object ToRight, object ToLeft)
        {
            throw new NotImplementedException();
        }

        public int PointsToScreenPixelsX(int Points)
        {
            throw new NotImplementedException();
        }

        public int PointsToScreenPixelsY(int Points)
        {
            throw new NotImplementedException();
        }

        public dynamic RangeFromPoint(int x, int y)
        {
            throw new NotImplementedException();
        }

        public void ScrollIntoView(int Left, int Top, int Width, int Height, object Start)
        {
            throw new NotImplementedException();
        }

        public dynamic PrintOut(object From, object To, object Copies, object Preview, object ActivePrinter, object PrintToFile, object Collate, object PrToFileName)
        {
            throw new NotImplementedException();
        }

        public Excel.Application Application => throw new NotImplementedException();

        public XlCreator Creator => throw new NotImplementedException();

        dynamic Excel.Window.Parent => throw new NotImplementedException();

        public Range ActiveCell => throw new NotImplementedException();

        public Chart ActiveChart => throw new NotImplementedException();

        public Pane ActivePane => throw new NotImplementedException();

        public dynamic ActiveSheet => throw new NotImplementedException();

        public dynamic Caption { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool DisplayFormulas { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool DisplayGridlines { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool DisplayHeadings { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool DisplayHorizontalScrollBar { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool DisplayOutline { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool _DisplayRightToLeft { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool DisplayVerticalScrollBar { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool DisplayWorkbookTabs { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool DisplayZeros { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool EnableResize { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool FreezePanes { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int GridlineColor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public XlColorIndex GridlineColorIndex { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int Index => throw new NotImplementedException();

        public string OnWindow { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Panes Panes => throw new NotImplementedException();

        public Range RangeSelection => throw new NotImplementedException();

        public int ScrollColumn { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int ScrollRow { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Sheets SelectedSheets => throw new NotImplementedException();

        public dynamic Selection => throw new NotImplementedException();

        public bool Split { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int SplitColumn { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double SplitHorizontal { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int SplitRow { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double SplitVertical { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double TabRatio { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public XlWindowType Type => throw new NotImplementedException();

        public double UsableHeight => throw new NotImplementedException();

        public double UsableWidth => throw new NotImplementedException();

        public bool Visible { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Range VisibleRange => throw new NotImplementedException();

        public int WindowNumber => throw new NotImplementedException();

        XlWindowState Excel.Window.WindowState { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public dynamic Zoom { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public XlWindowView View { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool DisplayRightToLeft { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public SheetViews SheetViews => throw new NotImplementedException();

        public dynamic ActiveSheetView => throw new NotImplementedException();

        public bool DisplayRuler { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool AutoFilterDateGrouping { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool DisplayWhitespace { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int Hwnd => throw new NotImplementedException();


        private void copyAlltoClipboard()
        {
            Clipboard.Clear();
            gridData.SelectAllCells();
            gridData.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, gridData);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Excel.Application excel = new Excel.Application(); //Microsoft Excel Bibliothek  
            excel.Visible = true;
            Workbook wb = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Worksheet sheet1 = (Excel.Worksheet)wb.Sheets[1];

            for (int i = 0; i < gridData.Columns.Count; i++)
            {
                Range rg = (Range)sheet1.Cells[1, i + 1];
                //sheet1.Cells[1, i + 1].Font.Bold = true;


                //sheet1.Columns[i + 1].ColumnWidth = 35;
                rg.Value2 = gridData.Columns[i].Header;
            }
            for (int j = 0; j < gridData.Columns.Count; j++)
            {
                for (int i = 0; i < gridData.Items.Count; i++)
                {
                    TextBlock b = gridData.Columns[j].GetCellContent(gridData.Items[i]) as TextBlock;
                    Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[i + 2, j + 1];
                    range.Value2 = b.Text;
                }
            }
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog choofdlog = new OpenFileDialog();
            choofdlog.Filter = "All Files (*.*)|*.*";
            if (choofdlog.ShowDialog() == DialogResult)
            {
                string sFileName = choofdlog.FileName;
                string path = System.IO.Path.GetFullPath(choofdlog.FileName);
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                DataSet ds = new DataSet();
                Microsoft.Office.Interop.Excel.Workbook wb = excel.Workbooks.Open(path);
                foreach (Microsoft.Office.Interop.Excel.Worksheet ws in wb.Worksheets)
                {
                    System.Data.DataTable td = new System.Data.DataTable();
                    td = await Task.Run(() => formofDataTable(ws));
                    ds.Tables.Add(td);//This will give the DataTable from Excel file in Dataset
                }
                gridData.ItemsSource = ds.Tables[0].DefaultView;
                wb.Close();
            }
        }
        public System.Data.DataTable formofDataTable(Microsoft.Office.Interop.Excel.Worksheet ws)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string worksheetName = ws.Name;
            dt.TableName = worksheetName;
            Microsoft.Office.Interop.Excel.Range xlRange = ws.UsedRange;
            object[,] valueArray = (object[,])xlRange.get_Value(XlRangeValueDataType.xlRangeValueDefault);
            for (int k = 1; k <= valueArray.GetLength(1); k++)
            {
                dt.Columns.Add((string)valueArray[1, k]);  //add columns to the data table.
            }
            object[] singleDValue = new object[valueArray.GetLength(1)]; //value array first row contains column names. so loop starts from 2 instead of 1
            for (int i = 2; i <= valueArray.GetLength(0); i++)
            {
                for (int j = 0; j < valueArray.GetLength(1); j++)
                {
                    if (valueArray[i, j + 1] != null)
                    {
                        singleDValue[j] = valueArray[i, j + 1].ToString();
                    }
                    else
                    {
                        singleDValue[j] = valueArray[i, j + 1];
                    }
                }
                dt.LoadDataRow(singleDValue, System.Data.LoadOption.PreserveChanges);
            }

            return dt;
        }
        }
}
