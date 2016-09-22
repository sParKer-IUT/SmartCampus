using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using MySql.Data.MySqlClient;

/**Note In Case I Forget
 * >>To do: To add a new user control class
 * - add new user control to the project. design it with necessary buttons, lables etc
 * - make sure that all the buttons are public
 * - follow the code of an existing user control of this project for the new button_click functions
 * - declare a static instance of that class in Main.cs
 * - create a theclassname_Events function (inside the Button Events region for organizing purpose)
 * - call that function in the constructor of Main.cs
 * - in that function assign all its button events to a function named theclassname_btnClick
 * - create theclassname_btnClick function (inside the Button Event Functions region for organizing purpose)
 * - follow an alreadyexisting_btnClick function to add actions inside the theclassname_btnClick for all the button events
 * 
 * >>To do: To add a new button in an existing user control
 * - follow its existing button_click functions for the new button_click functions
 * - go to thatclassname_Events function
 * - assign the new button event to the function named thatclassname_btnClick
 * - add an action inside thatclassname_btnClick for the new button event
*/

namespace SmartCampus
{
    public partial class Main : Form
    {
        private string server;
        private string database;
        private string uid;
        private string password;
        MySqlConnection connection;
        MySqlDataReader reader;
        MySqlCommand sc;

        int w = Screen.PrimaryScreen.Bounds.Width;
        int h = Screen.PrimaryScreen.Bounds.Height;

        UserControl currentControl = null;

        static MainMenu mainmenu = new MainMenu();
        static StudentPayment studentpayment = new StudentPayment();
        static TeacherPayment teacherpayment = new TeacherPayment();
        static Accounts accounts = new Accounts();
        static PaymentReceipt paymentreceipt = new PaymentReceipt();
        static TeacherPaymentReceipt teacherpaymentreceipt = new TeacherPaymentReceipt();
        static SetPaymentRate setpaymentrate = new SetPaymentRate();
        static Expenses expenses = new Expenses();
        static Admission admission = new Admission();
        static StudentDatabase studentdatabase = new StudentDatabase();
        static StudentInformation studentinformation = new StudentInformation();
        static StudentInfoEdit studentinfoedit = new StudentInfoEdit();
        static StdPaymentStatus stdpaymentstatus = new StdPaymentStatus();
        static EmployeeRecruit employeerecruit = new EmployeeRecruit();
        static EmployeeOptions employeeoptions = new EmployeeOptions();
        static SeeResult seeresult = new SeeResult();
        static ResultMain resultmain = new ResultMain();
        static EnterMarks entermarks = new EnterMarks();
        static EnterMarksMain entermarksmain = new EnterMarksMain();
        static ResultOption resultoption = new ResultOption();
        static SeeClassResult seeclassresult = new SeeClassResult();
        static EmployeeInfoShow empInfoShow = new EmployeeInfoShow();
        static EmployeeInfoEdit empInfoEdit = new EmployeeInfoEdit();
        static DailyAttendance dailyAtt = new DailyAttendance();
        static ShowAttendance showAttendance = new ShowAttendance("in");
        static DailyAttendanceAll dailyAttAll = new DailyAttendanceAll();
        static EmpDailyAttendance empDailyAtt = new EmpDailyAttendance("in");
        static EmpAttendanceType empAttType = new EmpAttendanceType();

        public Main()
        {
            InitializeComponent();

            AddToPanel(mainmenu, "Main Menu");
            MainMenu_Events();
            StudentPayment_Events();
            TeacherPayment_Events();
            Accounts_Events();
            PaymentReceipt_Events();
            TeacherPaymentReceipt_Events();
            SetPaymentRate_Events();
            Expenses_Events();
            Admission_Events();
            StudentDatabase_Events();
            StudentInformation_Events();
            StudentInfoEdit_Events();
            StdPaymentStatus_Events();
            EmployeeRecruit_Events();
            EmployeeOptions_Events();
            SeeResult_Events();
            ResultMain_Events();
            EnterMarks_Events();
            EnterMarksMain_Events();
            ResultOption_Events();
            SeeClassResult_Events();
            DailyAttendance_Events();
            ShowAttendance_Events();
            DailyAttendanceAll_Events();
            EmpDailyAttendance_Events();
            EmpAttendanceType_Events();
        }

