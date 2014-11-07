using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using DataAccessLayer;
using BusinessObjects;

namespace BusinessLogicLayer
{
    public class BusinessManager
    {
        public int BALadduser(BOAddUser BLUserMas)
        {
            DBManager DALObject = new DBManager();
            return DALObject.InsertIntoTableRegistration(BLUserMas);
        }

        public int BALupdateuser(BOAddUser BLUserMas)
        {
            DBManager DALObject = new DBManager();
            return DALObject.UpdateIntoTableRegistration(BLUserMas);
        }

        public int BALdeleteuser(BOAddUser delete)
        {
            DBManager deleteObject = new DBManager();
            return deleteObject.DeleteFromTableRegistration(delete);
        }

        public DataTable GetUserData()
        {
            DBManager dal = new DBManager();
            return dal.GetData();
        }

        public string BALCheckLogin(BOAddUser BLUserMas)
        {
            DBManager dal = new DBManager();
            return dal.UserLogin(BLUserMas);
        }

        public int BALChangePassword(BOAddUser BLUserMas)
        {
            DBManager DALObject = new DBManager();
            return DALObject.ChangeUserPassword(BLUserMas);
        }

        public int BALChangeUserPasswordbyAdmin(BOAddUser BLUserMas)
        {
            DBManager DALObject = new DBManager();
            return DALObject.ChangeUserPasswordbyAdmin(BLUserMas);
        }

        public bool BALVerifyUserName(BOAddUser User)
        {
            DBManager DALObject = new DBManager();
            return DALObject.VerifyUserName(User);
        }

        public bool BALVerifyCategoryName(BOProduct Category)
        {
            DBManager DALObject = new DBManager();
            return DALObject.VerifyCategoryName(Category);
        }

        public int BALInsertCategory(BOProduct Category)
        {
            DBManager DALObject = new DBManager();
            return DALObject.InsertCategoryName(Category);
        }

        public int BALDeleteCategory(BOProduct Category)
        {
            DBManager DALObject = new DBManager();
            return DALObject.DeleteCategory(Category);
        }

        public int BALUpdateCategory(BOProduct Category)
        {
            DBManager DALObject = new DBManager();
            return DALObject.UpdateCategoryName(Category);
        }

        public DataTable BALShowCategoryList()
        {
            DBManager DALObject = new DBManager();
            return DALObject.showCategoryList();
        }

        public bool BALVerifyCompanyName(BOCompany Company)
        {
            DBManager DALObject = new DBManager();
            return DALObject.VerifyCompanyName(Company);
        }

        public int BALInsertCompany(BOCompany Company)
        {
            DBManager DALObject = new DBManager();
            return DALObject.InsertCompanyName(Company);
        }

        public int BALDeleteCompany(BOCompany Company)
        {
            DBManager DALObject = new DBManager();
            return DALObject.DeleteCompany(Company);
        }

        public int BALUpdateCompany(BOCompany Company)
        {
            DBManager DALObject = new DBManager();
            return DALObject.UpdateCompanyName(Company);
        }

        public DataTable BALShowCompanyList()
        {
            DBManager DALObject = new DBManager();
            return DALObject.showCompanyList();
        }

        public bool BALVerifyProductName(BOProduct Product)
        {
            DBManager DALObject = new DBManager();
            return DALObject.VerifyProductName(Product);
        }

        public int BALInsertProductName(BOProduct Product)
        {
            DBManager DALObject = new DBManager();
            return DALObject.InsertProductName(Product);
        }

        public int BALDeleteProduct(BOProduct Product)
        {
            DBManager DALObject = new DBManager();
            return DALObject.DeleteProduct(Product);
        }

        public int BALUpdateProduct(BOProduct Product)
        {
            DBManager DALObject = new DBManager();
            return DALObject.UpdateProduct(Product);
        }

        public DataTable BALShowProductList()
        {
            DBManager DALObject = new DBManager();
            return DALObject.showProductList();
        }

        public DataSet BALFillCategoryCombo()
        {
            DBManager DALObject = new DBManager();
            return DALObject.FillCategoryCombo();
        }

        public DataSet BALFillCompanyCombo()
        {
            DBManager DALObject = new DBManager();
            return DALObject.FillCompanyCombo();
        }

        public DataSet BALFillProductCombo()
        {
            DBManager DALObject = new DBManager();
            return DALObject.FillProductCombo();
        }

