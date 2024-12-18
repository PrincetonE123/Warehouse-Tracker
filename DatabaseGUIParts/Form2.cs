using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatabaseGUIParts
{
    public partial class Form2 : Form
    {
        string connectionString = "Server=NEURALYNX;Initial Catalog=WHMngmntSystem;Integrated Security=True;";

        BindingSource EmployeeBindingSource = new BindingSource();

        public Form2()
        {
            InitializeComponent();
            LoadEmployeeData();
            label1.Text = "Welcome, " + SharedData.currentUser + "!";
            dataGridView1.ReadOnly = true;
            viewInventory.Visible = !(SharedData.permissionLevel < 1);
            viewMachine.Visible = !(SharedData.permissionLevel < 1);
            viewFloor.Visible = !(SharedData.permissionLevel < 3);
            viewWarehouse.Visible = !(SharedData.permissionLevel < 4);
            viewRegion.Visible = (SharedData.permissionLevel == 5);
        }


        private void Form2_Load(object sender, EventArgs e) // This runs when Form2 is loaded
        {
            LoadEmployeeData();
        }


        private void LoadEmployeeData()
        {
            EmployeeDataDAO dao = new EmployeeDataDAO();

            SharedData.permissionLevel = dao.getPermissionLevel(SharedData.currentUser);  // initializes public int permissionLevel
            SharedData.employeeID = dao.getEmployeeID(SharedData.currentUser);            // sets the current Employee ID to the current user's EmplyoeeID

            EmployeeDataDAO employeeDataDAO = new EmployeeDataDAO();

        }


        private void button3_Click(object sender, EventArgs e) //logout button, will logout user
        {//Sign Out button
            Form1 form1 = new Form1();
            form1.Show();
            this.Close();
            dataGridView1.DataSource = null;
            SharedData.currentUser = "";
            SharedData.permissionLevel = -1;
            SharedData.employeeID = -1;
        }


        private void button1_Click(object sender, EventArgs e)  // load database button probably
        {

        }
        private void Form2_Load_1(object sender, EventArgs e)
        {

        }


        private void viewInventory_Click(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            label1.Text = "Your Inventories";
            string query = "";
            //determine the query for Inventory dependent on Permission Level and EmployeeID

            DataTable myTable = new DataTable();
            switch (SharedData.permissionLevel)
            {
                default://deny by default
                    label1.Text = "Sorry! You don't have permission to view these tables :(";
                    return;

                case 1://Technician Wants to View the Inventory of their Warehouse
                    query = "SELECT DISTINCT PartNumber, VendorName, Description," +
                                    " UnitCost, Quantity FROM Inventory JOIN Floor ON Floor.WarehouseID = Inventory.WarehouseID" +
                                    " JOIN Machine ON Floor.FloorID = Machine.FloorID JOIN Part ON Part.PartNumber = Inventory.PartID" +
                                    " WHERE TechnicianID = @EmployeeID";
                    break;

                case 2://FloorManager Wants to View the Inventory of their Warehouse
                    query = "SELECT DISTINCT PartNumber, VendorName, Description, UnitCost, Quantity" +
                        " FROM Inventory Join Part ON Part.PartNumber = Inventory.PartID" +
                        " JOIN Floor ON Floor.WarehouseID = Inventory.WarehouseID Where FMangerID = @EmployeeID";
                    break;

                case 3://Warehouse Manager wants to veiw their Warehouse's Inventory
                    query = "SELECT DISTINCT PartNumber, VendorName, Description, UnitCost, Quantity" +
                        " FROM Inventory Join Part ON Part.PartNumber = Inventory.PartID" +
                        " JOIN Warehouse ON Warehouse.WarehouseID = Inventory.WarehouseID Where WManagerID = @EmployeeID";
                    break;
                case 4: //RegionManager wants to view their Warehouses' Inventories
                    query = "SELECT Warehouse.WarehouseID, PartNumber, VendorName, Description, UnitCost, Quantity" +
                        " FROM Inventory Join Part ON Part.PartNumber = Inventory.PartID JOIN Warehouse ON Warehouse.WarehouseID = Inventory.WarehouseID" +
                        " JOIN Region On Region.RegionID = Warehouse.RegionID Where RManagerID = @EmployeeID";
                    break;
                case 5://An administrator wants to view all inventories
                    label1.Text = "All Inventories";
                    query = "SELECT Warehouse.WarehouseID, PartNumber, VendorName, Description, UnitCost, Quantity" +
                        " FROM Inventory Join Part ON Part.PartNumber = Inventory.PartID JOIN Warehouse ON Warehouse.WarehouseID = Inventory.WarehouseID";
                    break;
            }

            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@EmployeeID", SharedData.employeeID);
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand = command;
                    adapter.Fill(myTable);
                    dataGridView1.DataSource = myTable;



                }
                catch (SqlException ex)
                {
                    // Handle SQL exceptions
                    Console.WriteLine("SQL Error");
                }
                catch (Exception ex)
                {
                    // Handle other exceptions
                    Console.WriteLine("Error");
                }
            }
        }



        private void viewMachine_Click(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            label1.Text = "Your Machines";
            string query = "";
            //determine the query for Inventory dependent on Permission Level and EmployeeID

            DataTable myTable = new DataTable();
            switch (SharedData.permissionLevel)
            {
                default://deny by default
                    label1.Text = "Sorry! You don't have permission to view these tables :(";
                    return;

                case 1://Technician Wants to View the Machines they Manage
                    query = "SELECT Distinct MachineID, Floor.Story, Manufacturer, Operational, Notes" +
                        " FROM Machine JOIN Floor ON Floor.FloorID = Machine.FloorID Where TechnicianID = @EmployeeID";
                    break;

                case 2://FloorManager Wants to View their Floors' Machines
                    query = "SELECT MachineID, Story, Manufacturer, Operational, Notes FROM Machine" +
                        " Join Floor on Floor.FloorID = Machine.FloorID Where FMangerID = @EmployeeID";
                    break;

                case 3://Warehouse Manager wants to veiw their Warehouses' Machines
                    query = "SELECT MachineID, Story, Manufacturer, Operational, Notes " +
                        "FROM Machine Join Floor on Floor.FloorID = Machine.FloorID " +
                        "Join Warehouse on Warehouse.WarehouseID = Floor.WarehouseID Where WManagerID = @EmployeeID";
                    break;
                case 4: //RegionManager wants to view their Regions' Machines'
                    query = "SELECT Warehouse.WarehouseID, MachineID, Story, Manufacturer, Operational, Notes " +
                        "FROM Machine Join Floor on Floor.FloorID = Machine.FloorID Join Warehouse on Warehouse.WarehouseID = Floor.WarehouseID " +
                        "Join Region on Region.RegionID = Warehouse.RegionID Where RManagerID = @EmployeeID";
                    break;
                case 5://Administrator wants to view All machines across All Regions
                    label1.Text = "All Machines";
                    query = "SELECT Warehouse.WarehouseID, MachineID, Story, Manufacturer, Operational, Notes " +
                        "FROM Machine Join Floor on Floor.FloorID = Machine.FloorID Join Warehouse on Warehouse.WarehouseID = Floor.WarehouseID " +
                        "Join Region on Region.RegionID = Warehouse.RegionID";
                    break;
            }

            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@EmployeeID", SharedData.employeeID);
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand = command;
                    adapter.Fill(myTable);
                    dataGridView1.DataSource = myTable;



                }
                catch (SqlException ex)
                {
                    // Handle SQL exceptions
                    Console.WriteLine("SQL Error");
                }
                catch (Exception ex)
                {
                    // Handle other exceptions
                    Console.WriteLine("Error");
                }
            }
        }


        private void viewFloor_Click(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            label1.Text = "Your Floors";
            string query = "";
            //determine the query for Inventory dependent on Permission Level and EmployeeID

            DataTable myTable = new DataTable();
            switch (SharedData.permissionLevel)
            {
                default://deny by default
                    label1.Text = "Sorry! You don't have permission to view these tables :(";
                    return;
                case 3://Warehouse Manager wants to veiw their Warehouse's Floors
                    query = "SELECT FloorID, FMangerID, Employees.FirstName, Employees.LastName, Story FROM Floor JOIN Employees " +
                        "ON Floor.FMangerID = Employees.EmployeeID JOIN Warehouse ON Warehouse.WarehouseID = Floor.WarehouseID Where WManagerID = @EmployeeID";
                    break;
                case 4: //RegionManager wants to view their Regions' Warehouses' Floors
                    query = "SELECT Warehouse.WarehouseID, Floor.FloorID, FMangerID, FirstName, LastName, Story" +
                        " FROM Floor Join Warehouse on Floor.WarehouseID = Warehouse.WarehouseID Join Employees on Employees.EmployeeID = Floor.FMangerID" +
                        " Join Region on Region.RegionID = Warehouse.RegionID Where RManagerID = @EmployeeID";
                    break;
                case 5:
                    label1.Text = "All Floors"; //An administrator needs to see all Floors
                    query = "SELECT Warehouse.WarehouseID, Floor.FloorID, FMangerID, FirstName, LastName, Story" +
                        " FROM Floor Join Warehouse on Floor.WarehouseID = Warehouse.WarehouseID Join Employees on Employees.EmployeeID = Floor.FMangerID";
                    break;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@EmployeeID", SharedData.employeeID);
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand = command;
                    adapter.Fill(myTable);
                    dataGridView1.DataSource = myTable;



                }
                catch (SqlException ex)
                {
                    // Handle SQL exceptions
                    Console.WriteLine("SQL Error");
                }
                catch (Exception ex)
                {
                    // Handle other exceptions
                    Console.WriteLine("Error");
                }
            }
        }


        private void viewWarehouse_Click(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            label1.Text = "Your Warehouses";
            string query = "";
            //determine the query for Inventory dependent on Permission Level and EmployeeID

            DataTable myTable = new DataTable();
            switch (SharedData.permissionLevel)
            {
                default://deny by default
                    label1.Text = "Sorry! You don't have permission to view these tables :(";
                    return;

                case 4: //RegionManager wants to view their Regions' Warehouses' Floors
                    query = "SELECT WarehouseID, WManagerID, FirstName, LastName, Street, State, ZIPCode FROM Warehouse" +
                        " JOIN Employees on Warehouse.WManagerID = Employees.EmployeeID Join Region on Warehouse.RegionID = Region.RegionID" +
                        " Where RManagerID = @EmployeeID";
                    break;

                case 5:
                    label1.Text = "All Warehouses";//An administrator needs to see ALL warehouses
                    query = "SELECT WarehouseID, WManagerID, FirstName, LastName, Street, State, ZIPCode FROM Warehouse" +
                        " JOIN Employees on Warehouse.WManagerID = Employees.EmployeeID Join Region on Warehouse.RegionID = Region.RegionID";
                    break;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@EmployeeID", SharedData.employeeID);
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand = command;
                    adapter.Fill(myTable);
                    dataGridView1.DataSource = myTable;



                }
                catch (SqlException ex)
                {
                    // Handle SQL exceptions
                    Console.WriteLine("SQL Error");
                }
                catch (Exception ex)
                {
                    // Handle other exceptions
                    Console.WriteLine("Error");
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }


        private void viewRegion_Click(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            label1.Text = "All Regions";
            string query = "";
            //determine the query for Inventory dependent on Permission Level and EmployeeID

            DataTable myTable = new DataTable();
            switch (SharedData.permissionLevel)
            {
                default://deny by default
                    label1.Text = "Sorry! You don't have permission to view these tables :(";
                    return;
                case 5://an adminsitrator needs to see All regions
                    query = "SELECT * From Region";
                    break;
            }

            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand = command;
                    adapter.Fill(myTable);
                    dataGridView1.DataSource = myTable;



                }
                catch (SqlException ex)
                {
                    // Handle SQL exceptions
                    Console.WriteLine("SQL Error");
                }
                catch (Exception ex)
                {
                    // Handle other exceptions
                    Console.WriteLine("Error");
                }
            }
        }

        private void Form2_Load_2(object sender, EventArgs e)
        {

        }
    }

    //user data such as Permission Level, Username, and EmployeeID for checking permissions
    public static class SharedData
    {
        public static int permissionLevel { get; set; }  // public int permissionLevel
        public static string currentUser { get; set; }   // public string current
        public static int employeeID { get; set; }   // public string current user's employee ID

    }
}
