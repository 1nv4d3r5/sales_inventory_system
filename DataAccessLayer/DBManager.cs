using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BusinessObjects;

namespace DataAccessLayer
{
    public class DBManager
    {
        private SqlConnection Con;
        private string ConString = "Data Source=PAL-VAIO\\SQLEXPRESS;Initial Catalog=salesdb;Integrated Security=True";

        public int InsertIntoTableRegistration(BOAddUser boUser)
        {
            int rowsAffected = 0;
            string query = "INSERT INTO Registration(NameOfUser,UserName,User_Password,ContactNo,Email,JoiningDate,UserType) VALUES('" + boUser.Name + "','" + boUser.Username + "','" + boUser.Password + "','" + boUser.Contact + "','" + boUser.Email + "','"+System.DateTime.Now+"','"+boUser.UserType+"');";
            query += "INSERT INTO Users(Username,User_Password,UserType) VALUES('" + boUser.Username + "','" + boUser.Password + "','"+boUser.UserType+"')";
            Con = new SqlConnection(ConString);
            Con.Open();
            SqlCommand cmd = new SqlCommand(query, Con);

            try
            {
                rowsAffected = cmd.ExecuteNonQuery();         
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                Con.Close();
                Con.Dispose();

            }
            return rowsAffected;
        }

        public int UpdateIntoTableRegistration(BOAddUser boUser)
        {
            int rowsAffected = 0;
            string query = "UPDATE Registration SET User_Password='" + boUser.Password + "',ContactNo='" + boUser.Contact + "',Email='" + boUser.Email + "',NameOfUser='" + boUser.Name + "',UserType='"+boUser.UserType+"' WHERE UserName='" + boUser.Username + "';";
            query += "UPDATE Users SET User_Password='" + boUser.Password + "',UserType='"+boUser.UserType+"' WHERE UserName='" + boUser.Username + "'";
            Con = new SqlConnection(ConString);
            Con.Open();
            SqlCommand cmd = new SqlCommand(query, Con);

            try
            {
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                Con.Close();
                Con.Dispose();

            }
            return rowsAffected;
        }

        public int DeleteFromTableRegistration(BOAddUser BLUserMas)
        {
            int rowsAffected = 0;
            string query = "DELETE FROM Registration WHERE UserName='" + BLUserMas.Username + "'; DELETE FROM Users WHERE UserName='" + BLUserMas.Username + "';";
            Con = new SqlConnection(ConString);
            Con.Open();
            SqlCommand cmd = new SqlCommand(query, Con);

            try
            {
                rowsAffected = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                Con.Close();
                Con.Dispose();

            }
            return rowsAffected;
        }

        public DataTable GetData()
        {
            string sql = "SELECT RTRIM(Username) as [User Name],RTRIM(User_Password) as [Password],RTRIM(NameOfUser) as [Name],RTRIM(ContactNo) as [Contact No],RTRIM(Email) as [Email ID],RTRIM(joiningdate) as [Date Joined],RTRIM(UserType) as [User Type] FROM registration";
            Con = new SqlConnection(ConString);
            DataTable dt = new DataTable();
            try
            {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter(sql, Con);            
            sda.Fill(dt);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                Con.Close();
                Con.Dispose();                
            }
            return dt;
        }

