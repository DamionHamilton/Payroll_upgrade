'Damion Hamilton
'Payroll program used to calculate; employee pay,deductions, gross pay, net pay

Option Infer On
Option Explicit On
Option Strict On


Public Class Form1
    ' following code pre loads dataset with employees information

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.EmployeesTableAdapter.Fill(Me.PayrolldbDataSet.Employees)

    End Sub

    ' Button1 = Search database for Employee by user input into textbox,

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click


        If TextBox1.Text = "" Then

            MessageBox.Show("ERROR:" + "" + "Please Enter Employee I.D Into Text Box", "Payroll_ERROR", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation)


        Else

            Dim Find_emp = From Employees In PayrolldbDataSet.Employees
                                 Where Employees.Emp_I_D.ToString = TextBox1.Text
                              Select Employees

            EmployeesBindingSource.DataSource = Find_emp.AsDataView


        End If
    End Sub

    ' button2 = search database for all employees with more than 40 hours
    ' button2 will find all employees in overtime

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click



        Dim search = From Employees In PayrolldbDataSet.Employees
                     Where Employees.Hours > 40
                     Select Employees

        EmployeesBindingSource.DataSource = search.AsDataView



    End Sub

    'button3 = Calculate the total amount of overtime pay a employee has earned
    'button3 will calculate total overtime pay for employee being searched

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        If TextBox1.Text = "" Then

            MessageBox.Show("ERROR:" + "" + "Please Enter Employee I.D Into Text Box", "Payroll_ERROR", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation)


        Else

            Dim hour As Integer = Aggregate Employees In PayrolldbDataSet.Employees
                                 Where Employees.Emp_I_D.ToString = TextBox1.Text
                             Select Employees.Hours
                             Into Sum()


            If hour > 40 Then


                Dim Pay As Double = Aggregate Employees In PayrolldbDataSet.Employees
                              Where Employees.Emp_I_D.ToString = TextBox1.Text
                             Select Employees.Pay_Rate
                             Into Sum()

                Dim othours As Double = Aggregate Employees In PayrolldbDataSet.Employees
                             Where Employees.Emp_I_D.ToString = TextBox1.Text
                            Select Employees.Hours - 40
                            Into Sum()

                Dim overtimepay As Double = Aggregate Employees In PayrolldbDataSet.Employees
                              Where Employees.Emp_I_D.ToString = TextBox1.Text
                             Select (Employees.Pay_Rate / 2) + Pay
                             Into Sum()

                Dim otamount As Double = Aggregate Employees In PayrolldbDataSet.Employees
                              Where Employees.Emp_I_D.ToString = TextBox1.Text
                             Select overtimepay * othours
                             Into Sum()

                Dim Find_emp = From Employees In PayrolldbDataSet.Employees
                                Where Employees.Emp_I_D.ToString = TextBox1.Text
                             Select Employees

                EmployeesBindingSource.DataSource = Find_emp.AsDataView

                MessageBox.Show("Overtime Pay: " & "$" & otamount.ToString("N2"), "Payroll", MessageBoxButtons.OK,
                                MessageBoxIcon.Information)
            ElseIf hour < 40 Then

                Dim Find_emp = From Employees In PayrolldbDataSet.Employees
                                Where Employees.Emp_I_D.ToString = TextBox1.Text
                             Select Employees

                EmployeesBindingSource.DataSource = Find_emp.AsDataView

                MessageBox.Show("No Over Time Pay: ", "Payroll", MessageBoxButtons.OK,
                               MessageBoxIcon.Information)
            End If
        End If


    End Sub

    'button4 = Calculate total gross pay
    ' button4 will calculate total gross pay amount of employees being searched

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If TextBox1.Text = "" Then

            MessageBox.Show("ERROR:" + "" + "Please Enter Employee I.D Into Text Box", "Payroll_ERROR", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation)

        Else

            Dim hour As Integer = Aggregate Employees In PayrolldbDataSet.Employees
                                 Where Employees.Emp_I_D.ToString = TextBox1.Text
                             Select Employees.Hours
                             Into Sum()


            If hour > 40 Then


                Dim Pay As Double = Aggregate Employees In PayrolldbDataSet.Employees
                              Where Employees.Emp_I_D.ToString = TextBox1.Text
                             Select Employees.Pay_Rate
                             Into Sum()

                Dim othours As Double = Aggregate Employees In PayrolldbDataSet.Employees
                             Where Employees.Emp_I_D.ToString = TextBox1.Text
                            Select Employees.Hours - 40
                            Into Sum()

                Dim overtimepay As Double = Aggregate Employees In PayrolldbDataSet.Employees
                              Where Employees.Emp_I_D.ToString = TextBox1.Text
                             Select (Employees.Pay_Rate / 2) + Pay
                             Into Sum()

                Dim otamount As Double = Aggregate Employees In PayrolldbDataSet.Employees
                              Where Employees.Emp_I_D.ToString = TextBox1.Text
                             Select overtimepay * othours
                             Into Sum()

                Dim grosspay As Double = Aggregate Employees In PayrolldbDataSet.Employees
                             Where Employees.Emp_I_D.ToString = TextBox1.Text
                            Select (overtimepay * othours) + (40 * Pay)
                            Into Sum()

                Dim Find_emp = From Employees In PayrolldbDataSet.Employees
                                Where Employees.Emp_I_D.ToString = TextBox1.Text
                             Select Employees

                EmployeesBindingSource.DataSource = Find_emp.AsDataView

                MessageBox.Show("Gross Pay: " & "$" & grosspay.ToString("N2"), "Employee Pay ", MessageBoxButtons.OK,
                                MessageBoxIcon.Information)


            ElseIf hour < 40 Then

                Dim Pay As Double = Aggregate Employees In PayrolldbDataSet.Employees
                              Where Employees.Emp_I_D.ToString = TextBox1.Text
                             Select Employees.Pay_Rate
                             Into Sum()

                Dim grosspay As Double = Aggregate Employees In PayrolldbDataSet.Employees
                             Where Employees.Emp_I_D.ToString = TextBox1.Text
                            Select Pay * hour
                            Into Sum()

                Dim Find_emp = From Employees In PayrolldbDataSet.Employees
                                Where Employees.Emp_I_D.ToString = TextBox1.Text
                             Select Employees

                EmployeesBindingSource.DataSource = Find_emp.AsDataView

                MessageBox.Show("Gross Pay: " & "$" & grosspay.ToString("N2"), "Employee Pay ", MessageBoxButtons.OK,
                                MessageBoxIcon.Information)

            End If
        End If
    End Sub
    'button4 = Calculate total Net pay
    ' button4 will calculate total Net pay amount of employees being searched

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim federal As Double = 0.18
        Dim state As Double = 0.045

        If TextBox1.Text = "" Then

            MessageBox.Show("ERROR:" + "" + "Please Enter Employee I.D Into Text Box", "Payroll_ERROR", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation)


        Else

            Dim hour As Integer = Aggregate Employees In PayrolldbDataSet.Employees
                                 Where Employees.Emp_I_D.ToString = TextBox1.Text
                             Select Employees.Hours
                             Into Sum()


            If hour > 40 Then


                Dim Pay As Double = Aggregate Employees In PayrolldbDataSet.Employees
                              Where Employees.Emp_I_D.ToString = TextBox1.Text
                             Select Employees.Pay_Rate
                             Into Sum()

                Dim othours As Double = Aggregate Employees In PayrolldbDataSet.Employees
                             Where Employees.Emp_I_D.ToString = TextBox1.Text
                            Select Employees.Hours - 40
                            Into Sum()

                Dim overtimepay As Double = Aggregate Employees In PayrolldbDataSet.Employees
                              Where Employees.Emp_I_D.ToString = TextBox1.Text
                             Select (Employees.Pay_Rate / 2) + Pay
                             Into Sum()

                Dim otamount As Double = Aggregate Employees In PayrolldbDataSet.Employees
                              Where Employees.Emp_I_D.ToString = TextBox1.Text
                             Select overtimepay * othours
                             Into Sum()

                Dim grosspay As Double = Aggregate Employees In PayrolldbDataSet.Employees
                             Where Employees.Emp_I_D.ToString = TextBox1.Text
                            Select (overtimepay * othours) + (40 * Pay)
                            Into Sum()

                Dim federaldeductions As Double = Aggregate Employees In PayrolldbDataSet.Employees
                            Where Employees.Emp_I_D.ToString = TextBox1.Text
                           Select grosspay * federal
                           Into Sum()

                Dim statedeductions As Double = Aggregate Employees In PayrolldbDataSet.Employees
                            Where Employees.Emp_I_D.ToString = TextBox1.Text
                           Select grosspay * state
                           Into Sum()

                Dim deductions As Double = Aggregate Employees In PayrolldbDataSet.Employees
                            Where Employees.Emp_I_D.ToString = TextBox1.Text
                           Select federaldeductions + statedeductions
                           Into Sum()

                Dim NetPay As Double = Aggregate Employees In PayrolldbDataSet.Employees
                            Where Employees.Emp_I_D.ToString = TextBox1.Text
                           Select grosspay - deductions
                           Into Sum()

                Dim Find_emp = From Employees In PayrolldbDataSet.Employees
                                Where Employees.Emp_I_D.ToString = TextBox1.Text
                             Select Employees

                EmployeesBindingSource.DataSource = Find_emp.AsDataView

                MessageBox.Show("Net Pay: " & "$" & NetPay.ToString("N2"), "Employee Pay ", MessageBoxButtons.OK,
                                MessageBoxIcon.Information)


            ElseIf hour < 40 Then

                Dim Pay As Double = Aggregate Employees In PayrolldbDataSet.Employees
                              Where Employees.Emp_I_D.ToString = TextBox1.Text
                             Select Employees.Pay_Rate
                             Into Sum()

                Dim grosspay As Double = Aggregate Employees In PayrolldbDataSet.Employees
                             Where Employees.Emp_I_D.ToString = TextBox1.Text
                            Select Pay * hour
                            Into Sum()

                Dim federaldeductions As Double = Aggregate Employees In PayrolldbDataSet.Employees
                           Where Employees.Emp_I_D.ToString = TextBox1.Text
                          Select grosspay * federal
                          Into Sum()

                Dim statedeductions As Double = Aggregate Employees In PayrolldbDataSet.Employees
                            Where Employees.Emp_I_D.ToString = TextBox1.Text
                           Select grosspay * state
                           Into Sum()

                Dim deductions As Double = Aggregate Employees In PayrolldbDataSet.Employees
                            Where Employees.Emp_I_D.ToString = TextBox1.Text
                           Select federaldeductions + statedeductions
                           Into Sum()

                Dim NetPay As Double = Aggregate Employees In PayrolldbDataSet.Employees
                            Where Employees.Emp_I_D.ToString = TextBox1.Text
                           Select grosspay - deductions
                           Into Sum()

                Dim Find_emp = From Employees In PayrolldbDataSet.Employees
                                Where Employees.Emp_I_D.ToString = TextBox1.Text
                             Select Employees

                EmployeesBindingSource.DataSource = Find_emp.AsDataView

                MessageBox.Show("Net Pay: " & "$" & NetPay.ToString("N2"), "Employee Pay ", MessageBoxButtons.OK,
                                MessageBoxIcon.Information)

            End If
        End If
    End Sub

    'button6 = calculate total amount of deductions being taken out of employees gross pay

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim federal As Double = 0.18
        Dim state As Double = 0.045

        If TextBox1.Text = "" Then

            MessageBox.Show("ERROR:" + "" + "Please Enter Employee I.D Into Text Box", "Payroll_ERROR", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation)

        Else

            Dim hour As Integer = Aggregate Employees In PayrolldbDataSet.Employees
                                 Where Employees.Emp_I_D.ToString = TextBox1.Text
                             Select Employees.Hours
                             Into Sum()


            If hour > 40 Then


                Dim Pay As Double = Aggregate Employees In PayrolldbDataSet.Employees
                              Where Employees.Emp_I_D.ToString = TextBox1.Text
                             Select Employees.Pay_Rate
                             Into Sum()

                Dim othours As Double = Aggregate Employees In PayrolldbDataSet.Employees
                             Where Employees.Emp_I_D.ToString = TextBox1.Text
                            Select Employees.Hours - 40
                            Into Sum()

                Dim overtimepay As Double = Aggregate Employees In PayrolldbDataSet.Employees
                              Where Employees.Emp_I_D.ToString = TextBox1.Text
                             Select (Employees.Pay_Rate / 2) + Pay
                             Into Sum()

                Dim otamount As Double = Aggregate Employees In PayrolldbDataSet.Employees
                              Where Employees.Emp_I_D.ToString = TextBox1.Text
                             Select overtimepay * othours
                             Into Sum()

                Dim grosspay As Double = Aggregate Employees In PayrolldbDataSet.Employees
                             Where Employees.Emp_I_D.ToString = TextBox1.Text
                            Select (overtimepay * othours) + (40 * Pay)
                            Into Sum()

                Dim federaldeductions As Double = Aggregate Employees In PayrolldbDataSet.Employees
                            Where Employees.Emp_I_D.ToString = TextBox1.Text
                           Select grosspay * federal
                           Into Sum()

                Dim statedeductions As Double = Aggregate Employees In PayrolldbDataSet.Employees
                            Where Employees.Emp_I_D.ToString = TextBox1.Text
                           Select grosspay * state
                           Into Sum()

                Dim deductions As Double = Aggregate Employees In PayrolldbDataSet.Employees
                            Where Employees.Emp_I_D.ToString = TextBox1.Text
                           Select federaldeductions + statedeductions
                           Into Sum()

                Dim Find_emp = From Employees In PayrolldbDataSet.Employees
                                Where Employees.Emp_I_D.ToString = TextBox1.Text
                             Select Employees

                EmployeesBindingSource.DataSource = Find_emp.AsDataView

                MessageBox.Show("Total Deductions: " & "$" & deductions.ToString("N2"), "Employee Pay ", MessageBoxButtons.OK,
                                MessageBoxIcon.Information)
                MessageBox.Show("Federal Deductions: " & "$" & federaldeductions.ToString("N2"), "Employee Pay ", MessageBoxButtons.OK,
                                MessageBoxIcon.Information)
                MessageBox.Show("State Deductions: " & "$" & statedeductions.ToString("N2"), "Employee Pay ", MessageBoxButtons.OK,
                                MessageBoxIcon.Information)
            ElseIf hour < 40 Then

                Dim Pay As Double = Aggregate Employees In PayrolldbDataSet.Employees
                              Where Employees.Emp_I_D.ToString = TextBox1.Text
                             Select Employees.Pay_Rate
                             Into Sum()

                Dim grosspay As Double = Aggregate Employees In PayrolldbDataSet.Employees
                             Where Employees.Emp_I_D.ToString = TextBox1.Text
                            Select Pay * hour
                            Into Sum()

                Dim federaldeductions As Double = Aggregate Employees In PayrolldbDataSet.Employees
                           Where Employees.Emp_I_D.ToString = TextBox1.Text
                          Select grosspay * federal
                          Into Sum()

                Dim statedeductions As Double = Aggregate Employees In PayrolldbDataSet.Employees
                            Where Employees.Emp_I_D.ToString = TextBox1.Text
                           Select grosspay * state
                           Into Sum()

                Dim deductions As Double = Aggregate Employees In PayrolldbDataSet.Employees
                            Where Employees.Emp_I_D.ToString = TextBox1.Text
                           Select federaldeductions + statedeductions
                           Into Sum()

                Dim Find_emp = From Employees In PayrolldbDataSet.Employees
                                Where Employees.Emp_I_D.ToString = TextBox1.Text
                             Select Employees

                EmployeesBindingSource.DataSource = Find_emp.AsDataView

                MessageBox.Show("Total Deductions: " & "$" & deductions.ToString("N2"), "Employee Pay ", MessageBoxButtons.OK,
                               MessageBoxIcon.Information)
                MessageBox.Show("Federal Deductions: " & "$" & federaldeductions.ToString("N2"), "Employee Pay ", MessageBoxButtons.OK,
                                MessageBoxIcon.Information)
                MessageBox.Show("State Deductions: " & "$" & statedeductions.ToString("N2"), "Employee Pay ", MessageBoxButtons.OK,
                                MessageBoxIcon.Information)

            End If
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub
    'button7 = displays all employees in database

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click

        Dim search = From Employees In PayrolldbDataSet.Employees
                     Select Employees

        EmployeesBindingSource.DataSource = search.AsDataView
        TextBox1.Clear()

    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        MsgBox("Enter employee I.D into textbox, for any employee you would like to receive information on. " & ControlChars.NewLine & "Next Click the corresponding button.")

    End Sub
End Class
