using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace MySavingsApp
{
    public partial class InterestCalculator : System.Web.UI.Page
    {
        static int numberOfDays, years, months, days, numberOfTimesInterestCalculatedPerYear, multiplier, groupLength, totalDays, remainingDays;
        static double totalInterest;
        static double interestRate, principal, amount, interest;

        protected void buttonclear_click(object sender, EventArgs e)
        {
            TextBoxFromDate.Text = string.Empty;
            TextBoxToDate.Text = string.Empty; ;
            TextBoxPrincipal.Text = string.Empty; 
            TextBoxInterestRate.Text = string.Empty;
            Response.Redirect("InterestCalculator.aspx");
        }

        static int rowCnt, rowCtr, cellCtr, cellCnt, rowCounterForTable, monthsToAdd, numberOfTimesTableHasToPrint;
        static string startDate, endDate;
        static DateTime fromDate, toDate, periodStartDate, periodEndDate, actualEndDate, fromDateQS;
        static string amountQS, interestRateQS, interestRateTypeQS, interestCalculationMethodQS, fromDateQSString, finalDate;

        protected void ButtonToDate_Click1(object sender, ImageClickEventArgs e)
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session.Count == 0)
            {
                Response.Redirect("Login.aspx");

            }

            if(!Page.IsPostBack)
            {
                if (Request.QueryString.Count > 0)
                {
                    string date = Request.QueryString["FromDate"];
                    DateTime tempDateTime = Convert.ToDateTime(date);
                    string shortDate = tempDateTime.ToShortDateString();
                    if (tempDateTime.Day < 10)
                    {
                        if (tempDateTime.Month < 10)
                        {
                            string tempDateTimeString = tempDateTime.ToShortDateString();
                            finalDate = DateTime.ParseExact(tempDateTimeString, "M/d/yyyy", CultureInfo.InvariantCulture).ToString("MM-dd-yyyy");
                        }

                        if (tempDateTime.Month >= 10)
                        {
                            string tempDateTimeString = tempDateTime.ToShortDateString();
                            finalDate = DateTime.ParseExact(tempDateTimeString, "MM/d/yyyy", CultureInfo.InvariantCulture).ToString("MM-dd-yyyy");
                        }

                    }

                    if (tempDateTime.Day > 10)
                    {
                        if (tempDateTime.Month < 10)
                        {
                            string tempDateTimeString = tempDateTime.ToShortDateString();
                            finalDate = DateTime.ParseExact(tempDateTimeString, "M/dd/yyyy", CultureInfo.InvariantCulture).ToString("MM-dd-yyyy");
                        }

                        if (tempDateTime.Month >= 10)
                        {
                            string tempDateTimeString = tempDateTime.ToShortDateString();
                            finalDate = DateTime.ParseExact(tempDateTimeString, "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MM-dd-yyyy");
                        }
                    }

                    TextBoxFromDate.Text = finalDate;

                    //string date = Request.QueryString["FromDate"];
                    /***fromDateQSString = Request.QueryString["FromDate"];***/
                    amountQS = Request.QueryString["Amount"];
                    interestRateQS = Request.QueryString["intRate"];
                    interestRateTypeQS = Request.QueryString["intType"];

                    //TextBoxFromDate.Text = formattedDate;
                    /****TextBoxFromDate.Text = fromDateQSString;****/
                    //TextBoxFromDate.Text = date;
                    TextBoxPrincipal.Text = amountQS;
                    TextBoxInterestRate.Text = interestRateQS;
                    RadioButtonListInterestType.SelectedValue = interestRateTypeQS;
                    if (RadioButtonListInterestType.SelectedValue != "Simple Interest")
                    {
                        interestCalculationMethodQS = Request.QueryString["calcType"];
                        DropDownListInterestCalculationMethod.SelectedValue = interestCalculationMethodQS;
                    }
                }
            }

            
            if (RadioButtonListInterestType.SelectedValue == "Compound Interest")
            {
                DropDownListInterestCalculationMethod.Enabled = true;
                DropDownListInterestCalculationMethod.Visible = true;
            }

            else
            {
                DropDownListInterestCalculationMethod.Enabled = false;
                DropDownListInterestCalculationMethod.Visible = false;
            }

        }

        //protected void CalendarFrom_SelectionChanged(object sender, EventArgs e)
        //{
        //    TextBoxFromDate.Text = CalendarFrom.SelectedDate.ToShortDateString();
        //    CalendarFrom.Visible = false;
        //}

        //protected void CalendarTo_SelectionChanged(object sender, EventArgs e)
        //{
        //    TextBoxToDate.Text = CalendarTo.SelectedDate.ToShortDateString();
        //    CalendarTo.Visible = false;
        //}


        //protected void TextBoxFromDate_Click(object sender, EventArgs e)
        //{
        //    if (CalendarFrom.Visible == true)
        //    {
        //        CalendarFrom.Visible = false;
        //    }
        //    else
        //    {
        //        CalendarFrom.Visible = true;
        //    }
        //}


        protected void DropDownListInterestCalculationMethod_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        static string interestType, interestCalculationMethod;


        //Calculating SI and CI.
        protected void ButtonCompute_Click(object sender, EventArgs e)
        {

            principal = Convert.ToDouble(TextBoxPrincipal.Text);
            //Response.Write(principal);
            interestRate = Convert.ToDouble(TextBoxInterestRate.Text);
            interestType = RadioButtonListInterestType.SelectedValue;
            interestCalculationMethod = DropDownListInterestCalculationMethod.SelectedValue;

            //numberOfDays = (Convert.ToInt32((CalendarTo.SelectedDate - CalendarFrom.SelectedDate).TotalDays)) + 1;

            /*Bootstrap Datepicker*/

            fromDate = Convert.ToDateTime(TextBoxFromDate.Text);
            toDate = Convert.ToDateTime(TextBoxToDate.Text);

            //if(toDate < fromDate)
            //{
            //    if(Request.QueryString.Count > 0)
            //    {
            //        LabelCalendarStatus.Text = "WARNING: To Date cannot be before Start Date!";
            //        TextBoxToDate.Text = string.Empty;
            //        return;
            //    }
            //    else
            //    {
            //        LabelCalendarStatus.Text = "WARNING: To Date cannot be before Start Date!";
            //        TextBoxFromDate.Text = string.Empty;
            //        TextBoxToDate.Text = string.Empty;
            //        TextBoxPrincipal.Text = string.Empty;
            //        TextBoxInterestRate.Text = string.Empty;
            //        RadioButtonListInterestType.SelectedValue = "Simple Interest";
            //        DropDownListInterestCalculationMethod.Visible = false;
            //        return;
            //    }
                
            //}


            int numberOfDays = Convert.ToInt32((toDate - fromDate).TotalDays);

            startDate = fromDate.ToShortDateString();
            endDate = toDate.ToShortDateString();

            //Response.Write(numberOfDays);

            years = (numberOfDays / 365);
            months = (numberOfDays % 365) / 30;
            days = ((numberOfDays % 365) % 30);

            totalDays = numberOfDays;

            if (RadioButtonListInterestType.SelectedValue == "Simple Interest")
            {
                totalInterest = CalculateSimpleInterest();
            }

            else if (interestType == "Compound Interest")
            {
                totalInterest = CalculateCompoundInterest();
            }

            //Printing the results in the corresponding TextBoxes
            //TextBoxTotalDays.Text = numberOfDays.ToString();
            //TextBoxYears.Text = years.ToString();
            //TextBoxMonths.Text = months.ToString();
            //TextBoxDays.Text = days.ToString();
            //TextBoxTotalInterest.Text = totalInterest.ToString("#,0.00");

            //Response.Write("<hr>");
            //Response.Write("Total Interest: ");
            //Response.Write(totalInterest);
            //Response.Write("<br/>");

        }

        //Clearing the contents of the form. 
        protected void ButtonClear_Click(object sender, EventArgs e)
        {
            //TextBoxFromDate.Text = string.Empty;
            //TextBoxToDate.Text = string.Empty; ;
            //TextBoxPrincipal.Text = string.Empty; ;
            //TextBoxInterestRate.Text = string.Empty;
            //TextBoxTotalDays.Text = string.Empty;
            //TextBoxYears.Text = string.Empty;
            //TextBoxMonths.Text = string.Empty;
            //TextBoxDays.Text = string.Empty;
            //TextBoxTotalInterest.Text = string.Empty;
        }

        //Calculating Simple Interest
        protected double CalculateSimpleInterest()
        {
            //double interestYears = (principal * years * interestRate) / 100;
            //double interestMonths = (principal * (months / 12.0) * interestRate) / 100;
            //double interestDays = (principal * (days / 365.0) * interestRate) / 100;

            //double simpleInterest = interestYears + interestMonths + interestDays;

            //periodStartDate = Convert.ToDateTime(fromDate);
            //actualEndDate = Convert.ToDateTime(toDate);

            numberOfTimesTableHasToPrint = 1;
            for(int i = 1; i<=numberOfTimesTableHasToPrint; i++)
            {
                int mainRowCnt;
                int mainRowCtr;
                int mainCellCtr;
                int mainCellCnt;

                mainRowCnt = 3;
                mainCellCnt = 2;

                for (mainRowCtr = 1; mainRowCtr <= mainRowCnt; mainRowCtr++)
                {
                    if (mainRowCtr == 1)
                    {
                        TableRow mainTableRow = new TableRow();
                        MainTable.Rows.Add(mainTableRow);
                        for (mainCellCtr = 1; mainCellCtr <= mainCellCnt; mainCellCtr++)
                        {
                            if (mainCellCtr % 2 != 0)
                            {
                                TableCell mainTableCell = new TableCell();
                                mainTableCell.Text = "Amount";
                                mainTableRow.Cells.Add(mainTableCell);
                            }
                            else if (mainCellCtr % 2 == 0)
                            {
                                TableCell mainTableCell = new TableCell();
                                mainTableCell.Text = principal.ToString("#,0.00");
                                mainTableRow.Cells.Add(mainTableCell);
                                mainTableCell.Attributes.Add("align", "right");

                            }
                        }
                    }

                    else if (mainRowCtr == 2)
                    {
                        TableRow mainTableRow = new TableRow();
                        MainTable.Rows.Add(mainTableRow);
                        for (mainCellCtr = 1; mainCellCtr <= mainCellCnt; mainCellCtr++)
                        {
                            if (mainCellCtr % 2 != 0)
                            {
                                TableCell mainTableCell = new TableCell();
                                string periodStartDate = fromDate.ToShortDateString();
                                string periodEndDate = toDate.ToShortDateString();

                                string dateString = periodStartDate + " - " + periodEndDate + "     ";

                                mainTableCell.Text = dateString;
                                mainTableRow.Cells.Add(mainTableCell);
                            }
                            else if (mainCellCtr % 2 == 0)
                            {
                                interest = 0;
                                double interestYears = (principal * years * interestRate) / 100;
                                double interestMonths = (principal * (months / 12.0) * interestRate) / 100;
                                double interestDays = (principal * (days / 365.0) * interestRate) / 100;

                                double simpleInterest = interestYears + interestMonths + interestDays;
                                interest = simpleInterest;
                                amount = principal + interest;

                                TableCell mainTableCell = new TableCell();
                                string interestToBePrinted = interest.ToString("#,0.00");
                                mainTableCell.Text = interestToBePrinted;
                                mainTableRow.Cells.Add(mainTableCell);
                                mainTableCell.Attributes.Add("align", "right");
                            }
                        }
                    }
                    else if (mainRowCtr == 3)
                    {
                        TableRow mainTableRow = new TableRow();
                        MainTable.Rows.Add(mainTableRow);
                        for (mainCellCtr = 1; mainCellCtr <= mainCellCnt; mainCellCtr++)
                        {
                            if(mainCellCtr %2 != 0)
                            {
                                TableCell mainTableCell = new TableCell();
                                mainTableCell.Text = "Amount";
                                mainTableRow.Cells.Add(mainTableCell);
                            }
                            else if(mainCellCtr %2 == 0)
                            {
                                TableCell mainTableCell = new TableCell();
                                mainTableCell.Text = amount.ToString("#,0.00");
                                mainTableRow.Cells.Add(mainTableCell);
                                mainTableCell.Attributes.Add("align", "right");
                            }
                        }
                    }
                }
            }

            return interest;
        }

        //Calculating Compound Interest
        protected double CalculateCompoundInterest()
        {
            totalInterest = 0.0;
            rowCounterForTable = 0;
            amount = principal;

            if (interestCalculationMethod == "Quarterly")
            {
                numberOfTimesInterestCalculatedPerYear = 4;
                groupLength = 91;
                monthsToAdd = 3;
            }

            else if (interestCalculationMethod == "Half Yearly")
            {
                numberOfTimesInterestCalculatedPerYear = 2;
                groupLength = 182;
                monthsToAdd = 6;
            }

            else if (interestCalculationMethod == "Yearly")
            {
                numberOfTimesInterestCalculatedPerYear = 1;
                groupLength = 365;
                monthsToAdd = 12;
            }

            /* *** DURATION TABLE *** */
            int durationRowCount, durationRowCounter, durationCellCount, durationCellCounter;

            durationRowCount = 1;   //Number of Rows
            durationCellCount = 2;

            for (durationRowCounter = 1; durationRowCounter <= durationRowCount; durationRowCounter++)
            {
                TableRow durationTableRow = new TableRow();
                TableDuration.Rows.Add(durationTableRow);
                for (durationCellCounter = 1; durationCellCounter <= durationCellCount; durationCellCounter++)
                {
                    if (durationCellCounter == 1)
                    {
                        TableCell durationTableCell = new TableCell();
                        durationTableCell.Text = "Duration: ";
                        durationTableRow.Cells.Add(durationTableCell);
                    }
                    else if (durationCellCounter == 2)
                    {
                        TableCell durationTableCell = new TableCell();
                        string duration = years + "Y. " + months + "M. " + days + "D.";
                        durationTableCell.Text = duration;
                        durationTableRow.Cells.Add(durationTableCell);
                        durationTableCell.Attributes.Add("align", "right");

                    }
                }
            }

            /* *** MAIN TABLE *** */

            periodStartDate = Convert.ToDateTime(fromDate);
            actualEndDate = Convert.ToDateTime(toDate);

            numberOfTimesTableHasToPrint = (years * numberOfTimesInterestCalculatedPerYear);
            for(int i=1; i<=numberOfTimesTableHasToPrint; i++)
            {

                int mainRowCnt;
                int mainRowCtr;
                int mainCellCtr;
                int mainCellCnt;

                mainRowCnt = 2;
                mainCellCnt = 2;

                

                for (mainRowCtr = 1; mainRowCtr <= mainRowCnt; mainRowCtr++)
                {
                    if (mainRowCtr % 2 != 0)
                    {
                        TableRow mainTableRow = new TableRow();
                        MainTable.Rows.Add(mainTableRow);
                        for (mainCellCtr = 1; mainCellCtr <= mainCellCnt; mainCellCtr++)
                        {
                            if (mainCellCtr % 2 != 0)
                            {
                                if(i == 1)
                                {
                                    TableCell mainTableCell = new TableCell();
                                    mainTableCell.Text = "Capital Amount";
                                    mainTableRow.Cells.Add(mainTableCell);
                                }
                                else
                                {
                                    TableCell mainTableCell = new TableCell();
                                    mainTableCell.Text = "Amount";
                                    mainTableRow.Cells.Add(mainTableCell);
                                    //mainTableCell.Attributes.Add("align", "right");
                                }
                            }

                            else if (mainCellCtr % 2 == 0)
                            {
                                TableCell mainTableCell = new TableCell();
                                mainTableCell.Text = amount.ToString("#,0.00");
                                mainTableRow.Cells.Add(mainTableCell);
                                mainTableCell.Attributes.Add("align", "right");
                            }
                        }
                    }

                    else if (mainRowCtr % 2 == 0)
                    {

                        //tempEndDate = endDate;
                        TableRow mainTableRow = new TableRow();
                        MainTable.Rows.Add(mainTableRow);
                        for (mainCellCtr = 1; mainCellCtr <= mainCellCnt; mainCellCtr++)
                        {
                            if (mainCellCtr % 2 != 0)
                            {
                                periodEndDate = periodStartDate.AddMonths(monthsToAdd);
                                TableCell mainTableCell = new TableCell();
                                string periodStartDateString = periodStartDate.ToShortDateString();
                                string periodEndDateString = periodEndDate.ToShortDateString();
                                string dateString = "Interest amount for period " + periodStartDateString + " - " + periodEndDateString + "     ";

                                mainTableCell.Text = dateString;
                                mainTableRow.Cells.Add(mainTableCell);

                                periodStartDate = periodEndDate.AddDays(1);
                                periodEndDate = periodEndDate.AddMonths(monthsToAdd);

                            }

                            else if (mainCellCtr % 2 == 0)
                            {
                                
                                interest = 0;
                                interest = amount * (interestRate / 100.0) * (groupLength / 365.0);
                                amount = amount + interest;
                                totalInterest = totalInterest + interest;
                                totalDays = totalDays - groupLength;

                                TableCell mainTableCell = new TableCell();
                                string interestToBePrinted = interest.ToString("#,0.00");
                                mainTableCell.Text = interestToBePrinted;
                                mainTableRow.Cells.Add(mainTableCell);
                                mainTableCell.Attributes.Add("align", "right");

                            }
                        }
                    }

                }

            }

            /* ************* */
            /* Months */
            /* ************* */

            numberOfTimesTableHasToPrint = (months / monthsToAdd);
            for(int i = 1;i<numberOfTimesTableHasToPrint;i++)
            {
                int monthsRowCnt;
                int monthsRowCtr;
                int monthsCellCtr;
                int monthsCellCnt;

                monthsRowCnt = 2;
                monthsCellCnt = 2;

                for(monthsRowCtr = 1; monthsRowCtr <= monthsRowCnt; monthsRowCtr++)
                {
                    if(monthsRowCtr % 2 != 0)
                    {
                        TableRow monthsTableRow = new TableRow();
                        TableMonths.Rows.Add(monthsTableRow);
                        for (monthsCellCtr = 1; monthsCellCtr <= monthsCellCnt; monthsCellCtr++)
                        {
                            if(monthsCellCtr %2 != 0)
                            {
                                TableCell monthsTableCell = new TableCell();
                                monthsTableCell.Text = "Amount";
                                monthsTableRow.Cells.Add(monthsTableCell);
                            }
                            else if(monthsCellCtr %2 == 0)
                            {
                                TableCell monthsTableCell = new TableCell();
                                monthsTableCell.Text = amount.ToString("#,0.00");
                                monthsTableRow.Cells.Add(monthsTableCell);
                                monthsTableCell.Attributes.Add("align", "right");
                            }
                        }
                    }
                    else if(monthsRowCtr % 2 == 0)
                    {
                        TableRow monthsTableRow = new TableRow();
                        TableMonths.Rows.Add(monthsTableRow);
                        for(monthsCellCtr = 1; monthsCellCtr <= monthsCellCnt; monthsCellCtr++)
                        {
                            if (monthsCellCtr % 2 != 0)
                            {
                                periodEndDate = periodStartDate.AddMonths(monthsToAdd);
                                TableCell monthsTableCell = new TableCell();
                                string periodStartDateString = periodStartDate.ToShortDateString();
                                string periodEndDateString = periodEndDate.ToShortDateString();
                                string dateString = "Interest amount for period " + periodStartDateString + " - " + periodEndDateString + "     ";

                                monthsTableCell.Text = dateString;
                                monthsTableRow.Cells.Add(monthsTableCell);

                                periodStartDate = periodEndDate.AddDays(1);
                                periodEndDate = periodEndDate.AddMonths(monthsToAdd);
                            }

                            else if(monthsCellCtr % 2 == 0)
                            {
                                if (totalDays >= groupLength)
                                {
                                    interest = 0;
                                    interest = amount * (interestRate / 100.0) * (groupLength / 365.0);
                                    amount = amount + interest;
                                    totalInterest = totalInterest + interest;
                                    totalDays = totalDays - groupLength;
                                    
                                    TableCell monthsTableCell = new TableCell();
                                    string interestToBePrinted = interest.ToString("#,0.00");
                                    monthsTableCell.Text = interestToBePrinted;
                                    monthsTableRow.Cells.Add(monthsTableCell);
                                    monthsTableCell.Attributes.Add("align", "right");
                                }
                            }
                        }
                    }
                }                    
            }

            ///* *** Days *** */
            ///* *** **** *** */

            //numberOfTimesTableHasToPrint = 1;       //Because the days will always be less than group length. 
            //for(int i = 0; i<numberOfTimesTableHasToPrint;i++)
            //{
            //    int daysRowCnt;
            //    int daysRowCtr;
            //    int daysCellCtr;
            //    int daysCellCnt;

            //    daysRowCnt = 2;
            //    daysCellCnt = 2;

            //    for(daysRowCtr = 1; daysRowCtr <= daysRowCnt; daysRowCtr++)
            //    {
            //        if(daysRowCtr % 2 != 0)
            //        {
            //            TableRow daysTableRow = new TableRow();
            //            TableDays.Rows.Add(daysTableRow);
            //            for(daysCellCtr = 1; daysCellCtr <= daysCellCnt; daysCellCtr++)
            //            {
            //                if(daysCellCtr %2 != 0)
            //                {
            //                    TableCell daysTableCell = new TableCell();
            //                    daysTableCell.Text = "Capital Amount";
            //                    daysTableRow.Cells.Add(daysTableCell);
            //                }
            //                else if(daysCellCtr %2 == 0)
            //                {
            //                    TableCell daysTableCell = new TableCell();
            //                    daysTableCell.Text = amount.ToString();
            //                    daysTableRow.Cells.Add(daysTableCell);
            //                }
            //            }
            //        }
            //        else if(daysRowCtr % 2 == 0)
            //        {
            //            TableRow daysTableRow = new TableRow();
            //            TableDays.Rows.Add(daysTableRow);
            //            for (daysCellCtr = 1; daysCellCtr <= daysCellCnt; daysCellCtr++)
            //            {
            //                if(daysCellCtr % 2 != 0)
            //                {
            //                    periodEndDate = periodStartDate.AddMonths(monthsToAdd);
            //                    TableCell daysTableCell = new TableCell();
            //                    string periodStartDateString = periodStartDate.ToShortDateString();
            //                    string periodEndDateString = periodEndDate.ToShortDateString();
            //                    string dateString = periodStartDateString + " - " + periodEndDateString;

            //                    daysTableCell.Text = dateString;
            //                    daysTableRow.Cells.Add(daysTableCell);

            //                    periodStartDate = periodEndDate.AddDays(1);
            //                    periodEndDate = periodEndDate.AddDays(totalDays);
            //                }

            //                else if(daysCellCtr %2 == 0)
            //                {
            //                    interest = 0;
            //                    interest = amount * (interestRate / 100.0) * (totalDays / 365.0);
            //                    amount = amount + interest;
            //                    totalInterest = totalInterest + interest;

            //                    TableCell daysTableCell = new TableCell();
            //                    string interestToBePrinted = interest.ToString("#,0.00");
            //                    daysTableCell.Text = interestToBePrinted;
            //                    daysTableRow.Cells.Add(daysTableCell);
            //                }
            //            }
            //        }
            //    }
            //}

            //Last
            numberOfTimesTableHasToPrint = 1;
            int lastRowCnt;
            int lastRowCtr;
            int lastCellCnt;
            int lastCellCtr;

            lastRowCnt = 3;
            lastCellCnt = 2;

            for(lastRowCtr = 1; lastRowCtr <= lastRowCnt; lastRowCtr++)
            {
                if(lastRowCtr == 1)
                {
                    TableRow lastTableRow = new TableRow();
                    LastTable.Rows.Add(lastTableRow);
                    for(lastCellCtr = 1; lastCellCtr <= lastCellCnt; lastCellCtr++)
                    {
                        if(lastCellCtr %2 != 0)
                        {
                            TableCell lastTableCell = new TableCell();
                            lastTableCell.Text = "Amount";
                            lastTableRow.Cells.Add(lastTableCell);
                        }
                        else if(lastCellCtr %2 == 0)
                        {
                            TableCell lastTableCell = new TableCell();
                            lastTableCell.Text = amount.ToString("#,0.00");
                            lastTableRow.Cells.Add(lastTableCell);
                            lastTableCell.Attributes.Add("align", "right");
                        }
                    }
                }
                else if(lastRowCtr == 2)        //2nd Row
                {
                    TableRow lastTableRow = new TableRow();
                    LastTable.Rows.Add(lastTableRow);
                    for(lastCellCtr = 1; lastCellCtr <= lastCellCnt; lastCellCtr++)
                    {
                        if(lastCellCtr % 2 != 0)
                        {
                            //periodStartDate = periodEndDate.AddDays(1);
                            periodEndDate = toDate;
                            TableCell lastTableCell = new TableCell();
                            string periodStartDateString = periodStartDate.ToShortDateString();
                            string periodEndDateString = periodEndDate.ToShortDateString();
                            string dateString = "Interest amount for period " + periodStartDateString + " - " + periodEndDateString + "     ";

                            lastTableCell.Text = dateString;
                            lastTableRow.Cells.Add(lastTableCell);
                        }
                        else if(lastCellCtr %2 == 0)        //2nd Row, 2nd Column
                        {
                            if (totalDays >= groupLength)
                            {
                                interest = 0;
                                interest = amount * (interestRate / 100.0) * (groupLength / 365.0);
                                amount = amount + interest;
                                totalInterest = totalInterest + interest;
                                totalDays = totalDays - groupLength;

                                TableCell lastTableCell = new TableCell();
                                string interestToBePrinted = interest.ToString("#,0.00");
                                lastTableCell.Text = interestToBePrinted;
                                lastTableRow.Cells.Add(lastTableCell);
                                lastTableCell.Attributes.Add("align", "right");

                            }
                            else
                            {
                                interest = 0;
                                interest = amount * (interestRate / 100.0) * (totalDays / 365.0);
                                amount = amount + interest;
                                totalInterest = totalInterest + interest;

                                TableCell lastTableCell = new TableCell();
                                string interestToBePrinted = interest.ToString("#,0.00");
                                lastTableCell.Text = interestToBePrinted;
                                lastTableRow.Cells.Add(lastTableCell);
                                lastTableCell.Attributes.Add("align", "right");
                            }

                            
                        }

                    }
                }
                else if(lastRowCtr == 3)        //3rd Row
                {
                    TableRow lastTableRow = new TableRow();
                    LastTable.Rows.Add(lastTableRow);
                    for(lastCellCtr = 1; lastCellCtr <= lastCellCnt; lastCellCtr++)
                    {
                        if(lastCellCtr %2 != 0)
                        {
                            TableCell lastTableCell = new TableCell();
                            lastTableCell.Text = "Amount";
                            lastTableRow.Cells.Add(lastTableCell);
                        }
                        else if(lastCellCtr %2 == 0)
                        {
                            TableCell lastTableCell = new TableCell();
                            lastTableCell.Text = amount.ToString("#,0.00");
                            lastTableRow.Cells.Add(lastTableCell);
                            lastTableCell.Attributes.Add("align", "right");
                        }
                    }
                }
            }


            return totalInterest;
        }
    }
}




            