        void AddToPanel(UserControl control, string formTitleText)
        {
            if(currentControl != null)MainPanel.Controls.Remove(currentControl);
            MainPanel.Controls.Add(control);
            currentControl = control;
            Controls.Add(MainPanel);
            //this.Size = new Size(w, h);
            this.Text = formTitleText;
            //MainPanel.Size = new Size(w, h);
            //mainmenu.Size = new Size(w, h);
        }

        #region Button Events
        void MainMenu_Events()
        {
            mainmenu.btn1Click += new EventHandler(MainMenu_btnClick);
        }
        void StudentPayment_Events()
        {
            studentpayment.btn1Click += new EventHandler(StudentPayment_btnClick);
            //studentpayment.btn2Click += new EventHandler(StudentPayment_btnClick);
        }
        void Accounts_Events()
        {
            accounts.btn1Click += new EventHandler(Accounts_btnClick);
        }
        void TeacherPayment_Events()
        {
            teacherpayment.btn1Click += new EventHandler(TeacherPayment_btnClick);
        }
        void PaymentReceipt_Events()
        {
            paymentreceipt.btn1Click += new EventHandler(PaymentReceipt_btnClick);
        }
        void TeacherPaymentReceipt_Events()
        {
            teacherpaymentreceipt.btn1Click += new EventHandler(TeacherPaymentReceipt_btnClick);
        }
        void SetPaymentRate_Events()
        {
            setpaymentrate.btn1Click += new EventHandler(SetPaymentRate_btnClick);
        }
        void Expenses_Events()
        {
            expenses.btn1Click += new EventHandler(Expenses_btnClick);
        }
        void Admission_Events()
        {
            admission.btn1Click += new EventHandler(Admission_btnClick);
        }
        void StudentDatabase_Events()
        {
            studentdatabase.btn1Click += new EventHandler(StudentDatabase_btnClick);
        }
        void StudentInformation_Events()
        {
            studentinformation.btnClick += new EventHandler(StudentInformation_btnClick);
        }
        void StudentInfoEdit_Events()
        {
            studentinfoedit.btn1Click += new EventHandler(StudentInfoEdit_btnClick);
        }
        void StdPaymentStatus_Events()
        {
            stdpaymentstatus.btn1Click += new EventHandler(StdPaymentStatus_btnClick);
        }
        void EmployeeRecruit_Events()
        {
            employeerecruit.btn1Click += new EventHandler(EmployeeRecruit_btnClick);
        }
        void EmployeeOptions_Events()
        {
            employeeoptions.btn1Click += new EventHandler(EmployeeOptions_btnClick);
        }
        void EmpInfoShow_Events()
        {
            empInfoShow.btnClick += new EventHandler(EmpInfoShow_btnClick);
        }
        void EmpInfoEdit_Events()
        {
            empInfoEdit.btn1Click += new EventHandler(EmpInfoEdit_btnClick);
        }
        void ResultMain_Events()
        {
            resultmain.resultMainBtnClick += new EventHandler(ResultMain_btnClick);
        }
        void ResultOption_Events()
        {
            resultoption.resultOptionBtnClick += new EventHandler(ResultOption_btnClick);
        }
        private void SeeClassResult_Events()
        {
            seeclassresult.seeClassResultBtnClick += new EventHandler(SeeClassResult_btnClick);
        }
        private void EnterMarksMain_Events()
        {
            entermarksmain.enterMarksMainBtnClicked += new EventHandler(EnterMarksMain_btnClick);
        }
        private void EnterMarks_Events()
        {
            entermarks.enterMarksBtnClick += new EventHandler(EnterMarks_btnClick);
        }
        private void SeeResult_Events()
        {
            seeresult.seeResultBtnClick += new EventHandler(SeeResult_btnClick);
        }
        void DailyAttendance_Events()
        {
            dailyAtt.btn1Click += new EventHandler(DailyAttendance_btnClick);
        }
        void ShowAttendance_Events()
        {
            showAttendance.btn1Click += new EventHandler(ShowAttendance_btnClick);
        }
        void DailyAttendanceAll_Events()
        {
            dailyAttAll.btn1Click += new EventHandler(DailyAttendanceAll_btnClick);
        }
        void EmpDailyAttendance_Events()
        {
            empDailyAtt.btn1Click += new EventHandler(EmpDailyAttendance_btnClick);
        }
        void EmpAttendanceType_Events() 
        {
            empAttType.btn1Click += new EventHandler(EmpAttendanceType_btnClick);
        }
        #endregion