        public void PassImageToDatabase(Image img)
        {
            byte[] bytes = ImageToByteArray(img);
            DBManager DALOBject = new DBManager();
            BOProduct BO = new BOProduct();
            DALOBject.InsertIntoConfig(BO);
        }

        public byte[] ImageToByteArray(Image image)
        {
            //convert image to byte[]
            MemoryStream ms = new MemoryStream();
            //Bitmap bmpImage = new Bitmap(image);
            //bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.GetBuffer();
        }

        public Image BbyteArrayToImage(byte[] byteArrayIn)
        {
            //when you have to convert byte[]back to image
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public int BALInsertIntoConfig(BOProduct Product)
        {
            DBManager DALObject = new DBManager();
            return DALObject.InsertIntoConfig(Product);
        }

        public DataTable BALgetConfigData1()
        {
            DBManager DALObject = new DBManager();
            return DALObject.getConfigData1();
        }

        public int BALDeleteConfigData1(BOProduct Product)
        {
            DBManager DALObject = new DBManager();
            return DALObject.DeleteConfigData1(Product);
        }

        public int BALUpdateConfigData1(BOProduct Product)
        {
            DBManager DALObject = new DBManager();
            return DALObject.UpdateConfigData1(Product);
        }

        public DataTable BALSearchConfigData1(BOProduct Product)
        {
            DBManager DALObject = new DBManager();
            return DALObject.SearchConfigData1(Product);
        }

        public DataTable BALSearchInventoryData(BOProduct Product)
        {
            DBManager DALObject = new DBManager();
            return DALObject.SearchInventoryData(Product);
        }

        public DataTable BALgetInventoryData()
        {
            DBManager DALObject = new DBManager();
            return DALObject.getInventoryData();
        }

        public DataSet BALgetCustomerDataRecord2()
        {
            DBManager DALObject = new DBManager();
            return DALObject.getCustomerDataRecord2();
        }

        public DataSet BALSearchCustomerDataRecord2(BOProduct Customer)
        {
            DBManager DALObject = new DBManager();
            return DALObject.SearchCustomerDataRecord2(Customer);
        }

        public DataSet BALCustomerReport()
        {
            DBManager DALObject = new DBManager();
            return DALObject.CustomerReport();
        }

        public bool BALVerifyCustomerName(BOCustomer Customer)
        {
            DBManager DALObject = new DBManager();
            return DALObject.VerifyCustomerName(Customer);
        }

        public int BALInsertIntoCustomer(BOCustomer Customer)
        {
            DBManager DALObject = new DBManager();
            return DALObject.InsertIntoCustomer(Customer);
        }

        public int BALUpdateCustomer(BOCustomer Customer)
        {
            DBManager DALObject = new DBManager();
            return DALObject.UpdateCustomer(Customer);
        }

        public DataSet BALSalesRecordCombo(BOCustomer Customer)
        {
            DBManager DALObject = new DBManager();
            return DALObject.SalesRecordCombo(Customer);
        }

        public DataTable BALSalesRecordFillCombo()
        {
            DBManager DALObject = new DBManager();
            return DALObject.SalesRecordFillCombo();
        }

        public DataTable BALSalesRecordFillList(BOCustomer Customer)
        {
            DBManager DALObject = new DBManager();
            return DALObject.SalesRecordFillList(Customer);
        }

        public DataTable BALGetInvoiceData()
        {
            DBManager DALObject = new DBManager();
            return DALObject.GetInvoiceData();
        }

        public int BALDeleteInvoiceData(BOSales Invoice)
        {
            DBManager DALObject = new DBManager();
            return DALObject.DeleteInvoiceData(Invoice);
        }

        public DataTable BALSearchInvoiceData(BOProduct Product)
        {
            DBManager DALObject = new DBManager();
            return DALObject.SearchInvoiceData(Product);
        }
        public DataSet BALSearchInventory(BOProduct Product)
        {
            DBManager DALObject = new DBManager();
            return DALObject.SearchInventory(Product);
        }

        public DataSet BALGetInventoryData()
        {
            DBManager DALObject = new DBManager();
            return DALObject.GetInventoryData();
        }

        public bool BALVerifyInventoryName(BOProduct Product)
        {
            DBManager DALObject = new DBManager();
            return DALObject.VerifyInventoryName(Product);
        }

        public int BALInsertIntoInventory(BOProduct Product, BOInventory Inventory)
        {
            DBManager DALObject = new DBManager();
            return DALObject.InsertIntoInventory(Product, Inventory);
        }

        public int BALDeleteFromInventory(BOInventory Inventory)
        {
            DBManager DALObject = new DBManager();
            return DALObject.DeleteFromInventory(Inventory);
        }

        public int BALUpdateIntoInventory(BOProduct Product, BOInventory Inventory)
        {
            DBManager DALObject = new DBManager();
            return DALObject.UpdateIntoInventory(Product, Inventory);
        }

        public DataTable BALGetProductData()
        {
            DBManager DALObject = new DBManager();
            return DALObject.GetProductData();
        }

        public DataTable BALSearchbyProductName(BOProduct Product)
        {
            DBManager DALObject = new DBManager();
            return DALObject.SearchbyProductName(Product);
        }

        public DataTable BALSearchbyCompanyName(BOProduct Product)
        {
            DBManager DALObject = new DBManager();
            return DALObject.SearchbyCompanyName(Product);
        }

        public DataTable BALSearchbyCategoryName(BOProduct Product)
        {
            DBManager DALObject = new DBManager();
            return DALObject.SearchbyCategoryName(Product);
        }

        public DataTable BALGetCompanyData()
        {
            DBManager DALObject = new DBManager();
            return DALObject.GetCompanyData();
        }

        public int BALDeleteFromCustomer(BOCustomer Customer)
        {
            DBManager DALObject = new DBManager();
            return DALObject.DeleteFromCustomer(Customer);
        }

        public int BALBackupDatabase(BOBackup Database)
        {
            DBManager DALObject = new DBManager();
            return DALObject.BackupDatabase(Database);
        }

        public int BALRestoreDatabase(BOBackup Database)
        {
            DBManager DALObject = new DBManager();
            return DALObject.RestoreDatabase(Database);
        }

        public int BALSaveStoreImage(BOProduct Product)
        {
            DBManager DALObject = new DBManager();
            return DALObject.SaveStoreImage(Product);
        }

        public byte[] BALGetImageData()
        {
            DBManager DALObject = new DBManager();
            return DALObject.GetImageData();
        }

        public int BALDeleteStoreImage()
        {
            DBManager DALObject = new DBManager();
            return DALObject.DeleteStoreImage();
        }

        public bool BALVerifyInvoiceNumber(BOSales Invoice)
        {
            DBManager DALObject = new DBManager();
            return DALObject.VerifyInvoiceNumber(Invoice);
        }

        public void BALInsertIntoSales(BOSales Sales, BOCustomer Customer)
        {
            DBManager DALObject = new DBManager();
            DALObject.InsertIntoSales(Sales, Customer);
        }

        public void BALInsertIntoProductSold(BOSales Sales, BOProduct Product)
        {
            DBManager DALObject = new DBManager();
            DALObject.InsertIntoProductSold(Sales, Product);
        }

        public void BALUpdateInventoryQuantity(BOInventory Inventory, BOProduct Product)
        {
            DBManager DALObject = new DBManager();
            DALObject.UpdateInventoryQuantity(Inventory, Product);
        }

        public void BALUpdateInventoryTotalPrice(BOInventory Inventory, BOProduct Product)
        {
            DBManager DALObject = new DBManager();
            DALObject.UpdateInventoryTotalPrice(Inventory, Product);
        }

        public void BALUpdateSales(BOSales Sales)
        {
            DBManager DALObject = new DBManager();
            DALObject.UpdateSales(Sales);
        }

        public void BALUpdateProductSold(BOSales Sales, BOProduct Product)
        {
            DBManager DALObject = new DBManager();
            DALObject.UpdateProductSold(Sales, Product);
        }

        public DataSet BALInvoiceReport(BOSales Sales)
        {
            DBManager DALObject = new DBManager();
            return DALObject.InvoiceReport(Sales);
        }

        public DataSet BALSalesReport()
        {
            DBManager DALObject = new DBManager();
            return DALObject.SalesReport();
        }

        public DataSet BALSalesReportByCustomer(BOCustomer Customer)
        {
            DBManager DALObject = new DBManager();
            return DALObject.SalesReportByCustomer(Customer);
        }

        public int GetUserType(BOAddUser Login)
        {
            DBManager DALObject = new DBManager();
            return DALObject.GetUserType(Login);
        }

        public bool IsEmail(BOCustomer Customer)
        {
            string MatchEmailPattern =@"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"+ @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."+ @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"+ @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";
            if (Customer.CustomerEmail != null)
            {
                return Regex.IsMatch(Customer.CustomerEmail, MatchEmailPattern);
            }
            else
            {
                return false;
            }
        }

    }
}
