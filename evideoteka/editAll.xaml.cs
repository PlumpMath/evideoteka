using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace evideoteka
{
    /// <summary>
    /// Interaction logic for editAll.xaml
    /// </summary>
    public partial class editAll : Window
    {
        string tableName;
        List<string> tableFieldNames, tableFieldValues;
        List<glumac> actor;
        List<korisnik> user;
        List<adresa> address;
        List<model> model;
        List<TextBox> listTextBox;
        List<Label> listLabel;
        List<film> movie;
        List<distributer> distributor;
        List<nacionalnost> nationality;
        List<redatelj> director;
        baza db;
        public  string numberID;
        public editAll()
        {
            InitializeComponent();
            
        }
        public editAll(dataToSend dts)
        {
            InitializeComponent();
            tableName = dts.getTableName;
            db = new baza();
            tableFieldNames = new List<string>();
            tableFieldValues = new List<string>();
            listTextBox = new List<TextBox>();
            listLabel = new List<Label>();
            MessageBox.Show(dts.getNumber);
            if (dts.getNumber == "1")
            {
                actor = new List<glumac>();
                actor = db.SelectActor();
                createInterface(actor);
            }
            else if (dts.getNumber == "2")
            {
                model = new List<model>();
                model = db.SelectModel();
                createInterface(model);
            }
            else if (dts.getNumber == "3")
            {
                user = new List<korisnik>();
                user = db.Select();
                createInterface(user);
            }
            else if (dts.getNumber == "4")
            {
                address = new List<adresa>();
                address = db.SelectAddress();
                createInterface(address);
            }
            else if (dts.getNumber == "5")
            {
                movie = new List<film>();
                movie = db.SelectMovie();
                createInterface(movie);
            }
            else if (dts.getNumber == "6")
            {
                distributor = new List<distributer>();
                distributor = db.SelectDistributor();
                createInterface(distributor);
            }
            else if (dts.getNumber == "7")
            {
                nationality = new List<nacionalnost>();
                nationality = db.SelectNationality();
                createInterface(nationality);
            }
            else if (dts.getNumber == "8")
            {
                director = new List<redatelj>();
                director = db.SelectDirector();
                createInterface(director);
            }
            //txtBoxField1.Margin = new Thickness(10, 10, 10, 10);
            //stackPanelMain.Children.Add(txtBoxField1);
            //stackPanelMain.Children.Add(textBoxField2);
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void AddressForm_ButtonClicked(object sender, dataToSend e)
        {
            numberID = e.getNumber;
            MessageBox.Show(numberID);
        }

        public void createInterface<T>(List<T> obj)
        {
            
            foreach (Object k in obj)
            {
                listBoxAll.Items.Add(k);
                
            }

            Type myType = obj[0].GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
            
          //  object propValue = props[1].GetValue(obj[0], null);
            //TextBox txtTest = new TextBox();
            //txtTest.Text = propValue.ToString();
            //stackPanelMain.Children.Add(txtTest);
            var testMe = obj[0].GetType().GetFields(
                            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            string[] names = Array.ConvertAll(testMe, field => field.Name);
            int i = 0;
            foreach (string a in names)
            {
                Label lbColumnName = new Label();
                string ss = a.Split(new char[] { '<', '>' })[1];
                lbColumnName.Content = ss;
                listLabel.Add(lbColumnName);
                tableFieldNames.Add(ss);
                
            }
            foreach (PropertyInfo prop in props)
            {
                object propValue = prop.GetValue(obj[0], null);
                TextBox insideLoop = new TextBox();
                tableFieldValues.Add(propValue.ToString());
                insideLoop.Text = propValue.ToString();
                listTextBox.Add(insideLoop);
                stackPanelMain.Children.Add(insideLoop);
                
            }
        }

        private void listBoxAll_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listTextBox.Clear();
            listLabel.Clear();
            while (stackPanelMain.Children.Count > 0)
                stackPanelMain.Children.RemoveAt(stackPanelMain.Children.Count - 1);
            Object objecto = listBoxAll.SelectedItem;
            Type myType = objecto.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
            foreach (PropertyInfo prop in props)
            {
                object propValue = prop.GetValue(objecto, null);
                TextBox insideLoop = new TextBox();
                insideLoop.Text = propValue.ToString();
                listTextBox.Add(insideLoop);
                stackPanelMain.Children.Add(insideLoop);

            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //Rjesit imena pomocu labela za update
        
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            /*
            string query = "insert into " + tableName + " values (";
            foreach (TextBox tx in listTextBox){
                query += "'";
                query += tx.Text;
                query += "',";
            }
            query = query.Remove(query.Length - 1, 1) + ")";
            MessageBox.Show(query);*/
            tableFieldValues.Clear();
            
            foreach (TextBox childOf in stackPanelMain.Children)
            {
                
                tableFieldValues.Add(childOf.Text);
            }
            string query = "UPDATE " + tableName + " SET ";
            for (int x = 1; x < listTextBox.Count; x++)
            {
                query += tableFieldNames[x] + " = " + "'" + tableFieldValues[x] + "'" + "," +" ";
            }
            query = query.Remove(query.Length - 2);
            query += " WHERE " + tableFieldNames[0] + " = " + tableFieldValues[0];
            db.insert(query);
        }
    }
}