        #region Button Event Functions
        void MainMenu_btnClick(object sender, EventArgs e)
        {
            if (mainmenu.clickedButton == mainmenu.Accounts)
            {
                accounts = new Accounts();
                Accounts_Events();
                AddToPanel(accounts, "Accounts");
            }
            if (mainmenu.clickedButton == mainmenu.Admission)
            {
                admission = new Admission();
                Admission_Events();
                AddToPanel(admission, "Admission");
            }
            if (mainmenu.clickedButton == mainmenu.StudentDB)
            {
                studentdatabase = new StudentDatabase();
                StudentDatabase_Events();
                AddToPanel(studentdatabase, "Student Database");
            }
            if (mainmenu.clickedButton == mainmenu.Employee)
            {
                employeeoptions = new EmployeeOptions();
                EmployeeOptions_Events();
                AddToPanel(employeeoptions, "Options");
            }
            if (mainmenu.clickedButton == mainmenu.Result)
            {
                resultmain = new ResultMain();
                ResultMain_Events();
                AddToPanel(resultmain, "Result");
            }
            if(mainmenu.clickedButton == mainmenu.DailyAtt)
            {
                dailyAttAll = new DailyAttendanceAll();
                DailyAttendanceAll_Events();
                AddToPanel(dailyAttAll, "Daily Attendance");
            }

        }
        void Accounts_btnClick(object sender, EventArgs e)
        {
            if (accounts.clickedButton == accounts.stdPayment)
            {
                studentpayment = new StudentPayment();
                StudentPayment_Events();
                AddToPanel(studentpayment, "Student Payment");
            }
            if (accounts.clickedButton == accounts.tchrPayment)
            {
                teacherpayment = new TeacherPayment();
                TeacherPayment_Events();
                AddToPanel(teacherpayment, "Teacher Payment");
            }
            if (accounts.clickedButton == accounts.Spendings)
            {
                expenses = new Expenses();
                Expenses_Events();
                AddToPanel(expenses, "Expenses");
            }
            if (accounts.clickedButton == accounts.PaymentRate)
            {
                accounts = new Accounts();
                Accounts_Events();
                AddToPanel(setpaymentrate, "Set Payment Rate");
            }
            if (accounts.clickedButton == accounts.Back)
            {
                mainmenu = new MainMenu();
                MainMenu_Events();
                AddToPanel(mainmenu, "Main Menu");
            }
        }
        void StudentPayment_btnClick(object sender, EventArgs e)
        {
            if (studentpayment.clickedButton == studentpayment.Proceed && studentpayment.proceed == true)
            {
                paymentreceipt = new PaymentReceipt();
                PaymentReceipt_Events();
                AddToPanel(paymentreceipt, "Payment Receipt");
                paymentreceipt.SetVariables();
            }
            if (studentpayment.clickedButton == studentpayment.Back)
            {
                accounts = new Accounts();
                Accounts_Events();
                AddToPanel(accounts, "Accounts");
            }
        }
        private void PaymentReceipt_btnClick(object sender, EventArgs e)
        {
            if (paymentreceipt.clickedButton == paymentreceipt.Back)
            {
                studentpayment = new StudentPayment();
                StudentPayment_Events();
                AddToPanel(studentpayment, "Student Payment");
            }
            if (paymentreceipt.clickedButton == paymentreceipt.TakePayment && paymentreceipt.takepayment == true)
            {
                studentpayment = new StudentPayment();
                StudentPayment_Events();
                AddToPanel(studentpayment, "Student Payment");
            }
        }
        private void TeacherPayment_btnClick(object sender, EventArgs e)
        {
            if (teacherpayment.clickedButton == teacherpayment.Proceed && teacherpayment.proceed == true)
            {
                teacherpaymentreceipt = new TeacherPaymentReceipt();
                TeacherPaymentReceipt_Events();
                AddToPanel(teacherpaymentreceipt, "Teacher Payment Receipt");
                teacherpaymentreceipt.SetVariables();
            }
            if (teacherpayment.clickedButton == teacherpayment.Back)
            {
                accounts = new Accounts();
                Accounts_Events();
                AddToPanel(accounts, "Accounts");
            }
        }
        private void TeacherPaymentReceipt_btnClick(object sender, EventArgs e)
        {
            if (teacherpaymentreceipt.clickedButton == teacherpaymentreceipt.Pay && teacherpaymentreceipt.givepayment == true)
            {
                teacherpayment = new TeacherPayment();
                TeacherPayment_Events();
                AddToPanel(teacherpayment, "Teacher Payment");
            }
            if (teacherpaymentreceipt.clickedButton == teacherpaymentreceipt.Back)
            {
                teacherpayment = new TeacherPayment();
                TeacherPayment_Events();
                AddToPanel(teacherpayment, "Teacher Payment");
            }
        }
        private void SetPaymentRate_btnClick(object sender, EventArgs e)
        {
            if (setpaymentrate.clickedButton == setpaymentrate.Proceed && setpaymentrate.proceed == true)
            {
                accounts = new Accounts();
                Accounts_Events();
                AddToPanel(accounts, "Accounts");
            }
            if (setpaymentrate.clickedButton == setpaymentrate.Back)
            {
                accounts = new Accounts();
                Accounts_Events();
                AddToPanel(accounts, "Accounts");
            }
        }
        private void Expenses_btnClick(object sender, EventArgs e)
        {
            if (expenses.clickedButton == expenses.Back)
            {
                accounts = new Accounts();
                Accounts_Events();
                AddToPanel(accounts, "Accounts");
            }
            if (expenses.clickedButton == expenses.Save && expenses.save == true)
            {
                accounts = new Accounts();
                Accounts_Events();
                AddToPanel(accounts, "Accounts");
            }
        }
        private void Admission_btnClick(object sender, EventArgs e)
        {
            if (admission.clickedButton == admission.Save && admission.save == true)
            {
                mainmenu = new MainMenu();
                MainMenu_Events();
                AddToPanel(mainmenu, "Main Menu");
            }
            if (admission.clickedButton == admission.Back)
            {
                mainmenu = new MainMenu();
                MainMenu_Events();
                AddToPanel(mainmenu, "Main Menu");
            }
        }
        private void StudentDatabase_btnClick(object sender, EventArgs e)
        {
            if (studentdatabase.clickedButton == studentdatabase.Back)
            {
                mainmenu = new MainMenu();
                MainMenu_Events();
                AddToPanel(mainmenu, "Main Menu");
            }
            if (studentdatabase.clickedButton == studentdatabase.stdInfo && studentdatabase.proceed == true)
            {
                studentinformation = new StudentInformation();
                StudentInformation_Events();
                AddToPanel(studentinformation, "Student Information");
            }
            if (studentdatabase.clickedButton == studentdatabase.Payment && studentdatabase.proceed == true)
            {
                stdpaymentstatus = new StdPaymentStatus();
                StdPaymentStatus_Events();
                AddToPanel(stdpaymentstatus, "Student Payment Status");
            }
        }
        private void StudentInformation_btnClick(object sender, EventArgs e)
        {
            if (studentinformation.clickedButton == studentinformation.Edit)
            {
                studentinfoedit = new StudentInfoEdit();
                StudentInfoEdit_Events();
                AddToPanel(studentinfoedit, "Student Information Edit");
            }
            if (studentinformation.clickedButton == studentinformation.Back)
            {
                studentdatabase = new StudentDatabase();
                StudentDatabase_Events();
                AddToPanel(studentdatabase, "Student Database");
            }
        }
        private void StudentInfoEdit_btnClick(object sender, EventArgs e)
        {
            if (studentinfoedit.clickedButton == studentinfoedit.Save && studentinfoedit.save == true)
            {
                studentinformation = new StudentInformation();
                StudentInformation_Events();
                AddToPanel(studentinformation, "Student Information"); ;
            }
            if (studentinfoedit.clickedButton == studentinfoedit.Back)
            {
                studentinformation = new StudentInformation();
                StudentInformation_Events();
                AddToPanel(studentinformation, "Student Information");
            }
            if(studentinfoedit.clickedButton == studentinfoedit.delete)
            {
                studentdatabase = new StudentDatabase();
                StudentDatabase_Events();
                AddToPanel(studentdatabase, "Student Database");
            }
        }
        private void StdPaymentStatus_btnClick(object sender, EventArgs e)
        {
            if (stdpaymentstatus.clickedButton == stdpaymentstatus.Back)
            {
                studentdatabase = new StudentDatabase();
                StudentDatabase_Events();
                AddToPanel(studentdatabase, "Student Database");
            }
        }
        private void EmployeeRecruit_btnClick(object sender, EventArgs e)
        {
            if (employeerecruit.clickedButton == employeerecruit.Back)
            {
                employeeoptions = new EmployeeOptions();
                EmployeeOptions_Events();
                AddToPanel(employeeoptions, "Options");
            }
        }
        private void EmployeeOptions_btnClick(object sender, EventArgs e)
        {
            if (employeeoptions.clickedButton == employeeoptions.Recruit)
            {
                employeerecruit = new EmployeeRecruit();
                EmployeeRecruit_Events();
                AddToPanel(employeerecruit, "Employee Information Form");
            }
            if (employeeoptions.clickedButton == employeeoptions.Back)
            {
                mainmenu = new MainMenu();
                MainMenu_Events();
                AddToPanel(mainmenu, "Main Menu");
            }
            if (employeeoptions.clickedButton == employeeoptions.EmpInfo)
            {
                empInfoShow = new EmployeeInfoShow();
                EmpInfoShow_Events();
                AddToPanel(empInfoShow, "Employee Information");
            }
        }
        private void EmpInfoShow_btnClick(object sender, EventArgs e)
        {
            if (empInfoShow.clickedButton == empInfoShow.Edit)
            {
                empInfoEdit = new EmployeeInfoEdit();
                EmpInfoEdit_Events();
                AddToPanel(empInfoEdit, "Employee Information Edit");
            }
            if (empInfoShow.clickedButton == empInfoShow.Back)
            {
                employeeoptions = new EmployeeOptions();
                EmployeeOptions_Events();
                AddToPanel(employeeoptions, "Options");
            }
        }
        private void EmpInfoEdit_btnClick(object sender, EventArgs e)
        {
            if (empInfoEdit.clickedButton == empInfoEdit.Save)
            {
                employeeoptions = new EmployeeOptions();
                EmployeeOptions_Events();
                AddToPanel(employeeoptions, "Options");
            }
            if (empInfoEdit.clickedButton == empInfoEdit.Back)
            {
                empInfoShow = new EmployeeInfoShow();
                EmpInfoShow_Events();
                AddToPanel(empInfoShow, "Employee Information");
            }
            if (empInfoEdit.clickedButton == empInfoEdit.delete)
            {
                employeeoptions = new EmployeeOptions();
                EmployeeOptions_Events();
                AddToPanel(employeeoptions, "Options");
            }
        }
        private void ResultMain_btnClick(object sender, EventArgs e)
        {
            if (resultmain.clickedButton == resultmain.btn_resultMain_seeResult)
            {
                resultoption = new ResultOption();
                ResultOption_Events();
                AddToPanel(resultoption, "Result Options");
            }
            if (resultmain.clickedButton == resultmain.btn_resultMain_enterMarks)
            {
                entermarks = new EnterMarks();
                EnterMarks_Events();
                AddToPanel(entermarks, "Enter Marks");
            }
            if (resultmain.clickedButton == resultmain.btn_resultMain_back)
            {
                mainmenu = new MainMenu();
                MainMenu_Events();
                AddToPanel(mainmenu, "Main Menu");
            }
        }