        public string UserLogin(BOAddUser Login)
        {
            string result;
            try
            {
                Con = new SqlConnection(ConString);
                string sql = "SELECT COUNT(*) FROM Users WHERE Username='" + Login.Username + "' AND User_Password='" + Login.Password + "'";
                SqlDataAdapter sda = new SqlDataAdapter(sql, Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                result = dt.Rows[0][0].ToString();
                Con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Con.Close();
                Con.Dispose();
            }
            return result;
        }

        public int ChangeUserPassword(BOAddUser boUser)
        {
            int rowsAffected = 0;
            string query = "UPDATE Users SET User_Password='" + boUser.NewPassword + "' WHERE Username='" + boUser.Username + "' AND User_Password='" + boUser.OldPassword + "'";
            Con = new SqlConnection(ConString);
            Con.Open();
            SqlCommand cmd = new SqlCommand(query, Con);

            try
            {
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                Con.Close();
                Con.Dispose();
            }
            return rowsAffected;
        }

        public int ChangeUserPasswordbyAdmin(BOAddUser boUser)
        {
            int rowsAffected = 0;
            string query = "UPDATE Users SET User_Password='" + boUser.NewPassword + "' WHERE Username='" + boUser.Username + "'";
            Con = new SqlConnection(ConString);
            Con.Open();
            SqlCommand cmd = new SqlCommand(query, Con);
            try
            {
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                Con.Close();
                Con.Dispose();
            }
            return rowsAffected;
        }

        public bool VerifyUserName(BOAddUser User)
        {
            bool result;
            string sql = "SELECT Username FROM Registration WHERE Username='" + User.Username + "'";
            Con = new SqlConnection(ConString);
            Con.Open();
            SqlCommand cmd = new SqlCommand(sql, Con);
            SqlDataReader rdr = null;
            try
            {
                rdr = cmd.ExecuteReader();
                result = rdr.Read();
                if (rdr != null)
                {
                    rdr.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                Con.Close();
                Con.Dispose();
            }
            return result;
        }
        
        public bool VerifyCategoryName(BOProduct Category)
        {
                bool result;
                string sql = "SELECT CategoryName FROM Category WHERE CategoryName='" + Category.CategoryName + "'";
                Con = new SqlConnection(ConString);
                Con.Open();
                SqlCommand cmd = new SqlCommand(sql, Con);
                SqlDataReader rdr = null;
                try
                {
                    rdr = cmd.ExecuteReader();
                    result = rdr.Read();
                        if (rdr != null)
                        {
                            rdr.Close();
                        }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    cmd.Dispose();
                    Con.Close();
                    Con.Dispose();
                }
                return result;
        }

        public int InsertCategoryName(BOProduct Category)
        {
            int RowsAffected = 0;
            string query = "INSERT INTO Category(CategoryName) VALUES('" + Category.CategoryName + "')";
            Con = new SqlConnection(ConString);
            SqlCommand cmd = new SqlCommand(query, Con);
            Con.Open();
            try
            {
                RowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                Con.Close();
                Con.Dispose();
            }
            return RowsAffected;
        }

        public int DeleteCategory(BOProduct Category)
        {
            int RowsAffected = 0;
            string query = "DELETE FROM Category WHERE CategoryName='" + Category.CategoryName + "'";
            Con = new SqlConnection(ConString);
            SqlCommand cmd = new SqlCommand(query, Con);
            Con.Open();
            try
            {
                RowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally 
            {
                cmd.Dispose();
                Con.Close();
                Con.Dispose();
            }
            return RowsAffected;
        }

        public int UpdateCategoryName(BOProduct Category)
        {
            int rowsAffected = 0;
            string query = "UPDATE Category SET CategoryName='" + Category.CategoryName + "' WHERE CategoryName='" + Category.TextBox + "'";
            Con = new SqlConnection(ConString);
            Con.Open();
            SqlCommand cmd = new SqlCommand(query, Con);
            try
            {
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                Con.Close();
                Con.Dispose();
            }
            return rowsAffected;
        }

         public DataTable showCategoryList()
        {
            string sql = "SELECT (CategoryName) as [Category Name] FROM Category ORDER BY CategoryName";
            Con = new SqlConnection(ConString);
            DataTable dt = new DataTable();
            try
            {
                Con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sql, Con);                
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Con.Close();
                Con.Dispose();
            }
            return dt;
        }

         public bool VerifyCompanyName(BOCompany Company)
         {
             bool result;
             string sql = "SELECT CompanyName FROM Company WHERE CompanyName='" + Company.CompanyName + "'";
             Con = new SqlConnection(ConString);
             Con.Open();
             SqlCommand cmd = new SqlCommand(sql, Con);
             SqlDataReader rdr = null;
             try
             {
                 rdr = cmd.ExecuteReader();
                 result = rdr.Read();
                 if (rdr != null)
                 {
                     rdr.Close();
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             finally
             {
                 cmd.Dispose();
                 Con.Close();
                 Con.Dispose();
             }
             return result;
         }

         public int InsertCompanyName(BOCompany Company)
         {
             int RowsAffected = 0;
             string query = "INSERT INTO Company(CompanyName) VALUES('" + Company.CompanyName + "')";
             Con = new SqlConnection(ConString);
             SqlCommand cmd = new SqlCommand(query, Con);
             Con.Open();
             try
             {
                 RowsAffected = cmd.ExecuteNonQuery();
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             finally
             {
                 cmd.Dispose();
                 Con.Close();
                 Con.Dispose();
             }
             return RowsAffected;
         }

         public int DeleteCompany(BOCompany Company)
         {
             int RowsAffected = 0;
             string query = "DELETE FROM Company WHERE CompanyName='" + Company.CompanyName + "'";
             Con = new SqlConnection(ConString);
             SqlCommand cmd = new SqlCommand(query, Con);
             Con.Open();
             try
             {
                 RowsAffected = cmd.ExecuteNonQuery();
              }
             catch (Exception ex)
             {
                 throw ex;
             }
             finally
             {
                 cmd.Dispose();
                 Con.Close();
                 Con.Dispose();
             }
             return RowsAffected;
         }

         public int UpdateCompanyName(BOCompany Company)
         {
             int rowsAffected = 0;
             string query = "UPDATE Company SET CompanyName='" + Company.CompanyName + "' WHERE CompanyName='" + Company.TextBox + "'";
             Con = new SqlConnection(ConString);
             Con.Open();
             SqlCommand cmd = new SqlCommand(query, Con);
             try
             {
                 rowsAffected = cmd.ExecuteNonQuery();
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             finally
             {
                 cmd.Dispose();
                 Con.Close();
                 Con.Dispose();
             }
             return rowsAffected;
         }

         public DataTable showCompanyList()
         {
             string sql = "SELECT (CompanyName) as [Company Name] FROM Company ORDER BY CompanyName";
             Con = new SqlConnection(ConString);
             DataTable dt = new DataTable();
             try
             {
                 Con.Open();
                 SqlDataAdapter sda = new SqlDataAdapter(sql, Con);
                 sda.Fill(dt);
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             finally
             {
                 Con.Close();
                 Con.Dispose();
             }
             return dt;
         }

         public bool VerifyProductName(BOProduct Product)
         {
             bool result;
             string sql = "SELECT ProductName FROM Product WHERE ProductName='" + Product.ProductName + "'";
             Con = new SqlConnection(ConString);
             Con.Open();
             SqlCommand cmd = new SqlCommand(sql, Con);
             SqlDataReader rdr = null;
             try
             {
                 rdr = cmd.ExecuteReader();
                 result = rdr.Read();
                 if (rdr != null)
                 {
                     rdr.Close();
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             finally
             {
                 cmd.Dispose();
                 Con.Close();
                 Con.Dispose();
             }
             return result;
         }

         public int InsertProductName(BOProduct Product)
         {
             int RowsAffected = 0;
             string query = "INSERT INTO Product (ProductName,Category,Company) VALUES('"+Product.ProductName+"','"+Product.ProductCategory+"','"+Product.ProductCompany+"')";
             Con = new SqlConnection(ConString);
             SqlCommand cmd = new SqlCommand(query, Con);
             Con.Open();
             try
             {
                 RowsAffected = cmd.ExecuteNonQuery();
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             finally
             {
                 cmd.Dispose();
                 Con.Close();
                 Con.Dispose();
             }
             return RowsAffected;
         }

         public int DeleteProduct(BOProduct Product)
         {
             int RowsAffected = 0;
             string query = "DELETE FROM Product WHERE ProductName='" + Product.ProductName + "'";
             Con = new SqlConnection(ConString);
             SqlCommand cmd = new SqlCommand(query, Con);
             Con.Open();
             try
             {
                 RowsAffected = cmd.ExecuteNonQuery();
                 Con.Close();
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             finally
             {
                 cmd.Dispose();
                 Con.Close();
                 Con.Dispose();
             }
             return RowsAffected;
         }

         public int UpdateProduct(BOProduct Product)
         {
             int rowsAffected = 0;
             string query = "UPDATE Product SET ProductName='" + Product.ProductName+ "',Category='" + Product.ProductCategory + "',Company='" + Product.ProductCompany + "' WHERE ProductName='" +Product.TextBox + "'";
             Con = new SqlConnection(ConString);
             Con.Open();
             SqlCommand cmd = new SqlCommand(query, Con);
             try
             {
                 rowsAffected = cmd.ExecuteNonQuery();
                 Con.Close();
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             finally
             {
                 cmd.Dispose();
                 Con.Close();
                 Con.Dispose();
             }
             return rowsAffected;
         }

         public DataTable showProductList()
         {
             string sql = "SELECT (ProductName) as [Product Name],(Company) as [Company Name],(Category) as [Category Name] FROM Product ORDER BY ProductName";
             Con = new SqlConnection(ConString);
             DataTable dt = new DataTable();
             try
             {
                 Con.Open();
                 SqlDataAdapter sda = new SqlDataAdapter(sql, Con);
                 sda.Fill(dt);
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             finally
             {
                 Con.Close();
                 Con.Dispose();
             }
             return dt;
         }

         public DataSet FillCategoryCombo()
         {
             string sql = "SELECT CategoryName FROM Category ORDER BY CategoryName";
             Con = new SqlConnection(ConString);
             DataSet ds = new DataSet();
             try
             {
                 Con.Open();
                 SqlDataAdapter sda = new SqlDataAdapter(sql, Con);
                 sda.Fill(ds, "Category");
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             finally
             {
                 Con.Close();
                 Con.Dispose();
             }
             return ds;
         }

         public DataSet FillCompanyCombo()
         {
             string sql = "SELECT CompanyName FROM Company ORDER BY CompanyName";
             Con = new SqlConnection(ConString);
             DataSet ds = new DataSet();
             try
             {
                 Con.Open();
                 SqlDataAdapter sda = new SqlDataAdapter(sql, Con);
                 sda.Fill(ds, "Company");
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             finally
             {
                 Con.Close();
                 Con.Dispose();
             }
             return ds;
         }

         public DataSet FillProductCombo()
         {
             string sql = "SELECT ProductName FROM Product ORDER BY ProductName";
             Con = new SqlConnection(ConString);
             DataSet ds = new DataSet();

             try
             {
                 Con.Open();
                 SqlDataAdapter sda = new SqlDataAdapter(sql, Con);
                 sda.Fill(ds, "Product");
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             finally {
                 Con.Close();
                 Con.Dispose();
             }
             return ds;
         }

         public int InsertIntoConfig(BOProduct Product)
         {
             int RowsAffected = 0;
             string query = "INSERT INTO Config(ProductName,Features,Price,Picture) VALUES('"+Product.ProductName+"','"+Product.Features+"','"+Product.Price+"',@img)";
             Con = new SqlConnection(ConString);
             SqlCommand cmd = new SqlCommand(query, Con);
             Con.Open();
             try
             {
                 SqlParameter param = new SqlParameter("@img", SqlDbType.VarBinary);
                 param.Value = Product.Picture;
                 cmd.Parameters.Add(param);
                 RowsAffected = cmd.ExecuteNonQuery();
              }
             catch (Exception ex)
             {
                 throw ex;
             }
             finally
             {
                 cmd.Dispose();
                 Con.Close();
                 Con.Dispose();
             }
             return RowsAffected;
 
         }

         public DataTable getConfigData1()
         {
             string sql = "SELECT (ConfigID) as [Config ID],(ProductName) as [Product Name],Features,Price,Picture FROM Config ORDER BY ProductName";
             Con = new SqlConnection(ConString);
             SqlDataAdapter sda = new SqlDataAdapter(sql, Con);
             DataTable dt = new DataTable();
             Con.Open();
             try
             {
                 sda.Fill(dt);
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             finally
             {
                 Con.Close();
                 Con.Dispose();
             }
             return dt;

         }

         public int DeleteConfigData1(BOProduct Product)
         {
             int RowsAffected = 0;
             string sql = "DELETE FROM Config WHERE ConfigID='" + Product.ConfigID + "'";
             Con = new SqlConnection(ConString);
             SqlCommand cmd = new SqlCommand(sql, Con);
             Con.Open();
             try
             {
                 RowsAffected = cmd.ExecuteNonQuery();
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             finally
             {
                 cmd.Dispose();
                 Con.Close();
                 Con.Dispose();
             }
             return RowsAffected;
         }

         public int UpdateConfigData1(BOProduct Product)
         {
             int RowsAffected = 0;
             string query = "UPDATE Config SET ProductName='" + Product.ProductName + "',Features='" + Product.Features + "',Price='" + Product.Price + "',Picture=@img WHERE ConfigID='" + Product.ConfigID + "'";
             Con = new SqlConnection(ConString);
             SqlCommand cmd = new SqlCommand(query, Con);
             Con.Open();
             try
             {
                 //cmd.Parameters.AddWithValue("@ProductName", Product.ProductName);
                 //cmd.Parameters.AddWithValue("@Features", Product.Features);
                 //cmd.Parameters.AddWithValue("@Price", Product.Price);
                 SqlParameter param = new SqlParameter("@img", SqlDbType.VarBinary);
                 param.Value = Product.Picture;
                 cmd.Parameters.Add(param);
                 RowsAffected = cmd.ExecuteNonQuery();
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             finally
             {
                 cmd.Dispose();
                 Con.Close();
                 Con.Dispose();
             }
             return RowsAffected;

         }

         public DataTable SearchConfigData1(BOProduct Product)
         {
             string sql = "SELECT (ConfigID) as [Config ID],(ProductName) as [Product Name],Features,Price,Picture FROM Config WHERE ProductName LIKE '"+Product.ProductName+"%' ORDER BY ProductName";
             Con = new SqlConnection(ConString);
             DataTable dt = new DataTable();
             SqlDataAdapter sda = new SqlDataAdapter(sql, Con);
             try
             {
                 Con.Open();
                 sda.Fill(dt);
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             finally
             {
                 Con.Close();
                 Con.Dispose();
             }
             return dt;
         }

         public DataTable SearchInventoryData(BOProduct Product)
         {
             string sql = "SELECT (InventoryID) as [Inventory ID],(Config.ConfigID) as [Config ID],(ProductName) as [Product Name],Features,Price,Quantity,(TotalPrice) as [Total Price],(InventoryDate) as [Inventory Date] FROM Inventory,Config WHERE Inventory.ConfigID = Config.ConfigID AND ProductName LIKE'" + Product.ProductName + "%' ORDER BY ProductName";
             Con = new SqlConnection(ConString);
             DataTable dt = new DataTable();
             SqlDataAdapter sda = new SqlDataAdapter(sql, Con);
             try
             {
                 Con.Open();
                 sda.Fill(dt);
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             finally
             {
                 Con.Close();
                 Con.Dispose();
             }
             return dt;
         }


         public DataTable getInventoryData()
         {
             string sql = "SELECT (InventoryID) as [Inventory ID],(Config.ConfigID) as [Config ID],(ProductName) as [Product Name],Features,Price,Quantity,(TotalPrice) as [Total Price],(InventoryDate) as [Inventory Date] FROM Config,Inventory WHERE Inventory.ConfigID = Config.ConfigID ORDER BY ProductName";
             Con = new SqlConnection(ConString);
             SqlDataAdapter sda = new SqlDataAdapter(sql, Con);
             DataTable dt = new DataTable();
             Con.Open();
             try
             {
                 sda.Fill(dt);
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             finally
             {
                 Con.Close();
                 Con.Dispose();
             }
             return dt;

         }

         public DataSet getCustomerDataRecord2()
         {
             string sql = "SELECT (CustomerID) as [Customer ID],(CustomerName) as [Customer Name],(Address) as [Address],(Place) as [VDC/Municipality],(City) as [City],(Zone) as [Zone],(PostalCode) as [Postal Code],(Phone) as [Phone No.],(MobileNo) as [Mobile No.],(FaxNo) as [Fax No.],(Email) as [Email],(Notes) as [Notes] FROM Customer ORDER BY CustomerName";
             Con = new SqlConnection(ConString);
             DataSet ds = new DataSet();
             try
             {
                 Con.Open();
                 SqlDataAdapter sda = new SqlDataAdapter(sql, Con);
                 sda.Fill(ds, "Customer");
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             finally
             {
                 Con.Close();
                 Con.Dispose();
             }
             return ds;
         }

         public DataSet SearchCustomerDataRecord2(BOProduct Customer)
         {
             string sql = "SELECT (CustomerID) as [Customer ID],(CustomerName) as [Customer Name],(Address) as [Address],(Place) as [VDC/Municipality],(City) as [City],(Zone) as [Zone],(PostalCode) as [Postal Code],(Phone) as [Phone No.],(MobileNo) as [Mobile No.],(FaxNo) as [Fax No.],(Email) as [Email],(Notes) as [Notes] FROM Customer WHERE CustomerName LIKE'"+Customer.TextBox+"%' ORDER BY CustomerName";
             Con = new SqlConnection(ConString);
             DataSet ds = new DataSet();
             try
             {
                 Con.Open();
                 SqlDataAdapter sda = new SqlDataAdapter(sql, Con);
                 sda.Fill(ds, "Customer");
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             finally
             {
                 Con.Close();
                 Con.Dispose();
             }
             return ds;
         }

         public DataSet CustomerReport()
         {
             string sql = "SELECT * FROM Customer ORDER BY CustomerName";
             Con = new SqlConnection(ConString);
             DataSet ds = new DataSet();
             try
             {
                 Con.Open();
                 SqlDataAdapter sda = new SqlDataAdapter(sql, Con);
                 sda.Fill(ds, "Customer");
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             finally
             {
                 Con.Close();
                 Con.Dispose();
             }
             return ds;
         }

         public DataSet InvoiceReport(BOSales Sales)
         {
             SqlConnection Con = new SqlConnection(ConString);    
             SqlCommand cmd = new SqlCommand();
             SqlDataAdapter sda = new SqlDataAdapter();
             DataSet ds = new DataSet();

             try
             {
                 Con.Open();
                 cmd.Connection = Con;
                 cmd.CommandText = "SELECT Config.ConfigID, Config.ProductName, Config.Features, Config.Price, Sales.InvoiceNo, Sales.InvoiceDate, Sales.CustomerID, Sales.SubTotal,Sales.VATPercentage, Sales.VATAmount, Sales.GrandTotal, Sales.TotalPayment, Sales.PaymentDue, Sales.Remarks, ProductSold.ID,ProductSold.InvoiceNo AS Expr1, ProductSold.ConfigID AS Expr2, ProductSold.Quantity, ProductSold.Price AS Expr3, ProductSold.TotalAmount,Customer.CustomerID AS Expr4, Customer.CustomerName, Customer.Address, Customer.Place, Customer.City, Customer.Zone, Customer.PostalCode,Customer.Phone, Customer.MobileNo, Customer.FaxNo, Customer.Email, Customer.Notes FROM (((Customer INNER JOIN Sales ON Customer.CustomerID = Sales.CustomerID) INNER JOIN ProductSold ON Sales.InvoiceNo = ProductSold.InvoiceNo) INNER JOIN Config ON ProductSold.ConfigID = Config.ConfigID) WHERE Sales.InvoiceNo='" + Sales.InvoiceNo + "'";
                 cmd.CommandType = CommandType.Text;
                 sda.SelectCommand = cmd;
                 sda.Fill(ds, "Config");
                 sda.Fill(ds, "Sales");
                 sda.Fill(ds, "ProductSold");
                 sda.Fill(ds, "Customer");
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             finally
             {
                 cmd.Dispose();
                 Con.Close();
                 Con.Dispose();
             }
             return ds;
         }
         public bool VerifyCustomerName(BOCustomer Customer)
         {
             bool result;
             string sql = "SELECT CustomerID FROM Customer WHERE CustomerID='" + Customer.CustomerID + "'";
             Con = new SqlConnection(ConString);
             SqlCommand cmd = new SqlCommand(sql, Con);
             SqlDataReader rdr = null;
             try
             {
                 Con.Open();
                 rdr = cmd.ExecuteReader();
                 result = rdr.Read();
                 if (rdr != null)
                 {
                     rdr.Close();
                 }
                 Con.Close();
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             finally
             {
                 cmd.Dispose();
                 Con.Close();
                 Con.Dispose();
             }
             return result;
         }

         public int InsertIntoCustomer(BOCustomer Customer)
         {
             string sql = "INSERT INTO Customer (CustomerID,CustomerName,Address,Place,City,Zone,PostalCode,Phone,MobileNo,FaxNo,Email,Notes) VALUES(@c_id,@c_name,@c_address,@c_place,@c_city,@c_zone,@c_postalcode,@c_phone,@c_mobile,@c_fax,@c_email,@c_notes)";
             Con = new SqlConnection(ConString);
             SqlCommand cmd = new SqlCommand(sql, Con);
             int RowsAffected = 0;

             cmd.Parameters.Add(new SqlParameter("@c_id", System.Data.SqlDbType.VarChar, 20, "CustomerID"));
             cmd.Parameters.Add(new SqlParameter("@c_name", System.Data.SqlDbType.VarChar, 100, "CustomerName"));
             cmd.Parameters.Add(new SqlParameter("@c_address", System.Data.SqlDbType.VarChar, 250, "Address"));
             cmd.Parameters.Add(new SqlParameter("@c_place", System.Data.SqlDbType.VarChar, 100, "Place"));
             cmd.Parameters.Add(new SqlParameter("@c_city", System.Data.SqlDbType.VarChar, 50, "City"));
             cmd.Parameters.Add(new SqlParameter("@c_zone", System.Data.SqlDbType.VarChar, 15, "Zone"));
             cmd.Parameters.Add(new SqlParameter("@c_postalcode", System.Data.SqlDbType.VarChar, 20, "PostalCode"));
             cmd.Parameters.Add(new SqlParameter("@c_phone", System.Data.SqlDbType.VarChar, 20, "Phone"));
             cmd.Parameters.Add(new SqlParameter("@c_mobile", System.Data.SqlDbType.VarChar, 20, "MobileNo"));
             cmd.Parameters.Add(new SqlParameter("@c_fax", System.Data.SqlDbType.VarChar, 20, "FaxNo"));
             cmd.Parameters.Add(new SqlParameter("@c_email", System.Data.SqlDbType.VarChar, 50, "Email"));
             cmd.Parameters.Add(new SqlParameter("@c_notes", System.Data.SqlDbType.VarChar, 250, "Notes"));

             cmd.Parameters["@c_id"].Value = Customer.CustomerID;
             cmd.Parameters["@c_name"].Value = Customer.CustomerName;
             cmd.Parameters["@c_address"].Value = Customer.CustomerAddress;
             cmd.Parameters["@c_postalcode"].Value = Customer.CustomerPostalCode;
             cmd.Parameters["@c_place"].Value = Customer.CustomerCity;
             cmd.Parameters["@c_city"].Value = Customer.CustomerCity;
             cmd.Parameters["@c_zone"].Value = Customer.CustomerZone;
             cmd.Parameters["@c_phone"].Value = Customer.CustomerPhone;
             cmd.Parameters["@c_mobile"].Value = Customer.CustomerMobile;
             cmd.Parameters["@c_fax"].Value = Customer.CustomerFax;
             cmd.Parameters["@c_email"].Value = Customer.CustomerEmail;
             cmd.Parameters["@c_notes"].Value = Customer.CustomerNotes;
             try 
             {
                 Con.Open();
                 RowsAffected = cmd.ExecuteNonQuery();
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             finally
             {
                 cmd.Dispose();
                 Con.Close();
                 Con.Dispose();
             }
             return RowsAffected;
         }

         public int UpdateCustomer(BOCustomer Customer)
         {
             string sql = "UPDATE Customer SET Customername=@c_name,Address=@c_address,Place=@c_place,City=@c_city,Zone=@c_zone,PostalCode=@c_postalcode,Phone=@c_phone,MobileNo=@c_mobile,FaxNo=@c_fax,Email=@c_email,Notes=@c_notes WHERE CustomerID=@c_id";
             Con = new SqlConnection(ConString);
             SqlCommand cmd = new SqlCommand(sql, Con);
             int RowsAffected = 0;

             cmd.Parameters.Add(new SqlParameter("@c_id", System.Data.SqlDbType.VarChar, 20, "CustomerID"));
             cmd.Parameters.Add(new SqlParameter("@c_name", System.Data.SqlDbType.VarChar, 100, "CustomerName"));
             cmd.Parameters.Add(new SqlParameter("@c_address", System.Data.SqlDbType.VarChar, 250, "Address"));
             cmd.Parameters.Add(new SqlParameter("@c_place", System.Data.SqlDbType.VarChar, 100, "Place"));
             cmd.Parameters.Add(new SqlParameter("@c_city", System.Data.SqlDbType.VarChar, 50, "City"));
             cmd.Parameters.Add(new SqlParameter("@c_zone", System.Data.SqlDbType.VarChar, 15, "Zone"));
             cmd.Parameters.Add(new SqlParameter("@c_postalcode", System.Data.SqlDbType.VarChar, 20, "PostalCode"));
             cmd.Parameters.Add(new SqlParameter("@c_phone", System.Data.SqlDbType.VarChar, 20, "Phone"));
             cmd.Parameters.Add(new SqlParameter("@c_mobile", System.Data.SqlDbType.VarChar, 20, "MobileNo"));
             cmd.Parameters.Add(new SqlParameter("@c_fax", System.Data.SqlDbType.VarChar, 20, "FaxNo"));
             cmd.Parameters.Add(new SqlParameter("@c_email", System.Data.SqlDbType.VarChar, 50, "Email"));
             cmd.Parameters.Add(new SqlParameter("@c_notes", System.Data.SqlDbType.VarChar, 250, "Notes"));

             cmd.Parameters["@c_id"].Value = Customer.CustomerID;
             cmd.Parameters["@c_name"].Value = Customer.CustomerName;
             cmd.Parameters["@c_address"].Value = Customer.CustomerAddress;
             cmd.Parameters["@c_postalcode"].Value = Customer.CustomerPostalCode;
             cmd.Parameters["@c_place"].Value = Customer.CustomerCity;
             cmd.Parameters["@c_city"].Value = Customer.CustomerCity;
             cmd.Parameters["@c_zone"].Value = Customer.CustomerZone;
             cmd.Parameters["@c_phone"].Value = Customer.CustomerPhone;
             cmd.Parameters["@c_mobile"].Value = Customer.CustomerMobile;
             cmd.Parameters["@c_fax"].Value = Customer.CustomerFax;
             cmd.Parameters["@c_email"].Value = Customer.CustomerEmail;
             cmd.Parameters["@c_notes"].Value = Customer.CustomerNotes;
             try
             {
                 Con.Open();
                 RowsAffected = cmd.ExecuteNonQuery();
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             finally
             {
                 cmd.Dispose();
                 Con.Close();
                 Con.Dispose();
             }
             return RowsAffected;
         }

         public int DeleteFromCustomer(BOCustomer Customer)
         {
             int rowsAffected = 0;
             string sql = "DELETE FROM Customer WHERE CustomerID=@c_id";
             Con = new SqlConnection(ConString);
             SqlCommand cmd = new SqlCommand(sql, Con); 
             cmd.Parameters.Add(new SqlParameter("@c_id", System.Data.SqlDbType.VarChar, 20, "CustomerID"));
             cmd.Parameters["@c_id"].Value = Customer.CustomerID;
             try
             {
                 Con.Open();
                 rowsAffected = cmd.ExecuteNonQuery();
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             finally
             {
                 cmd.Dispose();
                 Con.Close();
                 Con.Dispose();
             }
             return rowsAffected;
         }

         public DataSet SalesRecordCombo(BOCustomer Customer)
         {
             string query = "SELECT (InvoiceNO) as [Invoice No.],(InvoiceDate) as [Invoice Date],(Sales.CustomerID) as [Customer ID],(CustomerName) as [Customer Name],(SubTotal) as [Sub Total],(VATPercentage) as [VAT %],(VATAmount) as [VAT Amount],(GrandTotal) as [Grand Total],(TotalPayment) as [Total Payment],(PaymentDue) as [Payment Due],(Remarks) as [Remarks] FROM Sales,Customer WHERE Sales.CustomerID=Customer.CustomerID AND CustomerName='" + Customer.CustomerName + "' ORDER BY CustomerName,InvoiceDate";
             Con = new SqlConnection(ConString);
             DataSet ds = new DataSet();
             try
             {
                 Con.Open();
                 SqlDataAdapter sda = new SqlDataAdapter(query, Con);
                 sda.Fill(ds, "Sales");
                 sda.Fill(ds, "Customer");
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             finally
             {
                 Con.Close();
                 Con.Dispose();
             }
             return ds;
         }

         public DataTable SalesRecordFillCombo()
         {
             string sql = "SELECT DISTINCT CustomerName FROM Sales,Customer WHERE Sales.CustomerID=Customer.CustomerID";
             Con = new SqlConnection(ConString);
             DataTable dt = new DataTable();
             DataSet ds = new DataSet();
             try
             {
                 Con.Open();
                 SqlDataAdapter sda = new SqlDataAdapter(sql,Con);
                 sda.Fill(ds);
                 dt = ds.Tables[0];
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             finally
             {
                 Con.Close();
                 Con.Dispose();
             }
             return dt;
         }

         public DataTable SalesRecordFillList(BOCustomer Customer)
         {
             string sql = "SELECT Config.ConfigID,Config.ProductName,ProductSold.Price,ProductSold.Quantity,ProductSold.TotalAmount FROM Sales,ProductSold,Config WHERE Sales.InvoiceNo=ProductSold.InvoiceNo AND Config.ConfigID=ProductSold.ConfigID and Sales.InvoiceNo='" + Customer.InvoiceNo + "'";
             Con = new SqlConnection(ConString);
             DataTable dt = new DataTable();
             DataSet ds = new DataSet();
             try 
             {
                 Con.Open();
                 SqlDataAdapter sda = new SqlDataAdapter(sql, Con);
                 sda.Fill(ds);
                 dt = ds.Tables[0];
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             finally
             {
                 Con.Close();
                 Con.Dispose();
             }
             return dt;
         }

         public DataTable GetInvoiceData()
         {
             Con = new SqlConnection(ConString);
             string sql = "SELECT (InventoryID) as [Inventory ID],(Config.ConfigID) as [Config ID],(ProductName) as [Product Name],Features,(Price) as [Price(Rs.)],(SUM(Quantity)) as [Quantity] FROM Inventory,Config WHERE Inventory.ConfigID=Config.ConfigID GROUP BY InventoryID,ProductName,Price,Features,Config.ConfigID HAVING SUM(Quantity) > 0 ORDER BY ProductName";
             DataTable dt = new DataTable();
             DataSet ds = new DataSet();
             try 
             {
                 Con.Open();
                 SqlDataAdapter sda = new SqlDataAdapter(sql, Con);
                 sda.Fill(ds);
                 dt = ds.Tables[0];
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             finally
             {
                 Con.Close();
                 Con.Dispose();
             }
             return dt;
         }

         public DataTable SearchInvoiceData(BOProduct Product)
         {
             Con = new SqlConnection(ConString);
             string sql = "SELECT (InventoryID) as [Inventory ID],(Config.ConfigID) as [Config ID],(ProductName) as [Product Name],Features,(Price) as [Price(Rs.)],(SUM(Quantity)) as [Quantity] FROM Inventory,Config WHERE Inventory.ConfigID=Config.ConfigID AND ProductName LIKE '" + Product.ProductName + "%' GROUP BY InventoryID,ProductName,Price,Features,Config.ConfigID HAVING (SUM(Quantity) > 0) ORDER BY ProductName";
             DataTable dt = new DataTable();
             DataSet ds = new DataSet();
             try
             {
                 Con.Open();
                 SqlDataAdapter sda = new SqlDataAdapter(sql, Con);
                 sda.Fill(ds);
                 dt = ds.Tables[0];
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             finally
             {
                 Con.Close();
                 Con.Dispose();
             }
             return dt;
 
         }

         public DataSet SearchInventory(BOProduct Product)
         {
             string sql = "SELECT InventoryID as [Inventory ID], (ProductName) as [Product Name],Features,SUM(Quantity) as [Quantity],Price,SUM(TotalPrice) as [Total Price] FROM Config,Inventory WHERE Config.ConfigID=Inventory.ConfigID and ProductName LIKE '" + Product.ProductName + "%' GROUP BY InventoryID, ProductName,Features,Price HAVING (sum(quantity) > 0) ORDER BY ProductName";
             Con = new SqlConnection(ConString);
             DataSet ds = new DataSet();
             try
             {
                 Con.Open();
                 SqlCommand cmd = new SqlCommand(sql, Con);
                 SqlDataAdapter sda = new SqlDataAdapter(cmd);
                 sda.Fill(ds, "Inventory");
                 sda.Fill(ds, "Config");
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             finally
             {
                 Con.Close();
                 Con.Dispose();
             }
             return ds;
         }

         public DataSet GetInventoryData()
         {
             string sql = "SELECT InventoryID as [Inventory ID],(ProductName) as [Product Name],Features,sum(Quantity) as [Quantity],Price,sum(TotalPrice) as [Total Price] FROM Config,Inventory WHERE Config.ConfigID=Inventory.ConfigID GROUP BY InventoryID, ProductName,Features,Price HAVING (sum(Quantity) > 0) ORDER BY ProductName";
             Con = new SqlConnection(ConString);
             DataSet ds = new DataSet();
             try
             {
                 Con.Open();
                 SqlCommand cmd = new SqlCommand(sql, Con);
                 SqlDataAdapter sda = new SqlDataAdapter(cmd);
                 sda.Fill(ds, "Inventory");
                 sda.Fill(ds, "Config");
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             finally
             {
                 Con.Close();
                 Con.Dispose();
             }
             return ds;
         }

         public int DeleteInvoiceData(BOSales Invoice)
         {
                int RowsAffected = 0;
                Con = new SqlConnection(ConString);
                string sql = "";
                sql = "DELETE FROM ProductSold WHERE InvoiceNo='" + Invoice.InvoiceNo + "';";
                sql += "DELETE FROM Sales WHERE InvoiceNo='" + Invoice.InvoiceNo + "'";
                SqlCommand cmd = new SqlCommand(sql, Con);
                try
                {
                    Con.Open();
                    RowsAffected = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    cmd.Dispose();
                    Con.Close();
                    Con.Dispose();
                }
                return RowsAffected;
         }

        public bool VerifyInventoryName(BOProduct Product)
        {
                bool result;
                string sql = "SELECT ConfigID FROM Inventory WHERE ConfigID='" + Product.ConfigID + "'";
                Con = new SqlConnection(ConString);
                Con.Open();
                SqlCommand cmd = new SqlCommand(sql, Con);
                SqlDataReader rdr = null;
                try
                {
                    rdr = cmd.ExecuteReader();
                    result = rdr.Read();
                        if (rdr != null)
                        {
                            rdr.Close();
                        }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    cmd.Dispose();
                    Con.Close();
                    Con.Dispose();
                }
                return result;
        }

        public int InsertIntoInventory(BOProduct Product,BOInventory Inventory)
        {
            int rowsAffected = 0;
            string sql = "INSERT INTO Inventory(InventoryID,ConfigID,InventoryDate,Quantity,TotalPrice) VALUES('" + Inventory.InventoryID + "','" + Product.ConfigID+ "','" + Inventory.InventoryDate + "','" + Inventory.Quantity + "','" + Inventory.TotalPrice + "')";
            Con = new SqlConnection(ConString);
            SqlCommand cmd = new SqlCommand(sql,Con);
            try
            {
                 Con.Open();
                rowsAffected = cmd.ExecuteNonQuery();         
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                Con.Close();
                Con.Dispose();

            }
            return rowsAffected;
        }

        public int DeleteFromInventory(BOInventory Inventory)
        {
            int rowsAffected = 0;
            string sql = "DELETE FROM Inventory WHERE InventoryID='" + Inventory.InventoryID + "'";
            Con = new SqlConnection(ConString);
            SqlCommand cmd = null;
            try
            {
                Con.Open();   
                cmd = new SqlCommand(sql, Con);
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                Con.Close();
                Con.Dispose();
            }
            return rowsAffected;
        }


        public int UpdateIntoInventory(BOProduct Product,BOInventory Inventory)
        {
            int rowsAffected = 0;
            string sql = "UPDATE Inventory SET ConfigID='" + Product.ConfigID + "',InventoryDate='" + Inventory.InventoryDate + "',Quantity='" + Inventory.Quantity + "',TotalPrice='" + Inventory.TotalPrice + "' WHERE InventoryID='" + Inventory.InventoryID + "'";
            Con = new SqlConnection(ConString);
            SqlCommand cmd = null;
            try
            {
                Con.Open();
                cmd = new SqlCommand(sql, Con);
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                Con.Close();
                Con.Dispose();
            }
            return rowsAffected;
        }

        public DataTable GetProductData()
        {
            string sql = "SELECT (ProductName) as [Product Name],(Company) as [Company Name],(Category) as [Category Name] FROM Product ORDER BY ProductName";
            Con = new SqlConnection(ConString);
            DataTable dt = new DataTable();
            try
            {
                Con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sql, Con);
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Con.Close();
                Con.Dispose();
            }
            return dt;
        }

        public DataTable SearchbyProductName(BOProduct Product)
        {
            string sql = "SELECT (ProductName) as [Product Name],(Company) as [Company Name],(Category) as [Category Name] FROM Product WHERE ProductName LIKE'" + Product.ProductName + "%' ORDER BY ProductName";
            Con = new SqlConnection(ConString);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sql, Con);
            try
            {
                Con.Open();
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Con.Close();
                Con.Dispose();
            }
            return dt;
        }

        public DataTable SearchbyCompanyName(BOProduct Product)
        {
            string sql = "SELECT (ProductName) as [Product name],(Company) as [Company Name],(Category) as [Category Name] FROM Product WHERE Company LIKE'" + Product.ProductCompany + "%' ORDER BY ProductName";
            Con = new SqlConnection(ConString);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sql, Con);
            try
            {
                Con.Open();
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Con.Close();
                Con.Dispose();
            }
            return dt;
        }

        public DataTable SearchbyCategoryName(BOProduct Product)
        {
            string sql = "SELECT (ProductName) as [Product Name],(Company) as [Company Name],(Category) as [Category Name] FROM Product WHERE Category LIKE'" + Product.ProductCategory + "%' ORDER BY ProductName";
            Con = new SqlConnection(ConString);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sql, Con);
            try
            {
                Con.Open();
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Con.Close();
                Con.Dispose();
            }
            return dt;
        }

        public DataTable GetCompanyData()
        {
            string sql = "SELECT CompanyName as 'Company Name' FROM Company ORDER BY CompanyName";
            Con = new SqlConnection(ConString);
            DataTable dt = new DataTable();
            try
            {
                Con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sql, Con);
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Con.Close();
                Con.Dispose();
            }
            return dt;
        }

        public int BackupDatabase(BOBackup Database)
        {
            int rowsAffected = 0;
            Con = new SqlConnection(ConString);
            string sql = "BACKUP DATABASE " + Con.Database + " TO DISK='" + Database.BackupLocation + "\\" + Con.Database + "-" + DateTime.Now.Ticks.ToString() + ".bak'";
            SqlCommand cmd = new SqlCommand();
            try
            {
                Con.Open();                
                cmd = new SqlCommand(sql, Con);
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Con.Close();
                Con.Dispose();
            }
            return rowsAffected;
        }

        public int RestoreDatabase(BOBackup Database)
        {
            int rowsAffected = 0;
            string sql = "";
            Con = new SqlConnection(ConString);
            sql = "ALTER DATABASE " + Con.Database + " SET SINGLE_USER WITH ROLLBACK IMMEDIATE;";
            sql += "RESTORE DATABASE " + Con.Database + " FROM DISK ='" + Database.RestoreLocation + "' WITH REPLACE";
            SqlCommand cmd = new SqlCommand();
            try
            {
                Con.Open();
                cmd = new SqlCommand(sql, Con);
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Con.Close();
                Con.Dispose();
            }
            return rowsAffected;
        }

        public int SaveStoreImage(BOProduct Product)
        {
            int RowsAffected = 0;
            string query = "INSERT INTO Store(Image) VALUES(@img)";
            Con = new SqlConnection(ConString);
            SqlCommand cmd = new SqlCommand(query, Con);           
            try
            {
                Con.Open();
                SqlParameter param = new SqlParameter("@img", SqlDbType.VarBinary);
                param.Value = Product.Picture;
                cmd.Parameters.Add(param);
                RowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                Con.Close();
                Con.Dispose();
            }
            return RowsAffected;

        }

        public byte[] GetImageData()
        {
            byte[] buffer;
            string query = "SELECT Image from Store";
            Con = new SqlConnection(ConString);
            try 
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand(query, Con);
                buffer = (byte[])cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Con.Close();
                Con.Dispose();
            }
            return buffer;
        }

        public int DeleteStoreImage()
        {
            int RowsAffected = 0;
            string query = "TRUNCATE TABLE Store";
            Con = new SqlConnection(ConString);
            SqlCommand cmd = new SqlCommand(query, Con);
            try
            {
                Con.Open();                
                RowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                Con.Close();
                Con.Dispose();
            }
            return RowsAffected;
        }

        public bool VerifyInvoiceNumber(BOSales Invoice)
        {
            bool result;
            string sql = "SELECT InvoiceNo FROM Sales WHERE InvoiceNo=@num";
            Con = new SqlConnection(ConString);
            SqlCommand cmd = new SqlCommand(sql, Con);
            SqlDataReader rdr = null;
            cmd.Parameters.Add(new SqlParameter("@num", System.Data.SqlDbType.VarChar, 20, "InvoiceNo"));
            cmd.Parameters["@num"].Value = Invoice.InvoiceNo;
            try
            {
                Con.Open();
                rdr = cmd.ExecuteReader();
                result = rdr.Read();
                if (rdr != null)
                {
                    rdr.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                Con.Close();
                Con.Dispose();
            }
            return result;
        }

        public void InsertIntoSales(BOSales Sales,BOCustomer Customer)
        { 
            string sql = "INSERT INTO Sales(InvoiceNo,InvoiceDate,CustomerID,SubTotal,VATPercentage,VATAmount,GrandTotal,TotalPayment,PaymentDue,Remarks) VALUES (@invoiceno,@invoicedate,@custid,@subtotal,@vatpercent,@vatamount,@grandtotal,@totalpayment,@paymentdue,@remarks)";
            Con = new SqlConnection(ConString);
            SqlCommand cmd = new SqlCommand(sql, Con);

            cmd.Parameters.Add(new SqlParameter("@invoiceno",SqlDbType.VarChar,20,"InvoiceNo"));
            cmd.Parameters.Add(new SqlParameter("@invoicedate", SqlDbType.VarChar, 50, "InvoiceDate"));
            cmd.Parameters.Add(new SqlParameter("@custid", SqlDbType.VarChar, 20, "CustomerID"));
            cmd.Parameters.Add(new SqlParameter("@subtotal", SqlDbType.VarChar, 50, "SubTotal"));
            cmd.Parameters.Add(new SqlParameter("@vatpercent", SqlDbType.VarChar, 20, "VATPercentage"));
            cmd.Parameters.Add(new SqlParameter("@vatamount", SqlDbType.VarChar, 50, "VATAmount"));
            cmd.Parameters.Add(new SqlParameter("@grandtotal", SqlDbType.VarChar, 50, "GrandTotal"));
            cmd.Parameters.Add(new SqlParameter("@totalpayment", SqlDbType.VarChar, 50, "TotalPayment"));
            cmd.Parameters.Add(new SqlParameter("@paymentdue", SqlDbType.VarChar, 50, "PaymentDue"));
            cmd.Parameters.Add(new SqlParameter("@remarks", SqlDbType.VarChar, 100, "Remarks"));

            cmd.Parameters["@invoiceno"].Value = Sales.InvoiceNo;
            cmd.Parameters["@invoicedate"].Value = Sales.InvoiceDate;
            cmd.Parameters["@custid"].Value = Customer.CustomerID;
            cmd.Parameters["@subtotal"].Value = Sales.SubTotal;
            cmd.Parameters["@vatpercent"].Value = Sales.VATPercent;
            cmd.Parameters["@vatamount"].Value = Sales.VATAmount;
            cmd.Parameters["@grandtotal"].Value = Sales.GrandTotal;
            cmd.Parameters["@totalpayment"].Value = Sales.TotalPayment;
            cmd.Parameters["@paymentdue"].Value = Sales.PaymentDue;
            cmd.Parameters["@remarks"].Value = Sales.Remarks;

            try
            {
                Con.Open();
                cmd.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                cmd.Dispose();
                Con.Close();
                Con.Dispose();
            }

        }

        public void InsertIntoProductSold(BOSales Sales, BOProduct Product)
        {
            string sql = "INSERT INTO ProductSold(InvoiceNo,ConfigID,Quantity,Price,TotalAmount) VALUES (@invoiceno,@configid,@quantity,@price,@totalamount)";
            Con = new SqlConnection(ConString);
            SqlCommand cmd = new SqlCommand(sql, Con);

            cmd.Parameters.Add(new SqlParameter("@invoiceno", SqlDbType.VarChar, 20, "InvoiceNo"));
            cmd.Parameters.Add(new SqlParameter("@configid", SqlDbType.VarChar, 20, "ConfigID"));
            cmd.Parameters.Add(new SqlParameter("@quantity", SqlDbType.VarChar, 50, "Quantity"));
            cmd.Parameters.Add(new SqlParameter("@price", SqlDbType.VarChar, 20, "Price"));
            cmd.Parameters.Add(new SqlParameter("@totalamount", SqlDbType.VarChar, 50, "TotalAmount"));

            cmd.Parameters["@invoiceno"].Value = Sales.InvoiceNo;
            cmd.Parameters["@configid"].Value = Product.ConfigID;
            cmd.Parameters["@quantity"].Value = Product.ProductQuantity;
            cmd.Parameters["@price"].Value = Product.Price;
            cmd.Parameters["@totalamount"].Value = Product.TotalAmount;

            try
            {
                Con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                cmd.Dispose();
                Con.Close();
                Con.Dispose();
            }

        }

        public void UpdateInventoryQuantity(BOInventory Inventory,BOProduct Product)
        {
            string sql = "UPDATE Inventory SET Quantity = Quantity - " + Inventory.Quantity + " WHERE ConfigID= " + Product.ConfigID + "";
            Con = new SqlConnection(ConString);
            SqlCommand cmd = new SqlCommand(sql, Con);
            try
            {
                Con.Open();
                cmd.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                cmd.Dispose();
                Con.Close();
                Con.Dispose();
            }
        }

        public void UpdateInventoryTotalPrice(BOInventory Inventory, BOProduct Product)
        {
            string sql = "UPDATE Inventory SET TotalPrice = TotalPrice - '" + Inventory.TotalPrice + "' WHERE ConfigID= " + Product.ConfigID + "";
            Con = new SqlConnection(ConString);
            SqlCommand cmd = new SqlCommand(sql, Con);
            try
            {
                Con.Open();
                cmd.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                cmd.Dispose();
                Con.Close();
                Con.Dispose();
            }
        }

        public void UpdateSales(BOSales Sales)
        {
            string sql = "UPDATE Sales SET GrandTotal= " + Sales.GrandTotal + ",TotalPayment= " + Sales.TotalPayment + ",PaymentDue= " + Sales.PaymentDue + ",Remarks='" + Sales.Remarks + "' WHERE InvoiceNo= '" + Sales.InvoiceNo + "'";
            Con = new SqlConnection(ConString);
            SqlCommand cmd = new SqlCommand(sql, Con);
            try
            {
                Con.Open();
                cmd.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                cmd.Dispose();
                Con.Close();
                Con.Dispose();
            }           
        }

        public void UpdateProductSold(BOSales Sales, BOProduct Product)
        {
            string sql = "UPDATE ProductSold SET Quantity=" + Product.ProductQuantity + ",Price= " + Product.Price + ",TotalAmount= " + Product.TotalAmount + " WHERE InvoiceNo='" + Sales.InvoiceNo + "' AND ConfigID= " + Product.ConfigID + "";
            Con = new SqlConnection(ConString);
            SqlCommand cmd = new SqlCommand(sql, Con);
            try
            {
                Con.Open();
                cmd.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                cmd.Dispose();
                Con.Close();
                Con.Dispose();
            }     
        }

        public DataSet SalesReport()
        {
            string sql = "SELECT Sales.InvoiceNo, Sales.InvoiceDate, Sales.CustomerID, Sales.SubTotal, Sales.VATPercentage, Sales.VATAmount, Sales.GrandTotal, Sales.TotalPayment,Sales.PaymentDue, Sales.Remarks, Customer.CustomerID AS Expr1, Customer.CustomerName, Customer.Address, Customer.Place, Customer.City, Customer.Zone, Customer.PostalCode, Customer.Phone, Customer.MobileNo, Customer.FaxNo, Customer.Email, Customer.Notes FROM (Sales INNER JOIN Customer ON Sales.CustomerID = Customer.CustomerID) ORDER BY InvoiceDate";
            Con = new SqlConnection(ConString);
            DataSet ds = new DataSet();
            try
            {
                Con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sql, Con);
                sda.Fill(ds, "Sales");
                sda.Fill(ds, "Customer");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Con.Close();
                Con.Dispose();
            }
            return ds;
        }

        public DataSet SalesReportByCustomer(BOCustomer Customer)
        {
            string sql = "SELECT Sales.InvoiceNo, Sales.InvoiceDate, Sales.CustomerID, Sales.SubTotal, Sales.VATPercentage, Sales.VATAmount, Sales.GrandTotal, Sales.TotalPayment,Sales.PaymentDue, Sales.Remarks, Customer.CustomerID AS Expr1, Customer.CustomerName, Customer.Address, Customer.Place, Customer.City, Customer.Zone, Customer.PostalCode, Customer.Phone, Customer.MobileNo, Customer.FaxNo, Customer.Email, Customer.Notes FROM (Sales INNER JOIN Customer ON Sales.CustomerID = Customer.CustomerID) WHERE CustomerName='"+Customer.CustomerName+"' ORDER BY CustomerName,InvoiceDate";
            Con = new SqlConnection(ConString);
            DataSet ds = new DataSet();
            try
            {
                Con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sql, Con);
                sda.Fill(ds, "Sales");
                sda.Fill(ds, "Customer");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Con.Close();
                Con.Dispose();
            }
            return ds;
        }

        public int GetUserType(BOAddUser Login)
        {
            int res = 0;
            string sql = "SELECT UserType FROM Users WHERE Username='"+Login.Username+"'";
            Con = new SqlConnection(ConString);
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand(sql, Con);
                res = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Con.Close();
                Con.Dispose();
            }
            return res;
        }
}
}


