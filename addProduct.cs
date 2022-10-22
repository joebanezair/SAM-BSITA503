using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class frmAddProduct : Form
    {
        class CurrenceyFormatExeption : Exception
        {
            public CurrenceyFormatExeption(string price) : base(price) { }
        }
        class NumberFormatExeption : Exception {
            public NumberFormatExeption(string quantity) : base(quantity) { }
        }
        class StringFormatExeption : Exception
        {
            public StringFormatExeption(string name) : base(name) { }
        }

        private int _Quantity;
        private double _SellingPrice;
        private string _ProductName, _Category, _ManufacturingDate, _ExpirationDate, _Description;

        public frmAddProduct()
        {
            InitializeComponent();
        }

        public frmAddProduct(string ProductName, string Category, string MfgDate, string ExpDate,
        double Price, int Quantity, string Description)
        {
            this._Quantity = Quantity;
            this._SellingPrice = Price;
            this._ProductName = ProductName;
            this._Category = Category;
            this._ManufacturingDate = MfgDate;
            this._ExpirationDate = ExpDate;
            this._Description = Description;
        }


        public string productName
        {
            get
            {
                return this._ProductName;
            }
            set
            {
                this._ProductName = value;
            }
        }
        public string category
        {
            get
            {
                return this._Category;
            }
            set
            {
                this._Category = value;
            }
        }
        public string manufacturingDate
        {
            get
            {
                return this._ManufacturingDate;
            }
            set
            {
                this._ManufacturingDate = value;
            }
        }
        public string expirationDate
        {
            get
            {
                return this._ExpirationDate;
            }
            set
            {
                this._ExpirationDate = value;
            }
        }
        public string description
        {
            get
            {
                return this._Description;
            }
            set
            {
                this._Description = value;
            }
        }
        public int quantity
        {
            get
            {
                return this._Quantity;
            }
            set
            {
                this._Quantity = value;
            }
        }
        public double sellingPrice
        {
            get
            {
                return this._SellingPrice;
            }
            set
            {
                this._SellingPrice = value;
            }
        }

        private void frmAddProduct_Load(object sender, EventArgs e)
        {
            gridViewProductList.ColumnCount = 7;
            gridViewProductList.Columns[0].Name = "Product Name";
            gridViewProductList.Columns[1].Name = "Category";
            gridViewProductList.Columns[2].Name = "Mfg. Date";

            gridViewProductList.Columns[3].Name = "Exp. Date";
            gridViewProductList.Columns[4].Name = "Description";
            gridViewProductList.Columns[5].Name = "Quantity";
            gridViewProductList.Columns[6].Name = "Selling Price";

            string[] apple = { "Beverages", "Bread/Bakery", "Canned/Jarred Goods",
                "Dairy", "Frozen Goods", "Meat", "Personal Care", "Other" };
            foreach (string a in apple) { cbCategory.Items.Add(a); }
        }
       
        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            ArrayList row = new ArrayList();
            _ProductName = Product_Name(txtProductName.Text);
            _Category = cbCategory.Text;
            _ManufacturingDate = dtPickerMfgDate.Value.ToString("yyyy-MM-dd");
            _ExpirationDate = dtPickerExpDate.Value.ToString("yyyy-MM-dd");
            _Description = richTxtDescription.Text;
            _Quantity = Quantity(txtQuantity.Text);
            _SellingPrice = SellingPrice(txtSellPrice.Text);

            gridViewProductList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            row = new ArrayList();
            row.Add(_ProductName);
            row.Add(_Category);
            row.Add(_ManufacturingDate);

            row.Add(_ExpirationDate);
            row.Add(_Description);

            row.Add(_Quantity);
            row.Add(_SellingPrice);
            gridViewProductList.Rows.Add(row.ToArray());
        }

        public string Product_Name(string name)
        {
            try
            {
                if (!Regex.IsMatch(name, @"^[a-zA-Z]+$"))
                {
                    throw new StringFormatExeption(name);
                }

            }catch(StringFormatExeption sfe){
                MessageBox.Show("String format input in product name." + sfe.Message);
            }
            finally
            {
                Console.WriteLine("Input Error");
            }
            return Name;
        }
        public int Quantity(string qty)
        {
            try
            {
                if (!Regex.IsMatch(qty, @"^[0-9]"))
                    //Exception here
                    throw new NumberFormatExeption(qty);
            }
            catch(NumberFormatExeption nfe)
            {
                MessageBox.Show("Number format input in quantity : " + nfe.Message);
            }
            return Convert.ToInt32(qty);

        }
        public double SellingPrice(string price)
        {
            try
            {
                if (!Regex.IsMatch(price.ToString(), @"^(\d*\.)?\d+$"))
                {
                    throw new CurrenceyFormatExeption(price);
                }

            } catch(CurrenceyFormatExeption cxp){
                MessageBox.Show("Currency Format input in price " + cxp.Message);
            }
            return Convert.ToDouble(price);
            
        }
    }
}