        private void SeeResult_btnClick(object sender, EventArgs e)
        {
            if (seeresult.clickedButton == seeresult.btn_seeResult_back)
            {
                resultoption = new ResultOption();
                ResultOption_Events();
                AddToPanel(resultoption, "Result Options");
            }
        }

        private void EnterMarks_btnClick(object sender, EventArgs e)
        {
            if (entermarks.clickedButton == entermarks.Submit)
            {
                entermarks = new EnterMarks();
                EnterMarks_Events();
                AddToPanel(entermarksmain, "Enter Marks");
            }
            if (entermarks.clickedButton == entermarks.Back)
            {
                resultmain = new ResultMain();
                ResultMain_Events();
                AddToPanel(resultmain, "Result");
            }
        }

        private void EnterMarksMain_btnClick(object sender, EventArgs e)
        {
            if (entermarksmain.clickedButton == entermarksmain.btnBack)
            {
                entermarks = new EnterMarks();
                EnterMarks_Events();
                AddToPanel(entermarks, "Enter Marks");
            }
        }

        private void ResultOption_btnClick(object sender, EventArgs e)
        {
            if (resultoption.clickedButton == resultoption.btnSeeIndividual)
            {
                seeresult = new SeeResult();
                SeeResult_Events();
                AddToPanel(seeresult, "Individual Result");
            }
            if (resultoption.clickedButton == resultoption.btnSeeClassResult)
            {
                seeclassresult = new SeeClassResult();
                SeeClassResult_Events();
                AddToPanel(seeclassresult, "Class Result");
            }
            if (resultoption.clickedButton == resultoption.btnBack)
            {
                resultmain = new ResultMain();
                ResultMain_Events();
                AddToPanel(resultmain, "Result");
            }
        }

        private void SeeClassResult_btnClick(object sender, EventArgs e)
        {
            if (seeclassresult.ClickedButton == seeclassresult.btnBack)
            {
                resultoption = new ResultOption();
                ResultOption_Events();
                AddToPanel(resultoption, "Result Options");
            }
        }
        private void DailyAttendance_btnClick(object sender, EventArgs e)
        {
            if (dailyAtt.clickedButton == dailyAtt.back)
            {
                dailyAttAll = new DailyAttendanceAll();
                DailyAttendanceAll_Events();
                AddToPanel(dailyAttAll, "Daily Attendance");
            }
            if (dailyAtt.clickedButton == dailyAtt.viewAtt)
            {
                showAttendance = new ShowAttendance("in");
                ShowAttendance_Events();
                AddToPanel(showAttendance, "Student Check In");
            }
            if (dailyAtt.clickedButton == dailyAtt.viewCO)
            {
                showAttendance = new ShowAttendance("out");
                ShowAttendance_Events();
                AddToPanel(showAttendance, "Student Check Out");
            }
        }
        private void ShowAttendance_btnClick(object sender, EventArgs e)
        {
            if (showAttendance.clickedButton == showAttendance.back)
            {
                dailyAtt = new DailyAttendance();
                DailyAttendance_Events();
                AddToPanel(dailyAtt, "Student Attendance");
            }
        }

        private void DailyAttendanceAll_btnClick(object sender, EventArgs e)
        {
            if (dailyAttAll.clickedButton == dailyAttAll.back)
            {
                mainmenu = new MainMenu();
                MainMenu_Events();
                AddToPanel(mainmenu, "Main Menu");
            }

            if (dailyAttAll.clickedButton == dailyAttAll.stdAtt)
            {
                dailyAtt = new DailyAttendance();
                DailyAttendance_Events();
                AddToPanel(dailyAtt, "Student Attendance");
            }

            if (dailyAttAll.clickedButton == dailyAttAll.empAtt)
            {
                empAttType = new EmpAttendanceType();
                EmpAttendanceType_Events();
                AddToPanel(empAttType, "Employee Attendance");
            }
        }

        private void EmpDailyAttendance_btnClick(object sender, EventArgs e)
        {
            if (empDailyAtt.clickedButton == empDailyAtt.back)
            {
                empAttType = new EmpAttendanceType();
                EmpAttendanceType_Events();
                AddToPanel(empAttType, "Employee Attendance");
            }
        }

        private void EmpAttendanceType_btnClick(object sender, EventArgs e)
        {
            if(empAttType.clickedButton == empAttType.viewAtt)
            {
                empDailyAtt = new EmpDailyAttendance("in");
                EmpDailyAttendance_Events();
                AddToPanel(empDailyAtt, "Employee Check In");
            }

            if (empAttType.clickedButton == empAttType.viewCO)
            {
                empDailyAtt = new EmpDailyAttendance("out");
                EmpDailyAttendance_Events();
                AddToPanel(empDailyAtt, "Employee Check Out");
            }

            if (empAttType.clickedButton == empAttType.back)
            {
                dailyAttAll = new DailyAttendanceAll();
                DailyAttendanceAll_Events();
                AddToPanel(dailyAttAll, "Daily Attendance");
            }
        }
        #endregion

        private void MainPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Main_Load(object sender, EventArgs e)
        {
            try
            {
                server = "localhost";
                database = "shotabdi";
                uid = "root";
                password = "";
                string connectionString;
                connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
                connection = new MySqlConnection(connectionString);
                connection.Open();

                sc = new MySqlCommand("select * from Security", connection);
                reader = sc.ExecuteReader();

                while (reader.Read())
                {
                    if (DateTime.Today > Convert.ToDateTime("01/04/2016"))
                    if (reader[0].ToString().Equals("gmkl"))
                    {
                        MessageBox.Show("Application is Restricted");
                        Application.Exit();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Please Start Xampp");
                Application.Exit();
            }
            
        }
    }
}
